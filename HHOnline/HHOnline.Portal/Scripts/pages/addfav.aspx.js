$().ready(function() {
    var r = $('#divRate');
    r.rating({
        onRating: function(v, e) {
            r.next().val(v);
        },
        curvalue: 1
    });
});