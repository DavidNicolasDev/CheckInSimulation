using System.ComponentModel.DataAnnotations.Schema;

namespace CheckInSimulation.Models
{
    [Table("seat_type")]
    public class SeatType
    {
        [Column("seat_type_id")]
        public int SeatTypeId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        // Relaciones
        public ICollection<Seat> Seats { get; set; }
        public ICollection<BoardingPass> BoardingPasses { get; set; }
    }
}
