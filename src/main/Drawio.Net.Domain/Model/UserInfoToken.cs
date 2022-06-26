using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawio.Net.Domain.Model
{
    public class UserInfoToken
    {
        public string UserId
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

        public string ToUrl
        {
            get;
            set;
        }

        public DateTime CreateTime
        {
            get;
            set;
        }
    }
}
