using System;
using System.Collections;
using System.Text;
using HHOnline.Framework.Providers;

namespace HHOnline.Framework
{
    /// <summary>
    /// 计数管理基类
    /// </summary>
    public abstract class BaseViews
    {
        private Hashtable viewList = new Hashtable();

        public BaseViews()
        { }

        /// <summary>
        /// 增加次数
        /// </summary>
        /// <param name="relatedID"></param>
        public void AddViewCount(int relatedID)
        {
            lock (viewList.SyncRoot)
            {
                ViewCounter v = viewList[relatedID] as ViewCounter;
                if (v == null)
                {
                    v = new ViewCounter(relatedID);
                    viewList.Add(relatedID, v);
                }
                v.Count++;
            }
        }

        /// <summary>
        /// 复制并清空计数值
        /// </summary>
        /// <returns></returns>
        public Hashtable ReQueue()
        {
            Hashtable views = null;
            lock (viewList.SyncRoot)
            {
                views = new Hashtable(viewList);
                viewList.Clear();
            }
            return views;
        }

        /// <summary>
        /// 保持计数值入库
        /// </summary>
        public abstract void SaveQueue();
    }
}
