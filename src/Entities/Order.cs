namespace Backend_Teamwork.src.Controllers
{
    public class Order
    {
        public int OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public string? ShippingAddress { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
