using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_Commerce.Core.Specification
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ProductSortingParameters
    {
        NameAsc,NameDesc,PriceAsc,PriceDesc
    }
}
