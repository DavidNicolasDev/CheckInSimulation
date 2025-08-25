using CheckInSimulation.Data;
using CheckInSimulation.Models;
using CheckInSimulation.Models.Dtos;
using CheckInSimulation.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CheckInSimulation.Controllers
{
    [ApiController]
    [Route("flights")]

    public class FlightsController : ControllerBase
    {
        private readonly AirlineDbContext _context;
        private readonly SeatAssignmentService _seatAssignmentService;

        public FlightsController(AirlineDbContext context, SeatAssignmentService seatAssignmentService)
        {
            _context = context;
            _seatAssignmentService = seatAssignmentService;
        }

        [HttpGet("{id}/passengers")]
        public async Task<IActionResult> GetFlightWithPassengers(int id)
        {
            try
            {
                var flight = await _context.Flights.FirstOrDefaultAsync(f => f.FlightId == id);

                if (flight == null)
                {
                    return NotFound(new
                    {
                        code = 404,
                        data = new { }
                    });

                }
                // Traer pasajeros relacionados con ese vuelo
                var passangers = await (from bp in _context.BoardingPasses
                                        join p in _context.Passengers on bp.PassengerId equals p.PassengerId
                                        where bp.FlightId == id
                                        select new PassengerDto
                                        {
                                            PassengerId = p.PassengerId,
                                            Dni = p.Dni,
                                            Name = p.Name,
                                            Age = p.Age,
                                            Country = p.Country,
                                            BoardingPassId = bp.BoardingPassId,
                                            PurchaseId = bp.PurchaseId,
                                            SeatTypeId = bp.SeatTypeId,
                                            SeatId = bp.SeatId
                                        }).ToListAsync();


                // Filtrar asientos disponibles de ese avión
                var availableSeats = await _context.Seats
                    .Where(s => s.AirplaneId == flight.AirplaneId)
                    .ToListAsync();
                // Asignar asientos en memoria usando el servicio
                var assignedPassengers = _seatAssignmentService.AssignSeats(flight.AirplaneId, passangers, availableSeats);


                return Ok(new
                {
                    code = 200,
                    data = new
                    {
                        flightId = flight.FlightId,
                        takeoffDateTime = flight.TakeoffDateTime,
                        takeoffAirport = flight.TakeoffAirport,
                        landingDateTime = flight.LandingDateTime,
                        landingAirport = flight.LandingAirport,
                        airplaneId = flight.AirplaneId,
                        passangers = assignedPassengers
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new
                {
                    code = 400,
                    message = "could not connect to db",
                    details = ex.Message
                });
            }
        }

        [HttpGet("test/assign-seats")]
        public IActionResult TestSeatAssignment()
        {
            // Simulación de pasajeros en un mismo grupo de compra
            var passengers = new List<PassengerDto>
            {
                new PassengerDto
                {
                    PassengerId = 1,
                    Dni = "123",
                    Name = "Adulto 1",
                    Age = 35,
                    Country = "Chile",
                    BoardingPassId = 100,
                    PurchaseId = 500,
                    SeatTypeId = 3, // Clase económica
                    SeatId = null
                },
                new PassengerDto
                {
                    PassengerId = 2,
                    Dni = "124",
                    Name = "Menor 1",
                    Age = 12,
                    Country = "Chile",
                    BoardingPassId = 101,
                    PurchaseId = 500,
                    SeatTypeId = 3,
                    SeatId = null
                },
                new PassengerDto
                {
                    PassengerId = 3,
                    Dni = "125",
                    Name = "Adulto 2",
                    Age = 40,
                    Country = "Chile",
                    BoardingPassId = 102,
                    PurchaseId = 500,
                    SeatTypeId = 3,
                    SeatId = null
                }
            };

            // Simulación de asientos disponibles para AirNova-660
            var seats = new List<Seat>
            {
                new Seat { SeatId = 1, SeatRow = 20, SeatColumn = "A", SeatTypeId = 3, AirplaneId = 1 },
                new Seat { SeatId = 2, SeatRow = 20, SeatColumn = "B", SeatTypeId = 3, AirplaneId = 1 },
                new Seat { SeatId = 3, SeatRow = 20, SeatColumn = "C", SeatTypeId = 3, AirplaneId = 1 },
                new Seat { SeatId = 4, SeatRow = 20, SeatColumn = "E", SeatTypeId = 3, AirplaneId = 1 },
                new Seat { SeatId = 5, SeatRow = 20, SeatColumn = "F", SeatTypeId = 3, AirplaneId = 1 },
                new Seat { SeatId = 6, SeatRow = 20, SeatColumn = "G", SeatTypeId = 3, AirplaneId = 1 }
            };

            // Llamar al servicio para asignar
            var assigned = _seatAssignmentService.AssignSeats(
                1, // AirplaneId para AirNova-660
                passengers,
                seats
            );

            return Ok(new
            {
                code = 200,
                data = assigned
            });
        }

    }
}
