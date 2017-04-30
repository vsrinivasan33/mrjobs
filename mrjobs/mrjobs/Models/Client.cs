using System;
namespace mrjobs.Models
{
	public class Client
	{
		[Newtonsoft.Json.JsonProperty("id")]
		public string Id { get; set; }

		//[Microsoft.WindowsAzure.MobileServices.Version]
		//public string AzureVersion { get; set; }

		public string Name { get; set; }

		[Newtonsoft.Json.JsonIgnore]
		public Address BillingAddress { get; set; }

		public string ContactNumber { get; set; }

		public string Email { get; set; }

		[Newtonsoft.Json.JsonIgnore]
		public string AddressLine1 { get { return BillingAddress != null ? BillingAddress.AddressLine1 : String.Empty; } }
		[Newtonsoft.Json.JsonIgnore]
		public string AddressLine2
		{
			get
			{
				return BillingAddress != null ? String.Format("{0} {1} {2}", BillingAddress.Suburb, BillingAddress.State, BillingAddress.PostCode)
													  : String.Empty;
			}
		}
	}

	public class NewClient
	{
		public string Name { get; set; }
		public string AddressLine1 { get; set; }
		public string AddressLine2 { get; set; }
		public string Suburb { get; set; }
		public string State { get; set; }
		public string PostCode { get; set; }
		public string ContactNumber { get; set; }
		public string Email { get; set; }
		public string Country { get; set; } = "AUS";
	}
}
