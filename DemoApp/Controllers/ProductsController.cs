using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoApp.DTOs;
using DemoApp.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private productsContext productsContext;

        public ProductsController(productsContext productsContext)
        {
            this.productsContext = productsContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> GetProducts()
        {
            var products = productsContext.Products.Include(p => p.Category).ToList();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = productsContext.Products.Include(p => p.Category).FirstOrDefault(item => item.Id == id);

            if(product is null)
            {
                return NoContent();
            }

            return Ok(product);
        }

        [HttpPost]
        public ActionResult<Product> CreateProduct(ProductDTO createProductDTO)
        {
            Product product = new()
            {
                Id = 0,
                Name = createProductDTO.Name,
                Price = createProductDTO.Price,
                Amount = createProductDTO.Amount,
                CategoryId = createProductDTO.CategoryId,
                Active = 1
            };

            productsContext.Products.Add(product);
            productsContext.SaveChanges();

            return CreatedAtAction(nameof(GetProductById), new { id = productsContext.Products.OrderBy(product => product.Id).Last().Id}, product);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateProduct(ProductDTO updateProductDTO, int id)
        {
            var product = productsContext.Products.ToList().Find(product => product.Id == id);

            if(product is null)
            {
                return NotFound();
            }

            product.Name = updateProductDTO.Name;
            product.Price = updateProductDTO.Price;
            product.Amount = updateProductDTO.Amount;
            product.CategoryId = updateProductDTO.CategoryId;
            productsContext.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var product = productsContext.Products.ToList().Find(product => product.Id == id);

            if(product is null)
            {
                return NotFound();
            }

            product.Active = 0;
            productsContext.SaveChanges();

            return NoContent();
        }


    }
}
