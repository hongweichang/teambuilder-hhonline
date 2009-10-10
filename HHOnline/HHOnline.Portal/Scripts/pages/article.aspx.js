function AddArticle() {
    var url = 'ArticleAddEdit.aspx?act=add&catid=' + window.$selectNodeId + '&t=' + Math.random();
    //showPage({ title: '新增资讯', url: url ,width:680});
    parent.window.location.href = url;
    return false;
}

//function EditArticle(t) {    
//    var id = $(t).parent().attr('articleId');
//    var url= 'ArticleAddEdit.aspx?act=edit&id=' + id + '&t=' + Math.random();
//    //showPage({ title: '修改资讯', url: url });       
//    window.location.href = url;
//    return false;
//}

function EditArticle(id) {    
    var url= 'ArticleAddEdit.aspx?act=edit&id=' + id + '&t=' + Math.random();
    //showPage({ title: '修改资讯', url: url });       
    parent.window.location.href = url;
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
        var ids = $(t).parent().attr('articleid');

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


$().ready(function() {
    $('input[rel=ArticleDateStart]').datepick({
        year: '-40:5',
        popTo: 'input[rel=ArticleDateStart]',
        dateFormat: 'yyyy年MM月dd日'
    });
    
    $('input[rel=ArticleDateEnd]').datepick({
        year: '-40:5',
        popTo: 'input[rel=ArticleDateEnd]',
        dateFormat: 'yyyy年MM月dd日'
    });
});