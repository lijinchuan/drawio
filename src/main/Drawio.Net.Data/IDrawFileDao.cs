using Drawio.Net.Domain.Entity;
using System.Collections.Generic;

namespace Drawio.Net.Data
{
    public interface IDrawFileDao
    {
        long InsertFile(string title, string content, string userId);

        long SaveFile(long fileId, string title, string content);

        DrawFileEntity FindByTitle(string userId, string title);

        bool RenameFile(long fileId, string newTitle);

        /// <summary>获取用户的文件列表（支持搜索、分页、按修改时间倒序）</summary>
        List<DrawFileEntity> ListFiles(string userId, string search, int page, int pageSize, out int totalCount);

        DrawFileEntity GetFileInfo(long fileId);

        bool DeleteFile(long fileId);
    }
}
