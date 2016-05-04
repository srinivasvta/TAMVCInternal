using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TA.Classified.BLL.ViewModels;
using TA.Classified.DataAccess;

namespace TA.Classified.BLL
{
    public class BLLUser
    {
        public static bool RegisterUser(UserRegisterViewModel registerUser)
        {
            TAC_User newUser = new TAC_User();

            newUser.UserId = Guid.NewGuid();
            newUser.First_Name = registerUser.First_Name;
            newUser.Last_Name = registerUser.Last_Name;
            newUser.Email = registerUser.EmailAddress;
            newUser.UPassword = registerUser.Password;
            newUser.Address1 = registerUser.Address1;
            newUser.Address2 = registerUser.Address2;
            newUser.City = registerUser.City;
            newUser.Country = registerUser.Country;
            newUser.State = registerUser.State;
            newUser.Phone = registerUser.Phone;
            newUser.CreatedDate = DateTime.Now;
            newUser.IsActive = true;
            newUser.IsLocked = false;
            newUser.IsVerified = true;
            try
            {
                using (TAC_Team5Entities entities = new TAC_Team5Entities())
                {
                    entities.TAC_User.Add(newUser);
                    entities.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
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