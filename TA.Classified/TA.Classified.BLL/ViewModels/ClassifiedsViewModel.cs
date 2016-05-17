using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TA.Classified.BLL.ViewModels
{
    public class ClassifiedsViewModel
    {
        public IEnumerable<TA.Classified.BLL.ViewModels.ClassifiedViewModel> Classifieds { get; set; }

        public int PageCount { get; set; }

        public int CurrentPage { get; set; }
    }
}
