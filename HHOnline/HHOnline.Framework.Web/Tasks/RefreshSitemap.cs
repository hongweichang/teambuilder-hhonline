using System;
using System.Xml;
using HHOnline.Task;
using HHOnline.Framework.Web.SiteMap;
using HHOnline.Shops;
using System.Collections.Generic;
using HHOnline.News.Components;
using HHOnline.News.Services;
using System.Threading;

namespace HHOnline.Framework.Web.Tasks
{
    public class RefreshSitemap : ITask
    {

        public void Execute(XmlNode node)
        {
            bool isComplete = false;
            while (isComplete)
            {
                try
                {
                    SiteMapBuilder smb = new SiteMapBuilder();
                    smb.AddLocalUrl("pages/view.aspx?common-contactinfo", DateTime.Now);
                    smb.AddLocalUrl("pages/view.aspx?common-aboutehuaho", DateTime.Now);
                    smb.AddLocalUrl("pages/view.aspx?common-honeruser", DateTime.Now);
                    smb.AddLocalUrl("pages/view.aspx?common-wflist", DateTime.Now);
                    smb.AddLocalUrl("pages/view.aspx?common-friendlink", DateTime.Now);
                    smb.AddLocalUrl("pages/view.aspx?common-rightnotice", DateTime.Now);
                    smb.Save(GlobalSettings.MapPath("~/sitemap/main.xml"));

                    List<ProductCategory> cats = ProductCategories.GetCategories();
                    smb = new SiteMapBuilder();
                    foreach (ProductCategory cat in cats)
                    {
                        smb.AddLocalUrl("pages/view.aspx?product-category&ID=" + GlobalSettings.Encrypt(cat.CategoryID.ToString()), DateTime.Now);
                    }
                    smb.Save(GlobalSettings.MapPath("~/sitemap/categories.xml"));

                    List<ProductBrand> pbs = ProductBrands.GetProductBrands();
                    smb = new SiteMapBuilder();
                    foreach (ProductBrand pb in pbs)
                    {
                        smb.AddLocalUrl("pages/view.aspx?product-brand&ID=" + GlobalSettings.Encrypt(pb.BrandID.ToString()), DateTime.Now);
                    }
                    smb.Save(GlobalSettings.MapPath("~/sitemap/brands.xml"));

                    List<ProductIndustry> pis = ProductIndustries.GetProductIndustries();
                    smb = new SiteMapBuilder();
                    foreach (ProductIndustry pi in pis)
                    {
                        smb.AddLocalUrl("pages/view.aspx?product-industry&ID=" + GlobalSettings.Encrypt(pi.IndustryID.ToString()), DateTime.Now);
                    }
                    smb.Save(GlobalSettings.MapPath("~/sitemap/industries.xml"));

                    ProductQuery pq = new ProductQuery();
                    pq.HasPublished = true;
                    pq.PageIndex = 0;
                    pq.PageSize = int.MaxValue;

                    List<Product> ps = Products.GetProductList(pq);
                    smb = new SiteMapBuilder();
                    foreach (Product p in ps)
                    {
                        smb.AddLocalUrl("pages/view.aspx?product-product&ID=" + GlobalSettings.Encrypt(p.ProductID.ToString()), DateTime.Now);
                    }
                    smb.Save(GlobalSettings.MapPath("~/sitemap/products.xml"));


                    List<Article> arts = ArticleManager.GetAllArticles();
                    smb = new SiteMapBuilder();
                    foreach (Article p in arts)
                    {
                        smb.AddLocalUrl("pages/view.aspx?news-newsdetail&id=" + GlobalSettings.Encrypt(p.ID.ToString()), DateTime.Now);
                    }
                    smb.Save(GlobalSettings.MapPath("~/sitemap/articles.xml"));
                    isComplete = true;
                }
                catch {
                    isComplete = false;
                    Thread.Sleep(TimeSpan.FromMinutes(5));
                }
            }
        }
    }
}
