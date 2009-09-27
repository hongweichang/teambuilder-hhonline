﻿<%@ Page Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master"
    AutoEventWireup="true" CodeFile="ManageIndex.aspx.cs" Inherits="ControlPanel_Site_ManageIndex"
    Title="无标题页" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="ContentOpts" ContentPlaceHolderID="cphOpts" runat="Server">
    <asp:LinkButton ID="lbBuildAllIndex" runat="server" SkinID="lnkopts" OnClick="lbBuildAllIndex_Click"
        OnClientClick="return confirm('确定要重建所有索引吗？')">
        <span>重建所有索引</span>
    </asp:LinkButton>
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="cphContent" runat="Server">
    <asp:Repeater runat="server" ID="IndexReportRepeater">
        <HeaderTemplate>
            <h3>
                索引状态</h3>
            <ul>
        </HeaderTemplate>
        <ItemTemplate>
            <li>
                <label>
                    索引文件内容：</label>
                <asp:Label ID="lblIndexName" runat="server" />
            </li>
            <li>
                <label>
                    索引文件路径：</label>
                <asp:Label ID="lblIndexPath" runat="server" />
            </li>
            <li>
                <label>
                    索引文件大小：</label>
                <asp:Label ID="lblIndexLength" runat="server" />
            </li>
            <li>
                <label>
                    上次更新时间：</label>
                <asp:Label ID="lblIndexModify" runat="server" />
            </li>
            <li>
                <br />
                <asp:Label ID="lblIndexKey" Visible="false" runat="server" />
                <asp:LinkButton ID="lbBuildIndex" runat="server" SkinID="lnkopts" CommandName="BuildIndex"
                    OnClientClick="return confirm('确定要重建索引吗？')" PostBackUrl="#">
                    <span>重建索引</span>
                </asp:LinkButton>
            </li>
        </ItemTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
