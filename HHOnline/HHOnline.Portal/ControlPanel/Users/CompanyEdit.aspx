<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DetailMasterPage.master"
    AutoEventWireup="true" CodeFile="CompanyEdit.aspx.cs" Inherits="ControlPanel_Users_CompanyEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderDetail" runat="Server">
    <table class="detail" cellspacing="0" cellpadding="0">
        <tr>
            <th style="width: 140px">
                公司名称(<span class="needed">必填</span>)
            </th>
            <td>
                <asp:TextBox Width="230px" ID="txtCompanyName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCompanyName"
                    ErrorMessage="必须填写！" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
            <td>
                <span class="tip">公司全称！</span>
            </td>
        </tr>
        <tr>
            <th style="width: 100px;">
                所属区域(<span class="needed">必填</span>)
            </th>
            <td>
                <asp:TextBox Width="230px" ID="txtRegion" runat="server" ReadOnly="true"></asp:TextBox>
                <img id="showArea" src="../../images/default/choosearea.gif" alt="选择区域" title="选择区域"
                    style="cursor: pointer" />
                <asp:HiddenField ID="hfRegionCode" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtRegion"
                    ErrorMessage="必须填写！" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
            <td>
                <span class="tip">区域可为分区、省、市！</span>
            </td>
        </tr>
        <tr>
            <th style="width: 140px">
                联系电话(<span class="needed">必填</span>)
            </th>
            <td>
                <asp:TextBox Width="230px" ID="txtCompanyPhone" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtCompanyPhone"
                    ErrorMessage="必须填写！" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ValidationExpression="((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)"
                    Display="Dynamic" ControlToValidate="txtCompanyPhone" ErrorMessage="为固话或者手机号码！"></asp:RegularExpressionValidator>
            </td>
            <td>
                <span class="tip">格式形如"区号-号码"或"(区号)号码"或11位手机号码！</span>
            </td>
        </tr>
        <tr>
            <th>
                传真(<span class="unneeded">可选</span>)
            </th>
            <td>
                <asp:TextBox Width="230px" ID="txtCompanyFax" runat="server" MaxLength="200"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ValidationExpression="(\(\d{3,4}\)|\d{3,4}-|\s)?\d{7,8}"
                    Display="Dynamic" ControlToValidate="txtCompanyFax" ErrorMessage="格式不正确！"></asp:RegularExpressionValidator>
            </td>
            <td>
                <span class="tip">格式同上！</span>
            </td>
        </tr>
        <tr>
            <th>
                地址(<span class="unneeded">可选</span>)
            </th>
            <td>
                <asp:TextBox Width="230px" ID="txtCompanyAddress" runat="server" MaxLength="500"></asp:TextBox>
            </td>
            <td>
                <span class="tip">公司地址，便于联系或发送商品！</span>
            </td>
        </tr>
        <tr>
            <th>
                邮编(<span class="unneeded">可选</span>)
            </th>
            <td>
                <asp:TextBox Width="230px" ID="txtZipCode" runat="server" MaxLength="6"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ValidationExpression="\d{6}"
                    Display="Dynamic" ControlToValidate="txtZipCode" ErrorMessage="请输入6位数字的邮政编码！"></asp:RegularExpressionValidator>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <th>
                公司主页(<span class="unneeded">可选</span>)
            </th>
            <td>
                <asp:TextBox Width="230px" ID="txtCompanyWebsite" runat="server" MaxLength="300"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                    ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?" Display="Dynamic"
                    ControlToValidate="txtCompanyWebsite" ErrorMessage="请输入正确的网址！"></asp:RegularExpressionValidator>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <th>
                组织机构代码(<span class="unneeded">可选</span>)
            </th>
            <td>
                <asp:TextBox Width="230px" ID="txtOrgCode" runat="server" MaxLength="200"></asp:TextBox>
            </td>
            <td>
                <span class="tip">公司注册的组织机构代码！</span>
            </td>
        </tr>
        <tr>
            <th>
                工商注册登记号(<span class="unneeded">可选</span>)
            </th>
            <td>
                <asp:TextBox Width="230px" ID="txtIcpCode" runat="server" MaxLength="200"></asp:TextBox>
            </td>
            <td>
                <span class="tip">公司注册的工商注册登记号！</span>
            </td>
        </tr>
        <tr>
            <th>
                备注(<span class="unneeded">可选</span>)
            </th>
            <td>
                <asp:TextBox Width="230px" ID="txtCompanyMemo" runat="server" MaxLength="500" TextMode="MultiLine"
                    Height="100px"></asp:TextBox>
            </td>
            <td>
                <span class="tip">简要描述公司特色！</span>
            </td>
        </tr>
        <tr>
            <th>
                关联账户
            </th>
            <td class="link">
                <asp:Literal ID="ltUsers" runat="server"></asp:Literal>
            </td>
            <td>
                <span class="tip">该公司所关联的平台账户！</span>
            </td>
        </tr>
        <tr>
            <th>
                &nbsp;
            </th>
            <td colspan="2">
               <asp:Button ID="btnEdit" runat="server" Text="保存" CausesValidation="true" OnClick="btnEdit_Click" />
               <asp:Button ID="btnCancel" runat="server" Text="关闭" OnClientClick="return cancel();" CausesValidation="false" />
            </td>
        </tr>
    </table>
    <div id="regionViewer">
    </div>
</asp:Content>
