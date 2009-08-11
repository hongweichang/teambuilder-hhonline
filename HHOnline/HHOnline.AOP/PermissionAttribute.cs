using System;
using System.Collections.Generic;
using System.Text;
using PostSharp.Laos;

namespace HHOnline.AOP
{
	[Serializable]
	public class PermissionAttribute : OnMethodBoundaryAspect
	{
		public override void OnEntry(MethodExecutionEventArgs eventArgs)
		{
			eventArgs.FlowBehavior = FlowBehavior.Return;
		}

		public override void OnException(MethodExecutionEventArgs eventArgs)
		{
			base.OnException(eventArgs);
		}

		public override void OnExit(MethodExecutionEventArgs eventArgs)
		{
			base.OnExit(eventArgs);
		}

		public override void OnSuccess(MethodExecutionEventArgs eventArgs)
		{
			base.OnSuccess(eventArgs);
		}
	}
}
