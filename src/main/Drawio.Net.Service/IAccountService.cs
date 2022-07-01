using Drawio.Net.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawio.Net.Service
{
    public interface IAccountService
    {
        OpResult<bool> Register(string userName,string password,string email);

        OpResult<bool> Login(string userName, string password);

        UserInfoToken GetUserInfo(string userName);
    }
}
