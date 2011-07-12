using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using objectflow_example.Models;

namespace objectflow_example.Controllers
{
	public class PositionController : Controller
	{
		private ISession db;
		
		public PositionController(ISession db)
		{
			this.db = db;
		}

		public ActionResult Index()
		{
			var result = db.QueryOver<Position>().OrderBy(x => x.Title);
			return View(result);
		}

		//
		// GET: /Position/Details/5

		public ActionResult Details(int id)
		{
			return View();
		}

		//
		// GET: /Position/Create

		public ActionResult Create()
		{
			return View();
		} 

		//
		// POST: /Position/Create

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
		// GET: /Position/Edit/5
 
		public ActionResult Edit(int id)
		{
			return View();
		}

		//
		// POST: /Position/Edit/5

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
		// GET: /Position/Delete/5
 
		public ActionResult Delete(int id)
		{
			return View();
		}

		//
		// POST: /Position/Delete/5

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
