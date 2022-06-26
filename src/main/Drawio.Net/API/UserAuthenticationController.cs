using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Drawio.Net.Domain.Contract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
    public class UserAuthenticationController : ControllerBase
    {
        /// <summary>
        /// 认证
        /// </summary>
        ///<param name="req"></param>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> UserSignInAsync(UserSignInReq req)
        {
            try
            {
                if (req.UserId <= 0)
                {
                    throw new ArgumentNullException("UserId");
                }

                if (string.IsNullOrWhiteSpace(req.UserName))
                {
                    throw new ArgumentNullException("UserName");
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, req.UserName),
                    new Claim(ClaimTypes.Role, req.Role),
                    new Claim(ClaimTypes.NameIdentifier,req.UserId.ToString())
                };
                //通过Claim来创建ClaimsIdentity 类似于通过用户的身份来创建身份证
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    //应该允许刷新身份验证会话。
                    AllowRefresh = true,
                    //身份验证票证过期的时间10分钟
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1),
                    //允许持久化
                    IsPersistent = true,
                    //cookie过期时间1天
                    IssuedUtc = DateTime.Now.AddYears(10),
                    //重定向url地址
                    RedirectUri = req.redirectUri
                };
                //授权cookie
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return Ok(new
                {
                    code = 200,
                    messgae = "成功"
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    code = 500,
                    messgae = ex.Message
                });
            }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [HttpGet]
        public IActionResult GetUser()
        {
            //
            if (HttpContext.User.Identity != null)
            {
                if (HttpContext.User.Identity.IsAuthenticated)  //判断用户是否通过认证
                {
                    string name = HttpContext.User.Claims.ToList().First(p => p.Type == ClaimTypes.Name).Value;
                    return Ok(new
                    {
                        code = 200,
                        messgae = $"当前用户是{name}," + HttpContext.User.Identity.Name
                    }) ;
                }
                else
                {
                    return Ok(new
                    {
                        code = 400,
                        messgae = "未登录"
                    });
                }
            }
            return Ok(new
            {
                code = 400,
                messgae = "无权访问"
            });
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UserSignOutAsync()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(new
            {
                code = 200,
                messgae = "注销成功"
            });
        }
    }
}
