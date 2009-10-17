<%@ Page Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master"
    AutoEventWireup="true" CodeFile="AttachmentAddEdit.aspx.cs" Inherits="ControlPanel_News_AttachmentAddEdit"
    Title="新增附件" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
    <style type="text/css">
        #Select1
        {
            width: 141px;
        }
        #cboAttachmentType
        {
            width: 123px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphOpts" runat="Server">
    <asp:Label ID="lblTitle" runat="Server"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <table cellpadding="10" cellspacing="10" class="postform">
        <tr>
            <th>&nbsp;</th>
            <td>
                目前【附件管理】仅为资讯提供标题图片服务。
            </td>
        </tr>
        <tr>
            <th>
                附件名称(<span style="color: #ff0000">必填</span>)
            </th>
            <td>
                <asp:TextBox Width="400px" ID="txtTitle" runat="server" MaxLength="20"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv1" runat="server" Display="Dynamic" ControlToValidate="txtTitle"
                    ErrorMessage="必须填写！"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                附件类型
            </th>
            <td id="tdTypeSelector">
                <asp:DropDownList ID="cboAttachmentType" runat="server" onchange="CheckAttachmentType();">
                    <asp:ListItem Selected="True">本地上传</asp:ListItem>
                    <asp:ListItem>远程地址</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th>
                附件/地址
            </th>
            <td>
                <div id="divLocalAttachment">
                    <asp:FileUpload ID="fuLocal" Width="400px" runat="server" />
                </div>
                <div id="divServerAttachment" style="display:none">
                    <asp:TextBox ID="txtUrl" Width="400px" runat="server"></asp:TextBox>
                 <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                    ControlToValidate="txtUrl" ErrorMessage="必须填写！"></asp:RequiredFieldValidator>--%>
               </div>
            </td>
        </tr>
        <tr>
            <th>
                是否启用
            </th>
            <td>
                <hc:ComponentStatusList ID="csAttachment" runat="server" />
            </td>
        </tr>
        <tr>
            <th>
                备注(<span style="color: #4682B4">可选</span>)
            </th>
            <td>
                <asp:TextBox Width="400" ID="txtMemo" runat="server" MaxLength="50"></asp:TextBox>
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
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Button ID="btnPost" runat="server" Text=" 保 存 " OnClick="btnPost_Click" PostBackUrl="#">
                </asp:Button>
                <asp:Button ID="btnBack" runat="server" Text=" 返 回 " CausesValidation="false"  OnClick="btnBack_Click">
                </asp:Button>
            </td>
        </tr>
    </table>
</asp:Content>
