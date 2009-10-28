<%@ Page Title="" Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master" AutoEventWireup="true" CodeFile="FriendLink.aspx.cs" Inherits="ControlPanel_Common_FriendLink" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphOpts" Runat="Server">
<asp:LinkButton ID="lnkAdd" runat="server" PostBackUrl="#" SkinID="lnkopts" OnClientClick="return addFriendLink();" >
    <span>新增</span>
</asp:LinkButton>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" Runat="Server">
 <div class="linkStyle">
        <asp:Repeater ID="rpList" runat="server" OnItemCommand="rpList_ItemCommand" OnItemDataBound="rpList_ItemDataBound">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <a class="title" href='<%# Eval("Url") %>' target="_blank"><%# Eval("Title") %></a>
                    <span>--重要度: <%#Eval("Rank") %></span>
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="lnkEdit" OnClientClick='return editFriendLink(this);' ref='<%# Eval("ID") %>' CommandName="Edit" 
                                        runat="server" CssClass="delete" Text="[编辑]"></asp:LinkButton>
                    <asp:LinkButton ID="lnkDelete" CommandName="Delete" OnClientClick="return confirm('确定要删除此链接吗？');" CommandArgument='<%# Eval("ID") %>' runat="server" CssClass="delete" Text="[删除]"></asp:LinkButton>
                </li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>

