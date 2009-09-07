using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.News.Components;
using HHOnline.News.Services;

public partial class UserControls_ArticleCategoryCombo : System.Web.UI.UserControl
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
	/// 是否显示全部资讯
	/// </summary>
	public bool IsShowAllCategory
	{
		get
		{
			if (ddlArticleCategory.Items.Count == 0)
			{
				return false;
			}
			else
			{
				return ddlArticleCategory.Items[0].Value == "-1";
			}
		}

		set
		{
			if (!IsShowAllCategory)
			{
				ListItem li = new ListItem("全部", "-1");
				ddlArticleCategory.Items.Add(li);
			}
		}
	}

	/// <summary>
	/// 绑定资讯列表
	/// </summary>
	private void BindTreeList()
	{
		//ddlArticleCategory.Items.Clear();

		List<ArticleCategory> cates = ArticleManager.GetAllCategories();
		BindTreeItem(cates, null, 0);
	}

	/// <summary>
	/// 选中的分类ID
	/// </summary>
	public int SelectedCategoryID
	{
		get
		{
			return int.Parse(ddlArticleCategory.SelectedValue);
		}

		set
		{
			ddlArticleCategory.SelectedValue = value.ToString();
		}
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
				ddlArticleCategory.Items.Add(li);
				BindTreeItem(cates, item.ID, level + 1);
			}
		}
	}
}
