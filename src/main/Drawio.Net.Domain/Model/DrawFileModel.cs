using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawio.Net.Domain.Model
{
    public class DrawFileModel
    {
        public long Id
        {
            get;
            set;
        }

        public int UserId
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string Content
        {
            get;
            set;
        }

        public bool IsValid
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
