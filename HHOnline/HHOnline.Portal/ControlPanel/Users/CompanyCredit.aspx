<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DetailMasterPage.master" AutoEventWireup="true" CodeFile="CompanyCredit.aspx.cs" Inherits="ControlPanel_Users_CompanyCredit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderDetail" Runat="Server">

<table class="detail" cellspacing="0" cellpadding="0">
    <tr>
        <th colspan="2" style="text-align:center">信用信息</th>
    </tr>
    <tr>
        <th>信用记录日期</th>
        <td>
            <asp:TextBox ID="txtDate" runat="server" rel="datepicker"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDate"
                    ValidationExpression="\d{4}年\d{1,2}月\d{1,2}日" ErrorMessage="必须为日期格式 - yyyy年MM月dd日！"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <th>信用增量值</th>
        <td>
            <asp:TextBox ID="txtDelta" runat="server" MaxLength="30"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfv1" runat="server" Display="Dynamic" ErrorMessage="必须填写！" ControlToValidate="txtDelta"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="rvf1" runat="server" Display="Dynamic" ErrorMessage="只能填写数字！" ControlToValidate="txtDelta"
                 ValidationExpression="\d{1,30}"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <th>当前信用值</th>
        <td>
            <asp:TextBox ID="txtAmount" runat="server" MaxLength="30"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="必须填写！" ControlToValidate="txtAmount"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic" ErrorMessage="只能填写数字！" ControlToValidate="txtAmount"
                 ValidationExpression="\d{1,30}"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <th>描述(<span class="unneeded">可选</span>)</th>
        <td>
            <asp:TextBox ID="txtDesc" runat="server" Width="500px" MaxLength="100"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <th>&nbsp;</th>
        <td><asp:Literal ID="ltCreateInfo" runat="server"></asp:Literal></td>
    </tr>
    <tr>
        <th>&nbsp;</th>
        <td>
            <hc:MsgBox ID="mbMsg" runat="server" SkinID="msgBox"></hc:MsgBox>
        </td>
    </tr>
    <tr>
        <th>&nbsp;</th>
        <td>
            <asp:Button ID="btnSave" runat="server" Text=" 保存 " OnClick="btnSave_Click" />
            <asp:Button ID="btnBack" runat="server" Text=" 返回 " CausesValidation="false" />
        </td>
    </tr>
</table>
</asp:Content>

