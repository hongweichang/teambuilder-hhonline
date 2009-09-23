<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DetailMasterPage.master" AutoEventWireup="true" CodeFile="ChangeQA.aspx.cs" Inherits="Pages_Profiles_ChangeQA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderDetail" Runat="Server">
<table class="detail" cellpadding="0" cellspacing="0">
    <tr>
        <th style="width:150px">问题</th>
        <td>
            <asp:Literal ID="ltQuestion" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <th>原答案</th>
        <td>
            <asp:TextBox Width="230px" ID="txtOldAnswer" runat="server" MaxLength="250"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" Display="Dynamic"
                ValidationExpression="\S{4,250}" ControlToValidate="txtOldAnswer" ErrorMessage="长度为4~250个字符,不包含空格！"></asp:RegularExpressionValidator>                    
        </td>
    </tr>
    <tr>
        <th>新的答案</th>
        <td>
            <asp:TextBox Width="230px" ID="txtNewAnswer" runat="server" MaxLength="250"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic"
                ValidationExpression="\S{4,250}" ControlToValidate="txtNewAnswer" ErrorMessage="长度为4~250个字符,不包含空格！"></asp:RegularExpressionValidator>                    
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

