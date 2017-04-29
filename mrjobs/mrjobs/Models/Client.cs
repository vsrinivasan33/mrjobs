using System;
namespace mrjobs.Models
{
	public class Client
	{
		[Newtonsoft.Json.JsonProperty("Id")]
		public string Id { get; set; }

		[Microsoft.WindowsAzure.MobileServices.Version]
		public string AzureVersion { get; set; }

		public string Name { get; set; }

		public Address BillingAddress { get; set; }

		public string ContactNumber { get; set; }

		public string Email { get; set; }

		public string AddressLine1 { get { return BillingAddress != null ? BillingAddress.AddressLine1 : String.Empty; } }

		public string AddressLine2
		{
			get
			{
				return BillingAddress != null ? String.Format("{0} {1} {2}", BillingAddress.Suburb, BillingAddress.State, BillingAddress.PostCode)
													  : String.Empty;
			}
		}
	}
}
