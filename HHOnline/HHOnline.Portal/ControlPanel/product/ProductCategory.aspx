<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductCategory.aspx.cs"
    Inherits="ControlPanel_product_ProductCategory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>产品分类</title>
</head>
<body>
    <form id="form1" runat="server">
    <table class="treelist" cellpadding="0" cellspacing="5">
        <tr>
            <td class="lefttree" valign="top">
                <div class="row1">
                    分类列表</div>
                <div class="row2">
                    <asp:TreeView ID="tvCategory" runat="server" ShowLines="true" OnSelectedNodeChanged="tvCategory_SelectedNodeChanged">
                        <SelectedNodeStyle BackColor="#000080" HorizontalPadding="5" ForeColor="White" />
                        <NodeStyle HorizontalPadding="5" />
                    </asp:TreeView>
                </div>
            </td>
            <td class="righttree" valign="top">
                <div class="row1">
                    <ul>
                        <li>
                            <asp:LinkButton ID="lbAddCategory" CssClass="C-AddCat" runat="server" Text="新增分类"
                                OnClientClick='return AddCategory(window.$selectNodeId)'></asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="lbDeleteCategory" runat="server" CssClass="C-DeleteCat" Text="删除分类"
                                OnClientClick='return DeleteCategory()'></asp:LinkButton></li>
                        <li>
                            <asp:LinkButton ID="lbUpdateCategory" runat="server" CssClass="C-UpdateCat" Text="分类信息修改"
                                OnClientClick='return UpdateCategory(window.$selectNodeId)'></asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="lbAddProperty" runat="server" CssClass="C-AddProperty" Text="新增属性"
                                OnClientClick="return AddProperty(window.$selectNodeId)"></asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="lbDeleteProperty" runat="server" CssClass="C-DeleteCat" Text="删除属性"
                                OnClientClick="return DeleteProperty()"></asp:LinkButton>
                        </li>
                    </ul>
                </div>
                <div class="row2">
                    <hc:MsgBox runat="server" ID="mbMsg" SkinID="msgBox"></hc:MsgBox>
                    <h2 class="row2-cat">
                        子分类列表</h2>
                    <asp:Repeater ID="rpChildCategory" runat="server">
                        <HeaderTemplate>
                            <table id="tbCat">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="cat-c1">
                                    <input type="checkbox" rel="child-category" value='<%# Eval("CategoryID") %>' />
                                </td>
                                <td class="cat-c2">
                                    <a href="javascript:void(0)" onclick='redirectToProduct(<%# Eval("CategoryID") %>)'
                                        title='<%# Eval("CategoryDesc") %>'>
                                        <%# Eval("CategoryName")%></a>
                                </td>
                                <td class="cat-c3" catid='<%# Eval("CategoryID") %>'>
                                    <asp:LoginView ID="LoginView1" runat="server">
                                        <RoleGroups>
                                            <asp:RoleGroup Roles="ProductCategoryModule-Edit">
                                                <ContentTemplate>
                                                    <a href="javascript:{}" class="edit opts" onclick='return UpdateCategory(-1,this);'>
                                                    </a>
                                                </ContentTemplate>
                                            </asp:RoleGroup>
                                        </RoleGroups>
                                    </asp:LoginView>
                                    <asp:LoginView ID="LoginView2" runat="server">
                                        <RoleGroups>
                                            <asp:RoleGroup Roles="ProductCategoryModule-Delete">
                                                <ContentTemplate>
                                                    <a href="javascript:{}" class="delete opts" onclick='return DeleteCategory(this);'>
                                                    </a>
                                                </ContentTemplate>
                                            </asp:RoleGroup>
                                        </RoleGroups>
                                    </asp:LoginView>
                                    &nbsp;排序：<%# Eval("DisplayOrder", "{0:000}")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <br />
                    <h2 class="row2-property">
                        属性列表</h2>
                    <asp:Repeater ID="rpProperties" runat="server">
                        <HeaderTemplate>
                            <table id="tbProperty">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="cat-c1">
                                    <input type="checkbox" rel="child-category" value='<%# Eval("PropertyID") %>' />
                                </td>
                                <td class="cat-c2">
                                    <%# Eval("PropertyName")%>
                                </td>
                                <td class="cat-c3" catid='<%# Eval("PropertyID") %>'>
                                    <asp:LoginView ID="LoginView1" runat="server">
                                        <RoleGroups>
                                            <asp:RoleGroup Roles="ProductCategoryModule-Edit">
                                                <ContentTemplate>
                                                    <a href="javascript:{}" class="edit opts" onclick='return UpdateProperty(-1,this);'>
                                                    </a>
                                                </ContentTemplate>
                                            </asp:RoleGroup>
                                        </RoleGroups>
                                    </asp:LoginView>
                                    <asp:LoginView ID="LoginView2" runat="server">
                                        <RoleGroups>
                                            <asp:RoleGroup Roles="ProductCategoryModule-Delete">
                                                <ContentTemplate>
                                                    <a href="javascript:{}" class="delete opts" onclick='return DeleteProperty(this);'>
                                                    </a>
                                                </ContentTemplate>
                                            </asp:RoleGroup>
                                        </RoleGroups>
                                    </asp:LoginView>
                                    <asp:LoginView ID="LoginView3" runat="server">
                                        <RoleGroups>
                                            <asp:RoleGroup Roles="ProductCategoryModule-Add">
                                                <ContentTemplate>
                                                    <a href="javascript:{}" class="add opts" onclick='return AddCategory(window.$selectNodeId,this);'>
                                                    </a>
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
                    <h2 class="row2-property">
                        继承属性列表</h2>
                    <asp:Repeater ID="rpParentProperties" runat="server">
                        <HeaderTemplate>
                            <table id="tbParentProperty" class="subproperty-tb" cellpadding="0" cellspacing="0">
                                <thead>
                                    <tr>
                                        <th>
                                            &nbsp;
                                        </th>
                                        <th>
                                            属性名称
                                        </th>
                                        <th>
                                            所属分类名称
                                        </th>
                                    </tr>
                                </thead>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="cat-c1">
                                    &nbsp;
                                </td>
                                <td class="cat-c2">
                                    <asp:LinkButton ID="lnkParentProperty" runat="server" ToolTip='<%# Eval("PropertyDesc") %>'
                                        PostBackUrl="#"><%# Eval("PropertyName")%></asp:LinkButton>
                                </td>
                                <td class="cat-c3">
                                    <asp:LinkButton ID="lnlParentCategory" runat="server" ToolTip='所属分类' PostBackUrl="#"><%# Eval("CategoryName")%></asp:LinkButton>
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
