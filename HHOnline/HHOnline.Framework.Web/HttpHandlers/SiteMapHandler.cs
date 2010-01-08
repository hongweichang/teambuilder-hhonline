using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Xml;
using HHOnline.Framework.Web.SiteMap;
using HHOnline.Shops;
using HHOnline.News.Components;
using HHOnline.News.Services;

namespace HHOnline.Framework.Web.HttpHandlers
{
    public class SiteMapHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            string msg = string.Empty;
            bool result = false;
            try
            {
                switch (context.Request["action"])
                {
                    case "generateMain":
                        msg = GenerateMain(ref result);
                        break;
                    case "generateProduct":
                        msg = GenerateProduct(ref result);
                        break;
                    case "generateArticle":
                        msg = GenerateArticle(ref result);
                        break;
                    case "generateBrand":
                        msg = GenerateBrand(ref result);
                        break;
                    case "generateCategory":
                        msg = GenerateCategory(ref result);
                        break;
                    case "generateIndustry":
                        msg = GenerateIndustry(ref result);
                        break;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                result = false;
            }
            context.Response.Write("{msg:'" + msg + "',result:" + result.ToString().ToLower() + "}");
        }

        string GenerateMain(ref bool result)
        {
            SiteMapBuilder smb = new SiteMapBuilder();
            smb.AddLocalUrl("pages/view.aspx?common-contactinfo", DateTime.Now);
            smb.AddLocalUrl("pages/view.aspx?common-aboutehuaho", DateTime.Now);
            smb.AddLocalUrl("pages/view.aspx?common-honeruser", DateTime.Now);
            smb.AddLocalUrl("pages/view.aspx?common-wflist", DateTime.Now);
            smb.AddLocalUrl("pages/view.aspx?common-friendlink", DateTime.Now);
            smb.AddLocalUrl("pages/view.aspx?common-rightnotice", DateTime.Now);
            smb.AddLocalUrl("pages/view.aspx?common-recruitment", DateTime.Now);
            smb.AddLocalUrl("pages/view.aspx?common-sitemap", DateTime.Now);
            smb.AddLocalUrl("/register.aspx", DateTime.Now);
            smb.AddLocalUrl("/login.aspx", DateTime.Now);
            smb.AddLocalUrl("/main.aspx", DateTime.Now);
            smb.AddLocalUrl("/pages/view.aspx?product-productlist", DateTime.Now);
            smb.AddLocalUrl("/pages/view.aspx?news-newslist", DateTime.Now);
            smb.AddLocalUrl("/pages/view.aspx?product-brand", DateTime.Now);
            smb.AddLocalUrl("/pages/view.aspx?product-category", DateTime.Now);
            smb.AddLocalUrl("/pages/view.aspx?product-industry", DateTime.Now);
            smb.AddLocalUrl("/Pages/Common/SiteMap.aspx", DateTime.Now);
            smb.AddLocalUrl("/WebAdmin/ehuaho_logo.gif", DateTime.Now);
            smb.AddLocalUrl("/WebAdmin/ehuaho_logo_s.gif", DateTime.Now);

            smb.Save(GlobalSettings.MapPath("~/sitemap/main.xml"));
            result = true;
            return "成功生成/更新【总目录】地图！";
        }
        string GenerateCategory(ref bool result)
        {
            List<ProductCategory> cats = ProductCategories.GetCategories();
            SiteMapBuilder smb = new SiteMapBuilder();
            foreach (ProductCategory cat in cats)
            {
                smb.AddLocalUrl("pages/view.aspx?product-category&ID=" + GlobalSettings.Encrypt(cat.CategoryID.ToString()), DateTime.Now);
            }
            smb.Save(GlobalSettings.MapPath("~/sitemap/categories.xml"));
            result = true;
            return "成功生成/更新【产品分类】地图！";
        }
        string GenerateBrand(ref bool result)
        {
            List<ProductBrand> pbs = ProductBrands.GetProductBrands();
            SiteMapBuilder smb = new SiteMapBuilder();
            foreach (ProductBrand pb in pbs)
            {
                smb.AddLocalUrl("pages/view.aspx?product-brand&ID=" + GlobalSettings.Encrypt(pb.BrandID.ToString()), DateTime.Now);
            }
            smb.Save(GlobalSettings.MapPath("~/sitemap/brands.xml"));
            result = true;
            return "成功生成/更新【产品行业】地图！";
        }
        string GenerateIndustry(ref bool result)
        {
            List<ProductIndustry> pbs = ProductIndustries.GetProductIndustries();
            SiteMapBuilder smb = new SiteMapBuilder();
            foreach (ProductIndustry pb in pbs)
            {
                smb.AddLocalUrl("pages/view.aspx?product-industry&ID=" + GlobalSettings.Encrypt(pb.IndustryID.ToString()), DateTime.Now);
            }
            smb.Save(GlobalSettings.MapPath("~/sitemap/industries.xml"));
            result = true;
            return "成功生成/更新【产品品牌】地图！";
        }
        string GenerateProduct(ref bool result)
        {
            ProductQuery pq = new ProductQuery();
            pq.HasPublished = true;
            pq.PageIndex = 0;
            pq.PageSize = int.MaxValue;

            List<Product> ps = Products.GetProductList(pq);
            SiteMapBuilder smb = new SiteMapBuilder();
            foreach (Product p in ps)
            {
                smb.AddLocalUrl("pages/view.aspx?product-product&ID=" + GlobalSettings.Encrypt(p.ProductID.ToString()), DateTime.Now);
            }
            smb.Save(GlobalSettings.MapPath("~/sitemap/products.xml"));
            result = true;
            return "成功生成/更新【产品】地图！";
        }
        string GenerateArticle(ref bool result)
        {
            ProductQuery pq = new ProductQuery();
            pq.HasPublished = true;
            pq.PageIndex = 0;
            pq.PageSize = int.MaxValue;

            List<Article> arts = ArticleManager.GetAllArticles();
            SiteMapBuilder smb = new SiteMapBuilder();
            foreach (Article p in arts)
            {
                smb.AddLocalUrl("pages/view.aspx?news-newsdetail&id=" + GlobalSettings.Encrypt(p.ID.ToString()), DateTime.Now);
            }
            smb.Save(GlobalSettings.MapPath("~/sitemap/articles.xml"));
            result = true;
            return "成功生成/更新【资讯】地图！";
        }
    }
}
