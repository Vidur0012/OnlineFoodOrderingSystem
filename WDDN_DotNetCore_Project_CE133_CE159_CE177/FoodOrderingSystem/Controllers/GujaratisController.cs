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
    public class GujaratisController : Controller
    {
        private readonly AppDbContext _context;

        public GujaratisController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Gujaratis
        public async Task<IActionResult> Index()
        {
            return View(await _context.Gujarati.ToListAsync());
        }

        // GET: Gujaratis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gujarati = await _context.Gujarati
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gujarati == null)
            {
                return NotFound();
            }

            return View(gujarati);
        }

        // GET: Gujaratis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gujaratis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Item,Price")] Gujarati gujarati)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gujarati);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gujarati);
        }

        // GET: Gujaratis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gujarati = await _context.Gujarati.FindAsync(id);
            if (gujarati == null)
            {
                return NotFound();
            }
            return View(gujarati);
        }

        // POST: Gujaratis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Item,Price")] Gujarati gujarati)
        {
            if (id != gujarati.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gujarati);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GujaratiExists(gujarati.Id))
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
            return View(gujarati);
        }

        // GET: Gujaratis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gujarati = await _context.Gujarati
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gujarati == null)
            {
                return NotFound();
            }

            return View(gujarati);
        }

        // POST: Gujaratis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gujarati = await _context.Gujarati.FindAsync(id);
            _context.Gujarati.Remove(gujarati);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GujaratiExists(int id)
        {
            return _context.Gujarati.Any(e => e.Id == id);
        }

        public async Task<IActionResult> AddCard(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gujarati = await _context.Gujarati
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gujarati == null)
            {
                return NotFound();
            }
            
            HttpContext.Session.SetString("Item", gujarati.Item);
            ViewBag.var1 = HttpContext.Session.GetString("Item");
            HttpContext.Session.SetString("Price", gujarati.Price);
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
            ol.TotalPrice = (Convert.ToInt32(HttpContext.Request.Form["TotalPrice"])*ol.Quantity).ToString();

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
