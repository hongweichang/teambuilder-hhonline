<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Category.aspx.cs" Inherits="ControlPanel_News_Category"
	Title="无标题页" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>ArticleCategory</title>
</head>
<body>
	<form id="form1" runat="server">
	<table class="treelist" cellpadding="0" cellspacing="5">
		<tr>
			<td class="lefttree" valign="top">
				<div class="row1">
					资讯分类列表</div>
				<div class="row2">
					<asp:TreeView ID="tvwCategory" runat="server" ShowLines="true" OnSelectedNodeChanged="tvwCategory_SelectedNodeChanged">
						<SelectedNodeStyle BackColor="#000080" HorizontalPadding="5" ForeColor="White" />
						<NodeStyle HorizontalPadding="5" />
					</asp:TreeView>
				</div>
			</td>
			<td class="righttree" valign="top">
				<div class="row1">
					<ul>
						<li>
							<asp:LinkButton ID="btnAddCategory" runat="server" CssClass="C-AddCat" Text="新增分类"
								OnClientClick='return AddCategory(window.$selectNodeId);'></asp:LinkButton></li>
						<li>
							<asp:LinkButton ID="btnDeleteCategory" runat="server" CssClass="C-DeleteCat" Text="批量删除分类"
								OnClientClick='return DeleteCategory();'></asp:LinkButton></li>
						<%--<li>
							<asp:LinkButton ID="btnAddArticle" CssClass="C-AddNews" runat="server" Text="新增文章"
								OnClientClick='return AddArticle();'></asp:LinkButton></li>--%>
						<%--<li>
							<asp:LinkButton ID="btnDeleteArticle" CssClass="C-DeleteNews" runat="server" Text="批量删除文章"
								OnClientClick='return DeleteArticle();'></asp:LinkButton></li>--%>
					</ul>
				</div>
				<div class="row2" id="childList">
					<h2 class="row2-cat">
						分类信息列表</h2>
					<asp:Repeater ID="rpChildArticle" runat="server">
						<HeaderTemplate>
							<table>
						</HeaderTemplate>
						<ItemTemplate>
							<tr>
								<td class="cat-c1">
									<input type="checkbox" rel="child-cat" value='<%# Eval("ID") %>' />
								</td>
								<td class="cat-c2">
									<asp:LinkButton ID="btnChildCategory" runat="server" OnClick="btnChildCategory_Click"
										ToolTip='<%# Eval("Description") %>' PostBackUrl="#" CategoryID='<%# Eval("ID") %>'><%# Eval("Name")%></asp:LinkButton>
								</td>
								<td class="cat-c3" catid='<%# Eval("ID") %>'>
									<asp:LoginView ID="lv1" runat="server">
										<RoleGroups>
											<asp:RoleGroup Roles="NewsCategoryModule-Edit">
												<ContentTemplate>
													<a href="javascript:void(0)" title="编辑分类" class="edit opts" onclick="return EditCategory(this)">
													</a>
												</ContentTemplate>
											</asp:RoleGroup>
										</RoleGroups>
									</asp:LoginView>
									<asp:LoginView ID="LoginView1" runat="server">
										<RoleGroups>
											<asp:RoleGroup Roles="NewsCategoryModule-Delete">
												<ContentTemplate>
													<a href="javascript:void(0)" title="删除分类" class="delete opts" onclick="return DeleteCategoryOne(this)">
													</a>
												</ContentTemplate>
											</asp:RoleGroup>
										</RoleGroups>
									</asp:LoginView>
								</td>
							</tr>
						</ItemTemplate>
						<FooterTemplate>
							</table>
						</FooterTemplate>
					</asp:Repeater>
					<%--<br />
					<h2 class="row2-news">
						资讯信息列表</h2>
					<asp:Repeater ID="rpArticles" runat="server">
						<HeaderTemplate>
							<table>
						</HeaderTemplate>
						<ItemTemplate>
							<tr>
								<td class="cat-c1">
									<input type="checkbox" rel="child-article" value='<%# Eval("ID") %>' />
								</td>
								<td class="cat-c2">
									<a href="javascript:void(0)" onclick='showArticleInfo(<%# Eval("ID") %>)'>
										<%# Eval("Title")%></a>
								</td>
								<td class="cat-c3" articleid='<%# Eval("ID") %>'>
									<asp:LoginView ID="lv1" runat="server">
										<RoleGroups>
											<asp:RoleGroup Roles="ArticleModule-Edit">
												<ContentTemplate>
													<a href="javascript:void(0)" title="编辑资讯" class="edit opts" onclick="return EditArticle(this)">
													</a>
												</ContentTemplate>
											</asp:RoleGroup>
										</RoleGroups>
									</asp:LoginView>
									<asp:LoginView ID="LoginView1" runat="server">
										<RoleGroups>
											<asp:RoleGroup Roles="ArticleModule-Delete">
												<ContentTemplate>
													<a href="javascript:void(0)" title="删除资讯" class="delete opts" onclick="return DeleteArticleOne(this)">
													</a>
												</ContentTemplate>
											</asp:RoleGroup>
										</RoleGroups>
									</asp:LoginView>
								</td>
							</tr>
						</ItemTemplate>
						<FooterTemplate>
							</table>
						</FooterTemplate>
					</asp:Repeater>--%>
				</div>
			</td>
		</tr>
	</table>
	</form>
</body>
</html>
