namespace CheckInSimulation.Models
{
    public class SeatLayout
    {
        public int AirplaneId { get; set; }
        public Dictionary<int, SeatConfig> Classes { get; set; } // key = seatTypeId
    }

    public class SeatConfig
    {
        public List<int> Rows { get; set; } // lista de filas
        public List<List<string>> ColumnGroups { get; set; } // columnas agrupadas por lado
    }
}
