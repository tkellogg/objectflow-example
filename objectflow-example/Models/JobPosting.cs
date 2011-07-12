using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace objectflow_example.Models
{
	public class JobPosting : Rainbow.ObjectFlow.Stateful.IStatefulObject
	{

		public JobPosting()
		{
			CreationStep = CreationSteps.Begin;
		}

		public virtual int JobPostingId { get; set; }
		public virtual string Name { get; set; }
		public virtual Position Position { get; set; }
		public virtual CreationSteps CreationStep { get; set; }

		public virtual bool IsPosted 
		{ 
			get { return CreationStep == CreationSteps.Posted; } 
		}

		#region IStatefulObject Members

		public enum CreationSteps 
		{
			Begin,
			CreateWorkgroup,
			CreatePosition,
			CreateJobPosting,
			Posted
		}

		public virtual object GetStateId(object workflowId)
		{
			return CreationStep;
		}

		public virtual void SetStateId(object workflowId, object stateId)
		{
			CreationStep = (CreationSteps)stateId;
		}

		#endregion
	}
}