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
	private List<ArticleCategory> tempCates;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			BindList();
		}
	}

	private void BindList()
	{
		tempCates = ArticleManager.GetAllCategories();

		// 首先绑定父亲节点（一级分类）
		List<ArticleCategory> catesLevel1 = new List<ArticleCategory>();
		foreach (ArticleCategory item in tempCates)
		{
			// 1为“华宏资讯”分类
			if (item.ParentID == 1)
			{
				catesLevel1.Add(item);
			}
		}

		repCategories.DataSource = catesLevel1;
		repCategories.DataBind();

		// 移除一级分类的节点
		foreach (ArticleCategory item in catesLevel1)
		{
			tempCates.Remove(item);
		}

		// 绑定子分类
		repCategoryLevel1.DataSource = catesLevel1;
		repCategoryLevel1.DataBind();
	}

	protected void repCategoryLevel1_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		ArticleCategory cate = e.Item.DataItem as ArticleCategory;
		
		if (cate != null)
		{
			// 判断是否有父为cate的子节点
			bool isExists = false;
			foreach (ArticleCategory item in tempCates)
			{
				if (item.ParentID.HasValue && item.ParentID.Value == cate.ID)
				{
					isExists = true;
					break;
				}
			}

			if (!isExists)
			{
				e.Item.Visible = false;
			}
			else
			{
				// 获取二级rep
				Repeater rep = null;

				foreach (Control ctl in e.Item.Controls)
				{
					rep = ctl as Repeater;
					if (rep != null)
					{
						break;
					}
				}

				if (rep != null)
				{
					// 查找父为此分类的节点
					List<ArticleCategory> cates = new List<ArticleCategory>();
					foreach (ArticleCategory item in tempCates)
					{
						if (item.ParentID.HasValue && item.ParentID.Value == cate.ID)
						{
							cates.Add(item);
						}
					}

					foreach (ArticleCategory item in cates)
					{
						tempCates.Remove(item);
					}

					rep.DataSource = cates;
					rep.DataBind();
				}
			}
		}
	}
}
