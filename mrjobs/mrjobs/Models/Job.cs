using System;
namespace mrjobs.Models
{
	public class Job
	{
		[Newtonsoft.Json.JsonProperty("id")]
		public string Id { get; set; }

		public string ClientId { get; set; }

		public string ContactName { get; set; }

		public string ContactNumber { get; set; }

		public Address SiteAddress { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime InvoicedDate { get; set; }

		public string Notes { get; set; }

		public JobStatus Status { get; set; }

		[Newtonsoft.Json.JsonIgnore]
		public bool SameAsBillingAddress { get; set; }

		[Newtonsoft.Json.JsonIgnore]
		public string StartDateString { get { return StartDate == DateTime.MinValue ? "Not closed yet" : StartDate.ToString("dd-MMM-yyyy"); } }

		[Newtonsoft.Json.JsonIgnore]
		public string InvoicedDateString { get { return InvoicedDate == DateTime.MinValue ? "Not closed yet" : InvoicedDate.ToString("dd-MMM-yyyy"); } }

		[Newtonsoft.Json.JsonIgnore]
		public string JobStatusImage
		{
			get
			{
				return string.Format("{0}.png", Status.ToString().ToLower());
			}
		}
	}
}
