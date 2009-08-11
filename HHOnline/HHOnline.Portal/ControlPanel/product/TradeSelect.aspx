<%@ Page Title="选择产品行业" Language="C#" MasterPageFile="~/Masters/DetailMasterPage.master" AutoEventWireup="true" CodeFile="TradeSelect.aspx.cs" Inherits="ControlPanel_product_TradeSelect" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderDetail" Runat="Server">
<table cellpadding="0" cellspacing="0" class="detail" >
    <tr>
        <th style="width:100px">行业</th>
        <td id="trades">
            <div style="height:200px;overflow:auto">
                <asp:Repeater ID="rpTrade" runat="server">
                    <ItemTemplate>
                        <input type="checkbox" title='<%# Eval("IndustryTitle") %>' value='<%# Eval("IndustryID") %>' name='<%# Eval("IndustryName") %>' />
                        <span style="cursor:pointer" onclick="var p = $(this).prev();p.attr('checked',!p.attr('checked'))"><%# Eval("IndustryName") %></span>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </td>
    </tr>
    <tr>
        <th>&nbsp;</th>
        <td >
            <asp:Button ID="btnPost" runat="server" Text=" 确 定 " CausesValidation="false" OnClientClick="getTrade();return cancel();"></asp:Button>
            <asp:Button ID="btnClose" runat="server" Text=" 关 闭 " CausesValidation="false" OnClientClick="return cancel();"></asp:Button>
        </td>
    </tr>
</table>
<script type="text/javascript">

    function getTrade() {
        var t = $('#trades').find('input[checked=true]');
        if (t.length > 0) {
            var tids = t.map(function() {
                return '[' + $(this).val() + ']';
            }).get().join(',');
            var tns = t.map(function() {
                var mm=$(this);
                return '<li rel="' + mm.val() + '" title="' + mm.attr('title') + '">' + mm.attr('name') +
                                '<a title="删除" href="javascript:void(0)" >&nbsp;</a>' +
                            '</li>';
            }).get().join('');
            parent.window.insertTrade(tids, tns);
        }
    }
    $().ready(function() {
        var pp = parent.window.selectedTrades;
        if (parent.window.selectedTrades != null && pp != '') {
            var v, i;
            $('#trades').find('input').each(function() {
                v = $(this).val();
                if (pp.indexOf(v) >= 0) {
                    $(this).attr('checked', true);
                }
            })
        }
    })
</script>
</asp:Content>

