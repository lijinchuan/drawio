using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawio.Net.Domain.Model
{
    public class DrawFileInfoModel
    {
        public long Id
        {
            get;
            set;
        }

        public string UserId
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public long FileSize
        {
            get;
            set;
        }

        public DateTime CrateTime
        {
            get;
            set;
        }

        public DateTime UpdateTime
        {
            get;
            set;
        }
    }
}
