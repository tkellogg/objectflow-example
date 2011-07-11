using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace objectflow_example.Models
{
	public class JobPosting
	{
		public virtual int JobPostingId { get; set; }
		public virtual string Name { get; set; }
		public virtual Position Position { get; set; }
	}
}