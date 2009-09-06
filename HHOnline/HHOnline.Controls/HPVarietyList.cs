using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace HHOnline.Controls
{
    /// <summary>
    /// HomePage VarietyList
    /// </summary>
    public class HPVarietyList:UserControl
    {
        string RenderHTML()
        {
            StringBuilder sb = new StringBuilder();

            return sb.ToString();
        }
        public override void RenderControl(HtmlTextWriter writer)
        {
            if (this.Visible)
            {
                string html = RenderHTML();

                writer.Write(html);
                writer.WriteLine(Environment.NewLine);
            }
        }
    }
}
