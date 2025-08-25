using System.ComponentModel.DataAnnotations.Schema;

namespace CheckInSimulation.Models
{
    [Table("flight")]
    public class Flight
    {
        [Column("flight_id")]
        public int FlightId { get; set; }

        [Column("takeoff_date_time")]
        public int TakeoffDateTime { get; set; }

        [Column("takeoff_airport")]
        public string TakeoffAirport { get; set; }

        [Column("landing_date_time")]
        public int LandingDateTime { get; set; }

        [Column("landing_airport")]
        public string LandingAirport { get; set; }

        [Column("airplane_id")]
        public int AirplaneId { get; set; }

        // Relaciones
        public Airplane Airplane { get; set; }
        public ICollection<BoardingPass> BoardingPasses { get; set; }
    }
}
