using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace objectflow_example.Models
{
	public class Workgroup
	{
		public virtual int WorkgroupId { get; set; }
		public virtual string Name { get; set; }
	}
}