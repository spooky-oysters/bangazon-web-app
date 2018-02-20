using Bangazon.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models
{
    public class AccountSettingsViewModel
    {
        [Required]
        [StringLength(55, ErrorMessage = "The {0} must be no longer than {1} characters long.")]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(55, ErrorMessage = "The {0} must be no longer than {1} characters long.")]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(55, ErrorMessage = "The {0} must be no longer than {1} characters long.")]
        [DataType(DataType.Text)]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        //public ApplicationUser User { get; set; }
    }
}
