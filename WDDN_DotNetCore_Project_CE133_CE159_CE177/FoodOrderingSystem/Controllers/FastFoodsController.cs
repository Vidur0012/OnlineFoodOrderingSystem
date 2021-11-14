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
    public class FastFoodsController : Controller
    {
        private readonly AppDbContext _context;

        public FastFoodsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: FastFoods
        public async Task<IActionResult> Index()
        {
            return View(await _context.FastFood.ToListAsync());
        }

        // GET: FastFoods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fastFood = await _context.FastFood
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fastFood == null)
            {
                return NotFound();
            }

            return View(fastFood);
        }

        // GET: FastFoods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FastFoods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Item,Price")] FastFood fastFood)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fastFood);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fastFood);
        }

        // GET: FastFoods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fastFood = await _context.FastFood.FindAsync(id);
            if (fastFood == null)
            {
                return NotFound();
            }
            return View(fastFood);
        }

        // POST: FastFoods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Item,Price")] FastFood fastFood)
        {
            if (id != fastFood.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fastFood);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FastFoodExists(fastFood.Id))
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
            return View(fastFood);
        }

        // GET: FastFoods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fastFood = await _context.FastFood
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fastFood == null)
            {
                return NotFound();
            }

            return View(fastFood);
        }

        // POST: FastFoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fastFood = await _context.FastFood.FindAsync(id);
            _context.FastFood.Remove(fastFood);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FastFoodExists(int id)
        {
            return _context.FastFood.Any(e => e.Id == id);
        }

        public async Task<IActionResult> AddCard(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fastFood = await _context.FastFood
               .FirstOrDefaultAsync(m => m.Id == id);
            if (fastFood == null)
            {
                return NotFound();
            }

            HttpContext.Session.SetString("Item", fastFood.Item);
            ViewBag.var1 = HttpContext.Session.GetString("Item");
            HttpContext.Session.SetString("Price", fastFood.Price);
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
