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
    public class ChineseController : Controller
    {
        private readonly AppDbContext _context;

        public ChineseController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Chinese
        public async Task<IActionResult> Index()
        {
            return View(await _context.Chinese.ToListAsync());
        }

        // GET: Chinese/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chinese = await _context.Chinese
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chinese == null)
            {
                return NotFound();
            }

            return View(chinese);
        }

        // GET: Chinese/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Chinese/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Item,Price")] Chinese chinese)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chinese);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chinese);
        }

        // GET: Chinese/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chinese = await _context.Chinese.FindAsync(id);
            if (chinese == null)
            {
                return NotFound();
            }
            return View(chinese);
        }

        // POST: Chinese/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Item,Price")] Chinese chinese)
        {
            if (id != chinese.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chinese);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChineseExists(chinese.Id))
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
            return View(chinese);
        }

        // GET: Chinese/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chinese = await _context.Chinese
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chinese == null)
            {
                return NotFound();
            }

            return View(chinese);
        }

        // POST: Chinese/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chinese = await _context.Chinese.FindAsync(id);
            _context.Chinese.Remove(chinese);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChineseExists(int id)
        {
            return _context.Chinese.Any(e => e.Id == id);
        }

        public async Task<IActionResult> AddCard(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chinese = await _context.Chinese
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chinese == null)
            {
                return NotFound();
            }

            HttpContext.Session.SetString("Item", chinese.Item);
            ViewBag.var1 = HttpContext.Session.GetString("Item");
            HttpContext.Session.SetString("Price", chinese.Price);
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
