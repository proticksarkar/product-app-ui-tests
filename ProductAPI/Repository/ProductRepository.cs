using ProductAPI.Data;
using ProductAPI.Models;

namespace ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;

        public ProductRepository(ProductDbContext context)
        {
            _context = context;
        }

        public Product GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }

        public Product GetProductByName(string productName)
        {
            return _context.Products.FirstOrDefault(p => p.Name == productName);
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Product AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public Product UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
            return product;
        }

        public void DeleteProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public void DeleteProduct(string productName)
        {
            var product = _context.Products.FirstOrDefault(p => p.Name == productName);
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}