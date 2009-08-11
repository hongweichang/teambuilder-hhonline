using System;
using System.Collections.Generic;

namespace HHOnline.Framework
{
    /// <summary>
    /// 区域策略
    /// </summary>
    public class RegionStrategy : GeneralStrategy
    {
        public override List<KeyValue> GetValueRange()
        {
            return Areas.GetValueRange();
        }
    }
}
