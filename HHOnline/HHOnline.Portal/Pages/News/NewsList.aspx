﻿<%@ Page Language="C#" MasterPageFile="~/Masters/HomeMasterPage.master" AutoEventWireup="true"
	CodeFile="NewsList.aspx.cs" Inherits="News_NewsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="Server">
<script type="text/javascript">
    var activeTab = 'news';
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderSearch" runat="Server">
	<huc:ArticleSearch ID="asNews" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderContent" runat="Server">
	<div id="main">
		<div class="post_nav_block_wrapper">
			<ul class="post_nav_block">
				<li>
					<asp:Label ID="lblCategoryName" runat="server"></asp:Label></li></ul>
			<div class="clear">
			</div>
		</div>
		
		<a class="rss_link" href="javascript:void(0)" title="订阅Rss">&nbsp;</a>
		<a href="<% Response.Write("news-newslist&v=1&cate=" + Request.QueryString["cate"] + "&p=" + Request.QueryString["p"]); %>" title="查看方式 - 列表" class="news_list">&nbsp;</a>		
		<a href="<%Response.Write("news-newslist&v=0&cate=" + Request.QueryString["cate"] + "&p=" + Request.QueryString["p"]);%>" class="news_detail" title="查看方式 - 详细资料" >&nbsp;</a>
		
		<div id="post_list">
			<asp:Repeater ID="repArticlesList" runat="server">
				<ItemTemplate>
					<div class="post_item">
						<div class="post_item_body">
							<h3>
								<a class="titlelnk" href='news-newsdetail&id=<%#Eval("ID") %>' target="_blank">
									<%#Eval("Title") %></a>
								<%#Eval("SubTitle") %></h3>
							<div class="post_item_foot_list">
								发布于:
								<%#Eval("Date")%></div>
						</div>
						<div class="clear">
						</div>
					</div>
				</ItemTemplate>
			</asp:Repeater>
			<asp:Repeater ID="repArticles" runat="server" OnItemDataBound="repArticles_ItemDataBound">
				<ItemTemplate>
					<div class="post_item">
						<div class="post_item_body">
							<h3>
								<a class="titlelnk" href='news-newsdetail&id=<%#Eval("ID") %>' target="_blank">
									<%#Eval("Title") %></a>
								<%#Eval("SubTitle") %></h3>
							<div class="articleimg">
								<asp:Image ID="imgAttachment" runat="server" />
							</div>
							<div class="post_item_foot">
								发布于:
								<%#Eval("Date")%>
								作者: <a class="lightblue" href='#'>
									<%#Eval("Author")%></a> <span class="article_comment"><a class="grayline" title="目前还没有评论"
										href="#">评论(<span id="feedback_count_1560804">0</span>)</a></span> <span class="article_view">
											<a class="grayline" href='news-newsdetail&id=<%#Eval("ID") %>'>阅读(<span id="viewcount_1560804"><%#Eval("HitTimes")%></span>)</a></span></div>
							<div class="post_item_sep">
							</div>
							<p class="post_item_summary">
								<%#Eval("Abstract")%></p>
						</div>
						<div class="clear">
						</div>
					</div>
				</ItemTemplate>
			</asp:Repeater>
		</div>
		<div id="pager_block">
			<div id="pager">
				<% WritePagesNavigator(); %></div>
		</div>
	</div>
</asp:Content>