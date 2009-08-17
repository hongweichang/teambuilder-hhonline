<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/HomeMasterPage.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderSearch" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderContent" Runat="Server">
<div class="registerContent">
    <asp:SiteMapPath ID="smRegister" runat="server" PathSeparator="&nbsp;" CssClass="sitemappath">
        <NodeStyle CssClass="allnode"/>
        <CurrentNodeStyle CssClass="curnode" />
        <PathSeparatorStyle CssClass="pathSeparatorStyle" />
    </asp:SiteMapPath>
</div>
</asp:Content>

