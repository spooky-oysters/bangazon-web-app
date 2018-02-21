using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Bangazon.Models
{
    public class PaymentType
    {
        [Key]
        public int PaymentTypeId { get; set; }
        
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]       
        public DateTime? DateCreated { get; set; }


        [Required]
        [StringLength(12)]
        [Display(Name = "Type")]
        public string Description { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }

        [Required]
        public ApplicationUser User { get; set; }
    }
}