<%@ Page Language="C#" MasterPageFile="~/Masters/DetailMasterPage.master" AutoEventWireup="true"
    CodeFile="SelectTags.aspx.cs" Inherits="Utility_SelectTags" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderDetail" runat="Server">
    <table cellpadding="0" cellspacing="0" class="detail">
        <tr>
            <th style="width: 100px">
                关键字
            </th>
            <td id="trades">
                <div style="height: 200px; overflow: auto">
                    <asp:Repeater ID="rpTag" runat="server">
                        <ItemTemplate>
                            <input type="checkbox" title='<%# Eval("Name") %>' value='<%# Eval("Name") %>' name='<%# Eval("Name") %>' />
                            <span style="cursor: pointer" onclick="var p = $(this).prev();p.attr('checked',!p.attr('checked'))">
                                <%# Eval("Name")%></span>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </td>
        </tr>
        <tr>
            <th>
                &nbsp;
            </th>
            <td>
                <asp:Button ID="btnPost" runat="server" Text=" 确 定 " CausesValidation="false" OnClientClick="getTrade();return cancel();">
                </asp:Button>
                <asp:Button ID="btnClose" runat="server" Text=" 关 闭 " CausesValidation="false" OnClientClick="return cancel();">
                </asp:Button>
            </td>
        </tr>
    </table>
</asp:Content>
