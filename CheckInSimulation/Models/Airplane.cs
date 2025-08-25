using System.ComponentModel.DataAnnotations.Schema;

namespace CheckInSimulation.Models
{
    [Table("airplane")]
    public class Airplane
    {
        [Column("airplane_id")]
        public int AirplaneId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        // Relaciones
        public ICollection<Flight> Flights { get; set; }
        public ICollection<Seat> Seats { get; set; }
    }
}
