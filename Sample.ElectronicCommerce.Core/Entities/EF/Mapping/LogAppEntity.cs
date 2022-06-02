using System;

namespace Sample.ElectronicCommerce.Core.Entities.Mapping
{
	public class LogAppEntity
    {
		public long Id { get; set; }

		public long IdApplication { get; set; }

		public long? IdUserSession { get; set; }

		public DateTime DtCreation { get; set; }

		public DateTime? DtLastUpdate { get; set; }
		
		public string NuVersion { get; set; }
		
		public string NmMethod { get; set; }
		
		public string DeContent { get; set; }
		
		public string DeResult { get; set; }
		
		public string DeMessage { get; set; }
		
		public string DeExceptionMessage { get; set; }
		
		public string DeStackTrace { get; set; }
		
		public bool IsSuccess { get; set; }		

        public bool IsTest { get; set; }

        public bool IsActive { get; set; }
	}
}
