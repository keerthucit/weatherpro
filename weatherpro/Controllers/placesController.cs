using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using weatherpro.Models.DB;

namespace weatherpro.Controllers
{
    public class placesController : Controller
    {
        private weatherfavEntities db = new weatherfavEntities();

        public ActionResult home()
        {

            return View();
        }



        public ActionResult Search(string city)
        {
            place u = new place();

            using (weatherfavEntities dc = new weatherfavEntities())
            {
                var v = dc.places.Where(a => a.city.Equals(city)).FirstOrDefault();

                if (v != null)
                {
                    Session["LoggedID"] = v.pid.ToString();
                    Session["LoggedCityname"] = v.city.ToString();
                }
                else
                {
                    ViewBag.ErrorMessage = "invalid entry";
                }
                return View(v);
            }
        }

        //     return View(await db.places.ToListAsync());

        /*  [HttpPost]
         public ActionResult Search(place u)
          {
              if (ModelState.IsValid) // this is check validity
              {
                  using (weatherfavEntities dc = new weatherfavEntities())
                  {
                      var v = dc.places.Where(a => a.city.Equals(u.city)).FirstOrDefault();

                      if (v != null)
                      {
                          Session["LoggedCityname"] = v.city.ToString();
                      }
                      else
                      {
                          ViewBag.ErrorMessage = "invalid entry";
                      }
                  }
              }
                  return View(u);
              }
       
          */
        // GET: places
        public ActionResult Index()
        {

            return View();
        }

        // GET: places/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            place place = await db.places.FindAsync(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }

        // GET: places/Create
        public ActionResult Create()
        {
            ViewBag.pid = new SelectList(db.wdays, "pid", "pid");
            ViewBag.pid = new SelectList(db.wthreehs, "pid", "weather_description");
            return View();
        }

        // POST: places/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "pid,city,coord_lon,coord_lat")] place place)
        {
            if (ModelState.IsValid)
            {
                db.places.Add(place);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.pid = new SelectList(db.wdays, "pid", "pid", place.pid);
            ViewBag.pid = new SelectList(db.wthreehs, "pid", "weather_description", place.pid);
            return View(place);
        }

        // GET: places/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            place place = await db.places.FindAsync(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            ViewBag.pid = new SelectList(db.wdays, "pid", "pid", place.pid);
            ViewBag.pid = new SelectList(db.wthreehs, "pid", "weather_description", place.pid);
            return View(place);
        }

        // POST: places/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "pid,city,coord_lon,coord_lat")] place place)
        {
            if (ModelState.IsValid)
            {
                db.Entry(place).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.pid = new SelectList(db.wdays, "pid", "pid", place.pid);
            ViewBag.pid = new SelectList(db.wthreehs, "pid", "weather_description", place.pid);
            return View(place);
        }

        // GET: places/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            place place = await db.places.FindAsync(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }

        // POST: places/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            place place = await db.places.FindAsync(id);
            db.places.Remove(place);
            await db.SaveChangesAsync();
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
