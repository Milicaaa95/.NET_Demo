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
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private productsContext productsContext;

        public CategoriesController(productsContext productsContext)
        {
            this.productsContext = productsContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> GetCategories()
        {
            var categories = productsContext.Categories.Include(c => c.Products).ToList();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public ActionResult<Category> GetCategorieById(int id)
        {
            var category = productsContext.Categories.Include(c => c.Products).FirstOrDefault(item => item.Id == id);

            if(category is null)
            {
                return NoContent();
            }

            return Ok(category);
        }

        [HttpPost]
        public ActionResult<Category> CreateCategory(CategoryDTO createCategoryDTO)
        {
            Category category = new()
            {
                Id = 0,
                Name = createCategoryDTO.Name
            };

            productsContext.Categories.Add(category);
            var createdId = productsContext.Categories.OrderBy(category => category.Id).Last().Id;
            productsContext.SaveChanges();

            return CreatedAtAction(nameof(GetCategorieById), new { id = createdId }, category);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCategory(CategoryDTO updateCategoryDTO, int id)
        {
            var category = productsContext.Categories.ToList().Find(category => category.Id == id);

            if(category is null)
            {
                return NotFound();
            }

            category.Name = updateCategoryDTO.Name;
            productsContext.SaveChanges();

            return NoContent();
        }        
    }
}
