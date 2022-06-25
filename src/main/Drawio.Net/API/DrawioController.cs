using Drawio.Net.Domain.Contract;
using Drawio.Net.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drawio.Net.API
{
    /// <summary>
    /// drawIo api
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DrawioController : ControllerBase
    {
        private ISaveDrawFileService _saveDrawFileService;
        public DrawioController(ISaveDrawFileService saveDrawFileService)
        {
            _saveDrawFileService = saveDrawFileService;
        }

        /// <summary>
        /// test
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Test()
        {
            return "hello";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public InsertFileResp InsertFile(InsertFileReq req)
        {
            var ret= _saveDrawFileService.InsertFile(req.Title, req.Content, 0);

            return new InsertFileResp
            {
                Code=200,
                Msg=ret.Msg,
                Data=ret.Data
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public DeleteFileResp DeleteFile(DeleteFileReq req)
        {
            var ret = _saveDrawFileService.DeleteFile(0, req.FileId);

            return new DeleteFileResp
            {
                Code = 200,
                Msg = ret.Msg,
                Data = ret.Data
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public GetFileInfoResp GetFileInfo(GetFileInfoReq req)
        {
            var ret = _saveDrawFileService.GetFileInfo(0, req.FileId);
            return new GetFileInfoResp
            {
                Code = 200,
                Msg = ret.Msg,
                Data = ret.Data
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public ListFilesResp ListFiles(ListFilesReq req)
        {
            var ret = _saveDrawFileService.ListFiles(0);

            return new ListFilesResp
            {
                Code=200,
                Data=ret.Data,
                Msg=ret.Msg
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public RenameFileResp RenameFile(RenameFileReq req)
        {
            var ret = _saveDrawFileService.RenameFile(0, req.FileId, req.NewTitle);

            return new RenameFileResp
            {
                Code = 200,
                Data = ret.Data,
                Msg = ret.Msg
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public SaveFileResp SaveFile(SaveFileReq req)
        {
            var ret = _saveDrawFileService.SaveFile(0, req.FileId, req.Title, req.Content);

            return new SaveFileResp
            {
                Code = 200,
                Data = ret.Data,
                Msg = ret.Msg
            };
        }
    }
}
