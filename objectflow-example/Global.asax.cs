﻿using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using objectflow_example.Plumbing;

namespace objectflow_example
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication, IContainerAccessor
	{
		private static WindsorContainer container;
		private NHibernate.ISessionFactory sessionFactory;

		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
			);

		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
			sessionFactory = Fluently.Configure()
				.Database(
					MsSqlConfiguration.MsSql2008
					.ConnectionString(c => c.Server(@".\MSSQL2008R2")
											.Database("objectflow")
											.Username("objectflow")
											.Password("password"))
					.DefaultSchema("dbo")
				)
				.Mappings(x => x.FluentMappings.AddFromAssemblyOf<MvcApplication>())
				.BuildSessionFactory();
			BootstrapContainer();

		}

		private void BootstrapContainer()
		{
			container = new WindsorContainer();
			container.Install(FromAssembly.This());
			var controllerFactory = new WindsorControllerFactory(container.Kernel);
			ControllerBuilder.Current.SetControllerFactory(controllerFactory);
			container.Register(
				Component.For<NHibernate.ISession>()
					.UsingFactoryMethod(() => sessionFactory.GetCurrentSession())
					.LifeStyle.Transient
			);
		}

		#region IContainerAccessor Members

		public IWindsorContainer Container
		{
			get { return container; }
		}

		#endregion
	}
}