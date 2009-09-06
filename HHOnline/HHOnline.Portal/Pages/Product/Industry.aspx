﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/HomeMasterPage.master" AutoEventWireup="true" CodeFile="Industry.aspx.cs" Inherits="Pages_Product_Industry" %>

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
        <hc:IndustryNavigate ID="inProduct" runat="server"></hc:IndustryNavigate>
    </div>
    <div style="padding-left:20px;">
        <hc:HPIndustryList ID="hpilProduct" runat="server" CssClass="hpCategoryList pdCategoryList" Columns="1" />
    </div>
    <div class="navigateData">相关行业</div>
    <div>
        <hc:IndustryLikeList ID="illProduct" runat="server" CssClass="cllProduct"></hc:IndustryLikeList>
    </div>
     <div class="navigateData">子行业信息</div>
    <div>
        <hc:IndustrySubList ID="islProduct" runat="server" CssClass="cllProduct"></hc:IndustrySubList>
    </div>
    <asp:Panel ID="pnlSort" runat="server">
    <div class="sortList">
        <asp:LinkButton ID="lnkGrid" OnClick="linkButton_Click"  CssClass="showByGrid showBy" runat="server" Text=" " ToolTip="按列表显示"></asp:LinkButton>
        <asp:LinkButton ID="lnkList" OnClick="linkButton_Click" CssClass="showByList showBy" runat="server" ToolTip="按大图显示"></asp:LinkButton>
    </div>
    </asp:Panel>
    <div style="padding:5px;">
    </div>
</div>
</asp:Content>
