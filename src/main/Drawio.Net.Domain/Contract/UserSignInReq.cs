using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawio.Net.Domain.Contract
{
    public class UserSignInReq
    {
        public int UserId
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        public int Level
        {
            get;
            set;
        }

        public string Role
        {
            get;
            set;
        }

        public string redirectUri
        {
            get;
            set;
        }
    }
}
