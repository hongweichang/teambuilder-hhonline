<%@ Page Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master"
    AutoEventWireup="true" CodeFile="ManageCache.aspx.cs" Inherits="ControlPanel_Site_ManagerCache" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="ContentOpts" ContentPlaceHolderID="cphOpts" runat="Server">
    <asp:LinkButton ID="lbClear" runat="server" Text=" " OnClick="btnClear_Click" SkinID="lnkopts" PostBackUrl="#" OnClientClick="return confirm('删除全部缓存将可能影响到部分平台性能，\n执行删除操作后缓存将刷新，确认继续？')">
        <span>一键清除</span>
    </asp:LinkButton>
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="cphContent" runat="Server">
    <hc:ExtensionGridView ID="egvCaches" PageSize="10" DataKeyNames="Name" AutoGenerateColumns="false" 
        OnRowDeleting="egvCaches_RowDeleting" OnPageIndexChanging="egvCaches_PageIndexChanging"
        runat="server" SkinID="DefaultView" OnRowDataBound="egvCaches_RowDataBound" >
        <Columns>
            <asp:TemplateField HeaderText="缓存名称">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblCacheName"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="当前缓存数">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblCacheCount"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <asp:LoginView ID="lvCache" runat="server">
                        <RoleGroups>
                            <asp:RoleGroup Roles="ManageCacheModule-Delete">
                                <ContentTemplate>                                
                                    <asp:LinkButton ID="lnkClear" SkinID="lnkdelete" runat="server" CommandName="Delete"  ToolTip="清除缓存" OnClientClick="return confirm('删除缓存将可能影响到部分平台性能，\n执行删除操作后缓存将刷新，确认继续？')" PostBackUrl="#">
                                    </asp:LinkButton>
                                </ContentTemplate>
                            </asp:RoleGroup>
                        </RoleGroups>
                    </asp:LoginView>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </hc:ExtensionGridView>
</asp:Content>
