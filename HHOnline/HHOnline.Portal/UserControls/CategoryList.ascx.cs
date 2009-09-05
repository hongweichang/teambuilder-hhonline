using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using HHOnline.News.Components;
using HHOnline.News.Services;

public partial class UserControls_CategoryList : System.Web.UI.UserControl
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			BindList();
		}
	}

	private void BindList()
	{
		List<ArticleCategory> cates = ArticleManager.GetAllCategories();

		// 首先绑定父亲节点（一级分类）
		List<ArticleCategory> level0Cates = new List<ArticleCategory>();
		foreach (ArticleCategory item in cates)
		{
			// 1为“华宏资讯”分类
			if (item.ParentID == 1)
			{
				level0Cates.Add(item);
			}
		}

		repCategories.DataSource = level0Cates;
		repCategories.DataBind();
	}
}
