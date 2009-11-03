<%@ Page Title="" Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master" AutoEventWireup="true" CodeFile="SiteMaps.aspx.cs" Inherits="ControlPanel_Common_SiteMaps" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphOpts" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" Runat="Server">
<div class="sitemapContainer" id="sitemapContainer">
    <div class="smContainer">
        <div class="smc_c1">
            <div class="smc_c1_r1 smc_index"></div>
            <div class="smc_c1_r2">总目录</div>
        </div>
        <div class="smc_r2">
            <a href="javascript:{}" ref="generateMain" title="根据当前数据生成全新的站点地图。">立即生成</a>
            <a href='<%= _url %>sitemap/main.xml' target="_blank" title="查看当前站点地图信息。">查看地图</a>
        </div>
    </div>
    
    <div class="smContainer">
        <div class="smc_c1">
            <div class="smc_c1_r1 smc_product"></div>
            <div class="smc_c1_r2">产品</div>
        </div>
        <div class="smc_r2">
            <a href="javascript:{}" ref="generateProduct" title="根据当前数据生成全新的站点地图。">立即生成</a>
            <a href='<%= _url %>sitemap/products.xml' target="_blank" title="查看当前站点地图信息。">查看地图</a>
        </div>
    </div>
    
    <div class="smContainer">
        <div class="smc_c1">
            <div class="smc_c1_r1 smc_article"></div>
            <div class="smc_c1_r2">资讯</div>
        </div>
        <div class="smc_r2">
            <a href="javascript:{}" ref="generateArticle" title="根据当前数据生成全新的站点地图。">立即生成</a>
            <a href='<%= _url %>sitemap/articles.xml' target="_blank" title="查看当前站点地图信息。">查看地图</a>
        </div>
    </div>
    
    <div class="smContainer">
        <div class="smc_c1">
            <div class="smc_c1_r1 smc_category"></div>
            <div class="smc_c1_r2">产品分类</div>
        </div>
        <div class="smc_r2">
            <a href="javascript:{}" ref="generateCategory" title="根据当前数据生成全新的站点地图。">立即生成</a>
            <a href='<%= _url %>sitemap/categories.xml'  target="_blank" title="查看当前站点地图信息。">查看地图</a>
        </div>
    </div>
    
    <div class="smContainer">
        <div class="smc_c1">
            <div class="smc_c1_r1 smc_brand"></div>
            <div class="smc_c1_r2">产品品牌</div>
        </div>
        <div class="smc_r2">
            <a href="javascript:{}" ref="generateBrand" title="根据当前数据生成全新的站点地图。">立即生成</a>
            <a href='<%= _url %>sitemap/brands.xml'  target="_blank" title="查看当前站点地图信息。">查看地图</a>
        </div>
    </div>
    
    <div class="smContainer">
        <div class="smc_c1">
            <div class="smc_c1_r1 smc_industry"></div>
            <div class="smc_c1_r2">产品行业</div>
        </div>
        <div class="smc_r2">
            <a href="javascript:{}" ref="generateIndustry" title="根据当前数据生成全新的站点地图。">立即生成</a>
            <a href='<%= _url %>sitemap/industries.xml'  target="_blank" title="查看当前站点地图信息。">查看地图</a>
        </div>
    </div>
    
    <div class="smContainer">
        <div class="smLoader" id="smLoader">正在生成，请稍候。。。</div>
    </div>
</div>
</asp:Content>

