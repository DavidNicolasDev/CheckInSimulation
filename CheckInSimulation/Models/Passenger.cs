using System.ComponentModel.DataAnnotations.Schema;

namespace CheckInSimulation.Models
{
    [Table("passenger")]
    public class Passenger
    {
        [Column("passenger_id")]
        public int PassengerId { get; set; }

        [Column("dni")]
        public string Dni { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("age")]
        public int Age { get; set; }

        [Column("country")]
        public string Country { get; set; }

        // Relaciones
        public ICollection<BoardingPass> BoardingPasses { get; set; }
    }
}
