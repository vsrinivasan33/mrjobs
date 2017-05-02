using System;
namespace mrjobs.Models
{

	public class ContactAddress
	{
		[Newtonsoft.Json.JsonProperty("id")]
		public string Id { get; set; }

		//[Microsoft.WindowsAzure.MobileServices.Version]
		//public string AzureVersion { get; set; }

		public string AddressLine1 { get; set; }

		public string AddressLine2 { get; set; }

		public string Suburb { get; set; }

		public string State { get; set; }

		public string Country { get; set; } = "AUS";

		public string PostCode { get; set; }
	}
}
