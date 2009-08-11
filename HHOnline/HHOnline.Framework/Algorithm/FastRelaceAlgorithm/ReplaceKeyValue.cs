using System;
using System.Collections.Generic;
using System.Text;

namespace HHOnline.Framework.Algorithm.FastRelaceAlgorithm
{
    /// <summary>
    /// 替换字符串键值对
    /// </summary>
    public class ReplaceKeyValue
    {
        /// <summary>
        /// 创建<see cref="HHOnline.Framework.Algorithm.FastRelaceAlgorithm.ReplaceKeyValue"/>的新实例
        /// </summary>
        public ReplaceKeyValue() { }
        /// <summary>
        /// 创建<see cref="HHOnline.Framework.Algorithm.FastRelaceAlgorithm.ReplaceKeyValue"/>的新实例
        /// </summary>
        /// <param name="_Key">匹配键</param>
        /// <param name="_Value">替换值</param>
        public ReplaceKeyValue(string _Key, string _Value)
        {
            this.Key = _Key;
            this.Value = _Value;
        }
        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
    }
}
