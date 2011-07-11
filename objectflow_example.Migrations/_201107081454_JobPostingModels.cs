using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentMigrator;

namespace objectflow_example.Migrations
{
	[Migration(201107081454)]
	public class _201107081454_JobPostingModels : Migration
	{
		public override void Down()
		{
			Delete.Table("Workgroups");
			Delete.Table("Positions");
			Delete.Table("JobPostings");
		}

		public override void Up()
		{
			Create.Table("Workgroups")
				.WithColumn("WorkgroupId").AsInt32().Identity().PrimaryKey()
				.WithColumn("WorkgroupName").AsString(255);

			Create.Table("Positions")
				.WithColumn("PositionId").AsInt32().Identity().PrimaryKey()
				.WithColumn("Title").AsString(255)
				.WithColumn("Description").AsString()
				.WithColumn("WorkgroupId").AsInt32()
				.ForeignKey().References(null, "Workgroups", new[]{ "WorkgroupId" });

			Create.Table("JobPostings")
				.WithColumn("JobPostingId").AsInt32().Identity().PrimaryKey()
				.WithColumn("Name").AsString(255)
				.WithColumn("PositionId").AsInt32()
				.ForeignKey().References(null, "Positions", new[] { "PositionId" });
		}
	}
}