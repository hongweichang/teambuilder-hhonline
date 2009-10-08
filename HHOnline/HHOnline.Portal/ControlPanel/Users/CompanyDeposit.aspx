<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DetailMasterPage.master" AutoEventWireup="true" CodeFile="CompanyDeposit.aspx.cs" Inherits="ControlPanel_Users_CompanyDeposit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderDetail" Runat="Server">

<table class="detail" cellspacing="0" cellpadding="0">
    <tr>
        <th colspan="2" style="text-align:center">保证金信息</th>
    </tr>
    <tr>
        <th>保证金缴纳日期</th>
        <td>
            <asp:TextBox ID="txtDate" runat="server" rel="datepicker"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDate"
                    ValidationExpression="\d{4}年\d{1,2}月\d{1,2}日" ErrorMessage="必须为日期格式 - yyyy年MM月dd日！"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <th style ="width:140px">类型</th>
        <td>
            <asp:DropDownList ID="ddlType" runat="server">
                <asp:ListItem Selected="True" Text="=请选择=" Value="None"></asp:ListItem>
                <asp:ListItem Text="代理保证金" Value="1"></asp:ListItem>
                <asp:ListItem Text="供应商保证金" Value="2"></asp:ListItem>
                <asp:ListItem Text="混合保证金" Value="3"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv11" runat="server" ErrorMessage="请选择类型！" ControlToValidate="ddlType" InitialValue="None"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th>保证金增量值(元)</th>
        <td>
            <asp:TextBox ID="txtDelta" runat="server" MaxLength="30"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfv1" runat="server" Display="Dynamic" ErrorMessage="必须填写！" ControlToValidate="txtDelta"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="rvf1" runat="server" Display="Dynamic" ErrorMessage="只能填写数字！" ControlToValidate="txtDelta"
                 ValidationExpression="\d{1,30}"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <th>保证金余额(元)</th>
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

