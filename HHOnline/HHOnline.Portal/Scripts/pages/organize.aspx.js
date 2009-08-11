/// <reference path="../jquery-vsdoc.js" />
function AddDept(parentId) {
    if (typeof parentId == 'undefined') {
        showMsg({ msg: "必须选择一个部门作为上级部门！" });
        return false;
    }
    var url = 'OrganizeAdd.aspx?ID=' + parentId + '&t=' + Math.random();
    showPage({ title: '新增部门', url: url });
    return false;
}
function DeleteDept() {
    var deptIds = $('#childList').find('input[type=checkbox][rel=child-dept][checked=true]');
    if (deptIds.length <= 0) {
        showMsg({ msg: '请选择需要删除的部门！' });
        return false;
    }

    if (confirm("确定要删除所选中的组织机构信息吗？")) {
        var ids = deptIds.map(function() {
            return $(this).val();
        }).get().join(',');

        orgAjax({ orgid: ids, action: 'DeleteOrganize', t: Math.random() });
    }
    return false;
}
function DeleteDeptOne(t) {

    if (confirm("确定要删除所选中的组织机构信息吗？")) {
        var ids = $(t).parent().attr('orgId');
        orgAjax({ orgid: ids, action: 'DeleteOrganize', t: Math.random() });
    }
    return false;
}
function UpdateDept(t) {
    var id = $(t).parent().attr('orgId');
    var url = 'OrganizeUpdate.aspx?ID=' + id + '&t=' + Math.random();
    showPage({ title: '修改部门信息', url: url });
    return false;
}

function AddUser() {
    if (typeof window.$selectNodeId == 'undefined') {
        showMsg({ msg: "必须选择一个部门作为新增用户的所属部门！" });
        return false;
    }
    var url = 'UserAdd.aspx?ID=' + window.$selectNodeId + '&t=' + Math.random();
    showPage({ title: '新增内部用户', url: url });
    return false;
}
function showUserInfo(id) {
    var url = 'UserDetail.aspx?ID=' + id + '&t=' + Math.random();
    showPage({ title: "用户信息", url: url, closable: true });
}
function UpdateUser(t) {
    var id = $(t).parent().attr('userId')
    var url = 'UserUpdate.aspx?ID=' + id + '&t=' + Math.random();
    showPage({ title: "用户信息修改", url: url });
    return false;
}
function DeleteUser() {
    var userIds = $('#childList').find('input[type=checkbox][rel=inner-user][checked=true]');
    if (userIds.length <= 0) {
        showMsg({ msg: '请选择需要删除的用户！' });
        return false;
    }
    if (confirm("确定要删除所选中的用户信息吗？")) {
        var ids = userIds.map(function() {
            return $(this).val();
        }).get().join(',');

        orgAjax({ userid: ids, action: 'DeleteUser', t: Math.random() });
    }
    return false;
}
function DeleteUserOne(t) {
    if (confirm("确定要删除所选中的用户信息吗？")) {
        var ids = $(t).parent().attr('userId');

        orgAjax({ userid: ids, action: 'DeleteUser', t: Math.random() });
    }
    return false;
}
function SetGrade(t) {
    alert($(t).parent().attr('userId'));
}
function orgAjax(data) {
    $.ajax({
        url: 'organize.axd',
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