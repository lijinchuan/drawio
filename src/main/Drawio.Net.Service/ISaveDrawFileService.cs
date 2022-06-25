using Drawio.Net.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawio.Net.Service
{
    public interface ISaveDrawFileService
    {
        OpResult<long> InsertFile(string title, string content, int userId);

        OpResult<long> SaveFile(int opId, long fileId, string title, string content);

        OpResult<bool> RenameFile(int opId, long fileId, string newTitle);

        OpResult<List<DrawFileModel>> ListFiles(int userId);

        OpResult<DrawFileModel> GetFileInfo(int opId, long fileId);

        OpResult<bool> DeleteFile(int opId, long fileId);

        OpResult<DrawFileModel> FindByTitle(int opId,int userId, string title);
    }
}
