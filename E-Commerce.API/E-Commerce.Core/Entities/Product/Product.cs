using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Entities.Product
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public int TypeId { get; set; }
        public int BrandId { get; set; }
        //----------------ProductBrand-------------------
        public ProductBrand ProductBrand { get; set; }
        //----------------ProductType--------------------
        public ProductType ProductType { get; set; }
        //-----------------------------------------------
    }
}
