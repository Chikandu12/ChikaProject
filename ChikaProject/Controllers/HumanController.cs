using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChikaProject.Models;

namespace ChikaProject.Controllers
{
    public class HumanController : Controller
    {
        private HumanDbContext db = new HumanDbContext();

        // GET: Human
        [HttpGet]
        public ActionResult Login()
        { 
            return View();
        }

        [HttpPost]
        public ActionResult Login(string e, string p)
        {
            HumanDbContext db = new HumanDbContext();

            //Get List of Recorded from table using LINQ Query Format.
            var human = from h in db.HumanTbs.ToList()
                        where Equals(h.Email, e) && Equals(h.Password, p)
                        select h;

            //Convert "human" to  List.
            List<HumanTb> tb = (List<HumanTb>)human.ToList();

            if (tb.Count == 0)
            {
                return View();
            }

            //Get First Data
            HumanTb fd = tb.ElementAt(0);

            //Save To A Variable
            TempData["Data"] = fd;
            return RedirectToAction("HomePage");

        }

        public ActionResult HomePage()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(HumanTb obj)
        {
            obj.Email = obj.Email.Trim();
            HumanDbContext db = new HumanDbContext();
            db.HumanTbs.Add(obj);
            db.SaveChanges();

            return RedirectToAction("Login");
        }

        public ActionResult About()
        {
            return View();
        }

        // GET: Human/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HumanTb humanTb = db.HumanTbs.Find(id);
            if (humanTb == null)
            {
                return HttpNotFound();
            }
            return View(humanTb);
        }

        // GET: Human/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Human/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Email,FirstName,Password")] HumanTb humanTb)
        {
            if (ModelState.IsValid)
            {
                db.HumanTbs.Add(humanTb);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(humanTb);
        }

        
        [HttpGet]
        public ActionResult Edit(string em)
        {
            //string email = Request.QueryString["email"];
            if (string.IsNullOrEmpty(em))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HumanTb humanTb = db.HumanTbs.Find(em);
            if (humanTb == null)
            {
                return HttpNotFound();
            }
            return View(humanTb);
            //return View();
        }

      

        // POST: Human/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Email,FirstName,Password")] HumanTb humanTb)
        {
            if (ModelState.IsValid)
            {
                db.Entry(humanTb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(humanTb);
        }

        // GET: Human/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HumanTb humanTb = db.HumanTbs.Find(id);
            if (humanTb == null)
            {
                return HttpNotFound();
            }
            return View(humanTb);
        }

        // POST: Human/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HumanTb humanTb = db.HumanTbs.Find(id);
            db.HumanTbs.Remove(humanTb);
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
