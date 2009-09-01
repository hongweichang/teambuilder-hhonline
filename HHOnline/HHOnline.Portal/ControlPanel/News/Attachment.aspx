<%@ Page Title="" Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master"
    AutoEventWireup="true" CodeFile="Attachment.aspx.cs" Inherits="ControlPanel_News_Attachment" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="ContentOpts" ContentPlaceHolderID="cphOpts" runat="Server">
    <asp:LinkButton ID="btnNewAttachment" runat="server" SkinID="lnkopts">
        <span>新 增</span>
    </asp:LinkButton>
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="cphContent" runat="Server">
	<div class="detail-r1c1" style="border: solid 1px #ccc;">
		<table cellpadding="0" cellspacing="0" class="detail">
			<tr>
				<th>
					名称
				</th>
				<td>
					<asp:TextBox ID="txtAttachmentName" runat="server"></asp:TextBox>
				</td>
				<th>
					开始时间
				</th>
				<td>
					<asp:TextBox ID="txtCreateStartTime" rel="DatePickerStart" runat="server" Width="150px"
						></asp:TextBox>
				</td>
				<th>
					结束时间
				</th>
				<td>
					<asp:TextBox ID="txtCreateEndTime" rel="DatePickerEnd" runat="server" Width="150px"
						></asp:TextBox>
				</td>
			</tr>
			<tr>
				<th>
					快速过滤
				</th>
				<td colspan="6">
					<div class="productNav">
						<%--                        <asp:LinkButton ID="lnkAll" runat="server" OnClick="lnk_Click">所有产品</asp:LinkButton>
                        <asp:LinkButton ID="lnkPublished" runat="server" OnClick="lnk_Click">已发布</asp:LinkButton>
                        <asp:LinkButton ID="lnkUnPublishied" runat="server" OnClick="lnk_Click">未发布</asp:LinkButton>
                        <asp:LinkButton ID="lnkPriced" runat="server" OnClick="lnk_Click">已报价</asp:LinkButton>
                        <asp:LinkButton ID="lnkNoPriced" runat="server" OnClick="lnk_Click">未报价</asp:LinkButton>
                        <asp:LinkButton ID="lnkPicture" runat="server" OnClick="lnk_Click">有图商品</asp:LinkButton>
                        <asp:LinkButton ID="lnkNoPicture" runat="server" OnClick="lnk_Click">无图商品</asp:LinkButton>
                        --%>
                        <asp:LinkButton ID="btnAll" runat="server" OnClick="btnQuickSearch_Click">全部</asp:LinkButton>
                        <asp:LinkButton	ID="btnSmallSizeFile" runat="server" OnClick="btnQuickSearch_Click">小文件（&lt;=1M）</asp:LinkButton>
                        <asp:LinkButton ID="btnMediumSizeFile" runat="server" OnClick="btnQuickSearch_Click">中等文件（&gt;1M）</asp:LinkButton>
                        <asp:LinkButton ID="btnLargeSizeFile" runat="server" OnClick="btnQuickSearch_Click">大文件（&gt;10M）</asp:LinkButton>
					</div>
				</td>
				<td rowspan="2">
					<asp:Button ID="btnSearch" runat="server" Text="查找产品" OnClick="btnSearch_Click" />
				</td>
			</tr>
			<tr>
				<th>
					显示
				</th>
				<td colspan="6">
					<asp:Label ID="lblTip" runat="server" Text="Label"></asp:Label>的附件。
				</td>
			</tr>
		</table>
	</div>
	<br />
	<hc:ExtensionGridView runat="server" ID="egvAttachments" OnRowDataBound="egvAttachments_RowDataBound"
		OnRowDeleting="egvAttachments_RowDeleting" OnRowUpdating="egvAttachments_RowUpdating"
		PageSize="5" SkinID="DefaultView" AutoGenerateColumns="False" DataKeyNames="ID">
		<Columns>
			<asp:TemplateField HeaderText="展示图片">
				<HeaderStyle Width="100" />
				<ItemTemplate>
					<asp:Image ID="imgPicture" Style="border: double 3px #7d9edb;" Width="40" Height="40"
						runat="server" />
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="名称">
				<ItemTemplate>
					<asp:HyperLink ID="hlName" runat="server">[hlName]</asp:HyperLink>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:BoundField HeaderText="类型" DataField="ContentType" />
			<asp:BoundField HeaderText="大小" DataField="ContentSize" />
			<asp:BoundField HeaderText="创建时间" DataField="CreateTime" />
			<asp:TemplateField>
				<HeaderStyle Width="200" />
				<HeaderTemplate>
					操作</HeaderTemplate>
				<ItemTemplate>
					<asp:LoginView ID="LoginView1" runat="server">
						<RoleGroups>
							<asp:RoleGroup Roles="ProductModule-Edit">
								<ContentTemplate>
									<asp:LinkButton ID="lnkUpdate" runat="server" CommandName="Update" SkinID="lnkedit"
										PostBackUrl="#"></asp:LinkButton>
								</ContentTemplate>
							</asp:RoleGroup>
						</RoleGroups>
					</asp:LoginView>
					<asp:LoginView ID="LoginView2" runat="server">
						<RoleGroups>
							<asp:RoleGroup Roles="ProductModule-Delete">
								<ContentTemplate>
									<asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" SkinID="lnkdelete"
										OnClientClick="return confirm('确定要删除此记录吗？')" PostBackUrl="#"></asp:LinkButton>
								</ContentTemplate>
							</asp:RoleGroup>
						</RoleGroups>
					</asp:LoginView>
				</ItemTemplate>
			</asp:TemplateField>
		</Columns>
	</hc:ExtensionGridView>
</asp:Content>
