using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TA.Classified.BLL;
using TA.Classified.BLL.ViewModels;
using TA.Classified.DataAccess;
using Google.Apis.Plus.v1;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Auth.OAuth2.Flows;
using System.Threading;
using Google.Apis.Oauth2.v2;
using Google.Apis.Plus.v1.Data;
using Google.Apis.Oauth2.v2.Data;
using System.Threading.Tasks;
using System.Configuration;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.EntityFramework;


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
        [AllowAnonymous]
        public ActionResult Confirmation()
        {
            Guid TokenId = Guid.Parse(Request.QueryString["id"]);
            BLLUser confirmeduser = new BLLUser();
            if (confirmeduser.Confirmation(TokenId))
            {
                ViewBag.success = "Account Activated.";
                return View();
            }
            else
            {
                ViewBag.error = "Activation Link Expired. Please try again";
                return View();
            }
        }
        [HttpGet]
        public ActionResult Register()
        {
            country();
            TAC_Team5Entities entities = new TAC_Team5Entities();
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserRegisterViewModel model, string ConfirmPassword, bool Terms = false)
        {
            ModelState.Clear();

            BLLUser newuser = new BLLUser();
            bool status = newuser.UserRegistartion(model);
            if (status)
            {
                newuser.Registration(model, false);
                ViewBag.Email = "Confirmation mail has been sent to your given Email.";
                return View();
            }
            else
            {
                ViewBag.UserExists = "Email Id is already registered.";
                country();
                return View();
            }
        }


        public ActionResult Logout()
        {
            //Session["UserEmail"] = null;
            var AutheticationManager = HttpContext.GetOwinContext().Authentication;
            AuthenticationManager.SignOut();
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
        [AllowAnonymous]
        public async Task<ActionResult> Login(UserLoginViewModel user, string returnUrl, string Command)
        {
            if (Command == "login")
            {

                ModelState.Clear();
                //var user = await UserManager.FindAsync(model.Email, model.UPassword);
                BLLUser userverification = new BLLUser();
                TAC_User usermodel = userverification.UserVerification(user);
                if(usermodel !=null)
                { 
                ApplicationUser appUser = addClaims(usermodel.Email, usermodel.UserId.ToString(), usermodel.First_Name != null ? usermodel.First_Name : usermodel.Email);

                    //Session["UserEmail"] = usermodel.Email;
                    System.Web.HttpContext.Current.Session["UserEmail"] = usermodel.Email;

                    await SignInAsync(appUser, user.rememberme);
                    
                    //var result = await SignInAsync(userverification.UserVerification(user));
                
                   
                    if (this.Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    else
                    {
                        //return RedirectToAction("Index", "Classified");
                        return RedirectToAction("UpdateProfile", "Login");
                    }
                }
                else
                {
                    ViewBag.ErrorMsg = "Invalid credentials";
                }
            }
            else  {
                return RedirectToAction("Register", "Login");
            }


            return View(user);
        }
        [AllowAnonymous]

        public async Task<ActionResult> gplusLogin(bool Rememberme = false)
        {
            TA.Classified.DataAccess.TAC_User userDetails = new TA.Classified.DataAccess.TAC_User();
            ClientSecrets secrets = new ClientSecrets()
            {
                ClientId = ConfigurationManager.AppSettings["gplus-client-id"],
                ClientSecret = ConfigurationManager.AppSettings["gplus-client-secret"]
            };
            string[] SCOPES = { PlusService.Scope.PlusLogin, PlusService.Scope.UserinfoEmail };
            TokenResponse token;
            PlusService ps = null;

            IAuthorizationCodeFlow flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = secrets,
                Scopes = SCOPES
            });

            if (Request["code"] == null)
            {
                string url =
                     "https://accounts.google.com/o/oauth2/auth?redirect_uri=http://localhost:50682/Login/gplusLogin&response_type=code&scope=https://www.googleapis.com/auth/userinfo.email&openid.realm=&client_id=980354972421-11jkjchb98vc20jl7psppmnnt72tik09.apps.googleusercontent.com&access_type=offline&approval_prompt=force";
                Response.Redirect(url);
            }
            else
            {
                token = flow.ExchangeCodeForTokenAsync("", Request["code"], "http://localhost:50682/Login/gplusLogin",
                          CancellationToken.None).Result;
                // Get tokeninfo for the access token if you want to verify.
                Oauth2Service service = new Oauth2Service(
                    new Google.Apis.Services.BaseClientService.Initializer());
                Oauth2Service.TokeninfoRequest request = service.Tokeninfo();
                request.AccessToken = token.AccessToken;

                Tokeninfo info = request.Execute();
                string gplus_id = info.UserId;

                UserCredential gplusUserCredential = new UserCredential(flow, "me", token);
                ps = new PlusService(
                    new Google.Apis.Services.BaseClientService.Initializer()
                    {
                        ApplicationName = "TA-Classifieds",
                        HttpClientInitializer = gplusUserCredential
                    });

                Person some = ps.People.Get("me").Execute();

                userDetails.First_Name = some.Name.GivenName;
                userDetails.Last_Name = some.Name.FamilyName;
                userDetails.Email = some.Emails[0].Value;

                // userModel = new Model.User();
                UserRegisterViewModel userModel = new UserRegisterViewModel();
                userModel.isActive = true;
                userModel.isVerified = true;
                userModel.Email = some.Emails[0].Value;
                userModel.UPassword = "Dummy Password";
                BLLUser bl = new BLLUser();
                Session["UserEmail"] = userDetails.Email;
                ApplicationUser appUser = addClaims(userModel.Email, userModel.UPassword.ToString(), userModel.First_Name != null ? userModel.First_Name : userModel.Email);
                await SignInAsync(appUser, Rememberme);
                if (bl.UserRegistartion(userModel))
                {
                   // return RedirectToAction("Register", "Login");
                   bl.Registration(userModel, true);
                }

                BLLUser loggedinuser = new BLLUser();

            }

            return RedirectToAction("UpdateProfile", "Login");
        }

        private const string XsrfKey = "XsrfId";
        private string getClaims(string claimType)
        {
            string returnClaimValue = string.Empty;
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            switch (claimType)
            {
                case "email":
                    var resClaimArray = claims.Where(m => m.Type=="email");
                    returnClaimValue = resClaimArray.FirstOrDefault().Value;
                    break;
                case "guid":
                    resClaimArray = claims.Where(m => m.Type == "guid");
                    returnClaimValue = resClaimArray.FirstOrDefault().Value;
                    break;
                default:
                    returnClaimValue = string.Empty;
                    break;
            }

            return returnClaimValue;
        }

        private ApplicationUser addClaims(string email, string guid, string username)
        {
            ApplicationUser resUser = new ApplicationUser();
            resUser.UserName = username;
            resUser.SecurityStamp = Guid.NewGuid().ToString();
            resUser.Claims.Add(new IdentityUserClaim() { ClaimType = "email", ClaimValue = email });
            resUser.Claims.Add(new IdentityUserClaim() { ClaimType = "guid", ClaimValue = guid });
            return resUser;
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            string RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
            string UserIdClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
            string UserNameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
            var emailUser = user.Claims.Where(m => m.ClaimType == "email").FirstOrDefault().ClaimValue != null ? user.Claims.Where(m => m.ClaimType == "email").FirstOrDefault().ClaimValue : string.Empty;
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            ClaimsIdentity identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie, UserNameClaimType, RoleClaimType);
            identity.AddClaim(new Claim(UserIdClaimType, user.Id, "http://www.w3.org/2001/XMLSchema#string"));
            identity.AddClaim(new Claim(UserNameClaimType, user.UserName != null ? user.UserName : emailUser, "http://www.w3.org/2001/XMLSchema#string"));
            identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"));

            foreach (var item in user.Claims)
            {
                identity.AddClaim(new Claim(item.ClaimType, item.ClaimValue));
            }
            var prop = new AuthenticationProperties();
            prop.IsPersistent = isPersistent;
            if (isPersistent)
            {
                prop.ExpiresUtc = DateTime.Now.AddHours(5);
            }
            AuthenticationManager.SignIn(prop, identity);
        }



        [HttpGet]
        public ActionResult UpdateProfile()
        {

            country();
            string Userguid = getClaims("email");
            BLLUser obj = new BLLUser();
           TAC_User updateduser = obj.FetchUser(Userguid);
            return View("Update_Profile", updateduser);
        }

        [HttpPost]
        public ActionResult UpdateProfile(TAC_User user)
        {
            
            BLLUser newuser = new BLLUser();
            // string UserId = getClaims("guid");
            //Guid userid = user.UserId;
            //user.UserId= Guid.Parse(UserId);
            newuser.updateUser(user);
            return RedirectToAction("Index", "Classified");
        }
    }
}
