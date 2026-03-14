namespace StockManager.Application.DTOs.Product
{
    public class ProductoResponse
    {
        public int Id { get; set; }
        public decimal Precio { get; set; }
        public string Categoria { get; set; } = null!;
        public string FechaCarga { get; set; } = null!;
    }
}
