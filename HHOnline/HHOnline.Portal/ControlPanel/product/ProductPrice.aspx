<%@ Page Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master"
    AutoEventWireup="true" CodeFile="ProductPrice.aspx.cs" Inherits="ControlPanel_product_ProductPrice"
    Title="产品报价管理" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="ContentOpts" ContentPlaceHolderID="cphOpts" runat="Server">
    <asp:LinkButton ID="lbNewPrice" runat="server" SkinID="lnkopts">
        <span>新 增</span>
    </asp:LinkButton>
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="cphContent" runat="Server">
    <div id="nav">
        <asp:HyperLink ID="hyAllProduct" runat="server">所有产品</asp:HyperLink>
        >>
        <asp:HyperLink ID="hyProductPrice" runat="server"></asp:HyperLink>
    </div>
    <hc:ExtensionGridView ID="egvProductPrices" AutoGenerateColumns="False" DataKeyNames="PriceID"
        runat="server" OnRowDataBound="egvProductPrices_RowDataBound" PageSize="5" SkinID="DefaultView"
        OnRowDeleting="egvProductPrices_RowDeleting" OnRowUpdating="egvProductPrices_RowUpdating"
        OnPageIndexChanging="egvProductPrices_PageIndexChanging">
        <Columns>
            <asp:BoundField HeaderText="报价起始日期" DataField="QuoteFrom" DataFormatString="{0:d}" />
            <asp:BoundField HeaderText="报价截止日期" DataField="QuoteEnd" DataFormatString="{0:d}" />
            <asp:BoundField HeaderText="供货区域" DataField="SupplyRegionName" />
            <asp:TemplateField>
                <HeaderStyle Width="200" />
                <HeaderTemplate>
                    操作</HeaderTemplate>
                <ItemTemplate>
                    <asp:LoginView ID="LoginView4" runat="server">
                        <RoleGroups>
                            <asp:RoleGroup Roles="ProductModule-Add">
                                <ContentTemplate>
                                    <asp:LinkButton ID="lnkAddChild" runat="server" CommandName="AddChild" SkinID="lnkadd"
                                        PostBackUrl="#"></asp:LinkButton>
                                </ContentTemplate>
                            </asp:RoleGroup>
                        </RoleGroups>
                    </asp:LoginView>
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
