namespace StockManager.Application.DTOs.Product.Request
{
    public class ProductRequest
    {
        public string Descripcion { get; set; } = null!;
        public int Precio { get; set; }
        public string Categoria { get; set; } = null!;
    }
}
