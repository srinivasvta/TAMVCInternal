using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TA.Classified.BLL.ViewModels
{
    public class UserRegisterViewModel
    {
        [Required(ErrorMessage = "Please Enter First Name")]
        [DataType(DataType.Text)]
        public string First_Name { get; set; }

        [Required(ErrorMessage = "Please Enter Last Name")]
        [DataType(DataType.Text)]
        public string Last_Name { get; set; }

        [Required(ErrorMessage = "Please Enter EmailAddress")]
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessage = "Email is not valid")]
        [Range(6, 50, ErrorMessage = "Email address should be 0-50 charecters.")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [Range(6, 20, ErrorMessage = "Password should be 6-20 charecters only")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please Enter Gender")]
        public Nullable<bool> Gender { get; set; }

        [Required(ErrorMessage = "Please Enter DOB")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> DOB { get; set; }

        [Required(ErrorMessage ="Please enter address")]
        [DataType(DataType.MultilineText)]
        public string Address1 { get; set; }

        [DataType(DataType.MultilineText)]
        public string Address2 { get; set; }

        [Required(ErrorMessage = "Please enter City")]
        [DataType(DataType.Text)]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter State")]
        [DataType(DataType.Text)]
        public string State { get; set; }

        [Required(ErrorMessage = "Please enter Country")]
        [DataType(DataType.Text)]
        public Nullable<int> Country { get; set; }


        [Required(ErrorMessage = "Please enter address")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }


    }
}
