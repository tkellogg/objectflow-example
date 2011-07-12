using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using objectflow_example.Models;
using FluentNHibernate.Mapping;

namespace objectflow_example.Mappings
{
	public class PositionMapping : ClassMap<Position>
	{
		public PositionMapping()
		{
			Table("Positions");
			Id(x => x.PositionId);
			Map(x => x.Title);
			Map(x => x.Description);
			References(x => x.Workgroup).Column("WorkgroupId");
		}
	}
}