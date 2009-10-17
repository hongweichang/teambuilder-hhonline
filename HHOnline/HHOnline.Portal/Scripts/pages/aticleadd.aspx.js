
function CloseEdit() {
    var url = '../controlpanel.aspx?news-articleparent&t=' + Math.random();
    parent.window.location.href = url;
    return false;
}
function showTitlePics(m) {
    if (typeof m == 'undefined') {m = $('#selTitlePic').find('select')[0]; }
    var k = m.options[m.selectedIndex].value;

    var picUrl = eval('titlePics.item' + k);
    $('#tdTitleImg').find('img').attr('src', picUrl);
}

$().ready(function() {
    $('input[rel=datepicker]').datepick({
        year: '-40:5',
        popTo: 'input[rel=datepicker]',
        dateFormat: 'yyyy年MM月dd日'
    });
    showTitlePics();
});