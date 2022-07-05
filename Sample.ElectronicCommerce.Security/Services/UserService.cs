using System;
using System.Collections.Generic;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;
using Sample.ElectronicCommerce.Security.Repositories;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using Sample.ElectronicCommerce.Core.Util;
using Sample.ElectronicCommerce.Core.Entities.Exceptions;

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
                Phone = pEntity.Phone
            };
            _repository.Create(entity);
            this.ForgotPassword(new ForgotPasswordDTO() { UserName = pEntity.Mail });
        }

        public void ForgotPassword(ForgotPasswordDTO pEntity)
        {
            UserEntity user = this.ReadByMail(pEntity.UserName);
            user.Password = null;
            user.Status = AppConstant.StatusBlock;
            user.NuAuthAttempts = 0;
            user.Code = Guid.NewGuid().ToString().Substring(0, 8);
            user.DtLastBlock = DateTime.Now;
            _repository.Update(user);
            //enviar e-mail para recuperação de senha
        }
        public void ResetPassword(ResetPasswordDTO pEntity)
        {
            UserEntity user = this.ReadById(pEntity.Id);
            if (!user.Status.Equals(AppConstant.StatusBlock)) throw new BadRequestException("Usuário já foi ativado!");
            if (!user.Code.Equals(pEntity.Code)) throw new BadRequestException("Código inválido para recuperação de senha!");
            user.Password = pEntity.NewPassword;
            user.Status = pEntity.Unblock ? AppConstant.StatusActive : AppConstant.StatusBlock;
            user.NuAuthAttempts = 0;
            user.Code = null;
            user.DtLastDesblock = DateTime.Now;
            _repository.Update(user);
            //enviar e-mail para senha cadastrada com sucesso
        }
    }
}