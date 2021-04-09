using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpier.Web.UI.Interfaces
{
    public interface IServiceBase<T>
    {
        Task<List<T>> GetAllAsync();
    }
}
