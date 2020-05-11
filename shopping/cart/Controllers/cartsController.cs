using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cart.Data;
using cart.Models;

namespace Cart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class cartsController : ControllerBase
    {
        private readonly CartContext _context;

        public cartsController(CartContext context)
        {
            _context = context;
        }

        // GET: api/carts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<carts>>> Getcart()
        {
            return await _context.cart.ToListAsync();
        }

        // GET: api/carts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<carts>> Getcart(int id)
        {
            var cart = await _context.cart.FindAsync(id);

            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }

        // PUT: api/carts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> Putcart(int id, carts cart)
        {
            if (id != cart.Id)
            {
                return BadRequest();
            }

            _context.Entry(cart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!cartExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/carts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<carts>> Postcart(carts cart)
        {
            _context.cart.Add(cart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getcart", new { id = cart.Id }, cart);
        }

        // DELETE: api/carts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<carts>> Deletecart(int id)
        {
            var cart = await _context.cart.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            _context.cart.Remove(cart);
            await _context.SaveChangesAsync();

            return cart;
        }

        private bool cartExists(int id)
        {
            return _context.cart.Any(e => e.Id == id);
        }
    }
}
