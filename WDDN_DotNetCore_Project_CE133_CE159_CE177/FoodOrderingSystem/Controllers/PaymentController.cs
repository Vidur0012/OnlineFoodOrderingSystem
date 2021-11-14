using FoodOrderingSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrderingSystem.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Payment(int? id)
        {
            HttpContext.Session.SetInt32("tp", (int)id);
            //TempData["tp"] = id;
            return View();
        }
        [HttpPost]
        public IActionResult Payment_Online(Payment payment)
        {
            ViewBag.TPOnline =  HttpContext.Session.GetInt32("tp");
            if (ModelState.IsValid)
            {
                return View();
                
            }
            return View("~/Views/Payment/Payment.cshtml");

        }

        public IActionResult Payment_Offline()
        {
            ViewBag.TPOffline = HttpContext.Session.GetInt32("tp");
            return View();
        }
        [HttpPost]
        public IActionResult Success_Online(Payment_Online paymentonline)
        {
            ViewBag.TPOnline = HttpContext.Session.GetInt32("tp");
            if (ModelState.IsValid)
            {
                return View();

            }
            return View("~/Views/Payment/Payment_Online.cshtml");
           
        }

        public IActionResult Success_Offline()
        {
            return View();
        }
    }
}
