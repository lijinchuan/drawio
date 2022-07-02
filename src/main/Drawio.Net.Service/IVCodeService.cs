using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawio.Net.Service
{
    public interface IVCodeService
    {
        byte[] GenVode(string userName);

        bool Valid(string key,string code);

        string GetCookieName();
    }
}
