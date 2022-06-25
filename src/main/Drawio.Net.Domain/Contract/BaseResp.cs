using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawio.Net.Domain.Contract
{
    public class BaseResp<T>
    {
        public int Code
        {
            get;
            set;
        }

        public T Data
        {
            get;
            set;
        }

        public string Msg
        {
            get;
            set;
        }
    }
}
