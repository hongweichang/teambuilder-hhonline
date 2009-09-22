<%@ Page Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master"
    AutoEventWireup="true" CodeFile="ManageShowPicture.aspx.cs" Inherits="ControlPanel_Site_ManageShowPicture"
    Title="展示图像" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="ContentOpts" ContentPlaceHolderID="cphOpts" runat="Server">
    <asp:LinkButton ID="lblNewShowPicture" runat="server" SkinID="lnkopts">
        <span>新 增</span>
    </asp:LinkButton>
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="cphContent" runat="Server">
    <hc:ExtensionGridView ID="egvShowPictures" AutoGenerateColumns="False" DataKeyNames="ShowPictureID"
        runat="server" PageSize="5" SkinID="DefaultView" OnRowDataBound="egvShowPictures_RowDataBound"
        OnRowDeleting="egvShowPictures_RowDeleting" OnRowUpdating="egvShowPictures_RowUpdating"
        OnPageIndexChanging="egvShowPictures_PageIndexChanging">
        <Columns>
            <asp:TemplateField HeaderText="缩略图">
                <HeaderStyle Width="100" />
                <ItemTemplate>
                    <asp:Image ID="ShowPictureThumb" Width="40" Height="40" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="图片标题" DataField="Title" DataFormatString="{0:S10}" />
            <asp:BoundField HeaderText="图片描述" DataField="Description"  DataFormatString="{0:S20}" />
            <asp:BoundField HeaderText="链接地址" DataField="Link" DataFormatString="{0:S20}"  />
            <asp:TemplateField>
                <HeaderStyle Width="200" />
                <HeaderTemplate>
                    操作</HeaderTemplate>
                <ItemTemplate>
                    <asp:LoginView ID="LoginView1" runat="server">
                        <RoleGroups>
                            <asp:RoleGroup Roles="VarietyModule-Edit">
                                <ContentTemplate>
                                    <asp:LinkButton ID="lnkUpdate" runat="server" CommandName="Update" SkinID="lnkedit"
                                        PostBackUrl="#"></asp:LinkButton>
                                </ContentTemplate>
                            </asp:RoleGroup>
                        </RoleGroups>
                    </asp:LoginView>
                    <asp:LoginView ID="LoginView2" runat="server">
                        <RoleGroups>
                            <asp:RoleGroup Roles="VarietyModule-Delete">
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
