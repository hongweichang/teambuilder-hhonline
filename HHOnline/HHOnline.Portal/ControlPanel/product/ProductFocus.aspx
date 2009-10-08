<%@ Page Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master"
    AutoEventWireup="true" CodeFile="ProductFocus.aspx.cs" Inherits="ControlPanel_product_ProductFocus"
    Title="分类栏目" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="ContentOpts" ContentPlaceHolderID="cphOpts" runat="Server">
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="cphContent" runat="Server">
    <hc:ExtensionGridView ID="egvProductFocus" runat="server">
        <Columns>
        </Columns>
    </hc:ExtensionGridView>
</asp:Content>
