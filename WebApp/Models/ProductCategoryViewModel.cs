﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class ProductCategoryViewModel
    {
        public int ID { set; get; }
        [Required]
        public string Name { set; get; }
        [Required]
        public string Alias { set; get; }

        public string Description { set; get; }
        public int? ParentID { set; get; }
        public int? DisplayOrder { set; get; }

        public string Image { set; get; }

        public bool? HomeFlag { set; get; }
        public DateTime? CreatedDate { set; get; }


        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }


        public string UpdatedBy { set; get; }


        public string MetaKeyword { set; get; }


        public string MetaDescription { set; get; }
        [Required]
        public bool Status { set; get; }

        public virtual IEnumerable<ProductViewModel> Products { set; get; }
    }
}