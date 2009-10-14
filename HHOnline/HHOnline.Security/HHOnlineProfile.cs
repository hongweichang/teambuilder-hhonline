using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Profile;
using HHOnline.Framework;
using HHOnline.Shops;

namespace HHOnline.Security
{
    public class HHOnlineProfile : ProfileBase
    {
        public HHOnlineProfile() { }
        public virtual HHOnlineProfile GetProfile(string username)
        {
            return (HHOnlineProfile)ProfileBase.Create(username);
        }

        // Properties
        /// <summary>
        /// 用户侧信息
        /// </summary>
        [SettingsAllowAnonymous(true)]
        [ProfileProvider("HHProfileProvider")]
        public virtual User AccountInfo
        {
            get
            {
                return (User)base.GetPropertyValue("AccountInfo");
            }
            set
            {
                base.SetPropertyValue("AccountInfo", value);
            }

        }
        /// <summary>
        /// 购物车信息
        /// </summary>
        [SettingsAllowAnonymous(true)]
        [ProfileProvider("HHProfileProvider")]
        public virtual ShoppingCart ShoppingCart
        {
            get
            {
                return (ShoppingCart)base.GetPropertyValue("ShoppingCart");
            }
            set
            {
                base.SetPropertyValue("ShoppingCart", value);
            }

        }

    }
}
