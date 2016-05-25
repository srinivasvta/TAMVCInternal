using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace TA.Classified.Web.Controllers
{
  public class CategoryController : Controller
  {
    // GET: CategoryListing
    public ActionResult Index()
    {
      return PartialView("_CategoryListing", BLL.BLLCategory.GetCategories());
    }

  
    public ActionResult Category(string categoryName)
    {
      Session["categoryName"] = categoryName;
      return Redirect(Request.UrlReferrer.ToString());
    }
  }
}