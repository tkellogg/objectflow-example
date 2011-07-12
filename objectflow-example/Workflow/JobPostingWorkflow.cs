using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rainbow.ObjectFlow.Stateful;
using objectflow_example.Models;
using NHibernate;

namespace objectflow_example.Workflow
{
	public class JobPostingWorkflow : WorkflowMediator<JobPosting>
	{
		private ISession db;

		public JobPostingWorkflow(ISession db)
		{
			this.db = db;
		}

		protected override IStatefulWorkflow<JobPosting> Define()
		{
			var wf = new StatefulWorkflow<JobPosting>();
			wf.Yield(JobPosting.CreationSteps.Begin);
			wf.Yield(JobPosting.CreationSteps.CreateJobPosting);
			wf.Yield(JobPosting.CreationSteps.CreatePosition);
			wf.Yield(JobPosting.CreationSteps.CreateWorkgroup);
			wf.Yield(JobPosting.CreationSteps.Posted);
			return wf;
		}

		protected override void OnFinished(JobPosting subject, object from, object to)
		{
			using (var tran = db.BeginTransaction())
			{
				db.SaveOrUpdate(subject);
				db.Flush();
				tran.Commit();
			}
		}
	}
}