using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AuthorBookAuthorizedMVC.Data;
using AuthorBookAuthorizedMVC.Models;

namespace AuthorBookAuthorizedMVC.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult GetBooks()
        {
            if (Session["AuthorId"] == null)
            {
                return RedirectToAction("Login", "Author");
            }
            Guid authorId = (Guid)Session["AuthorId"];
            using (var session = NHibernateHelper.CreateSession())
            {
                var books = session.Query<Book>().Where(b => b.Author.Id == authorId).ToList();
                return View(books);
            }
        }
        public ActionResult Edit(Guid id)
        {
            if (Session["AuthorId"] == null)
            {
                return RedirectToAction("Login", "Author");
            }
            Guid authorId = (Guid)Session["AuthorId"];
            using (var session = NHibernateHelper.CreateSession())
            {
                var book = session.Query<Book>().FirstOrDefault(b => b.Author.Id == authorId && b.Id == id);
                return View(book);
            }
        }
        [HttpPost]
        public ActionResult Edit(Book book)
        {
            if (Session["AuthorId"] == null)
            {
                return RedirectToAction("Login", "Author");
            }
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var existingBook = session.Get<Book>(book.Id);
                    existingBook.Name = book.Name;
                    existingBook.Genre = book.Genre;
                    existingBook.Description = book.Description;
                    session.Update(existingBook);
                    transaction.Commit();
                    return RedirectToAction("GetBooks");
                }
            }
        }
        public ActionResult Delete(Guid id)
        {
            if (Session["AuthorId"] == null)
            {
                return RedirectToAction("Login", "Author");
            }
            Guid authorId = (Guid)Session["AuthorId"];
            using (var session = NHibernateHelper.CreateSession())
            {
                var book = session.Query<Book>().FirstOrDefault(b => b.Author.Id == authorId && b.Id == id);
                return View(book);
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var existingBook = session.Get<Book>(id);
                    session.Delete(existingBook);
                    transaction.Commit();
                    return RedirectToAction("GetBooks");
                }
            }
        }
        public ActionResult Create()
        {
            if (Session["AuthorId"] == null)
            {
                return RedirectToAction("Login", "Author");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Create(Book book)
        {
            if (Session["AuthorId"] == null)
            {
                return RedirectToAction("Login", "Author");
            }
            Guid authorId = (Guid)Session["AuthorId"];
            using (var session = NHibernateHelper.CreateSession())
            {
                using(var transaction = session.BeginTransaction())
                {
                    var author = session.Query<Author>().FirstOrDefault(a => a.Id == authorId);
                    book.Author = author;
                    session.Save(book);
                    transaction.Commit();
                    return RedirectToAction("GetBooks");
                }
            }
        }
    }

}