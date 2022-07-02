using Drawio.Net.Domain.Model;
using Ljc.Com.Blog.Model.Contract;
using LJC.NetCoreFrameWork.SOA;

namespace Drawio.Net.Service.Impl
{
    public class AccountService : IAccountService
    {
        public OpResult<UserInfoToken> Login(string userName, string password)
        {
            var resp = ESBClient.DoSOARequest2<UserLoginV2Response>(Ljc.Com.Blog.Model.Consts.SNo, Ljc.Com.Blog.Model.Consts.Func_UserLoginV2,
                new UserLoginV1Request
                {
                    UserPassword = password,
                    UserName = userName
                });

            if (resp.Code != 1)
            {
                return new OpResult<UserInfoToken>
                {
                    Msg=resp.Msg,
                    Success=false
                };
            }

            return new OpResult<UserInfoToken>
            {
                Data=resp.User==null?null:new UserInfoToken
                {
                   CreateTime=resp.User.CTime,
                   Level=resp.User.UserLevel,
                   UserId=resp.User.UserId,
                   UserName=resp.User.UserName
                },
                Msg=resp.Msg,
                Success=resp.Code==1
            };
        }

        public UserInfoToken GetUserInfo(string userName)
        {
            var resp = ESBClient.DoSOARequest2<GetUserListResponse>(Ljc.Com.Blog.Model.Consts.SNo, Ljc.Com.Blog.Model.Consts.Func_GetUserList,
                new GetUserListRequest
                {
                    IsValid = true,
                    Pi = 1,
                    Ps = 10,
                    SearchWord = userName
                });
            
            if (resp.Items.Count > 0)
            {
                return new UserInfoToken
                {
                    CreateTime=resp.Items[0].CTime,
                    Level=resp.Items[0].UserLevel,
                    UserId= resp.Items[0].UserId,
                    UserName=resp.Items[0].UserName
                };
            }
            return null;
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
