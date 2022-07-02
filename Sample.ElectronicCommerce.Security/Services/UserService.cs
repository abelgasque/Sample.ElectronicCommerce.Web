using System;
using System.Collections.Generic;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;
using Sample.ElectronicCommerce.Security.Repositories;
using Sample.ElectronicCommerce.Core.Entities.DTO;

namespace Sample.ElectronicCommerce.Security.Services
{
    public class UserService
    {
        private readonly UserRepository _repository;

        public UserService(UserRepository repository) => _repository = repository;

        public UserEntity Create(UserEntity pEntity) => _repository.Create(pEntity);

        public UserEntity ReadById(string pId) => _repository.ReadById(pId);

        public UserEntity ReadByMail(string pMail) => _repository.ReadByMail(pMail);

        public List<UserEntity> ReadAll() => _repository.ReadAll();

        public UserEntity Update(UserEntity pEntity) => _repository.Update(pEntity);

        public void Delete(string pId) => _repository.Delete(pId);

        public void CreateLeadAsync(UserLeadDTO pEntity)
        {
            UserEntity entity = new UserEntity()
            {
                Name = pEntity.Name,
                Mail = pEntity.Mail,
                NuCellPhone = pEntity.Phone
            };
            _repository.Create(entity);
            this.Block(entity);
        }

        public void Block(UserEntity pEntity)
        {
            pEntity.IsBlock = true;
            pEntity.NuAuthAttemptsFail = 0;
            pEntity.Code = Guid.NewGuid().ToString().Substring(0, 8);
            _repository.Update(pEntity);
        }
    }
}