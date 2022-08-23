using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookApp;
using BookApp.Models;

namespace BookApp.Controllers
{
    public class BillingsController : Controller
    {
        private readonly DatabaseContext _context;

        public BillingsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Billings
        public async Task<IActionResult> Index()
        {
              return _context.Billing != null ? 
                          View(await _context.Billing.ToListAsync()) :
                          Problem("Entity set 'DatabaseContext.Billing'  is null.");
        }

        // GET: Billings/Details/5
   

        // GET: Billings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Billings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransactionId,Total_Payment,Payment_Type,Bank_Name,Branch,CreditCard_Number,CardExpiryDate,cvv")] Billing billing)
        {
           
            if (billing.Payment_Type == "COD")
            {
                billing.Branch = "null";
                billing.CreditCard_Number = "null";
                billing.cvv = "null";
                billing.CardExpiryDate = (DateTime.Today);
                billing.Bank_Name = "null";


            }
            var TransactionId = billing.TransactionId;
            billing.Total_Payment = Convert.ToInt32(HttpContext.Session.GetString("TotalAmt"));
            billing.User_Id = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            billing.DateTime = (DateTime.Today).ToShortDateString();
            billing.Book_Id = Convert.ToInt32(HttpContext.Session.GetString("Book_Id"));
            billing.Book_Name = HttpContext.Session.GetString("Book_Name").ToString();
                
            billing.Quantity = Convert.ToInt32(HttpContext.Session.GetString("Quantity"));
            HttpContext.Session.SetString("TransactionId", (TransactionId).ToString());
            Book book = _context.Book.FirstOrDefault(b => b.Book_Name == billing.Book_Name);
            book.Quantity = book.Quantity -billing.Quantity;
            _context.Add(billing);
                await _context.SaveChangesAsync();
                return View();
          
        }
        public async Task<IActionResult> Order()
        {
            

            var Items = await _context.Billing.FromSqlRaw("select *  from billing ").ToListAsync();
            return View(Items);
        }
        public async Task<IActionResult> CustomerOrders()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var orItems = await _context.Billing.FromSqlRaw("select * from billing where  user_id = '" + id + "'  ").ToListAsync();
            return View(orItems);

        }
       
        // GET: Billings/Edit/5
       

        private bool BillingExists(int id)
        {
          return (_context.Billing?.Any(e => e.TransactionId == id)).GetValueOrDefault();
        }
    }
}
