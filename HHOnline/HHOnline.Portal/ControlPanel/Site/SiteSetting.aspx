<%@ Page Title="" Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master"
    AutoEventWireup="true" CodeFile="SiteSetting.aspx.cs" Inherits="ControlPanel_Site_SiteSetting" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="ContentOpts" ContentPlaceHolderID="cphOpts" runat="Server">
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="cphContent" runat="Server">
    <table class="postform" cellpadding="10" cellspacing="10">
        <tr>
            <th style="width: 150px;">
                站点名称<br />
                全局title信息
            </th>
            <td>
                <asp:TextBox ID="txtSiteName" runat="server" Width="300" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="站点名称不能为空。" ControlToValidate="txtSiteName"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                站点描述<br />
                全局description信息
            </th>
            <td>
                <asp:TextBox ID="txtSiteDesc" runat="server" Width="500" MaxLength="200" TextMode="MultiLine"
                    Height="45" Wrap="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                站点关键字<br />
                全局keywords信息
            </th>
            <td>
                <asp:TextBox ID="txtSearchMetaKeywords" runat="server" Width="500" MaxLength="200"
                    TextMode="MultiLine" Height="45" Wrap="true"></asp:TextBox>
            </td>
        </tr>
        <tr runat="server" visible="false">
            <th>
                页面头信息描述
            </th>
            <td>
                <asp:TextBox ID="txtSearchMetaDescription" runat="server" Width="500" MaxLength="200"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                客服电话
            </th>
            <td>
                <asp:TextBox ID="txtServiceTel" runat="server" Width="500" MaxLength="200"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                公司理念
            </th>
            <td>
                <hc:Editor ID="txtIdea" runat="server" Width="500"></hc:Editor>
            </td>
        </tr>
        <tr>
            <th>
                特色服务
            </th>
            <td>
                <hc:Editor ID="txtService" runat="server" Width="500"></hc:Editor>
            </td>
        </tr>
        <tr runat="server" visible="false">
            <th>
                展示图像
            </th>
            <td>
                <asp:Image runat="server" ID="imgShow" Width="80" Height="80" />
            </td>
        </tr>
        <tr runat="server" visible="false">
            <th>
                &nbsp;
            </th>
            <td>
                <asp:FileUpload ID="fuShow" runat="server" Width="200" />
            </td>
        </tr>
        <tr>
            <th>
                版权声明
            </th>
            <td>
                <asp:TextBox ID="txtCopyright" runat="server" Width="500" MaxLength="200"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                备案
            </th>
            <td>
                <asp:TextBox ID="txtICP" runat="server" Width="500" MaxLength="200"></asp:TextBox>
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
                <asp:Button ID="btnPost" runat="server" Text="更 新" PostBackUrl="#" Visible="true"
                    OnClick="btnPost_Click" />&nbsp;&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
