
//页面初始加载
$(function () {
    ser(1, 8);
    serMap("", 5, 0, "");

    //    $("#ttt_mid").click(function () {
    //        $("#div_zhibiao").removeClass("current");
    //    })

})

//页面搜索(该方法必须存在)
function solve() {
    clearMarkerAndWindow();
    $("#idnostr").val("");
    ser(1, 8);
    serMap("", 6, 1, "");
}



//列表处理（也索取条件跟地图数据请求一致）
function ser(pageindex, pagesize) {

    //条件
    var type = $("#hidType").val();
    var key = $("#txtkey").val();
    var province = $("#province").find("option:selected").val();
    var city = $("#city").find("option:selected").val();



    var pollution = $("input[name='pollution']:checked").val();
    var enterprisids = "";
    $("input[name='enterprise']").each(function () {
        if ($(this).is(':checked')) {
            if (enterprisids != "") {
                enterprisids = enterprisids + "/";
            }
            enterprisids = enterprisids + $(this).val();
        }
    })

    var standardids = "";
    $("input[name='standard']").each(function () {
        if ($(this).is(':checked')) {
            if (standardids != "") {
                standardids += "/";
            }
            standardids += $(this).val();
        }
    })

    var feedbacks = "";
    $("input[name='feedback']").each(function () {
        if ($(this).is(':checked')) {
            if (feedbacks != "") {
                feedbacks += "/";
            }
            feedbacks += $(this).val();
        }
    })
    //请求
    $.post('../data_ashx/GetAirData.ashx',
    {
        headers: { 'Cookie': document.cookie }, cmd: 'getpollution_fenye',
        pageindex: pageindex,
        pagesize: pagesize,
        province: province,
        city: city,
        key: key,
        pollution: pollution,
        enterprisids: enterprisids,
        standardids: standardids,
        feedbacks: feedbacks,
        type: type
    },
     function (data) {
         var jsonData = eval('(' + data + ')');
         if (jsonData.isSuccess == '200') {
             window.location.href = window.location.href;
         }
         else if (jsonData.isSuccess == '1') {

             if (jsonData.Content != "") {

                 $("#table_con").html(unescape(jsonData.Content));
             }
             else {
                 $("#table_con").html("");
             }
             if (jsonData.Page != "") {
                 $("#idpage").html(unescape(jsonData.Page));
             }
             else {
                 $("#idpage").html("");
             }
             if (jsonData.TJ != "") {
                 $("#div_tongji").html(unescape(jsonData.TJ));
             }
             else {
                 $("#div_tongji").html("");
             }
         }
         else {
             $("#table_con").html("数据请求失败！");

         }
     });


}


function dotimer(time) {
    clearMarkerAndWindow();
    $("#idnostr").val("");
    var mapprovince = $("#timer_province").val();
    var level = $("#timer_level").val();
    var issearch = $("#timer_issearch").val();
    serMap(mapprovince, level, issearch, time);
}



//地图数据加载
function serMap(mapprovince, level, issearch, time) {

    //留存值
    $("#timer_province").val(mapprovince);
    $("#timer_level").val(level);
    $("#timer_issearch").val(issearch);


    var zoomchange_v = $("#zoomchange").val();
    var type = $("#hidType").val();
    //条件
    var key = $("#txtkey").val();
    var province = $("#province").find("option:selected").val();
    var city = $("#city").find("option:selected").val();

    var nostr = $("#idnostr").val();

    var pollution = $("input[name='pollution']:checked").val();
    var enterprisids = "";
    $("input[name='enterprise']").each(function () {
        if ($(this).is(':checked')) {
            if (enterprisids != "") {
                enterprisids = enterprisids + "/";
            }
            enterprisids = enterprisids + $(this).val();
        }
    })

    var standardids = "";
    $("input[name='standard']").each(function () {
        if ($(this).is(':checked')) {
            if (standardids != "") {
                standardids += "/";
            }
            standardids += $(this).val();
        }
    })

    var feedbacks = "";
    $("input[name='feedback']").each(function () {
        if ($(this).is(':checked')) {
            if (feedbacks != "") {
                feedbacks += "/";
            }
            feedbacks += $(this).val();
        }
    })
    //请求下层
    $.post('../data_ashx/GetAirData.ashx',
    {
        headers: { 'Cookie': document.cookie }, cmd: 'getpollution_totalmap',

        province: province,
        city: city,
        key: key,
        pollution: pollution,
        enterprisids: enterprisids,
        standardids: standardids,
        feedbacks: feedbacks,
        type: type,
        mapprovince: mapprovince,
        level: level,
        nostr: nostr,
        time: time,
        issearch: issearch
    },
     function (jsonData) {
         var obj = eval("(" + jsonData + ")");
         if (obj.IsSuccess == '200') {
             window.location.href = window.location.href;
         }
         else if (obj.IsSuccess == '1') {
             var data = obj.Data;
             for (var i = 0; i < data.length; i++) {

                 //                 var content = "<div class='marker-level level" + classNameIndex + "'>" + show + "<i class='icon icon-feedback-samll'></i><em></em></div>";
                 var content = "";
                 var indexid = 0;
                 if (type == 1) {
                     content = "<div onclick=\"getInfoWindow(" + data[i][0] + ",0,'" + time + "');\" class='iconszf g_" + data[i][8] + "_" + data[i][10] + "'>" + data[i][5] + "</div>";

                 }
                 else {
                     content = "<div onclick=\"getInfoWindow(" + data[i][0] + ",10000,'" + time + "');\" class='iconszf w_" + data[i][8] + "_" + data[i][10] + "'>" + data[i][5] + "</div>";

                 }
                 CreatePoint(data[i][2], data[i][1], content, " <div id=\"info_" + data[i][0] + "\" class=\"mark-detail\"></div>", data.length, data[i][0]);




             }
             $("#idnostr").val(obj.nostr);
             if (issearch == 1) {
                 setVV();
             }

         }
         else {

         }
     });


}



//工具类辅助方法
//打开内容页

function OpenDetail(industryid, indexid) {


    $.post('../data_ashx/GetAirData.ashx',
    {
        headers: { 'Cookie': document.cookie }, cmd: 'getpollution_content',
        IndustryId: industryid,
        IndexId: indexid

    },
     function (data) {
         var jsonData = eval('(' + data + ')');
         if (jsonData.isSuccess == '1') {

             if (jsonData.Content != "") {

                 $("#leftdiv_content").html(unescape(jsonData.Content));


                 //下拉
                 $(".city-select .city-select-column").on("click", function (e) {
                     var theme = "current";
                     var $p = $(this).parent();

                     $p.toggleClass(theme);
                 })
                 //点击下拉框
                 $(".city-select li").on("click", function (e) {
                     var theme = "current";
                     var $p = $(this).parents(".city-select");
                     var text = $(this).find("span").text();
                     var vv = $(this).find("span").attr("vv");
                     var $link = $p.find(".city-select-column span");
                     $link.text(text);
                     $link.attr("vv", vv);
                     $p.removeClass(theme);
                     //设置 切换
                     //self.setSwitch();
                 })



                 //日历
                 $("input.date-input").each(function () {
                     var $ele = $(this);
                     var val = $.trim($ele.val());
                     var option = {
                         defaultDate: new Date(val)
                     };
                     if (!val) {
                         var date = new Date();
                         var time = _.formatDate(date);
                         $ele.val(time.yymmdd_en);
                         option = {
                             defaultDate: date
                         };
                     }
                     $.extend(option, $.datepicker.regional["zh"]);
                     $ele.datepicker(option);
                 })

                 //柱状图
                 getHistory();

                 //柱状图切换
                 $("#switch a").on("click", function (e) {
                     setSwitch($(this).index());
                 })

                 //时间点击事件
                 $("#end_date").change(function () {
                     getHistory();
                 })

                 $("#start_date").change(function () {
                     getHistory();
                 })



                 $("#arrow_btn").on("click", function () {
                     var $btn = $(this);
                     var theme = "arrow-right";
                     toggleSidePanel($btn.hasClass(theme));
                 })
                 //表格 面板伸缩 事件
                 $("#side_info_btn_content").on("click", function () {
                     var $btn = $(this);
                     var theme = "arrow-right";
                     toggleSideInfo($btn.hasClass(theme));
                 })
                 //移进 移除 表格
                 $("#side_info_content").on("mouseenter", _.throttle(function () {
                     toggleSideInfo(true);
                 }, 50)).on("mouseleave", _.throttle(function () {
                     toggleSideInfo(false);
                 }, 50));



             }
             else {
                 $("#leftdiv_content").html("");
             }

         }
         else {
             $("#table_con").html("数据请求失败！");

         }
     });
    $("#leftdiv_content").show();
    $("#leftdiv_list").hide();
}

//回到列表页
function openlist() {
    $("#leftdiv_content").hide();
    $("#leftdiv_list").show();
}

//弹窗
function getInfoWindow(id, indexid, time) {

    $.post('../data_ashx/GetAirData.ashx',
    {
        headers: { 'Cookie': document.cookie }, cmd: 'getpollution_InfoWindow',
        IndustryId: id,
        IndexId: indexid,
        time: time

    },
     function (data) {
         var jsonData = eval('(' + data + ')');
         if (jsonData.isSuccess == '1') {

             if (jsonData.Content != "") {

                 $("#info_" + id).html(unescape(jsonData.Content));
             }
             else {
                 $("#info_" + id).html("");
             }

         }
         else {
             $("#table_con").html("数据请求失败！");

         }
     });

}

function changeZB() {
    setSwitch(0);
    getHistory();
}
//根据监测点搜索指标
function serZhiBiao(mid, indexid) {
    $("#div_zhibiao").removeClass("current");
    $.post('../data_ashx/GetAirData.ashx',
    {
        headers: { 'Cookie': document.cookie }, cmd: 'getzhibiaobymid',
        mid: mid,
        indexid: indexid
    },
     function (data) {
         var jsonData = eval('(' + data + ')');
         if (jsonData.isSuccess == '1') {

             if (jsonData.Content != "") {

                 $("#div_zhibiao").html(unescape(jsonData.Content));

                 //下拉
                 $("#div_zhibiao  .city-select-column").on("click", function (e) {
                     var theme = "current";
                     var $p = $(this).parent();
                     $p.toggleClass(theme);
                 })
                 //点击下拉框
                 $("#div_zhibiao  li").on("click", function (e) {
                     var theme = "current";
                     var $p = $(this).parents(".city-select");
                     var text = $(this).find("span").text();
                     var vv = $(this).find("span").attr("vv");

                     var $link = $p.find(".city-select-column span");
                     $link.text(text);
                     $link.attr("vv", vv);
                     $p.removeClass(theme);
                     //设置 切换
                     //self.setSwitch();
                 })
                 setSwitch(0);


             }
             else {
                 $("#div_zhibiao").html("");
             }

         }
         else {
             $("#table_con").html("数据请求失败！");

         }
     });



}


//获取历史数据
function getHistory() {

    $("#div_jiancedian").removeClass("current");
    //    var obj = {
    //        data: ['1', '2', '3'],
    //        categories: ['2016-6-28 00:00:00', '2016-6-28 01:00:00', '2016-6-28 02:00:00']
    //    };
    var v = $("#switch .current").index();
    var indexid = $("#ttt_iid").attr("vv");
    var mid = $("#ttt_mid").attr("vv");
    var starttime = $("#start_date").val();
    var endtime = $("#end_date").val();

    $.post('../data_ashx/GetAirData.ashx',
    {
        headers: { 'Cookie': document.cookie }, cmd: 'getpollutionhistory',
        mid: mid,
        indexid: indexid,
        type: v,
        starttime: starttime,
        endtime: endtime
    },
     function (data) {
         var jsonData = eval('(' + data + ')');
         if (jsonData.S == '1') {



             var data = [];
             var categories = [];

             for (var i = jsonData.data.length - 1; i >= 0; i--) {
                 data.push(jsonData.data[i]);
                 categories.push(jsonData.ser[i]);
             }
             var obj = {
                 data: data.reverse(),
                 categories: categories.reverse()
             }

             var bz = parseInt(jsonData.BZ);

             setChartData_PL(v, obj, bz);


         }
         else {

         }
     });

}



function toggleSidePanel(flag) {
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
}


function toggleSideInfo(flag) {
    var self = this;
    var $panel = $("#side_info_content");
    var $btn = $("#side_info_btn_content");
    var height = $panel.outerHeight(true);
    var top = $panel.position().top;
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
    param.top = top;
    $panel.find(".side-an-info").css(param);
}
//设置 切换
function setSwitch(index) {
    var theme = "current";
    var $btn = $("#switch a").eq(index);
    $btn.addClass(theme).siblings().removeClass(theme);
    //刷新图表 数据

    var lastTime = $("#ttt_iid").attr("ll");

    if (_.isUndefined(index)) {
        index = $("#switch a.current").index();
    }


    if (index == 0) {
        var sb = GetDateStr_d(-1, lastTime);

        $("#start_date").val(sb);
    }
    else if (index == 1) {
        var sb = GetDateStr_d(-30, lastTime);

        $("#start_date").val(sb);
    }
    getHistory();
}
function GetDateStr_d(AddDayCount, dt) {
    var dd = new Date(dt);
    dd.setDate(dd.getDate() + AddDayCount); //获取AddDayCount天后的日期
    var y = dd.getFullYear();
    var m = dd.getMonth() + 1; //获取当前月份的日期
    var d = dd.getDate();
    return y + "-" + m + "-" + d;
}
function GetDateStr(AddDayCount) {
    var dd = new Date();
    dd.setDate(dd.getDate() + AddDayCount); //获取AddDayCount天后的日期
    var y = dd.getFullYear();
    var m = dd.getMonth() + 1; //获取当前月份的日期
    var d = dd.getDate();
    return y + "-" + m + "-" + d;
}
////假设数据
//function getChartData(way) {// hour day

//    if (way == "hour") {
//        //data 纵坐标   categories 横坐标
//        return {
//            data: ['1', '2', '3'],
//            categories: ['2016-6-28 00:00:00', '2016-6-28 01:00:00', '2016-6-28 02:00:00']
//        }
//    }
//    else if (way == "day") {
//        //data 纵坐标   categories 横坐标
//        return {
//            data: ['1', '2', '3'],
//            categories: ['2016-6-28 00:00:00', '2016-6-28 01:00:00', '2016-6-28 02:00:00']
//        }

//    }
//    else {
//        return {
//            data: [],
//            categories: []
//        }

//    }
//}


//function getChartDataByMonth() {

//    var data = [];
//    data.push({
//        name: '优',
//        type: 'bar',
//        stack: '总量',
//        label: {
//            normal: {
//                show: false,
//                position: 'insideRight'
//            }
//        },
//        data: [1, 2, 1, 1],
//        itemStyle: {
//            normal: {
//                color: null
//            }
//        }
//    });
//    data.push({
//        name: '良',
//        type: 'bar',
//        stack: '总量',
//        label: {
//            normal: {
//                show: false,
//                position: 'insideRight'
//            }
//        },
//        data: [1, 2, 1, 1],
//        itemStyle: {
//            normal: {
//                color: null
//            }
//        }
//    });
//    data.push({
//        name: '轻度',
//        type: 'bar',
//        stack: '总量',
//        label: {
//            normal: {
//                show: false,
//                position: 'insideRight'
//            }
//        },
//        data: [1, 2, 1, 1],
//        itemStyle: {
//            normal: {
//                color: null
//            }
//        }
//    });
//    return {
//        data: data.reverse(),
//        categories: ['2016-6-28 00:00:00', '2016-6-28 01:00:00', '2016-6-28 02:00:00']
//    }



//}
