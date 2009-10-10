using System;
using System.Collections.Generic;
using System.Text;
using HHOnline.Framework;
using HHOnline.Shops.Providers;

namespace HHOnline.Shops
{
	public class ProductSupplyManager
	{
		/// <summary>
		/// 更新产品供应信息
		/// </summary>
		/// <param name="ps"></param>
		/// <returns></returns>
		public static DataActionStatus Update(ProductSupply ps)
		{
			DataActionStatus status;
			ShopDataProvider.Instance.UpdateProductSupply(ps, out status);

			return status;
		}

		/// <summary>
		/// 根据产品供应信息ID获取产品供应信息
		/// </summary>
		/// <param name="psID"></param>
		/// <returns></returns>
		public static ProductSupply GetProductSupply(int psID)
		{
			return ShopDataProvider.Instance.GetProductSupply(psID);
		}

		/// <summary>
		/// 根据产品ID和公司ID查找产品供应信息
		/// </summary>
		/// <param name="productID"></param>
		/// <param name="supplierID"></param>
		/// <returns></returns>
		public static ProductSupply GetProductSupply(int productID, int supplierID)
		{
			return ShopDataProvider.Instance.GetProductSupply(productID, supplierID);
		}
	}
}
