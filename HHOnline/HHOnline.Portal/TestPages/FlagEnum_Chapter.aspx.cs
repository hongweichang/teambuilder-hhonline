using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework;

public partial class TestPages_FlagEnum_Chapter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CompanyType c = (CompanyType)5;
        Response.Write(c.ToString());
    }
}
