using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AuthorBookAuthorizedMVC.Data;
using AuthorBookAuthorizedMVC.Models;
using NHibernate.Linq;

namespace AuthorBookAuthorizedMVC.Controllers
{
    [Authorize]
    public class AuthorDetailController : Controller
    {
        // GET: AuthorDetail
        public ActionResult GetAuthorDetails()
        {
            if (Session["AuthorId"] == null)
            {
                return RedirectToAction("Login", "Author");
            }
            Guid authorId = (Guid)Session["AuthorId"];
            using (var session = NHibernateHelper.CreateSession())
            {
                var authordetail = session.Query<AuthorDetail>().Fetch(ad => ad.Author).FirstOrDefault(ad => ad.Author.Id == authorId);
                return View(authordetail);
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
                var authordetail = session.Query<AuthorDetail>().FirstOrDefault(ad => ad.Author.Id == authorId && ad.Id == id);
                return View(authordetail);
            }
        }
        [HttpPost]
        public ActionResult Edit(AuthorDetail authorDetail)
        {
            if (Session["AuthorId"] == null)
            {
                return RedirectToAction("Login", "Author");
            }
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var existingAuthorDetail = session.Get<AuthorDetail>(authorDetail.Id);
                    existingAuthorDetail.Street = authorDetail.Street;
                    existingAuthorDetail.City = authorDetail.City;
                    existingAuthorDetail.State = authorDetail.State;
                    existingAuthorDetail.Country = authorDetail.Country;
                    existingAuthorDetail.IsActive = true;
                    session.Update(existingAuthorDetail);
                    transaction.Commit();
                    return RedirectToAction("GetAuthorDetails");
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
                var authordetail = session.Query<AuthorDetail>().FirstOrDefault(ad => ad.Author.Id == authorId && ad.Id == id);
                return View(authordetail);
            }
        }
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var existingAuthorDetail = session.Get<AuthorDetail>(id);
                    existingAuthorDetail.IsActive = false;
                    existingAuthorDetail.Street = null;
                    existingAuthorDetail.City = null;
                    existingAuthorDetail.State = null;  
                    existingAuthorDetail.Country = null;
                    session.Update(existingAuthorDetail);
                    transaction.Commit();
                    return RedirectToAction("GetAuthorDetails");
                }
            }
        }
    }
}