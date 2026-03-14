namespace StockManager.Application.DTOs.Product.Response
{
    public class ProductItemResponse
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = null!;
        public int Precio { get; set; }
        public string Categoria { get; set; } = null!;
        public string FechaCarga { get; set; } = null!;
    }
}
