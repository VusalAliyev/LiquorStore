using System.ComponentModel.DataAnnotations;

namespace LiquorStoreFinalProject.ViewModels
{
    public class PaymentVM
    {
        [Required]
        public string Address { get; set; }
        [Required]
        public string PostCode { get; set; }
        [Required]
        public string CardName { get; set; }
        [Required]
        public string CardNumber { get; set; }
        [Required]
        public string Expiration { get; set; }
        [Required]
        public string CVV { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
    }
}
