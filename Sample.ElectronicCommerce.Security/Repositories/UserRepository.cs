using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AppMongoClient = Sample.ElectronicCommerce.Core.Entities.Settings;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;

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

        #region Methods CRUD
        public void Create(UserEntity pEntity) => _collection.InsertOne(pEntity);

        public List<UserEntity> ReadAll() => _collection.Aggregate().ToList();

        public UserEntity ReadById(string pId)
        {
            Expression<Func<UserEntity, bool>> filter = x => x.Id.Equals(ObjectId.Parse(pId));
            return _collection.Find(filter).FirstOrDefault();
        }

        public UserEntity ReadByMail(string pMail)
        {
            Expression<Func<UserEntity, bool>> filter = x => x.Mail.Equals(pMail);
            return _collection.Find(filter).FirstOrDefault();
        }

        public void Update(UserEntity pEntity)
        {
            Expression<Func<UserEntity, bool>> filter = x => x.Id.Equals(pEntity.Id);
            _collection.ReplaceOne(filter, pEntity);
        }

        public void Delete(string pId) => _collection.FindOneAndDelete(pId);
        #endregion
    }
}