using Docker.DotNet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Redpier.Application.Commands.Images;
using Redpier.Application.Queries.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpier.WebUI.Server.Controllers
{
    public class ImageController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IList<ImagesListResponse>> Images (ListImagesQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("[action]")]
        public async Task<ImageInspectResponse> Inspect (InspectImageQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("[action]")]
        public async Task<IList<ImageSearchResponse>> Search (SearchImagesQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("[action]")]
        public async Task<IList<ImageHistoryResponse>> History (GetImageHistoryQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost("[action]")]
        public async Task<bool> Build(BuildImageCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("[action]")]
        public async Task<bool> Create(CreateImageCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("[action]")]
        public async Task<bool> Push(PushImageCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("[action]")]
        public async Task<bool> Tag(TagImageCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("[action]")]
        public async Task<List<Dictionary<string, string>>> Remove(RemoveImageCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
