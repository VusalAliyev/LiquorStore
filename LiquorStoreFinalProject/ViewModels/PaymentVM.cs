namespace LiquorStoreFinalProject.ViewModels
{
    public class PaymentVM
    {
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string CVV { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
