using E_Commerce.Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Data
{
    public static class DataContextSeed
    {
        public static async Task SeedDataAsync(DataContext context)
        {
            #region ProductBrand
            if (!context.Set<ProductBrand>().Any()) {
                //1.Read Data From Files
                var BrandsData = await File.ReadAllTextAsync(@"..\E-Commerce.Repository\Data\DataSeeding\brands.json");
                //2.Convert Data to C# Objects
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
                //3.Insert Data to Database
                if (Brands is not null && Brands.Any())
                {
                    await context.Set<ProductBrand>().AddRangeAsync(Brands);
                    await context.SaveChangesAsync();
                }
            }
            #endregion
            #region ProductType
            if (!context.Set<ProductType>().Any())
            {
                //1.Read Data From Files
                var TypeData = await File.ReadAllTextAsync(@"..\E-Commerce.Repository\Data\DataSeeding\types.json");
                //2.Convert Data to C# Objects
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypeData);
                //3.Insert Data to Database
                if (Types is not null && Types.Any())
                {
                    await context.Set<ProductType>().AddRangeAsync(Types);
                    await context.SaveChangesAsync();
                }
            }
            #endregion
            #region Product
            if (!context.Set<Product>().Any())
            {
                //1.Read Data From Files
                var ProductData = await File.ReadAllTextAsync(@"..\E-Commerce.Repository\Data\DataSeeding\products.json");
                //2.Convert Data to C# Objects
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);
                //3.Insert Data to Database
                if (Products is not null && Products.Any())
                {
                    await context.Set<Product>().AddRangeAsync(Products);
                    await context.SaveChangesAsync();
                }
            }
            #endregion 
        }
    }
}
