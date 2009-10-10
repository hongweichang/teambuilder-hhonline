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
	}
}
