function showDetails(id) {
    showPage({
        title: '用户详细信息',
        bgColor: '#888',
        marginTop: 100,
        closable: true,
        url: 'profiles/companyuserdetails.aspx?ID='+id+'&t=' + Math.random()
    });
}
function editUser(id) {
    showPage({
        width:800,
        title: '修改用户信息',
        bgColor: '#888',
        marginTop: 50,
        closable: false,
        url: 'profiles/companyuseredit.aspx?ID=' + id + '&t=' + Math.random()
    });
}
function addUser() {
    showPage({
        width: 800,
        title: '修改用户信息',
        bgColor: '#888',
        marginTop: 50,
        closable: false,
        url: 'profiles/companyuseredit.aspx?mode=Add&t=' + Math.random()
    });
    return false;
}