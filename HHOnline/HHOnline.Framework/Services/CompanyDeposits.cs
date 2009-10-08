using System;
using System.IO;
using System.Collections.Generic;
using System.Web;
using HHOnline.Cache;
using HHOnline.Framework.Providers;

namespace HHOnline.Framework
{
    /// <summary>
    /// 公司资质文件服务类
    /// </summary>
    public class CompanyDeposits
    {
        public static CompanyDeposit DepositSelect(int companyID)
        {
            return CommonDataProvider.Instance.DepositSelect(companyID);
        }
        public static bool DepositSave(CompanyDeposit deposit)
        {
            return CommonDataProvider.Instance.DepositSave(deposit);
        }
    }
}
