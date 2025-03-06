namespace SimpleOrder.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Order
    {
        public Guid Id { get; set; }

        public int Number { get; set; }

        public string SenderCity { get; set; }

        public string SenderAddress { get; set; }

        public string RecipientCity { get; set; }

        public string RecipientAddress { get; set; }

        public double Weight { get; set; }

        public DateTime ShipmentDate { get; set; }
    }

    public class OrderCreateViewModel : OrderViewModel
    { }

    public class OrderDisplayViewModel : OrderViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Number")]
        public int Number { get; set; }
    }

    public class OrderEditViewModel : OrderViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Number")]
        public int Number { get; set; }      
    }

    public abstract class OrderViewModel
    {
        [Required(ErrorMessage = "Required field")]
        [Display(Name = "Sender city")]
        public string SenderCity { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Display(Name = "Sender address")]
        public string SenderAddress { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Display(Name = "Recipient city")]
        public string RecipientCity { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Display(Name = "Recipient address")]
        public string RecipientAddress { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Range(0.001, 100_000, ErrorMessage = "Value is not valid for weight")]
        [Display(Name = "Weight, kg")]
        public double Weight { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Display(Name = "Shipment date")]
        [DataType(DataType.Date)]
        public DateTime ShipmentDate { get; set; }
    }
}
