(function (factory) {
    if (typeof define === 'function' && define.amd) {
        define([], factory);
    } else if (typeof module !== 'undefined' && module.exports) {
        module.exports = factory;
    } else {
        window.Map = factory();
    }
}(function () {

    var $panel = $(".side-panel");
    var $side = $('.side-wrap');
    var $side_in = $('.side-an-info');
    var step = 50;//一次滚动50
    var $top;

    if ($panel[0]) {
        $top = $panel.position().top;
        $side.on("mousewheel DOMMouseScroll", ".side-an.current", function (e) {
            var oldScrollTop = $side.scrollTop();

            var newTop;
            //向上/下？滚动
            //console.log(e);
            //console.log(e.originalEvent.detail+"  "+   e.originalEvent.wheelDelta);
            if ((e.type == "DOMMouseScroll" && e.originalEvent.detail > 0) || e.originalEvent.wheelDelta > 0) {
                //$side.scrollTop(oldScrollTop - step);
                newTop = oldScrollTop - step;
                //$side_in.css('top',(+$side.scrollTop()+$top)+'px');
                // console.log("up");

            } else if ((e.type == "DOMMouseScroll" && e.originalEvent.detail == 0) || e.originalEvent.wheelDelta == 0) {
                return;
            } else {
                //$side.scrollTop(oldScrollTop + step);
                //$side_in.css('top',(-$side.scrollTop()+$top)+'px');

                newTop = oldScrollTop + step;
                // console.log("down");

            }
            $side.scrollTop(newTop);
            $side_in.css('top', $side_in.parent().offset().top);
            e.stopPropagation();
        })
    }



    var Map = function (options) {
        this.options = $.extend({
            ele: "map",//容器 ID
            zoomOutEle: "narrow", //缩小地图 元素 ID
            zoomInEle: "magnify",//放大地图 元素 ID
            satelliteEle: "sate_llite", //切换卫星 元素ID
            trafficEle: "traffic", //切换路况 元素ID
            trafficTimeEle: "road_time",  //切换路况时间 元素ID
            fullScreenEle: "screen", //全屏 元素ID
            refreshTrafficBtn: "road_refresh", //刷新路况 按钮 元素ID
            center: [107.159969, 32.567962], //地图中心
            zoom: 5,//地图 缩放 3-18
            zooms: [4, 18], //地图缩放的级别 3 - 18
            lang: "zh_cn", //设置地图语言类型，默认：中文简体
            complete: null, //完成的 回调 事件
            zoomchange: null//缩放改变的时候 回调函数
        }, options);

        this.init();
    }
    Map.prototype = {
        //创建标记
        createMarker: function (params) {
            var map = this.map;
            var param = $.extend({
                map: map, //将点添加到地图
                position: null, //位置
                content: "" //内容
            }, params);
            return new AMap.Marker(param);
        },
        //创建信息窗口
        createInfoWindow: function (params) {
            var map = this.map;
            var param = $.extend({
                /* offset: new AMap.Pixel(2, -33),
                 size: new AMap.Size(300, 22),*/
                content: "" //内容
            }, params);
            return new AMap.InfoWindow(param);
        },
        setZoom: function (zoom) {
            this.map.setZoom(zoom);
        },
        getZoom: function (zoom) {
            return this.map.getZoom();
        },
        setCenter: function (position) {
            this.map.setZoom(position);
        },
        getMap: function () {
            return this.map;
        },
        setFitView: function () {
            this.map.setFitView();
        },
        createSatelliteLayer: function () {
            var self = this;
            //实时路况图层
            var satelliteLayer = this.satelliteLayer = new AMap.TileLayer.Satellite({
                zIndex: 10
            });
            satelliteLayer.setMap(self.map);
            return satelliteLayer;
        },
        createTrafficLayer: function () {
            var self = this;
            //实时路况图层
            var trafficLayer = this.trafficLayer = new AMap.TileLayer.Traffic({
                zIndex: 11
            });
            trafficLayer.setMap(self.map);
            return trafficLayer;
        },
        removeMapEle: function (obj) {
            if (_.isArray(obj)) {
                for (var i = 0; i < obj.length; i++) {
                    obj[i].setMap();
                }
            } else if (_.isObject(obj)) {
                obj.setMap();
            } else {

            }
            obj = null;
        },
        clear: function () {
            this.map.clearMap();
        },
        fullScreen: function () {
            var element = document.documentElement;
            if (element.requestFullscreen) {
                element.requestFullscreen();
            } else if (element.mozRequestFullScreen) {
                element.mozRequestFullScreen();
            } else if (element.webkitRequestFullscreen) {
                element.webkitRequestFullscreen();
            } else if (element.msRequestFullscreen) {
                element.msRequestFullscreen();
            }
        },
        exitFullscreen: function () {
            if (document.exitFullscreen) {
                document.exitFullscreen();
            } else if (document.mozCancelFullScreen) {
                document.mozCancelFullScreen();
            } else if (document.webkitExitFullscreen) {
                document.webkitExitFullscreen();
            }
        },
        //由于地图设计样式不一致，此处 绑定事件
        bindEvents: function () {
            var self = this;
            var map = self.map;
            var options = self.options;
            //此处绑定 全屏 和 退出 全屏事件
            $("#" + options.fullScreenEle).on("click", function () {
                var $btn = $(this);
                var theme = "full-screen";
                var flag = $btn.hasClass(theme);
                if (flag) { //表示 全屏，要退出 全屏
                    self.exitFullscreen();
                    $btn.removeClass(theme);
                } else {
                    self.fullScreen();
                    $btn.addClass(theme);
                }
            })
            //缩小地图
            $("#" + options.zoomOutEle).on("click", function () {
                /*var zoom = self.getZoom();
                 zoom = zoom - 1;
                 if(zoom < 3){
                 zoom = 3;
                 }
                 self.setZoom(zoom);*/
                self.map.zoomOut();
            })
            //放大地图
            $("#" + options.zoomInEle).on("click", function () {
                /*var zoom = self.getZoom();
                 zoom = zoom + 1;
                 if(zoom > 18){
                 zoom = 18;
                 }
                 self.setZoom(zoom);*/
                self.map.zoomIn();
            });
            //切换卫星
            var satelliteLayer;
            $("#" + options.satelliteEle).on("click", function () {
                var theme = "current";
                var $ele = $(this);
                if (!satelliteLayer) {
                    satelliteLayer = self.createSatelliteLayer();
                }
                if ($ele.hasClass(theme)) {
                    $ele.removeClass(theme);
                    satelliteLayer.hide();
                } else {
                    $ele.addClass(theme);
                    satelliteLayer.show();
                }
            });
            //切换 路况
            var setTrafficTime = function () {
                var time = new Date();
                var hour = time.getHours();
                var min = time.getMinutes();
                if (hour < 10) {
                    hour = "0" + hour;
                }
                if (min < 10) {
                    min = "0" + min;
                }
                $("#" + options.trafficTimeEle).text(hour + ":" + min);
            }
            var trafficLayer;
            $("#" + options.trafficEle).on("click", function () {
                var theme = "current";
                var $ele = $(this);
                if (!trafficLayer) {
                    trafficLayer = self.createTrafficLayer();
                }
                if ($ele.hasClass(theme)) {
                    $ele.removeClass(theme);
                    trafficLayer.hide();
                } else {
                    $ele.addClass(theme);
                    trafficLayer.show();
                }
                setTrafficTime();
            });
            //刷新 路况
            $("#" + options.refreshTrafficBtn).on("click", function (e) {
                trafficLayer = self.createTrafficLayer();
                trafficLayer.show();
                setTrafficTime();
                e.stopPropagation();
            });
            //加缩放事件。
            AMap.event.addListener(self.map, "zoomchange", function (e) {
                if (_.isFunction(self.options.zoomchange)) {
                    self.options.zoomchange();
                }
            });

        },
        initialize: function () {
            var self = this;
            var options = self.options;
            self.map = new AMap.Map(options.ele, options);
            self.map.on('complete', function (e) {
                if (_.isFunction(options.complete)) {
                    options.complete.call(self);
                }
            });
            self.setFitView();
        },
        //指定当前 地图的显示范围
        setInfoWindowInViewPoint: function (infoWin) {
            var self = this;
            if (this.task) {
                window.clearTimeout(this.task);
            }
            this.task = function () {
                var $navWrap = $(".nav-wrap");
                var $map = $("#map");
                var mapToolOffset = $(".map-tool").offset();
                var $arrowBtn = $("#arrow_btn");

                var left = /*$arrowBtn.offset().left + */$arrowBtn.width();
                var top = $navWrap.height();
                var right = mapToolOffset.left;
                var bottom = $map.height() - $("#time_line .tl-wrap").height();

                var map = self.map;

                var size = infoWin.getSize();
                var lngLat = infoWin.getPosition();
                var pixel = map.lngLatToContainer(lngLat);
                var arrowHeight = 20;

                var box = {
                    left: parseInt(pixel.getX()) - size.width / 2,
                    top: parseInt(pixel.getY()) - (size.height + 10) - arrowHeight,
                    width: size.width,
                    height: size.height + arrowHeight
                }
                if (infoWin.wf && infoWin.wf.toBeClose && infoWin.wf.toBeClose.qe) {
                    var $dom = $(infoWin.wf.toBeClose.qe);
                    var offset = $dom.offset();
                    box = {
                        left: offset.left,
                        top: offset.top,
                        width: $dom.outerWidth(true),
                        height: $dom.outerHeight(true)
                    }
                } else if (infoWin.jf) {
                    var $dom = $(infoWin.jf.toBeClose.yO).parent();
                    var offset = $dom.offset();
                    box = {
                        left: offset.left,
                        top: offset.top,
                        width: $dom.outerWidth(true),
                        height: $dom.outerHeight(true)
                    }
                }
                var panX = 0, panY = 0;
                if (box.left <= left) {
                    panX = left - box.left/* - $arrowBtn.offset().left*/;
                }
                if (box.left + box.width >= right) {
                    panX = right - (box.left + box.width);
                }
                if (box.top <= top) {
                    panY = top - box.top;
                }
                map.panBy(panX, panY);
            }
            this.task();

        },
        init: function () {
            this.initialize();
            this.bindEvents();
        }
    }
    return Map;
}));