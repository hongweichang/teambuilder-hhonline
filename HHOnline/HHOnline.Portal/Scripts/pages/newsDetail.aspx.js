var curfontsize=12;
var curlineheight=20;
function fontZoomA(){
  if(curfontsize>8){
    document.getElementById('ctl00_ContentPlaceHolderContent_lblContent').style.fontSize=(--curfontsize)+'pt';
    document.getElementById('ctl00_ContentPlaceHolderContent_lblAbstract').style.fontSize=curfontsize+'pt';
   // document.getElementById('newsbody').style.lineHeight=(--curlineheight)+'pt';
  //document.getElementById('newshome').style.fontSize=(--curfontsize)+'pt';
 document.getElementById('news_content_detail').style.lineHeight=(--curlineheight)+'pt';
  }
}
function fontZoomB(){
  if(curfontsize<64){
    document.getElementById('ctl00_ContentPlaceHolderContent_lblContent').style.fontSize=(++curfontsize)+'pt';
    document.getElementById('ctl00_ContentPlaceHolderContent_lblAbstract').style.fontSize=curfontsize+'pt';
 document.getElementById('news_content_detail').style.lineHeight=(++curlineheight)+'pt';
 // document.getElementById('newshome').style.fontSize=(++curfontsize)+'pt';
// document.getElementById('newshome').style.lineHeight=(++curlineheight)+'pt';
  }
}
