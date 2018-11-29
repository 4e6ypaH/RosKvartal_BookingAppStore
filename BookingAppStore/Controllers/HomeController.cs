using BookingAppStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookingAppStore.Controllers
{
    public class HomeController : Controller
    {
        BookContext db = new BookContext();

        public ActionResult Index()
        {
            var books = db.Books;
            ViewBag.Message = "Это частичное представление";
            //ViewBag.Books = books;

            SelectList authors = new SelectList(db.Books, "Author", "Name");
            ViewBag.Authors = authors;

            return View(books);
        }

        [HttpPost]
        public string GetForm(string [] countries)
        {
            string result = "";
            foreach (string c in countries)
            {
                result += c;
                result += ";";
            }
            return "Вы выбрали: " + result;
        }

            public ActionResult Getlist()
        {
            string[] states = new string[] { "Russia", "USA", "Canada", "France" };
            return PartialView(states);
        }

        public ActionResult BookIndex()
        {
            var books = db.Books;
            return View(books);
        }

        [HttpGet]
        public ActionResult Buy(int id)
        {           
            ViewBag.BookId = id;
            Purchase purchase = new Purchase { BookId = id, Person= "Неизвестно" };
            return View(purchase);
        }

        [HttpPost]
        public String Buy(Purchase purchase)
        {
            purchase.Date = DateTime.Now;
            //добавляем информацию о покупке в базу данных
            db.Purchases.Add(purchase);
            //сохраняем в бд все изменения
            db.SaveChanges();
            return "Спасибо," + purchase.Person + ", за покупку!";
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}