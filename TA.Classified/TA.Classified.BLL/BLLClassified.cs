using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TA.Classified.DataAccess;
using TA.Classified.BLL.ViewModels;

namespace TA.Classified.BLL
{
    public class BLLClassified
    {
        public static IEnumerable<ClassifiedViewModel> GetAllClassifieds(int pageSize, int pageNumber)
        {
            try
            {
                using (TAC_Team5Entities entities = new TAC_Team5Entities())
                {
                    return (from clsfd in entities.TAC_Classified
                           select new ClassifiedViewModel() { ClassifiedImage = clsfd.ClassifiedImage , ClassifiedTitle = clsfd.ClassifiedTitle , Description = clsfd.Description , Summary = clsfd.Summary , ClassifiedPrice = clsfd.ClassifiedPrice , PostedDate = clsfd.PostedDate }).ToList().GetRange(pageSize * (pageNumber - 1), (pageSize * pageNumber) - 1);
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static bool AddClassified(ClassifiedViewModel addClassified)
        {
            TAC_Classified newClassfied = new TAC_Classified();
            newClassfied.ClassifiedImage = addClassified.ClassifiedImage;
            newClassfied.ClassifiedPrice = addClassified.ClassifiedPrice;
            newClassfied.ClassifiedTitle = addClassified.ClassifiedTitle;
            newClassfied.Description = addClassified.Description;
            newClassfied.Summary = addClassified.Summary;
            newClassfied.PostedDate = DateTime.Now;

            TAC_ClassifiedContact contact = new TAC_ClassifiedContact();
            contact.ContactName = addClassified.ContactName;
            contact.ContactPhone = addClassified.ContactPhone;
            contact.ContactCity = addClassified.ContactCity;

            try
            {
                using (TAC_Team5Entities entities = new TAC_Team5Entities())
                {
                    newClassfied = entities.TAC_Classified.Add(newClassfied);

                    contact.ClassifiedId = newClassfied.ClassifiedId;
                    entities.TAC_ClassifiedContact.Add(contact);

                    entities.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public static IEnumerable<ClassifiedViewModel> GetClassifiedsbyCategory(string categoryName, int pageSize, int pageNumber)
        {
            try
            {
                using (TAC_Team5Entities entities = new TAC_Team5Entities())
                {
                    int categoryID = (from cat in entities.TAC_Category
                                      where cat.CategoryName.Equals(categoryName)
                                      select cat.CategoryId).First();

                    return (from c in entities.TAC_Classified
                            where c.CategoryId.Equals(categoryID)
                            select new ClassifiedViewModel() { ClassifiedImage = c.ClassifiedImage, ClassifiedTitle = c.ClassifiedTitle, Description = c.Description, Summary = c.Summary, ClassifiedPrice = c.ClassifiedPrice, PostedDate = c.PostedDate }).ToList().GetRange(pageSize * (pageNumber - 1), (pageSize * pageNumber) - 1);
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static IEnumerable<ClassifiedViewModel> GetClassifiedsbyUser(string emailAddress, int pageSize, int pageNumber)
        {
            try
            {
                using (TAC_Team5Entities entities = new TAC_Team5Entities())
                {
                    Guid userID = (from u in entities.TAC_User
                                   where u.Email.Equals(emailAddress)
                                   select u.UserId).First();

                    return (from c in entities.TAC_Classified
                            where c.CreatedBy.Equals(userID)
                            select new ClassifiedViewModel() { ClassifiedImage = c.ClassifiedImage, ClassifiedTitle = c.ClassifiedTitle, Description = c.Description, Summary = c.Summary, ClassifiedPrice = c.ClassifiedPrice, PostedDate = c.PostedDate }).ToList().GetRange(pageSize * (pageNumber - 1), (pageSize * pageNumber) - 1);
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
