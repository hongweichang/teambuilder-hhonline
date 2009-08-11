<%@ Page Title="" Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master" AutoEventWireup="true" CodeFile="RoleAdd.aspx.cs" Inherits="ControlPanel_Permission_RoleAdd" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="ContentOpts" ContentPlaceHolderID="cphOpts" Runat="Server">
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="cphContent" Runat="Server">
<table class="postform" cellpadding="10" cellspacing="10">
    <tr>
        <th style="width:50px;">角色名称</th>
        <td>
            <asp:TextBox ID="txtRoleName" runat="server" Width="200" MaxLength="50"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="角色名称不能为空。" Display="Dynamic" ControlToValidate="txtRoleName"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th>角色描述</th>
        <td>
            <asp:TextBox ID="txtRoleDesc" runat="server" Width="500" MaxLength="200"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfv2" runat="server" ErrorMessage="角色描述不能为空。" Display="Dynamic" ControlToValidate="txtRoleDesc"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th valign="top" style="padding-top:5px;">角色权限</th>
        <td>
            <div style="height:300px;overflow:auto;width:500px" class="normalbg">
                <asp:TreeView CssClass="treeviewpermission" ID="tvPermission" runat="server" ShowLines="true"></asp:TreeView>
            </div>
        </td>
    </tr>
    <tr>
        <th>&nbsp;</th>
        <td><hc:MsgBox ID="mbMessage" runat="server" SkinID="msgBox"></hc:MsgBox></td>
    </tr>
    <tr>
        <th>&nbsp; </th>
        <td><asp:Button ID="btnPost" runat="server" Text="提 交" PostBackUrl="#" Visible="false" OnClick="btnPost_Click" />&nbsp;&nbsp;
        <asp:Button ID="btnPostBack" runat="server"  Text="返 回" Visible="false" CausesValidation="false" /></td>
    </tr>
</table>
</asp:Content>

