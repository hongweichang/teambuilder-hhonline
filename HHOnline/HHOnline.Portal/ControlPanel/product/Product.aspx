﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master"
    AutoEventWireup="true" CodeFile="Product.aspx.cs" Inherits="ControlPanel_Product_Product" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="ContentOpts" ContentPlaceHolderID="cphOpts" runat="Server">
    <asp:LinkButton ID="lbNewProduct" runat="server" SkinID="lnkopts">
        <span>新 增</span>
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
                <th>
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
                <td rowspan="2">
                    <asp:Button ID="lnkSearch" runat="server" Text="查找产品" OnClick="lnkSearch_Click" />
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
    <hc:extensiongridview runat="server" id="egvProducts" onrowdatabound="egvProducts_RowDataBound"
        onrowdeleting="egvProducts_RowDeleting" onrowupdating="egvProducts_RowUpdating"
        onpageindexchanging="egvProducts_PageIndexChanging" pagesize="5" skinid="DefaultView"
        autogeneratecolumns="False" datakeynames="ProductID">
        <Columns>
            <asp:TemplateField HeaderText="展示图片">
                <HeaderStyle Width="100" />
                <ItemTemplate>
                    <asp:Image ID="ProductPicture" style="border:double 3px #7d9edb;" Width="40" Height="40" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="产品名称">
                <ItemTemplate>
                    <asp:HyperLink ID="hlProductName" runat="server"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="产品品牌" DataField="BrandName" />
            <asp:BoundField HeaderText="介绍摘要" DataField="ProductAbstract" />
            <asp:TemplateField>
                <HeaderStyle Width="200" />
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
    </hc:extensiongridview>
</asp:Content>
