using System.ComponentModel.DataAnnotations.Schema;

namespace CheckInSimulation.Models
{
    [Table("boarding_pass")]
    public class BoardingPass
    {
        [Column("boarding_pass_id")]
        public int BoardingPassId { get; set; }

        [Column("purchase_id")]
        public int PurchaseId { get; set; }

        [Column("passenger_id")]
        public int PassengerId { get; set; }

        [Column("seat_type_id")]
        public int SeatTypeId { get; set; }

        [Column("seat_id")]
        public int? SeatId { get; set; }

        [Column("flight_id")]
        public int FlightId { get; set; }

        // Relaciones
        public Purchase Purchase { get; set; }
        public Passenger Passenger { get; set; }
        public SeatType SeatType { get; set; }
        public Seat Seat { get; set; }
        public Flight Flight { get; set; }
    }
}
