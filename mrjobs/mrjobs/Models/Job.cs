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

		public string SiteAddressId { get; set; }

		[Newtonsoft.Json.JsonIgnore]
		public ContactAddress SiteAddress { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime InvoicedDate { get; set; }

		public string Notes { get; set; }

		public JobStatus Status { get; set; }

		public bool SameAsBillingAddress { get; set; } = true;

		[Newtonsoft.Json.JsonIgnore]
		public string AddressLine1 { get { return SiteAddress != null ? String.Format("{0} {1}", SiteAddress.AddressLine1, SiteAddress.AddressLine2) : "Not Available"; } }
		[Newtonsoft.Json.JsonIgnore]
		public string AddressLine2
		{
			get
			{
				return SiteAddress != null ? String.Format("{0} {1} {2}", SiteAddress.Suburb, SiteAddress.State, SiteAddress.PostCode)
													  : "Not Available";
			}
		}

		[Newtonsoft.Json.JsonIgnore]
		public string StartDateString { get { return StartDate == DateTime.MinValue ? "Not closed yet" : StartDate.ToString("dd-MMM-yyyy"); } }

		[Newtonsoft.Json.JsonIgnore]
		public string InvoicedDateString { get { return InvoicedDate == DateTime.MinValue ? "Yet to Invoice" : InvoicedDate.ToString("dd-MMM-yyyy"); } }

		[Newtonsoft.Json.JsonIgnore]
		public string JobStatusImage
		{
			get
			{
				return string.Format("{0}.png", Status.ToString().ToLower());
			}
		}

		[Newtonsoft.Json.JsonIgnore]
		public string JobTitle
		{
			get { return String.Format("JOB ID {0}", Id.Length >= 6 ? Id.Substring(0, 6).ToUpper() : Id.ToUpper()); }
		}
	}
}
