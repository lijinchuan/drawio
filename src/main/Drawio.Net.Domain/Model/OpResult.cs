using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawio.Net.Domain.Model
{
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
