using AutoMapper;
using Drawio.Net.Data;
using Drawio.Net.Domain.Entity;
using Drawio.Net.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawio.Net.Service.Impl
{
    public class SaveDrawFileService : ISaveDrawFileService
    {
        private readonly IDrawFileDao _drawFileDao;
        private readonly IMapper _mapper;
        public SaveDrawFileService(IDrawFileDao drawFileDao,IMapper mapper)
        {
            _drawFileDao = drawFileDao;
            _mapper = mapper;
        }

        public OpResult<bool> DeleteFile(int opId, long fileId)
        {
            try
            {
                var file = _drawFileDao.GetFileInfo(fileId);
                if (file == null || file.UserId != opId)
                {
                    throw new Exception("无权限");
                }

                var boo = _drawFileDao.DeleteFile(fileId);

                return new OpResult<bool>
                {
                    Data = boo,
                    Success = true,
                    Msg = "成功"
                };
            }
            catch(Exception ex)
            {
                return new OpResult<bool>
                {
                    Data=false,
                    Msg=ex.Message,
                    Success=false
                };
            }
        }

        public OpResult<DrawFileModel> GetFileInfo(int opId, long fileId)
        {
            try
            {
                var file = _drawFileDao.GetFileInfo(fileId);
                if (file == null || file.UserId != opId)
                {
                    throw new Exception("无权限");
                }
                return new OpResult<DrawFileModel>
                {
                    Data = _mapper.Map<DrawFileModel>(file),
                    Success = true,
                    Msg = "成功"
                };
            }
            catch (Exception ex)
            {
                return new OpResult<DrawFileModel>
                {
                    Data = null,
                    Msg = "未知错误",
                    Success = false
                };
            }
        }

        public OpResult<long> InsertFile(string title, string content, int userId)
        {
            try
            {
                var boo = _drawFileDao.InsertFile(title, content, userId);

                return new OpResult<long>
                {
                    Data = boo,
                    Success = true,
                    Msg = "成功"
                };
            }
            catch(Exception ex)
            {
                return new OpResult<long>
                {
                    Data = 0,
                    Msg = ex.Message,
                    Success = false
                };
            }
        }

        public OpResult<List<DrawFileModel>> ListFiles(int userId)
        {
            try
            {
                var files = _drawFileDao.ListFiles(userId);
                return new OpResult<List<DrawFileModel>>
                {
                    Data = _mapper.Map<List<DrawFileModel>>(files),
                    Success = true,
                    Msg = "成功"
                };
            }
            catch(Exception ex)
            {
                return new OpResult<List<DrawFileModel>>
                {
                    Data = null,
                    Success = true,
                    Msg = ex.Message
                };
            }

        }

        public OpResult<bool> RenameFile(int opId, long fileId, string newTitle)
        {
            try
            {
                var file = _drawFileDao.GetFileInfo(fileId);
                if (file == null || file.UserId != opId)
                {
                    throw new Exception("无权限");
                }
                return new OpResult<bool>
                {
                    Data = _drawFileDao.RenameFile(fileId,newTitle),
                    Success = true,
                    Msg = "成功"
                };
            }
            catch (Exception ex)
            {
                return new OpResult<bool>
                {
                    Data = false,
                    Msg = ex.Message,
                    Success = false
                };
            }
        }

        public OpResult<bool> SaveFile(int opId, long fileId, string title, string content)
        {
            try
            {
                var file = _drawFileDao.GetFileInfo(fileId);
                if (file == null || file.UserId != opId)
                {
                    throw new Exception("无权限");
                }
                return new OpResult<bool>
                {
                    Data = _drawFileDao.SaveFile(fileId,title,content),
                    Success = true,
                    Msg = "成功"
                };
            }
            catch (Exception ex)
            {
                return new OpResult<bool>
                {
                    Data = false,
                    Msg = ex.Message,
                    Success = false
                };
            }
        }
    }
}
