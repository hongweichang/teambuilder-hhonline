<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DetailMasterPage.master" AutoEventWireup="true" CodeFile="FriendLinkAdd.aspx.cs" Inherits="ControlPanel_Common_FriendLinkAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderDetail" Runat="Server">
<table class="detail" cellpadding="0" cellspacing="0">
    <tr>
        <th>标题(<span class="needed">必填</span>)</th>
        <td><asp:TextBox ID="txtTitle" Width="250px" runat="server" MaxLength="150"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtTitle" Display="Dynamic" ErrorMessage="必须填写！" Text=""></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th>重要性(<span class="unneeded">可选</span>)</th>
        <td>
            <asp:DropDownList ID="ddlRank" runat="server" Width="50px">
                <asp:ListItem Text="1" Value="1" Selected="True"></asp:ListItem>
                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                <asp:ListItem Text="6" Value="6"></asp:ListItem>
                <asp:ListItem Text="7" Value="7"></asp:ListItem>
                <asp:ListItem Text="8" Value="8"></asp:ListItem>
                <asp:ListItem Text="9" Value="9"></asp:ListItem>
                <asp:ListItem Text="10" Value="10"></asp:ListItem>
            </asp:DropDownList>
            (值越高，重要性越高)
        </td>
    </tr>
    <tr>
        <th>链接(<span class="needed">必填</span>)</th>
        <td><asp:TextBox ID="txtLink" Width="250px" runat="server" MaxLength="250"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfv2" Display="Dynamic" runat="server" ControlToValidate="txtLink" ErrorMessage="必须填写！" Text=""></asp:RequiredFieldValidator>
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

