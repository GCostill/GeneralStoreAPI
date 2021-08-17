using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GeneralStoreAPI.Models
{
    public class Transaction
    {
        //if we want the user to decide/change a property, then that's when we would not put logic in a property
        [Key]
        [Required]
        public int ID { get; set; }

        [ForeignKey(nameof(Customer))]
        [Required]
        public int CustomerID { get; set; }
        // "virtual" helps set up foreign key and loads object to help set up inquiries
        public virtual Customer Customer { get; set; }

        [ForeignKey(nameof(Product))]
        [Required]
        public string ProductSKU { get; set; }
        public virtual Product Product { get; set; }

        public int ItemCount { get; set; }

        public DateTime DateOfTransaction { get; set; }
    }
}