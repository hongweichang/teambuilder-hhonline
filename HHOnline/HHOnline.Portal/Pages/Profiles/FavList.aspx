<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/AdminMasterPage.master" AutoEventWireup="true" CodeFile="FavList.aspx.cs" Inherits="Pages_Profiles_FavList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphOpts" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">
<div class="detail-r1c1" style="border: solid 1px #ccc;">
	<table cellpadding="0" cellspacing="0" class="detail">
		<tr>
			<th>类型</th>
			<td>
			    <asp:DropDownList ID="ddlType" Width="150px" runat="server">
			        <asp:ListItem Text="=全部=" Value="0" Selected="True"></asp:ListItem>
			        <asp:ListItem Text="产品" Value="1"></asp:ListItem>
			        <asp:ListItem Text="资讯" Value="2"></asp:ListItem>
			    </asp:DropDownList>
			</td>
			<th style="width:150px">
				标题
			</th>
			<td style="width:410px">
				<asp:TextBox ID="txtName" Width="300px" runat="server"></asp:TextBox>	
			</td>
			<td >	    
				<asp:Button ID="btnSearch" runat="server" Width="75px" Text="查找" OnClick="btnSearch_Click" />
			</td>
		</tr>
	</table>
</div>
<br />
<hc:ExtensionGridView ID="egvFavs" PageSize="15" DataKeyNames="FavoriteID" OnRowDeleting="egvFavs_RowDeleting"
    AutoGenerateColumns="false" runat="server" SkinID="DefaultView" OnPageIndexChanging="egvFavs_PageIndexChanging">
    <Columns>
       <asp:BoundField HeaderText="标题" DataFormatString="{0:20S}" DataField="FavoriteTitle" />
       <asp:TemplateField>
        <HeaderTemplate>类型</HeaderTemplate>
        <ItemTemplate>
            <%# GetFavoriteType(Eval("FavoriteType")) %>
        </ItemTemplate>
       </asp:TemplateField>
       <asp:TemplateField>
        <HeaderTemplate>级别</HeaderTemplate>
        <ItemTemplate>
            <%# Eval("favoriteLevel") %>星
        </ItemTemplate>
       </asp:TemplateField>
       <asp:BoundField HeaderText="相关描述" DataFormatString="{0:40S}" DataField="FavoriteMemo" />
       <asp:TemplateField>
        <HeaderTemplate>操作</HeaderTemplate>
        <ItemTemplate>
            <a href='<%# GetView(Eval("RelatedID"),Eval("FavoriteType")) %>' target="_blank" class="opts view" title="跳转到此链接"></a>
            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" SkinID="lnkdelete" OnClientClick="return confirm('此收藏对象将被永久删除，确定要继续吗？')"></asp:LinkButton>
        </ItemTemplate>
       </asp:TemplateField>
    </Columns>
</hc:ExtensionGridView>
</asp:Content>

