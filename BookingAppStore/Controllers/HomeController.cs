using BookingAppStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            //using(BookContext db2 = new BookContext())
            //{
            //    var books = db2.Books;
            //}

            return View(db.Books.ToList());

            //var books = db.Books;
            //ViewBag.Message = "Это частичное представление";
            ////ViewBag.Books = books;

            //SelectList authors = new SelectList(db.Books, "Author", "Name");
            //ViewBag.Authors = authors;

            //return View(books);
        }

        [HttpGet]

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Create(Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        //public ActionResult Delete (int id)
        //{
        //    Book b = db.Books.Find(id);
        //    if (b != null)
        //    {
        //        db.Books.Remove(b);
        //        db.SaveChanges();
        //    }

        //    //Book b = new Book { Id = id };
        //    //db.Entry(b).State = EntityState.Deleted;
        //    //db.SaveChanges();

        //    return RedirectToAction("Index");
        //}

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Book b = db.Books.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            return View(b);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Book b = db.Books.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            db.Books.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Book book = db.Books.Find(id);
            if (book != null)
            {
                return View(book);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Edit(Book book)
        {
            db.Entry(book).State = EntityState.Modified; //UPDATE
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetBook(int id)
        {
            Book b = db.Books.Find(id);
            if (b == null)
                return HttpNotFound();
            return View(b);
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
            return View(new Purchase { BookId = id, Person= "Неизвестно" });

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