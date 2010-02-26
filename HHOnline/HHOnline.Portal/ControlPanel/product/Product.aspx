<%@ Page Title="" Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master"
    AutoEventWireup="true" CodeFile="Product.aspx.cs" Inherits="ControlPanel_Product_Product" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="ContentOpts" ContentPlaceHolderID="cphOpts" runat="Server">
    <asp:LinkButton ID="lbQuickNew" runat="server" SkinID="lnkopts">
        <span>新增</span>
    </asp:LinkButton>
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="cphContent" runat="Server">
    <div class="detail-r1c1" style="border: solid 1px #ccc;">
        <table cellpadding="0" cellspacing="0" class="detail">
            <tr>
                <th>
                    名称
                </th>
                <td>
                    <asp:TextBox ID="txtProductName" runat="server"></asp:TextBox>
                </td>
                <th>
                    品牌
                </th>
                <td>
                    <asp:DropDownList ID="ddlBrands" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
                <th>
                    行业
                </th>
                <td>
                    <asp:DropDownList ID="ddlIndustry" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
                <th>
                    分类
                </th>
                <td>
                    <asp:DropDownList ID="ddlCategory" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th rowspan="2">
                    快速过滤
                </th>
                <td colspan="6">
                    <div class="productNav">
                        <asp:LinkButton ID="lnkAll" runat="server" OnClick="lnk_Click">所有产品</asp:LinkButton>
                        <asp:LinkButton ID="lnkPublished" runat="server" OnClick="lnk_Click">已发布</asp:LinkButton>
                        <asp:LinkButton ID="lnkUnPublishied" runat="server" OnClick="lnk_Click">未发布</asp:LinkButton>
                        <asp:LinkButton ID="lnkPriced" runat="server" OnClick="lnk_Click">已报价</asp:LinkButton>
                        <asp:LinkButton ID="lnkNoPriced" runat="server" OnClick="lnk_Click">未报价</asp:LinkButton>
                        <asp:LinkButton ID="lnkPicture" runat="server" OnClick="lnk_Click">有图商品</asp:LinkButton>
                        <asp:LinkButton ID="lnkNoPicture" runat="server" OnClick="lnk_Click">无图商品</asp:LinkButton>
                    </div>
                </td>
                <td rowspan="3">
                    <asp:Button ID="lnkSearch" runat="server" Text="查找产品" Height="50px" OnClick="lnkSearch_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <div class="productNav">       
                        <asp:LinkButton ID="lnProvider" runat="server" OnClick="lnk_Click">供应商提供产品</asp:LinkButton>                 
                        <asp:LinkButton ID="lnProviderInspect" runat="server" OnClick="lnk_Click">供应商已发布产品</asp:LinkButton>
                        <asp:LinkButton ID="lnProviderDeny" runat="server" OnClick="lnk_Click">供应商未发布产品</asp:LinkButton>
                    </div>                    
                </td>
            </tr>
            <tr>
                <th>
                    显示
                </th>
                <td colspan="6">
                    <asp:Label ID="lblTip" runat="server" Text="Label"></asp:Label>的商品.
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div id="cmContainer">
    <hc:ExtensionGridView runat="server" ID="egvProducts" OnRowDataBound="egvProducts_RowDataBound"
        OnRowDeleting="egvProducts_RowDeleting" OnRowUpdating="egvProducts_RowUpdating" AllowPaging="true"
        OnRowCommand="egvProducts_RowCommand" OnPageIndexChanging="egvProducts_PageIndexChanging"
        PageSize="10" SkinID="DefaultView" AutoGenerateColumns="False" DataKeyNames="ProductID">
        <Columns>
           
            <asp:BoundField HeaderText="名称" DataField="ProductName"  DataFormatString="{0:S40}" ItemStyle-Width="500"/>
            <asp:BoundField HeaderText="品牌" DataField="BrandName" />
            <asp:BoundField HeaderText="发布" DataField="ProductStatus" DataFormatString="{0:G}" ItemStyle-Width="40" />
             <asp:TemplateField HeaderText="来源">
                <HeaderStyle/>
                <ItemTemplate>
                    <asp:Literal ID="ltComming" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderStyle Width="120" />
                <HeaderTemplate>
                    操作</HeaderTemplate>
                <ItemTemplate>
                    <asp:LoginView ID="LoginView1" runat="server">
                        <RoleGroups>
                            <asp:RoleGroup Roles="ProductModule-Edit">
                                <ContentTemplate>
                                    <asp:LinkButton ID="lnkUpdate" runat="server" CommandName="Update" SkinID="lnkedit"
                                        PostBackUrl="#"></asp:LinkButton>
                                </ContentTemplate>
                            </asp:RoleGroup>
                        </RoleGroups>
                    </asp:LoginView>
                    <asp:LoginView ID="LoginView4" runat="server">
                        <RoleGroups>
                            <asp:RoleGroup Roles="ProductModule-Edit">
                                <ContentTemplate>
                                    <asp:LinkButton ID="lnkSetFocus" runat="server" CommandName="SetFocus" SkinID="lnksetfocus"
                                        PostBackUrl="#"></asp:LinkButton>
                                </ContentTemplate>
                            </asp:RoleGroup>
                        </RoleGroups>
                    </asp:LoginView>
                    <asp:LoginView ID="LoginView3" runat="server">
                        <RoleGroups>
                            <asp:RoleGroup Roles="ProductModule-View">
                                <ContentTemplate>
                                    <asp:LinkButton ID="lnkViewPrice" runat="server" CommandName="ViewPrice" SkinID="lnkviewprice"
                                        PostBackUrl="#"></asp:LinkButton>
                                </ContentTemplate>
                            </asp:RoleGroup>
                        </RoleGroups>
                    </asp:LoginView>
                    <asp:LoginView ID="LoginView2" runat="server">
                        <RoleGroups>
                            <asp:RoleGroup Roles="ProductModule-Delete">
                                <ContentTemplate>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" SkinID="lnkdelete"
                                        OnClientClick="return confirm('确定要删除此记录吗？')" PostBackUrl="#"></asp:LinkButton>
                                </ContentTemplate>
                            </asp:RoleGroup>
                        </RoleGroups>
                    </asp:LoginView>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </hc:ExtensionGridView>
    </div>
</asp:Content>
