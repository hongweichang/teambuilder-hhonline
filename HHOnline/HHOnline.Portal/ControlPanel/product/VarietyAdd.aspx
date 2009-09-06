<%@ Page Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master"
    AutoEventWireup="true" CodeFile="VarietyAdd.aspx.cs" Inherits="ControlPanel_product_VarietyAdd"
    Title="无标题页" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="ContentOpts" ContentPlaceHolderID="cphOpts" runat="Server">
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="cphContent" runat="Server">

    <script type="text/javascript">
function IsNewGroup(si)
{
     var groupRow = $('#<%=newGroupRow.ClientID %>');
     var sel = $(si);
     var v = sel.find('option:eq(0)');
     var v1 = sel.val();
      if (groupRow && v1 == v.val())
        {
            groupRow.show();
            return;
        }  
        
        if (groupRow)
        {
            groupRow.hide();
        }
}
    </script>

    <table class="postform" cellpadding="10" cellspacing="10">
        <tr>
            <th style="width: 100px;">
                品牌名称
            </th>
            <td>
                <asp:TextBox ID="txtBrandName" runat="server" Width="200" MaxLength="50" />
                <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="品牌名称不能为空。" Display="Dynamic"
                    ControlToValidate="txtBrandName"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr runat="server" id="groupSelectRow">
            <th>
                品牌分组
            </th>
            <td>
                <asp:DropDownList ID="ddlBrandGroup" runat="server" onchange="IsNewGroup(this)" />
            </td>
        </tr>
        <tbody runat="server" id="newGroupRow" style="display: none;">
            <tr >
                <th>
                    新建分组
                </th>
                <td>
                    <asp:TextBox ID="txtBrandGroup" runat="server" Width="200" MaxLength="100" />
                </td>
            </tr>
        </tbody>
        <tr>
            <th>
                标题说明
            </th>
            <td>
                <asp:TextBox ID="txtBrandTitle" runat="server" Width="500" MaxLength="100" />
            </td>
        </tr>
        <tr>
            <th>
                介绍摘要
            </th>
            <td>
                <asp:TextBox ID="txtBrandAbstract" runat="server" Width="500" />
            </td>
        </tr>
        <tr>
            <th>
                介绍内容
            </th>
            <td>
                <hc:Editor ID="txtBrandContent" runat="server" Width="500" />
            </td>
        </tr>
        <tr>
            <th>
                品牌商标
            </th>
            <td>
                <asp:Image runat="server" ID="imgLogo" Width="80" Height="80" />
            </td>
        </tr>
        <tr>
            <th>
                &nbsp;
            </th>
            <td>
                <asp:FileUpload ID="fuLogo" runat="server" Width="200" />
            </td>
        </tr>
        <tr>
            <th>
                排序序号
            </th>
            <td>
                <asp:TextBox ID="txtDisplayOrder" runat="server" MaxLength="8" Text="0"></asp:TextBox>
                <asp:RegularExpressionValidator ID="rev1" runat="server" ControlToValidate="txtDisplayOrder"
                    ValidationExpression="(\d){1,3}" ErrorMessage="必须为0-999的数字"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="rfv3" Display="Dynamic" runat="server" ControlToValidate="txtDisplayOrder"
                    ErrorMessage="必须填写！"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                是否启用
            </th>
            <td>
                <hc:ComponentStatusList ID="csBrand" runat="server" />
            </td>
        </tr>
        <tr>
            <th>
                &nbsp;
            </th>
            <td>
                <hc:MsgBox ID="mbMessage" runat="server" SkinID="msgBox"></hc:MsgBox>
            </td>
        </tr>
        <tr>
            <th>
                &nbsp;
            </th>
            <td>
                <asp:Button ID="btnPost" runat="server" Text="提 交" PostBackUrl="#" Visible="false"
                    OnClick="btnPost_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnPostBack" runat="server" Text="返 回" Visible="false" CausesValidation="false" />
            </td>
        </tr>
    </table>
</asp:Content>
