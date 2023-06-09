﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bulkybook.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]

        public string ISBN { get; set; }
        [Required]

        public string Author { get; set; }
        [Required]
        [Range(1, 10000)]
        public double ListPrice { get; set; }
        [Required]
        [Range(1, 10000)]
        public double Price { get; set; }
        [Required]
        [Range(1, 10000)]
        public double Price50 { get; set; }
        [Required]
        [Range(1, 10000)]
        public double Price100 { get; set; }
        [ValidateNever]
        public string ImgUrl { get; set; }
        [Required]
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; } //creates a fk relation
        [ValidateNever]
        public Category Category { get; set; }
        [Required]

        [ForeignKey("CoverTypeId")]
        public int CoverTypeId { get; set; } //creates a fk relation
        [ValidateNever]
        public CoverType CoverType { get; set; }

    }
}
