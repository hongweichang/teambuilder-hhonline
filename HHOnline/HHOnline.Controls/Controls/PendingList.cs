using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using HHOnline.Shops;
using HHOnline.Framework;

namespace HHOnline.Controls
{
    public class PendingList:Control
    {
        static PendingList()
        {
            Pendings.Updated += delegate { _Html = null; };
        }
        private List<Pending> cs = null;
        private int _ItemCount = 10;
        private string _format = "<li><div class=\"companyList_col1\" title=\"{0}\">{1}</div><div class=\"companyList_col2\">{2}</div></li>";
        public int ItemCount
        {
            get { return _ItemCount; }
            set { _ItemCount = value; }
        }
        public static object _lock = new object();
        private static string _Html;
        public string HTML
        {
            get
            {
                if (string.IsNullOrEmpty(_Html))
                {
                    lock (_lock)
                    {
                        if (string.IsNullOrEmpty(_Html))
                        {
                            _Html = RenderHTML();
                        }
                    }
                }
                return _Html;
            }
        }
        public string RenderHTML()
        {
            cs = Pendings.PendingsLoad();
            string nav = GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?users-pendinglist";
           
            StringBuilder sb = new StringBuilder();
            int count = cs.Count;
            if (count == 0)
            {
                sb.Append("<div class=\"empty_cl\">暂无待审核企业类型变更申请！</div>");
            }
            else
            {
                int min = Math.Min(count, _ItemCount);
                sb.Append("<ul>");
                string comName = string.Empty;
                for (int i = 0; i < min; i++)
                {
                    comName = GetCompanyName(cs[i].CompanyID);

                    sb.AppendFormat(_format,
                            comName,
                            GlobalSettings.SubString(comName, 20),
                            GetCompanyType(cs[i].CompanyType)
                            );
                }
                sb.Append("</ul>");
                if (min < count)
                {
                    sb.Append("<a class=\"clExec\" onclick=\"resolvePending('"+nav+"')\" href=\"#\" >还有<span>" + (count - min) + "</span>条待审核企业类型变更信息，马上处理！</a>");
                }
                else
                {
                    sb.Append("<a class=\"clExec\" onclick=\"resolvePending('" + nav + "')\" href=\"#\">马上处理此<span>" + count + "</span>条待审核企业类型变更信息！</a>");
                }
            }
            return sb.ToString();
        }
        string GetCompanyName(int comId)
        {
            return Companys.GetCompany(comId).CompanyName;
        }
        string GetCompanyType(CompanyType ct)
        {
            string r = string.Empty;
            switch (ct)
            {
                case CompanyType.Ordinary:
                    r = "客户";
                    break;
                case CompanyType.Agent:
                    r = "代理商";
                    break;
                case CompanyType.Provider:
                    r = "供应商";
                    break;
                case CompanyType.Agent|CompanyType.Ordinary:
                    r = "客户&&代理商";
                    break;
                case CompanyType.Provider | CompanyType.Ordinary:
                    r = "客户&&供应商";
                    break;
            }
            return r;
        }
        public override void RenderControl(HtmlTextWriter writer)
        {
            writer.Write(HTML);
            writer.WriteLine(Environment.NewLine);
        }
    }
}
