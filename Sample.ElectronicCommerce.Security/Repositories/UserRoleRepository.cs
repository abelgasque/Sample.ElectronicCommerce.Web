using System;
using System.Collections.Generic;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using MongoDB.Bson;
using AppMongoClient = Sample.ElectronicCommerce.Core.Entities.Settings;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;

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
        public void Create(UserRoleEntity pEntity) => _collection.InsertOneAsync(pEntity);

        public List<UserRoleEntity> ReadAll()
        {
            Expression<Func<UserRoleEntity, bool>> filter = x => x.IsActive == true;
            return _collection.Find(filter).ToList();
        }

        public UserRoleEntity ReadById(string pId)
        {
            Expression<Func<UserRoleEntity, bool>> filter = x => x.Id.Equals(ObjectId.Parse(pId));
            return _collection.Find(filter).FirstOrDefault();
        }

        public void Update(UserRoleEntity pEntity)
        {
            Expression<Func<UserRoleEntity, bool>> filter = x => x.Id.Equals(pEntity.Id);            
            _collection.ReplaceOne(filter, pEntity);
        }

        public void Delete(string pId) => _collection.FindOneAndDelete(pId);
        #endregion
    }
}