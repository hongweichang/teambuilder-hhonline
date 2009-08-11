<%@ Page Title="" Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master"
    AutoEventWireup="true" CodeFile="Variety.aspx.cs" Inherits="ControlPanel_Product_Variety" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="ContentOpts" ContentPlaceHolderID="cphOpts" runat="Server">
    <asp:LinkButton ID="lbNewBrand" runat="server" SkinID="lnkopts">
        <span>新 增</span>
    </asp:LinkButton>
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="cphContent" runat="Server">
    <hc:ExtensionGridView ID="egvBrands" AutoGenerateColumns="False" DataKeyNames="BrandID"
        runat="server" OnRowDataBound="egvBrands_RowDataBound" PageSize="5" SkinID="DefaultView"
        OnRowDeleting="egvBrands_RowDeleting" OnRowUpdating="egvBrands_RowUpdating" OnPageIndexChanging="egvBrands_PageIndexChanging">
        <Columns>
            <asp:TemplateField HeaderText="商标">
                <HeaderStyle Width="100" />
                <ItemTemplate>
                    <asp:Image ID="BrandLogo" Width="40" Height="40" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="品牌名称">
                <ItemTemplate>
                    <asp:HyperLink ID="hlBrandName" runat="server"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="品牌分组" DataField="BrandGroup" />
            <asp:BoundField HeaderText="标题说明" DataField="BrandTitle" />
            <asp:BoundField HeaderText="介绍摘要" DataField="BrandAbstract" />
            <asp:TemplateField>
                <HeaderStyle Width="200" />
                <HeaderTemplate>
                    操作</HeaderTemplate>
                <ItemTemplate>
                    <asp:LoginView ID="LoginView1" runat="server">
                        <RoleGroups>
                            <asp:RoleGroup Roles="VarietyModule-Edit">
                                <ContentTemplate>
                                    <asp:LinkButton ID="lnkUpdate" runat="server" CommandName="Update" SkinID="lnkedit"
                                        PostBackUrl="#"></asp:LinkButton>
                                </ContentTemplate>
                            </asp:RoleGroup>
                        </RoleGroups>
                    </asp:LoginView>
                    <asp:LoginView ID="LoginView2" runat="server">
                        <RoleGroups>
                            <asp:RoleGroup Roles="VarietyModule-Delete">
                                <ContentTemplate>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" SkinID="lnkdelete"
                                        OnClientClick="return confirm('确定要删除此记录吗？')" PostBackUrl="#"></asp:LinkButton>
                                </ContentTemplate>
                            </asp:RoleGroup>
                        </RoleGroups>
                    </asp:LoginView>
                    <%--  <asp:LoginView ID="LoginView3" runat="server">
                        <RoleGroups>
                            <asp:RoleGroup Roles="VarietyModule-View">
                                <ContentTemplate>
                                    <a href='javascript:void(0)' rel="showdetails" class="opts view" title="查看详细"></a>
                                </ContentTemplate>
                            </asp:RoleGroup>
                        </RoleGroups>
                    </asp:LoginView>--%>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </hc:ExtensionGridView>
</asp:Content>
