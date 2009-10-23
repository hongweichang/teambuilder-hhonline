<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DetailMasterPage.master" AutoEventWireup="true" CodeFile="SearchWordAdd.aspx.cs" Inherits="ControlPanel_Site_SearchWordAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderDetail" Runat="Server">
<table class="detail" cellpadding="0" cellspacing="0">
    <tr>
        <th style="width:140px;">关键词</th>
        <td>
            <asp:TextBox ID="txtSearchWord" runat="server" MaxLength="50"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfv1" runat="server" Display="Dynamic" ControlToValidate="txtSearchWord" ErrorMessage="必须填写！" Text=""></asp:RequiredFieldValidator>
        </td>
    </tr>
     <tr>
        <th>搜索次数</th>
        <td>
            <asp:TextBox ID="txtCount" runat="server" MaxLength="10" Text="0"></asp:TextBox>
            <asp:RegularExpressionValidator ID="rev1" runat="server" ErrorMessage="只能输入正整数！" Text="" Display="Dynamic"
                         ValidationExpression="^[0-9]*[1-9][0-9]*$" ControlToValidate="txtCount" ></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <th>&nbsp;</th>
        <td>
            <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Text=" 保存 " />
            <asp:Button ID="btnCancel" runat="server" Text=" 取消 " CausesValidation="false" OnClientClick="return cancel()" />
        </td>
    </tr>
</table>
</asp:Content>

