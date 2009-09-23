<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DetailMasterPage.master" AutoEventWireup="true" CodeFile="ChangePwd.aspx.cs" Inherits="Pages_Profiles_ChangePwd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderDetail" Runat="Server">
<table class="detail" cellpadding="0" cellspacing="0">
    <tr>
        <th style="width:150px">原密码</th>
        <td>
            <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfv1" runat="server" Display="Dynamic" ControlToValidate="txtOldPassword" ErrorMessage="必须填写!"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th>新密码</th>
        <td>
            <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password"></asp:TextBox>
            
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="txtNewPassword" ErrorMessage="必须填写!"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" Display="Dynamic"
                ValidationExpression="^[0-9a-zA-z_]{8,25}$" ControlToValidate="txtNewPassword" ErrorMessage="长度为8~25位,由数字字母和下划线组成！"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <th>重复密码</th>
        <td>
            <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
            <asp:CompareValidator ID="cv1" runat="server" ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmPassword"
             ErrorMessage="密码必须一致！"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <th>&nbsp;</th>
        <td>
            <asp:Button ID="btnChange" runat="server" Text="更改" OnClick="btnChange_Click"  />
            <asp:Button ID="btnCancel" runat="server" Text="取消" OnClientClick="return cancel();" />
        </td>
    </tr>
</table>
</asp:Content>

