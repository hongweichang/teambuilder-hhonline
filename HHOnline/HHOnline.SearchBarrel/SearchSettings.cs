using System;
using System.Collections.Generic;
using System.Text;
using HHOnline.Framework;

namespace HHOnline.SearchBarrel
{
    /// <summary>
    /// 产品查询设置
    /// </summary>
    public class ProductSearchSetting : BaseSearchSetting
    {
        public override string SearchName
        {
            get
            {
                if (GlobalSettings.IsNullOrEmpty(base.SearchName))
                    return "产品";
                else
                    return base.SearchName;
            }
            set
            {
                base.SearchName = value;
            }
        }

        public override string IndexFileDirectory
        {
            get
            {
                if (GlobalSettings.IsNullOrEmpty(base.IndexFileDirectory))
                    return "Product";
                else
                    return base.IndexFileDirectory;
            }
            set
            {
                base.IndexFileDirectory = value;
            }
        }

        public override void InitializeIndex(string indexPath)
        {
            ProductSearchManager.InitializeIndex(indexPath);
        }
    }

    /// <summary>
    /// 新闻查询设置
    /// </summary>
    public class NewsSearchSetting : BaseSearchSetting
    {
        public override string SearchName
        {
            get
            {
                if (GlobalSettings.IsNullOrEmpty(base.SearchName))
                    return "资讯";
                else
                    return base.SearchName;
            }
            set
            {
                base.SearchName = value;
            }
        }

        public override string IndexFileDirectory
        {
            get
            {
                if (GlobalSettings.IsNullOrEmpty(base.IndexFileDirectory))
                    return "News";
                else
                    return base.IndexFileDirectory;
            }
            set
            {
                base.IndexFileDirectory = value;
            }
        }

        public override void InitializeIndex(string indexPath)
        {
            NewsSearchManager.InitializeIndex(indexPath);
        }
    }
}
