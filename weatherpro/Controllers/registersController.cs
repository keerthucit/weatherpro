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
    public class registersController : Controller
    {
        private weatherEntities db = new weatherEntities();

        public ActionResult home()
        {
            return View();
        }
     
        // GET: registers
        public async Task<ActionResult> Index()
        {
            return View(await db.registers.ToListAsync());
        }

        // GET: registers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            register register = await db.registers.FindAsync(id);
            if (register == null)
            {
                return HttpNotFound();
            }
            return View(register);
        }

        // GET: registers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: registers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,username,email,password,phone,address1,address2,city,state,country,pincode")] register register)
        {
            if (ModelState.IsValid)
            {
                db.registers.Add(register);
                await db.SaveChangesAsync();
                ModelState.AddModelError("","Registered successfully..!!You can Login now...");
            }

            return View(register);
        }

        // GET: registers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            register register = await db.registers.FindAsync(id);
            if (register == null)
            {
                return HttpNotFound();
            }
            return View(register);
        }

        // POST: registers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,username,email,password,phone,address1,address2,city,state,country,pincode")] register register)
        {
            if (ModelState.IsValid)
            {
                db.Entry(register).State = EntityState.Modified;
                await db.SaveChangesAsync();
                ModelState.AddModelError("", "Updated Successfully!!!");
                return RedirectToAction("home");
                

            }
            return View(register);
        }

        // GET: registers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            register register = await db.registers.FindAsync(id);
            if (register == null)
            {
                return HttpNotFound();
            }
            return View(register);
        }

        // POST: registers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            register register = await db.registers.FindAsync(id);
            db.registers.Remove(register);
            await db.SaveChangesAsync();
            ModelState.AddModelError("", "Deleted successfully..!!");
            return RedirectToAction("home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Login()
        {
            ViewBag.ErrorMessage = "";
            return View();
        }
        [HttpPost]

        public ActionResult Login(register u)
        {

          //  if (string.IsNullOrEmpty(u.username) || string.IsNullOrEmpty(u.password))
          //  {
           //     ViewBag.ErrorMessage = "Invalid Credentials";
          //  }
                         
             // this action is for handle post (login)
                if (ModelState.IsValid) // this is check validity
                {
                    using (weatherEntities dc = new weatherEntities())
                    {
                        var v = dc.registers.Where(a => a.username.Equals(u.username) && a.password.Equals(u.password)).FirstOrDefault();
                        if (v != null)
                        {
                            Session["LoggedID"] = v.id.ToString();
                            Session["LoggedUsername"] = v.username.ToString();
                            return RedirectToAction("AfterLogin");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Try again!! Incorrect Username or Password";
                    }
                    }
                }
            
            return View(u);

        }
        public ActionResult AfterLogin()
        {
            if (Session["LoggedID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("home");
            }
        }

    }
}
