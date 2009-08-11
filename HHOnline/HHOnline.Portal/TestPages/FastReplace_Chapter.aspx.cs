using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Algorithm.FastRelaceAlgorithm;

public partial class TestPages_FastReplace_Chapter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindText();
    }
    void BindText()
    {
        string text = "淘宝的诚信自查系统昨天（7月24日）上线了，这是为净化网上购物大环境的一个举措，但是自查系统误查现象十分严重，有的卖家打电话到淘宝客服狠骂客服，一部分卖家自发成立反淘宝联盟发起反淘宝活动。昨日下午16：00左右，淘宝网客服部门收到“柠檬绿茶”、“双生儿香港评价店”等网店店主的举报，有人使用大量“马甲帐号”恶意拍下不买。在短时期内，有超过200个帐户几乎同一时刻拍下这些网店中的商品，但是，最终这些所谓的买家并没有付款，直接导致这些网店出现大量商品下架的情况，对这些店铺的交易产生了重大的影响。";
        ReplaceKeyValue[] rkv = new ReplaceKeyValue[]{
            new ReplaceKeyValue("淘宝","<a href=\"http://www.taobao.com\">淘宝</a>"),
            new ReplaceKeyValue("狠骂","**")
        };
        FastReplace fr = new FastReplace(rkv);
        divContent.InnerHtml = fr.ReplaceAll(text);
    }
}
