using Drawio.Net.Domain.Entity;
using Drawio.Net.Domain.Model;
using LJC.FrameWorkV3.Data.EntityDataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawio.Net.Data.Impl
{
    public class LocalDBDrawFileDao : IDrawFileDao
    {
        static LocalDBDrawFileDao()
        {
            BigEntityTableEngine.LocalEngine.CreateTable<DrawFileEntity>(p => p.Id, b =>
            {
                b.AddIndex(DrawFileEntity.INDEXUSERID, i => i.Asc(j => j.UserId))
                .AddIndex(DrawFileEntity.INDEXUSERIDTITLE, i => i.Asc(j => j.UserId).Asc(j => j.Title))
                .AddIndex(DrawFileEntity.INDEXUSERIDCREATIME, i => i.Asc(j => j.UserId).Desc(j => j.CrateTime));
            });
        }

        public bool DeleteFile(long fileId)
        {
            var entity = BigEntityTableEngine.LocalEngine.Find<DrawFileEntity>(nameof(DrawFileEntity), fileId);
            if (entity == null)
            {
                return false;
            }
            BigEntityTableEngine.LocalEngine.Delete<DrawFileEntity>(nameof(DrawFileEntity), fileId);
            return true;
        }

        public DrawFileEntity FindByTitle(int userId, string title)
        {
            var entity = BigEntityTableEngine.LocalEngine.Find<DrawFileEntity>(nameof(DrawFileEntity), DrawFileEntity.INDEXUSERIDTITLE, new object[] { userId, title }).FirstOrDefault();
            return entity;
        }

        public DrawFileEntity GetFileInfo(long fileId)
        {
            var entity = BigEntityTableEngine.LocalEngine.Find<DrawFileEntity>(nameof(DrawFileEntity), fileId);
            return entity;
        }

        public long InsertFile(string title, string content, int userId)
        {
            var entity = BigEntityTableEngine.LocalEngine.Find<DrawFileEntity>(nameof(DrawFileEntity), DrawFileEntity.INDEXUSERIDTITLE, new object[] { userId, title }).FirstOrDefault();
            if (entity != null)
            {
                throw new Exception("名称不能重复");
            }
            entity = new DrawFileEntity
            {
                Title = title,
                UserId = userId,
                Content = content,
                CrateTime=DateTime.Now,
                UpdateTime=DateTime.Now,
                IsValid=true
            };
            BigEntityTableEngine.LocalEngine.Insert(nameof(DrawFileEntity), entity);

            return entity.Id;
        }

        public List<DrawFileEntity> ListFiles(int userId)
        {
            var entities = BigEntityTableEngine.LocalEngine.Find<DrawFileEntity>(nameof(DrawFileEntity), DrawFileEntity.INDEXUSERID, new object[] { userId });

            return entities.ToList();

        }

        public bool RenameFile(long fileId, string newTitle)
        {
            var entity = BigEntityTableEngine.LocalEngine.Find<DrawFileEntity>(nameof(DrawFileEntity), fileId);
            if (entity == null)
            {
                throw new Exception("记录不存在"); 
            }
            if (entity.Title == newTitle)
            {
                throw new Exception("名称未修改");
            }
            entity.Title = newTitle;
            entity.UpdateTime = DateTime.Now;
            return BigEntityTableEngine.LocalEngine.Update(nameof(DrawFileEntity), entity);
        }

        public long SaveFile(long fileId, string title, string content)
        {
            var entity = BigEntityTableEngine.LocalEngine.Find<DrawFileEntity>(nameof(DrawFileEntity), fileId);
            if (entity == null)
            {
                throw new Exception("记录不存在");
            }
            if (entity.Title == title && entity.Content == content)
            {
                return fileId;
            }
            entity.Title = title;
            entity.Content = content;
            entity.UpdateTime = DateTime.Now;
            BigEntityTableEngine.LocalEngine.Update(nameof(DrawFileEntity), entity);

            return fileId;
        }
    }
}
