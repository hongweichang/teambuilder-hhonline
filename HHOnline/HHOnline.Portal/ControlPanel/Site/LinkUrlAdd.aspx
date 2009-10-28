<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DetailMasterPage.master" AutoEventWireup="true" CodeFile="LinkUrlAdd.aspx.cs" Inherits="ControlPanel_Site_LinkUrlAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderDetail" Runat="Server">
<table class="detail" cellpadding="0" cellspacing="0">
    <tr>
        <th style="width:70px;">标题(<span class="needed">必填</span>)</th>
        <td><asp:TextBox ID="txtTitle" Width="300px" runat="server" MaxLength="150"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfv1" runat="server" Display="Dynamic" ControlToValidate="txtTitle" ErrorMessage="必须填写！" Text=""></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th>描述(<span class="unneeded">可选</span>)</th>
        <td><asp:TextBox ID="txtDesc" Width="300px" runat="server" MaxLength="200" TextMode="MultiLine" Height="50"></asp:TextBox></td>
    </tr>
    <tr>
        <th>链接(<span class="needed">必填</span>)</th>
        <td><asp:TextBox ID="txtLink" Width="300px" runat="server" MaxLength="250"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfv2" runat="server" Display="Dynamic" ControlToValidate="txtLink" ErrorMessage="必须填写！" Text=""></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th>&nbsp;</th>
        <td>
            <asp:Button ID="btnPost" runat="server" OnClick="btnPost_Click" Text="保存" />
            <asp:Button ID="btnCancel" runat="server" Text="关闭" CausesValidation="false" OnClientClick="return cancel();" />
        </td>
    </tr>
</table>
</asp:Content>

