namespace StockManager.Data.StoredProcedures
{
    public class ProductSpResult
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public int Price { get; set; }
        public string Category { get; set; } = null!;
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? UpdatedBy { get; set; } = default;
        public DateTime? UpdatedAt { get; set; } = default;
    }
}
