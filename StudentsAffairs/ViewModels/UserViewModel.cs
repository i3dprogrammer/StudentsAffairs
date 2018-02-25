using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentsAffairs.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [Display(Name = "البريد")]
        public string Email { get; set; }
    }
}