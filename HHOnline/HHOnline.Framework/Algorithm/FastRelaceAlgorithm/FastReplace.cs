using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace HHOnline.Framework.Algorithm.FastRelaceAlgorithm
{
    /// <summary>
    /// 快速替换算法
    /// </summary>
    public class FastReplace:IFastRelace
    {
        #region -Constructor-
        /// <summary>
        /// 创建<see cref="HHOnline.Framework.Algorithm.FastRelaceAlgorithm.FastReplace"/>的新实例
        /// </summary>
        public FastReplace() { }
        /// <summary>
        /// 创建<see cref="HHOnline.Framework.Algorithm.FastRelaceAlgorithm.FastReplace"/>的新实例
        /// </summary>
        /// <param name="_Keywords">待匹配关键字</param>
        public FastReplace(ReplaceKeyValue[] _KeyValues)
        {
            this.KeyValues = _KeyValues;
        }
        #endregion

        #region -Search Tree-
        /// <summary>
        /// 构建搜索树
        /// </summary>
        class SearchTree
        {
            #region -Constructor-
            /// <summary>
            /// 构建搜索树<see cref="SearchTree"/>的新实例
            /// </summary>
            public SearchTree()
                : this(null, ' ')
            { }
            /// <summary>
            /// 构建搜索树<see cref="SearchTree"/>的新实例
            /// </summary>
            /// <param name="_Parent">母节点</param>
            /// <param name="_Char">字</param>
            public SearchTree(SearchTree _Parent, char _Char)
            {
                this._Char = _Char;
                this._Parent = _Parent;
                _Results = new ArrayList();
                _ResultsArray = new ReplaceKeyValue[] { };
                _Transitions = new SearchTree[] { };
                _TransHash = new Hashtable();
            }
            #endregion

            #region -Methods-
            /// <summary>
            /// 新增匹配结果
            /// </summary>
            /// <param name="result"></param>
            public void AddResult(ReplaceKeyValue result)
            {
                if (_Results.Contains(result)) return;
                _Results.Add(result);
                _ResultsArray = (ReplaceKeyValue[])_Results.ToArray(typeof(ReplaceKeyValue));
            }

            /// <summary>
            /// 新增节点
            /// </summary>
            /// <param name="node"></param>
            public void AddTransition(SearchTree node)
            {
                _TransHash.Add(node.Char, node);
                SearchTree[] st = new SearchTree[_TransHash.Values.Count];
                _TransHash.Values.CopyTo(st, 0);
                _Transitions = st;
            }


            /// <summary>
            /// 获取节点
            /// </summary>
            /// <param name="_Char"></param>
            /// <returns></returns>
            public SearchTree GetTransition(char _Char)
            {
                return (SearchTree)_TransHash[_Char];
            }

            /// <summary>
            /// 检查节点是否存在
            /// </summary>
            /// <param name="_Char"></param>
            /// <returns></returns>
            public bool Contains(char _Char)
            {
                return GetTransition(_Char) != null;
            }

            #endregion

            #region -Properties-
            private char _Char;
            private SearchTree _Parent;
            private SearchTree _Failure;
            private ArrayList _Results;
            private SearchTree[] _Transitions;
            private ReplaceKeyValue[] _ResultsArray;
            private Hashtable _TransHash;

            /// <summary>
            /// 匹配字
            /// </summary>
            public char Char
            {
                get { return _Char; }
            }
            /// <summary>
            /// 母节点
            /// </summary>
            public SearchTree Parent
            {
                get { return _Parent; }
            }
            /// <summary>
            /// 匹配失败 - 子节点
            /// </summary>
            public SearchTree Failure
            {
                get { return _Failure; }
                set { _Failure = value; }
            }
            /// <summary>
            /// 匹配成功 - 子节点
            /// </summary>
            public SearchTree[] Transitions
            {
                get { return _Transitions; }
            }
            /// <summary>
            /// 匹配结果
            /// </summary>
            public ReplaceKeyValue[] Results
            {
                get { return _ResultsArray; }
            }
            #endregion
        }
        #endregion

        #region -Private Properties-
        private SearchTree _Root;
        private ReplaceKeyValue[] _KeyValues;
        #endregion

        #region -Methods-
        void BuildTree()
        {
            _Root = new SearchTree();

            #region -Get Transions-
            foreach (ReplaceKeyValue rkv in _KeyValues)
            {
                SearchTree st = _Root;
                foreach (char c in rkv.Key)
                {
                    SearchTree stNew = null;
                    foreach (SearchTree s in st.Transitions)
                    {
                        if (s.Char == c)
                        {
                            stNew = s; break;
                        }
                    }

                    if (stNew == null)
                    {
                        stNew = new SearchTree(st, c);
                        st.AddTransition(stNew);
                    }
                    st = stNew;
                }
                st.AddResult(rkv);
            }
            #endregion

            #region -Find Failures-
            ArrayList nodes = new ArrayList();
            foreach (SearchTree st in _Root.Transitions)
            {
                st.Failure = _Root;
                foreach (SearchTree trans in st.Transitions)
                {
                    nodes.Add(trans);
                }
            }
            // 广度搜索算法(BFS)
            while (nodes.Count != 0)
            {
                ArrayList newNodes = new ArrayList();
                foreach (SearchTree st in nodes)
                {
                    SearchTree f = st.Parent.Failure;
                    char c = st.Char;

                    while (f != null && !f.Contains(c)) f = f.Failure;
                    if (f == null)
                        st.Failure = _Root;
                    else
                    {
                        st.Failure = f.GetTransition(c);
                        foreach (ReplaceKeyValue result in st.Failure.Results)
                        {
                            st.AddResult(result);
                        }
                    }
                    foreach (SearchTree child in st.Transitions)
                    {
                        newNodes.Add(child);
                    }
                }
                nodes = newNodes;
            }
            #endregion

            _Root.Failure = _Root;
        }
        #endregion

        #region -Fields From Interface-
        /// <summary>
        /// 待替换键值对
        /// </summary>
        public ReplaceKeyValue[] KeyValues
        {
            get { return _KeyValues; }
            set
            {
                _KeyValues = value;
                BuildTree();
            }
        }
        /// <summary>
        /// 替换文本内容
        /// </summary>
        /// <param name="text">待替换文本</param>
        public string ReplaceAll(string text)
        {
            List<FastReplaceResult> results = FindAll(text);
            results.Sort();
            string temp = text;
            foreach (FastReplaceResult r in results)
            {
                temp = temp.Remove(r.Index, r.KeyValues.Key.Length).Insert(r.Index, r.KeyValues.Value);
            }
            return temp;
        }
        /// <summary>
        /// 查找所有匹配结果
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public List<FastReplaceResult> FindAll(string text)
        {
            List<FastReplaceResult> results=new List<FastReplaceResult>();
            SearchTree st = _Root;
            int index = 0;

            while (index < text.Length)
            {
                SearchTree node = null;
                while (node == null)
                {
                    node = st.GetTransition(text[index]);
                    if (st == _Root) break;
                    if (node == null) st = st.Failure;
                }
                if (node != null) st = node;

                foreach (ReplaceKeyValue found in st.Results)
                {
                    results.Add(new FastReplaceResult(index - found.Key.Length + 1, found));
                }
                index++;
            }
            return results;
        }
        /// <summary>
        /// 查找首个匹配结果
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public FastReplaceResult FindFirst(string text)
        {
            SearchTree st = _Root;
            int index = 0;
            while (index < text.Length)
            {
                SearchTree node = null;
                while (node == null)
                {
                    node = st.GetTransition(text[index]);
                    if (node == null) st = st.Failure;
                    if (st == node) break;
                }
                if (node != null)
                    st = node;
                foreach (ReplaceKeyValue found in st.Results)
                {
                    return new FastReplaceResult(index - found.Key.Length + 1, found);
                }
                index++;
            }
            return FastReplaceResult.Empty;
        }
        /// <summary>
        /// 是否包含匹配结果
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool ContainsKey(string text)
        {
            SearchTree st = _Root;
            int index = 0;
            while (index < text.Length)
            {
                SearchTree node = null;
                while (node == null)
                {
                    node = st.GetTransition(text[index]);
                    if (node == null) st = st.Failure;
                    if (st == node) break;
                }
                if (node != null)
                    st = node;
                if (st.Results.Length > 0)
                    return true;
                index++;
            }
            return false;
        }
        #endregion
    }
}
