using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Product.API.Context;
using Product.API.Models;
using System.Xml.Linq;

namespace Product.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(ProductContext context) : ControllerBase
    {
        [HttpGet]
        [DisableCors]
        public IActionResult GetAll()
        {
            return Ok(context.Products.ToList());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetByID(int id)
        {
            try
            {
                ProductModel? order = context.Products.FirstOrDefault(x => x.Id == id);

                if (order is null) return NotFound("No such product");

                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{name:alpha}")]
        public IActionResult GetByName(string name)
        {
            try
            {
                List<ProductModel> orders = context.Products.Where(x => x.Name.Contains(name)).ToList();

                if (orders.Count == 0) return NotFound("No products with this name");

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("New")]
        public IActionResult CreateOrder([FromBody] ProductModel product)
        {
            try
            {
                ProductModel newProduct = new ProductModel() { Name = product.Name, Price = product.Price };
                context.Products.Add(newProduct);
                context.SaveChanges();
                return CreatedAtAction(nameof(GetByID), new { id = newProduct.Id }, newProduct);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("ID/{id}")]
        public IActionResult UpdateOrder(int id, [FromBody] ProductModel product)
        {
            try
            {
                ProductModel? model = context.Products.FirstOrDefault(x => x.Id == id);
                if (model is null) return NotFound("No such product");

                model.Price = product.Price;
                model.Name = product.Name;
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                ProductModel? model = context.Products.FirstOrDefault(x => x.Id == id);
                if (model is null) return NotFound("No such product");
                context.Products.Remove(model);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
