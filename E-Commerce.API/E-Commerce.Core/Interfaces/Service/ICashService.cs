using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Interfaces.Service
{
    public interface ICashService
    {
        Task SetCashResponseAsync(string key, object response, TimeSpan time);
        Task<string?> GetCashResponseAsync(string key);
    }
}
