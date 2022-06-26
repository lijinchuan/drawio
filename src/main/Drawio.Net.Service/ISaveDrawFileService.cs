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
        OpResult<long> InsertFile(string title, string content, string userId);

        OpResult<long> SaveFile(string opId, long fileId, string title, string content);

        OpResult<bool> RenameFile(string opId, long fileId, string newTitle);

        OpResult<List<DrawFileModel>> ListFiles(string userId);

        OpResult<DrawFileModel> GetFileInfo(string opId, long fileId);

        OpResult<bool> DeleteFile(string opId, long fileId);

        OpResult<bool> DeleteFile(string opId, string userId, string title);

        OpResult<DrawFileModel> FindByTitle(string opId, string userId, string title);
    }
}
