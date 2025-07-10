namespace SantoSabor.Core.Entities
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public Guid ClientId { get; set; }
        public string PaymentStatus { get; set; } // pending, paid, cancelled
        public DateTime? PaymentDate { get; set; }
        public DateTime CreatedAt { get; set; }

        public Client Client { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}