<%@ Page Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master"
    AutoEventWireup="true" CodeFile="ProductAdd.aspx.cs" Inherits="ControlPanel_product_ProductAdd"
    Title="新增产品" %>

<%@ Register Src="ProductPropertyAdd.ascx" TagName="ProductPropertyAdd" TagPrefix="ucProperty" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="ContentOpts" ContentPlaceHolderID="cphOpts" runat="Server">
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="cphContent" runat="Server">
    <asp:MultiView ID="mvProductAdd" runat="server" ActiveViewIndex="0">
        <asp:View runat="server" ID="vwProductCategoies">
            <div class="cpStep">
                <span class="stepOne stepOneSelected">选择产品分类</span> <span class="stepTwo">填写产品详细信息</span>
            </div>
            <div class="catSelect">
                <div class="cs-row2">
                    <asp:TreeView ID="tvCategories" runat="server" ShowLines="true">
                        <SelectedNodeStyle BackColor="#000080" HorizontalPadding="5" ForeColor="White" />
                        <NodeStyle HorizontalPadding="5" CssClass="cs-cell" />
                    </asp:TreeView>
                </div>
                <div class="cs-row3">
                    <asp:Button runat="server" ID="btnNext" Text="好了，去发布商品" OnClick="btnNext_Click" />
                </div>
            </div>
        </asp:View>
        <asp:View runat="server" ID="vwProductDetail">
            <div class="cpStep">
                <span class="stepOne">选择产品分类</span> <span class="stepTwo stepTwoSelected">填写产品详细信息</span>
            </div>
            <div class="cpAdd">
                <table class="postform" cellpadding="10" cellspacing="10">
                    <tr>
                        <th>
                            产品分类信息
                        </th>
                        <td class="itemList">
                            <asp:PlaceHolder runat="server" ID="phCategoryName"></asp:PlaceHolder>
                            <asp:LinkButton runat="server" ID="btnBack" Text=" " CssClass="editPCat" CausesValidation="false"
                                OnClick="btnBack_Click" PostBackUrl="#"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            产品名称
                        </th>
                        <td>
                            <asp:TextBox ID="txtProductName" runat="server" Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="产品名称不能为空。" Display="Dynamic"
                                ControlToValidate="txtProductName"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            产品品牌
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlProductBrand" Width="305px" runat="server" EnableViewState="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            产品行业
                        </th>
                        <td class="itemList">
                            <ul id="tradeNames">
                            </ul>
                            <asp:HiddenField ID="hfTrade" runat="server" />
                            <asp:HiddenField ID="hfTradeList" runat="server" />
                            <a href="javascript:void(0)" class="selecttrade" onclick="selectTrade()"></a>
                        </td>
                    </tr>
                    <tr runat="server" id="rowProductProperty">
                        <th>
                            产品属性
                        </th>
                        <td class="itemList">
                            <ucProperty:ProductPropertyAdd ID="ucProductProperty" runat="server" EnableViewState="true" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            产品简介
                        </th>
                        <td>
                            <asp:TextBox ID="txtProductAbstract" Width="540px" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            关键字(使用英文法输入状态下";"分割)
                        </th>
                        <td>
                            <asp:TextBox ID="txtKeyWords" Width="540px" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            产品介绍
                        </th>
                        <td>
                            <hc:Editor ID="txtProductContent" runat="server" Width="500px" EditorMode="Enhanced" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            添加图片
                        </th>
                        <td class="itemList">
                            <div id="productImages">
                            </div>
                            <a href="javascript:void(0)" class="uploadimages" onclick="uploadImage()"></a>
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
                            是否上架
                        </th>
                        <td>
                            <hc:ComponentStatusList ID="csProduct" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            &nbsp;
                        </th>
                        <td>
                            <asp:Button runat="server" ID="btnSubmit" Text="确定" OnClick="btnSubmit_Click" />
                            &nbsp;&nbsp;
                            <asp:Button runat="server" ID="btnBackToProduct" Text="返回" OnClick="btnBackToProduct_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:View>
    </asp:MultiView>
    <hc:MsgBox ID="mbMessage" runat="server" SkinID="msgBox"></hc:MsgBox>
</asp:Content>
