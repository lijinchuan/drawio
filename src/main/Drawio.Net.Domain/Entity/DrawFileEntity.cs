using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawio.Net.Domain.Entity
{
    public class DrawFileEntity
    {
        public const string INDEXUSERID = nameof(UserId);
        public const string INDEXUSERIDTITLE = nameof(UserId) + nameof(Title);
        public const string INDEXUSERIDCREATIME = nameof(UserId) + nameof(CrateTime);

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

        public string Content
        {
            get;
            set;
        }

        public int FileSize
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
