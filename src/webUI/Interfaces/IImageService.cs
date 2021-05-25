using Docker.DotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpier.Web.UI.Interfaces
{
    public interface IImageService : IServiceBase<ImagesListResponse>
    {
        Task<ImageInspectResponse> Inspect(string name);

        Task<IList<ImageHistoryResponse>> GetHistory(string name);

        Task<bool> Remove(string name, bool force = false);

        Task<bool> Pull(string imageName);
    }
}
