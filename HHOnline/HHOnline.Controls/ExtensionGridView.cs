using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Security;
using System.Security.Permissions;
using System.Collections;
using System.Web.UI.Design;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

/**
 * *by Jericho
 * */
//[assembly: TagPrefix("Jericho.JControls.ExtensionGridView", "j")]
namespace HHOnline.Controls
{
    //分页控件的水平对齐方式
    public enum PagerAlignment { Left, Center, Right }

    [DefaultProperty("ID")]
    [ToolboxData("<{0}:ExtensionGridView runat=server></{0}:ExtensionGridView>")]
    [DefaultEvent("SelectedIndexChanged"), SupportsEventValidation, Designer("System.Web.UI.Design.WebControls.GridViewDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), ControlValueProperty("SelectedValue"), AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal), AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class ExtensionGridView : System.Web.UI.WebControls.GridView
    {
        #region -Public properties-

        /// <summary>
        /// 鼠标经过时颜色
        /// </summary>      
        [Category("扩展设置")]
        [TypeConverter(typeof(WebColorConverter))]
        [Description("鼠标经过时颜色")]
        [DefaultValue(typeof(Color), "#9900FF")]
        public Color MouseOverColor
        {
            get
            {
                if (ViewState["MouseOverColor"] == null)
                {
                    return ColorTranslator.FromHtml("#9900FF");
                }
                string htmlColor = (string)ViewState["MouseOverColor"];
                return ColorTranslator.FromHtml(htmlColor);
            }

            set
            {
                if (null != value)
                {
                    ViewState["MouseOverColor"] = ColorTranslator.ToHtml(value);
                }
            }
        }

        /// <summary>
        /// 鼠标经过时颜色
        /// </summary>      
        [Category("扩展设置")]
        [TypeConverter(typeof(WebColorConverter))]
        [Description("鼠标离开时颜色")]
        [DefaultValue(typeof(Color), "")]
        public Color MouseOutColor
        {
            get
            {
                if (ViewState["MouseOutColor"] == null)
                {
                    return Color.Empty;
                }
                string htmlColor = ViewState["MouseOutColor"] as string;
                return ColorTranslator.FromHtml(htmlColor);
            }

            set
            {
                if (null != value)
                {
                    ViewState["MouseOutColor"] = ColorTranslator.ToHtml(value);
                }
            }
        }

        /// <summary>
        /// 是否启用 HighlightColor
        /// </summary>
        [Category("扩展设置")]
        [Description("是否启用 MouseOverColor")]
        [DefaultValue(false)]
        public bool IsOpenMouseOverColor
        {
            get
            {
                if (ViewState["enableSelection"] == null)
                {
                    return false;
                }
                return (bool)ViewState["enableSelection"];
            }

            set
            {
                ViewState["enableSelection"] = value;
            }
        }

        /// <summary>
        /// 是否启用扩展
        /// </summary>
        [Category("扩展设置")]
        [Description("是否启用扩展")]
        [DefaultValue(true)]
        public bool ActivePagerBar
        {
            get
            {
                if (ViewState["enableExPager"] == null)
                {
                    return true;
                }
                return (bool)ViewState["enableExPager"];
            }

            set
            {
                ViewState["enableExPager"] = value;
            }
        }

        /// <summary>
        /// Get or Set Image location to be used to display Ascending Sort order.
        /// </summary>
        [
        Description("降序时显示的图片URL"),
        Category("扩展设置"),
        Editor(typeof(System.Web.UI.Design.UrlEditor), typeof(System.Drawing.Design.UITypeEditor)),
        DefaultValue("")
        ]
        public string SortDescImageUrl
        {
            get
            {
                string url = ViewState["SortImageDesc"] as string;
                return url;
            }
            set
            {
                ViewState["SortImageDesc"] = value;
            }
        }

        /// <summary>
        /// Get or Set Image location to be used to display Ascending Sort order.
        /// </summary>
        [
        Description("升序时显示的图片"),
        Category("扩展设置"),
        Editor(typeof(System.Web.UI.Design.UrlEditor), typeof(System.Drawing.Design.UITypeEditor)),
        DefaultValue("")
        ]
        public string SortAscImageUrl
        {
            get
            {
                string url = ViewState["SortImageAsc"] as string;
                return url;
            }
            set
            {
                ViewState["SortImageAsc"] = value;
            }
        }


        /// <summary>
        /// 是否显示升序降序图标
        /// </summary>
        [
        Description("是否显示升序降序图片URL"),
        Category("扩展设置"),
        DefaultValue(false)
        ]
        public bool IsShowSortDirectionImg
        {
            get
            {
                object o = ViewState["ShowSortDirection"];
                return (o != null ? Convert.ToBoolean(o) : false);
            }
            set
            {
                ViewState["ShowSortDirection"] = value;
            }
        }

        /// <summary>
        /// 分页信息.当前第{0}页共{1}页{2}条记录
        /// </summary>
        [Category("扩展设置")]
        [Description("分页信息")]
        [DefaultValue("当前{0}页,共{1}页 {2}条记录")]
        public string PagerText
        {
            get
            {
                if (ViewState["PagerText"] == null)
                {
                    return "第<font color='#0099CC'>{0}</font>页/共<font color='#0099CC'>{1}</font>页"+
                        "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;共<font color='#0099CC'>{2}</font>条记录" +
                        "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;每页<font color='#0099CC'>" + this.PageSize + "</font>条";
                }
                return (string)ViewState["PagerText"];
            }

            set
            {
                ViewState["PagerText"] = value;
            }
        }

        /// <summary>
        /// 分页信息显示在左边还是在右边
        /// </summary>
        [Category("扩展设置")]
        [Description("分页控件的水平对齐方式")]
        [DefaultValue(PagerAlignment.Left)]
        public PagerAlignment PagerAlign
        {
            get
            {
                if (ViewState["PagerAlign"] == null)
                {
                    return PagerAlignment.Left;
                }
                return (PagerAlignment)ViewState["PagerAlign"];
            }

            set
            {
                ViewState["PagerAlign"] = value;
            }
        }

        [Category("扩展设置")]
        [Description("启用自动排序")]
        [DefaultValue(false)]
        public bool AutoSort
        {
            get
            {
                if (ViewState["AutoSort"] == null)
                {
                    return false;
                }

                return (bool)ViewState["AutoSort"];
            }

            set
            {
                ViewState["AutoSort"] = value;
            }
        }

        [Category("扩展设置")]
        [Description("启用页跳转按钮")]
        [DefaultValue(true)]
        public bool EnablePageJump
        {
            get
            {
                if (ViewState["EnableJump"] == null)
                {
                    return true;
                }

                return (bool)ViewState["EnableJump"];
            }

            set
            {
                ViewState["EnableJump"] = value;
            }
        }

        /// <summary>
        /// 记录总数
        /// </summary>
        public int RecordCount
        {
            get
            {
                if (ViewState["rCount"] == null)
                {
                    return -1;//no set
                }

                return (int)ViewState["rCount"];
            }

            set
            {
                if (value >= 0)
                {
                    ViewState["rCount"] = value;
                }
            }
        }

        string pagerClassName = string.Empty;
        [Category("Appearance")]
        [Description("分页部分样式")]
        [DefaultValue("")]
        public string PagerClassName
        {
            get { return pagerClassName; }
            set { pagerClassName = value; }
        }
        #endregion

        #region Private methods-

        //Header中加入排序的图片
        private void AddSortImageToHeaderCell(TableCell headerCell)
        {
            // 查出headerCell中的 linkButton
            LinkButton lnk = (LinkButton)headerCell.Controls[0];
            if (lnk != null)
            {
                System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                // 设置图片的URL
                img.ImageUrl = (this.SortDirection == SortDirection.Ascending ? this.SortAscImageUrl : this.SortDescImageUrl);
                // 如果用户选择了排序,则加入排序图片
                if (this.SortExpression == lnk.CommandArgument)
                {
                    //加入一个空格
                    headerCell.Controls.Add(new LiteralControl(" "));
                    headerCell.Controls.Add(img);
                }
            }
        }

        #endregion

        #region -Pager-
        /// <summary>
        /// 初始化在分页功能启用时显示的页导航行。
        /// </summary>
        /// <param name="row">一个 GridViewRow，表示要初始化的页导航行。 </param>
        /// <param name="columnSpan">页导航行应跨越的列数</param>
        /// <param name="pagedDataSource">一个 PagedDataSource，表示数据源。 </param>
        protected override void InitializePager(GridViewRow row, int columnSpan, PagedDataSource pagedDataSource)
        {
            int rCount = (RecordCount > PageSize) ? RecordCount : pagedDataSource.DataSourceCount;

            int realPageCount = (rCount - 1) / PageSize + 1;
            int idx = (null != ViewState["RealPageIndex"]) ? ((int)ViewState["RealPageIndex"]) : pagedDataSource.CurrentPageIndex;

            string pagerText = this.PagerText.Replace("{0}", (idx + 1).ToString()).Replace
                ("{1}", realPageCount.ToString()).Replace("{2}", rCount.ToString()).Replace("{3}", this.Rows.Count.ToString());

            if (this.ActivePagerBar)
            {
                PagerInfo pagerinfo = new PagerInfo();
                pagerinfo.RecordCount = rCount;
                pagerinfo.pageCount = realPageCount;
                pagerinfo.CurrentPageIndex = idx;
                pagerinfo.align = PagerAlign;
                pagerinfo.EnablePagerJump = EnablePageJump;
                pagerinfo.PageSize = PageSize;
                pagerinfo.PagerText = pagerText;
                pagerinfo.PagerSetting = PagerSettings;
                pagerinfo.cssName = pagerClassName;
                if (pagerinfo.PagerSetting != null)
                {
                    base.PagerTemplate = new ExtensionPagerTemplate(this, pagerinfo);
                }
            }
            base.InitializePager(row, columnSpan, pagedDataSource);
        }
        /// <summary>
        /// 过滤字符串中敏感字段
        /// </summary>
        /// <param name="filterString">待过滤字符串</param>
        /// <returns></returns>
        private string StringFilter(string filterString)
        {
            filterString = filterString.Replace("<", "&lt;");
            filterString = filterString.Replace(">", "&gt;");
            filterString = filterString.Replace(" ", "&nbsp;");
            filterString = filterString.Replace("\n", "<br />");
            return filterString;
        }
        #endregion

        #region -Override methods-

        protected override void OnDataBinding(EventArgs e)
        {
            int tempPIndex = this.PageIndex;
            if (RecordCount > PageSize)
            {
                ViewState["RealPageIndex"] = this.PageIndex;
                this.PageIndex = 0;
            }
            base.OnDataBinding(e);
        }

        protected override void OnDataBound(EventArgs e)
        {
            if (this.AllowPaging && RecordCount > PageSize)
            {
                switch (PagerSettings.Position)
                {
                    case PagerPosition.Bottom:
                        BottomPagerRow.Visible = true;
                        break;
                    case PagerPosition.Top:
                        TopPagerRow.Visible = true;
                        break;
                    case PagerPosition.TopAndBottom:
                        TopPagerRow.Visible = true;
                        BottomPagerRow.Visible = true;
                        break;
                }
            }
            base.OnDataBound(e);
        }

        protected override void OnRowCreated(GridViewRowEventArgs e)
        {
            base.OnRowCreated(e);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (IsOpenMouseOverColor)
                {
                    string mOverColor = (Color.Empty == this.MouseOverColor) ? "#ffffff" : ColorTranslator.ToHtml(this.MouseOverColor);
                    string mOutColor = (Color.Empty == this.MouseOutColor) ? "#ffffff" : ColorTranslator.ToHtml(this.MouseOutColor);
                    e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor = '" + mOverColor + "';");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor = '" + mOutColor + "';");
                }
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                foreach (TableCell headerCell in e.Row.Cells)
                {
                    if (IsShowSortDirectionImg && headerCell.HasControls())
                    {
                        AddSortImageToHeaderCell(headerCell);
                    }
                }
            }
        }

        protected override void OnRowDataBound(GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextEllipsisProvider provider = new TextEllipsisProvider();
                for (int i = 0; i < this.Columns.Count; i++)
                {
                    BoundField col = this.Columns[i] as BoundField;
                    if (null != col && !string.IsNullOrEmpty(col.DataFormatString))
                    {
                        string text = e.Row.Cells[i].Text;
                        e.Row.Cells[i].ToolTip = text;
                        e.Row.Cells[i].Text = string.Format(provider, col.DataFormatString, text);
                    }
                    //e.Row.Cells[i].Text = StringFilter(e.Row.Cells[i].Text);
                    //if (this.Columns[i] is BoundField)
                    //{
                    //    e.Row.Cells[i].Text = StringFilter(e.Row.Cells[i].Text);
                    //}
                }
            }
            base.OnRowDataBound(e);
        }

        //pageIndexChanging
        protected override void OnPageIndexChanging(GridViewPageEventArgs e)
        {
            base.OnPageIndexChanging(e);
        }

        #endregion

    }

    #region -Pager Informations-
    public struct PagerInfo
    {
        #region Public fields
        public string cssName;
        //分页控件的水平对齐方式
        public PagerAlignment align;
        //分页控件的提示信息
        public string PagerText;
        //记录总数
        public int RecordCount;
        //页总数
        public int pageCount;
        //每页显示的记录数
        public int PageSize;
        //当前页
        public int CurrentPageIndex;
        //启用页跳转
        public bool EnablePagerJump;
        //支持分页的控件中的分页控件的属性
        public PagerSettings PagerSetting;

        #endregion
    }
    #endregion
}
