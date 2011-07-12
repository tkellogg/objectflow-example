using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using objectflow_example.Models;

namespace objectflow_example.ViewModels
{
	public class JobPostingWorkflowViewModel
	{
		public JobPosting JobPosting { get; set; }
		public IList<JobPosting.CreationSteps> NextSteps { get; set; }
	}
}