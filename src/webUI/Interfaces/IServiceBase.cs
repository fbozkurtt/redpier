using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpier.Web.UI.Interfaces
{
    public interface IServiceBase<T>
    {
        Task<bool> CreateAsync(T item);
        Task<IList<T>> GetAsync();
    }
}
