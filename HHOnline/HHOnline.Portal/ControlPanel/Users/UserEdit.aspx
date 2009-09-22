<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DetailMasterPage.master" AutoEventWireup="true" CodeFile="UserEdit.aspx.cs" Inherits="ControlPanel_Users_UserEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderDetail" Runat="Server">
<table cellpadding="0" cellspacing="0" class="detail">
   <tr>
       <th style="width:140px">登录名(<span class="needed" >必填</span>)</th>
        <td style="width:300px;" id="validateName">
            <asp:TextBox style="float:left;" ID="txtLoginName" runat="server" MaxLength="50"></asp:TextBox>
            <span id="msgTip" ></span>
            <asp:RequiredFieldValidator ID="rvrf1" runat="server" ControlToValidate="txtLoginName"
                    runat="server" ErrorMessage="必须填写！" Display="Dynamic"></asp:RequiredFieldValidator>
        </td>
        <td><span class="tip">登录名作为用户登录时的凭证！</span></td>
    </tr>
    <tr>
        <th>密码(<span class="needed" >必填</span>)</th>
        <td id="tdPassword">
            <asp:TextBox TextMode="Password" MaxLength="25"  style="float:left;" ID="txtPassword" runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" Display="Dynamic"
                ValidationExpression="^[0-9a-zA-z_]{8,25}$" ControlToValidate="txtPassword" ErrorMessage="长度为8~25位,由数字字母和下划线组成！"></asp:RegularExpressionValidator>
        </td>
        <td><span class="tip"><asp:Literal ID="ltPwdDesc" runat="server"></asp:Literal></span></td>
    </tr>
    <tr>
        <th>重复密码(<span class="needed" >必填</span>)</th>
        <td>
            <asp:TextBox TextMode="Password" ID="txtConfirmPassword" runat="server"></asp:TextBox>
            <asp:CompareValidator ID="cv1" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword"
                     ErrorMessage="两次密码不一致！"></asp:CompareValidator>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <th>显示名称(<span class="needed" >必填</span>)</th>
        <td>
            <asp:TextBox ID="txtDisplayName" runat="server" MaxLength="100"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDisplayName"
                    ErrorMessage="必须填写！" Display="Dynamic"></asp:RequiredFieldValidator>
        </td>
        <td><span class="tip">显示名称设定后不可更改！</span></td>
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
        <td><span class="tip">密码提示问题作为找回用户密码的关键凭证！</span></td>
    </tr>
    <tr>
        <th>密码提示答案(<span class="needed" >必填</span>)</th>
        <td>
            <asp:TextBox Width="230px" ID="txtAnswer" MaxLength="250" runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" Display="Dynamic"
                ValidationExpression="\S{4,250}" ControlToValidate="txtAnswer" ErrorMessage="长度为4~250个字符,不包含空格！"></asp:RegularExpressionValidator>                    
        </td>
        <td><span class="tip"><asp:Literal ID="ltPADesc" runat="server"></asp:Literal></span></td>
    </tr>
     <tr>
        <th>是否领导(<span class="needed" >必填</span>)</th>
        <td>
            <hc:YesNoRadioButtonList ID="rblManager" runat="server" SelectedValue="false"></hc:YesNoRadioButtonList>
        </td>
        <td><span class="tip">领导能查看同一公司其他员工的订单！</span></td>
    </tr>
    <tr>
        <th>电子邮件(<span class="unneeded" >可选</span>)</th>
        <td>
            <asp:TextBox Width="230px" ID="txtEmail" runat="server" MaxLength="200"></asp:TextBox>
            <asp:RegularExpressionValidator ID="rev1" runat="server" Display="Dynamic"
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                ControlToValidate="txtEmail" ErrorMessage="格式不正确！"></asp:RegularExpressionValidator>
        </td>
        <td><span class="tip">需要提供正确格式的Email账号！</span></td>
    </tr>
    <tr>
        <th>手机号码(<span class="unneeded" >可选</span>)</th>
        <td>
            <asp:TextBox Width="230px" ID="txtMobile" runat="server" MaxLength="50"></asp:TextBox>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                ValidationExpression="^((\+86)|(86))?(13|15|18|10)\d{9}$" Display="Dynamic"
                ControlToValidate="txtMobile" ErrorMessage="格式不正确！"></asp:RegularExpressionValidator>
        </td>
        <td><span class="tip">13位手机号码，可使用"+86"或"86"前缀！</span></td>
    </tr>
    <tr>
        <th>固定电话(<span class="unneeded" >可选</span>)</th>
        <td>
            <asp:TextBox Width="230px" ID="txtPhone" runat="server" MaxLength="50"></asp:TextBox>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                ValidationExpression="(\(\d{3,4}\)|\d{3,4}-|\s)?\d{7,8}"  Display="Dynamic"
                ControlToValidate="txtPhone" ErrorMessage="格式不正确！"></asp:RegularExpressionValidator>
        </td>
         <td><span class="tip">格式形如"区号-号码"或"(区号)号码"！</span></td>
    </tr> 
    <tr>
        <th>传真(<span class="unneeded" >可选</span>)</th>
        <td>
            <asp:TextBox Width="230px" ID="txtFax" runat="server" MaxLength="50"></asp:TextBox>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                ValidationExpression="(\(\d{3,4}\)|\d{3,4}-|\s)?\d{7,8}"  Display="Dynamic"
                ControlToValidate="txtFax" ErrorMessage="格式不正确！"></asp:RegularExpressionValidator>
        </td>
         <td><span class="tip">格式要求同上！</span></td>
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
        <td colspan="2">
            <asp:TextBox Width="530px" ID="txtMemo" runat="server" MaxLength="200"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <th>
            &nbsp;
        </th>
        <td colspan="2">
           <asp:Button ID="btnEdit" runat="server" Text="保存" CausesValidation="true" OnClick="btnEdit_Click" />
           <asp:Button ID="btnPostBack" runat="server" Text="返回" CausesValidation="false" OnClick="btnPostBack_Click" />
           <asp:Button ID="btnCancel" runat="server" Text="关闭" OnClientClick="return cancel();" CausesValidation="false" />
        </td>
    </tr>
</table>
</asp:Content>

