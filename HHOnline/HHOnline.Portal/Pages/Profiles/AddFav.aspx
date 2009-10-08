<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DetailMasterPage.master" AutoEventWireup="true" CodeFile="AddFav.aspx.cs" Inherits="Pages_Profiles_AddFav" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderDetail" Runat="Server">

<table class="detail" cellspacing="0" cellpadding="0">
    <tr>
        <th style="width:140px;">标题</th>
        <td><asp:TextBox ID="txtTitle" runat="server" Width="300px" MaxLength="100"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtTitle" ErrorMessage="必须填写！" Display="Dynamic"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th>
            级别
        </th>
        <td>
            <div id="divRate"></div>
            <asp:HiddenField ID="hfRate" runat="server" Value="1" />
        </td>
    </tr>
    <tr>
        <th>描述</th>
        <td><asp:TextBox ID="txtDesc" runat="server" Width="300px" TextMode="MultiLine" Height="50px" MaxLength="300" Display="Dynamic"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDesc" ErrorMessage="必须填写！"></asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <th>&nbsp;</th>
        <td>
            <asp:Button ID="btnSave" runat="server" Text=" 保存 " OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" Text=" 取消 " CausesValidation="false" OnClientClick="return cancel();" />
        </td>
    </tr>
</table>
</asp:Content>

