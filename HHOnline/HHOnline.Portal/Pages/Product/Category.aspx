<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/HomeMasterPage.master" AutoEventWireup="true" CodeFile="Category.aspx.cs" Inherits="Pages_Product_Category" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" Runat="Server">
<script type="text/javascript">
    var activeTab = 'product';
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderSearch" Runat="Server">
    <huc:Search ID="sMain" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderContent" Runat="Server">
<div class="homemastercontent">
    <div class="cdCatNavigate">
        <hc:CategoryNavigate ID="cnProduct" runat="server"></hc:CategoryNavigate>
    </div>
    <div style="padding-left:20px;">
        <hc:CategoryList ID="clProduct" runat="server" CssClass="hpCategoryList pdCategoryList" Columns="1" />
    </div>
    <div class="navigateData">相关类别</div>
    <div>
        <hc:CategoryLikeList ID="cllProduct" runat="server" CssClass="cllProduct"></hc:CategoryLikeList>
    </div>
     <div class="navigateData">子分类信息</div>
    <div>
        <hc:CategorySubList ID="cslProduct" runat="server" CssClass="cllProduct"></hc:CategorySubList>
    </div>
    <huc:UCProductList ID="ucpProducts" runat="server" />
</div>
</asp:Content>

