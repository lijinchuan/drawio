using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Drawio.Net.API
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AuthControllerBaseController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected (string userId,string userName) GetUserInfo()
        {
            if (HttpContext.User.Identity.IsAuthenticated)  //判断用户是否通过认证
            {
                return (HttpContext.User.Claims.First(p=>p.Type==ClaimTypes.NameIdentifier).Value
                    , HttpContext.User.Identity.Name);
            }

            return default;
        }
    }
}
