using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawio.Net.Domain.Model
{

    public class OpPageResult<T>:OpResult<T>
    {
        public int PageIndex
        {
            get;
            set;
        }

        public int PageSize
        {
            get;
            set;
        }

        public int TotalCount
        {
            get;
            set;
        }
    }

    public class OpResult<T>
    {
        public bool Success
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
