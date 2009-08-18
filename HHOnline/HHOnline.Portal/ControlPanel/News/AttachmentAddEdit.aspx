<%@ Page Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master"
    AutoEventWireup="true" CodeFile="AttachmentAddEdit.aspx.cs" Inherits="ControlPanel_News_AttachmentAddEdit"
    Title="��������" %>

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
            <th>
                ��������(<span style="color: #ff0000">����</span>)
            </th>
            <td>
                <asp:TextBox Width="230px" ID="txtTitle" runat="server" MaxLength="20"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv1" runat="server" Display="Dynamic" ControlToValidate="txtTitle"
                    ErrorMessage="������д��"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                MIME����(<span style="color: #ff0000">����</span>)
            </th>
            <td>
                <asp:TextBox Width="230px" ID="txtMIMEType" runat="server" Text="" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                    ControlToValidate="txtMIMEType" ErrorMessage="������д��"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                ��ע(<span style="color: #ff0000">����</span>)
            </th>
            <td>
                <asp:TextBox Width="230px" ID="txtDesc" runat="server" Text="" MaxLength="250"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                    ControlToValidate="txtDesc" ErrorMessage="������д��"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                ��������
            </th>
            <td>
                <asp:DropDownList ID="cboAttachmentType" runat="server" onchange="CheckAttachmentType();">
                    <asp:ListItem Selected="True">�����ϴ�</asp:ListItem>
                    <asp:ListItem>Զ�̵�ַ</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th>
                ����
            </th>
            <td>
                <div id="divLocalAttachment">
                    <asp:FileUpload ID="fuLocal" runat="server" Width="316px" />
                </div>
                <div id="divServerAttachment" style="display:none">
                    <asp:TextBox ID="txtUrl" runat="server" Width="237px"></asp:TextBox>
                 <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                    ControlToValidate="txtUrl" ErrorMessage="������д��"></asp:RequiredFieldValidator>--%>
               </div>
            </td>
        </tr>
        <tr>
            <th>
                �Ƿ�����
            </th>
            <td>
                <hc:ComponentStatusList ID="csAttachment" runat="server" />
            </td>
        </tr>
        <tr>
            <th>
                ��ע(<span style="color: #4682B4">��ѡ</span>)
            </th>
            <td>
                <asp:TextBox Width="500" ID="txtMemo" runat="server" MaxLength="50"></asp:TextBox>
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
                <asp:Button ID="btnPost" runat="server" Text=" �� �� " OnClick="btnPost_Click" PostBackUrl="#">
                </asp:Button>
                <asp:Button ID="btnClose" runat="server" Text=" �� �� " CausesValidation="false" OnClientClick="return CloseEdit();">
                </asp:Button>
            </td>
        </tr>
    </table>
</asp:Content>
