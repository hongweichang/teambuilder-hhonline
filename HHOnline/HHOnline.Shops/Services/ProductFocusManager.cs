using System;
using System.Collections.Generic;
using HHOnline.Cache;
using HHOnline.Framework;
using HHOnline.Shops.Providers;

namespace HHOnline.Shops
{
    /// <summary>
    /// 关注类型管理
    /// </summary>
    public class ProductFocusManager
    {

        /// <summary>
        /// 创建关注类型
        /// </summary>
        /// <param name="focus"></param>
        public DataActionStatus Create(ProductFocus focus)
        {
            DataActionStatus status;
            focus = ShopDataProvider.Instance.CreateUpdateFocus(focus, DataProviderAction.Create, out status);
            return status;
        }

        /// <summary>
        /// 修改关注类型
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public DataActionStatus Update(ProductFocus focus)
        {
            DataActionStatus status;
            focus = ShopDataProvider.Instance.CreateUpdateFocus(focus, DataProviderAction.Update, out status);
            return status;
        }

        /// <summary>
        /// 删除关注类型
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public DataActionStatus Delete(int focusID)
        {
            DataActionStatus status = ShopDataProvider.Instance.DeleteFocus(focusID);
            return status;
        }

        /// <summary>
        /// 获取关注
        /// </summary>
        /// <param name="focusID"></param>
        /// <returns></returns>
        public ProductFocus Get(int focusID)
        {
            return ShopDataProvider.Instance.GetFocus(focusID);
        }

        /// <summary>
        /// 根据关注类型获取关注信息
        /// </summary>
        /// <param name="type">关注类型</param>
        /// <returns></returns>
        public List<ProductFocus> GetList(FocusType type)
        {
            return ShopDataProvider.Instance.GetFocusList(type);
        }
    }
}
