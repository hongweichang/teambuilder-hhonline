/// <reference path="jquery-vsdoc.js" />

//添加分类
function AddCategory(parentId,target) {
    var url = 'CategoryAdd.aspx?ParentID=' + parentId;
    if (typeof target != 'undefined') {
        var propertyID = $(target).parent().attr('catId');
        url =url +'&PropertyID='+propertyID;
    }
    url =url + '&t=' + Math.random();
    showPage({ title: '新增产品分类', url: url });
    return false;
}
function redirectToProduct(id) {
    parent.window.location.href = productUrl + '&ci=' + id;
}
//修改分类信息
function UpdateCategory(id, target) {
    if (typeof target != 'undefined') {
        id = $(target).parent().attr('catId');
    }
    
    if (typeof id=='undefined' || id =='0')  {
        showMsg({ msg: '请选择需要进行信息修改的分类信息！' });
        return false;
    }
    var url = 'CategoryAdd.aspx?ID=' + id + '&t=' + Math.random();
    showPage({ title: '修改分类信息', url: url });
    return false; 
}

//删除分类信息
function DeleteCategory(target){
    
     if (typeof target != 'undefined') {
        id = $(target).parent().attr('catId');
    }
    else{
        var catIds = $('#tbCat').find('input[type=checkbox][rel=child-category][checked=true]');
        if (catIds.length <= 0) {
            showMsg({ msg: '请选择需要删除的分类！' });
            return false;
            }
        id =   catIds.map(function() {
            return $(this).val();
        }).get().join(',');
    }

    if (confirm("确定要删除所选中的产品分类信息吗？")) {
        catAjax({ categoryID: id, action: 'DeleteCategory', t: Math.random() });
    }
    return false;
    
}

//添加分类属性
    function AddProperty(categoryID) {
     if (typeof categoryID=='undefined' || categoryID =='0')  {
         showMsg({ msg: '请选择需要进行属性增加的分类信息！' });
        return false;
    }
    var url = 'PropertyAdd.aspx?CategoryID=' + categoryID + '&t=' + Math.random();
    showPage({ title: '新增分类属性', url: url });
    return false;
}

//修改分类属性信息
function UpdateProperty(id, target) {
    if (typeof target != 'undefined') {
        id = $(target).parent().attr('catId');
    }
    
    if (typeof id=='undefined' || id =='0')  {
        showMsg({ msg: '请选择需要进行信息修改的分类属性信息！' });
        return false;
    }
    var url = 'PropertyAdd.aspx?ID=' + id + '&t=' + Math.random();
    showPage({ title: '修改分类属性信息', url: url });
    return false; 
}

//删除分类信息
function DeleteProperty(target){
     if (typeof target != 'undefined') {
        id = $(target).parent().attr('catId');
    }
    else{
        var catIds = $('#tbProperty').find('input[type=checkbox][rel=child-category][checked=true]');
        if (catIds.length <= 0) {
            showMsg({ msg: '请选择需要删除的分类属性！' });
            return false;
            }
        id =   catIds.map(function() {
            return $(this).val();
        }).get().join(',');
    }

    if (confirm("确定要删除所选中的分类属性信息吗？")) {
        catAjax({ propertyID: id, action: 'DeleteProperty', t: Math.random() });
    }
    return false;
    
}


//发送AJax请求
function catAjax(data) {
    $.ajax({
        url: 'productcategory.axd',
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