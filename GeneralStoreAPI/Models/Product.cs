﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GeneralStoreAPI.Models
{
    public class Product
    {
        [Key]
        [Required]
        public string SKU { get; set; }
        [Required]
        public string Name { get; set; }
        public double Cost { get; set; }
        public int NumberInInventory { get; set; }
        public bool IsInStock 
        { 
            get 
            {
                return (NumberInInventory > 0);
            } 
        }
    }
}