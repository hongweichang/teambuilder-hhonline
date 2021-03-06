﻿<%@ Page Title="新增部门" Language="C#" MasterPageFile="~/Masters/DetailMasterPage.master" AutoEventWireup="true" CodeFile="OrganizeAdd.aspx.cs" Inherits="ControlPanel_Permission_OrganizeAdd" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderDetail" Runat="Server">

        <table cellpadding="0" cellspacing="0" class="detail">
            <tr>
                <th>上级部门</th>
                <td><asp:Literal  ID="ltParDept" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <th>上级部门描述</th>
                <td><asp:Literal  ID="ltParDeptDesc" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <th >部门名称</th>
                <td><asp:TextBox ID="txtDeptName" runat="server" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv1" Display="Dynamic" runat="server" ControlToValidate="txtDeptName" ErrorMessage="必须填写！"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>部门描述</th>
                <td><asp:TextBox ID="txtDeptDesc" runat="server" Width="300px" MaxLength="200"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ControlToValidate="txtDeptDesc" ErrorMessage="必须填写！"></asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr>
                <td>&nbsp;</td>
                <td style="text-align:left;height:20px;">
                    <hc:MsgBox ID="mbMsg" runat="server" SkinID="msgBox"></hc:MsgBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:center">
                    <asp:Button ID="btnPost" runat="server" Text=" 增 加 " OnClick="btnPost_Click" PostBackUrl="#"></asp:Button>
                    <asp:Button ID="btnClose" runat="server" Text=" 关 闭 " CausesValidation="false" OnClientClick="return cancel();"></asp:Button>
                </td>
            </tr>
        </table>
    </asp:Content>
