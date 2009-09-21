<%@ Page Title="" Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master"
    AutoEventWireup="true" CodeFile="Article.aspx.cs" Inherits="ControlPanel_News_Article" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="ContentOpts" ContentPlaceHolderID="cphOpts" runat="Server">
    <asp:LinkButton ID="btnNewArticle" runat="server" SkinID="lnkopts">
        <span>新 增</span>
    </asp:LinkButton>
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="cphContent" runat="Server">
    <div class="detail-r1c1" style="border: solid 1px #ccc;">
		<table cellpadding="0" cellspacing="0" class="detail">
			<tr>
				<th>
					分类
				</th>
				<td>
					<huc:ArticleCategoryCombo ID="ascCategory" runat="server" />
				</td>
				<th>
					标题
				</th>
				<td>
					<asp:TextBox ID="txtArticleTitle" runat="server" Width="200px"></asp:TextBox>
				</td>
				<td rowspan="3">
					<asp:Button ID="btnSearch" runat="server" Height="50px" Text="查找资讯" OnClick="btnSearch_Click" />
				</td>
			</tr>
			<tr>
				<th>
					开始时间
				</th>
				<td>
					<asp:TextBox ID="txtCreateStartTime" rel="DatePickerStart" runat="server" Width="200px"
						></asp:TextBox>
				</td>
				<th>
					结束时间
				</th>
				<td>
					<asp:TextBox ID="txtCreateEndTime" rel="DatePickerEnd" runat="server" Width="200px"
						></asp:TextBox>
				</td>
			</tr>
			<tr>
				<th>
					快速过滤
				</th>
				<td colspan="3">
					<div class="productNav">
                        <asp:LinkButton ID="btnAll" runat="server" OnClick="btnQuickSearch_Click">全部</asp:LinkButton>
                        <asp:LinkButton	ID="btnSmallHitTimes" runat="server" OnClick="btnQuickSearch_Click">小访问量（&lt;=100）</asp:LinkButton>
                        <asp:LinkButton ID="btnMediumHitTimes" runat="server" OnClick="btnQuickSearch_Click">中等访问量（100~1000）</asp:LinkButton>
                        <asp:LinkButton ID="btnLargeHitTimes" runat="server" OnClick="btnQuickSearch_Click">大访问量（&gt;1000）</asp:LinkButton>
					</div>
				</td>
			</tr>
			<tr>
				<th>
					显示
				</th>
				<td colspan="5">
					<asp:Label ID="lblTip" runat="server" Text="Label"></asp:Label>的资讯。
				</td>
			</tr>
		</table>
	</div>
	<br />
	<hc:ExtensionGridView runat="server" ID="egvArticles" OnRowDataBound="egvArticles_RowDataBound"
		OnRowDeleting="egvArticles_RowDeleting" OnRowUpdating="egvArticles_RowUpdating"
		PageSize="10" SkinID="DefaultView" AutoGenerateColumns="false" DataKeyNames="ID" 
        onpageindexchanging="egvArticles_PageIndexChanging">
		<Columns>
			<asp:TemplateField HeaderText="资讯图片">
				<HeaderStyle Width="100" />
				<ItemTemplate>
					<asp:Image ID="imgPicture" Style="border: double 3px #7d9edb;" Width="40" Height="40"
						runat="server" />
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="标题">
				<ItemTemplate>
					<asp:HyperLink ID="hlName" runat="server">[hlName]</asp:HyperLink>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:BoundField HeaderText="所属分类" DataField="CategoryObject" />
			<asp:BoundField HeaderText="时间" DataField="CreateTime" />
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
