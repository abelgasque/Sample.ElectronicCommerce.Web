using System.Collections.Generic;
using Sample.ElectronicCommerce.Security.Repositories;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;

namespace Sample.ElectronicCommerce.Security.Services
{
    public class UserRoleService
    {
        private readonly UserRoleRepository _repository;

        public UserRoleService(UserRoleRepository repository) => _repository = repository;

        public UserRoleEntity Create(UserRoleEntity pEntity) => _repository.Create(pEntity);

        public UserRoleEntity ReadById(string pId) => _repository.ReadById(pId);

        public List<UserRoleEntity> ReadAll() => _repository.ReadAll();

        public UserRoleEntity Update(UserRoleEntity pEntity) => _repository.Update(pEntity);

        public void Delete(string pId) => _repository.Delete(pId);
    }
}