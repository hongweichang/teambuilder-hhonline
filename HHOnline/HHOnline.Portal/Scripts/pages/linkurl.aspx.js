/// <reference path="../jquery-vsdoc.js" />
function addLinkUrl() {
    showPage({
        marginTop: 120,
        width: 500,
        title: '新增常用链接',
        url: 'site/LinkUrlAdd.aspx?t=' + Math.random(),
        bgColor: '#888'
    });
    return false;
}
function addFriendLink() {
    showPage({
        marginTop: 120,
        width: 500,
        title: '新增友情链接',
        url: 'common/FriendLinkAdd.aspx?t=' + Math.random(),
        bgColor: '#888'
    });
    return false;
}
function editFriendLink(t) {
    showPage({
        marginTop: 120,
        width: 500,
        title: '修改友情链接',
        url: 'common/FriendLinkEdit.aspx?id=' + t.getAttribute('ref') + '&t=' + Math.random(),
        bgColor: '#888'
    });
    return false;
}