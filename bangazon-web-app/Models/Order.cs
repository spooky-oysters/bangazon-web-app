using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bangazon.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public int? PaymentTypeId { get; set; }

        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CompletedDate { get; set; }

        public virtual ICollection<LineItem> LineItem { get; set; }
    }
}
