using E_Commerce.Core.Entities.Product;
using E_Commerce.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Specification
{
    public class ProductCountWithSpec : BaseSpecifications<Product>
    {
        public ProductCountWithSpec(ProductSpecificationParameters specs)
            : base(product =>
            (!specs.TypeId.HasValue || product.Id == specs.TypeId.Value) &&
            (!specs.TypeId.HasValue || product.Id == specs.TypeId.Value) &&
            (string.IsNullOrWhiteSpace(specs.Search) || product.Name.ToLower().Contains(specs.Search)))
        {
        }
    }
}
