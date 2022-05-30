using Newtonsoft.Json;
using System;

namespace Sample.ElectronicCommerce.Shared.Entities.DataBase
{
    public class GetLogAppForChartMonthDb
    {
        public GetLogAppForChartMonthDb() { }

        public GetLogAppForChartMonthDb(dynamic pRowData)
        {
            IdMonth = pRowData.ID_MONTH;
            NmMonth = pRowData.NM_MONTH;
            DtStartRange = pRowData.DT_START_RANGE;
            DtEndRange =pRowData.DT_END_RANGE;
            NuSuccess = pRowData.NU_SUCCESS;
            NuError = pRowData.NU_ERROR;
        }

        [JsonProperty("idMonth")]
        public int IdMonth { get; set; }

        [JsonProperty("nmMonth")]
        public string NmMonth { get; set; }
        
        [JsonProperty("dtStartRange")]
        public DateTime DtStartRange { get; set; }

        [JsonProperty("dtEndRange")]
        public DateTime DtEndRange { get; set; }

        [JsonProperty("nuSuccess")]
        public long NuSuccess { get; set; }

        [JsonProperty("nuError")]
        public long NuError { get; set; }
    }
}
