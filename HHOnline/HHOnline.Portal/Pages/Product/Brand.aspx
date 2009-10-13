<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/HomeMasterPage.master" AutoEventWireup="true" CodeFile="Brand.aspx.cs" Inherits="Pages_Product_Brand" %>

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
        <hc:VarietyNavigate ID="vnProduct" runat="server"></hc:VarietyNavigate>
    </div>
    <div style="padding-left:20px;">
        <hc:HPVarietyList ID="hpvlList" Max="99999" runat="server" CssClass="hpCategoryList pdCategoryList" Columns="1" />
    </div>
    <div class="navigateData">相关品牌</div>
    <div>
        <hc:VarietyLikeList ID="hpblList" runat="server" CssClass="cllProduct"></hc:VarietyLikeList>
    </div>
   <huc:UCProductList ID="ucpProducts" runat="server" />
</div>
</asp:Content>

