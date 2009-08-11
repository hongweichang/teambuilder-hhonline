<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Article.aspx.cs" Inherits="ControlPanel_News_Article" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>无标题页</title>
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
							<asp:LinkButton ID="btnAddArticle" CssClass="C-AddNews" runat="server" Text="新增文章"
								OnClientClick='return AddArticle();'></asp:LinkButton></li>
						<li>
							<asp:LinkButton ID="btnDeleteArticle" CssClass="C-DeleteNews" runat="server" Text="批量删除文章"
								OnClientClick='return DeleteArticle();'></asp:LinkButton></li>
					</ul>
				</div>
				<div class="row2" id="childList">
					
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
									<a href="javascript:void(0)" onclick='EditArticle(<%# Eval("ID") %>)'>
										<%# Eval("Title")%></a>
								</td>
								<td class="cat-c3" articleid='<%# Eval("ID") %>'>
									<asp:LoginView ID="lv1" runat="server">
										<RoleGroups>
											<asp:RoleGroup Roles="ArticleModule-Edit">
												<ContentTemplate>
													<a href="javascript:void(0)" title="编辑资讯" class="edit opts" onclick="return EditArticle(<%# Eval("ID") %>)">
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
					</asp:Repeater>
				</div>
			</td>
		</tr>
	</table>
	</form>
</body>
</html>
