using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Redpier.WebUI.Server.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class OpenIdConfigurationController : Controller
    {
        private readonly ILogger<OpenIdConfigurationController> _logger;

        public OpenIdConfigurationController(ILogger<OpenIdConfigurationController> logger, IClientRequestParametersProvider clientRequestParametersProvider)
        {
            ClientRequestParametersProvider = clientRequestParametersProvider;
            _logger = logger;
        }

        public IClientRequestParametersProvider ClientRequestParametersProvider { get; }

        [HttpGet("_configuration/{clientId}")]
        public IActionResult GetClientRequestParameters([FromRoute] string clientId)
        {
            var parameters = ClientRequestParametersProvider.GetClientParameters(HttpContext, clientId);
            return Ok(parameters);
        }
    }
}
