using Microsoft.AspNetCore.Mvc;
using GeoMap.Model.User;
using AutoMapper;
using Services.Abstractions;
using Services.Contracts.User;

namespace GeoMap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IUserService service, ILogger<UserController> logger, IMapper mapper) : ControllerBase
    {       

        [HttpPost]
        public void Create(CreatingUserModel userModel)
        {
            service.Create(mapper.Map<CreatingUserDto>(userModel));
        }
    }
}
