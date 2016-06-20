using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TA.Classified.BLL.ViewModels;
using TA.Classified.DataAccess;

namespace TA.Classified.BLL
{
    public class BLLUser
    {
        private TAC_Team5Entities entities = new TAC_Team5Entities();
        private TAC_User User = new TAC_User();
        
        public Boolean Confirmation(Guid ticketId)
        {

            TAC_TicketVerification vp = entities.TAC_TicketVerification.Where(m => m.TicketId == ticketId).FirstOrDefault();
            //if (vp.CreatedDate.AddHours(24)>DateTime.Now)
            if (vp.CreatedDate.AddHours(24) > DateTime.Now && vp.IsUsed != true)
            {
                vp.TicketId = ticketId;
                vp.IsUsed = true;
                //entities.TAC_TicketVerification.Add(vp);
                entities.TAC_TicketVerification.Attach(vp);
                entities.Entry(vp).State = System.Data.Entity.EntityState.Modified;
                entities.SaveChanges();
                Guid userid = vp.UserId;
                TAC_User u = entities.TAC_User.Where(m => m.UserId == userid).FirstOrDefault();
                //u. = userid;
                u.IsVerified = true;
                u.IsActive = true;
                entities.TAC_User.Attach(u);
                entities.Entry(u).State = System.Data.Entity.EntityState.Modified;
                entities.SaveChanges();
                return true;
            }
            else
            {
                vp.IsExpired = true;
                entities.TAC_TicketVerification.Add(vp);
                entities.SaveChanges();
                Guid userid = vp.UserId;
                TAC_User op = entities.TAC_User.Where(m => m.UserId == userid).FirstOrDefault();
                op.IsActive = false;
                entities.TAC_User.Attach(op);
                entities.Entry(op).State = System.Data.Entity.EntityState.Modified;
                entities.SaveChanges();
                return false;
            }
        }

        public bool UserRegistartion(UserRegisterViewModel userRegistration)
        {
            using (TAC_Team5Entities entities = new TAC_Team5Entities())
            {
                TAC_User user = new TAC_User();
                IEnumerable<TAC_User> u = entities.TAC_User.Where(m => m.Email == userRegistration.Email);
                var detailsOfUser = u.FirstOrDefault();
                if (detailsOfUser != null)
                {
                    if (detailsOfUser.IsVerified == true && detailsOfUser.IsActive == true)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                return true;
            }
        }
        private string Encryption(string data)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(data);
            byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }
        public TAC_User UserVerification(UserLoginViewModel model)
        {
            string password = Encryption(model.Password);
            TAC_User user = new TAC_User();
            IEnumerable<TAC_User> u = entities.TAC_User.Where(m => m.Email == model.EmailAddress && m.UPassword == model.Password); 
            TAC_User op = null;
            foreach (TAC_User member in u)
            {
                if (!member.IsActive.Equals(false) && (member.IsVerified.Equals(true)))
                {
                    op = member;
                    break;
                }
            }
            return op;
        }
       
        public TAC_User FetchUserInfo(Guid userid)
        {
            return entities.TAC_User.Find(userid);
        }
        public TAC_User FetchUser(string email)
        {
            var user = entities.TAC_User.Where(m => m.Email == email).FirstOrDefault();
            TAC_User u = user;
            return u;
        }

        public void Registration(UserRegisterViewModel registerUser, Boolean isSocialLogin)
        {
            //check for user exist
            TAC_User newUser = new TAC_User();
            var user = entities.TAC_User.Where(m => m.Email == registerUser.Email).Count();
            if (user != 0 && !newUser.IsVerified.HasValue)
            {
                //send new verification link
                var verification = new TAC_TicketVerification() { TicketId = Guid.NewGuid(), UserId = newUser.UserId, CreatedDate = DateTime.Now };
                // uw.VerifyTokenRepository.Insert(tokenverification1);
               var userVerification1 = entities.TAC_TicketVerification.Add(verification);
                entities.SaveChanges();
                if (!isSocialLogin)
                {
                    AccountActivation(registerUser, userVerification1);
                }

            }
            else
            {
                string encryptedpwd = Encryption(registerUser.UPassword);
                
                newUser.UserId = Guid.NewGuid();
                newUser.Email = registerUser.Email;
                newUser.UPassword = registerUser.UPassword;
                newUser.First_Name = registerUser.First_Name;
                newUser.Last_Name = registerUser.Last_Name;
                newUser.Gender = registerUser.Gender;
                newUser.DOB = registerUser.DOB;
                newUser.Address1 = registerUser.Address1;
                newUser.Address2 = registerUser.Address2;
                newUser.City = registerUser.City;
                newUser.State = registerUser.State;
                newUser.Country = Convert.ToInt16(registerUser.Country);
                if(newUser.Country==0)
                {
                    newUser.Country = 1;
                }
                newUser.Phone = registerUser.Phone;
                newUser.CreatedDate = DateTime.Now;
                newUser.IsVerified = registerUser.isVerified != null ? registerUser.isVerified : null;
                newUser.IsActive = registerUser.isActive != null ? registerUser.isActive : null;
            }
            
                var insertedUser = entities.TAC_User.Add(newUser);
                entities.SaveChanges();
                var tokenverification = new TAC_TicketVerification() { TicketId = Guid.NewGuid(), UserId = newUser.UserId, CreatedDate = DateTime.Now };
                var userVerification = entities.TAC_TicketVerification.Add(tokenverification);
                entities.SaveChanges();
            if (!isSocialLogin)
            {
                AccountActivation(registerUser, userVerification);
            }
        }

       

            public void updateUser(TAC_User registerUser)
        {
            TAC_User newUser = FetchUserInfo(registerUser.UserId);
            //newUser.UserId = registerUser.UserId;
           //newUser.Email = registerUser.Email;
            newUser.UPassword = registerUser.UPassword;
            newUser.First_Name = registerUser.First_Name;
            newUser.Last_Name = registerUser.Last_Name;
            newUser.Gender = registerUser.Gender;
            newUser.DOB = registerUser.DOB;
            newUser.Address1 = registerUser.Address1;
            newUser.Address2 = registerUser.Address2;
            newUser.City = registerUser.City;
            newUser.State = registerUser.State;
            newUser.Country = Convert.ToInt16(registerUser.Country);
            newUser.Phone = registerUser.Phone;
            newUser.CreatedDate = DateTime.Now;
            newUser.IsVerified = true;
            newUser.IsActive = true;
            entities.Entry(newUser).State = System.Data.Entity.EntityState.Modified;
            entities.SaveChanges();
        }

        private void AccountActivation(UserRegisterViewModel registerUser, TAC_TicketVerification userVerification)
        {

            MailMessage mailmessage = new MailMessage();
            mailmessage.IsBodyHtml = true;
            string ActivationUrl = HttpContext.Current.Server.HtmlEncode("http://localhost:50682/Login/Confirmation" + "?id=" + userVerification.TicketId);
            mailmessage.Subject = "Confirmation email for account activation- TA Classifieds";
            mailmessage.Body = "Hi," + "!\n" + "Please <a href='" + ActivationUrl + "'>click here</a> the following link to activate your account." + "!\n" + "TA Classifieds.";
            mailmessage.From = new MailAddress("techaspectclassifieds@gmail.com");
            mailmessage.To.Add(registerUser.Email);
            SmtpClient smtp = new SmtpClient();
            smtp.Send(mailmessage);
            // throw new NotImplementedException();
        }

        public static bool AuthenticateUser(UserLoginViewModel user)
        {
            try
            {
                using (TAC_Team5Entities entities = new TAC_Team5Entities())
                {
                    return (from u in entities.TAC_User
                            where u.Email.Equals(user.EmailAddress) && u.UPassword.Equals(user.Password) && u.IsActive == true && u.IsVerified == true && u.IsLocked == false
                            select u).Count() > 0 ? true : false;
                }
            }
            catch (Exception e)
            {
                
                return false;
            }
        }

       
    }
}