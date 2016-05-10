using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TA.Classified.BLL.ViewModels
{
    public class ClassifiedViewDetail
    {
        public string ClassifiedTitle { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }

        public string ClassifiedImage { get; set; }

        public decimal ClassifiedPrice { get; set; }

        public System.DateTime PostedDate { get; set; }

        public string ContactCity { get; set; }
    }
}
