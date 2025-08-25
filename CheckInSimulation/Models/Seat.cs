using System.ComponentModel.DataAnnotations.Schema;

namespace CheckInSimulation.Models
{
    [Table("seat")]
    public class Seat
    {
        [Column("seat_id")]
        public int SeatId { get; set; }

        [Column("seat_column")]
        public string SeatColumn { get; set; }

        [Column("seat_row")]
        public int SeatRow { get; set; }

        [Column("seat_type_id")]
        public int SeatTypeId { get; set; }

        [Column("airplane_id")]
        public int AirplaneId { get; set; }

        // Relaciones
        public Airplane Airplane { get; set; }
        public SeatType SeatType { get; set; }
        public ICollection<BoardingPass> BoardingPasses { get; set; }
    }
}
