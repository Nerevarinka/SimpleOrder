namespace SimpleOrder.Models.NewFolder
{
    using System.ComponentModel.DataAnnotations;

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
        [Range(0.001, 100_000_000, ErrorMessage = "Value is not valid for weight")]
        [Display(Name = "Weight, kg")] // #fix 5 Грамм не могу отправить, форма создания, тут заполнить 0.05. и 0.5 тоже нельзя
        public decimal Weight { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Display(Name = "Shipment date")]
        [DataType(DataType.Date)]
        public DateTime ShipmentDate { get; set; }
    }
}
