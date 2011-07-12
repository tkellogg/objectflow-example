using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using objectflow_example.Models;

namespace objectflow_example.Controllers
{
	public class JobPostingController : Controller
	{
		private ISession db;

		public JobPostingController(ISession db)
		{
			this.db = db;
		}

		public ActionResult Index(int parentId)
		{
			var group = db.QueryOver<JobPosting>().Where(x => x.Position.PositionId == parentId);

			return View();
		}

		//
		// GET: /JobPosting/Details/5

		public ActionResult Details(int id)
		{
			return View();
		}

		//
		// GET: /JobPosting/Create

		public ActionResult Create()
		{
			return View();
		} 

		//
		// POST: /JobPosting/Create

		[HttpPost]
		public ActionResult Create(FormCollection collection)
		{
			try
			{
				// TODO: Add insert logic here

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}
		
		//
		// GET: /JobPosting/Edit/5
 
		public ActionResult Edit(int id)
		{
			return View();
		}

		//
		// POST: /JobPosting/Edit/5

		[HttpPost]
		public ActionResult Edit(int id, FormCollection collection)
		{
			try
			{
				// TODO: Add update logic here
 
				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		//
		// GET: /JobPosting/Delete/5
 
		public ActionResult Delete(int id)
		{
			return View();
		}

		//
		// POST: /JobPosting/Delete/5

		[HttpPost]
		public ActionResult Delete(int id, FormCollection collection)
		{
			try
			{
				// TODO: Add delete logic here
 
				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}
	}
}
