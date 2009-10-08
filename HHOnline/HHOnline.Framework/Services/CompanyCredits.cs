using System;
using System.IO;
using System.Collections.Generic;
using System.Web;
using HHOnline.Cache;
using HHOnline.Framework.Providers;

namespace HHOnline.Framework
{
    /// <summary>
    /// 公司信用
    /// </summary>
    public class CompanyCredits
    {
        public static CompanyCredit CreditSelect(int companyID)
        {
            return CommonDataProvider.Instance.CreditSelect(companyID);
        }
        public static bool CreditSave(CompanyCredit credit)
        {
            return CommonDataProvider.Instance.CreditSave(credit);
        }
    }
}
