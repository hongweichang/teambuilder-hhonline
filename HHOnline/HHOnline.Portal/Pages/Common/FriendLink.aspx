<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/CommonMasterPage.master" AutoEventWireup="true" CodeFile="FriendLink.aspx.cs" Inherits="Pages_Common_FriendLink" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeadName" Runat="Server">
<asp:SiteMapPath ID="smpMain" runat="server"></asp:SiteMapPath>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderCart" Runat="Server">

<div class="cart_header">
    <div class="cart_header_p">友情链接</div>
</div>

<div class="cart_list">
<div class="linksContainer">
<asp:Repeater ID="rpList" runat="server">
    <ItemTemplate>
        <a href='<%# Eval("Url") %>' target="_blank" style='font-size:<%# GetSize(Eval("Rank")) %>%'><%# Eval("Title") %></a>
    </ItemTemplate>
</asp:Repeater>
</div>
</div>
</asp:Content>

