using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TA.Classified.Core;

namespace TA.Classified.Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: UserLogin
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserLogin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserLogin/Create
        public ActionResult Register()
        {
            return View();
        }

        // POST: UserLogin/Create
        [HttpPost]
        public ActionResult Register(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //Login//
        [HttpGet]
            public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            return View();
        }

        // GET: UserLogin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserLogin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UserLogin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserLogin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
