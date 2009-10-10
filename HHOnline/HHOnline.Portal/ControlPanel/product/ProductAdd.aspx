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
                <span class="stepOne stepOneSelected">产品分类</span> 
                <span class="stepTwo">产品详细信息</span> 
                <span class="stepThree">产品相关型号信息</span>
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
                <span class="stepOne">产品分类</span> 
                <span class="stepTwo stepTwoSelected">产品详细信息</span> 
                <span class="stepThree">产品相关型号信息</span>
            </div>
            <div class="cpAdd">
                <table class="postform" cellpadding="10" cellspacing="10">
                    <tr>
                        <th>
                            产品分类信息
                        </th>
                        <td class="itemList">
                            <div style="width:600px">
                            <asp:PlaceHolder runat="server" ID="phCategoryName"></asp:PlaceHolder>
                            <asp:LinkButton runat="server" ID="btnBack" Text=" " CssClass="editPCat" CausesValidation="false"
                                OnClick="btnBack_Click" PostBackUrl="#"></asp:LinkButton>
                            </div>
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
                            <ul id="tradeNames" style="width:600px">
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
                            <div style="width:600px">
                            <ucProperty:ProductPropertyAdd ID="ucProductProperty" runat="server" EnableViewState="true" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            产品简介
                        </th>
                        <td>
                            <asp:TextBox ID="txtProductAbstract" Width="600px" runat="server" MaxLength="200" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            关键字
                        </th>
                        <td>
                            <asp:TextBox ID="txtKeyWords" Width="600px" runat="server" MaxLength="200" />(使用分号划分)
                        </td>
                    </tr>
                    <tr>
                        <th>
                            产品介绍
                        </th>
                        <td>
                            <hc:Editor ID="txtProductContent" runat="server" Width="540px" EditorMode="Enhanced" />
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
                            <asp:Button runat="server" ID="btnModel" Visible="false" Text="型号管理" OnClick="btnModel_Click" />
                            &nbsp;&nbsp;
                            <asp:Button runat="server" ID="btnBackToProduct" Text="返回" CausesValidation="false" OnClick="btnBackToProduct_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:View>
        
        <asp:View runat="server" ID="vwProductModel">
            <div class="cpStep">
                <span class="stepOne">产品分类</span> 
                <span class="stepTwo">产品详细信息</span> 
                <span class="stepThree stepThreeSelected">产品相关型号信息</span>
            </div>
            <div style="padding:10px;">
            <table class="postform" cellpadding="10" cellspacing="10">
                <tr>
                    <th style="width:100px;">所有型号</th>
                    <td>
                        <asp:GridView ID="gvCurrentModel" runat="server" AutoGenerateColumns="false" AllowPaging="false" OnRowEditing="gvCurrentModel_RowEditing"
                                BackColor="White"  AlternatingRowStyle-BackColor="AliceBlue"  OnRowDeleting="gvCurrentModel_RowDeleting"
                                BorderStyle="solid" BorderWidth="1px" BorderColor="#C1DAD7" CssClass="mytable" DataKeyNames="ModelID"
                                OnRowCancelingEdit="gvCurrentModel_RowCancelingEdit" OnRowUpdating="gvCurrentModel_RowUpdating">
                            <RowStyle HorizontalAlign="Left" CssClass="mytabletd" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <EmptyDataRowStyle CssClass="mytableempty" />
                            <EmptyDataTemplate>
                               无相关型号信息，请录入！
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="150px">                                    
                                    <HeaderTemplate>名称</HeaderTemplate>
                                    <ItemTemplate><%# Eval("ModelName") %></ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtInnerModelName" Text='<%# Eval("ModelName") %>' Width="120px" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ValidationGroup="isb" Display="Dynamic" ID="rfv1" ControlToValidate="txtInnerModelName" runat="server" ErrorMessage="必须填写！"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="100px">                                    
                                    <HeaderTemplate>编码</HeaderTemplate>
                                    <ItemTemplate><%# Eval("ModelCode") %></ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtInnerModelCode" Text='<%# Eval("ModelCode") %>' Width="80px"  runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ValidationGroup="isb" Display="Dynamic" ID="rfv2" ControlToValidate="txtInnerModelCode" runat="server" ErrorMessage="必须填写！"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>                                    
                                    <HeaderTemplate>描述</HeaderTemplate>
                                    <ItemTemplate><%# Eval("ModelDesc") %></ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtInnerDesc" Text='<%# Eval("ModelDesc") %>' Width="420px"  runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ValidationGroup="isb" Display="Dynamic" ID="rfv3" ControlToValidate="txtInnerDesc" runat="server" ErrorMessage="必须填写！"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  ItemStyle-Width="120px">
                                    <HeaderTemplate>操作</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" Text=" " SkinID="lnkedit" CausesValidation="false"></asp:LinkButton>
                                        <asp:LinkButton ID="btnDelete" runat="server" OnClientClick="return confirm('你确定要删除此类型吗？')" CommandName="Delete" Text=" " SkinID="lnkdelete" CausesValidation="false"></asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="btnSave" ValidationGroup="isb" CommandName="Update" runat="server" Text=" " SkinID="lnksave"></asp:LinkButton>
                                        <asp:LinkButton ID="btnCancel" CommandName="Cancel" runat="server" Text=" " SkinID="lnkcancel" CausesValidation="false"></asp:LinkButton>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <th>型号编码</th>
                    <td><asp:TextBox ID="txtCode" runat="server" MaxLength="25"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="sub" Display="Dynamic" ID="rfvCode" ControlToValidate="txtCode" runat="server" ErrorMessage="必须填写！"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <th>型号名称</th>
                    <td><asp:TextBox ID="txtModelName" runat="server" MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="sub" Display="Dynamic"  ID="RequiredFieldValidator1" ControlToValidate="txtModelName" runat="server" ErrorMessage="必须填写！"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <th>简要描述</th>
                    <td><asp:TextBox ID="txtModelDesc" Width="400px" runat="server" MaxLength="100"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="sub"  Display="Dynamic" ID="RequiredFieldValidator2" ControlToValidate="txtModelDesc" runat="server" ErrorMessage="必须填写！"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                 <tr>
                    <th>
                        &nbsp;
                    </th>
                    <td>
                        <asp:Button runat="server" ValidationGroup="sub"  ID="btnSaveModel" Text="保存" OnClick="btnSaveModel_Click" />
                        &nbsp;&nbsp;
                        <asp:Button runat="server" ID="btnBackProductInfo" Text="修改产品信息" CausesValidation="false" OnClick="btnBackProductInfo_Click" />
                        &nbsp;&nbsp;
                        <asp:Button runat="server" ID="btnBackTP" Text="浏览产品" CausesValidation="false" OnClick="btnBackToProduct_Click" />
                    </td>
                </tr>
            </table>
            </div>
        </asp:View>
    </asp:MultiView>
    <hc:MsgBox ID="mbMessage" runat="server" SkinID="msgBox"></hc:MsgBox>
</asp:Content>
