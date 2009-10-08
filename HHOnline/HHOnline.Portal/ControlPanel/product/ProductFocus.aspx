<%@ Page Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master"
    AutoEventWireup="true" CodeFile="ProductFocus.aspx.cs" Inherits="ControlPanel_product_ProductFocus"
    Title="分类栏目" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="ContentOpts" ContentPlaceHolderID="cphOpts" runat="Server">
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="cphContent" runat="Server">
    <div class="productNav">
        <asp:LinkButton ID="lnkRecomment" runat="server" OnClick="lnk_Click">促销</asp:LinkButton>
        <asp:LinkButton ID="lnkPromotion" runat="server" OnClick="lnk_Click">推荐</asp:LinkButton>
        <asp:LinkButton ID="lnkHot" runat="server" OnClick="lnk_Click">热卖</asp:LinkButton>
        <asp:LinkButton ID="lnkNew" runat="server" OnClick="lnk_Click">新品上架</asp:LinkButton>
    </div>
    <hc:ExtensionGridView ID="egvProductFocus" runat="server">
        <Columns>
            <asp:BoundField HeaderText="关注起始日期" DataField="FocusFrom" DataFormatString="{0:d}" />
            <asp:BoundField HeaderText="关注截止日期" DataField="FocusEnd" DataFormatString="{0:d}" />
        </Columns>
    </hc:ExtensionGridView>
</asp:Content>
