using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoneySpentControl.Models;
using System;
using System.Diagnostics;

namespace MoneySpentControl.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public static int balance = 0;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("/gg/calc")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("/gg/send")]
        public IActionResult Sended()
        {
            bool isProfit = Request.Form["exampleRadios"] == "option1" ? false : true;
            string desc = Request.Form["desc"];
            string cat = Request.Form["cat"];
            string summa = Request.Form["summ"];
            Operation oper = new Operation(cat, Convert.ToInt32(summa), isProfit, desc);
            ProcessSQLite.InsertOperation(oper);
            return RedirectToAction("Index");
        }

        [Route("/get")]
        public IActionResult History()
        {
            balance = 0;
            var x = ProcessSQLite.GetAllHistory();
            foreach (var el in Models.ProcessSQLite.GetAllHistory())
            {
                if (el.IsProfit)
                {
                    balance += el.Amount;
                }
                else
                {
                    balance -= el.Amount;
                }
            }
            ViewBag.Balance = balance;
            return View(x);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
