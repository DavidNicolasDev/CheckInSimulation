using CheckInSimulation.Data;
using CheckInSimulation.Models;
using CheckInSimulation.Models.Dtos;

namespace CheckInSimulation.Services
{
    public class SeatAssignmentService
    {
        /// Asigna asientos en memoria para cada boarding pass que no tenga SeatId
        /// /// <param name="airplaneId">ID del avión</param>
        /// <param name="passengers">Lista de pasajeros con su boarding pass</param>
        /// <param name="availableSeats">Asientos disponibles del avión y clase</param>
        /// <returns>Lista de pasajeros con seatId asignado</returns>
        /// 
        public List<PassengerDto> AssignSeats(int airplaneId, List<PassengerDto> passengers, List<Seat> availableSeats)
        {
            var assignedPassengers = passengers.Select(p => new PassengerDto
            {
                PassengerId = p.PassengerId,
                Dni = p.Dni,
                Name = p.Name,
                Age = p.Age,
                Country = p.Country,
                BoardingPassId = p.BoardingPassId,
                PurchaseId = p.PurchaseId,
                SeatTypeId = p.SeatTypeId,
                SeatId = p.SeatId // Inicialmente sin asiento asignado
            }).ToList();

            // TODO: Implementar lógica de asignación
            AssignSeatByRules(airplaneId, assignedPassengers, availableSeats);

            return assignedPassengers;
        }

        /// Lógica principal para asignar asientos según reglas de negocio
        /// 

        private void AssignSeatByRules(int airplaneId, List<PassengerDto> passengers, List<Seat> availableSeats)
        {
            Console.WriteLine($"[INFO] Iniciando asignación de asientos para el avión {airplaneId}...");
            Console.WriteLine($"[INFO] Total pasajeros: {passengers.Count}");
            Console.WriteLine($"[INFO] Total asientos disponibles: {availableSeats.Count}");

            // Obtener layout del avión
            var layout = SeatLayouts.Layouts.FirstOrDefault(l => l.AirplaneId == airplaneId);
            if (layout==null)
            {
                throw new Exception($"No layout found for airplaneId {airplaneId}");
            }


            // Asignación por grupo de compra
            var groupedPurchases = passengers.GroupBy(p => p.PurchaseId).ToList();

            foreach (var group in groupedPurchases)
            {
                Console.WriteLine($"\n[GROUP] Procesando grupo de compra: {group.Key}");
                var groupPassengers = group.ToList();

                // Tomar el seatTypeId del primer pasajero del grupo
                var seatTypeId = groupPassengers.First().SeatTypeId;

                // Buscar configuración de clase
                if (!layout.Classes.TryGetValue(seatTypeId, out var seatConfig))
                {
                    Console.WriteLine($"[WARN] No layout encontrado para seatTypeId {seatTypeId}");
                    continue;
                }
                    

                // Filtrar asientos válidos para esa clase
                var classSeats = availableSeats
                    .Where(s => s.SeatTypeId == seatTypeId &&
                                seatConfig.Rows.Contains(s.SeatRow))
                    .OrderBy(s => s.SeatRow)
                    .ThenBy(s => s.SeatColumn)
                    .ToList();

                if (!classSeats.Any())
                {
                    Console.WriteLine($"No hay asientos disponibles para clase {seatTypeId}");
                    continue;
                }

                Console.WriteLine($"[INFO] Pasajeros en grupo: {groupPassengers.Count}");
                Console.WriteLine($"[INFO] Asientos disponibles en clase {seatTypeId}: {classSeats.Count}");

                // Separar adultos y menores
                var minors = groupPassengers.Where(p => p.Age < 18).ToList();
                var adults = groupPassengers.Where(p => p.Age >= 18).ToList();

                // 1. Priorizar asignación de menores con adultos
                foreach (var minor in minors)
                {
                    var availableAdult = adults.FirstOrDefault(a => a.SeatId == null);
                    if (availableAdult == null) continue;

                    // Buscar par de asientos contiguos
                    var adultSeat = classSeats.FirstOrDefault();
                    if (adultSeat == null) break;

                    var adjacentSeat = FindAdjacentSeat(adultSeat, classSeats, seatConfig);

                    if (adjacentSeat != null)
                    {
                        Console.WriteLine($"[ASSIGN] Adulto {availableAdult.Name} -> asiento {adultSeat.SeatRow}{adultSeat.SeatColumn}");
                        Console.WriteLine($"[ASSIGN] Menor {minor.Name} -> asiento contiguo {adjacentSeat.SeatRow}{adjacentSeat.SeatColumn}");

                        // Asignar al adulto
                        availableAdult.SeatId = adultSeat.SeatId;
                        classSeats.Remove(adultSeat);
                        availableSeats.Remove(adultSeat);

                        // Asignar al menor contiguo
                        minor.SeatId = adjacentSeat.SeatId;
                        classSeats.Remove(adjacentSeat);
                        availableSeats.Remove(adjacentSeat);
                    }
                    else
                    {

                        Console.WriteLine($"[FALLBACK] No se encontró asiento contiguo para menor {minor.Name}, asignando el siguiente libre.");
                        // Si no hay contigüidad, asignar el primero libre
                        availableAdult.SeatId = adultSeat.SeatId;
                        classSeats.Remove(adultSeat);
                        availableSeats.Remove(adultSeat);

                        var fallbackSeat = classSeats.FirstOrDefault();
                        if (fallbackSeat != null)
                        {

                            minor.SeatId = fallbackSeat.SeatId;
                            Console.WriteLine($"[ASSIGN] Menor {minor.Name} -> asiento {fallbackSeat.SeatRow}{fallbackSeat.SeatColumn}");
                            classSeats.Remove(fallbackSeat);
                            availableSeats.Remove(fallbackSeat);
                        }
                    }
                }

                // 2. Asignar adultos restantes
                foreach (var adult in adults.Where(a => a.SeatId == null))
                {
                    var seat = classSeats.FirstOrDefault();
                    if (seat == null) break;

                    Console.WriteLine($"[ASSIGN] Adulto {adult.Name} -> asiento {seat.SeatRow}{seat.SeatColumn}");
                    adult.SeatId = seat.SeatId;
                    classSeats.Remove(seat);
                    availableSeats.Remove(seat);
                }

                // 3. Asignar menores restantes (sin adultos disponibles)
                foreach (var minor in minors.Where(m => m.SeatId == null))
                {
                    var seat = classSeats.FirstOrDefault();
                    if (seat == null) break;

                    Console.WriteLine($"[ASSIGN] Menor {minor.Name} -> asiento {seat.SeatRow}{seat.SeatColumn}");
                    minor.SeatId = seat.SeatId;
                    classSeats.Remove(seat);
                    availableSeats.Remove(seat);
                }


            }

            Console.WriteLine("[INFO] Asignación finalizada.\n");
            
        }

        /// Busca un asiento contiguo a otro, usando el layout configurado
        /// 
        private Seat? FindAdjacentSeat(Seat adultSeat, List<Seat> availableSeats, SeatConfig seatConfig)
        {
            // Obtener grupos de columnas de esa clase
            foreach (var group in seatConfig.ColumnGroups)
            {
                if (group.Contains(adultSeat.SeatColumn))
                {
                    // Buscar columnas del mismo grupo en la misma fila
                    foreach (var column in group)
                    {
                        if (column == adultSeat.SeatColumn) continue;

                        var adjacent = availableSeats
                            .FirstOrDefault(s => s.SeatRow == adultSeat.SeatRow && s.SeatColumn == column);

                        if (adjacent != null)
                            return adjacent;
                    }
                }
            }

            return null;
        }
    }
}
