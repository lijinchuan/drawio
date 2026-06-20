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
                .AddIndex(DrawFileEntity.INDEXUSERIDCREATIME, i => i.Asc(j => j.UserId).Desc(j => j.CrateTime))
                .AddIndex(DrawFileEntity.INDEXUSERIDUPDATETIME, i => i.Asc(j => j.UserId).Desc(j => j.UpdateTime));
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

        public DrawFileEntity FindByTitle(string userId, string title)
        {
            var entity = BigEntityTableEngine.LocalEngine.Find<DrawFileEntity>(nameof(DrawFileEntity), DrawFileEntity.INDEXUSERIDTITLE, new object[] { userId, title }).FirstOrDefault();
            return entity;
        }

        public DrawFileEntity GetFileInfo(long fileId)
        {
            var entity = BigEntityTableEngine.LocalEngine.Find<DrawFileEntity>(nameof(DrawFileEntity), fileId);
            return entity;
        }

        public long InsertFile(string title, string content, string userId)
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
                FileSize=content.Length,
                CrateTime=DateTime.Now,
                UpdateTime=DateTime.Now,
                IsValid=true
            };
            BigEntityTableEngine.LocalEngine.Insert(nameof(DrawFileEntity), entity);

            return entity.Id;
        }

        public List<DrawFileEntity> ListFiles(string userId, string search, int page, int pageSize, out int totalCount)
        {
            // 1. 使用索引按 UserId 查询该用户全部文件
            var entities = BigEntityTableEngine.LocalEngine
                .Find<DrawFileEntity>(nameof(DrawFileEntity), DrawFileEntity.INDEXUSERID, new object[] { userId });

           // 2. 按 UpdateTime 降序排序（最新修改的在前）
           var orderedQuery = entities.OrderByDescending(e => e.UpdateTime);
           IEnumerable<DrawFileEntity> query = orderedQuery;

            // 3. 如果 search 非空，按标题模糊匹配（不区分大小写）
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(e => e.Title != null
                    && e.Title.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            // 4. 计算符合条件的总记录数
            var list = query.ToList();
            totalCount = list.Count;

            // 5. 分页截取
            return list.Skip((page - 1) * pageSize).Take(pageSize).ToList();
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
            entity.FileSize = content.Length;
            entity.UpdateTime = DateTime.Now;
            BigEntityTableEngine.LocalEngine.Update(nameof(DrawFileEntity), entity);

            return fileId;
        }
    }
}
