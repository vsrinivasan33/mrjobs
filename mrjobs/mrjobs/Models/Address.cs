using System;
namespace mrjobs.Models
{

	public class Address
	{
		[Newtonsoft.Json.JsonProperty("id")]
		public string Id { get; set; }

		//[Microsoft.WindowsAzure.MobileServices.Version]
		//public string AzureVersion { get; set; }
		[Newtonsoft.Json.JsonIgnore]
		public string AddressLine1 { get; set; }
		[Newtonsoft.Json.JsonIgnore]
		public string AddressLine2 { get; set; }

		public string Suburb { get; set; }

		public string State { get; set; }

		public string Country { get; set; } = "AUS";

		public string PostCode { get; set; }
	}
}
