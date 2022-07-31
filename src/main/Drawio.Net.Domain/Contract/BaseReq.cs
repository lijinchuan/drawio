using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawio.Net.Domain.Contract
{
    public class BaseReq
    {
        public string UserId
        {
            get;
            set;
        }

        public string OpId
        {
            get;
            set;
        }
    }
}
