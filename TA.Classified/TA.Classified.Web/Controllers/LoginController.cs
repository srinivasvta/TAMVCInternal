using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TA.Classified.BLL;
using TA.Classified.BLL.ViewModels;
using TA.Classified.Core;
using TA.Classified.DataAccess;

namespace TA.Classified.Web.Controllers
{
  public class LoginController : Controller
  {
        //get country//
        private void country()
        {

            TAC_Team5Entities entities = new TAC_Team5Entities();
            var countries = entities.TAC_Country.ToList();
            IEnumerable<SelectListItem> items = countries.Select(m => new SelectListItem
            {
                Value = m.CountryId.ToString(),
                Text = m.CountryName
            });
            ViewBag.CountryId = items;


        }
        [HttpGet]
        public ActionResult Register()
    {
            country();
            TAC_Team5Entities entities = new TAC_Team5Entities();
            return View();
    }

    [HttpPost]
    public ActionResult Register(UserRegisterViewModel model)
    {

            // TODO: Add insert logic here

            try
            {
                if (ModelState.IsValid)
                {
                    BLLUser b = new BLLUser();
                    b.RegisterUser(model);
                    ViewBag.Successmessage = "Successfully Registered.";
                    return RedirectToAction("Successful");
                    //country();  
                }
               
                // dropdownbind();
                //return View();
                
            }
            catch (Exception e)
            {
                ViewBag.Failuremessage = "Email Already Registered try with another Email";
                //return View();
            }
            country();
            return View();

        }

        public ActionResult Logout()
    {
      Session["UserEmail"] = null;
      return RedirectToAction("Index", "Classified");
    }

        //Suucessfully registered message

            [HttpGet]
            public ActionResult Successful()
        {
            return View();
        }
    //Login//
    [HttpGet]
    public ActionResult Login()
    {
      UserLoginViewModel model = new UserLoginViewModel();
      return View(model);
    }
        

    [HttpPost]
     public ActionResult Login(UserLoginViewModel user, string Command, string returnUrl)
        {
            if (Command == "login")
            {
                if (BLL.BLLUser.AuthenticateUser(user).Equals(true))
                {

                    Session["UserEmail"] = user.EmailAddress;
                    FormsAuthentication.SetAuthCookie(user.EmailAddress, user.rememberme);
                    if (this.Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                }

                else
                {
                    Session["UserEmail"] = null;
                    ViewBag.LoginMessage = "Login Failed..Please enter valid credentials";
                    return View(user);
                }
            }
            else if (Command == "register")
            {
                return RedirectToAction("Register", "Login");
            }

            return RedirectToAction("Index", "Classified");
        }
    }
}
