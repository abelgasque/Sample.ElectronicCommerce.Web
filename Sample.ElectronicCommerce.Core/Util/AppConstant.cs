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

        //user roles
        public const string UserRoleSystem = "ROLE_SYSTEM";
        public const string UserRoleAdmin = "ROLE_ADMINISTRATOR";
        public const string UserRoleCustomer = "ROLE_CUSTOMER";

        //database sql server
        public const string P_IS_TEST = "@P_IS_TEST";
        public const string P_MUST_FILTER_YEAR = "@P_MUST_FILTER_YEAR";
        public const string SPR_WS_GET_LOG_APP_FOR_CHART_DYNAMIC = "SPR_WS_GET_LOG_APP_FOR_CHART_DYNAMIC";
        public const string LOG_APP = "LOG_APP";
        public const string ID_APPLICATION = "ID_APPLICATION";
        public const string ID_USER_SESSION = "ID_USER_SESSION";
        public const string ID_LOG_APP = "ID_LOG_APP";
        public const string DE_CONTENT = "DE_CONTENT";
        public const string DE_RESULT = "DE_RESULT";
        public const string DE_EXCEPTION_MESSAGE = "DE_EXCEPTION_MESSAGE";
        public const string DE_STACK_TRACE = "DE_STACK_TRACE";
        public const string DE_MESSAGE = "DE_MESSAGE";
        public const string NM_METHOD = "NM_METHOD";
        public const string NU_VERSION = "NU_VERSION";
        public const string DT_CREATION = "DT_CREATION";
        public const string DT_LAST_UPDATE = "DT_LAST_UPDATE";
        public const string IS_TEST = "IS_TEST";
        public const string IS_SUCCESS = "IS_SUCCESS";
        public const string IS_ACTIVE = "IS_ACTIVE";
    }
}
