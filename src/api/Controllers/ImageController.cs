using Docker.DotNet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Redpier.Application.Commands.Docker.Images;
using Redpier.Application.Queries.Docker.Images;
using Redpier.Shared.Constants;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Redpier.Web.API.Controllers
{
    public class ImageController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IList<ImagesListResponse>> GetImages()
            => await Mediator.Send(new ListImagesQuery());

        [HttpPost]
        public async Task<IList<ImagesListResponse>> GetImages(ListImagesQuery query)
            => await Mediator.Send(query);

        [HttpGet("[action]")]
        public async Task<ImageInspectResponse> Inspect([FromQuery] InspectImageQuery query)
            => await Mediator.Send(query);

        [HttpGet("[action]")]
        public async Task<IList<ImageSearchResponse>> Search(SearchImagesQuery query)
            => await Mediator.Send(query);

        [HttpGet("[action]")]
        public async Task<IList<ImageHistoryResponse>> History([FromQuery] GetImageHistoryQuery query)
            => await Mediator.Send(query);

        [Authorize(Roles = DefaultRoleNames.Admin)]
        [HttpPost("[action]")]
        public async Task<ActionResult> Build(BuildImageCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [Authorize(Roles = DefaultRoleNames.Admin)]
        [HttpPost("[action]")]
        public async Task<ActionResult> Create(CreateImageCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [Authorize(Roles = DefaultRoleNames.Admin)]
        [HttpPut("[action]")]
        public async Task<ActionResult> Push(PushImageCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [Authorize(Roles = DefaultRoleNames.Admin)]
        [HttpPut("[action]")]
        public async Task<ActionResult> Tag(TagImageCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [Authorize(Roles = DefaultRoleNames.Admin)]
        [HttpDelete]
        public async Task<IList<IDictionary<string, string>>> Remove([FromQuery] RemoveImageCommand command)
            => await Mediator.Send(command);
    }
}
