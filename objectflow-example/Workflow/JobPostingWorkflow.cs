using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rainbow.ObjectFlow.Stateful;
using objectflow_example.Models;
using NHibernate;
using System.Threading;
using Rainbow.ObjectFlow.Helpers;

namespace objectflow_example.Workflow
{
	public interface IJobPostingWorkflow : IWorkflowMediator<JobPosting>
	{
		void TransitionTo(JobPosting posting, JobPosting.CreationSteps step);
	}

	public class JobPostingWorkflow : WorkflowMediator<JobPosting>, IJobPostingWorkflow
	{
		private ISession db;

		public JobPostingWorkflow(ISession db)
		{
			this.db = db;
		}

		protected override IStatefulWorkflow<JobPosting> Define()
		{
			var createWorkgroup = Declare.Step();
			var createPosition = Declare.Step();

			var wf = new StatefulWorkflow<JobPosting>();
			wf.Yield(JobPosting.CreationSteps.Begin);
			wf.When((post, dict) => (JobPosting.CreationSteps)dict["next"] == JobPosting.CreationSteps.CreatePosition,
						otherwise: createPosition);
			wf.Do(DoThatThing);

			wf.Define(createWorkgroup);
			wf.Yield(JobPosting.CreationSteps.CreateWorkgroup);

			wf.When(x => false);

			wf.Define(createPosition);
			wf.Yield(JobPosting.CreationSteps.CreatePosition);


			wf.Yield(JobPosting.CreationSteps.CreateJobPosting);
			wf.Yield(JobPosting.CreationSteps.Posted);
			return wf;
		}

		private JobPosting DoThatThing(JobPosting posting)
		{
			posting.Name = "";
			return posting;
		}

		/// <summary>
		/// This is a convenient place to plug in the persistence layer - NHibernate.
		/// This always gets called as the workflow is exiting if no exception occurs.
		/// </summary>
		protected override void OnFinished(JobPosting subject, object from, object to)
		{
			using (var tran = db.BeginTransaction())
			{
				db.SaveOrUpdate(subject);
				db.Flush();
				tran.Commit();
			}
		}

		public void TransitionTo(JobPosting posting, JobPosting.CreationSteps step)
		{
			if (step != posting.CreationStep)
			{
				Start(posting, new { next = step });
			}
		}
	}
}