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
    public class SouthIndiansController : Controller
    {
        private readonly AppDbContext _context;

        public SouthIndiansController(AppDbContext context)
        {
            _context = context;
        }

        // GET: SouthIndians
        public async Task<IActionResult> Index()
        {
            return View(await _context.SouthIndian.ToListAsync());
        }

        // GET: SouthIndians/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var southIndian = await _context.SouthIndian
                .FirstOrDefaultAsync(m => m.Id == id);
            if (southIndian == null)
            {
                return NotFound();
            }

            return View(southIndian);
        }

        // GET: SouthIndians/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SouthIndians/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Item,Price")] SouthIndian southIndian)
        {
            if (ModelState.IsValid)
            {
                _context.Add(southIndian);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(southIndian);
        }

        // GET: SouthIndians/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var southIndian = await _context.SouthIndian.FindAsync(id);
            if (southIndian == null)
            {
                return NotFound();
            }
            return View(southIndian);
        }

        // POST: SouthIndians/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Item,Price")] SouthIndian southIndian)
        {
            if (id != southIndian.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(southIndian);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SouthIndianExists(southIndian.Id))
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
            return View(southIndian);
        }

        // GET: SouthIndians/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var southIndian = await _context.SouthIndian
                .FirstOrDefaultAsync(m => m.Id == id);
            if (southIndian == null)
            {
                return NotFound();
            }

            return View(southIndian);
        }

        // POST: SouthIndians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var southIndian = await _context.SouthIndian.FindAsync(id);
            _context.SouthIndian.Remove(southIndian);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SouthIndianExists(int id)
        {
            return _context.SouthIndian.Any(e => e.Id == id);
        }

        public async Task<IActionResult> AddCard(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var southIndian = await _context.SouthIndian
                .FirstOrDefaultAsync(m => m.Id == id);
            if (southIndian == null)
            {
                return NotFound();
            }

            HttpContext.Session.SetString("Item", southIndian.Item);
            ViewBag.var1 = HttpContext.Session.GetString("Item");
            HttpContext.Session.SetString("Price", southIndian.Price);
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
