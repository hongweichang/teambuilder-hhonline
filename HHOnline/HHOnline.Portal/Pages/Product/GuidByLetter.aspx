<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/HomeMasterPage.master" AutoEventWireup="true" CodeFile="GuidByLetter.aspx.cs" Inherits="Pages_Product_GuidByLetter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" Runat="Server">
<script type="text/javascript">
    var activeTab = 'product';
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderSearch" Runat="Server">
<huc:Search ID="sProduct" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderContent" Runat="Server">
<div class="homemastercontent">
 <div class="cdCatNavigate">
    <asp:Literal ID="ltLetterType" runat="server"></asp:Literal>
 </div>
 <div style="padding:10px 20px;">
    <hc:LetterList ID="llCategory" runat="server" CssClass="hpCategoryList pdCategoryList"  />
</div>
</div>
</asp:Content>

