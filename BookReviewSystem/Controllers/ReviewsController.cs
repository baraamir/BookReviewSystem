using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BookReviewSystem.Models;

namespace BookReviewSystem.Controllers
{
    public class ReviewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reviews
        public ActionResult Index()
        {
            var reviews = db.Reviews
                .Include(r => r.Book)
                .Include(r => r.Book.Author);
            return View(reviews.ToList());
        }

        // GET: Reviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Eager load Book and its Author
            Review review = db.Reviews
                .Include(r => r.Book)
                .Include(r => r.Book.Author)
                .FirstOrDefault(r => r.ReviewID == id);

            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: Reviews/Create
        public ActionResult Create()
        {
            // Show book title with author in dropdown
            var books = db.Books.Include(b => b.Author).ToList();
            ViewBag.BookID = new SelectList(books.Select(b => new {
                b.BookID,
                DisplayText = $"{b.Title} by {b.Author.FirstName} {b.Author.LastName}"
            }), "BookID", "DisplayText");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReviewID,BookID,ReviewerName,Rating,Comment")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Repopulate dropdown with formatted text
            var books = db.Books.Include(b => b.Author).ToList();
            ViewBag.BookID = new SelectList(books.Select(b => new {
                b.BookID,
                DisplayText = $"{b.Title} by {b.Author.FirstName} {b.Author.LastName}"
            }), "BookID", "DisplayText", review.BookID);

            return View(review);
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }

            // Show book title with author in dropdown
            var books = db.Books.Include(b => b.Author).ToList();
            ViewBag.BookID = new SelectList(books.Select(b => new {
                b.BookID,
                DisplayText = $"{b.Title} by {b.Author.FirstName} {b.Author.LastName}"
            }), "BookID", "DisplayText", review.BookID);

            return View(review);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReviewID,BookID,ReviewerName,Rating,Comment")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Repopulate dropdown with formatted text
            var books = db.Books.Include(b => b.Author).ToList();
            ViewBag.BookID = new SelectList(books.Select(b => new {
                b.BookID,
                DisplayText = $"{b.Title} by {b.Author.FirstName} {b.Author.LastName}"
            }), "BookID", "DisplayText", review.BookID);

            return View(review);
        }

    }
}