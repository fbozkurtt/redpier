using Docker.DotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpier.Web.UI.Interfaces
{
    public interface IImageService : IServiceBase<ImageInspectResponse>
    {
        Task<ImageInspectResponse> InspectImage(string name);

        Task<IList<ImageHistoryResponse>> GetImageHistory(string name);
    }
}
