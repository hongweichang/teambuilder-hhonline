<%@ Page Title="" Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master" AutoEventWireup="true" CodeFile="UserRole.aspx.cs" Inherits="ControlPanel_Permission_UserRole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphOpts" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">
<asp:Repeater ID="rpRoles" runat="server">
    <ItemTemplate>
        <div class="rolerepeat"><label ><%# Eval("RoleName") %></label><a href="javascript:void(0)" onclick='UserSelect(<%# Eval("RoleID") %>)'>选择用户</a></div>
    </ItemTemplate>
 </asp:Repeater>
</asp:Content>

