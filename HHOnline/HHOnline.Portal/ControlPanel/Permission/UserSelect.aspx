<%@ Page Title="新增部门" Language="C#" MasterPageFile="~/Masters/DetailMasterPage.master" AutoEventWireup="true" CodeFile="UserSelect.aspx.cs" Inherits="ControlPanel_Permission_UserSelect" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderDetail" Runat="Server">
    <div class="selectuser" id="selectUserForRole">
    <div class="su-c1">
        <div class="su-c1-c1">待选择用户</div>
        <div class="su-c1-c2" id="treeviewOrganize">
            <asp:TreeView ID="tvOrganize" runat="server" Width="100%" ShowLines="true">
                <NodeStyle HorizontalPadding="5" VerticalPadding="2" CssClass="user-select" />
            </asp:TreeView>
        </div>
    </div>    
    <div class="su-c2">
        <asp:LinkButton ID="lbAdd" OnClientClick="return addUser()" runat="server" OnClick="lbAdd_Click" CssClass="userrole-add userrole-opts" Text=" " PostBackUrl="#"></asp:LinkButton>
        <asp:LinkButton ID="lbDelete" OnClientClick="return deleteUser()" OnClick="lbDelete_Click" runat="server" CssClass="userrole-delete userrole-opts" Text=" " PostBackUrl="#"></asp:LinkButton>
        <asp:LinkButton ID="lbSave" OnClick="lbSave_Click" runat="server" CssClass="userrole-save userrole-opts" Text=" " PostBackUrl="#"></asp:LinkButton>
        <asp:LinkButton ID="lbCancel" OnClientClick="return cancelSelect();" runat="server" CssClass="userrole-cancel userrole-opts" Text=" " PostBackUrl="#"></asp:LinkButton>
    </div>
    <div class="su-c3">
        <div class="su-c3-c1">已选择用户</div>
        <div class="su-c3-c2" id="treeViewUser">          
            <asp:TreeView ID="tvUsers" runat="server" Width="100%" ShowLines="false">
                <NodeStyle HorizontalPadding="5" VerticalPadding="2" CssClass="user-select" />                
            </asp:TreeView>
        </div>
    </div>
    </div>
</asp:Content>
