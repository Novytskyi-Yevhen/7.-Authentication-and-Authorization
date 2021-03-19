using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskAuthenticationAuthorization.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email not specified!")]
        [EmailAddress(ErrorMessage = "Email is invalid!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password not specified!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
