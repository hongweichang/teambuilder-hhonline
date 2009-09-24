<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/AdminMasterPage.master" AutoEventWireup="true" CodeFile="CompanyProfile.aspx.cs" Inherits="Pages_Profiles_CompanyProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphOpts" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">
<hc:MsgBox ID="mbNC" runat="server" SkinID="msgBox"></hc:MsgBox>
<asp:Panel ID="pnlManager" runat="server">
    <table class="postform" cellpadding="10" cellspacing="10">
        <tr>
            <th>名称</th>
            <td><asp:Literal ID="ltCompanyName" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <th style="width:140px;">所属区域(<span class="needed" >必填</span>)</th>
            <td><asp:TextBox Width="230px" ID="txtRegion" runat="server" ReadOnly="true"></asp:TextBox>
                    <img id="showArea" src="../images/default/choosearea.gif" alt="选择区域" title="选择区域" style="cursor:pointer" />
                    <asp:HiddenField ID="hfRegionCode" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtRegion"
                        ErrorMessage="必须填写！" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th style="width:140px">联系电话(<span class="needed" >必填</span>)</th>
            <td><asp:TextBox Width="230px" ID="txtCompanyPhone" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtCompanyPhone"
                        ErrorMessage="必须填写！" Display="Dynamic"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" 
                    ValidationExpression="((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)"  Display="Dynamic"
                    ControlToValidate="txtCompanyPhone" ErrorMessage="为固话或者手机号码！"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <th>传真(<span class="unneeded" >可选</span>)</th>
            <td>
                <asp:TextBox Width="230px" ID="txtCompanyFax" runat="server" MaxLength="200"></asp:TextBox>
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" 
                    ValidationExpression="(\(\d{3,4}\)|\d{3,4}-|\s)?\d{7,8}"  Display="Dynamic"
                    ControlToValidate="txtCompanyFax" ErrorMessage="格式不正确！"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <th>地址(<span class="unneeded" >可选</span>)</th>
            <td>
                <asp:TextBox Width="230px" ID="txtCompanyAddress" runat="server" MaxLength="500"></asp:TextBox>
            </td>
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
        </tr>
         <tr>
            <th>组织机构代码(<span class="unneeded" >可选</span>)</th>
            <td>
                <asp:TextBox Width="230px" ID="txtOrgCode" runat="server" MaxLength="200"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>工商注册登记号(<span class="unneeded" >可选</span>)</th>
            <td>
                <asp:TextBox Width="230px" ID="txtIcpCode" runat="server" MaxLength="200"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <th>备注(<span class="unneeded" >可选</span>)</th>
            <td>
                <asp:TextBox Width="230px" ID="txtCompanyMemo" runat="server" MaxLength="500" TextMode="MultiLine" Height="100px"></asp:TextBox>
            </td>             
        </tr>
        <tr>
            <th>&nbsp;</th>
            <td><asp:Button ID="btnSave" runat="server" Text=" 保存 " OnClick="btnSave_Click" /></td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlNormal" runat="server">
    <table class="postform" cellpadding="10" cellspacing="10">
        <tr>
            <th>名称</th>
            <td><asp:Literal ID="ltName" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <th style="width:140px;">所属区域</th>
            <td><asp:Literal ID="ltArea" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th style="width:140px">联系电话</th>
            <td>
                <asp:Literal ID="ltPhone" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>传真</th>
            <td>
               <asp:Literal ID="ltFax" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>地址</th>
            <td>
                <asp:Literal ID="ltAddress" runat="server"></asp:Literal>
            </td>
        </tr>
         <tr>
            <th>邮编</th>
            <td>
               <asp:Literal ID="ltZipcode" runat="server"></asp:Literal>
            </td>
        </tr>
         <tr>
            <th>公司主页</th>
            <td>
               <asp:Literal ID="ltWebsite" runat="server"></asp:Literal>
            </td>
        </tr>
         <tr>
            <th>组织机构代码</th>
            <td>
                <asp:Literal ID="ltOrgCode" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>工商注册登记号</th>
            <td>
                <asp:Literal ID="ltRegCode" runat="server"></asp:Literal>
            </td>
        </tr>
         <tr>
            <th>备注</th>
            <td>
                <asp:Literal ID="ltDescription" runat="server"></asp:Literal>
            </td>             
        </tr>
    </table>
</asp:Panel>
<div id="regionViewer"></div>
</asp:Content>

