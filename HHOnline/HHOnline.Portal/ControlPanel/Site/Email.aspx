<%@ Page Title="" Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master"
    AutoEventWireup="true" CodeFile="Email.aspx.cs" Inherits="ControlPanel_Site_Email" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="ContentOpts" ContentPlaceHolderID="cphOpts" runat="Server">
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="cphContent" runat="Server">
    <table class="postform" cellpadding="10" cellspacing="10">
        <tr>
            <th style="width: 150px;">
                管理员电子邮件
            </th>
            <td>
                <asp:TextBox ID="txtAdminEmail" runat="server" Width="200" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                启用邮件通知
            </th>
            <td>
                <hc:YesNoRadioButtonList ID="rblEnableEmail" runat="server">
                </hc:YesNoRadioButtonList>
            </td>
        </tr>
        <tr>
            <th>
                SMTP服务器
            </th>
            <td>
                <asp:TextBox ID="txtSmtpServer" runat="server" Width="200" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                SMTP使用SSL
            </th>
            <td>
                <hc:YesNoRadioButtonList ID="rblUseSSL" runat="server">
                </hc:YesNoRadioButtonList>
            </td>
        </tr>
        <tr>
            <th>
                SMTP服务器要求登录
            </th>
            <td>
                <hc:YesNoRadioButtonList ID="rblNeedLogin" runat="server">
                </hc:YesNoRadioButtonList>
            </td>
        </tr>
        <tr>
            <th>
                SMTP登录名
            </th>
            <td>
                <asp:TextBox ID="txtSmtpUserName" runat="server" Width="200" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                SMTP密码
            </th>
            <td>
                <asp:TextBox ID="txtSmtpPwd" runat="server" Width="200" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                SMTP端口
            </th>
            <td>
                <asp:TextBox ID="txtSmtpPort" runat="server" Width="200" MaxLength="50"></asp:TextBox>
                <asp:RegularExpressionValidator ID="rev1" ControlToValidate="txtSmtpPort" runat="server"
                    ErrorMessage="端口号应该为数字类型" ValidationExpression="(\d)*"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <th>
                邮件发送限制
            </th>
            <td>
                <asp:TextBox ID="txtEmailThrottle" runat="server" Width="200" MaxLength="50"></asp:TextBox>
                <asp:RegularExpressionValidator ID="rev2" ControlToValidate="txtEmailThrottle"
                    runat="server" ErrorMessage="发送限制应该为数字类型,-1代表无限制" ValidationExpression="(\-)?(\d)*"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <th>
                &nbsp;
            </th>
            <td>
                <hc:MsgBox ID="mbMessage" runat="server" SkinID="msgBox"></hc:MsgBox>
            </td>
        </tr>
        <tr>
            <th>
                &nbsp;
            </th>
            <td>
                <asp:Button ID="btnPost" runat="server" Text="更 新" PostBackUrl="#" Visible="true"
                    OnClick="btnPost_Click" />&nbsp;&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
