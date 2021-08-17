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
    public class CustomerController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        //POST
        [HttpPost]
        public async Task<IHttpActionResult> CreateCustomer([FromBody] Customer model)
        {
            if (model is null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                _context.Customers.Add(model);
                int changeCount = await _context.SaveChangesAsync();
                return Ok();
            }
                return BadRequest();

            //var customerEntity = await _context.Customers.FindAsync(model.ID);
            //if (customerEntity is null)
            //    return BadRequest();

            //if (await _context.SaveChangesAsync() == 1)
            //    return Ok();

            //return InternalServerError();
        }


        //GET all
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Customer> customers = await _context.Customers.ToListAsync();
            return Ok(customers);
        }

        //GET by ID
        [HttpGet]
        public async Task<IHttpActionResult> GetById([FromUri] int id)
        {
            Customer customers = await _context.Customers.FindAsync(id);
            if(customers != null)
            {
                return Ok(customers);
            }
            return NotFound();
        }

        //PUT
        [HttpPut]
        public async Task<IHttpActionResult> UpdateCustomer([FromUri] int id, [FromBody] Customer updatedCustomer)
        {
            if(id != updatedCustomer?.ID)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Customer customer = await _context.Customers.FindAsync(id);

            if (customer != null)
                return NotFound();

            customer.FirstName = updatedCustomer.FirstName;
            customer.LastName = updatedCustomer.LastName;

            await _context.SaveChangesAsync();

            return Ok();
        }


        //DELETE
        [HttpDelete]
        public async Task<IHttpActionResult> Delete([FromUri] int id)
        {
            Customer customer = await _context.Customers.FindAsync(id);

            if(customer is null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);

            if(await _context.SaveChangesAsync() == 1)
            {
                return Ok();
            }
            return InternalServerError();
        }

    }
}
