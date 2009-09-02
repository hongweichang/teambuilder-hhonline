<%@ Page Title="" Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master" AutoEventWireup="true" CodeFile="Corps.aspx.cs" Inherits="ControlPanel_Users_Corps" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphOpts" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" Runat="Server">
<div class="detail-r1c1" style="border: solid 1px #ccc;">
		<table cellpadding="0" cellspacing="0" class="detail">
			<tr>
				<th style="width:150px">
					名称(模糊)
				</th>
				<td style="width:410px">
					<asp:TextBox ID="txtCompanyName" Width="400px" runat="server"></asp:TextBox>	
				</td>
				<td >	    
					<asp:Button ID="btnSearch" runat="server" Width="75px" Text="查找" OnClick="btnSearch_Click" />
				</td>
			</tr>
			<tr>
				<th>
					客户状态
				</th>
				<td colspan="2">
					<div class="productNav">
                        <asp:LinkButton ID="btnAll" runat="server" OnClick="btnQuickSearch_Click">全部</asp:LinkButton>
                        <asp:LinkButton	ID="btnAuth" runat="server" OnClick="btnQuickSearch_Click">已审核</asp:LinkButton>
                        <asp:LinkButton ID="btnAuthPre" runat="server" OnClick="btnQuickSearch_Click">待审核</asp:LinkButton>
                        <asp:LinkButton ID="btnDisAuth" runat="server" OnClick="btnQuickSearch_Click">审核未通过</asp:LinkButton>
                        <asp:LinkButton ID="btnLockon" runat="server" OnClick="btnQuickSearch_Click">公司停用</asp:LinkButton>
					</div>
				</td>
			</tr>
		</table>
	</div>
	<br />
	
 <hc:ExtensionGridView ID="egvCorps" PageSize="10" DataKeyNames="CompanyID" OnRowUpdating="egvCorps_RowUpdating" OnRowDeleting="egvCorps_RowDeleting"
            AutoGenerateColumns="false" runat="server" SkinID="DefaultView" OnPageIndexChanging="egvCorps_PageIndexChanging">
        <Columns>
            <asp:TemplateField>
                <HeaderStyle Width="200" />
                <HeaderTemplate>名称</HeaderTemplate>
                <ItemTemplate><%# Eval("CompanyName")%></ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField>
                <HeaderTemplate>地区</HeaderTemplate>
                <ItemTemplate><%# GetRegion(Eval("CompanyRegion").ToString())%></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>工商注册号</HeaderTemplate>
                <ItemTemplate><%# string.IsNullOrEmpty(Eval("Regcode").ToString())?"--":Eval("Phone")%></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>联系电话</HeaderTemplate>
                <ItemTemplate><%# string.IsNullOrEmpty(Eval("Phone").ToString())?"--":Eval("Phone")%></ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField>
                <HeaderTemplate>类型</HeaderTemplate>
                <ItemTemplate><%# GetCompantType(Eval("CompanyType"))%></ItemTemplate>
            </asp:TemplateField> 
            <asp:TemplateField>
                <HeaderTemplate>状态</HeaderTemplate>
                <ItemTemplate><%# GetStatus(Eval("CompanyStatus"))%></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderStyle Width="200" />
                <HeaderTemplate>操作</HeaderTemplate>
                <ItemTemplate>
                    <asp:LoginView ID="LoginView1" runat="server">
                        <RoleGroups>
                            <asp:RoleGroup Roles="CorpUserModule-Edit"> 
                                <ContentTemplate>    
                                <asp:LinkButton ID="lnkUpdate" runat="server" CommandName="Update" SkinID="lnkedit" PostBackUrl="#"></asp:LinkButton>
                                </ContentTemplate>
                            </asp:RoleGroup>
                        </RoleGroups>
                    </asp:LoginView>
                    <asp:LoginView ID="LoginView2" runat="server">
                        <RoleGroups>
                            <asp:RoleGroup Roles="CorpUserModule-Delete"> 
                                <ContentTemplate>    
                                <asp:LinkButton ID="lnkDelete" runat="server"  CommandName="Delete" SkinID="lnkdelete"  OnClientClick="return confirm('确定要删除此记录吗？')" PostBackUrl="#"></asp:LinkButton>
                                </ContentTemplate>
                            </asp:RoleGroup>
                        </RoleGroups>
                    </asp:LoginView>
                    <asp:LoginView ID="LoginView3" runat="server">
                        <RoleGroups>
                             <asp:RoleGroup Roles="CorpUserModule-View"> 
                                <ContentTemplate>    
                                    <%--<a href="javascript:popWin('<%# "Permission/RoleDetail.aspx?ID="+Eval("RoleID").ToString() %>',800)" title="查看详细" class="opts view">&nbsp;</a>--%>
                                    <a href='javascript:void(<%# Eval("CompanyID") %>)' rel="showdetails" class="opts view" title="查看详细"></a>
                                </ContentTemplate>
                            </asp:RoleGroup>
                        </RoleGroups>
                    </asp:LoginView>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </hc:ExtensionGridView>
</asp:Content>

