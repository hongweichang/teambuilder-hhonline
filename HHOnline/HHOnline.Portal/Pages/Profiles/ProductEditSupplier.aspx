<%@ Page Language="C#" MasterPageFile="~/Masters/AdminMasterPage.master" AutoEventWireup="true"
	CodeFile="ProductEditSupplier.aspx.cs" Inherits="Pages_Profiles_ProductEditSupplier"
	Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphOpts" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="Server">
	<table class="postform" cellspacing="10" cellpadding="10">
		<tr>
			<th style="width: 180px;">
				供应商
			</th>
			<td>
				<asp:Label ID="lblSupplierName" runat="server" Text="Label"></asp:Label>
			</td>
		</tr>
		<tr>
			<th style="width: 180px;">
				产品名称
			</th>
			<td>
				<asp:Label ID="lblProductName" runat="server" Text="Label"></asp:Label>
			</td>
		</tr>
		<tr>
			<th style="width: 180px;">
				产品型号(<span class="unneeded">可选</span>)
			</th>
			<td>
				<asp:DropDownList ID="ddlModel" runat="server">
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<th style="width: 180px;">
				最短供货时间（时长格式）(<span class="unneeded">可选</span>)
			</th>
			<td>
				<asp:TextBox Width="230px" ID="txtDeliverySpan" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<th style="width: 180px;">
				产品保修期（时长格式）(<span class="unneeded">可选</span>)
			</th>
			<td>
				<asp:TextBox Width="230px" ID="txtWarrantySpan" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<th style="width: 180px;">
				供货单价(<span class="unneeded">可选</span>)
			</th>
			<td>
				<asp:TextBox Width="230px" ID="txtQuotePrice" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<th style="width: 180px;">
				最小订货量(<span class="unneeded">可选</span>)
			</th>
			<td>
				<asp:TextBox Width="230px" ID="txtQuoteMOQ" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<th style="width: 180px;">
				包含运费(<span class="unneeded">可选</span>)
			</th>
			<td>
				<hc:IncludeTypeList runat="server" ID="ilIncludeFreight">
				</hc:IncludeTypeList>
			</td>
		</tr>
		<tr>
			<th style="width: 180px;">
				包含税(<span class="unneeded">可选</span>)
			</th>
			<td>
				<hc:IncludeTypeList runat="server" ID="ilIncludeTax">
				</hc:IncludeTypeList>
			</td>
		</tr>
		<tr>
			<th style="width: 180px;">
				供货税率%(<span class="needed">必填</span>)
			</th>
			<td>
				<asp:TextBox Width="230px" ID="txtApplyTaxRate" runat="server"></asp:TextBox>
				<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtApplyTaxRate"
					ErrorMessage="必须填写！" Display="Dynamic"></asp:RequiredFieldValidator>
			</td>
		</tr>
		<tr>
			<th style="width: 180px;">
				供应区域(<span class="unneeded">可选</span>)
			</th>
			<td>
				<asp:TextBox Width="230px" ID="txtRegion" runat="server" ReadOnly="true"></asp:TextBox>
				<img id="showArea" src="../images/default/choosearea.gif" alt="选择区域" title="选择区域"
					style="cursor: pointer" />
				<asp:HiddenField ID="hfRegionCode" runat="server" />
				<a id="clearArea" href="javascript:{}">清空</a>
			</td>
		</tr>
		<tr>
			<th style="width: 180px;">
				报价起始日期(<span class="needed">必填</span>)
			</th>
			<td>
				<asp:TextBox Width="230px" ID="txtQuoteFrom" rel="quotefromdate" runat="server"></asp:TextBox>
				<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtQuoteFrom"
					ErrorMessage="必须填写！" Display="Dynamic"></asp:RequiredFieldValidator>
			</td>
		</tr>
		<tr>
			<th style="width: 180px;">
				报价截止日期(<span class="needed">必填</span>)
			</th>
			<td>
				<asp:TextBox Width="230px" ID="txtQuoteEnd" rel="quoteenddate" runat="server"></asp:TextBox>
				<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtQuoteEnd"
					ErrorMessage="必须填写！" Display="Dynamic"></asp:RequiredFieldValidator>
			</td>
		</tr>
		<tr>
			<th style="width: 180px;">
				报价自动续期周期(<span class="needed">必填</span>)
			</th>
			<td>
				<asp:TextBox Width="230px" ID="txtQuoteRenewal" runat="server"></asp:TextBox>
				<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtQuoteRenewal"
					ErrorMessage="必须填写！" Display="Dynamic"></asp:RequiredFieldValidator>
			</td>
		</tr>
		<tr>
			<th style="width: 180px;">
				是否启用(<span class="needed">必填</span>)
			</th>
			<td>
				<hc:ComponentStatusList ID="csSupplyStatus" runat="server" />
			</td>
		</tr>
		<tr>
			<th>
				&nbsp;
			</th>
			<td>
				&nbsp;
			</td>
		</tr>
		<tr>
			<th>
				&nbsp;
			</th>
			<td>
				<asp:Button ID="btnSave" runat="server" Text=" 保存 " />
			</td>
		</tr>
	</table>
	<div id="regionViewer"></div>
</asp:Content>
