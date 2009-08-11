using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework;
using HHOnline.Framework.Web;
using HHOnline.Framework.Web.Enums;



public partial class Utility_SelectTags : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<Tag> tags = Tags.GetTags();

            rpTag.DataSource = tags;
            rpTag.DataBind();
        }
    }
}
