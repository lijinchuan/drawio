using Drawio.Net.Domain.Contract;
using Drawio.Net.Service;
using Microsoft.AspNetCore.Authorization;
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
    public class DrawioController : AuthControllerBaseController
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
        public InsertFileResp InsertFile([FromForm] InsertFileReq req)
        {
            var ret = _saveDrawFileService.InsertFile(req.Title, req.Content, GetUserInfo().userId);

            return new InsertFileResp
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
        public DeleteFileResp DeleteFile([FromForm] DeleteFileReq req)
        {
            if (req.FileId > 0)
            {
                var ret = _saveDrawFileService.DeleteFile(GetUserInfo().userId, req.FileId);
                return new DeleteFileResp
                {
                    Code = 200,
                    Msg = ret.Msg,
                    Data = ret.Data
                };
            }
            else
            {
                var ret = _saveDrawFileService.DeleteFile(GetUserInfo().userId, GetUserInfo().userId, req.Title);
                return new DeleteFileResp
                {
                    Code = 200,
                    Msg = ret.Msg,
                    Data = ret.Data
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public FindByTitleResp FindByTitle([FromForm] FindByTitleReq req)
        {
            var ret = _saveDrawFileService.FindByTitle(GetUserInfo().userId, GetUserInfo().userId, req.Title);
            return new FindByTitleResp
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
        public GetFileInfoResp GetFileInfo([FromForm] GetFileInfoReq req)
        {
            var ret = _saveDrawFileService.GetFileInfo(GetUserInfo().userId, req.FileId);
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
        [HttpPost]
        public ListFilesResp ListFiles([FromForm] ListFilesReq req)
        {
            var ret = _saveDrawFileService.ListFiles(GetUserInfo().userId);

            return new ListFilesResp
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
        [HttpPost]
        public RenameFileResp RenameFile([FromForm]RenameFileReq req)
        {
            var ret = _saveDrawFileService.RenameFile(GetUserInfo().userId, req.FileId, req.NewTitle);

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
        [HttpPost]
        public SaveFileResp SaveFile([FromForm]SaveFileReq req)
        {
            var ret = _saveDrawFileService.SaveFile(GetUserInfo().userId, req.FileId, req.Title, req.Content);

            return new SaveFileResp
            {
                Code = 200,
                Data = ret.Data,
                Msg = ret.Msg
            };
        }
    }
}
