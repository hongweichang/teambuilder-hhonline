using System;
using System.Collections.Generic;
using System.Text;

namespace HHOnline.Framework
{
    /// <summary>
    /// 计数工厂类
    /// </summary>
    public class ViewsFactory
    {
        private ViewsFactory() { }

        private static Dictionary<Type, BaseViews> dicViews = new Dictionary<Type, BaseViews>();

        private static object locker = new object();

        /// <summary>
        /// 获取计数管理类
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static BaseViews GetViews(Type type)
        {
            lock (locker)
            {
                if (dicViews.ContainsKey(type))
                    return dicViews[type];
                BaseViews view = Activator.CreateInstance(type) as BaseViews;
                dicViews[type] = view;
                return view;
            }
        }

        /// <summary>
        /// 将所有计数器清空并更新数据库
        /// </summary>
        public static void SaveQueue()
        {
            lock (locker)
            {
                foreach (KeyValuePair<Type, BaseViews> pair in dicViews)
                {
                    pair.Value.SaveQueue();
                }
            }
        }
    }
}
