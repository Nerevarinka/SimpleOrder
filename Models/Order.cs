namespace SimpleOrder.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class Order
    {
        public Guid Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Number { get; set; }

        public string SenderCity { get; set; }

        public string SenderAddress { get; set; }

        public string RecipientCity { get; set; }

        public string RecipientAddress { get; set; }

        public decimal Weight { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ShipmentDate { get; set; }
    }
}
