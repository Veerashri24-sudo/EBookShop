using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using Microsoft.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookApp;
using BookApp.Models;
using Newtonsoft.Json;

namespace BookApp.Controllers
{
    public class CartItemsController : Controller
    {
        private readonly DatabaseContext _context;

        List<Transaction> list = new List<Transaction>();
     
        public CartItemsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: CartItems
        public async Task<IActionResult> Index()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            return _context.CartItems != null ? 
                          View(await _context.CartItems.FromSqlRaw("select *  from CartItems where  userid = '" + id + "'  ").ToListAsync()) :
                          Problem("Entity set 'DatabaseContext.CartItems'  is null.");
        }




        // GET: CartItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CartItems == null)
            {
                return NotFound();
            }

            var cartItems = await _context.CartItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cartItems == null)
            {
                return NotFound();
            }

            return View(cartItems);
        }

        // GET: CartItems/Create
        public async Task<IActionResult> Create(int? id)
        {
            var book = await _context.Book.FindAsync(id);
            return View(book);
        }

        // POST: CartItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int bookId, string bookName, string Author, string ImageUrl, int quantity,int price)
        {
            CartItems cart = new CartItems();
            cart.Book_Id = bookId;
            cart.Book_Name = bookName;
            cart.Author = Author;
            cart.ImageUrl = ImageUrl;
            cart.Quantity = quantity;
            cart.TotalAmt = quantity * price;
            cart.UserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
     
            HttpContext.Session.SetString("Quantity", quantity.ToString());
            HttpContext.Session.SetString("Book_Id", (bookId).ToString());
            HttpContext.Session.SetString("Book_Name",bookName);
            HttpContext.Session.SetString("TotalAmt", (cart.TotalAmt).ToString());
            _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
   
        }
     
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CartItems == null)
            {
                return NotFound();
            }

            var cartItems = await _context.CartItems.FindAsync(id);
            if (cartItems == null)
            {
                return NotFound();
            }
            return View(cartItems);
        }

        // POST: CartItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Book_Id,Book_Name,Author,Quantity,TotalAmt,ImageUrl,UserId")] CartItems cartItems)
        {
            if (id != cartItems.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartItems);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartItemsExists(cartItems.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cartItems);
        }

      
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CartItems == null)
            {
                return NotFound();
            }

            var cartItems = await _context.CartItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cartItems == null)
            {
                return NotFound();
            }

            return View(cartItems);
        }

        // POST: CartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CartItems == null)
            {
                return Problem("Entity set 'DatabaseContext.CartItems'  is null.");
            }
            var cartItems = await _context.CartItems.FindAsync(id);
            if (cartItems != null)
            {
                _context.CartItems.Remove(cartItems);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartItemsExists(int id)
        {
          return (_context.CartItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
