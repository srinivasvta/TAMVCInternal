using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TA.Classified.BLL.ViewModels
{
    public class PostAdViewModel
    {

        //public PostAdViewModel()
        //{
        //}
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClassifiedId { get; set; }

        [Required(ErrorMessage = "Please Enter Title")]
        [DataType(DataType.Text)]
        public string ClassifiedTitle { get; set; }

        [Required(ErrorMessage = "Please Enter Summary")]
        [DataType(DataType.MultilineText)]
        public string Summary { get; set; }

        [Required(ErrorMessage = "Please Enter Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please choose a Image")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ClassifiedImage { get; set; }

        [Required(ErrorMessage = "Please Enter price")]
        [DataType(DataType.Currency)]
        public decimal ClassifiedPrice { get; set; }

        [Required(ErrorMessage = "Please enter user email id")]
        public string Createdby { get; set; }


        public string CategoryName { get; set; }

        public System.DateTime PostedDate { get; set; }

        [Required(ErrorMessage = "Please enter contact name")]
        [DataType(DataType.Text)]
        public string ContactName { get; set; }

        [Required(ErrorMessage = "Please enter contact phone")]
        [DataType(DataType.PhoneNumber)]
        public string ContactPhone { get; set; }

        [Required(ErrorMessage = "Please enter contact city")]
        [DataType(DataType.Text)]
        public string ContactCity { get; set; }


    }
}
