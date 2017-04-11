using System;
namespace MrJobs.Models
{
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

		[Newtonsoft.Json.JsonIgnore]
		public bool SameAsBillingAddress { get; set; }
	}
}
