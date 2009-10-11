using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using HHOnline.Framework.Web;
using HHOnline.Framework;
using HHOnline.Shops;
using Image = System.Web.UI.WebControls.Image;
using HHOnline.Shops.Enums;

public partial class Pages_Profiles_ProductEditSupplier : HHPage
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			CheckPermission();
			BindData();
		}
	}

	private void CheckPermission()
	{
		User u = Profile.AccountInfo;
		if (u.UserType != UserType.CompanyUser ||
			(u.Company.CompanyType == CompanyType.Ordinary ||
			(u.Company.CompanyType == (CompanyType.Ordinary | CompanyType.Agent)))
			|| u.IsManager != 1)
		{
			throw new HHException(ExceptionType.ModuleInitFail, "没有相应的权限！");
		}
	}

	public override void OnPageLoaded()
	{
		ShortTitle = "供应信息";
		SetTitle();
		SetTabName(ShortTitle);

		//AddJavaScriptInclude("scripts/jquery.cookie.js", false, false);
		//base.ExecuteJs("$.fn.cookie({ action: 'set', name: 'hhonline_menu', value: 'item_productmanage' });", false);
		AddJavaScriptInclude("scripts/jquery.datepick.js", false, false);
		AddJavaScriptInclude("scripts/pages/producteditsupplier.aspx.js", false, false);
	}

	/// <summary>
	/// 绑定信息
	/// </summary>
	public void BindData()
	{
		int productID = Convert.ToInt32(Request.QueryString["ProductID"]);
		Product product = Products.GetProduct(productID);

		if (product == null)
		{
			throw new HHException(ExceptionType.ProductNotFound);
		}
		else
		{
			// 根据产品ID和公司ID查找产品供应信息
			ProductSupply ps = ProductSupplyManager.GetProductSupply(product.ProductID, Profile.AccountInfo.CompanyID);

			if (ps == null)
			{
				throw new HHException(ExceptionType.ProductSupplyNotFound);
			}
			else
			{
				// 产品名称
				lblProductName.Text = product.ProductName;

				// TODO: 产品型号

				// 最短供货时间
				txtDeliverySpan.Text = ps.DeliverySpan;

				// 产品保修期
				txtWarrantySpan.Text = ps.WarrantySpan;

				// 供货单价
				txtQuotePrice.Text = ps.QuotePrice.HasValue ? ps.QuotePrice.Value.ToString() : string.Empty;

				// 最小订货量
				txtQuoteMOQ.Text = ps.QuoteMOQ.HasValue ? ps.QuoteMOQ.Value.ToString() : string.Empty;

				// 包含运费
				ilIncludeFreight.SelectedValue = ps.IncludeFreight.HasValue ? (PriceIncludeType)ps.IncludeFreight : PriceIncludeType.Include;

				// 包含税
				ilIncludeTax.SelectedValue = (PriceIncludeType)ps.IncludeTax;

				// 供货税率
				txtApplyTaxRate.Text = ps.ApplyTaxRate.ToString();

				// 供应区域
				if (ps.SupplyRegion.HasValue)
				{
					hfRegionCode.Value = ps.SupplyRegion.Value.ToString();

					try
					{
						Area a = Areas.GetArea(ps.SupplyRegion.Value);
						txtRegion.Text = a.RegionName;
					}
					catch { }
				}

				// 报价起始日期
				if (ps.QuoteFrom == DateTime.MinValue)
				{
					txtQuoteFrom.Text = string.Empty;
				}
				else
				{
					txtQuoteFrom.Text = ps.QuoteFrom.ToString("yyyy年MM月dd日");
				}

				// 报价截止日期
				if (ps.QuoteEnd == DateTime.MinValue)
				{
					txtQuoteEnd.Text = string.Empty;
				}
				else
				{
					txtQuoteEnd.Text = ps.QuoteEnd.ToString("yyyy年MM月dd日");
				}

				// 报价自动续期周期
				txtQuoteRenewal.Text = ps.QuoteRenewal.ToString();

				// 是否启用
				csSupplyStatus.SelectedValue = ps.SupplyStatus;
			}
		}
	}

	protected void btnSave_Click(object sender, EventArgs e)
	{
		int productID = Convert.ToInt32(Request.QueryString["ProductID"]);
		Product product = Products.GetProduct(productID);

		if (product == null)
		{
			throw new HHException(ExceptionType.ProductNotFound);
		}
		else
		{
			// 根据产品ID和公司ID查找产品供应信息
			ProductSupply ps = ProductSupplyManager.GetProductSupply(product.ProductID, Profile.AccountInfo.CompanyID);

			if (ps == null)
			{
				throw new HHException(ExceptionType.ProductSupplyNotFound);
			}
			else
			{
				// 产品名称
				product.ProductName = lblProductName.Text;

				// TODO: 产品型号

				// 最短供货时间
				ps.DeliverySpan = txtDeliverySpan.Text;

				// 产品保修期
				ps.WarrantySpan = txtWarrantySpan.Text;

				// 供货单价
				if (string.IsNullOrEmpty(txtQuotePrice.Text))
				{
					ps.QuotePrice = null;
				}
				else
				{
					ps.QuotePrice = Convert.ToDecimal(txtQuotePrice.Text);
				}

				// 最小订货量
				if (string.IsNullOrEmpty(txtQuoteMOQ.Text))
				{
					ps.QuoteMOQ = null;
				}
				else
				{
					ps.QuoteMOQ = Convert.ToInt32(txtQuoteMOQ.Text);
				}

				// 包含运费
				ps.IncludeFreight = (FreightIncludeType)ilIncludeFreight.SelectedValue;

				// 包含税
				ps.IncludeTax = (TaxIncludeType)ilIncludeTax.SelectedValue;

				// 供货税率
				ps.ApplyTaxRate = Convert.ToDecimal(txtApplyTaxRate.Text);

				// 供应区域
				int regionID = Convert.ToInt32(hfRegionCode.Value);
				if (regionID <= 0)
				{
					ps.SupplyRegion = null;
				}
				else
				{
					ps.SupplyRegion = regionID;
				}

				// 报价起始日期
				ps.QuoteFrom = DateTime.Parse(txtQuoteFrom.Text);

				// 报价截止日期
				ps.QuoteEnd = DateTime.Parse(txtQuoteEnd.Text);

				// 报价自动续期周期
				ps.QuoteRenewal = Convert.ToInt32(txtQuoteRenewal.Text);

				// 是否启用
				ps.SupplyStatus = csSupplyStatus.SelectedValue;

				// 开始更新
				DataActionStatus state = ProductSupplyManager.Update(ps);

				if (state == DataActionStatus.Success)
					throw new HHException(ExceptionType.Success, "修改成功！");
				else
					throw new HHException(ExceptionType.Failed, "修改失败，请确认信息是否正确！");
			}
		}
	}
}
