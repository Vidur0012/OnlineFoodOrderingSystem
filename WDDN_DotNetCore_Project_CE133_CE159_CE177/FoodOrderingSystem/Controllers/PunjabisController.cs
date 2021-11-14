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
    public class PunjabisController : Controller
    {
        private readonly AppDbContext _context;

        public PunjabisController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Punjabis
        public async Task<IActionResult> Index()
        {
            return View(await _context.Punjabi.ToListAsync());
        }

        // GET: Punjabis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var punjabi = await _context.Punjabi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (punjabi == null)
            {
                return NotFound();
            }

            return View(punjabi);
        }

        // GET: Punjabis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Punjabis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Item,Price")] Punjabi punjabi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(punjabi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(punjabi);
        }

        // GET: Punjabis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var punjabi = await _context.Punjabi.FindAsync(id);
            if (punjabi == null)
            {
                return NotFound();
            }
            return View(punjabi);
        }

        // POST: Punjabis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Item,Price")] Punjabi punjabi)
        {
            if (id != punjabi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(punjabi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PunjabiExists(punjabi.Id))
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
            return View(punjabi);
        }

        // GET: Punjabis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var punjabi = await _context.Punjabi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (punjabi == null)
            {
                return NotFound();
            }

            return View(punjabi);
        }

        // POST: Punjabis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var punjabi = await _context.Punjabi.FindAsync(id);
            _context.Punjabi.Remove(punjabi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PunjabiExists(int id)
        {
            return _context.Punjabi.Any(e => e.Id == id);
        }

        public async Task<IActionResult> AddCard(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var punjabi = await _context.Punjabi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (punjabi == null)
            {
                return NotFound();
            }

            HttpContext.Session.SetString("Item", punjabi.Item);
            ViewBag.var1 = HttpContext.Session.GetString("Item");
            HttpContext.Session.SetString("Price", punjabi.Price);
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
