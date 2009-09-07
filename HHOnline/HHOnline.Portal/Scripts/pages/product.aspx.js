/// <reference path="../jquery-vsdoc.js" />
$().ready(function() {
    var pics = new Array();
    $.each(pictures, function(i, n) {
        pics.push({
            Link: n.Url,
            Url: n.Url,
            Title: '',
            Description: ''
        })
    });
    if (pics.length != 0 && pics[0].Url != '') {
        $('#divPics').hrzAccordion({
            pictures: pics,
            width: 300,
            height: 200,
            titleHeight: 0
        });
    }
    else {
        $('#divPics').html('没有展示图片！');
    }
});