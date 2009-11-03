<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/CommonMasterPage.master" AutoEventWireup="true" CodeFile="HonerUser.aspx.cs" Inherits="Pages_Common_HonerUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeadName" Runat="Server">
<asp:SiteMapPath ID="smpMain" runat="server"></asp:SiteMapPath>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderCart" Runat="Server">
<div class="cart_header">
    <div class="cart_header_p">荣誉客户</div>
</div>

<div class="cart_list aboutContent">
<asp:Literal ID="ltAbout" runat="server"></asp:Literal>
</div>
</asp:Content>

