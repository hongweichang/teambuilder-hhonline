<%@ Page Title="用户信息" Language="C#" MasterPageFile="~/Masters/DetailMasterPage.master" AutoEventWireup="true" CodeFile="UserDetail.aspx.cs" Inherits="ControlPanel_Permission_UserDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderDetail" Runat="Server">
<asp:Repeater ID="rpUser" runat="server" >
    <ItemTemplate>
         <table cellpadding="0" cellspacing="0"  class="detail">
                <tr>
                    <th style="width:20%">用户名</th>
                    <td style="width:30%"><%# Eval("UserName") %></td>
                    <th style="width:20%">昵称</th>
                    <td style="width:30%"><%# Eval("DisplayName") %></td>
                </tr>
                 <tr>
                    <th>是否领导</th>
                    <td><%# GetYesNo(Eval("IsManager").ToString().ToLower())%></td>
                    <th>电子邮件</th>
                    <td><%# ParseString(Eval("Email").ToString())%></td>
                </tr>
                 <tr>
                    <th>手机号码</th>
                    <td><%# ParseString(Eval("Mobile").ToString())%></td>
                    <th>固定电话</th>
                    <td><%# ParseString(Eval("Phone").ToString())%></td>
                </tr>
                 <tr>
                    <th>传真号码</th>
                    <td><%# ParseString(Eval("Fax").ToString())%></td>
                    <th>用户状态</th>
                    <td><%# GetUserStatus(Eval("AccountStatus").ToString())%></td>
                </tr>
                 <tr>
                    <th>所属部门</th>
                    <td><%# Eval("Department") %></td>
                    <th>职务</th>
                    <td><%# ParseString(Eval("Title").ToString())%></td>
                </tr>
                 <tr>
                    <th>备注</th>
                    <td colspan="3"><%# ParseString(Eval("Remark").ToString())%></td>
                </tr>
                 <tr>
                    <th>最近一次锁定</th>
                    <td><%# ParseDateTime(Eval("LastLockonDate").ToString())%></td>
                    <th>最近一次登录</th>
                    <td><%#  ParseDateTime(Eval("LastActiveDate").ToString()) %></td>
                </tr>
                 <tr>
                    <th>创建时间</th>
                    <td><%#  ParseDateTime(Eval("CreateTime").ToString()) %></td>
                    <th>创建用户</th>
                    <td><%# GetUserName(Eval("CreateUser").ToString())%></td>
                </tr>
                <tr>
                    <th>更新时间</th>
                    <td><%#  ParseDateTime(Eval("UpdateTime").ToString()) %></td>
                    <th>更新用户</th>
                    <td><%#  GetUserName(Eval("UpdateUser").ToString())%></td>
                </tr>
        </table>
    </ItemTemplate>
</asp:Repeater>
</asp:Content>

