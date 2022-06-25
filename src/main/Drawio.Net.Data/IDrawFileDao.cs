using Drawio.Net.Domain.Entity;
using System.Collections.Generic;

namespace Drawio.Net.Data
{
    public interface IDrawFileDao
    {
        long InsertFile(string title, string content, int userId);

        bool SaveFile(long fileId, string title, string content);

        bool RenameFile(long fileId, string newTitle);

        List<DrawFileEntity> ListFiles(int userId);

        DrawFileEntity GetFileInfo(long fileId);

        bool DeleteFile(long fileId);
    }
}
