$().ready(function() {
    $('input[rel=datepicker1]').datepick({
        year: '-40:5',
        popTo: 'input[rel=datepicker1]',
        dateFormat: 'yyyy年MM月dd日'
    });
     $('input[rel=datepicker2]').datepick({
        year: '-40:5',
        popTo: 'input[rel=datepicker2]',
        dateFormat: 'yyyy年MM月dd日'
    });
});