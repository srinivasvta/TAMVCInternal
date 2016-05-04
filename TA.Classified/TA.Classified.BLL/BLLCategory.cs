using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TA.Classified.BLL.ViewModels;
using TA.Classified.DataAccess;

namespace TA.Classified.BLL
{
    class BLLCategory
    {
        public static IEnumerable<CategoryViewModel> GetCategories()
        {
            using (TAC_Team5Entities entities = new TAC_Team5Entities())
            {
                return from cat in entities.TAC_Category
                       select new CategoryViewModel() { CategoryName = cat.CategoryName, CategoryImage = cat.CategoryImage };
            }
        }
    }
}
