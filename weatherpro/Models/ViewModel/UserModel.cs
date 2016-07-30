using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace weatherpro.Models.ViewModel
{
    public class UserModel
    {
        [Key]
        public int register { get; set; }
        public int id{ get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Email")]
        public string email { get; set; }
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string pwd { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Phone")]
        public int phone { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "address1")]
        public string address1 { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "address2")]
        public string address2 { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "City")]
        public string city { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "state")]
        public string state { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Country")]
        public string country { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "pin")]
        public int pin { get; set; }
    }
}