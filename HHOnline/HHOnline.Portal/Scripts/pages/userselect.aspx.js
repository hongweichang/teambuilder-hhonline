/// <reference path="../jquery-vsdoc.js" />
function cancelSelect() {
    if (confirm('退出将不会对所做的修改进行任何保存操作，确定继续？')) {
        cancel();
    }
    return false;
}
function addUser() {
    var ids = $('#treeviewOrganize').find('input[type=checkbox][checked=true]');
    if (ids.length == 0) {
        alert('请从【待选择用户】列表中选择需分配此权限的用户！');
        return false;
    }
    return true;
}
function deleteUser() {
    var ids = $('#treeViewUser').find('input[type=checkbox][checked=true]');
    if (ids.length == 0) {
        alert('请从【已选择用户】列表中选择需分配此权限的用户！');
        return false;
    }
    return true;
}