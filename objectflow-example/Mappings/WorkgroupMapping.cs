using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using objectflow_example.Models;
using FluentNHibernate.Mapping;

namespace objectflow_example.Mappings
{
	public class WorkgroupMapping : ClassMap<Workgroup>
	{
		public WorkgroupMapping()
		{
			Table("Workgroups");
			Id(x => x.WorkgroupId);
			Map(x => x.Name);
		}
	}
}