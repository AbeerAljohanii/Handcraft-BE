using Backend_Teamwork.src.Entities;
using static Backend_Teamwork.src.DTO.OrderDetailDTO;

namespace Backend_Teamwork.src.DTO
{
    public class OrderDTO
    {
        // DTO for creating a new order
        public class OrderCreateDto
        {
            public decimal TotalAmount { get; set; }
            public string ShippingAddress { get; set; }
            public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
            public Guid UserId { get; set; }
            public List<OrderDetailCreateDto> OrderDetails { get; set; }
        }

        // DTO for reading order data
        public class OrderReadDto
        {
            public Guid Id { get; set; }
            public decimal TotalAmount { get; set; }
            public string? ShippingAddress { get; set; }
            public DateTime? CreatedAt { get; set; }
            public Guid UserId { get; set; }
            public List<OrderDetailReadDto> OrderDetails { get; set; }
        }

        // DTO for updating an existing order
        public class OrderUpdateDto
        {
            public decimal TotalAmount { get; set; }
            public string? ShippingAddress { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    }
}
