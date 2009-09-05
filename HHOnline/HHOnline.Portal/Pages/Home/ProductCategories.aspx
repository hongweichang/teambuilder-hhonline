<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductCategories.aspx.cs" Inherits="Pages_Home_ProductCategories" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:650px;padding:2px;">
        <asp:DataList ID="dlProductCategories" OnItemDataBound="dlProductCategories_ItemDataBound" DataKeyField="CategoryID" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table" Width="100%"> 
            <ItemTemplate>
                <div><%# Eval("CategoryName") %></div>
                <asp:DataList ID="dlSubProductCategories" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <ItemTemplate>
                        <a href='pages/product-category&ID=<%# Eval("CategoryID") %>' ><%# Eval("CategoryName") %></a>
                    </ItemTemplate>                    
                </asp:DataList>
            </ItemTemplate>
        </asp:DataList>
    </div>
    </form>
</body>
</html>
