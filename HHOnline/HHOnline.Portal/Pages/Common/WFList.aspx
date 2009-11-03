<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/CommonMasterPage.master" AutoEventWireup="true" CodeFile="WFList.aspx.cs" Inherits="Pages_Common_WFList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeadName" Runat="Server">
<asp:SiteMapPath ID="smpMain" runat="server"></asp:SiteMapPath>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderCart" Runat="Server">

<div class="cart_header">
    <div class="cart_header_p">业务流程</div>
</div>

<div class="cart_list aboutContent">
<asp:Literal ID="ltAbout" runat="server"></asp:Literal>
</div>
</asp:Content>

