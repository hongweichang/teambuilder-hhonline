<%@ Page Title="" Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master"
    AutoEventWireup="true" CodeFile="Task.aspx.cs" Inherits="ControlPanel_Site_Task" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="ContentOpts" ContentPlaceHolderID="cphOpts" runat="Server">
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="cphContent" runat="Server">
    <asp:Repeater ID="ThreadsRepeater" runat="server" OnItemCreated="ThreadsRepeater_ItemCreated"
        OnItemDataBound="ThreadsRepeater_ItemDataBound">
        <ItemTemplate>
            <h3><asp:Label ID="ThreadTitle" runat="server"></asp:Label></h3>
            <table class="postform">                
                <tr>
                    <th style="width:90px"><b>创建于</b></th>
                    <td><asp:Label ID="Created" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <th><b>上次启动</b></th>
                    <td><asp:Label ID="LastStart" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <th><b>上次终止</b></th>
                     <td><asp:Label ID="LastStop" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <th><b>是否运行</b></th>
                    <td><asp:Label ID="IsRunning" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <th><b>时间间隔</b></th>
                    <td><asp:Label ID="Minutes" runat="server"></asp:Label></td>
                </tr>
                </table>
                 <hc:ExtensionGridView ID="egvTasks" PageSize="100"
                        AutoGenerateColumns="false" runat="server" SkinID="DefaultView">
                    <Columns>
                        <asp:BoundField HeaderText = "类型" DataField="JobType" DataFormatString="{0:S30}" />
                        <asp:BoundField HeaderText = "已激活" DataField="Enabled" DataFormatString="{0:B}" />
                        <asp:BoundField HeaderText = "在运行" DataField="IsRunning"  DataFormatString="{0:B}" />
                        <asp:BoundField HeaderText = "启动" DataField="LastStarted" DataFormatString="{0:D}"  />
                        <asp:BoundField HeaderText = "终止" DataField="LastEnd" DataFormatString="{0:D}" />
                        <asp:BoundField HeaderText = "成功" DataField="LastSuccess" DataFormatString="{0:D}"  />
                    </Columns>
                </hc:ExtensionGridView>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
