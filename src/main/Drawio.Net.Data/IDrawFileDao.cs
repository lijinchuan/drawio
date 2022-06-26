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

        List<DrawFileEntity> ListFiles(string userId);

        DrawFileEntity GetFileInfo(long fileId);

        bool DeleteFile(long fileId);
    }
}
