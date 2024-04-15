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
    public class ProductSpecification : BaseSpecifications<Product>
    {
        // Get Product with Filtration
        //1.brandId&TypeId
        public ProductSpecification(ProductSpecificationParameters specs) 
            : base(product=>
            (!specs.TypeId.HasValue || product.Id==specs.TypeId.Value)&&
            (!specs.TypeId.HasValue || product.Id == specs.TypeId.Value)&&
            (string.IsNullOrWhiteSpace(specs.Search)||product.Name.ToLower().Contains(specs.Search)))
        {
            IncludeExpressions.Add(product=>product.ProductBrand);
            IncludeExpressions.Add(product=>product.ProductType);

            ApplyPagination(specs.PageSize,specs.PageIndex);

            if (specs.Sort is not null)
            {
                switch(specs.Sort)
                {
                    case ProductSortingParameters.NameAsc:
                        OrderBy = x => x.Name;
                        break;
                    case ProductSortingParameters.NameDesc:
                        OrderByDesc = x => x.Name;
                        break;
                    case ProductSortingParameters.PriceAsc:
                        OrderBy = x => x.Price;
                        break;
                    case ProductSortingParameters.PriceDesc:
                        OrderByDesc = x => x.Price;
                        break;
                    default:
                        OrderBy = x => x.Name;
                        break;
                }
            }
            else
            {
                OrderBy = x => x.Name;
            }
        }
        // Get Product By id
        public ProductSpecification(int id) : base(product=>product.Id==id)
        {
            IncludeExpressions.Add(product => product.ProductBrand);
            IncludeExpressions.Add(product => product.ProductType);
        }
    }
}
