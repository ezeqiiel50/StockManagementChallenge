namespace StockManager.Application.DTOs.Product.Model
{
    public class ProductCreateModel
    {
        public string Descripcion { get; set; } = null!;
        public int Precio { get; set; }
        public string Categoria { get; set; } = null!;
        public int UserId { get; set; }
    }
}
