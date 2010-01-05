using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using HHOnline.Framework;

public partial class Pages_Common_SiteMap : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialDataBind();
        }
    }

    private void InitialDataBind()
    {
        StringBuilder sbItems = null;

        sbItems = new StringBuilder();
        //get all brand's ID and Name from Database
        for (int i = 0; i < 1; i++)
        {
            sbItems.AppendFormat("<li><a href=\"http://www.ehuaho.com/pages/view.aspx?product-{0}&ID={1}\" target=\"_blank\" title=\"{2}\">{3}</a></li>",
                "brand",
                GlobalSettings.Encrypt("[ID]".ToString()),
                "[NAME]",
                GlobalSettings.SubString("[NAME]", 10));
        }
        ltBrand.Text = sbItems.ToString();
        //ltCategory; ltIndustry; ltNews; ltProduct;

        throw new NotImplementedException();
    }
}
