namespace SimpleOrder.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        public int Number { get; set; }

        public string SenderCity { get; set; }

        public string SenderAddress { get; set; }

        public string RecipientCity { get; set; }

        public string RecipientAddress { get; set; }

        /// <summary>
        /// Килограммы
        /// </summary>
        public decimal Weight { get; set; }

        public DateTime ShipmentDate { get; set; }
    }
}
