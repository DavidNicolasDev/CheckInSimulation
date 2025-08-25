using System.ComponentModel.DataAnnotations.Schema;

namespace CheckInSimulation.Models
{
    [Table("purchase")]
    public class Purchase
    {
        [Column("purchase_id")]
        public int PurchaseId { get; set; }

        [Column("purchase_date")]
        public int PurchaseDate { get; set; }

        // Relaciones
        public ICollection<BoardingPass> BoardingPasses { get; set; }
    }
}
