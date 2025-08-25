namespace CheckInSimulation.Models.Dtos
{
    public class PassengerDto
    {
        public int PassengerId { get; set; }
        public string Dni { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public int BoardingPassId { get; set; }
        public int PurchaseId { get; set; }
        public int SeatTypeId { get; set; }
        public int? SeatId { get; set; }
    }
}
