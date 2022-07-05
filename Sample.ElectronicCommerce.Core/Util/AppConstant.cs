namespace Sample.ElectronicCommerce.Core.Util
{
    public class AppConstant
    {
        //messages
        public const string DeMessageSuccessWS = "OK";
        public const string DeMessageDataNotFoundWS = "Nenhum registro encontrado";
        public const string DeMessageInvalidModel = "Erro de validação";
        public const string StandardErrorMessageForDataBase = "Ocorreu um erro ao realizar chamada do Banco de Dados";
        public const string StandardErrorMessageRepository = "Ocorreu um erro ao realizar chamada na classe de repositório";
        public const string StandardErrorMessageService = "Ocorreu um erro ao realizar chamada na classe de serviço";
        public const string ServerExceptionHandlerMessageWS = "Ocorreu um erro ao realizar chamada do Web Service";
        public const string DeErrorMessageMailBrokerWS = "Agente de e-mail não configurado para está mensagem";

        public const string RoleAdmin = "ROLE_ADMINISTRATOR";
        public const string RoleCustomer = "ROLE_CUSTOMER";
        public const string RoleSystem = "ROLE_SYSTEM";

        public const string StatusActive = "active";
        public const string StatusInactive = "desactive";
        public const string StatusBlock = "block";
    }
}
