<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="UserControls_Search" %>
<div class="main-r2">
    <div class="main-r2c1">
        <div class="search-bg">
            <div class="search-left search">
            </div>
            <div class="search-title search-m search">
                产品搜索：</div>
            <div class="search-type search-m">
                <asp:DropDownList ID="ddlSearchType" runat="server">
                    <asp:ListItem Text="全部范围" Selected="True" Value="0"></asp:ListItem>
                    <asp:ListItem Text="产品名称" Value="1"></asp:ListItem>
                    <asp:ListItem Text="产品品牌" Value="2"></asp:ListItem>
                    <asp:ListItem Text="产品型号" Value="3"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="search-input search-m">
                <asp:TextBox ID="txtSearch" rel="searchproduct" runat="server" Width="350px" MaxLength="255"
                    Style="padding: 3px;"></asp:TextBox>
            </div>
            <div class="search-search search-m search">
                <div class="search-icon">
                    &nbsp;</div>
                <asp:LinkButton ID="lnkSearch" runat="server" Text="搜索"></asp:LinkButton>
            </div>
            <div class="search-right search">
            </div>
        </div>
        <div class="search-hot">
            热门搜索：<a href="#">AND</a><a href="#">精密天平</a>
        </div>
    </div>
</div>
