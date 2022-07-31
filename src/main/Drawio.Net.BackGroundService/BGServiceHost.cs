using Drawio.Net.Domain.Contract;
using Drawio.Net.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawio.Net.BackGroundService
{
    public class BGServiceHost : LJC.FrameWork.SOA.ESBService
    {
        public BGServiceHost():base(Consts.SNo, serviceName:"drawioService")
        {

        }

        public override object DoResponse(int funcId, byte[] Param, string clientid)
        {
            switch (funcId)
            {
                case Consts.FunId_InsertFile:
                    {
                        var req = LJC.FrameWork.EntityBuf.EntityBufCore.DeSerialize<InsertFileReq>(Param);
                        return AutofacBuilder.GetFromFac<ISaveDrawFileService>().InsertFile(req.Title, req.Content, req.UserId);
                    }
                case Consts.FunId_SaveFile:
                    {
                        var req = LJC.FrameWork.EntityBuf.EntityBufCore.DeSerialize<SaveFileReq>(Param);
                        return AutofacBuilder.GetFromFac<ISaveDrawFileService>().SaveFile(req.OpId, req.FileId, req.Title, req.Content);
                    }
                case Consts.FunId_DeleteFile:
                    {
                        var req = LJC.FrameWork.EntityBuf.EntityBufCore.DeSerialize<DeleteFileReq>(Param);
                        return AutofacBuilder.GetFromFac<ISaveDrawFileService>().DeleteFile(req.OpId, req.FileId);
                    }
                case Consts.FunId_DeleteFileByTitle:
                    {
                        var req = LJC.FrameWork.EntityBuf.EntityBufCore.DeSerialize<DeleteFileReq>(Param);
                        return AutofacBuilder.GetFromFac<ISaveDrawFileService>().DeleteFile(req.OpId, req.UserId, req.Title);
                    }
                case Consts.FunId_FindByTitle:
                    {
                        var req = LJC.FrameWork.EntityBuf.EntityBufCore.DeSerialize<FindByTitleReq>(Param);
                        return AutofacBuilder.GetFromFac<ISaveDrawFileService>().FindByTitle(req.OpId, req.UserId, req.Title);
                    }
                case Consts.FunId_GetFileInfo:
                    {
                        var req = LJC.FrameWork.EntityBuf.EntityBufCore.DeSerialize<GetFileInfoReq>(Param);
                        return AutofacBuilder.GetFromFac<ISaveDrawFileService>().GetFileInfo(req.OpId, req.FileId);
                    }
                case Consts.FunId_ListFiles:
                    {
                        var req = LJC.FrameWork.EntityBuf.EntityBufCore.DeSerialize<ListFilesReq>(Param);
                        return AutofacBuilder.GetFromFac<ISaveDrawFileService>().ListFiles(req.UserId);
                    }
                case Consts.FunId_RenameFile:
                    {
                        var req = LJC.FrameWork.EntityBuf.EntityBufCore.DeSerialize<RenameFileReq>(Param);
                        return AutofacBuilder.GetFromFac<ISaveDrawFileService>().RenameFile(req.OpId, req.FileId, req.NewTitle);
                    }
            }
            return base.DoResponse(funcId, Param, clientid);
        }
    }
}
