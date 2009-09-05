using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.Security;
using HHOnline.Cache;
using HHOnline.Framework.Providers;


namespace HHOnline.Framework
{
    /// <summary>
    /// 查询管理类
    /// </summary>
    public class WordSearchManager
    {
        /// <summary>
        /// 查询关键字入库
        /// </summary>
        /// <param name="keyword"></param>
        public void Insert(string keyword)
        {
            CommonDataProvider.Instance.InsertWordSearch(keyword);
        }
    }
}
