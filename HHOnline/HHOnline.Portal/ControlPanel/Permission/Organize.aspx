<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Organize.aspx.cs" Inherits="ControlPanel_Permission_Organize" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Organize</title>
</head>
<body>
    <form id="form1" runat="server">
    <table class="treelist" cellpadding="0" cellspacing="5">
        <tr>
            <td class="lefttree" valign="top">
                <div class="row1">部门列表</div>
                <div class="row2">
                    <asp:TreeView ID="tvOganize" runat="server"  ShowLines="true" 
                        OnSelectedNodeChanged="tvOganize_SelectedNodeChanged" >
                        <SelectedNodeStyle BackColor="#000080" HorizontalPadding="5" ForeColor="White" />
                        <NodeStyle  HorizontalPadding="5" />
                    </asp:TreeView>
                </div>
            </td>
            <td class="righttree" valign="top">
                <div class="row1">
                    <ul>
                        <li><asp:LinkButton ID="lbAddDept" CssClass="O-AddDept" runat="server" Text="新增部门" OnClientClick='return AddDept(window.$selectNodeId);'></asp:LinkButton></li>
                        <li><asp:LinkButton ID="lbDeleteDept" CssClass="O-DeleteDept"  runat="server" Text="批量删除部门" OnClientClick='return DeleteDept();'></asp:LinkButton></li>
                        <li><asp:LinkButton ID="lbAddUser" runat="server" CssClass="O-AddUser" Text="新增用户" OnClientClick="return AddUser();"></asp:LinkButton></li>
                        <li><asp:LinkButton ID="lbDeleteUser" runat="server" CssClass="O-DeleteUser" Text="批量删除用户" OnClientClick="return DeleteUser();"></asp:LinkButton></li>
                    </ul>
                </div>
                <div class="row2" id="childList">
                    <h2 class="row2-dept">
                        部门信息列表</h2>
                    <asp:Repeater ID="rpChildDept" runat="server">
                        <HeaderTemplate>
                            <table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="cat-c1">                                    
                                    <input type="checkbox" rel="child-dept" value='<%# Eval("OrganizationID") %>' />
                                </td>
                                <td class="cat-c2 department-cell" >
                                    <asp:LinkButton ID="lnkChildDept" runat="server" OnClick="lnkChildDept_Click" ToolTip='<%# Eval("OrganizationDesc") %>' PostBackUrl="#" OrgID='<%# Eval("OrganizationID") %>'><%# Eval("OrganizationName") %></asp:LinkButton>                                    
                                </td>
                                <td class="cat-c3" orgId='<%# Eval("OrganizationID") %>'>
                                    <asp:LoginView ID="LoginView1" runat="server">
                                        <RoleGroups>
                                            <asp:RoleGroup Roles="OrganizeModule-Edit">
                                                <ContentTemplate>
                                                    <a href="#" class="edit opts" title="编辑" onclick="return UpdateDept(this)"></a>
                                                </ContentTemplate>
                                            </asp:RoleGroup>
                                        </RoleGroups>
                                    </asp:LoginView>
                                    <asp:LoginView ID="lv1" runat="server">
                                        <RoleGroups>
                                            <asp:RoleGroup Roles="OrganizeModule-Delete">
                                                <ContentTemplate>
                                                    <a href="#" class="delete opts" title="删除" onclick="return DeleteDeptOne(this)"></a>
                                                </ContentTemplate>
                                            </asp:RoleGroup>
                                        </RoleGroups>
                                    </asp:LoginView>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <br />
                     <h2 class="row2-user">
                        用户信息列表</h2>
                    <asp:Repeater ID="rpUsers" runat="server">
                        <HeaderTemplate>
                            <table >
                        </HeaderTemplate>
                        <ItemTemplate>  
                            <tr>
                                <td class="cat-c1">
                                    <input type="checkbox" rel="inner-user" value='<%# Eval("UserID") %>' />
                                </td>
                                <td class="cat-c2 department-cell person-cell">                                    
                                    <a href="javascript:void(0)" onclick='showUserInfo(<%# Eval("UserID") %>)'><%# Eval("DisplayName") %></a>
                                </td>
                                <td class="cat-c3" userId='<%# Eval("UserID") %>'>
                                    <asp:LoginView ID="LoginView2" runat="server">
                                        <RoleGroups>
                                            <asp:RoleGroup Roles="UserModule-SetGrade">
                                                <ContentTemplate>
                                                    <a href="#" class="add opts" title="设置用户级别" onclick="return SetGrade(this)"></a>
                                                </ContentTemplate>
                                            </asp:RoleGroup>
                                        </RoleGroups>
                                    </asp:LoginView>
                                     <asp:LoginView ID="LoginView1" runat="server">
                                        <RoleGroups>
                                            <asp:RoleGroup Roles="UserModule-Edit">
                                                <ContentTemplate>
                                                    <a href="#" class="edit opts" title="编辑" onclick="return UpdateUser(this)"></a>
                                                </ContentTemplate>
                                            </asp:RoleGroup>
                                        </RoleGroups>
                                    </asp:LoginView>
                                    <asp:LoginView ID="lv1" runat="server">
                                        <RoleGroups>
                                            <asp:RoleGroup Roles="UserModule-Delete">
                                                <ContentTemplate>
                                                    <a href="#" class="delete opts" title="删除" onclick="return DeleteUserOne(this)"></a>
                                                </ContentTemplate>
                                            </asp:RoleGroup>
                                        </RoleGroups>
                                    </asp:LoginView>
                                </td>
                            </tr>     
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
