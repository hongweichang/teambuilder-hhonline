
function changePwd() {
    showPage({
        title: '修改密码',
        bgColor: '#888',
        marginTop: 100,
        closable: false,
        url: 'profiles/changepwd.aspx?t=' + Math.random()
    });
}
function changeQA() {
    showPage({
        title: '修改密码提示',
        bgColor: '#888',
        marginTop: 100,
        closable: false,
        url: 'profiles/changeqa.aspx?t=' + Math.random()
    });
}
$().ready(function() {
    $.fn.cookie({ action: 'set', name: 'hhonline_menu', value: 'item_dashboard' });
});