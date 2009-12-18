<%@ Page Title="" Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master"
    AutoEventWireup="true" CodeFile="ProductQuickAdd.aspx.cs" Inherits="ControlPanel_product_ProductQuickAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphOpts" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <hc:MsgBox ID="mbMsg" runat="server" SkinID="msgBox"></hc:MsgBox>
    <table class="postform" cellpadding="10" cellspacing="10">
        <tr>
            <th style="width: 20%" id="hfCatListHolder">
                产品分类信息
                <asp:HiddenField ID="hfCategories" runat="server" />
                <asp:HiddenField ID="hfCatHTML" runat="server" />
            </th>
            <td style="width: 80%">
                <span id="divCategories"></span><a href="javascript:{}" title="设置" onclick="selectCategories()"
                    class="setting">[设置]</a>
            </td>
        </tr>
        <tr>
            <th>
                产品名称
            </th>
            <td>
                <asp:TextBox ID="txtProductName" runat="server" Width="600px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv1" ControlToValidate="txtProductName" runat="server"
                    ErrorMessage="必须填写！"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                产品品牌
            </th>
            <td>
                <asp:DropDownList Width="300px" ID="ddlBrands" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th id="hfIndListHolder">
                产品行业
                <asp:HiddenField ID="hfIndustries" runat="server" />
                <asp:HiddenField ID="hfIndHTML" runat="server" />
            </th>
            <td>
                <span id="spanIndustries"></span><a href="javascript:{}" title="设置" onclick="selectIndustries()"
                    class="setting">[设置]</a>
            </td>
        </tr>
        <tr>
            <th>
                产品关键字
            </th>
            <td>
                <asp:TextBox ID="txtKeywords" runat="server" Width="600px" TextMode="MultiLine" Height="40px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv2" ControlToValidate="txtKeywords" runat="server"
                    ErrorMessage="必须填写！"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                产品摘要/简介
            </th>
            <td>
                <asp:TextBox ID="txtAbstract" runat="server" Width="600px" TextMode="MultiLine" Height="80px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv3" ControlToValidate="txtAbstract" runat="server"
                    ErrorMessage="必须填写！"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                详细介绍
            </th>
            <td>
                <hc:Editor ID="txtContent" runat="server" Width="606px" EditorMode="Enhanced" />
            </td>
        </tr>
        <tr>
            <th>
                产品图片
            </th>
            <td>
                <div id="productImages" style="width: 606px;">
                </div>
                <a href="javascript:{}" onclick="uploadImage()" title="设置" class="setting">[设置]</a>
            </td>
        </tr>
        <asp:Panel ID="pnlPrice" runat="server">
            <tr>
                <th>
                    产品栏目(30天有效期)
                </th>
                <td>
                    <hc:FocusTypeList ID="ddlFocusType" runat="server" RepeatDirection="Horizontal" SelectedValue="None"
                        CssClass="focusType">
                    </hc:FocusTypeList>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="padding-left: 20px; color: Red;">
                    报价说明：无市场价或市场价格为0时，各级报价都无效；各级报价为0时表示“需询价”
                </td>
            </tr>
            <tr>
                <th>
                    市场价(必填项)
                </th>
                <td>
                    <asp:TextBox ID="txtMarketPrice" runat="server" Width="200px">0</asp:TextBox>
                    <asp:RegularExpressionValidator ID="rev1" runat="server" ControlToValidate="txtMarketPrice"
                        ValidationExpression="^(-)?(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$" ErrorMessage="必须是数字(可精确到小数点后2位)！"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <th>
                    终端正常价<sup>1级</sup>
                </th>
                <td>
                    <asp:TextBox ID="txtPrice1" runat="server" Width="200px">0</asp:TextBox>
                    <asp:RegularExpressionValidator ID="rev2" runat="server" ControlToValidate="txtPrice1"
                        ValidationExpression="^(-)?(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$" ErrorMessage="必须是数字(可精确到小数点后2位)！"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <th>
                    终端最低价<sup>2级</sup>
                </th>
                <td>
                    <asp:TextBox ID="txtPrice2" runat="server" Width="200px">0</asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPrice2"
                        ValidationExpression="^(-)?(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$" ErrorMessage="必须是数字(可精确到小数点后2位)！"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <th>
                    终端VIP价<sup>3级</sup>
                </th>
                <td>
                    <asp:TextBox ID="txtPrice3" runat="server" Width="200px">0</asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtPrice3"
                        ValidationExpression="^(-)?(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$" ErrorMessage="必须是数字(可精确到小数点后2位)！"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <th>
                    经销商正常价<sup>4级</sup>
                </th>
                <td>
                    <asp:TextBox ID="txtPrice4" runat="server" Width="200px">0</asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtPrice4"
                        ValidationExpression="^(-)?(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$" ErrorMessage="必须是数字(可精确到小数点后2位)！"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <th>
                    经销商最低价<sup>5级</sup>
                </th>
                <td>
                    <asp:TextBox ID="txtPrice5" runat="server" Width="200px">0</asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtPrice5"
                        ValidationExpression="^(-)?(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$" ErrorMessage="必须是数字(可精确到小数点后2位)！"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <th>
                    促销价(仅用于促销)
                </th>
                <td>
                    <asp:TextBox ID="txtPromotionPrice" runat="server" Width="200px">0</asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtPromotionPrice"
                        ValidationExpression="^(-)?(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$" ErrorMessage="必须是数字(可精确到小数点后2位)！"></asp:RegularExpressionValidator>
                </td>
            </tr>
        </asp:Panel>
        <asp:Panel ID="pnlNavigation" runat="server" Visible="false">
            <tr>
                <th>
                    栏目设置
                </th>
                <td class="cat_navC">
                    <asp:LinkButton ID="lnkSetFocus" runat="server" Text="跳转到栏目设置页"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <th>
                    价格设置
                </th>
                <td class="cat_navC">
                    <asp:LinkButton ID="lnkSetPrice" runat="server" Text="跳转到价格设置页"></asp:LinkButton>
                </td>
            </tr>
        </asp:Panel>
        <tr>
            <th>
                是否上架
            </th>
            <td>
                <hc:YesNoRadioButtonList ID="rdPublish" runat="server" DefaultValue="true">
                </hc:YesNoRadioButtonList>
            </td>
        </tr>
        <tr>
            <th>
                &nbsp;
            </th>
            <td>
                <asp:Button ID="btnPost" runat="server" Text=" 提交 " OnClick="btnPost_Click" />
                &nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" Text=" 返回 " OnClick="btnBack_Click" CausesValidation="false" />
            </td>
        </tr>
    </table>
</asp:Content>
