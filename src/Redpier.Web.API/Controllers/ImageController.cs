using Docker.DotNet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Redpier.Application.Commands.Images;
using Redpier.Application.Queries.Images;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Redpier.Web.API.Controllers
{
    public class ImageController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IList<ImagesListResponse>> GetImages()
        {
            return await Mediator.Send(new ListImagesQuery());
        }

        [HttpPost]
        public async Task<IList<ImagesListResponse>> GetImages(ListImagesQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("[action]")]
        public async Task<ImageInspectResponse> Inspect(InspectImageQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("[action]")]
        public async Task<IList<ImageSearchResponse>> Search(SearchImagesQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("[action]")]
        public async Task<IList<ImageHistoryResponse>> History(GetImageHistoryQuery query)
        {
            return await Mediator.Send(query);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("[action]")]
        public async Task<bool> Build(BuildImageCommand command)
        {
            return await Mediator.Send(command);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("[action]")]
        public async Task<bool> Create(CreateImageCommand command)
        {
            return await Mediator.Send(command);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("[action]")]
        public async Task<bool> Push(PushImageCommand command)
        {
            return await Mediator.Send(command);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("[action]")]
        public async Task<bool> Tag(TagImageCommand command)
        {
            return await Mediator.Send(command);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("[action]")]
        public async Task<List<Dictionary<string, string>>> Remove(RemoveImageCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
