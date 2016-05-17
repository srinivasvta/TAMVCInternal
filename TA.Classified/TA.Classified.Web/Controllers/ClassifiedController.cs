using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TA.Classified.BLL.ViewModels;

namespace TA.Classified.Web.Controllers
{
  public class ClassifiedController : Controller
  {
    // GET: Classified
    public ActionResult Index(int? page)

    {
      if (page == 0 || page == null)
      {
        page = 1;
      }
      if (Session["categoryName"] != null)
      {
        return View(BLL.BLLClassified.GetClassifiedsbyCategory(Session["categoryName"].ToString(), 3, (int)page));
      }
      else
      {
        return View(BLL.BLLClassified.GetAllClassifieds(3, (int)page));
      }
    }

    public ActionResult MyAccount(int? page)
    {
      if (page == 0)
      {
        page = 1;
      }
      if (Session["UserEmail"] != null)
      {
        return View(BLL.BLLClassified.GetClassifiedsbyUser(Session["UserEmail"].ToString(), 3, (int)page));
      }
      else
      {
        return RedirectToAction("Login", "Login");
      }
    }


    public ActionResult ViewDetail(int ClassifiedId)
    {
      if (ClassifiedId != 0)
      {
        return View(BLL.BLLClassified.GetClassifiedById(ClassifiedId));
      }
      else
        return RedirectToAction("Index", "Home");
    }

    public ActionResult PostAd()
    {
      PostAdViewModel viewModel = new PostAdViewModel();
      return View(viewModel);
    }

    [HttpPost]
    public ActionResult PostAd(PostAdViewModel model)
    {
      model.ClassifiedImage.SaveAs(Server.MapPath(@"/content/images/" + model.ClassifiedImage.FileName));
      ClassifiedViewModel classified = new ClassifiedViewModel();
      classified.ClassifiedImage = @"/content/images/" + model.ClassifiedImage.FileName;
      classified.ClassifiedPrice = model.ClassifiedPrice;
      classified.ClassifiedTitle = model.ClassifiedTitle;
      classified.ContactCity = model.ContactCity;
      classified.ContactName = model.ContactName;
      classified.ContactPhone = model.ContactPhone;
      classified.Createdby = Session["UserEmail"]?.ToString();
      classified.PostedDate = DateTime.Now;
      classified.Summary = model.Summary;
      classified.Description = model.Description;
      classified.CategoryName = Session["categoryName"]?.ToString();
      if (BLL.BLLClassified.AddClassified(classified).Equals(true))
      {
        ViewBag.PostAdMessage = "Ad/Classified published successfully";
      }
      else
      {
        ViewBag.PostAdMessage = "Internal server issues, please try again later";
      }

      return View(model);
    }
  }
}