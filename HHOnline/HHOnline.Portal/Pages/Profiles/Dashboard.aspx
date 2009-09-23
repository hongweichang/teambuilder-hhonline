<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/AdminMasterPage.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Pages_Profiles_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphOpts" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">
<table class="postform" cellspacing="10" cellpadding="10">
    <tr>
        <th style="width:140px">登录名</th>
        <td><asp:Literal ID="lblLoginName" runat="server"></asp:Literal></td>
    </tr>
    <tr>
        <th>显示名称(<span class="needed" >必填</span>)</th>
        <td>
            <asp:TextBox ID="txtDisplayName" runat="server" MaxLength="100"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDisplayName"
                    ErrorMessage="必须填写！" Display="Dynamic"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th>&nbsp;</th>
        <td>
           <a class="blueStyle" href="javascript:{}" onclick="changePwd()">[修改密码]</a>
        </td>
    </tr>
    <tr>
        <th>密码提示问题(<span class="needed" >必填</span>)</th>
        <td>
            <asp:TextBox Width="230px" ID="txtQuestion" runat="server" MaxLength="250"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtQuestion"
                    ErrorMessage="必须填写！" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" Display="Dynamic"
                ValidationExpression="\S{6,250}" ControlToValidate="txtQuestion" ErrorMessage="长度为6~250个字符,不包含空格！"></asp:RegularExpressionValidator>                    
        </td>
    </tr>
    <tr>
        <th>&nbsp;</th>
        <td>
           <a class="blueStyle" href="javascript:{}" onclick="changeQA()">[修改密码提示答案]</a>
        </td>
    </tr>
    <tr>
        <th>电子邮件(<span class="unneeded" >可选</span>)</th>
        <td>
            <asp:TextBox Width="230px" ID="txtEmail" runat="server" MaxLength="200"></asp:TextBox>
            <asp:RegularExpressionValidator ID="rev1" runat="server" Display="Dynamic"
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                ControlToValidate="txtEmail" ErrorMessage="格式不正确！"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <th>手机号码(<span class="unneeded" >可选</span>)</th>
        <td>
            <asp:TextBox Width="230px" ID="txtMobile" runat="server" MaxLength="50"></asp:TextBox>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                ValidationExpression="^((\+86)|(86))?(13|15|18|10)\d{9}$" Display="Dynamic"
                ControlToValidate="txtMobile" ErrorMessage="格式不正确,13位手机号码，可使用+86或86前缀！！"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <th>固定电话(<span class="unneeded" >可选</span>)</th>
        <td>
            <asp:TextBox Width="230px" ID="txtPhone" runat="server" MaxLength="50"></asp:TextBox>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                ValidationExpression="(\(\d{3,4}\)|\d{3,4}-|\s)?\d{7,8}"  Display="Dynamic"
                ControlToValidate="txtPhone" ErrorMessage="格式形如 区号-号码 或 (区号)号码！"></asp:RegularExpressionValidator>
        </td>
    </tr> 
    <tr>
        <th>传真(<span class="unneeded" >可选</span>)</th>
        <td>
            <asp:TextBox Width="230px" ID="txtFax" runat="server" MaxLength="50"></asp:TextBox>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                ValidationExpression="(\(\d{3,4}\)|\d{3,4}-|\s)?\d{7,8}"  Display="Dynamic"
                ControlToValidate="txtFax" ErrorMessage="格式形如 区号-号码 或 (区号)号码！"></asp:RegularExpressionValidator>
        </td>
    </tr> 
    <tr>
        <th>职务(<span class="unneeded" >可选</span>)</th>
        <td>
            <asp:TextBox Width="230px" ID="txtTitle" runat="server" MaxLength="50"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <th>备注(<span class="unneeded" >可选</span>)</th>
        <td>
            <asp:TextBox Width="530px" ID="txtMemo" runat="server" MaxLength="200"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <th>&nbsp;</th>
        <td><asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text=" 保存 " /></td>
    </tr>
</table>
</asp:Content>

