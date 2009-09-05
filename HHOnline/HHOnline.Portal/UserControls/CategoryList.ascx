<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryList.ascx.cs"
	Inherits="UserControls_CategoryList" %>
<div id="cate_title_block">
	<div id="cate_title_title">
		<h2 id="cate_title">
			资讯分类</h2>
	</div>
	<asp:Repeater ID="repCategories" runat="server">
		<HeaderTemplate>
			<ul id="cate_item">
				<li id="cate_item_parent %>"><a class="cate_link" href="newslist.aspx">资讯首页</a></li>
		</HeaderTemplate>
		<ItemTemplate>
			<li id="cate_item_<%#Eval("ID") %>" onmouseover="cateShow(<%#Eval("ID") %>)" onmouseout="cateHidden(<%#Eval("ID") %>)">
				<a class="cate_link" href="newslist.aspx?cate=<%#Eval("ID") %>">
					<%#Eval("Name") %></a></li>
		</ItemTemplate>
		<FooterTemplate>
			</ul>
		</FooterTemplate>
	</asp:Repeater>
	<div class="cate_bottom">
	</div>
</div>
