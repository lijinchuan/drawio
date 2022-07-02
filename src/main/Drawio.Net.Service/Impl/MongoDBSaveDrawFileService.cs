using AutoMapper;
using Drawio.Net.Domain.Contract;
using Drawio.Net.Domain.Entity;
using Drawio.Net.Domain.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawio.Net.Service.Impl
{
    public class MongoDBSaveDrawFileService : ISaveDrawFileService
    {
        private IMongoClient _mongoClient;
        private IMapper _mapper;
        public MongoDBSaveDrawFileService(IMongoClient mongoClient,IMapper mapper)
        {
            _mongoClient = mongoClient;
            _mapper = mapper;
        }

        public OpResult<bool> DeleteFile(string opId, long fileId)
        {
            var delResult= _mongoClient.GetDatabase(MongoDrawFileEntity.DBName)
                .GetCollection<MongoDrawFileEntity>(MongoDrawFileEntity.CollectionName)
                .DeleteOneAsync(p => p.Fid == fileId).Result;
            return new OpResult<bool>
            {
                Data = delResult.DeletedCount > 0,
                Msg = delResult.DeletedCount > 0 ? "成功" : "失败",
                Success = delResult.DeletedCount > 0
            };
        }

        public OpResult<bool> DeleteFile(string opId, string userId, string title)
        {
            if (opId != userId)
            {
                return new OpResult<bool>
                {
                    Data = false,
                    Success = false,
                    Msg = "没权限"
                };
            }
            var collection = _mongoClient.GetDatabase(MongoDrawFileEntity.DBName)
                .GetCollection<MongoDrawFileEntity>(MongoDrawFileEntity.CollectionName);
            var entity = collection
                .Find(p => p.UserId == userId && p.Title == title).FirstOrDefault();

            if (entity == null)
            {
                return new OpResult<bool>
                {
                    Data = false,
                    Success = false,
                    Msg = "数据不存在"
                };
            }

            var delResult = collection.DeleteOne(p => p.Fid == entity.Fid);

            return new OpResult<bool>
            {
                Data = delResult.DeletedCount > 0,
                Msg = delResult.DeletedCount > 0 ? "成功" : "失败",
                Success = delResult.DeletedCount > 0
            };
        }

        public OpResult<DrawFileModel> FindByTitle(string opId, string userId, string title)
        {
            var collection = _mongoClient.GetDatabase(MongoDrawFileEntity.DBName)
              .GetCollection<MongoDrawFileEntity>(MongoDrawFileEntity.CollectionName);
            var file = collection.Find(p=>p.UserId==userId&&p.Title==title).FirstOrDefault();
            if (file == null || file.UserId != opId)
            {
                file = null;
            }
            return new OpResult<DrawFileModel>
            {
                Data = _mapper.Map<DrawFileModel>(file),
                Success = true,
                Msg = "成功"
            };
        }

        public OpResult<DrawFileModel> GetFileInfo(string opId, long fileId)
        {
            var collection = _mongoClient.GetDatabase(MongoDrawFileEntity.DBName)
             .GetCollection<MongoDrawFileEntity>(MongoDrawFileEntity.CollectionName);
            var file = collection.Find(p => p.Fid == fileId).FirstOrDefault();
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

        public OpResult<long> InsertFile(string title, string content, string userId)
        {
            var filter=new FilterDefinitionBuilder<MongoAutoId>().Eq(p => p.Name,nameof(MongoDrawFileEntity));
            var update = new UpdateDefinitionBuilder<MongoAutoId>().Inc(p => p.LastId, 1);
            var options = new FindOneAndUpdateOptions<MongoAutoId,MongoAutoId>()
            {
                IsUpsert=true,
            };
            var collectionId= _mongoClient.GetDatabase(MongoAutoId.DBName)
             .GetCollection<MongoAutoId>(MongoAutoId.CollectionName);

            var upresult = collectionId.FindOneAndUpdate<MongoAutoId>(filter, update, options);
            var newid = upresult == null ? 1 : upresult.LastId + 1;

            var collection = _mongoClient.GetDatabase(MongoDrawFileEntity.DBName)
            .GetCollection<MongoDrawFileEntity>(MongoDrawFileEntity.CollectionName);

            collection.InsertOne(new MongoDrawFileEntity
            {
                Fid=newid,
                Content=content,
                Title=title,
                IsValid=true,
                CrateTime=DateTime.Now,
                UpdateTime=DateTime.Now,
                UserId=userId
            });

            return new OpResult<long>
            {
                Success=true,
                Msg = "成功",
                Data = newid
            };
        }

        public OpResult<List<DrawFileModel>> ListFiles(string userId)
        {
            var collection = _mongoClient.GetDatabase(MongoDrawFileEntity.DBName)
           .GetCollection<MongoDrawFileEntity>(MongoDrawFileEntity.CollectionName);
            var files = collection.Find(p=>p.UserId==userId).ToList();
            return new OpResult<List<DrawFileModel>>
            {
                Data = _mapper.Map<List<DrawFileModel>>(files),
                Success = true,
                Msg = "成功"
            };
        }

        public OpResult<bool> RenameFile(string opId, long fileId, string newTitle)
        {
            var collection = _mongoClient.GetDatabase(MongoDrawFileEntity.DBName)
        .GetCollection<MongoDrawFileEntity>(MongoDrawFileEntity.CollectionName);
            var file = collection.Find(p => p.Fid == fileId).FirstOrDefault();
            if (file == null || file.UserId != opId)
            {
                throw new Exception("无权限");
            }
            var boo = false;
            if (file.Title != newTitle)
            {
                var ret = collection.UpdateOne(new FilterDefinitionBuilder<MongoDrawFileEntity>().Eq(p => p.Fid, fileId),
                    new UpdateDefinitionBuilder<MongoDrawFileEntity>().Set(p => p.Title, newTitle));

                boo = ret.ModifiedCount == 1;
            }
            return new OpResult<bool>
            {
                Data = boo,
                Success = true,
                Msg = "成功"
            };
        }

        public OpResult<long> SaveFile(string opId, long fileId, string title, string content)
        {
            if (fileId == 0)
            {
                return InsertFile(title, content, opId);
            }
            var file = GetFileInfo(opId, fileId).Data;
            if (file == null || file.UserId != opId)
            {
                throw new Exception("无权限");
            }

            var collection = _mongoClient.GetDatabase(MongoDrawFileEntity.DBName)
      .GetCollection<MongoDrawFileEntity>(MongoDrawFileEntity.CollectionName);
            var filter = new FilterDefinitionBuilder<MongoDrawFileEntity>().Eq(p => p.Fid, fileId);
            var updateBuilder = new UpdateDefinitionBuilder<MongoDrawFileEntity>();
            UpdateDefinition<MongoDrawFileEntity> update = null;
            if (title != file.Title)
            {
                update=updateBuilder.Set(p => p.Title, title);
            }
            if (content != file.Content)
            {
                update = update.Set(p => p.Content, content);
            }
            
            var ret = collection.UpdateOne(filter, update);

            return new OpResult<long>
            {
                Data = fileId,
                Success = true,
                Msg = "成功"
            };
        }
    }
}
