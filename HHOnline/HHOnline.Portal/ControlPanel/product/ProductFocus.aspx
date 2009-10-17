<%@ Page Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master"
    AutoEventWireup="true" CodeFile="ProductFocus.aspx.cs" Inherits="ControlPanel_product_ProductFocus"
    Title="分类栏目" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="ContentOpts" ContentPlaceHolderID="cphOpts" runat="Server">
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="cphContent" runat="Server">
    <div class="productNav promotionNav">
        <asp:LinkButton ID="lnkRecomment" runat="server" CssClass="recommend" OnClick="lnk_Click">推荐</asp:LinkButton>
        <asp:LinkButton ID="lnkPromotion" runat="server" CssClass="promotion" OnClick="lnk_Click">促销</asp:LinkButton>
        <asp:LinkButton ID="lnkHot" runat="server" CssClass="hot" OnClick="lnk_Click">热卖</asp:LinkButton>
        <asp:LinkButton ID="lnkNew" runat="server" CssClass="new" OnClick="lnk_Click">新品上架</asp:LinkButton>
    </div>
    <hc:ExtensionGridView ID="egvProductFocus" runat="server" AutoGenerateColumns="False"
        DataKeyNames="FocusID" PageSize="15" SkinID="DefaultView" OnPageIndexChanging="egvProductFocus_PageIndexChanging"
        OnRowDataBound="egvProductFocus_RowDataBound" OnRowDeleting="egvProductFocus_RowDeleting"
        OnRowUpdating="egvProductFocus_RowUpdating">
        <Columns>
            <asp:TemplateField HeaderText="产品名称">
                <ItemTemplate>
                    <asp:HyperLink ID="hlProductName" runat="server"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="关注起始日期" DataField="FocusFrom" DataFormatString="{0:d}" />
            <asp:BoundField HeaderText="关注截止日期" DataField="FocusEnd" DataFormatString="{0:d}" />
            <asp:TemplateField>
                <HeaderStyle Width="200" />
                <HeaderTemplate>
                    操作</HeaderTemplate>
                <ItemTemplate>
                    <asp:LoginView ID="LoginView1" runat="server">
                        <RoleGroups>
                            <asp:RoleGroup Roles="ProductModule-Edit">
                                <ContentTemplate>
                                    <asp:LinkButton ID="lnkUpdate" runat="server" CommandName="Update" SkinID="lnkedit"
                                        PostBackUrl="#"></asp:LinkButton>
                                </ContentTemplate>
                            </asp:RoleGroup>
                        </RoleGroups>
                    </asp:LoginView>
                    <asp:LoginView ID="LoginView2" runat="server">
                        <RoleGroups>
                            <asp:RoleGroup Roles="ProductModule-Delete">
                                <ContentTemplate>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" SkinID="lnkdelete"
                                        OnClientClick="return confirm('确定要删除此记录吗？')" PostBackUrl="#"></asp:LinkButton>
                                </ContentTemplate>
                            </asp:RoleGroup>
                        </RoleGroups>
                    </asp:LoginView>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </hc:ExtensionGridView>
</asp:Content>
