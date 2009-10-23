<%@ Page Title="" Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master" AutoEventWireup="true" CodeFile="ManageSearchWords.aspx.cs" Inherits="ControlPanel_Site_ManageSearchWords" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphOpts" Runat="Server">
    <asp:LinkButton ID="lbNewProduct" runat="server" SkinID="lnkopts" OnClientClick="return addSearchWord();">
        <span>新 增</span>
    </asp:LinkButton>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" Runat="Server">

    <hc:ExtensionGridView runat="server" ID="egvSW"  OnPageIndexChanging="egvSW_PageIndexChanging"
        PageSize="20" SkinID="DefaultView" AutoGenerateColumns="False" DataKeyNames="StatisticID"
        OnRowUpdating="egvSW_RowUpdating" OnRowDeleting="egvSW_RowDeleting" OnRowEditing="egvSW_RowEditing"
        OnRowCancelingEdit="egvSW_RowCancelingEdit">
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>关键字</HeaderTemplate>
                <ItemTemplate><%# Eval("SearchWord") %></ItemTemplate>
                <EditItemTemplate><%# Eval("SearchWord") %></EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderStyle Width="120px" />
                <HeaderTemplate>搜索次数</HeaderTemplate>
                <ItemTemplate>
                    <%# Eval("HitCount") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtCount" Width="50px" runat="server" Text='<%# Eval("HitCount") %>'></asp:TextBox>
                    <asp:RegularExpressionValidator ID="rev1" runat="server" ErrorMessage="只能输入正整数！" Text="" Display="Dynamic"
                         ValidationExpression="^[0-9]*[1-9][0-9]*$" ControlToValidate="txtCount" ></asp:RegularExpressionValidator>
                </EditItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField>
                <HeaderStyle Width="150px" />
                <HeaderTemplate>创建时间</HeaderTemplate>
                <ItemTemplate><%# Eval("CreateTime","{0:D}")%></ItemTemplate>
                <EditItemTemplate><%# Eval("CreateTime")%></EditItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField>
                <HeaderStyle Width="150px" />
                <HeaderTemplate>更新时间</HeaderTemplate>
                <ItemTemplate><%# Eval("UpdateTime","{0:D}")%></ItemTemplate>
                <EditItemTemplate><%# Eval("UpdateTime")%></EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderStyle Width="130px" />
                <HeaderTemplate>操作</HeaderTemplate>
                <ItemTemplate>
                    <asp:LoginView ID="LoginView1" runat="server">
                        <RoleGroups>
                            <asp:RoleGroup Roles="SearchWordModule-View">
                                <ContentTemplate>
                                    <asp:LinkButton ID="lnkUpdate" runat="server" CommandName="Edit" SkinID="lnkedit"
                                        PostBackUrl="#" CausesValidation="false"></asp:LinkButton>
                                </ContentTemplate>
                            </asp:RoleGroup>
                        </RoleGroups>
                    </asp:LoginView>
                    <asp:LoginView ID="LoginView2" runat="server">
                        <RoleGroups>
                            <asp:RoleGroup Roles="SearchWordModule-View">
                                <ContentTemplate>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" SkinID="lnkdelete" OnClientClick="return confirm('确定要删除此记录吗？');"
                                        PostBackUrl="#" CausesValidation="false"></asp:LinkButton>
                                </ContentTemplate>
                            </asp:RoleGroup>
                        </RoleGroups>
                    </asp:LoginView>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Update" SkinID="lnksave"
                                        PostBackUrl="#" CausesValidation="true"></asp:LinkButton>
                    <asp:LinkButton ID="lnkCancel" runat="server" CommandName="Cancel" SkinID="lnkcancel"
                                        PostBackUrl="#" CausesValidation="false"></asp:LinkButton>
                </EditItemTemplate>
            </asp:TemplateField>
        </Columns>
    </hc:ExtensionGridView>
</asp:Content>

