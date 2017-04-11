using System;
namespace MrJobs.Models
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
	}


	public class Job
	{
		[Newtonsoft.Json.JsonProperty("Id")]
		public string Id { get; set; }

		public string ClientId { get; set; }

		public string ContactPerson { get; set; }

		public string ContactNumber { get; set; }

		public Address SiteAddress { get; set; }

		public DateTime StartDate { get; set; }

		public string Notes { get; set; }

		public JobStatus Status { get; set; }

		public bool SameAsBillingAddress { get; set; }

	}


	public class Address
	{
		[Newtonsoft.Json.JsonProperty("Id")]
		public string Id { get; set; }
		
		public string AddressLine1 { get; set; }

		public string AddressLine2 { get; set; }

		public string Suburb { get; set; }

		public string State { get; set; }

		public string Country { get; set; }
	}

	public enum JobStatus 
	{ 
		Open = 0,
		Pending = 1,
		Overdue = 2,
		Complete = 3
	}
}
