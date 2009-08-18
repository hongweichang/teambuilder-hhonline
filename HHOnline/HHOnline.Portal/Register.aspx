<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/HomeMasterPage.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderSearch" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderContent" Runat="Server">
<div class="registerContent">
    <asp:SiteMapPath ID="smRegister" runat="server" SkinID="mappath"></asp:SiteMapPath>
    <fieldset class="fsNormal"><legend>联系人信息</legend>        
        <table class="postform" cellspacing="5">
            <tr>
                <th style="width:140px">登录名(<span class="needed" >必填</span>)</th>
                <td style="width:300px;" id="validateName">
                    <asp:TextBox style="float:left;" ID="txtLoginName" runat="server"></asp:TextBox>
                    <span id="msgTip" ></span>
                    <asp:RequiredFieldValidator ID="rvrf1" runat="server" ControlToValidate="txtLoginName"
                            runat="server" ErrorMessage="必须填写！" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
                <td><span class="tip">登录名作为用户登录时的凭证！</span></td>
            </tr>
            <tr>
                <th>密码(<span class="needed" >必填</span>)</th>
                <td id="tdPassword"><asp:TextBox TextMode="Password"  style="float:left;" ID="txtPassword" runat="server"></asp:TextBox></td>
                <td><span class="tip">请尽量设置较复杂密码！</span></td>
            </tr>
            <tr>
                <th>重复密码(<span class="needed" >必填</span>)</th>
                <td><asp:TextBox TextMode="Password" ID="TextBox5" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <th>显示名称(<span class="needed" >必填</span>)</th>
                <td><asp:TextBox ID="TextBox6" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <th>密码提示问题(<span class="needed" >必填</span>)</th>
                <td><asp:TextBox ID="TextBox8" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <th>密码提示答案(<span class="needed" >必填</span>)</th>
                <td><asp:TextBox ID="TextBox9" runat="server"></asp:TextBox></td>
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
                        ValidationExpression="^((\+86)|(86))?(13|15|18|10)\d{9}$" 
                        ControlToValidate="txtMobile" ErrorMessage="格式不正确！"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <th>固定电话(<span class="unneeded" >可选</span>)</th>
                <td>
                    <asp:TextBox Width="230px" ID="txtPhone" runat="server" MaxLength="50"></asp:TextBox>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                        ValidationExpression="(\(\d{3,4}\)|\d{3,4}-|\s)?\d{8}" 
                        ControlToValidate="txtPhone" ErrorMessage="格式不正确！"></asp:RegularExpressionValidator>
                </td>
            </tr> 
            <tr>
                <th>传真(<span class="unneeded" >可选</span>)</th>
                <td>
                    <asp:TextBox Width="230px" ID="txtFax" runat="server" MaxLength="50"></asp:TextBox>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                        ValidationExpression="(\(\d{3,4}\)|\d{3,4}-|\s)?\d{8}" 
                        ControlToValidate="txtFax" ErrorMessage="格式不正确！"></asp:RegularExpressionValidator>
                </td>
            </tr>        
            <tr>
                <th >所属部门(<span class="unneeded" >已选</span>)</th>
                <td> <asp:TextBox Width="230px" ID="TextBox7" runat="server" MaxLength="50"></asp:TextBox></td>
            </tr>   
            <tr>
                <th>职务(<span class="unneeded" >可选</span>)</th>
                <td>
                    <asp:TextBox Width="230px" ID="txtTitle" runat="server" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>备注(<span class="unneeded" >可选</span>)</th>
                <td>
                    <asp:TextBox Width="230px" ID="txtMemo" runat="server" MaxLength="200"></asp:TextBox>
                </td>
            </tr>
        </table>
    </fieldset>
    
    <fieldset class="fsNormal"><legend>所属公司信息</legend>        
        <table class="postform" cellspacing="5">
            <tr>
                <th style="width:140px">公司名称(<span class="needed" >必填</span>)</th>
                <td><asp:TextBox ID="txtCompanyName" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <th style="width:100px;">所属区域(<span class="needed" >必填</span>)</th>
                <td><asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <th style="width:140px">联系电话(<span class="needed" >必填</span>)</th>
                <td><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <th>传真(<span class="unneeded" >可选</span>)</th>
                <td>
                    <asp:TextBox Width="230px" ID="TextBox12" runat="server" MaxLength="200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>地址(<span class="unneeded" >可选</span>)</th>
                <td>
                    <asp:TextBox Width="230px" ID="TextBox10" runat="server" MaxLength="200"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <th>邮编(<span class="unneeded" >可选</span>)</th>
                <td>
                    <asp:TextBox Width="230px" ID="TextBox11" runat="server" MaxLength="200"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <th>公司主页(<span class="unneeded" >可选</span>)</th>
                <td>
                    <asp:TextBox Width="230px" ID="TextBox13" runat="server" MaxLength="200"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <th>组织机构代码(<span class="unneeded" >可选</span>)</th>
                <td>
                    <asp:TextBox Width="230px" ID="TextBox14" runat="server" MaxLength="200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>工商注册登记号(<span class="unneeded" >可选</span>)</th>
                <td>
                    <asp:TextBox Width="230px" ID="TextBox15" runat="server" MaxLength="200"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <th>备注(<span class="unneeded" >可选</span>)</th>
                <td>
                    <asp:TextBox Width="230px" ID="TextBox16" runat="server" MaxLength="200"></asp:TextBox>
                </td>
            </tr>
        </table>
    </fieldset>
</div>
</asp:Content>

