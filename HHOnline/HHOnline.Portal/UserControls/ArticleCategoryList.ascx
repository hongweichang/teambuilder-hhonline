<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ArticleCategoryList.ascx.cs"
	Inherits="UserControls_CategoryList" %>
<div id="cate_title_block">
	<div id="cate_title_title">
		<h2 id="cate_title">
			资讯分类</h2>
	</div>
	<asp:Repeater ID="repCategories" runat="server">
		<HeaderTemplate>
			<ul id="cate_item">
				<li id="cate_item_parent"><a class="cate_link" href="view.aspx?news-newslist">华宏资讯</a></li>
		</HeaderTemplate>
		<ItemTemplate>
			<li id="cate_item_<%#Eval("ID") %>" onmouseover="cateShow(<%#Eval("ID") %>)" onmouseout="cateHidden(<%#Eval("ID") %>)">
				<a class="cate_link" href="view.aspx?view.aspx?news-newslist&cate=<%# HHOnline.Framework.GlobalSettings.Encrypt(Eval("ID").ToString()) %>">
					<%#Eval("Name") %></a></li>
		</ItemTemplate>
		<FooterTemplate>
			</ul>
		</FooterTemplate>
	</asp:Repeater>
	<div class="cate_bottom">
	</div>
	<asp:Repeater ID="repCategoryLevel1" runat="server" 
		onitemdatabound="repCategoryLevel1_ItemDataBound">
		<ItemTemplate>
			<div class="cate_content_block_wrapper" id="cate_content_block_<%#Eval("ID") %>"
				style="top: 29px;" onmouseover="cateShow(<%#Eval("ID") %>)" onmouseout="cateHidden(<%#Eval("ID") %>)">
				<div class="cate_content_top">
				</div>
				<div class="cate_content_block">
					<ul class="cate_content_body">
						<asp:Repeater ID="repCategoryLevel2" runat="server">
							<ItemTemplate>
								<li><a class="cate_link" href="view.aspx?news-newslist&cate=<%# HHOnline.Framework.GlobalSettings.Encrypt(Eval("ID").ToString()) %>">
									<%#Eval("Name") %></a> </li>
							</ItemTemplate>
						</asp:Repeater>
					</ul>
				</div>
				<div class="cate_content_bottom">
				</div>
			</div>
		</ItemTemplate>
	</asp:Repeater>
</div>
