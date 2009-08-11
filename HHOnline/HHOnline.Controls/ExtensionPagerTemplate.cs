using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

/**
 * *by Jericho
 * */
namespace HHOnline.Controls
{
    /// <summary>
    /// 自定义分页模板
    /// </summary>
    public class ExtensionPagerTemplate : ITemplate
    {
        #region -Private Fields-

        private PagerInfo _pagerInfo;
        private WebControl control;
        private string jumpText = string.Empty;

        #endregion

        #region -Constructor-

        public ExtensionPagerTemplate(WebControl ctrl, PagerInfo pagerInfo)
        {
            control = ctrl;
            _pagerInfo = pagerInfo;
        }

        #endregion

        #region -ITemplate Members-

        public void InstantiateIn(Control container)
        {
            Table table = new Table();
            table.Width = Unit.Parse("100%");

            TableRow pagerRow = new TableRow();

            TableCell cell = new TableCell();
            cell.Attributes.Add("align", _pagerInfo.align.ToString());

            switch (_pagerInfo.PagerSetting.Mode)
            {
                case PagerButtons.NextPreviousFirstLast :
                    CreateNextPreviousFirstLastTemplate(cell);
                    break;
                case PagerButtons.NextPrevious :
                    CreateNextPreviousTemplate(cell);
                    break;
                case PagerButtons.NumericFirstLast :
                    CreateNumericFirstLastTemplate(cell);
                    break;
                case PagerButtons.Numeric :
                    CreateNumericTemplate(cell);
                    break;
            }
            pagerRow.Controls.Add(cell);
            table.Rows.Add(pagerRow);
            container.Controls.Add(table);
        }

        #endregion

        #region -Private methods for create container of pager control-

        private void CreateNextPreviousFirstLastTemplate(Control container)
        {
            AddText(container);
            AddSpacer(container, 3);
            AddFirstLinkButton(container);
            AddSpacer(container, 3);
            AddPreviousLinkButton(container);
            AddSpacer(container, 3);
            AddNextLinkButton(container);
            AddSpacer(container, 3);
            AddLastLinkButton(container);
            if (_pagerInfo.EnablePagerJump)
            {
                AddSpacer(container, 3);
                AddJumpButton(container);
            }
        }

        private void CreateNextPreviousTemplate(Control container)
        {
            AddText(container);
            AddSpacer(container, 3);
            AddPreviousLinkButton(container);
            AddSpacer(container, 3);
            AddNextLinkButton(container);
            if (_pagerInfo.EnablePagerJump)
            {
                AddSpacer(container, 3);
                AddJumpButton(container);
            }
        }

        private void CreateNumericFirstLastTemplate(Control container)
        {
            int pBtnCount = _pagerInfo.PagerSetting.PageButtonCount;
            int startPage = this._pagerInfo.CurrentPageIndex / pBtnCount * pBtnCount + 1;
            int endPage = startPage + _pagerInfo.PagerSetting.PageButtonCount - 1;
            if (endPage > _pagerInfo.pageCount)
            {
                endPage = _pagerInfo.pageCount;
            }

            AddText(container);
            AddSpacer(container, 2);

            AddSpacer(container);
            AddFirstLinkButton(container);
            AddSpacer(container, 2);

            if (startPage > pBtnCount)
            {
                AddSpacer(container);
                AddWizard(container, false);
            }

            for (int i = startPage; i <= endPage; i++)
            {
                AddSpacer(container);
                AddNumericButton(container, i.ToString());
            }

            if (endPage != _pagerInfo.pageCount)
            {
                AddSpacer(container);
                AddWizard(container, true);
            }

            AddSpacer(container, 3);
            AddLastLinkButton(container);

            if (_pagerInfo.EnablePagerJump)
            {
                AddSpacer(container, 3);
                AddJumpButton(container);
            }
        }

        private void CreateNumericTemplate(Control container)
        {
            int pBtnCount = _pagerInfo.PagerSetting.PageButtonCount;
            int startPage = this._pagerInfo.CurrentPageIndex / pBtnCount * pBtnCount + 1;
            int endPage = startPage + _pagerInfo.PagerSetting.PageButtonCount - 1;
            if (endPage > _pagerInfo.pageCount)
            {
                endPage = _pagerInfo.pageCount;
            }

            AddText(container);
            AddSpacer(container, 2);

            if (startPage > pBtnCount)
            {
                AddSpacer(container);
                AddWizard(container, false);
            }

            for (int i = startPage; i <= endPage; i++)
            {
                AddSpacer(container);
                AddNumericButton(container, i.ToString());
            }

            if (endPage != _pagerInfo.pageCount)
            {
                AddSpacer(container);
                AddWizard(container, true);
            }

            if (_pagerInfo.EnablePagerJump)
            {
                AddSpacer(container, 3);
                AddJumpButton(container);
            }
            
        }

        #endregion

        #region -Private methods for create child controls of pager control-

        private void AddSpacer(Control container)
        {
            Literal literal = new Literal();
            literal.Text = "&nbsp;";
            container.Controls.Add(literal);
        }

        private void AddSpacer(Control container, int len)
        {
            Literal literal = new Literal();
            for (int i = 0; i < len; i++)
            {
                literal.Text += "&nbsp;";
            }
            container.Controls.Add(literal);
        }

        private void AddText(Control container)
        {
            HtmlGenericControl span = new HtmlGenericControl("span");
            span.InnerHtml = _pagerInfo.PagerText;
            container.Controls.Add(span);
        }

        private void AddText(Control container, string text)
        {
            HtmlGenericControl span = new HtmlGenericControl("span");
            span.InnerHtml = text;
            container.Controls.Add(span);
        }

        private void AddFirstLinkButton(Control container)
        {

            if (!string.IsNullOrEmpty(_pagerInfo.PagerSetting.FirstPageImageUrl))
            {
                ImageButton first = new ImageButton();
                first.CausesValidation = false;
                first.ImageUrl = _pagerInfo.PagerSetting.FirstPageImageUrl;
                if (_pagerInfo.CurrentPageIndex <= 0)
                {
                    first.Enabled = false;
                }
                else
                {
                    first.CommandName = "Page";
                    first.CommandArgument = "1";
                }
                container.Controls.Add(first);
            }
            else
            {
                LinkButton first = new LinkButton();
                first.PostBackUrl = "#";
                first.CausesValidation = false;
                first.Text = _pagerInfo.PagerSetting.FirstPageText;
                if (string.IsNullOrEmpty(first.Text))
                {
                    first.Text = "首页";
                }
                if (_pagerInfo.CurrentPageIndex <= 0)
                {
                    first.Enabled = false;
                }
                else
                {
                    first.CommandName = "Page";
                    first.CommandArgument = "1";
                }
                first.CssClass = _pagerInfo.cssName;
                container.Controls.Add(first);
            }
        }

        private void AddPreviousLinkButton(Control container)
        {
            if (!string.IsNullOrEmpty(_pagerInfo.PagerSetting.PreviousPageImageUrl))
            {
                ImageButton prev = new ImageButton();
                prev.CausesValidation = false;
                prev.ImageUrl = _pagerInfo.PagerSetting.PreviousPageImageUrl;
                if (_pagerInfo.CurrentPageIndex <= 0)
                {
                    prev.Enabled = false;
                }
                else
                {
                    prev.CommandName = "Page";
                    prev.CommandArgument = (_pagerInfo.CurrentPageIndex).ToString();
                }
                container.Controls.Add(prev);
            }
            else
            {
                LinkButton prev = new LinkButton();
                prev.CausesValidation = false;
                prev.PostBackUrl = "#";
                prev.Text = _pagerInfo.PagerSetting.PreviousPageText;
                if (string.IsNullOrEmpty(prev.Text))
                {
                    prev.Text = "上页";
                }
                if (_pagerInfo.CurrentPageIndex <= 0)
                {
                    prev.Enabled = false;
                }
                else
                {
                    prev.CommandName = "Page";
                    prev.CommandArgument = (_pagerInfo.CurrentPageIndex).ToString();
                }
                prev.CssClass = _pagerInfo.cssName;
                container.Controls.Add(prev);
            }
        }

        private void AddNextLinkButton(Control container)
        {
            if (!string.IsNullOrEmpty(_pagerInfo.PagerSetting.NextPageImageUrl))
            {
                ImageButton next = new ImageButton();
                next.CausesValidation = false;
                next.ImageUrl = _pagerInfo.PagerSetting.NextPageImageUrl;
                if (_pagerInfo.CurrentPageIndex == _pagerInfo.pageCount - 1)
                {
                    next.Enabled = false;
                }
                else
                {
                    next.CommandName = "Page";
                    next.CommandArgument = (_pagerInfo.CurrentPageIndex + 2).ToString();
                }
                container.Controls.Add(next);
            }
            else
            {
                LinkButton next = new LinkButton();
                next.PostBackUrl = "#";
                next.CausesValidation = false;
                next.Text = _pagerInfo.PagerSetting.NextPageText;
                if (string.IsNullOrEmpty(next.Text))
                {
                    next.Text = "下页";
                }
                if (_pagerInfo.CurrentPageIndex == _pagerInfo.pageCount - 1)
                {
                    next.Enabled = false;
                }
                else
                {
                    next.CommandName = "Page";
                    next.CommandArgument = (_pagerInfo.CurrentPageIndex + 2).ToString();
                }
                next.CssClass = _pagerInfo.cssName;
                container.Controls.Add(next);
            }
        }

        private void AddLastLinkButton(Control container)
        {
            if (!string.IsNullOrEmpty(_pagerInfo.PagerSetting.LastPageImageUrl))
            {
                ImageButton last = new ImageButton();
                last.CausesValidation = false;
                last.ImageUrl = _pagerInfo.PagerSetting.LastPageImageUrl;
                if (_pagerInfo.CurrentPageIndex == _pagerInfo.pageCount - 1)
                {
                    last.Enabled = false;
                }
                else
                {
                    last.CommandName = "Page";
                    last.CommandArgument = _pagerInfo.pageCount.ToString();
                }
                container.Controls.Add(last);
            }
            else
            {
                LinkButton last = new LinkButton();
                last.PostBackUrl = "#";
                last.CausesValidation = false;
                last.Text = _pagerInfo.PagerSetting.LastPageText;
                if (string.IsNullOrEmpty(last.Text))
                {
                    last.Text = "尾页";
                }
                if (_pagerInfo.CurrentPageIndex == _pagerInfo.pageCount - 1)
                {
                    last.Enabled = false;
                }
                else
                {
                    last.CommandName = "Page";
                    last.CommandArgument = _pagerInfo.pageCount.ToString();
                }
                last.CssClass = _pagerInfo.cssName;
                container.Controls.Add(last);
            }
        }

        private void AddNumericButton(Control container, string text)
        {
            if (Int32.Parse(text) == _pagerInfo.CurrentPageIndex + 1)
            {
                Label lab = new Label();
                lab.Text = text;
                lab.CssClass = _pagerInfo.cssName;
                container.Controls.Add(lab);
            }
            else
            {
                LinkButton num = new LinkButton();
                num.PostBackUrl = "#";
                num.CausesValidation = false;
                num.Text = text;
                num.CommandName = "Page";
                num.CommandArgument = text;
                num.CssClass = _pagerInfo.cssName;
                container.Controls.Add(num);
            }
        }

        private void AddWizard(Control container, bool nextSibling)
        {
            string wizardArg = string.Empty;
            int pBtnCount = _pagerInfo.PagerSetting.PageButtonCount;
            if (nextSibling)
            {
                wizardArg = ((_pagerInfo.CurrentPageIndex / pBtnCount + 1) * pBtnCount + 1).ToString();
            }
            else
            {
                wizardArg = (_pagerInfo.CurrentPageIndex / pBtnCount * pBtnCount).ToString();
            }
            LinkButton wizardBtn = new LinkButton();
            wizardBtn.PostBackUrl = "#";
            wizardBtn.CausesValidation = false;
            wizardBtn.Text = "...";
            wizardBtn.CommandName = "Page";
            wizardBtn.CommandArgument = wizardArg;
            container.Controls.Add(wizardBtn);
        }

        private void AddJumpButton(Control container)
        {
            //HtmlInputText tbJump = new HtmlInputText();
            //tbJump.Style.Add("width", "30px");
            //container.Controls.Add(tbJump);
            //HtmlInputButton btnJump = new HtmlInputButton();
            //btnJump.Attributes.Add("value", "GO");
            ////control.Page.ClientScript.RegisterForEventValidation(btnJump.UniqueID);
            //control.Page.ClientScript.ValidateEvent(btnJump.UniqueID);
            //btnJump.Attributes.Add("onclick", control.Page.ClientScript.GetPostBackEventReference(control, "Page$1", false));
            AddText(container, "跳转至:");
            TextBox tbJump = new TextBox();
            tbJump.Width = Unit.Pixel(20);
            tbJump.Load += new EventHandler(tbJump_Load);
            container.Controls.Add(tbJump);
            LinkButton btnJump = new LinkButton();
            btnJump.PostBackUrl = "#";
            btnJump.CausesValidation = false;
            btnJump.Click += new EventHandler(btnJump_Click);
            btnJump.Text = "GO";
            btnJump.CssClass = _pagerInfo.cssName;
            container.Controls.Add(tbJump);
            container.Controls.Add(btnJump);
        }

        #endregion

        #region -EventHandlers-

        void tbJump_Load(object sender, EventArgs e)
        {
            jumpText = (sender as TextBox).Text;
        }

        void btnJump_Click(object sender, EventArgs e)
        {
            int pageNumber = -1;
            bool canConvertToInt = Int32.TryParse(jumpText, out pageNumber);
            if (canConvertToInt)
            {
                LinkButton btnJump = sender as LinkButton;
                if (pageNumber >= _pagerInfo.pageCount)
                {
                    jumpText = _pagerInfo.pageCount.ToString();
                }
                else if (pageNumber < 1)
                {
                    jumpText = "1";
                }
                btnJump.PostBackUrl = "#";
                btnJump.CommandName = "Page";
                btnJump.CommandArgument = jumpText;
            }
        }

        #endregion
    }
}
