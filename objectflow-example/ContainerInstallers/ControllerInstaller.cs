using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using System.Web.Mvc;

namespace objectflow_example.ContainerInstallers
{
	public class ControllerInstaller : IWindsorInstaller
	{
		#region IWindsorInstaller Members

		public void Install(IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
		{
			container.Register(
				AllTypes.FromThisAssembly()
					.BasedOn<IController>()
					.WithService.Self()
					.Configure(x => x.LifeStyle.Transient)
			);
		}

		#endregion
	}
}