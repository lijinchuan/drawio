using Drawio.Net.Domain.Model;
using Ljc.Com.Blog.Model.Contract;
using LJC.NetCoreFrameWork.SOA;

namespace Drawio.Net.Service.Impl
{
    public class AccountService : IAccountService
    {
        public OpResult<bool> Login(string userName, string password)
        {
            var resp = ESBClient.DoSOARequest2<UserLoginV1Response>(Ljc.Com.Blog.Model.Consts.SNo, Ljc.Com.Blog.Model.Consts.Func_UserLoginV1,
                new UserLoginV1Request
                {
                    UserPassword = password,
                    UserName = userName
                });

            return new OpResult<bool>
            {
                Data=resp.Code==1,
                Msg=resp.Msg,
                Success=resp.Code==1
            };
        }

        public OpResult<bool> Register(string userName, string password, string email)
        {
            var resp = ESBClient.DoSOARequest2<UserRegV1Response>(Ljc.Com.Blog.Model.Consts.SNo, Ljc.Com.Blog.Model.Consts.Func_UserRegV1,
                    new UserRegV1Request
                    {
                        Email = email,
                        UserPassword = password,
                        UserName = userName
                    });

            var boo = resp.Code == 1;

            return new OpResult<bool>
            {
                Data=boo,
                Msg=resp.Msg,
                Success=boo
            };
        }
    }
}
