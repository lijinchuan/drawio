using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Drawio.Net.Domain.Contract;
using Drawio.Net.Domain.Model;
using Drawio.Net.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        private async Task<IActionResult> UserSignInAsync(UserSignInReq req)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(req.UserId))
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

                if (!string.IsNullOrWhiteSpace(req.redirectUri))
                {
                    return Redirect(req.redirectUri);
                }

                return Ok(new
                {
                    loggedin = 1,
                    code = 200,
                    message = "成功"
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    loggedin = -1,
                    code = 500,
                    message = ex.Message
                });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register([FromServices] IAccountService accountService,
            [FromServices] IVCodeService vCodeService,
            string um_captcha, string username, string password, string email)
        {
            if (!HttpContext.Request.Cookies.TryGetValue(vCodeService.GetCookieName(), out string key)
                || !vCodeService.Valid(key, um_captcha))
            {
                return Ok(new
                {
                    code = 403,
                    success = false,
                    message = "验证码错误"
                });
            }

            var ret=accountService.Register(username, password, email);

            return Ok(new
            {
                code = 200,
                success=ret.Data,
                message = ret.Msg
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountService"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Login([FromServices] IAccountService accountService, string username, string password)
        {
            var ret = accountService.Login(username, password);

            if (ret.Success)
            {
                return await UserSignInAsync(new UserSignInReq
                {
                    UserId = username,
                    Level = ret.Data.Level,
                    Role = "",
                    UserName = username
                });
            }

            return Ok(new
            {
                loggedin = -1,
                code = 200,
                message = ret.Msg
            });
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="uid"></param>
        ///// <param name="name"></param>
        ///// <returns></returns>
        //[AllowAnonymous]
        //[HttpGet]
        //public string GetToken(string uid,string name)
        //{
        //    return new Utils.EncryHelper().Encrypto(JsonConvert.SerializeObject(new UserInfoToken
        //    {
        //        UserId = uid,
        //        UserName = name,
        //        CreateTime=DateTime.Now,
        //        ToUrl= "http://118.24.243.32:8080/webapp/?dev=1"
        //    }));
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Jump([FromQuery]string token)
        {
            try
            {
                var json = new Utils.EncryHelper().Decrypto(token);
                var user = JsonConvert.DeserializeObject<UserInfoToken>(json);
                if (DateTime.Now.Subtract(user.CreateTime).TotalMinutes > 3)
                {
                    throw new Exception("token失效");
                }

                return await UserSignInAsync(new UserSignInReq
                {
                    Level = user.Level,
                    redirectUri = user.ToUrl,
                    Role = user.Role,
                    UserId = user.UserId??user.UserName,
                    UserName = user.UserName
                });
            }
            catch
            {
                return Ok(new
                {
                    code = 404,
                    message = "非法请求"
                });
            }

        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [HttpGet]
        [Authorize]
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
                        message = $"当前用户是{name}," + HttpContext.User.Identity.Name
                    }) ;
                }
                else
                {
                    return Ok(new
                    {
                        code = 400,
                        message = "未登录"
                    });
                }
            }
            return Ok(new
            {
                code = 400,
                message = "无权访问"
            });
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UserSignOutAsync()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(new
            {
                code = 200,
                message = "注销成功"
            });
        }

        /// <summary>
        /// 验证码
        /// </summary>
        /// <param name="vCodeService"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> VCode([FromServices]IVCodeService vCodeService)
        {
            HttpContext.Request.Cookies.TryGetValue(vCodeService.GetCookieName(), out string key);
            if (string.IsNullOrWhiteSpace(key))
            {
                key = Guid.NewGuid().ToString("N");
                HttpContext.Response.Cookies.Append(vCodeService.GetCookieName(), key);
            }
            return File(vCodeService.GenVode(key), "image/Gif");
        }

        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="accountService"></param>
        /// <param name="action"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="um_captcha"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Process([FromServices]IAccountService accountService,
            [FromServices]IVCodeService vCodeService, [FromForm][FromQuery] string action,
            [FromForm][FromQuery] string username, [FromForm][FromQuery] string password,
            [FromForm][FromQuery] string um_captcha, [FromForm][FromQuery] string email)
        {
            switch (action)
            {
                case "ajax_login":
                    {
                        return await Login(accountService, username, password);
                    }
                case "ajax_register":
                    {
                        
                        return Register(accountService, vCodeService,um_captcha, username, password, email);
                    }
            }

            return Ok();
        }
    }
}
