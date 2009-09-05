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
using HHOnline.News.Services;
using HHOnline.News.Components;

public partial class UserControls_ArticleSearch : System.Web.UI.UserControl
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			// 绑定资讯列表
			BindTreeList();
		}
	}

	/// <summary>
	/// 绑定资讯列表
	/// </summary>
	private void BindTreeList()
	{
		ddlSearchType.Items.Clear();

		List<ArticleCategory> cates = ArticleManager.GetAllCategories();
		BindTreeItem(cates, null, 0);
	}

	private void BindTreeItem(List<ArticleCategory> cates, int? parentID, int level)
	{
		string prefix = string.Empty;
		if (level != 0)
		{
			for (int n = 0; n < level; ++n)
			{
				prefix += "　";
			}
		}

		foreach (ArticleCategory item in cates)
		{
			if (item.ParentID == parentID)
			{
				ListItem li = new ListItem(prefix + item.Name, item.ID.ToString());
				ddlSearchType.Items.Add(li);
				BindTreeItem(cates, item.ID, level + 1);
			}
		}
	}
}
