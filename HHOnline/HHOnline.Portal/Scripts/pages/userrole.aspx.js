function UserSelect(id) {
    var url = 'permission/UserSelect.aspx?ID=' + id + '&t=' + Math.random();
    showPage({
        url: url,
        title: '用户角色关系',
        marginTop: 50,
        bgColor:'#888',
        width: 610,
        closable: false
    });
}