<%@ Page Title="" Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master" AutoEventWireup="true" CodeFile="PendingList.aspx.cs" Inherits="ControlPanel_product_PendingList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphOpts" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" Runat="Server">
<hc:ExtensionGridView ID="egvPendings" PageSize="15" DataKeyNames="CompanyID" OnRowDataBound="egvPendings_RowDataBound"
            AutoGenerateColumns="false" runat="server" SkinID="DefaultView" OnPageIndexChanging="egvPendings_PageIndexChanging">
        <Columns>
            <asp:TemplateField>
                <HeaderStyle Width="200" />
                <HeaderTemplate>名称</HeaderTemplate>
                <ItemTemplate><asp:Literal ID="ltCompanyName" runat="server"></asp:Literal></ItemTemplate>
            </asp:TemplateField> 
            <asp:TemplateField>
                <HeaderTemplate>当前类型</HeaderTemplate>
                <ItemTemplate><asp:Literal ID="ltCompanyType" runat="server"></asp:Literal></ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField>
                <HeaderTemplate>申请类型</HeaderTemplate>
                <ItemTemplate><%# GetCompantType(Eval("CompanyType")) %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderStyle Width="200" />
                <HeaderTemplate>操作</HeaderTemplate>
                <ItemTemplate>
                    <input type="hidden" class="first" value='<%# Eval("CompanyID") %>' />
                    <input type="hidden" class="second" value='<%# Eval("ID") %>' />
                    <asp:LoginView ID="LoginView1" runat="server">
                        <RoleGroups>
                            <asp:RoleGroup Roles="CorpUserModule-Edit"> 
                                <ContentTemplate>    
                                <a href='javascript:void(0)' rel="editcompany" class="opts edit" title="编辑信息"></a>
                                </ContentTemplate>
                            </asp:RoleGroup>
                        </RoleGroups>
                    </asp:LoginView>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </hc:ExtensionGridView>
</asp:Content>

