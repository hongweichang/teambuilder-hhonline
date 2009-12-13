/// <reference path="../jquery-vsdoc.js" />
//#region
function uploadImage() {
    showPage({
        initWidth: 540,
        marginTop: 20,
        bgColor:'#888',
        title: '产品图片管理',
        url: relativeUrl +'controlpanel/product/productpicturemanager.aspx?t=' + Math.random(),
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
    if (data != null) {
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
//#endregion

//#region
var catHtml = '<a id="catItem_{0}" href="javascript:{}" class="cat_list roundbg"><span><span><span><span>' +
                '<input type="hidden" name="ids" value="{0}"/>' +
                '<input type="hidden" name="names" value="{1}"/>{1}' +
                '<span class="close" title="删除" onclick="removeItem({0});" onmouseover="this.className=\'close closeHover_CL\';" onmouseout="this.className=\'close\';" catId="{0}"></span>' +
            '</span></span></span></span></a>';
function addCategories(id, name) {
    var v = hfCatListHolder.val();
    if (v == '' || v.indexOf('[' + id + ']') < 0) {
        $(catHtml.format(id, name)).appendTo(catContainer);
        hfCatListHolder.val(v + ':[' + id + ']');
        hfCatHTML.val(catContainer.html());
    }
}
function removeItem(id) {
    $('#catItem_' + id).remove();
    var v = hfCatListHolder.val();
    if (v.indexOf('[' + id + ']') >= 0) {
        hfCatListHolder.val(v.replace('[' + id + ']', ''));
        hfCatHTML.val(catContainer.html());
    }
}
function selectCategories() {
    showPage({
        title: '选择产品类别',
        url: relativeUrl + 'controlpanel/productmodal/selectCategories.aspx?t=' + Math.random(),
        closable: false,
        marginTop: 150,
        bgColor: '#888'
    })
}
//#endregion

//#region
var indHtml = '<a id="indItem_{0}" href="javascript:{}" class="cat_list roundbg"><span><span><span><span>' +
                '<input type="hidden" name="ids" value="{0}"/>' +
                '<input type="hidden" name="names" value="{1}"/>{1}' +
                '<span class="close" title="删除" onclick="removeIndItem({0});" onmouseover="this.className=\'close closeHover_CL\';" onmouseout="this.className=\'close\';" indId="{0}"></span>' +
            '</span></span></span></span></a>';
function addIndustries(id, name) {
    var v = hfIndListHolder.val();
    if (v == '' || v.indexOf('[' + id + ']') < 0) {
        $(indHtml.format(id, name)).appendTo(indContainer);
        hfIndListHolder.val(v + ':[' + id + ']');
        hfIndHTML.val(indContainer.html());
    }
}
function removeIndItem(id) {
    $('#indItem_' + id).remove();
    var v = hfIndListHolder.val();
    if (v.indexOf('[' + id + ']') >= 0) {
        hfIndListHolder.val(v.replace('[' + id + ']', ''));
        hfIndHTML.val(indContainer.html());
    }
}
function selectIndustries() {
    showPage({
        title: '选择产品行业',
        url: relativeUrl + 'controlpanel/productmodal/selectIndustries.aspx?t=' + Math.random(),
        closable: false,
        marginTop: 150,
        bgColor: '#888'
    })
}
//#endregion

var catContainer, hfCatListHolder, hfCatHTML,indContainer,hfIndListHolder,hfIndHTML;
$().ready(function() {
    catContainer = $('#divCategories');
    hfCatListHolder = $('#hfCatListHolder').children('input:first-child');
    hfCatHTML = hfCatListHolder.next();
    if (hfCatHTML.val() != '') {
        catContainer.html(hfCatHTML.val());
    }

    indContainer = $('#spanIndustries');
    hfIndListHolder = $('#hfIndListHolder').children('input:first-child');
    hfIndHTML = hfIndListHolder.next();
    if (hfIndHTML.val() != '') {
        indContainer.html(hfIndHTML.val());
    }
    renderDataToClient(uploaded);
});