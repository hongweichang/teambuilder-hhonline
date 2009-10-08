<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DetailMasterPage.master" AutoEventWireup="true" CodeFile="CompanyPendingEdit.aspx.cs" Inherits="ControlPanel_Users_CompanyPendingEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderDetail" Runat="Server">
<table class="detail" cellspacing="0" cellpadding="0">
        <tr>
            <th style="width: 140px">
                名称
            </th>
            <td>
                <asp:Literal ID="ltCompanyName" runat="server"></asp:Literal>
            </td>
            <th>
                区域
            </th>
            <td>
                <asp:Literal ID="ltRegion" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>
                联系电话
            </th>
            <td>
                <asp:Literal ID="ltPhone" runat="server"></asp:Literal>
            </td>
            <th>
                公司类型
            </th>
            <td>
                <asp:Literal ID="ltCompanyType" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>
                传真
            </th>
            <td>
               <asp:Literal ID="ltFax" runat="server"></asp:Literal>
            </td>
            <th>
                地址
            </th>
            <td>
                <asp:Literal ID="ltAddress" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>
                邮编
            </th>
            <td>
                <asp:Literal ID="ltZipCode" runat="server"></asp:Literal>
            </td>
            <th>
                公司主页
            </th>
            <td>
               <asp:Literal ID="ltWebSite" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>
                组织机构代码
            </th>
            <td>
                <asp:Literal ID="ltOrgCode" runat="server"></asp:Literal>
            </td>
            <th>
                工商注册登记号
            </th>
            <td>
                <asp:Literal ID="ltRegCode" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>
                备注
            </th>
            <td colspan="3">
               <asp:Literal ID="ltRemark" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>申请类型</th>
            <td>
                <asp:Literal ID="ltPendingType" runat="server"></asp:Literal>
            </td>
            <td colspan="2">
                <asp:Button ID="btnUpdateQualify" runat="server" Text="资质文件管理" />
                <asp:Button ID="btnUpdateDeposit" runat="server" Text="保证金管理" />
                <asp:Button ID="btnUpdateCredit" runat="server" Text="信用管理" />
            </td>
        </tr>
        <tr>
            <th>审批是否通过</th>
            <td colspan="3">
                <hc:YesNoRadioButtonList ID="ynblPending" runat="server"></hc:YesNoRadioButtonList>
            </td>
        </tr>
        <tr>
            <th>相关说明</th>
            <td colspan="3">
                <asp:TextBox ID="txtDesc" runat="server" Width="400px" TextMode="MultiLine" Height="50px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                &nbsp;
            </th>
            <td colspan="3">
                <asp:Button ID="btnSave" runat="server" Text="提交" OnClick="btnSave_Click" />
               <asp:Button ID="btnCancel" runat="server" Text="关闭" OnClientClick="return cancel();" CausesValidation="false" />
            </td>
        </tr>
    </table>
</asp:Content>

