using ProductAPI.Models;

namespace ProductAPI.Repository
{
    public interface IProductRepository
    {
        Product AddProduct(Product product);
        void DeleteProduct(int id);
        void DeleteProduct(string productName);
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        Product GetProductByName(string productName);
        Product UpdateProduct(Product product);
    }
}