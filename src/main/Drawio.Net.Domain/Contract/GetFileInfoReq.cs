using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawio.Net.Domain.Contract
{
    public class GetFileInfoReq : BaseReq
    {
        public int FileId
        {
            get;
            set;
        }
    }
}
