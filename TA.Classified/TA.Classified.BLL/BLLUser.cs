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
        public  bool RegisterUser(UserRegisterViewModel registerUser)
        {

            using (TAC_Team5Entities entities = new TAC_Team5Entities())
            { 
                TAC_User newUser = new TAC_User();
            var s = entities.TAC_User.Where(m => m.Email == registerUser.EmailAddress).Count();
                if (s == 0)
                {
                    newUser.UserId = Guid.NewGuid();
                    newUser.Email = registerUser.EmailAddress;
                    newUser.UPassword = registerUser.Password;
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
                    newUser.IsActive = true;
                    newUser.IsLocked = false;
                    newUser.IsVerified = true;

                    //using (TAC_Team5Entities entities = new TAC_Team5Entities())
                    //{
                    try
                    {
                        entities.TAC_User.Add(newUser);
                        entities.SaveChanges();
                        return true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        return false;
                    }
                }
                else
                {
                    //
                }

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