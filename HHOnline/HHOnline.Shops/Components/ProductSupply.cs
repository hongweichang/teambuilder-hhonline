using System;
using System.Collections.Generic;
using System.Text;
using HHOnline.Shops.Enums;
using HHOnline.Framework;

namespace HHOnline.Shops
{
	/// <summary>
	/// 产品供应表
	/// </summary>
	public class ProductSupply : ExtendedAttributes
	{
		/// <summary>
		/// 产品供应编号
		/// </summary>
		public int SupplyID { get; set; }
		/// <summary>
		/// 供应商编号
		/// </summary>
		public int SupplierID { get; set; }
		/// <summary>
		/// 产品编号
		/// </summary>
		public int ProductID { get; set; }
		/// <summary>
		/// 产品型号编号
		/// </summary>
		public int? ModelID { get; set; }
		/// <summary>
		/// 最短供货时间（时长格式）
		/// </summary>
		public string DeliverySpan { get; set; }
		/// <summary>
		/// 产品保修期（时长格式）
		/// </summary>
		public string WarrantySpan { get; set; }
		/// <summary>
		/// 供货单价
		/// </summary>
		public decimal? QuotePrice { get; set; }
		/// <summary>
		/// 最小订货量
		/// </summary>
		public int? QuoteMOQ { get; set; }
		/// <summary>
		/// 是否包含运费，1包含、2不包含
		/// </summary>
		public FreightIncludeType? IncludeFreight { get; set; }
		/// <summary>
		/// 是否包含税，1包含、2不包含
		/// </summary>
		public TaxIncludeType IncludeTax { get; set; }
		/// <summary>
		/// 供货税率（显示为百分数）
		/// </summary>
		public decimal ApplyTaxRate { get; set; }
		/// <summary>
		/// 供应区域（特定地区，空值表示全国）
		/// </summary>
		public int? SupplyRegion { get; set; }
		/// <summary>
		/// 报价起始日期
		/// </summary>
		public DateTime QuoteFrom { get; set; }
		/// <summary>
		/// 报价截止日期
		/// </summary>
		public DateTime QuoteEnd { get; set; }
		/// <summary>
		/// 报价自动续期周期（单位自然月、0表示不延期）
		/// </summary>
		public int QuoteRenewal { get; set; }
		/// <summary>
		/// 供应状态，1启用、2停用
		/// </summary>
		public ComponentStatus SupplyStatus { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime { get; set; }
		/// <summary>
		/// 创建操作人
		/// </summary>
		public int CreateUser { get; set; }
		/// <summary>
		/// 最后更新时间
		/// </summary>
		public DateTime UpdateTime { get; set; }
		/// <summary>
		/// 最后更新操作人
		/// </summary>
		public int UpdateUser { get; set; }

		public ProductSupply()
		{
			SupplyStatus = ComponentStatus.Enabled;
		}
	}
}
