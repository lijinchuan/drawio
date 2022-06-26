using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawio.Net.Domain.Contract
{
    public class DeleteFileReq
    {
        public long FileId
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }
    }
}
