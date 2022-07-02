using System;
using System.Collections.Generic;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using MongoDB.Bson;
using AppMongoClient = Sample.ElectronicCommerce.Core.Entities.Settings;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;
using Sample.ElectronicCommerce.Core.Util;
using Sample.ElectronicCommerce.Core.Entities.Exceptions;

namespace Sample.ElectronicCommerce.Security.Repositories
{
    public class UserRoleRepository
    {
        #region Variables
        private readonly AppMongoClient.MongoClientSettings _mongoClientSettings;

        private readonly IMongoCollection<UserRoleEntity> _collection;
        #endregion

        #region Constructor
        public UserRoleRepository(IOptions<AppMongoClient.MongoClientSettings> mongoClientSettings)
        {
            _mongoClientSettings = mongoClientSettings.Value;
            var mongoClient = new MongoClient(_mongoClientSettings.GetConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(_mongoClientSettings.DataBase);
            _collection = mongoDatabase.GetCollection<UserRoleEntity>(_mongoClientSettings.UserRoleColletion);
        }
        #endregion

        #region Methods CRUD
        public UserRoleEntity Create(UserRoleEntity pEntity)
        {
            pEntity.DtCreation = DateTime.Now;
            _collection.InsertOneAsync(pEntity);
            return pEntity;
        }

        public UserRoleEntity ReadById(string pId)
        {
            Expression<Func<UserRoleEntity, bool>> filter = x => x.Id.Equals(ObjectId.Parse(pId));
            UserRoleEntity entity = _collection.Find(filter).FirstOrDefault();
            if (entity == null) throw new BadRequestException(AppConstant.DeMessageDataNotFoundWS);
            return entity;
        }

        public List<UserRoleEntity> ReadAll()
        {
            Expression<Func<UserRoleEntity, bool>> filter = x => x.IsActive == true;
            List<UserRoleEntity> listEntities = _collection.Find(filter).ToList();
            if ((listEntities == null) || (listEntities.Count <= 0)) throw new BadRequestException(AppConstant.DeMessageDataNotFoundWS);
            return listEntities;
        }
        public UserRoleEntity Update(UserRoleEntity pEntity)
        {
            Expression<Func<UserRoleEntity, bool>> filter = x => x.Id.Equals(pEntity.Id);
            UserRoleEntity entity = _collection.Find(filter).FirstOrDefault();
            if (entity == null) throw new BadRequestException(AppConstant.DeMessageDataNotFoundWS);
            pEntity.DtLastUpdate = DateTime.Now;
            _collection.ReplaceOne(filter, pEntity);
            return entity;
        }

        public void Delete(string pId)
        {
            UserRoleEntity entity = this.ReadById(pId);
            _collection.FindOneAndDelete(pId);
        }
        #endregion
    }
}