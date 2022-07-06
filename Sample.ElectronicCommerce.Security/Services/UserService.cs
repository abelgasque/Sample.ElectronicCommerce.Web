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

        private readonly MailHelper _mailHelper;

        public UserService(
            UserRepository repository,
            MailHelper mailHelper
        )
        {
            _repository = repository;
            _mailHelper = mailHelper;
        }

        public UserEntity Create(UserEntity pEntity)
        {
            pEntity.DtCreation = DateTime.Now;
            _repository.Create(pEntity);
            return pEntity;
        }

        public UserEntity ReadById(string pId)
        {
            UserEntity entity = _repository.ReadById(pId);
            if (entity == null) throw new BadRequestException(AppConstant.DeMessageDataNotFoundWS);
            return entity;
        }

        public UserEntity ReadByMail(string pMail)
        {
            UserEntity entity = _repository.ReadByMail(pMail);
            if (entity == null) throw new BadRequestException(AppConstant.DeMessageDataNotFoundWS);
            return entity;
        }

        public List<UserEntity> ReadAll()
        {
            List<UserEntity> list = _repository.ReadAll();
            return (list.Count > 0) ? list : null;
        }

        public UserEntity Update(UserEntity pEntity)
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

        public void ForgotPassword(ForgotPasswordDTO pEntity)
        {
            UserEntity user = this.ReadByMail(pEntity.UserName);
            user.Password = null;
            user.Status = AppConstant.StatusBlock;
            user.NuAuthAttempts = 0;
            user.Code = Guid.NewGuid().ToString().Substring(0, 8);
            user.DtLastBlock = DateTime.Now;
            _repository.Update(user);
            _mailHelper.SendMailBasicWithApplicationAddress(user.UserName, "E-mail de recuperação de senha do usuário");
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
            _mailHelper.SendMailBasicWithApplicationAddress(user.UserName, "E-mail de atualização de senha do usuário");
        }
    }
}