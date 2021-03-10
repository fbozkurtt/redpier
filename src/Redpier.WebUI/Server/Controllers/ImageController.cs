using Docker.DotNet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Redpier.Application.Commands.Images;
using Redpier.Application.Queries.Images;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Redpier.WebUI.Server.Controllers
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
        public async Task<ActionResult> Build(BuildImageCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("[action]")]
        public async Task<ActionResult> Create(CreateImageCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("[action]")]
        public async Task<ActionResult> Push(PushImageCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("[action]")]
        public async Task<ActionResult> Tag(TagImageCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("[action]")]
        public async Task<IList<IDictionary<string, string>>> Remove(RemoveImageCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
