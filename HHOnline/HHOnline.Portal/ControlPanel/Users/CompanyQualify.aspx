<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DetailMasterPage.master" AutoEventWireup="true" CodeFile="CompanyQualify.aspx.cs" Inherits="ControlPanel_Users_CompanyQualify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderDetail" Runat="Server">
<table class="detail" cellspacing="0" cellpadding="0">
    <tr>
        <th colspan="2" style="text-align:center;">
            资质文件管理
        </th>
    </tr>
    <tr>
        <th style="width:140px;">已录入</th>
        <td>
        <hc:ExtensionGridView ID="egvQualify" DataKeyNames="QualificationID" AllowPaging="false" ShowHeader="false" OnPageIndexChanging="egvQualify_PageIndexChanging"
            AutoGenerateColumns="false" runat="server" SkinID="DefaultView" OnRowEditing="egvQualify_RowEditing" OnRowDeleting="egvQualify_RowDeleting" >
        <Columns>
            <asp:TemplateField>
                <HeaderStyle Width="200" />
                <HeaderTemplate>名称</HeaderTemplate>
                <ItemTemplate><%# Eval("QualificationName")%></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>操作</HeaderTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" Text=" " SkinID="lnkedit" CausesValidation="false"></asp:LinkButton> 
                    <asp:LinkButton ID="lnkDelete" OnClientClick="return confirm('删除后将不可被恢复，确定继续？');" runat="server" CommandName="Delete" Text=" " SkinID="lnkdelete" CausesValidation="false"></asp:LinkButton> 
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </hc:ExtensionGridView>
        </td>
    </tr>
    <tr>
        <th>名称(<span class="needed">唯一性</span>)</th>
        <td><asp:TextBox ID="txtName" runat="server" MaxLength="25" Width="200px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rvf1" runat="server" ControlToValidate="txtName" ErrorMessage="必须填写！"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th>描述</th>
        <td><asp:TextBox ID="txtDesc" runat="server" MaxLength="100" Width="400px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDesc" ErrorMessage="必须填写！"></asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <th>附件</th>
        <td>
        <asp:Literal ID="ltUpload" runat="server"></asp:Literal>
        <asp:FileUpload ID="fuQualify" runat="server" /></td>
    </tr>
    <tr>
        <th>&nbsp;</th>
        <td>
            <hc:MsgBox ID="mbMsg" runat="server" SkinID="msgBox"></hc:MsgBox>
        </td>
    </tr>
    <tr>
        <th>&nbsp;</th>
        <td>
            <asp:HiddenField ID="hfCurID" runat="server" />
            <asp:Button ID="btnAdd" runat="server" Text=" 新增 " OnClick="btnAdd_Click" />
            <asp:Button ID="btnEdit" runat="server" Text=" 保存修改 " ForeColor="Red" OnClick="btnEdit_Click"  />
            <asp:Button ID="btnBack" runat="server" Text=" 返回 " CausesValidation="false" />
        </td>
    </tr>
</table>
</asp:Content>

