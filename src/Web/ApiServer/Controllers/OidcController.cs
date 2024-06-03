using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Mvc;

namespace ApiServer.Controllers;

public class OidcController(IClientRequestParametersProvider clientRequestParametersProvider)
    : Controller
{
    private IClientRequestParametersProvider ClientRequestParametersProvider { get; } = clientRequestParametersProvider;

    [HttpGet("_configuration/{clientId}")]
    public IActionResult GetClientRequestParameters([FromRoute] string clientId)
    {
        var parameters = ClientRequestParametersProvider.GetClientParameters(HttpContext, clientId);
        return Ok(parameters);
    }
}