/// <reference path="../jquery-vsdoc.js" />
function changeProperty(t) {
    var m = $(t);
    if (m.val() != '0') {
        m.prev().val(m.val());
    }
}

var selectedTrades = null;
function selectTrade() {
    selectedTrades = null;
    selectedTrades = $('#tradeNames').next().val();
   
    showPage({
        title:'选择产品行业',
        url: 'product/TradeSelect.aspx?t=' + Math.random(),
        closable:false,
        marginTop:150,
        bgColor:'#888'
    })
}
function insertTrade(ids, names) {
    var pp = null;

    var tradeNames = $('#tradeNames').html(names);
    var vv = tradeNames.next();

    tradeNames.find('a').click(function() {
        pp = $(this).parent();
        vv.val(vv.val().replace('[' + pp.attr('rel') + ']', '')).next().val(tradeNames.html());
        pp.remove();
    });
    tradeNames.next().val(ids).next().val(names);
   
}

function uploadImage() {
    showPage({
        initWidth: 540,
        marginTop: 20,
        bgColor:'#888',
        title: '产品图片管理',
        url: 'product/productpicturemanager.aspx?t=' + Math.random(),
        closable:true
    });
}

var urlTemp = "<div rel=\"{5}\" ptIndex=\"pt_{4}\" class=\"productThumbnailList\" style=\"background:{0}\">" +
                                "<div class=\"productThumbnail\" zoomUrl=\"{3}\" style=\"background:url({1}) no-repeat center center;\" >&nbsp;</div>" +
                                "<div class=\"productContent\">{2}</div>" +
                                "<div class=\"productTbOpts\">" +
                                    "<a class=\"setDefault\" href=\"javascript:void(0)\" title=\"设置此图为默认图片\">&nbsp;</a>" +
                                    "<a class=\"deleteAttach\" href=\"javascript:void(0)\" title=\"删除此图片\">&nbsp;</a>" +
                                "</div>" +
                              "</div>";
function renderDataToClient(data) {
    if (data != '') {
        var fp = $('#productImages');
        fp.html('');
        var c = '', pt;

        $(data).each(function(i, n) {
            if (i % 2 == 0) { c = '#F0FFF0'; }
            else { c = '#B0E0E6'; }
            if (first) {
                pt = $(urlTemp.format(c, n.ThumbnailUrl, n.FriendlyFileName, n.Url, i, n.AttachmentID)).appendTo(fp);
            }
            else {
                pt = $(urlTemp.format(c, n.ThumbnailUrl, n.PictureName, n.Url, i, n.PirctureID)).appendTo(fp);
            }
            pt.find('a').click(function() {
                ptIndex = i;
                switch ($(this).attr('class')) {
                    case 'deleteAttach':
                        callDeleting('deleteAttach:' + (first ? n.AttachmentID : n.PictureID));
                        break;
                    case 'setDefault':
                        callDefault('setDefault:' + (first ? n.AttachmentID : n.PictureID));
                        break;
                }
            });
        });
    }
}


//callback server
var ptIndex = null;
function refreshBinder(result, context) {
    renderDataToClient(eval('(' + result + ')'));
}
function deleteAttach(result, context) {
    if (result != '0') {
        alert(result);
    }
    else {
        $('div[ptIndex=pt_' + ptIndex + ']').animate({ 'height': '0px' },
                function() {
                    $(this).remove();
                });
    }
}
function setDefault(result, context) {
    try {
        renderDataToClient(eval('(' + result + ')'));
    }
    catch (ex) {
        alert(result);
    }
}

$().ready(function() {
    var tns = $('#tradeNames');
    var l = tns.next().next();
    if (l.val() != '') {
        tns.html(l.val());
        var vv = tns.next();
        tns.find('a').click(function() {
            pp = $(this).parent();
            vv.val(vv.val().replace('[' + pp.attr('rel') + ']', '')).next().val(tns.html());
            pp.remove();
        });
    }

    var pi = $('#productImages');
    var pn = pi.next().next().val();
    if (pn != '') {
        pi.html(pn);
    }

    renderDataToClient(uploaded);
});