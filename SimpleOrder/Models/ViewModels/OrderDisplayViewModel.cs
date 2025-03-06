namespace SimpleOrder.Models.NewFolder
{
    using System.ComponentModel.DataAnnotations;

    public class OrderDisplayViewModel : OrderViewModel
    {
        public Guid Id { get; set; }
       
        public int Number { get; set; }

        [Display(Name = "Number")]
        public string FormattedOrderNumber => $"ORD-{CreatedAt:yyMMdd}-{Number}";

        public DateTime CreatedAt { get; set; }
    }
}
