using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookApp;
using BookApp.Models;
using System.Data.SqlClient;

namespace BookApp.Controllers
{
    public class UserAccountsController : Controller
    {
        private readonly DatabaseContext _context;

        public UserAccountsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: UserAccounts
        public async Task<IActionResult> Index()
        {
            var role = "User";
            return View(await _context.UserAccounts.FromSqlRaw("select *  from UserAccounts where  Role = '" + role + "'  ").ToListAsync());
             
        }

        // GET: UserAccounts/Details/5
      

        // GET: UserAccounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,First_Name,Last_Name,EmailAddress,password,ConfirmPassword,Role")] UserAccounts userAccounts)
        {
               
                _context.Add(userAccounts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Login));
   
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, ActionName("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string na, string pa,string role)
        {   
            SqlConnection conn1 = new SqlConnection("Data Source=DESKTOP-J6USA91\\SQLEXPRESS;Initial Catalog=BookShopApp;Integrated Security=true;Connect Timeout=30");
            string sql;
            sql = "SELECT * FROM UserAccounts where EmailAddress ='" + na + "' and  Password ='" + pa + "' and Role='" + role + "' ";
            SqlCommand comm = new SqlCommand(sql, conn1);
            conn1.Open();
            SqlDataReader reader = comm.ExecuteReader();

            if (reader.Read())
            {
                //string role = (string)reader["role"];
                string id = (string)reader["UserId"];
                string name = (string)reader["First_Name"];
                HttpContext.Session.SetString("EmailAddress", na);
                HttpContext.Session.SetString("Role", role);
                HttpContext.Session.SetString("First_Name", name);
                HttpContext.Session.SetString("UserId", id);
               
                if (role == "User")
                    return RedirectToAction("Catalogue", "Books");

                else
                    return RedirectToAction("Index", "Books");
                reader.Close();
                

            }
            else
            {
                ViewData["Message"] = "wrong user name password";
                return View();
            }
            conn1.Close();
        }


        // GET: UserAccounts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.UserAccounts == null)
            {
                return NotFound();
            }

            var userAccounts = await _context.UserAccounts.FindAsync(id);
            if (userAccounts == null)
            {
                return NotFound();
            }
            return View(userAccounts);
        }

        // POST: UserAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( [Bind("UserId,First_Name,Last_Name,EmailAddress,password,ConfirmPassword,Role")] UserAccounts userAccounts)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetString("userid"));
            _context.Update(userAccounts);
             await _context.SaveChangesAsync();
          
          return RedirectToAction(nameof(ProfileView));
      
        }

        // GET: UserAccounts/Delete/5
        public async Task<IActionResult> ProfileView()
        {

            int id = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var orItems = await _context.UserAccounts.FromSqlRaw("select *  from UserAccounts where  userid = '" + id + "'  ").ToListAsync();
            return View(orItems);

        }
        public async Task<IActionResult> Details(string name)
        {
            if ( name== null || _context.UserAccounts == null)
            {
                return NotFound();
            }

            var user = await _context.UserAccounts
                .FirstOrDefaultAsync(m => m.First_Name == name);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        private bool UserAccountsExists(string id)
        {
          return (_context.UserAccounts?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
