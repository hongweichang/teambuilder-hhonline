
function CloseEdit() {
    var url = '../controlpanel.aspx?news-articleparent&t=' + Math.random();
    parent.window.location.href = url;
    return false;
}


$().ready(function() {
    $('input[rel=datepicker]').datepick({
        year: '-40:5',
        popTo: 'input[rel=datepicker]',
        dateFormat: 'yyyy年MM月dd日'
    });
});