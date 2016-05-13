using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TA.Classified.BLL.ViewModels;
using TA.Classified.Core;

namespace TA.Classified.Web.Controllers
{
  public class LoginController : Controller
  {
    public ActionResult Register()
    {
      return View();
    }

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

    public ActionResult Logout()
    {
      Session["UserEmail"] = null;
      return RedirectToAction("Index", "Classified");
    }

    //Login//
    [HttpGet]
    public ActionResult Login()
    {
      UserLoginViewModel model = new UserLoginViewModel();
      return View(model);
    }

    [HttpPost]
    public ActionResult Login(UserLoginViewModel user)
    {
      if (BLL.BLLUser.AuthenticateUser(user).Equals(true))
      {
        Session["UserEmail"] = user.EmailAddress;
        FormsAuthentication.SetAuthCookie(user.EmailAddress, true);
      }
      else
      {
        Session["UserEmail"] = null;
        ViewBag.LoginMessage = "Login Failed..Please enter valid credentials";
        return View(user);
      }
      return RedirectToAction("Index", "Classified");
    }
  }
}
