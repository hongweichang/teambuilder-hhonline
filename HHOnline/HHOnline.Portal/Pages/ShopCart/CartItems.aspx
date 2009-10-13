<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/CartMasterPage.master" AutoEventWireup="true" CodeFile="CartItems.aspx.cs" Inherits="Pages_ShopCart_CartItems" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderHeadName" Runat="Server">
<asp:SiteMapPath ID="smpcart" runat="server">

</asp:SiteMapPath>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderCart" Runat="Server">
<div class="cart_header">
    <div class="cart_header_c">我的购物车</div>
</div>
<div class="cart_list">
    <hc:ExtensionGridView ID="egvShoppings" PageSize="100" DataKeyNames="ShoppingID"  OnPageIndexChanging="egvShoppings_PageIndexChanging"
        AutoGenerateColumns="false" runat="server" SkinID="DefaultView" OnRowDataBound="egvShoppings_RowDataBound"
         OnRowEditing="egvShoppings_RowEditing" OnRowDeleting="egvShoppings_RowDeleting" OnRowCancelingEdit="egvShoppings_RowCancelingEdit" 
         OnRowUpdating="egvShoppings_RowUpdating"
        >
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate><div style="padding-left:20px;">产品</div></HeaderTemplate>
                <ItemTemplate>
                    <asp:Literal ID="ltProductName" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField ItemStyle-Width="150px">
                <HeaderTemplate>型号</HeaderTemplate>
                <ItemTemplate>
                    <asp:Literal ID="ltModelName" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="50px">
                <HeaderTemplate>数目</HeaderTemplate>
                <ItemTemplate>
                    <%# Eval("Quantity") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtAmount" Text='<%# Eval("Quantity") %>' MaxLength="4" Width="30px" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="txtAmount" ErrorMessage="必须填写！" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="rev1" runat="server" ControlToValidate="txtAmount" ErrorMessage="只能是大于0的数字！" Display="Dynamic" ValidationExpression="\d{1,4}"></asp:RegularExpressionValidator>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="70px">
                <HeaderTemplate>单价(元)</HeaderTemplate>
                <ItemTemplate>
                    <asp:Literal ID="ltPrice" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="60px">
                <HeaderStyle HorizontalAlign="Center" />
                <HeaderTemplate>&nbsp;</HeaderTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="btnEdit" PostBackUrl="#" runat="server" CommandName="Edit" Text=" " SkinID="lnkedit" CausesValidation="false"></asp:LinkButton>
                    <asp:LinkButton ID="btnDelete" PostBackUrl="#"  runat="server" OnClientClick="return confirm('你确定要将此产品从购物车中删除吗？')" CommandName="Delete" Text=" " SkinID="lnkdelete" CausesValidation="false"></asp:LinkButton>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="btnSave" PostBackUrl="#"  CommandName="Update" runat="server" Text=" " SkinID="lnksave"></asp:LinkButton>
                    <asp:LinkButton ID="btnCancel" PostBackUrl="#" CommandName="Cancel" runat="server" Text=" " SkinID="lnkcancel" CausesValidation="false"></asp:LinkButton>
                </EditItemTemplate>
            </asp:TemplateField>
        </Columns>
    </hc:ExtensionGridView>
    <div class="cart_amount">
       总计：<asp:Literal ID="ltItemsAmount" runat="server"></asp:Literal>
    </div>
    <div class="cart_askprice"><a href="view.aspx?shopcart-askprice" class="favcar askprice">&nbsp;</a></div>
</div>
</asp:Content>

