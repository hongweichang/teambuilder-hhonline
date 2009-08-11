using System;
using System.Collections.Generic;
using System.Text;

namespace HHOnline.Framework
{
    /// <summary>
    /// 级别策略接口
    /// </summary>
    public interface IGradeStrategy
    {
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        object Value { get; set; }

        /// <summary>
        /// 生成SQL
        /// </summary>
        /// <returns></returns>
        string BuildQuery();

        /// <summary>
        /// 获取取值范围
        /// </summary>
        List<KeyValue> GetValueRange();
    }
}
