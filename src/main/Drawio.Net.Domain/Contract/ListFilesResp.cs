using Drawio.Net.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawio.Net.Domain.Contract
{
    public class ListFilesResp : BaseResp<List<DrawFileInfoModel>>
    {
        /// <summary>符合条件的总记录数</summary>
        public int TotalCount { get; set; }

        /// <summary>当前页码</summary>
        public int Page { get; set; }

        /// <summary>每页条数</summary>
        public int PageSize { get; set; }
    }
}
