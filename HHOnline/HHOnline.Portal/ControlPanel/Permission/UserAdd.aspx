<%@ Page Title="新增用户" Language="C#" MasterPageFile="~/Masters/DetailMasterPage.master" AutoEventWireup="true" CodeFile="UserAdd.aspx.cs" Inherits="ControlPanel_Permission_UserAdd" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderDetail" Runat="Server">
  
        <table cellpadding="0" cellspacing="0" class="detail">
            <tr>
                <th style="width:130px;">用户名(<span style="color:#ff0000">必填</span>)</th>
                <td>
                    <asp:TextBox Width="230px" ID="txtUserName" runat="server" MaxLength="20" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfv1" runat="server" Display="Dynamic" ControlToValidate="txtUserName" ErrorMessage="必须填写！"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="rev11" runat="server" ValidationExpression="([a-zA-Z]{1})([a-zA-Z0-9_]{5,20})" Display="Dynamic" ControlToValidate="txtUserName" ErrorMessage="只能字母数字下划线！"></asp:RegularExpressionValidator>
                </td>
            </tr>            
             <tr>
                <th>昵称(<span style="color:#ff0000">必填</span>)</th>
                <td>
                    <asp:TextBox Width="230px" ID="txtDisplayName" runat="server" Text="" MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="txtDisplayName" ErrorMessage="必须填写！"></asp:RequiredFieldValidator>                
                </td>
            </tr>     
            <tr>
                <th style="width:130px;">密码(<span style="color:#ff0000">必填</span>)</th>
                <td>
                    <asp:TextBox Width="230px" ID="txtPassword" TextMode="Password" AutoCompleteType="None" Text="" runat="server" MaxLength="50" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="txtPassword" ErrorMessage="必须填写！"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th style="width:130px;">密码确认(<span style="color:#ff0000">必填</span>)</th>
                <td>
                    <asp:TextBox Width="230px" ID="txtPasswordConfirm" TextMode="Password" runat="server" MaxLength="50" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txtPasswordConfirm" ErrorMessage="必须填写！"></asp:RequiredFieldValidator>                
                    <asp:CompareValidator Type="String" ID="cv1" runat="server" Display="Dynamic" ControlToValidate="txtPasswordConfirm" ControlToCompare="txtPassword" ErrorMessage="密码不一致！"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <th>是否领导(<span style="color:#4682B4">可选</span>)</th>
                <td>
                    <hc:YesNoRadioButtonList ID="rblManager" runat="server" RepeatLayout="Flow"></hc:YesNoRadioButtonList>
               </td>
            </tr> 
            <tr>
                <th>电子邮件(<span style="color:#4682B4">可选</span>)</th>
                <td>
                    <asp:TextBox Width="230px" ID="txtEmail" runat="server" MaxLength="200"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="rev1" runat="server" 
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                        ControlToValidate="txtEmail" ErrorMessage="格式不正确！"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <th>手机号码(<span style="color:#4682B4">可选</span>)</th>
                <td>
                    <asp:TextBox Width="230px" ID="txtMobile" runat="server" MaxLength="50"></asp:TextBox>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ValidationExpression="^((\+86)|(86))?(13|15|18|10)\d{9}$" 
                        ControlToValidate="txtMobile" ErrorMessage="格式不正确！"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <th>固定电话(<span style="color:#4682B4">可选</span>)</th>
                <td>
                    <asp:TextBox Width="230px" ID="txtPhone" runat="server" MaxLength="50"></asp:TextBox>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                        ValidationExpression="(\(\d{3,4}\)|\d{3,4}-|\s)?\d{8}" 
                        ControlToValidate="txtPhone" ErrorMessage="格式不正确！"></asp:RegularExpressionValidator>
                </td>
            </tr> 
            <tr>
                <th>传真(<span style="color:#4682B4">可选</span>)</th>
                <td>
                    <asp:TextBox Width="230px" ID="txtFax" runat="server" MaxLength="50"></asp:TextBox>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                        ValidationExpression="(\(\d{3,4}\)|\d{3,4}-|\s)?\d{8}" 
                        ControlToValidate="txtFax" ErrorMessage="格式不正确！"></asp:RegularExpressionValidator>
                </td>
            </tr>        
            <tr>
                <th >所属部门(<span style="color:#4682B4">已选</span>)</th>
                <td><asp:Literal ID="ltDeptName" runat="server"></asp:Literal></td>
            </tr>   
            <tr>
                <th>职务(<span style="color:#4682B4">可选</span>)</th>
                <td>
                    <asp:TextBox Width="230px" ID="txtTitle" runat="server" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>备注(<span style="color:#4682B4">可选</span>)</th>
                <td>
                    <asp:TextBox Width="230px" ID="txtMemo" runat="server" MaxLength="200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="text-align:left;height:20px;">
                    <hc:MsgBox ID="mbMsg" runat="server" SkinID="msgBox"></hc:MsgBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:center">
                    <asp:Button ID="btnPost" runat="server" Text=" 增 加 " OnClick="btnPost_Click" PostBackUrl="#"></asp:Button>
                    <asp:Button ID="btnClose" runat="server" Text=" 关 闭 " CausesValidation="false" OnClientClick="return cancel();"></asp:Button>
                </td>
            </tr>
        </table>
    </asp:Content>