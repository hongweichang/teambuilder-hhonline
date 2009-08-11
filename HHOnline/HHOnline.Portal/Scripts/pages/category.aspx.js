/// <reference path="jquery-vsdoc.js" />
function AddCategory() {
    var url = 'CategoryAdd.aspx?ID=' + window.$selectNodeId + '&t=' + Math.random();
    showPage({ title: '新增资讯分类', url: url });
    return false;
}

function DeleteCategory() {
    var catIds = $('#childList').find('input[type=checkbox][rel=child-cat][checked=true]');
    if (catIds.length <= 0) {
        showMsg({ msg: '请选择需要删除的资讯分类！' });
        return false;
    }


    if (confirm("确定要删除所选中的资讯分类信息吗？")) {
        var ids = catIds.map(function() {
            return $(this).val();
        }).get().join(',');

        articleAjax({ categoryIds: ids, action: 'DeleteArticleCategory', t: Math.random() });
    }
    return false;
}
function DeleteCategoryOne(t) {
    if (confirm("确定要删除所选中的资讯分类信息吗？")) {
        var ids = $(t).parent().attr('catId');

        articleAjax({ categoryIds: ids, action: 'DeleteArticleCategory', t: Math.random() });
    }
    return false;
}

function EditCategory(t) {
    var id = $(t).parent().attr('catId');
    var url = 'CategoryEdit.aspx?ID=' + id + '&t=' + Math.random();
    showPage({ title: '修改资讯分类', url: url });
    return false;
}

function AddArticle() {
    var url = 'ArticleAdd.aspx?ID=' + window.$selectNodeId + '&t=' + Math.random();
    showPage({ title: '新增资讯', url: url ,width:680});
    return false;
}

function EditArticle(t) {    
    var id = $(t).parent().attr('articleId');
    var url= 'ArticleEdit.aspx?ID=' + id + '&t=' + Math.random();
    showPage({ title: '修改资讯', url: url });       
    return false;
}

function DeleteArticle() {
    var deptIds = $('#childList').find('input[type=checkbox][rel=child-article][checked=true]');
    if (deptIds.length <= 0) {
        showMsg({ msg: '请选择需要删除的资讯！' });
        return false;
    }


    if (confirm("确定要删除所选中的资讯分类信息吗？")) {
        var ids = deptIds.map(function() {
            return $(this).val();
        }).get().join(',');

        articleAjax({ newsIds: ids, action: 'DeleteArticle', t: Math.random() });
    }
    return false;
}
function DeleteArticleOne(t) {
    if (confirm("确定要删除所选中的资讯信息吗？")) {
        var ids = $(t).parent().attr('catId');

        articleAjax({ newsIds: ids, action: 'DeleteArticle', t: Math.random() });
    }
    return false;
}

function showArticleInfo(id) {
    var url = 'ArticleDetail.aspx?ID=' + id + '&t=' + Math.random();
    showPage({ title: '资讯详细信息', url: url, closable: true });
}


function articleAjax(data) {
    $.ajax({
        url: 'article.axd',
        data: data,
        dataType: 'json',
        error: function(ex) {
            showMsg({ msg: ex.responseText });
            return false;
        },
        success: function(d) {
            showMsg({ msg: d.msg, onunload: function() {
                if (d.suc) {
                    window.location.href = window.location.href;
                }
            }
            });
        }
    })
}