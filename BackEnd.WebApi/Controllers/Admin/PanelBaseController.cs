using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BackEnd.WebApi.Controllers.Admin
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class PanelBaseController : ControllerBase
    {
    }
}
