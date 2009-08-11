<%@ Page Title="角色详细信息" Language="C#" MasterPageFile="~/Masters/DetailMasterPage.master" AutoEventWireup="true" CodeFile="RoleDetail.aspx.cs" Inherits="ControlPanel_Permission_RoleDetail" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderDetail" Runat="Server">
<asp:Repeater ID="rpRole" runat="server" >
    <ItemTemplate>
         <table cellpadding="0" cellspacing="0" class="detail">
                <tr>
                    <th>角色名称</th>
                    <td><%# Eval("RoleName") %></td>
                </tr>
                <tr>
                    <th>角色描述</th>
                    <td><%# Eval("Description") %></td>
                </tr>
                <tr>
                    <th>创建用户</th>
                    <td><%# GetUserName( Eval("CreateUser").ToString()) %></td>
                </tr>
                <tr>
                    <th>创建时间</th>
                    <td><%# Eval("CreateTime") %></td>
                </tr>
                <tr>
                    <th>更新用户</th>
                    <td><%# GetUserName( Eval("UpdateUser").ToString()) %></td>
                </tr>
                <tr>
                    <th>最近更新</th>
                    <td><%# Eval("UpdateTime") %></td>
                </tr>
        </table>
    </ItemTemplate>
</asp:Repeater>
</asp:Content>
