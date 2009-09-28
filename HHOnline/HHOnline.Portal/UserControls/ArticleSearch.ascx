<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ArticleSearch.ascx.cs" Inherits="UserControls_ArticleSearch" %>
<div class="main-r2">
    <div class="main-r2c1">
        <div class="search-bg">
            <div class="search-left search">
            </div>
            <div class="search-title search-m search">
                资讯搜索：</div>
            <div class="search-input search-m">
                <asp:TextBox ID="txtSearch" rel="searcharticle" runat="server" Width="450px" MaxLength="255"
                    Style="padding: 3px;"></asp:TextBox>
            </div>
            <div class="search-search search-m search">
                <div class="search-icon">
                    &nbsp;</div>
                <a id="searchArticle" href="javascript:void(0)" >搜索</a>
            </div>
            <div class="search-right search">
            </div>
        </div>
        <div class="search-hot">
            热门搜索：<a href="#">AND</a><a href="#">行业信息</a>
        </div>
    </div>
</div>
