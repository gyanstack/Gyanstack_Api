using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [AllowAnonymous]
    [Produces("application/json")]
    [Route("api/BaseApi")]
    public class BaseApiController : BaseController
    {
    }
}