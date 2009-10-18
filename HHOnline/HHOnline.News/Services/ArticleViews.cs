using System;
using System.Collections;
using HHOnline.Framework;
using HHOnline.News.Providers;

namespace HHOnline.News
{
    /// <summary>
    /// 咨询计数管理类
    /// </summary>
    public class ArticleViews : BaseViews
    {
        /// <summary>
        /// 清空计数并更新数据库
        /// </summary>
        public override void SaveQueue()
        {
            Hashtable viewList = ReQueue();
            ArticleProvider.Instance.SaveViewList(viewList);
        }
    }
}
