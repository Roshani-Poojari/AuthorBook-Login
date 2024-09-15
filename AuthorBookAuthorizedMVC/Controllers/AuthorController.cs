using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using AuthorBookAuthorizedMVC.Data;
using AuthorBookAuthorizedMVC.Models;
using NHibernate.Linq;

namespace AuthorBookAuthorizedMVC.Controllers
{
    [AllowAnonymous]
    public class AuthorController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Author author)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var findAuthor = session.Query<Author>().SingleOrDefault(a => a.Name == author.Name);
                    if (findAuthor != null)
                    {
                        if (BCrypt.Net.BCrypt.Verify(author.Password, findAuthor.Password))
                        {
                            FormsAuthentication.SetAuthCookie(author.Name,true);
                            Session["AuthorId"] = findAuthor.Id;
                            return RedirectToAction("GetAuthorDetails","AuthorDetail");
                        }
                    }
                    ModelState.AddModelError("", "UserName/Password doesn't exists");
                    return View();
                }
            }
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Author author)
        {
            author.Password = BCrypt.Net.BCrypt.HashPassword(author.Password);
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    author.AuthorDetail.Author = author;
                    session.Save(author);
                    transaction.Commit();
                    return RedirectToAction("Login");
                }
            }
        }
        public ActionResult GetAllBooks()
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                var books = session.Query<Book>().Fetch(b=>b.Author).ToList();
                return View(books);
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}