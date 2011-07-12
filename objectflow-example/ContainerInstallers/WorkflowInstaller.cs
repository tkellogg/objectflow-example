using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.Registration;
using Rainbow.ObjectFlow.Stateful;

namespace objectflow_example.ContainerInstallers
{
	public class WorkflowInstaller : IWindsorInstaller
	{
		#region IWindsorInstaller Members

		public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
		{
			container.Register(
				AllTypes.FromThisAssembly()
					.BasedOn(typeof(IWorkflowMediator<>))
					.WithService.FirstInterface()
					.WithService.DefaultInterface()
			);
		}

		#endregion
	}
}