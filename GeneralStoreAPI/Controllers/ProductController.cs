using GeneralStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GeneralStoreAPI.Controllers
{
    public class ProductController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        //POST
        [HttpPost]
        public async Task<IHttpActionResult> Create([FromBody] Product model)
        {
            if(model is null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if(ModelState.IsValid)
            {
                _context.Products.Add(model);
                int changeCount = await _context.SaveChangesAsync();
                return Ok();
            }

            return InternalServerError();

        }

        //GET all
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        //GET by ID
        [HttpGet]
        public async Task<IHttpActionResult> GetByID([FromUri] int id)
        {
            Product products = await _context.Products.FindAsync(id);

            if(products != null)
            {
                return Ok(products);
            }

            return NotFound();
        }
    }
}
