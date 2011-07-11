using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace objectflow_example.Models
{
	public class Position
	{
		public virtual int PositionId { get; set; }
		public virtual string Title { get; set; }
		public virtual string Description { get; set; }
		public virtual Workgroup Workgroup { get; set; }
	}
}