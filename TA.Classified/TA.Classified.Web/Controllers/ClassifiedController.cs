using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TA.Classified.BLL.ViewModels;
using TA.Classified.DataAccess;

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
        private void Category()
        {
            TAC_Team5Entities entities = new TAC_Team5Entities();
            var Category = entities.TAC_Category.ToList();
            IEnumerable<SelectListItem> categories = Category.Select(m => new SelectListItem
            {
                Value = m.CategoryId.ToString(),
                Text = m.CategoryName
            });
            ViewBag.CategoryId = categories;
        }

        public ActionResult MyAccount(int? page)
        {
            if (page == 0||page == null)
            {
                page = 1;
            }
            if (Request.IsAuthenticated)
            {
                
                return View(BLL.BLLClassified.GetClassifiedsbyUser(Session["UserEmail"].ToString(), 3, Convert.ToInt32(page)));
                //return View(BLL.BLLClassified.GetClassifiedsbyUser(s).ToString(),3,Convert.ToInt32(page)));
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

        [Authorize]
        [HttpGet]
        public ActionResult PostAd()
        {
            //Category();
            var q = BLL.BLLCategory.GetCategories();
            ViewData["Categories"] = q;
            return View();
        }

        [HttpPost]
        public ActionResult PostAd(HttpPostedFileBase ClassifiedImage, PostAdViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    
                    if (ClassifiedImage != null)
                    {                  
                        ClassifiedViewModel classified = new ClassifiedViewModel();
                        classified.CategoryName = Session["CategoryName"].ToString();
                        ClassifiedImage.SaveAs(HttpContext.Server.MapPath("~/content/images/") + ClassifiedImage.FileName);
                        classified.ClassifiedImage = "~/content/images/" + ClassifiedImage.FileName;
                        classified.ClassifiedPrice = model.ClassifiedPrice;
                        classified.ClassifiedTitle = model.ClassifiedTitle;
                        classified.ContactCity = model.ContactCity;
                        classified.ContactName = model.ContactName;
                        classified.ContactPhone = model.ContactPhone;
                        classified.Createdby = Session["UserEmail"]?.ToString();
                        classified.PostedDate = DateTime.Now;
                        classified.Summary = model.Summary;
                        classified.Description = model.Description;
                        if (BLL.BLLClassified.AddClassified(classified).Equals(true))
                        {
                            ViewBag.PostAdMessage = "Ad/Classified published successfully";
                        }
                        else
                        {
                            ViewBag.PostAdMessage = "Internal server issues, please try again later";
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }




            return View(model);
        }



    }
}