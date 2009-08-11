using System;
using System.Collections;
using System.Reflection;

namespace HHOnline.Framework
{
    [Serializable]
    public abstract class HHCopy
    {
        private static readonly Hashtable objects = new Hashtable();

        protected object CreateNewInstance()
        {
            ConstructorInfo ci = objects[this.GetType()] as ConstructorInfo;
            if (ci == null)
            {
                ci = this.GetType().GetConstructor(new Type[0]);
                objects[this.GetType()] = ci;
            }

            return ci.Invoke(null);
        }

        public virtual object Copy()
        {
            return CreateNewInstance();
        }
    }
}
