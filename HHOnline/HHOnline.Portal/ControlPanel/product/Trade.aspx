<%@ Page Title="" Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master"
    AutoEventWireup="true" CodeFile="Trade.aspx.cs" Inherits="ControlPanel_Product_Trade" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="ContentOpts" ContentPlaceHolderID="cphOpts" runat="Server">
    <asp:LinkButton ID="lbNewIndustry" runat="server" SkinID="lnkopts">
        <span>新 增</span>
    </asp:LinkButton>
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="cphContent" runat="Server">
    <hc:ExtensionGridView ID="egvIndustries" AutoGenerateColumns="False" DataKeyNames="IndustryID"
        runat="server" OnRowDataBound="egvIndustries_RowDataBound" PageSize="5" SkinID="DefaultView"
        OnRowDeleting="egvIndustries_RowDeleting" OnRowUpdating="egvIndustries_RowUpdating"
        OnPageIndexChanging="egvIndustries_PageIndexChanging" OnRowCommand="egvIndustries_RowCommand">
        <Columns>
            <asp:TemplateField HeaderText="商标">
                <HeaderStyle Width="100" />
                <ItemTemplate>
                    <asp:Image ID="IndustryLogo" Width="40" Height="40" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="行业名称">
                <ItemTemplate>
                    <asp:HyperLink ID="hlIndustryName" runat="server"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="标题说明" DataField="IndustryTitle" />
            <asp:BoundField HeaderText="介绍摘要" DataField="IndustryAbstract" />
            <asp:TemplateField>
                <HeaderStyle Width="200" />
                <HeaderTemplate>
                    操作</HeaderTemplate>
                <ItemTemplate>
                    <asp:LoginView ID="LoginView4" runat="server">
                        <RoleGroups>
                            <asp:RoleGroup Roles="TradeModule-Add">
                                <ContentTemplate>
                                    <asp:LinkButton ID="lnkAddChild" runat="server" CommandName="AddChild" SkinID="lnkadd"
                                        PostBackUrl="#"></asp:LinkButton>
                                </ContentTemplate>
                            </asp:RoleGroup>
                        </RoleGroups>
                    </asp:LoginView>
                    <asp:LoginView ID="LoginView1" runat="server">
                        <RoleGroups>
                            <asp:RoleGroup Roles="TradeModule-Edit">
                                <ContentTemplate>
                                    <asp:LinkButton ID="lnkUpdate" runat="server" CommandName="Update" SkinID="lnkedit"
                                        PostBackUrl="#"></asp:LinkButton>
                                </ContentTemplate>
                            </asp:RoleGroup>
                        </RoleGroups>
                    </asp:LoginView>
                    <asp:LoginView ID="LoginView2" runat="server">
                        <RoleGroups>
                            <asp:RoleGroup Roles="TradeModule-Delete">
                                <ContentTemplate>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" SkinID="lnkdelete"
                                        OnClientClick="return confirm('确定要删除此记录吗？')" PostBackUrl="#"></asp:LinkButton>
                                </ContentTemplate>
                            </asp:RoleGroup>
                        </RoleGroups>
                    </asp:LoginView>
                  <%--  <asp:LoginView ID="LoginView3" runat="server">
                        <RoleGroups>
                            <asp:RoleGroup Roles="TradeModule-View">
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
