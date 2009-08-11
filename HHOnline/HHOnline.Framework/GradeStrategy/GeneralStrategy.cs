using System;
using System.Collections.Generic;

namespace HHOnline.Framework
{
    /// <summary>
    /// 通用策略
    /// </summary>
    public abstract class GeneralStrategy : IGradeStrategy
    {
        public virtual string Name
        {
            get;
            set;
        }

        public virtual string Text
        {
            get;
            set;
        }

        public virtual object Value
        {
            get;
            set;
        }

        public virtual string BuildQuery()
        {
            return Name + " = " + Value;
        }

        public abstract List<KeyValue> GetValueRange();
    }
}
