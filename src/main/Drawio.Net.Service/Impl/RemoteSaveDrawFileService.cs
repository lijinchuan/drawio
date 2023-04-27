using Drawio.Net.Domain.Contract;
using Drawio.Net.Domain.Model;
using LJC.NetCoreFrameWork.SOA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawio.Net.Service.Impl
{
    public class RemoteSaveDrawFileService : ISaveDrawFileService
    {
        public OpResult<bool> DeleteFile(string opId, long fileId)
        {
            return ESBClient.DoSOARequest2<OpResult<bool>>(Domain.Contract.Consts.SNo,
                Domain.Contract.Consts.FunId_DeleteFile, new DeleteFileReq
                {
                    OpId=opId,
                    FileId=fileId
                });
        }

        public OpResult<bool> DeleteFile(string opId, string userId, string title)
        {
            return ESBClient.DoSOARequest2<OpResult<bool>>(Domain.Contract.Consts.SNo,
                Domain.Contract.Consts.FunId_DeleteFileByTitle, new DeleteFileReq
                {
                    OpId = opId,
                    Title = title,
                    UserId = userId
                });
        }

        public OpResult<DrawFileModel> FindByTitle(string opId, string userId, string title)
        {
            return ESBClient.DoSOARequest2<OpResult<DrawFileModel>>(Domain.Contract.Consts.SNo,
                Domain.Contract.Consts.FunId_FindByTitle, new FindByTitleReq
                {
                    OpId = opId,
                    Title = title,
                    UserId = userId
                });
        }

        public OpResult<DrawFileModel> GetFileInfo(string opId, long fileId)
        {
            return ESBClient.DoSOARequest2<OpResult<DrawFileModel>>(Domain.Contract.Consts.SNo,
                Domain.Contract.Consts.FunId_GetFileInfo, new GetFileInfoReq
                {
                    OpId = opId,
                    FileId=(int)fileId
                });
        }

        public OpResult<long> InsertFile(string title, string content, string userId)
        {
            return ESBClient.DoSOARequest2<OpResult<long>>(Domain.Contract.Consts.SNo,
                Domain.Contract.Consts.FunId_InsertFile, new InsertFileReq
                {
                    Content=content,
                    Title=title,
                    UserId=userId
                });
        }

        public OpResult<List<DrawFileInfoModel>> ListFiles(string userId)
        {
            return ESBClient.DoSOARequest2<OpResult<List<DrawFileInfoModel>>>(Domain.Contract.Consts.SNo,
                Domain.Contract.Consts.FunId_ListFiles, new ListFilesReq
                {
                    UserId = userId
                });
        }

        public OpResult<bool> RenameFile(string opId, long fileId, string newTitle)
        {
            return ESBClient.DoSOARequest2<OpResult<bool>>(Domain.Contract.Consts.SNo,
                Domain.Contract.Consts.FunId_RenameFile, new RenameFileReq
                {
                    FileId=fileId,
                    OpId=opId,
                    NewTitle=newTitle
                });
        }

        public OpResult<long> SaveFile(string opId, long fileId, string title, string content)
        {
            return ESBClient.DoSOARequest2<OpResult<long>>(Domain.Contract.Consts.SNo,
                Domain.Contract.Consts.FunId_SaveFile, new SaveFileReq
                {
                    Content=content,
                    OpId=opId,
                    FileId=fileId,
                    Title=title
                });
        }
    }
}
