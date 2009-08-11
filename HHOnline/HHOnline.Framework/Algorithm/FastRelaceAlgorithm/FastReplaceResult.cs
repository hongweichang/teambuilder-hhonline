using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace HHOnline.Framework.Algorithm.FastRelaceAlgorithm
{
    /// <summary>
    /// 快速替换结果
    /// </summary>
    public class FastReplaceResult : IComparable<FastReplaceResult>
    {
        #region -Construtor-
        /// <summary>
        /// 创建<see cref="HHOnline.Framework.Algorithm.FastRelaceAlgorithm.FastRelaceResult"/>的新实例
        /// </summary>
        public FastReplaceResult()
            : this(-1, null)
        { }
        /// <summary>
        /// 创建<see cref="HHOnline.Framework.Algorithm.FastRelaceAlgorithm.FastRelaceResult"/>的新实例
        /// </summary>
        /// <param name="_Index">关键字索引</param>
        /// <param name="_Keyword">关键字键值</param>
        public FastReplaceResult(int _Index, ReplaceKeyValue _KeyValues)
        {
            this._Index = _Index;
            this._KeyValues = _KeyValues;
        }
        /// <summary>
        /// 返回空的<see cref="HHOnline.Framework.Algorithm.FastRelace.FastRelaceResult"/>新实例
        /// </summary>
        public static FastReplaceResult Empty
        {
            get { return new FastReplaceResult(); }
        }
        #endregion

        #region -Properties-
        private int _Index;
        /// <summary>
        /// 索引位置
        /// </summary>
        public int Index
        {
            get { return _Index; }
            set { _Index = value; }
        }
        private ReplaceKeyValue _KeyValues;
        /// <summary>
        /// 关键字键值
        /// </summary>
        public ReplaceKeyValue KeyValues
        {
            get { return _KeyValues; }
            set { _KeyValues = value; }
        }
        #endregion

        #region -IComparable<FastReplaceResult> Member-

        public int CompareTo(FastReplaceResult other)
        {
            return other.Index.CompareTo(this.Index);
        }

        #endregion
    }
}
