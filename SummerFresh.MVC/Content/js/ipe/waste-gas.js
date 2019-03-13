(function (View, Map, TimeLine) {
    var timeLineClick = false;
    //地图
    //    var mapView = new View({
    //        map:null,
    //        marker:null,
    //        infoWindow: null,
    //        //创建地图
    //        initMap:function(){
    //            this.map = new Map({
    //                ele:"map",
    //                complete:null //成功回调函数
    //            });
    //        },
    //        initMarker:function(data){
    //            var self = this;
    //            this.map.clear();
    //            this.clear();
    //            if(!data){
    //                return;
    //            }
    //            for (var i = 0; i < data.length; i++) {
    //                var o = data[i];
    //                o.stateText = self.getState(o.IndexState);
    //                var content = self.getInfoWindowMessage(o);
    //                self.createPoint(o.Lng,o.Lat , o.IndexState , Math.round(o.IndexValue) , content, data.length , o.Level + "-" + o.MC);
    //            }
    //            this.map.setFitView();
    //        },
    //        getInfoWindowMessage:function(obj) {
    //            obj.showLink = !timeLineClick;
    //            //渲染 表格数据
    //            var tplFn = _.getTemplateById("infoContentTemplate");
    //            return _.renderTemplate(tplFn,obj);
    //        },
    //        getState:function(key){
    //            var state = {
    //                "1":"优",
    //                "2":"良",
    //                "3":"轻度污染",
    //                "4":"中度污染",
    //                "5":"重度污染",
    //                "6":"严重污染",
    //                "7":"爆表"
    //            }
    //            return state[key];
    //        },
    //        createPoint:function(lng, lat, classNameIndex, show, message, pointCount, id){
    //            var self = this;
    //            var mapObj = this.map.getMap();
    //            if (lng == 0 || lat == 0) {
    //                return;
    //            }
    //            var LngLat = new AMap.LngLat(lng, lat);
    //            //添加google坐标mkr
    //            var marker = new AMap.Marker({
    //                map: mapObj, //将点添加到地图
    //                position: LngLat,
    //                content: "<div class='marker-level level"  + classNameIndex + "'>" + show + "<i class='icon icon-feedback-samll'></i><em></em></div>"
    //            });

    //            var infoWindow = new AMap.InfoWindow({
    //                offset: new AMap.Pixel(-10, -33),
    //                content: message,
    //                size: new AMap.Size(350,280)
    //            });
    //            //打开信息窗口 事件
    //            AMap.event.addListener(infoWindow, 'open', function (e) {
    //                window.setTimeout(function(){
    //                    self.map.setInfoWindowInViewPoint(e.target);
    //                },0);
    //            });
    //            AMap.event.addListener(marker, 'click', function () { //鼠标点击marker弹出自定义的信息窗体
    //                infoWindow.open(mapObj, marker.getPosition());
    //            });
    //            this.setMarkerAndWindow(id, marker, infoWindow);
    //            return marker;
    //        },
    //        setMarkerAndWindow:function(id, marker, win){
    //            this.marker[id] = marker;
    //            this.infoWindow[id] = win;
    //        },
    //        clear:function(){
    //            this.marker = {};
    //            this.infoWindow = {};
    //        },
    //        initialize:function(data){
    //            //创建地图
    //            if(!this.map) {
    //                this.initMap();
    //            }
    //            this.initMarker(data);
    //        },
    //        init:function(){
    //            this.initMap();

    //            // 生成地图 定位坐标
    //            var self = this;
    //            var data = [
    //                {
    //                    "IndexName": "aqi",
    //                    "IndexState": "1",
    //                    "IndexValue": "100",
    //                    "Lat": "39.929976",
    //                    "Level": "1",
    //                    "Lng": "116.355648",
    //                    "MC": "45",
    //                    "Name": "北京",
    //                    "SiteName":"西直门",
    //                    "Company":"华宇电子公司",
    //                    "Tm": "07日05时"
    //                },
    //                {
    //                    "IndexName": "aqi",
    //                    "IndexState": "2",
    //                    "IndexValue": "200",
    //                    "Lat": "39.829995",
    //                    "Level": "1",
    //                    "Lng": "116.385647",
    //                    "MC": "42",
    //                    "Name": "北京",
    //                    "SiteName":"前门",
    //                    "Company":"海城华宇有限公司",
    //                    "Tm": "07日06时"
    //                },
    //                {
    //                    "IndexName": "aqi",
    //                    "IndexState": "3",
    //                    "IndexValue": "300",
    //                    "Lat": "39.729976",
    //                    "Level": "1",
    //                    "Lng": "116.355648",
    //                    "MC": "45",
    //                    "Name": "北京",
    //                    "SiteName":"车公门",
    //                    "Company":"平信华宇有限公司",
    //                    "Tm": "08日08时"
    //                },
    //                {
    //                    "IndexName": "aqi",
    //                    "IndexState": "3",
    //                    "IndexValue": "400",
    //                    "Lat": "39.829976",
    //                    "Level": "1",
    //                    "Lng": "116.495645",
    //                    "MC": "45",
    //                    "Name": "北京",
    //                    "SiteName":"永定门",
    //                    "Company":"铝电华宇有限公司",
    //                    "Tm": "07日12时"
    //                },
    //                {
    //                    "IndexName": "aqi",
    //                    "IndexState": "3",
    //                    "IndexValue": "300",
    //                    "Lat": "39.759976",
    //                    "Level": "1",
    //                    "Lng": "116.355648",
    //                    "MC": "41",
    //                    "Name": "北京",
    //                    "SiteName":"海门市监测站",
    //                    "Company":"华宇铝铁有限公司",
    //                    "Tm": "07日09时"
    //                },
    //                {
    //                    "IndexName": "aqi",
    //                    "IndexState": "3",
    //                    "IndexValue": "500",
    //                    "Lat": "39.729976",
    //                    "Level": "1",
    //                    "Lng": "116.395645",
    //                    "MC": "42",
    //                    "Name": "北京",
    //                    "SiteName":"天门市政府",
    //                    "Company":"华宇天然气有限公司",
    //                    "Tm": "07日08时"
    //                },
    //                {
    //                    "IndexName": "aqi",
    //                    "IndexState": "3",
    //                    "IndexValue": "700",
    //                    "Lat": "39.729976",
    //                    "Level": "1",
    //                    "Lng": "117.494477",
    //                    "MC": "40",
    //                    "Name": "北京",
    //                    "SiteName":"耒阳市东门子站",
    //                    "Company":"华宇煤气有限公司",
    //                    "Tm": "08日08时"
    //                },
    //                {
    //                    "IndexName": "aqi",
    //                    "IndexState": "3",
    //                    "IndexValue": "600",
    //                    "Lat": "39.729976",
    //                    "Level": "1",
    //                    "Lng": "116.395643",
    //                    "MC": "53",
    //                    "Name": "北京",
    //                    "SiteName":"仓门街子站",
    //                    "Company":"天意华宇有限公司",
    //                    "Tm": "07日08时"
    //                }
    //            ]
    //            self.initMarker(data);
    //        }
    //    });

    var view = new View({
        //面板 收起 展开
        toggleSidePanel: function (flag) {
            var self = this;
            var $panel = $("#side_panel");
            var $btn = $("#arrow_btn");
            var width = $panel.width();
            var theme = "arrow-right";
            var left = 0;
            var diw = 0;
            if (flag) { //表示已经收起,要展开
                left = 0;
                diw = -width;
                $btn.removeClass(theme);
            } else {
                left = -width;
                diw = 0;
                $btn.addClass(theme);
            }
            $panel.stop().animate({ "margin-left": left });
        },
        //面板 收起 展开
        toggleSideInfo: function (flag) {
            var self = this;
            var $panel = $("#side_info");
            var $btn = $("#side_info_btn");
            var height = $panel.outerHeight(true);
            var top = $panel.offset().top;
            var theme = "arrow-right";
            var panelTheme = "current";
            if (flag) { //表示已经收起,要展开
                $btn.removeClass(theme);
                $panel.addClass(panelTheme);
            } else {
                $btn.addClass(theme);
                $panel.removeClass(panelTheme);
                height = "auto";
            }
            var param = { height: height };
            $panel.css(param);
            $panel.find(".side-an-info").css(param);
        },
        //事件 绑定
        bindEvents: function () {
            var self = this;
            //面板伸缩 事件
            $("#arrow_btn").on("click", function () {
                var $btn = $(this);
                if ($btn.hasClass("disabled")) {
                    return;
                }
                var theme = "arrow-right";
                self.toggleSidePanel($btn.hasClass(theme));
            });
            //表格 面板伸缩 事件
            $("#side_info_btn").on("click", function () {
                var $btn = $(this);
                var theme = "arrow-right";
                self.toggleSideInfo($btn.hasClass(theme));
            })
            //移进 移除 表格
            $("#side_info").on("mouseover", function () {
                self.toggleSideInfo(true);
            }).on("mouseout", function () {
                self.toggleSideInfo(false);
            })

            //条件筛选
            var $conditionWrap = $("#condition_wrap");
            $conditionWrap.find(".condition-btn").on("click", function () {
                var theme = "current";
                $conditionWrap.toggleClass(theme);
            })
            //生成 复选框 单选框
            self.createCheckbox(["enterprise", "standard", "feedback"]);
            self.createRadio(["pollution"]);

            //点击  筛选 按钮
            $(document).delegate("#filter_btn", "click", function (e) {
                var theme = "current";
                $conditionWrap.removeClass(theme);

            });
        },
        createCheckbox: function (names) {
            for (var i = 0; i < names.length; i++) {
                Checkbox.init(names[i]);
            }
        },
        createRadio: function (names) {
            for (var i = 0; i < names.length; i++) {
                Radio.init(names[i]);
            }
        },
        //播放到帧 回调
        onactive: function () {
            var vv = $(".tl-prompt-con").find("p").html();
            dotimer(vv);
        },
        //时间轴
        timeLine: null,
        createTimeLine: function () {
            var self = this;
            var $refreshBtn = $("#refresh_btn");
            var $arrowBtn = $("#arrow_btn");
            var infoLink = ".info-link";
            var attr = "disabled";
            var theme = "";
            var timeLine = self.timeLine = new TimeLine({
                $el: $("#time_line"), //容器
                // template:null, //模板 ,现在 为默认模板,null 表示已创建
                //data:data, //数据
                //dur:2000,  // 播放间隔时间,
                // timeSplit:60*60*1000 , //时间间隔，两个相隔 之前的时间 值  毫秒数，用于定时计算内部进度条宽度
                //currentIndex:self.currentIndex, // 当前的 数据下标， 红色的进度条颜色 会到这一格
                //格式化 提示的文字
                formatPromptText: function (param) {
                    return param;
                },
                onComplete: function () {
                    //返回实时数据
                    $refreshBtn.off("click").on("click", function () {
                        $(this).hide();
                        self.createTimeLine();
                        $("#arrow_btn").removeClass("disabled");
                    })
                },
                onclick: function () {
                    $refreshBtn.show();
                    $arrowBtn.addClass(attr);

                    //隐藏 面板
                    self.toggleSidePanel(false);
                    timeLineClick = true;
                },
                //播放 回调
                onplay: function () {
                    //$refreshBtn.hide();
                    timeLineClick = false;
                },
                //暂停 回调
                onpause: function () {
                    $refreshBtn.hide();
                    $arrowBtn.removeClass(attr);
                    timeLineClick = false;
                },
                //播放 完成 回调
                onplayComplete: function () {
                    $refreshBtn.hide();
                    $arrowBtn.removeClass(attr);
                    timeLineClick = false;
                },
                //播放到帧 回调
                onactive: function (index, obj, $ele) {
                    self.onactive();
                }
            })
        },
        oninit: function () {
            this.bindEvents();
            this.createTimeLine();
        }
    });
})(BasicView, Map, TimeLine);