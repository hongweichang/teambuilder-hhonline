<%@ Page Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master" AutoEventWireup="true" CodeFile="ArticleParent.aspx.cs" Inherits="ControlPanel_News_ArticleParent" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphOpts" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" Runat="Server">
<iframe width="100%" scrolling="no" height="500px" frameborder="0" src="News/Article.aspx"></iframe>
</asp:Content>

