namespace StockManager.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; } = null!;
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UpdateBy { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
