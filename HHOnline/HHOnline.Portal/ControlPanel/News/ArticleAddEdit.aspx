<%@ Page Title="新增文章" Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master"
    AutoEventWireup="true" CodeFile="ArticleAddEdit.aspx.cs" Inherits="ControlPanel_News_ArticleAddEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphOpts" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <table cellpadding="10" cellspacing="10" class="postform">
        <tr>
            <th style="width: 100px">
                所属分类
            </th>
            <td>
                <huc:ArticleCategoryCombo ID="ascCategory" runat="server" />
            </td>
        </tr>
        <tr>
            <th>
                标题(<span style="color: #ff0000">必填</span>)
            </th>
            <td>
                <asp:TextBox Width="230px" ID="txtTitle" runat="server" MaxLength="20"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv1" runat="server" Display="Dynamic" ControlToValidate="txtTitle"
                    ErrorMessage="必须填写！"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                子标题(<span style="color: #4682B4">可选</span>)
            </th>
            <td>
                <asp:TextBox Width="230px" ID="txtSubTitle" runat="server" Text="" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                发布日期(<span style="color: #4682B4">可选</span>)
            </th>
            <td>
                <asp:TextBox ID="txtDate" rel="datepicker" runat="server"></asp:TextBox>&nbsp;&nbsp;(未填写将使用当前日期！)
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDate"
                    ValidationExpression="\d{4}年\d{1,2}月\d{1,2}日" ErrorMessage="必须为日期格式 - yyyy年MM月dd日！"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <th>
                作者(<span style="color: #4682B4">可选</span>)
            </th>
            <td>
                <asp:TextBox Width="500" ID="txtAuthor" runat="server" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                来源(<span style="color: #4682B4">可选</span>)
            </th>
            <td>
                <asp:TextBox Width="500" ID="txtCopyFrom" runat="server" MaxLength="200"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                关键字（多个关键字用分号分隔）(<span style="color: #4682B4">可选</span>)
            </th>
            <td>
                <asp:TextBox Width="500" ID="txtKeywords" runat="server" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                标题图像(<span style="color: #4682B4">可选</span>)
            </th>
            <td>
				<asp:DropDownList ID="ddlArticleImages" runat="server">
				</asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th>
                摘要(<span style="color: #4682B4">可选</span>)
            </th>
            <td>
                <asp:TextBox ID="txtAbstract" Width="500" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                内容(<span style="color: #4682B4">可选</span>)
            </th>
            <td>
                <hc:Editor ID="txtContent" EditorMode="Enhanced" runat="server"></hc:Editor>
            </td>
        </tr>
        <tr>
            <th>
                显示顺序(<span style="color: #ff0000">必填</span>)
            </th>
            <td>
                <asp:TextBox Width="230px" Text="0" ID="txtDisplayOrder" runat="server" MaxLength="50"></asp:TextBox>
                <asp:RegularExpressionValidator ID="rev1" runat="server" ControlToValidate="txtDisplayOrder"
                    ValidationExpression="(\d){1,3}" ErrorMessage="必须为0-999的数字"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="rfv3" Display="Dynamic" runat="server" ControlToValidate="txtDisplayOrder"
                    ErrorMessage="必须填写！"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                是否启用(<span style="color: #ff0000">必填</span>)
            </th>
            <td>
                <hc:ComponentStatusList ID="csArticle" runat="server" />
            </td>
        </tr>
        <tr>
            <th>
                备注(<span style="color: #4682B4">可选</span>)
            </th>
            <td>
                <asp:TextBox Width="500" ID="txtMemo" runat="server" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td style="text-align: left; height: 20px;">
                <hc:MsgBox ID="mbMsg" runat="server" SkinID="msgBox"></hc:MsgBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Button ID="btnPost" runat="server" Text=" 增 加 " OnClick="btnPost_Click" PostBackUrl="#">
                </asp:Button>
                <asp:Button ID="btnClose" runat="server" Text=" 返 回 " CausesValidation="false" OnClientClick="return CloseEdit();">
                </asp:Button>
            </td>
        </tr>
    </table>
</asp:Content>
