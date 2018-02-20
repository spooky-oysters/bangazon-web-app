using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bangazon.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public int? PaymentTypeId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CompletedDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<LineItem> LineItem { get; set; }
    }
}
