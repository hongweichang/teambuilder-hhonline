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
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPassword"
                            ErrorMessage="必须填写！" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" Display="Dynamic"
                        ValidationExpression="\S{8,25}" ControlToValidate="txtPassword" ErrorMessage="长度为8~25位！"></asp:RegularExpressionValidator>
                </td>
                <td><span class="tip">请尽量设置较复杂密码！</span></td>
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
                        ValidationExpression="\S{25,250}" ControlToValidate="txtQuestion" ErrorMessage="长度为25~250个字符！"></asp:RegularExpressionValidator>                    
                </td>
                <td><span class="tip">密码提示问题作为找回用户密码的关键凭证！</span></td>
            </tr>
            <tr>
                <th>密码提示答案(<span class="needed" >必填</span>)</th>
                <td>
                    <asp:TextBox Width="230px" ID="txtAnswer" MaxLength="250" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtAnswer"
                            ErrorMessage="必须填写！" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" Display="Dynamic"
                        ValidationExpression="\S{25,250}" ControlToValidate="txtAnswer" ErrorMessage="长度为25~250个字符！"></asp:RegularExpressionValidator>                    
                </td>
                <td><span class="tip">找回密码时需此答案与问题匹配才能取回密码！</span></td>
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
                <th >所属部门(<span class="unneeded" >已选</span>)</th>
                <td> <asp:TextBox Width="230px" ID="txtDepartment" runat="server" MaxLength="50"></asp:TextBox></td>
            </tr>   
            <tr>
                <th>职务(<span class="unneeded" >可选</span>)</th>
                <td>
                    <asp:TextBox Width="230px" ID="txtTitle" runat="server" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>备注(<span class="unneeded" >可选</span>)</th>
                <td colspan="2">
                    <asp:TextBox Width="630px" ID="txtMemo" runat="server" MaxLength="200"></asp:TextBox>
                </td>
            </tr>
        </table>
    </fieldset>
    
    <fieldset class="fsNormal"><legend>所属公司信息</legend>        
        <table class="postform" cellspacing="5">
            <tr>
                <th style="width:140px">公司名称(<span class="needed" >必填</span>)</th>
                <td>
                    <asp:TextBox Width="230px" ID="txtCompanyName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCompanyName"
                            ErrorMessage="必须填写！" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
                <td><span class="tip">公司全称！</span></td>
            </tr>
            <tr>
                <th style="width:100px;">所属区域(<span class="needed" >必填</span>)</th>
                <td><asp:TextBox Width="230px" ID="txtRegion" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtRegion"
                            ErrorMessage="必须填写！" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
                <td><span class="tip">区域可为分区、省、市！</span></td>
            </tr>
            <tr>
                <th style="width:140px">联系电话(<span class="needed" >必填</span>)</th>
                <td><asp:TextBox Width="230px" ID="txtCompanyPhone" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtCompanyPhone"
                            ErrorMessage="必须填写！" Display="Dynamic"></asp:RequiredFieldValidator>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" 
                        ValidationExpression="(\(\d{3,4}\)|\d{3,4}-|\s)?\d{7,8}"  Display="Dynamic"
                        ControlToValidate="txtCompanyPhone" ErrorMessage="格式不正确！"></asp:RegularExpressionValidator>
                </td>
                 <td><span class="tip">格式形如"区号-号码"或"(区号)号码"！</span></td>
            </tr>
            <tr>
                <th>传真(<span class="unneeded" >可选</span>)</th>
                <td>
                    <asp:TextBox Width="230px" ID="txtCompanyFax" runat="server" MaxLength="200"></asp:TextBox>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" 
                        ValidationExpression="(\(\d{3,4}\)|\d{3,4}-|\s)?\d{7,8}"  Display="Dynamic"
                        ControlToValidate="txtCompanyFax" ErrorMessage="格式不正确！"></asp:RegularExpressionValidator>
                </td>
                 <td><span class="tip">格式同上！</span></td>
            </tr>
            <tr>
                <th>地址(<span class="unneeded" >可选</span>)</th>
                <td>
                    <asp:TextBox Width="230px" ID="txtCompanyAddress" runat="server" MaxLength="500"></asp:TextBox>
                </td>
                <td><span class="tip">公司地址，便于联系或发送商品！</span></td>
            </tr>
             <tr>
                <th>邮编(<span class="unneeded" >可选</span>)</th>
                <td>
                    <asp:TextBox Width="230px" ID="txtZipCode" runat="server" MaxLength="6"></asp:TextBox>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" 
                        ValidationExpression="\d{6}"  Display="Dynamic"
                        ControlToValidate="txtZipCode" ErrorMessage="请输入6位数字的邮政编码！"></asp:RegularExpressionValidator>
                </td>
            </tr>
             <tr>
                <th>公司主页(<span class="unneeded" >可选</span>)</th>
                <td>
                    <asp:TextBox Width="230px" ID="txtCompanyWebsite" runat="server" MaxLength="300"></asp:TextBox>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" 
                        ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"  Display="Dynamic"
                        ControlToValidate="txtCompanyWebsite" ErrorMessage="请输入正确的网址！"></asp:RegularExpressionValidator>
                </td>
                <td>&nbsp;</td>
            </tr>
             <tr>
                <th>组织机构代码(<span class="unneeded" >可选</span>)</th>
                <td>
                    <asp:TextBox Width="230px" ID="txtOrgCode" runat="server" MaxLength="200"></asp:TextBox>
                </td>
                <td><span class="tip">公司注册的组织机构代码！</span></td>
            </tr>
            <tr>
                <th>工商注册登记号(<span class="unneeded" >可选</span>)</th>
                <td>
                    <asp:TextBox Width="230px" ID="txtIcpCode" runat="server" MaxLength="200"></asp:TextBox>
                </td>
                <td><span class="tip">公司注册的工商注册登记号！</span></td>
            </tr>
             <tr>
                <th>备注(<span class="unneeded" >可选</span>)</th>
                <td>
                    <asp:TextBox Width="230px" ID="txtCompanyMemo" runat="server" MaxLength="500" TextMode="MultiLine" Height="150px"></asp:TextBox>
                </td>
                <td><span class="tip">简要描述公司特色！</span></td>                
            </tr>
        </table>
    </fieldset>
</div>
</asp:Content>

