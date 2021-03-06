﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TA.Classified.Core
{
    public class User
    {
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Please Enter EmailAddress")]
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessage = "Email is not valid")]
        [Range(6, 50, ErrorMessage = "Email address should be 0-50 charecters.")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        [Range(6, 20, ErrorMessage = "Password should be 6-20 charecters only")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public Boolean isVerified { get; set; }
        public Boolean isActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
