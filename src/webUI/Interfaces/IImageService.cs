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

        Task RemoveAsync(string name, bool force = false);

        Task<bool> Pull(string name);

        Task<bool> Push(ImagePushParameters parameters);

        Task TagAsync(string name, string repoName, string tag);
    }
}
