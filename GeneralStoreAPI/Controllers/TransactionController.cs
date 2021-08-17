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
    public class TransactionController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        //POST
        //verify the product is in stock, check that there is enough in stock to complete transaction, remove bought products
        [HttpPost]
        public async Task<IHttpActionResult> Create([FromBody]Transaction model)
        {
            if(model is null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //verify product exits
            var productEntity = await _context.Products.FindAsync(model.ID);
            if(productEntity is null)
            {
                return BadRequest();
            }

            //verify product is in stock
            if(productEntity.IsInStock)
            {
                return Ok();
            }

            //check that there is enough to complete transaction
            if(productEntity.NumberInInventory >= model.ItemCount)
            {
                return Ok();
                //remove bought products
                _context.Transactions.Remove(model);
            }

            return BadRequest();

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
        public async Task<IHttpActionResult> GetById([FromUri] int id)
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
