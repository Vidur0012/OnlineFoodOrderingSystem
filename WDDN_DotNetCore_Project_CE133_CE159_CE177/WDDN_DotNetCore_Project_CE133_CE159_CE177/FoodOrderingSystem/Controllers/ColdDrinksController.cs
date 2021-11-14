using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodOrderingSystem.Models;
using Microsoft.AspNetCore.Http;

namespace FoodOrderingSystem.Controllers
{
    public class ColdDrinksController : Controller
    {
        private readonly AppDbContext _context;

        public ColdDrinksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ColdDrinks
        public async Task<IActionResult> Index()
        {
            return View(await _context.ColdDrinks.ToListAsync());
        }

        // GET: ColdDrinks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coldDrinks = await _context.ColdDrinks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coldDrinks == null)
            {
                return NotFound();
            }

            return View(coldDrinks);
        }

        // GET: ColdDrinks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ColdDrinks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Item,Price")] ColdDrinks coldDrinks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coldDrinks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(coldDrinks);
        }

        // GET: ColdDrinks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coldDrinks = await _context.ColdDrinks.FindAsync(id);
            if (coldDrinks == null)
            {
                return NotFound();
            }
            return View(coldDrinks);
        }

        // POST: ColdDrinks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Item,Price")] ColdDrinks coldDrinks)
        {
            if (id != coldDrinks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coldDrinks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ColdDrinksExists(coldDrinks.Id))
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
            return View(coldDrinks);
        }

        // GET: ColdDrinks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coldDrinks = await _context.ColdDrinks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coldDrinks == null)
            {
                return NotFound();
            }

            return View(coldDrinks);
        }

        // POST: ColdDrinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coldDrinks = await _context.ColdDrinks.FindAsync(id);
            _context.ColdDrinks.Remove(coldDrinks);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ColdDrinksExists(int id)
        {
            return _context.ColdDrinks.Any(e => e.Id == id);
        }

        public async Task<IActionResult> AddCard(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colddrinks = await _context.ColdDrinks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (colddrinks == null)
            {
                return NotFound();
            }

            HttpContext.Session.SetString("Item", colddrinks.Item);
            ViewBag.var1 = HttpContext.Session.GetString("Item");
            HttpContext.Session.SetString("Price", colddrinks.Price);
            ViewBag.var2 = HttpContext.Session.GetString("Price");
            return View();
        }

        [HttpPost, ActionName("AddCard")]
        [ValidateAntiForgeryToken]
        public IActionResult AddCard()
        {
            OrderList ol = new OrderList();
            ol.Item = HttpContext.Request.Form["Item"].ToString();
            ol.Quantity = Convert.ToInt32(HttpContext.Request.Form["Quantity"]);
            ol.TotalPrice = (Convert.ToInt32(HttpContext.Request.Form["TotalPrice"]) * ol.Quantity).ToString();

            _context.OrderList.Add(ol);
            _context.SaveChanges();

            //ol.Item = order.Item;
            //ol.Quantity = order.Quantity;
            //ol.TotalPrice = order.TotalPrice;

            //int result = ol.SaveDetails();

            //if (result > 0)
            //{
            //    ViewBag.Result = "Data Saved Successfully";
            //}
            //else
            //{
            //    ViewBag.Result = "Something Went Wrong";
            //}
            return RedirectToRoute(new { controller = "OrderLists", action = "Index" });
        }
    }
}
