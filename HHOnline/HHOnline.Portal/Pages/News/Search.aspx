<%@ Page Language="C#" MasterPageFile="~/Masters/HomeMasterPage.master" AutoEventWireup="true"
	CodeFile="Search.aspx.cs" Inherits="Pages_News_Search" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="Server">

	<script type="text/javascript">
    var activeTab = 'article';
	</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderSearch" runat="Server">
	<huc:Search ID="sArticle" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderContent" runat="Server">
	<div class="homemastercontent">
		<div class="searchDuration">
			<asp:Literal ID="ltSearchDuration" runat="server"></asp:Literal></div>
		<div style="padding: 5px;">
			<hc:MsgBox ID="msgBox" SkinID="msgBox" runat="server"></hc:MsgBox>
			<asp:DataList ID="dlArticle" runat="server" OnItemDataBound="dlArticle_ItemDataBound"
				RepeatColumns="1" RepeatDirection="Horizontal" RepeatLayout="Flow">
				<ItemTemplate>
					<div class="productGridShow">
						<asp:Image BorderWidth="4" BorderColor="#dedede" ID="imgArticle" runat="server" />
						<div class="productTitle">
							<div class="productTitle-r1">
								<a href='view.aspx?news-newsdetail&ID=<%# HHOnline.Framework.GlobalSettings.Encrypt(Eval("ID").ToString()) %>'
									target="_blank">
									<%# Eval("Title") %></a>
							</div>
							<div class="productTitle-r2">
								<%# Eval("Abstract")%>
							</div>
							<div class="productTitle-r3">
								关键字：<%# Eval("Keywords")%>
							</div>
							<br />
						</div>
						<%--<div class="productPrice price">
							<asp:Literal ID="ltPrice" runat="server"></asp:Literal></div>--%>
						<div class="productBrand">
							分类： <a href='view.aspx?news-newslist&cate=<%# HHOnline.Framework.GlobalSettings.Encrypt(Eval("Category").ToString()) %>'
								target="_blank">
								<%# Eval("CategoryObject")%>
							</a>
						</div>
					</div>
				</ItemTemplate>
			</asp:DataList>
			<hc:CollectionPager ID="cpArticle" PageSize="10" runat="server">
			</hc:CollectionPager>
		</div>
	</div>
</asp:Content>
