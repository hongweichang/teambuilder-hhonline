using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using HHOnline.Shops;
using HHOnline.Framework;

namespace HHOnline.Controls
{
    public class CompanyList:UserControl
    {
        public CompanyList()
        {
            cs = Companys.GetCompanys(CompanyStatus.ApprovalPending, CompanyType.None, string.Empty);
        }
        private static List<Company> cs = null;
        private int _ItemCount = 10;
        private string _format = "<li><div class=\"companyList_col1\" title=\"{0}\">{1}</div><div class=\"companyList_col2\">{2}</div></li>";
        public int ItemCount
        {
            get { return _ItemCount; }
            set { _ItemCount = value; }
        }
        public string RenderHTML()
        {
            string nav = GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?users-corps&des=authpre";
           
            StringBuilder sb = new StringBuilder();
            int count = cs.Count;
            if (count == 0)
            {
                sb.Append("<div class=\"empty_cl\">暂无待审核企业信息！</div>");
            }
            else
            {
                string _url=string.Empty;
                int min = Math.Min(count, _ItemCount);
                sb.Append("<ul>");
                for (int i = 0; i < min; i++)
                {
                    _url=cs[i].Website;
                    sb.AppendFormat(_format,
                            GlobalSettings.SubString(cs[i].CompanyName, 20),
                            cs[i].CompanyName,
                            !string.IsNullOrEmpty(_url) ? "<a href=\"" + _url + "\" target=\"_blank\">"+GlobalSettings.SubString(_url,20)+"</a>" : "--"
                            );
                }
                sb.Append("</ul>");
                if (min < count)
                {
                    sb.Append("<a class=\"clExec\" onclick=\"resolvePendingCompany('"+nav+"')\" href=\"#\" >还有<span>" + (count - min) + "</span>条待审核企业信息，马上处理！</a>");
                }
                else
                {
                    sb.Append("<a class=\"clExec\" onclick=\"resolvePendingCompany('" + nav + "')\" href=\"#\">马上处理此<span>" + count + "</span>条待审核企业信息！</a>");
                }
            }
            return sb.ToString();
        }


        public override void RenderControl(HtmlTextWriter writer)
        {
            string html = RenderHTML();

            writer.Write(html);
            writer.WriteLine(Environment.NewLine);
        }
    }
}
