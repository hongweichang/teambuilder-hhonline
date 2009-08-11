<%@ Page Title="新增用户" Language="C#" MasterPageFile="~/Masters/DetailMasterPage.master"  AutoEventWireup="true" CodeFile="UserUpdate.aspx.cs" Inherits="ControlPanel_Permission_UserUpdate" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderDetail" Runat="Server">
  
        <table cellpadding="0" cellspacing="0" class="detail">
            <tr>
                <th style="width:130px;">用户名(<span style="color:#ff0000">必填</span>)</th>
                <td>
                    <asp:Literal ID="ltUserName" runat="server"></asp:Literal>   
                </td>
            </tr>            
             <tr>
                <th>昵称(<span style="color:#ff0000">必填</span>)</th>
                <td>
                    <asp:TextBox Width="230px" ID="txtDisplayName" runat="server" MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="txtDisplayName" ErrorMessage="必须填写！"></asp:RequiredFieldValidator>                
                </td>
            </tr>  
            <tr>
                <th>用户状态(<span style="color:#ff0000">必填</span>)</th>
                <td>
                    <hc:AccountStatusList ID="aslStatus" runat="server" Width="240px"></hc:AccountStatusList>  
                </td>
            </tr>   
            <tr>
                <th style="width:130px;">密码(<span style="color:#ff0000">空保持原值</span>)</th>
                <td>
                    <asp:TextBox Width="230px" ID="txtPassword" TextMode="Password" runat="server" MaxLength="50" ></asp:TextBox>
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
                <td>
                    <asp:DropDownList Width="240px" ID="dlDepartment" runat="server" ></asp:DropDownList>
                </td>
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
                    <asp:Button ID="btnPost" runat="server" Text=" 更新 " OnClick="btnPost_Click" PostBackUrl="#"></asp:Button>
                    <asp:Button ID="btnClose" runat="server" Text=" 关 闭 " CausesValidation="false" OnClientClick="return cancel()"></asp:Button>
                </td>
            </tr>
        </table>
        </asp:Content>
