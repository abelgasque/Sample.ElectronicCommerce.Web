namespace Sample.ElectronicCommerce.Core.Entities.DTO
{
    public class ReturnDTO
    {
        #region Constructor
        public ReturnDTO() { }

        public ReturnDTO(bool pIsSuccess, string pDeMessage, object pResultObject)
        {
            IsSuccess = pIsSuccess;
            DeMessage = pDeMessage;
            ResultObject = pResultObject;
        }

        public ReturnDTO(ResponseDTO pEntity)
        {
            IsSuccess = pEntity.IsSuccess;
            DeMessage = pEntity.DeMessage;
            ResultObject = pEntity.DataObject;
        }
        #endregion

        #region Atributtes
        /// <summary>
        /// Status do retorno. Se for = true, é retorno OK
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Descrição do retorno. Se IsSuccess = false, retorna o motivo do erro
        /// </summary>
        public string DeMessage { get; set; }

        /// <summary>
        /// Status do retorno. Se for = true, é retorno OK
        /// </summary>
        public object ResultObject { get; set; }
        #endregion
    }
}
