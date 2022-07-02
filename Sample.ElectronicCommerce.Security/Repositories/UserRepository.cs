using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AppMongoClient = Sample.ElectronicCommerce.Core.Entities.Settings;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;
using Sample.ElectronicCommerce.Core.Util;
using Sample.ElectronicCommerce.Core.Entities.Exceptions;

namespace Sample.ElectronicCommerce.Security.Repositories
{
    public class UserRepository
    {
        #region Variables
        private readonly AppMongoClient.MongoClientSettings _mongoClientSettings;

        private readonly IMongoCollection<UserEntity> _collection;
        #endregion

        #region Constructor
        public UserRepository(IOptions<AppMongoClient.MongoClientSettings> mongoClientSettings)
        {
            _mongoClientSettings = mongoClientSettings.Value;
            var mongoClient = new MongoClient(_mongoClientSettings.GetConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(_mongoClientSettings.DataBase);
            _collection = mongoDatabase.GetCollection<UserEntity>(_mongoClientSettings.UserColletion);
        }
        #endregion

        #region Methods Crud
        public UserEntity Create(UserEntity pEntity)
        {
            pEntity.DtCreation = DateTime.Now;
            _collection.InsertOne(pEntity);
            return pEntity;
        }

        public UserEntity ReadById(string pId)
        {
            Expression<Func<UserEntity, bool>> filter = x => x.Id.Equals(ObjectId.Parse(pId));
            UserEntity entity = _collection.Find(filter).FirstOrDefault();
            if (entity == null) throw new BadRequestException(AppConstant.DeMessageDataNotFoundWS);
            return entity;
        }

        public UserEntity ReadByMail(string pMail)
        {
            Expression<Func<UserEntity, bool>> filter = x => x.Mail.Equals(pMail);
            UserEntity entity = _collection.Find(filter).FirstOrDefault();
            if (entity == null) throw new BadRequestException(AppConstant.DeMessageDataNotFoundWS);
            return entity;
        }

        public List<UserEntity> ReadAll()
        {
            List<UserEntity> listEntities = _collection.Aggregate().ToList();
            if ((listEntities == null) || (listEntities.Count <= 0)) throw new BadRequestException(AppConstant.DeMessageDataNotFoundWS);
            return listEntities;
        }

        public UserEntity Update(UserEntity pEntity)
        {
            Expression<Func<UserEntity, bool>> filter = x => x.Id.Equals(pEntity.Id);
            UserEntity entity = _collection.Find(filter).FirstOrDefault();
            if (entity == null) throw new BadRequestException(AppConstant.DeMessageDataNotFoundWS);
            pEntity.DtLastUpdate = DateTime.Now;
            _collection.ReplaceOne(filter, pEntity);
            return entity;
        }

        public void Delete(string pId)
        {
            UserEntity entity = this.ReadById(pId);
            _collection.FindOneAndDelete(pId);
        }
        #endregion
    }
}