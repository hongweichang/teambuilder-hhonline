<%@ Page Title="" Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master" AutoEventWireup="true" CodeFile="Role.aspx.cs" Inherits="ControlPanel_Permission_Role" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphOpts" Runat="Server">
    <asp:LinkButton ID="lbNewRole" runat="server" SkinID="lnkopts">
        <span>新 增</span>
    </asp:LinkButton>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">
    <hc:ExtensionGridView ID="egvRoles" PageSize="10" DataKeyNames="RoleID" OnRowUpdating="egvRoles_RowUpdating" OnRowDeleting="egvRoles_RowDeleting"
            AutoGenerateColumns="false" runat="server" SkinID="DefaultView" OnPageIndexChanging="egvRoles_PageIndexChanging">
        <Columns>
            <asp:TemplateField>
                <HeaderStyle Width="200" />
                <HeaderTemplate>角色名称</HeaderTemplate>
                <ItemTemplate><%# Eval("RoleName") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>角色描述</HeaderTemplate>
                <ItemTemplate><%# Eval("Description") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderStyle Width="200" />
                <HeaderTemplate>操作</HeaderTemplate>
                <ItemTemplate>
                    <asp:LoginView ID="LoginView1" runat="server">
                        <RoleGroups>
                            <asp:RoleGroup Roles="RoleModule-Edit"> 
                                <ContentTemplate>    
                                <asp:LinkButton ID="lnkUpdate" runat="server" CommandName="Update" SkinID="lnkedit" PostBackUrl="#"></asp:LinkButton>
                                </ContentTemplate>
                            </asp:RoleGroup>
                        </RoleGroups>
                    </asp:LoginView>
                    <asp:LoginView ID="LoginView2" runat="server">
                        <RoleGroups>
                            <asp:RoleGroup Roles="RoleModule-Delete"> 
                                <ContentTemplate>    
                                <asp:LinkButton ID="lnkDelete" runat="server"  CommandName="Delete" SkinID="lnkdelete"  OnClientClick="return confirm('确定要删除此记录吗？')" PostBackUrl="#"></asp:LinkButton>
                                </ContentTemplate>
                            </asp:RoleGroup>
                        </RoleGroups>
                    </asp:LoginView>
                    <asp:LoginView ID="LoginView3" runat="server">
                        <RoleGroups>
                             <asp:RoleGroup Roles="RoleModule-View"> 
                                <ContentTemplate>    
                                    <%--<a href="javascript:popWin('<%# "Permission/RoleDetail.aspx?ID="+Eval("RoleID").ToString() %>',800)" title="查看详细" class="opts view">&nbsp;</a>--%>
                                    <a href='javascript:void(<%# Eval("RoleID") %>)' rel="showdetails" class="opts view" title="查看详细"></a>
                                </ContentTemplate>
                            </asp:RoleGroup>
                        </RoleGroups>
                    </asp:LoginView>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </hc:ExtensionGridView>
</asp:Content>

