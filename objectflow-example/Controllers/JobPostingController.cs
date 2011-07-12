using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using objectflow_example.Models;
using Rainbow.ObjectFlow.Stateful;
using objectflow_example.ViewModels;

namespace objectflow_example.Controllers
{
	public class JobPostingController : Controller
	{
		private ISession db;
		private IWorkflowMediator<JobPosting> workflow;

		public JobPostingController(ISession db, IWorkflowMediator<JobPosting> workflow)
		{
			this.db = db;
			this.workflow = workflow;
		}

		public ActionResult Index()
		{
			var result = db.QueryOver<JobPosting>().OrderBy(x => x.Name).Asc.List();
			return View(result);
		}

		[HttpGet]
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(JobPosting posting)
		{
			using (var tran = db.BeginTransaction())
			{
				db.Save(posting);
				db.Flush();
				tran.Commit();
			}
			return CreationWorkflow(posting.JobPostingId, null);
		}

		[HttpPost]
		public ActionResult CreationWorkflow(int id, FormCollection collection)
		{
			var posting = db.Get<JobPosting>(id);
			workflow.Start(posting);

			var result = new JobPostingWorkflowViewModel() 
			{ 
				JobPosting = posting,
				NextSteps = workflow.GetPossibleTransitions(posting)
						.Select(x => (JobPosting.CreationSteps)x.To).ToList()
			};
			return View(result);
		}
	}
}
