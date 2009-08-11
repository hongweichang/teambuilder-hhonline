using System;
using System.Collections.Generic;
using System.Text;

namespace HHOnline.Framework.Algorithm.FastRelaceAlgorithm
{ 
    /// <summary>
    /// 文本快速替换接口
    /// </summary>
    public interface IFastRelace
    {
        /// <summary>
        /// 待替换字符串键值对
        /// </summary>
        ReplaceKeyValue[] KeyValues { get; set; }
        /// <summary>
        /// 查找所有与关键字匹配的结果
        /// </summary>
        /// <param name="text">待查找文本</param>
        /// <returns></returns>
        List<FastReplaceResult> FindAll(string text);
        /// <summary>
        /// 替换文本内容
        /// </summary>
        /// <param name="text">待替换文本</param>
        string ReplaceAll(string text);
        /// <summary>
        /// 查找第一条与关键字匹配的结果
        /// </summary>
        /// <param name="text">待查找文本</param>
        /// <returns></returns>
        FastReplaceResult FindFirst(string text);
        /// <summary>
        /// 是否存在能被替换的关键字
        /// </summary>
        /// <param name="text">待查找文本</param>
        /// <returns></returns>
        bool ContainsKey(string text);
    }
}
