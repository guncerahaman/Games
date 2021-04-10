using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Games.Models;

namespace Games.Controllers
{
    //[Authorize]
    public class AdminController : Controller
    {
        private Entities db = new Entities();

        // GET: admin
        public ActionResult Index()
        {
            return View(db.game.ToList());
        }
        public ActionResult Comments()
        {
            return View(db.comment.ToList());
        }

        
        public ActionResult DeleteComment(int id)
        {
            comment comment = db.comment.Find(id);
            db.comment.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("comments");
        }
        // GET: admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            game game = db.game.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            List<comment> commentList = new List<comment>();
            foreach (comment item in game.comment)
            {
                commentList.Add(item);
            }

            ViewBag.comments = commentList.Count;
            return View(game);
        }

        // GET: admin/Create
        public ActionResult Create()
        {
            ViewBag.categories = game.categories;
            return View();
        }

        // POST: admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,title,image,link,width,height,description,category")] game game)
        {

            if (ModelState.IsValid)
            {
                game.playcount = 0;
                db.game.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categories = game.categories;
            return View(game);
        }

        // GET: admin/Edit/5
        public ActionResult Edit(int? id)
        {
   
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            game game = db.game.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            ViewBag.categories = game.categories;
            return View(game);
        }

        // POST: admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,title,image,link,width,height,description,category")] game game)
        {
          
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categories = game.categories;
            return View(game);
        }

        // GET: admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            game game = db.game.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            game game = db.game.Find(id);
            db.game.Remove(game);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
