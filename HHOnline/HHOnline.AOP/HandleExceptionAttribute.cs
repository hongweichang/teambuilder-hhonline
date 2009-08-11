using System;
using System.Collections.Generic;
using System.Text;
using PostSharp.Laos;
using System.Diagnostics;

namespace HHOnline.AOP
{
    [Serializable]
    public class HandleExceptionAttribute : OnMethodBoundaryAspect
    {
        public override void OnException(MethodExecutionEventArgs eventArgs)
        {
            base.OnException(eventArgs);
            Debug.Assert(false, eventArgs.Exception.Message);
            eventArgs.ReturnValue = null;
            eventArgs.FlowBehavior = FlowBehavior.Return;
        }
    }
}
