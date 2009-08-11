<%@ Page Language="C#" MasterPageFile="~/Masters/DetailMasterPage.master" AutoEventWireup="true"
    CodeFile="CategoryAdd.aspx.cs" Inherits="ControlPanel_product_CategoryAdd" Title="产品管理" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderDetail" runat="Server">
    <table cellpadding="0" cellspacing="0" class="detail">
        <tr runat="server" id="parentName">
            <th>
                上级分类
            </th>
            <td>
                <asp:Literal ID="ltParCategory" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr runat="server" id="parentDesc">
            <th>
                上级分类描述
            </th>
            <td>
                <asp:Literal ID="ltParCategoryDesc" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr runat="server" id="propertyName">
            <th>
                分类依据属性
            </th>
            <td>
                <asp:Literal ID="ltPropertyName" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>
                分类名称
            </th>
            <td>
                <asp:TextBox ID="txtCategoryName" runat="server" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv1" Display="Dynamic" runat="server" ControlToValidate="txtCategoryName"
                    ErrorMessage="必须填写！"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                分类描述
            </th>
            <td>
                <asp:TextBox ID="txtCategoryDesc" runat="server" Width="300px" MaxLength="200"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                排序序号
            </th>
            <td>
                <asp:TextBox ID="txtDisplayOrder" runat="server" MaxLength="8" Text="0"></asp:TextBox>
                <asp:RegularExpressionValidator ID="rev1" runat="server" ControlToValidate="txtDisplayOrder"
                    ValidationExpression="(\d){1,3}" ErrorMessage="必须为0-999的数字"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="rfv3" Display="Dynamic" runat="server" ControlToValidate="txtDisplayOrder"
                    ErrorMessage="必须填写！"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td style="text-align: left; height: 20px;">
                <hc:MsgBox ID="mbMsg" runat="server" SkinID="msgBox"></hc:MsgBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <asp:Button ID="btnPost" runat="server" Text=" 增 加 " OnClick="btnPost_Click" PostBackUrl="#">
                </asp:Button>
                <asp:Button ID="btnClose" runat="server" Text=" 关 闭 " CausesValidation="false" OnClientClick="return cancel();">
                </asp:Button>
            </td>
        </tr>
    </table>
</asp:Content>
