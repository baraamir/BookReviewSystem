using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BookReviewSystem.Models;

namespace BookReviewSystem.Controllers
{
    public class BooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        
        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.Author);
            return View(books.ToList());
        }

       
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Book book = db.Books
                .Include(b => b.Author)
                .Include(b => b.Reviews)
                .FirstOrDefault(b => b.BookID == id);

            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

      
        public ActionResult Create()
        {
            // Show full name in dropdown
            ViewBag.AuthorID = new SelectList(db.Authors.Select(a => new {
                a.AuthorID,
                FullName = a.FirstName + " " + a.LastName
            }), "AuthorID", "FullName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookID,Title,PublishedYear,Genre,AuthorID")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

          
            ViewBag.AuthorID = new SelectList(db.Authors.Select(a => new {
                a.AuthorID,
                FullName = a.FirstName + " " + a.LastName
            }), "AuthorID", "FullName", book.AuthorID);

            return View(book);
        }

        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }

          
            ViewBag.AuthorID = new SelectList(db.Authors.Select(a => new {
                a.AuthorID,
                FullName = a.FirstName + " " + a.LastName
            }), "AuthorID", "FullName", book.AuthorID);

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookID,Title,PublishedYear,Genre,AuthorID")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

           
            ViewBag.AuthorID = new SelectList(db.Authors.Select(a => new {
                a.AuthorID,
                FullName = a.FirstName + " " + a.LastName
            }), "AuthorID", "FullName", book.AuthorID);

            return View(book);
        }

    }
}