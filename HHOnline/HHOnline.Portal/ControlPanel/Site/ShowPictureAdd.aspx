<%@ Page Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master"
    AutoEventWireup="true" CodeFile="ShowPictureAdd.aspx.cs" Inherits="ControlPanel_Site_ShowPictureAdd"
    Title="无标题页" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="ContentOpts" ContentPlaceHolderID="cphOpts" runat="Server">
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="cphContent" runat="Server">
    <table class="postform" cellpadding="10" cellspacing="10">
        <tr>
            <th>
                图片标题
            </th>
            <td>
                <asp:TextBox ID="txtTitle" runat="server" Width="500" MaxLength="100" />
            </td>
        </tr>
        <tr>
            <th>
                图片说明
            </th>
            <td>
                <hc:Editor ID="txtDescription" runat="server" />
            </td>
        </tr>
        <tr>
            <th>
                图片链接
            </th>
            <td>
                <asp:TextBox ID="txtLink" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                展示图片
            </th>
            <td>
                <asp:Image runat="server" ID="imgLogo" Width="80" Height="80" />
            </td>
        </tr>
        <tr>
            <th>
                &nbsp;
            </th>
            <td>
                <asp:FileUpload ID="fuLogo" runat="server" Width="200" />
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
            <th>
                &nbsp;
            </th>
            <td>
                <hc:MsgBox ID="mbMessage" runat="server" SkinID="msgBox"></hc:MsgBox>
            </td>
        </tr>
        <tr>
            <th>
                &nbsp;
            </th>
            <td>
                <asp:Button ID="btnPost" runat="server" Text="提 交" PostBackUrl="#" Visible="false"
                    OnClick="btnPost_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnPostBack" runat="server" Text="返 回" Visible="false" CausesValidation="false" />
            </td>
        </tr>
    </table>
</asp:Content>
