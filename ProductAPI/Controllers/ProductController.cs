using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;
using ProductAPI.Repository;

namespace ProductAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        // GET: ProductController/GetProductById
        [HttpGet]
        [Route("/[controller]/[action]/{id}")]
        public Product GetProductById(int id)
        {
            return productRepository.GetProductById(id);
        }

        // GET: ProductController/GetProducts
        [HttpGet]
        [Route("/[controller]/[action]")]
        public ActionResult<List<Product>> GetProducts()
        {
            return productRepository.GetAllProducts();
        }

        // POST: ProductController/Create
        [HttpPost]
        [Route("/[controller]/[action]")]
        public Product Create(Product product)
        {
            return productRepository.AddProduct(product);
        }

        // PUT: ProductController/Update
        [HttpPut]
        [Route("/[controller]/[action]")]
        public Product Update(Product product)
        {
            return productRepository.UpdateProduct(product);
        }

        // DELETE: ProductController/Delete
        [HttpDelete]
        [Route("/[controller]/[action]")]
        public void Delete(int id)
        {
            productRepository.DeleteProduct(id);
        }
    }
}