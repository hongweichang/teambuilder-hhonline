<%@ Page Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master"
    AutoEventWireup="true" CodeFile="ManageIndex.aspx.cs" Inherits="ControlPanel_Site_ManageIndex"
    Title="无标题页" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="ContentOpts" ContentPlaceHolderID="cphOpts" runat="Server">
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="cphContent" runat="Server">
    <asp:Repeater runat="server">
        <HeaderTemplate>
            <h3>
                索引状态</h3>
            <ul>
            </ul>
        </HeaderTemplate>
        <ItemTemplate>
            <li>
                <label>
                    索引文件内容：</label>
            </li>
            <li>
                <label>
                    索引文件路径：</label>
            </li>
            <li>
                <label>
                    索引文件大小：</label>
            </li>
            <li>
                <label>
                    上次更新时间：</label>
            </li>
            <li>
                <br />
                <a href="#">重建索引</a> </li>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
