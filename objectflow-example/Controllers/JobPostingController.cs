using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using objectflow_example.Models;
using Rainbow.ObjectFlow.Stateful;
using objectflow_example.ViewModels;
using objectflow_example.Workflow;

namespace objectflow_example.Controllers
{
	public class JobPostingController : Controller
	{
		private ISession db;
		private IJobPostingWorkflow workflow;

		public JobPostingController(ISession db, IJobPostingWorkflow workflow)
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
			db.Save(posting);
			db.Flush();
			var result = MapToViewModel(posting);
			return RedirectToAction("CreationWorkflow", new { id = posting.JobPostingId });
		}

		[HttpGet]
		public ActionResult CreationWorkflow(int id)
		{
			var posting = db.Get<JobPosting>(id);
			var result = MapToViewModel(posting);
			return View(result);
		}

		private JobPostingWorkflowViewModel MapToViewModel(JobPosting posting)
		{
			return new JobPostingWorkflowViewModel()
			{
				JobPosting = posting,
				PostingName = posting != null ? posting.Name : null,
				NextSteps = workflow.GetPossibleTransitions(posting)
						.Select(x => (JobPosting.CreationSteps)x.To).ToList()
			};
		}

		[HttpPost]
		public ActionResult CreationWorkflow(int id, string actionId, FormCollection collection)
		{
			var action = (JobPosting.CreationSteps)Enum.Parse(typeof(JobPosting.CreationSteps), actionId);
			var posting = db.Get<JobPosting>(id);
			workflow.TransitionTo(posting, action);
			var result = MapToViewModel(posting);
			return View(result);
		}
	}
}
