<%@ Page Title="" Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master"
    AutoEventWireup="true" CodeFile="LinkURL.aspx.cs" Inherits="ControlPanel_Site_LinkURL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphOpts" runat="Server">
<asp:LinkButton ID="lnkAdd" runat="server" PostBackUrl="#" SkinID="lnkopts" OnClientClick="return addLinkUrl();" >
    <span>新增</span>
</asp:LinkButton>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <div class="linkStyle">
        <asp:Repeater ID="rpList" runat="server" OnItemCommand="rpList_ItemCommand">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <a class="title" href='<%# Eval("Url") %>' target="_blank"><%# Eval("Title") %></a>
                    <span>--<%#Eval("Desc") %></span>
                    &nbsp;&nbsp;                    
                    <asp:LinkButton ID="lnkDelete" CommandName="Delete" OnClientClick="return confirm('确定要删除此链接吗？');" 
                        CommandArgument='<%# Eval("ID") %>' runat="server" CssClass="delete" Text="[删除]"></asp:LinkButton>
                </li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
