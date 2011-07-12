using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using objectflow_example.Models;
using FluentNHibernate.Mapping;

namespace objectflow_example.Mappings
{
	public class JobPostingMapping : ClassMap<JobPosting>
	{
		public JobPostingMapping()
		{
			Table("JobPostings");
			Id(x => x.JobPostingId);
			Map(x => x.Name);
			References(x => x.Position).Column("PositionId");
			Map(x => x.CreationStep).CustomType<int>();
		}
	}
}