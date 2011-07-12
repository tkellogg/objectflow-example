using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using objectflow_example.Models;

namespace objectflow_example.Controllers
{
	public class WorkgroupController : Controller
	{
		private ISession db;

		public WorkgroupController(ISession session)
		{
			this.db = session;
		}

		//
		// GET: /Workgroub/

		public ActionResult Index()
		{
			var result = db.QueryOver<Workgroup>().OrderBy(x => x.WorkgroupName).Asc.List();
			return View(result);
		}

		//
		// GET: /Workgroub/Details/5

		public ActionResult Details(int id)
		{
			return View();
		}

		//
		// GET: /Workgroub/Create

		public ActionResult Create()
		{
			return View();
		} 

		//
		// POST: /Workgroub/Create

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
		// GET: /Workgroub/Edit/5
 
		public ActionResult Edit(int id)
		{
			return View();
		}

		//
		// POST: /Workgroub/Edit/5

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
		// GET: /Workgroub/Delete/5
 
		public ActionResult Delete(int id)
		{
			return View();
		}

		//
		// POST: /Workgroub/Delete/5

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
