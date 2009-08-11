using System;
using System.Collections;
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
using System.Collections.Generic;
using HHOnline.Framework;
using HHOnline.Framework.Web;
using HHOnline.Cache;

public partial class ControlPanel_News_Category : HHPage
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack && !IsCallback)
		{
			BindCategories();
			//BindLinkButton();
		}
	}

	protected void btnChildCategory_Click(object sender, EventArgs e)
	{
		LinkButton btn = sender as LinkButton;
		int categoryID = int.Parse(btn.Attributes["CategoryID"]);

		CheckNode(tvwCategory.Nodes, categoryID.ToString());
		SetValue();
		LoadArticleCategory(categoryID);
		LoadChildArticles(categoryID);
	}

	private void LoadArticleCategory(int categoryID)
	{
		rpChildArticle.DataSource = ArticleManager.GetChildCategories(categoryID);
		rpChildArticle.DataBind();
	}

	/// <summary>
	/// 添加子节点
	/// </summary>
	/// <param name="node"></param>
	void LoadChild(TreeNode node)
	{
		int parentID = int.Parse(node.Value);
		List<ArticleCategory> categories = ArticleManager.GetChildCategories(parentID);
		if (categories != null && categories.Count > 0)
		{
			foreach (ArticleCategory info in categories)
			{
				TreeNode newNode = MakeCategoryNode(info);

				LoadChild(newNode);
				node.ChildNodes.Add(newNode);
			}
		}
	}

	/// <summary>
	/// 创建分类节点
	/// </summary>
	/// <param name="info"></param>
	/// <returns></returns>
	private TreeNode MakeCategoryNode(ArticleCategory info)
	{
		TreeNode result = new TreeNode(
			info.Name,
			info.ID.ToString(),
			GlobalSettings.RelativeWebRoot + "images/default/cat.gif");

		result.ToolTip = info.Description;

		return result;
	}

	private static string nodeState = CacheKeyManager.PagePrefix + "SelectedArticleCategoryValue/";

	/// <summary>
	/// 绑定分类集合
	/// </summary>
	private void BindCategories()
	{
		List<ArticleCategory> categories = ArticleManager.GetAllCategories();

		foreach (ArticleCategory info in categories)
		{
			if (info.ParentID == null || info.ParentID == 0)
			{
				TreeNode newNode = MakeCategoryNode(info);
				newNode.ExpandAll();

				LoadChild(newNode);

				tvwCategory.Nodes.Add(newNode);
			}
		}

		HttpCookie cache = HHCookie.GetCookie(nodeState + Profile.AccountInfo.UserName);
		if (cache != null)
		{
			CheckNode(tvwCategory.Nodes, cache.Value);
			SetValue();
			LoadChildArticles(int.Parse(cache.Value));
		}

		if (tvwCategory.Nodes.Count != 0)
		{
			HttpCookie hhcache = HHCookie.GetCookie(nodeState + Profile.AccountInfo.UserName);
			if (hhcache != null)
			{
				CheckNode(tvwCategory.Nodes, hhcache.Value);
				SetValue();
				//BindUserDept(int.Parse(hhcache.Value));
			}
			else
			{
				tvwCategory.Nodes[0].Selected = true;
			}

			tvwCategory_SelectedNodeChanged(tvwCategory, null);
		}
	}

	void CheckNode(TreeNodeCollection nodes, string value)
	{
		if (nodes != null && nodes.Count > 0)
		{
			foreach (TreeNode root in nodes)
			{
				if (value == root.Value)
				{
					root.Select();
					ExpandParent(root);
				}
				else
				{
					CheckNode(root.ChildNodes, value);
				}
			}
		}
	}

	void ExpandParent(TreeNode tn)
	{
		TreeNode node = tn;
		while (node != null && node.Parent != null)
		{
			node = node.Parent;
			if (node != null)
			{
				node.Expand();
			}
		}
	}

	void SetValue()
	{
		ExecuteJs("window.$selectNodeId=" + tvwCategory.SelectedValue, false);
	}

	/// <summary>
	/// 载入文章
	/// </summary>
	/// <param name="id"></param>
	void LoadChildArticles(int id)
	{
		//UserQuery q = new UserQuery();
		//q.OrganizationID = orgId;
		//q.PageSize = Int32.MaxValue;
		//PagingDataSet<User> pds = Users.GetUsers(q, false);
		//rpUsers.DataSource = pds.Records;
		//rpUsers.DataBind();

		//ArticleQuery query = new ArticleQuery();
		//query.CategoryID = id;
		//query.PageSize = Int32.MaxValue;

		//PagingDataSet<Article> pds = ArticleManager.GetArticles(query);
		//rpArticles.DataSource = pds.Records;
		//rpArticles.DataBind();

		LoadArticleCategory(id);
	}

	protected void tvwCategory_SelectedNodeChanged(object sender, EventArgs e)
	{
		TreeNode node = tvwCategory.SelectedNode;
        node.Expand();
		HHCookie.AddCookie(nodeState + Profile.AccountInfo.UserName, node.Value, DateTime.Now.AddMinutes(1));

		LoadChildArticles(int.Parse(node.Value));
		SetValue();
	}

	public override void OnPageLoaded()
	{
		this.PageInfoType = HHOnline.Framework.Web.Enums.InfoType.IframeInfo;
		base.OnPageLoaded();
		
		AddJavaScriptInclude("scripts/jquery.jmodal.js", false, true);
		AddJavaScriptInclude("scripts/pages/category.aspx.js", false, false);
	}

	protected override void OnPermissionChecking(PermissionCheckingArgs e)
	{
		this.PagePermission = "NewsCategoryModule-View";
		e.CheckPermissionControls.Add("NewsCategoryModule-Add", btnAddCategory);
		e.CheckPermissionControls.Add("NewsCategoryModule-Delete", btnDeleteCategory);
		//e.CheckPermissionControls.Add("ArticleModule-Add", btnAddArticle);
		//e.CheckPermissionControls.Add("ArticleModule-Delete", btnDeleteArticle);

		base.OnPermissionChecking(e);
	}
}
