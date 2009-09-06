using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HHOnline.Controls
{
    /// <summary>
    /// 时长控件
    /// </summary>
    public class DateSpan : WebControl, INamingContainer
    {
        public override ControlCollection Controls
        {
            get
            {
                EnsureChildControls();
                return base.Controls;
            }
        }

        /// <summary>
        ///时长值
        /// </summary>
        public string DateSpanValue
        {
            get
            {
                EnsureChildControls();
                string value = "";
                if (ddlYear.SelectedIndex > 0)
                {
                    value += ddlYear.Text + "Y";
                }
                if (ddlMonth.SelectedIndex > 0)
                {
                    if (value.Trim() != string.Empty)
                        value += "-";
                    value += ddlMonth.Text + "M";
                }
                if (ddlDay.SelectedIndex > 0)
                {
                    if (value.Trim() != string.Empty)
                        value += "-";
                    value += ddlDay.Text + "D";
                }
                return value;
            }
            set
            {
                EnsureChildControls();
                if (string.IsNullOrEmpty(value))
                {
                    ddlYear.SelectedIndex = 0;
                    ddlMonth.SelectedIndex = 0;
                    ddlDay.SelectedIndex = 0;
                }
                else
                {
                    string[] spans = value.Split('-');
                    foreach (string span in spans)
                    {
                        if (span.Contains("Y"))
                        {
                            ddlYear.Text = span.Replace("Y", "").Trim();
                        }
                        else if (span.Contains("M"))
                        {
                            ddlMonth.Text = span.Replace("M", "").Trim();
                        }
                        if (span.Contains("D"))
                        {
                            ddlDay.Text = span.Replace("D", "").Trim();
                        }
                    }
                }
            }
        }

        private DropDownList ddlYear = null;

        private DropDownList ddlMonth = null;

        private DropDownList ddlDay = null;

        protected override void CreateChildControls()
        {
            Controls.Clear();

            ddlYear = new DropDownList();
            ddlYear.ID = "ddlYear";
            //ddlYear.Items.Add("   ");
            for (int i = 0; i <= 20; i++)
            {
                ddlYear.Items.Add(i.ToString());
            }

            ddlMonth = new DropDownList();
            ddlMonth.ID = "ddlMonth";
            //ddlMonth.Items.Add("   ");
            for (int i = 0; i <= 12; i++)
            {
                ddlMonth.Items.Add(i.ToString());
            }

            ddlDay = new DropDownList();
            ddlDay.ID = "ddlDay";
            //ddlDay.Items.Add("   ");
            for (int i = 0; i <= 30; i++)
            {
                ddlDay.Items.Add(i.ToString());
            }

            this.Controls.Add(ddlYear);
            this.Controls.Add(ddlMonth);
            this.Controls.Add(ddlDay);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            ddlYear.RenderControl(writer);
            writer.Write("年");
            ddlMonth.RenderControl(writer);
            writer.Write("月");
            ddlDay.RenderControl(writer);
            writer.Write("日");
        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Span;
            }
        }
    }
}
