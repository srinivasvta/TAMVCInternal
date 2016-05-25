using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using TA.Classified.DataAccess;

namespace TA.Classified.BLL.ViewModels
{
    [Serializable]

    public class UserRegisterViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "it allows minimum 4 charecters maaximum 20 charecters.")]
        public string First_Name { get; set; }

        [Required(ErrorMessage = "Enter Lastname")]
        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "it allows minimum 4 charecters maaximum 20 charecters.")]
        public string Last_Name { get; set; }

        [Required(ErrorMessage = "please Enter Email Address")]
        [DataType(DataType.EmailAddress)]

        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        //[Range(6, 20, ErrorMessage = "Password should be 6-20 charecters only")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]

        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please Enter Gender")]
        public bool Gender { get; set; }


        [Required(ErrorMessage = "Please Enter DOB")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> DOB { get; set; }

        [Required(ErrorMessage = "Please enter address")]
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
        //[DataType(DataType.Text)]

        public string Country { get; set; }


        //[Required(ErrorMessage = "Please enter address")]
        //[Range(10,10,ErrorMessage ="mobile number should be 10digits")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        //[MustBeTrue(ErrorMessage = "You gotta tick the box!")]
        [Required(ErrorMessage = "click terms and conditions")]
        public bool TermsAndConditions { get; set; }

    }
    //define the validation for check box
    public class MustBeTrueAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value is bool && (bool)value;
        }
    }




}
