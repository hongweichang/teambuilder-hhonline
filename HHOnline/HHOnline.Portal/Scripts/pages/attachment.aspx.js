
$().ready(function() {
    $('input[rel=DatePickerStart]').datepick({
        year: '-40:5',
        popTo: 'input[rel=DatePickerStart]',
        dateFormat: 'yyyy年MM月dd日'
    });
    
    $('input[rel=DatePickerEnd]').datepick({
        year: '-40:5',
        popTo: 'input[rel=DatePickerEnd]',
        dateFormat: 'yyyy年MM月dd日'
    });
});