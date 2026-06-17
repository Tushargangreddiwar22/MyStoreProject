namespace MyStore.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }

        // Order FK
        public int OrderId { get; set; }

        public Order Order { get; set; }

        // Product FK
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }
    }
}