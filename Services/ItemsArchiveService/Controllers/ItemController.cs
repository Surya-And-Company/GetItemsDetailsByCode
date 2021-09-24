using ItemsArchiveService.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ItemsArchiveService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v1/api/[controller]/[action]")]
    public class ItemController : ControllerBase
    {
        
    }
}