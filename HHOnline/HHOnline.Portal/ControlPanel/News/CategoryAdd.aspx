<%@ Page Title="新增部门" Language="C#" MasterPageFile="~/Masters/DetailMasterPage.master"
	AutoEventWireup="true" CodeFile="CategoryAdd.aspx.cs" Inherits="ControlPanel_News_CategoryAdd" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderDetail" runat="Server">
	<table cellpadding="0" cellspacing="0" class="detail">
		<tr>
			<th>
				父资讯分类
			</th>
			<td>
				<asp:Literal ID="ltParentCategory" runat="server"></asp:Literal>
			</td>
		</tr>
		<tr>
			<th>
				父资讯分类描述
			</th>
			<td>
				<asp:Literal ID="ltParentCategoryDesc" runat="server"></asp:Literal>
			</td>
		</tr>
		<tr>
			<th>
				资讯分类名称
			</th>
			<td>
				<asp:TextBox ID="txtCategoryName" runat="server" MaxLength="50"></asp:TextBox>
				<asp:RequiredFieldValidator ID="rfv1" Display="Dynamic" runat="server" ControlToValidate="txtCategoryName"
					ErrorMessage="必须填写！"></asp:RequiredFieldValidator>
			</td>
		</tr>
		<tr>
			<th>
				资讯分类描述
			</th>
			<td>
				<asp:TextBox ID="txtCategoryDesc" runat="server" Width="300px" MaxLength="200"></asp:TextBox>
				<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server"
					ControlToValidate="txtCategoryDesc" ErrorMessage="必须填写！"></asp:RequiredFieldValidator>
			</td>
		</tr>
		<tr>
			<th>
				显示顺序
			</th>
			<td>
				<asp:TextBox ID="txtDisplayOrder" runat="server" Width="300px" MaxLength="200" Text="0"></asp:TextBox>
				<asp:RegularExpressionValidator ID="rv1" runat="server" ErrorMessage="数字！"  ValidationExpression="(\d*)"
				    ControlToValidate="txtDisplayOrder" Display="Dynamic"></asp:RegularExpressionValidator>
				<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server"
					ControlToValidate="txtDisplayOrder" ErrorMessage="必须填写！"></asp:RequiredFieldValidator>
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
