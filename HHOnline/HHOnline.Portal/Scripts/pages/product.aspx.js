/// <reference path="../jquery-vsdoc.js" />
function addFav() {
    if (_infos.l) {
        showPage({
            title: '加为收藏',
            bgColor: '#888',
            marginTop: 100,
            url: relativeUrl + 'pages/profiles/AddFav.aspx?type=p&&id=' + _infos.d + "&&t=" + Math.random()
        });
    }
    else {
        showMsg({
            top: 150,
            bgColor: '#888',
            msg: '登录用户才能进行收藏操作!'
        });
    }
}
function addCar() {

}

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

    $('#anchorAddFav').click(addFav);
    $('#anchorAddCar').click(addCar);
});