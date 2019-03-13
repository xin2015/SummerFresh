
//地图对象。
var mapObj;
//标点集合。
var markerArr = new Array();

//弹窗集合。
var windowArr = new Array();

var map;



$(function () {
    InitMap();
})
//初始化地图并定义拖动和缩放
function InitMap() {

    var mapLayers = null;
    //    var position = new AMap.LngLat(108.66666, 34.533333);
    //    mapObj = new AMap.Map("map", {
    //        view: new AMap.View2D({
    //            center: position, //创建地图二维视口
    //            zoom: 4, //设置地图缩放级别
    //            rotation: 0 //设置地图旋转角度
    //        }),
    //        zooms: [4, 19],
    //        lang: "zh_cn", //设置地图语言类型，默认：中文简体
    //        layers: mapLayers
    //    }); //创建地图实例

    map = new Map({
        ele: "map",
        center: [108.66666, 34.533333], //地图中心
        zoom: 4,
        zoomchange: zc,
        dragend: dr

    });
    mapOjb = map.getMap();





}

function zc() {
    var isstop = $("#isStopChange").val();
    if (isstop == '0') {

        getzoomAndCenter(mapOjb);
    }
}
function dr() {
    var isstop = $("#isStopChange").val();
    if (isstop == '0') {
        getzoomAndCenter(mapOjb);
    }
}


function getzoomAndCenter(e) {

    //获取页面存储
    var province = $("#nowprovince").val();
    var zoom = $("#nowzoom").val();
    var zoomchange = $("#zoomchange").val();
    //获取新的缩放级别
    var newzoom = e.getZoom();

    $("#nowzoom").val(newzoom);
    //判断缩放级别是否越界

    if (zoom > zoomchange || newzoom > zoomchange) {
        var ischange = 0;
        if (zoom < zoomchange && newzoom > zoomchange) {
            ischange = 1;
        }
        else if (zoom > zoomchange && newzoom < zoomchange) {
            ischange = 1;
        }

        if (ischange == 1) {
            $("#idnostr").val("");
            clearMarkerAndWindow();
        }
        e.getCity(function (result) {

            if (result.province != province) {
                ischange = 1;
                $("#nowprovince").val(result.province);
            }
            if (ischange > 0) {
                //            alert('请求数据：' + result.province+'-'+zoom+':'+newzoom);
                var pp = result.province;
                if (pp == "") {
                    pp = "北京";
                }
                serMap(pp, newzoom, 0, "");
            }
        })
    }


}


//添加单个点和弹窗。
function pushMarkerAndWindow(id, mrk, win) {
    markerArr.push({ id: id, marker: mrk });
    windowArr.push({ id: id, window: win });
}
//清空点和窗体容器。
function clearMarkerAndWindow() {
    map.clear();
    markerArr = new Array();
    windowArr = new Array();
}
function setVV() {
    $("#isStopChange").val('1');
    var mapObj = map.getMap();
    mapObj.setFitView();
    setTimeout(function () { $("#isStopChange").val('0'); }, 1000);

}

////锚点。
function CreatePoint(lng, lat, makercontent, infocontent, pointCount, id) {

    var mapObj = map.getMap();
    if (lng == 0 || lat == 0) {
        return;
    }

    //添加google坐标mkr
    var marker = new AMap.Marker({
        map: mapObj, //将点添加到地图
        position: new AMap.LngLat(lng, lat),
        content: makercontent
    });

    //    if (pointCount > 1 && pointCount < 5) {
    //        marker.setAnimation('AMAP_ANIMATION_DROP'); //设置点标记的动画效果，此处为弹跳效果
    //    }
    var infoWindow = new AMap.InfoWindow({
        offset: new AMap.Pixel(2, -33),
        content: infocontent
    });
    //    ,
    //        size: new AMap.Size(400, 200)
    AMap.event.addListener(marker, 'click', function () { //鼠标点击marker弹出自定义的信息窗体
        infoWindow.open(mapObj, marker.getPosition());
    });

    AMap.event.addListener(infoWindow, 'open', function (e) {
        window.setTimeout(function () {

            self.map.setInfoWindowInViewPoint(e.target);
        }, 1000);
    });

    pushMarkerAndWindow(id, new AMap.LngLat(lng, lat), infoWindow);

    return marker;
}






