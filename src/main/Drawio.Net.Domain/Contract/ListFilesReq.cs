using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawio.Net.Domain.Contract
{
    public class ListFilesReq : BaseReq
    {
        /// <summary>标题模糊搜索关键字（可选）</summary>
        public string Search { get; set; }

        /// <summary>页码，从 1 开始（默认 1）</summary>
        public int Page { get; set; } = 1;

        /// <summary>每页条数（默认 20）</summary>
        public int PageSize { get; set; } = 20;
    }
}
