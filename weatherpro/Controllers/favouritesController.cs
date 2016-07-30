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
    public class favouritesController : Controller
    {
        private weatherfavEntities db = new weatherfavEntities();

        // GET: favourites
        public async Task<ActionResult> Index()
        {
            var favourites = db.favourites.Include(f => f.place).Include(f => f.register);
            return View(await favourites.ToListAsync());
        }

        // GET: favourites/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            favourite favourite = await db.favourites.FindAsync(id);
            if (favourite == null)
            {
                return HttpNotFound();
            }
            return View(favourite);
        }

        // GET: favourites/Create
        public ActionResult Create()
        {
            ViewBag.pid = new SelectList(db.places, "pid", "city");
            ViewBag.u_id = new SelectList(db.registers, "u_id", "username");
            return View();
        }

        // POST: favourites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "fid,pid,u_id")] favourite favourite)
        {
            if (ModelState.IsValid)
            {
                db.favourites.Add(favourite);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.pid = new SelectList(db.places, "pid", "city", favourite.pid);
            ViewBag.u_id = new SelectList(db.registers, "u_id", "username", favourite.u_id);
            return View(favourite);
        }

        // GET: favourites/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            favourite favourite = await db.favourites.FindAsync(id);
            if (favourite == null)
            {
                return HttpNotFound();
            }
            ViewBag.pid = new SelectList(db.places, "pid", "city", favourite.pid);
            ViewBag.u_id = new SelectList(db.registers, "u_id", "username", favourite.u_id);
            return View(favourite);
        }

        // POST: favourites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "fid,pid,u_id")] favourite favourite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(favourite).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.pid = new SelectList(db.places, "pid", "city", favourite.pid);
            ViewBag.u_id = new SelectList(db.registers, "u_id", "username", favourite.u_id);
            return View(favourite);
        }

        // GET: favourites/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            favourite favourite = await db.favourites.FindAsync(id);
            if (favourite == null)
            {
                return HttpNotFound();
            }
            return View(favourite);
        }

        // POST: favourites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            favourite favourite = await db.favourites.FindAsync(id);
            db.favourites.Remove(favourite);
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
