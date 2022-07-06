using System.Collections.Generic;
using Sample.ElectronicCommerce.Security.Repositories;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;
using System;
using Sample.ElectronicCommerce.Core.Entities.Exceptions;
using Sample.ElectronicCommerce.Core.Util;

namespace Sample.ElectronicCommerce.Security.Services
{
    public class UserRoleService
    {
        private readonly UserRoleRepository _repository;

        public UserRoleService(UserRoleRepository repository) => _repository = repository;

        public UserRoleEntity Create(UserRoleEntity pEntity)
        {
            pEntity.DtCreation = DateTime.Now;
            _repository.Create(pEntity);
            return pEntity;
        }

        public UserRoleEntity ReadById(string pId)
        {
            UserRoleEntity entity = _repository.ReadById(pId);
            if (entity == null) throw new BadRequestException(AppConstant.DeMessageDataNotFoundWS);
            return entity;
        }

        public List<UserRoleEntity> ReadAll()
        {
            List<UserRoleEntity> list = _repository.ReadAll();
            return (list.Count > 0) ? list : null;
        }

        public UserRoleEntity Update(UserRoleEntity pEntity)
        {
            pEntity.DtLastUpdate = DateTime.Now;
            _repository.Update(pEntity);
            return pEntity;
        }

        public void Delete(string pId)
        {
            this.ReadById(pId);
            _repository.Delete(pId);
        }
    }
}