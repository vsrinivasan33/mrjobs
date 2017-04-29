using System;
namespace mrjobs.Models
{

	public class Address
	{
		[Newtonsoft.Json.JsonProperty("Id")]
		public string Id { get; set; }
		
		public string AddressLine1 { get; set; }

		public string AddressLine2 { get; set; }

		public string Suburb { get; set; }

		public string State { get; set; }

		public string Country { get; set; }

		public string PostCode { get; set; }
	}
}
