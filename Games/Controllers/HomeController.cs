using Games.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Games.Controllers
{
    public class HomeController : Controller
    {
        private Entities db = new Entities();
        public ActionResult Index(string searchString, string category)
        {

            List<string> categoryList = new List<string>();
            var categoryQuery = from g in db.game
                             orderby g.category
                             select g.category;
            var allGames = from m in db.game
                            select m;
            categoryList.AddRange(categoryQuery.Distinct());
            ViewBag.category = new SelectList(categoryList);



            if (!string.IsNullOrEmpty(searchString))
            {
                allGames = allGames.Where(x => x.title.Contains(searchString));

            }
            if (!string.IsNullOrEmpty(category))
            {
                allGames = allGames.Where(x => x.category == category);

            }
            if (string.IsNullOrEmpty(category) && string.IsNullOrEmpty(searchString))
            {
                allGames = (from m in db.game
                            orderby m.Id descending
                            select m).Take(10);
                ViewBag.mostplayed = (from m in db.game
                            orderby m.playcount descending
                            select m).Take(10);
            }

            return View(allGames);
        }

        public ActionResult About()
        {
                       return View();
        }

 
        public ActionResult game(int? gameID)
        {
            var allGames = from m in db.game
                           select m;
            if (gameID == null)
            {
                return RedirectToAction("Index");
            }
            game game = db.game.Find(gameID);
            if (game == null)
            {
                return RedirectToAction("Index");
            }
            List<comment> commentList = new List<comment>();
            foreach (comment item in game.comment)
            {
                commentList.Add(item);
            }

            ViewBag.comments = commentList;

            List<game> gameList = new List<game>();

            allGames = allGames.Where(x => x.category == game.category);
            gameList.AddRange(allGames);
          
            ViewBag.games = gameList.Take(4);
            if (game.playcount == null)
            {
                game.playcount = 0;
            }
            game.playcount += 1;
            db.Entry(game).State = EntityState.Modified;
            db.SaveChanges();
         
            return View(game);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult game(comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.date = DateTime.Now;
                db.comment.Add(comment);
                if (comment.rating == null)
                {
                    comment.rating = 0;
                }
                db.SaveChanges();
                return RedirectToAction("game", new { gameID = comment.gameID });
            }
            

            return View(comment.gameID);
        }
        public ActionResult Games()
        {
          List<game> games = db.game.ToList();
            List<comment> comments = db.comment.ToList();
         
            foreach (game gam in games)
            {

                foreach (comment com in comments)
                {
                    if (gam.totalRating == null)
                {
                    gam.totalRating = new List<int>();
                }
                    if (com.gameID == gam.Id)
                    {
                        
                        gam.totalRating.Add( Convert.ToInt32(com.rating));

                    }
                   
                }
               
            }

           return View(games);

        }
        

    }
}