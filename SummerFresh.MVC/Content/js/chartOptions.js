var chartOptions =
{
    defaultOption: function (container ) {
        return {
            title: { text: container.attr("ChartTitle") },
            subtitle: { text: container.attr("SubTitle") },
            xAxis: {
                categories: [],
                tickInterval: parseFloat(container.attr("XAxisTickInterval")),
            },
            series: [],
            credits: {enabled:false},
        };
    },

    nonTitleOption: function (container)
    {
        return {
            title:{text:""},
            xAxis: { categories: [] },
            series: [],
            credits: { enabled: false },
        };
    },     

};

var echartOptions = {

    defaultOption: function (container)
    {
        return {
            backgroundColor: '#eee',
            title: {
                text: container.attr("ChartTitle"),//填值
                subtext: container.attr("ChartSubTitle"),//填值
                left: 'center',
                textStyle: {
                    color: '#000'
                }
            },
            tooltip: {
                trigger: 'item',
                formatter: function (para)
                {
                    //填值
                    //eval("(" + container.attr("TipFormatter") + ")");
                    var str = container.attr("TipFormatter");
                    if (str == "")
                        return "";
                    else
                        return eval("(" + container.attr("TipFormatter") + ")");
                }
            },
            legend: {
                orient: 'vertical',
                y: 'bottom',
                x: 'right',
                data: [],//填值
                textStyle: {
                    color: '#000'
                }
            },
            geo: {
                map: container.attr("MapName"),//填值
                roam: container.attr("IsZoom")=="true",//填值
                itemStyle: {
                    normal: {
                        areaColor: '#eee',
                        borderColor: '#111'
                    },
                    emphasis: {
                        areaColor: '#aaa'
                    }
                },
                show: container.attr("EChartType")!="Map",
            },
            visualMap: {
                type: 'piecewise',
                pieces: [
                    { min: 0, max: 50 },
                    { min: 51, max: 100 },
                    { min: 101, max: 150 },
                    { min: 151, max: 200 },
                    { min: 201, max: 300 },
                    { min: 300 },
                ],
                color: ['#00e400', '#ffff00', '#ff7e00', '#ff0000', '#99004c', '#7e0023'],
                textStyle: {
                    color: '#fff'
                },
                show: true,
            },
            series: [
                {
                    name: '',//填值
                    type: 'scatter',
                    coordinateSystem: 'geo',
                    symbolSize: 12,
                    label: {
                        normal: {
                            formatter: '{b}',
                            position: 'right',
                            show: true
                        }
                    },
                    //itemStyle: {
                    //    normal: {
                    //        color: ''//填值
                    //    }
                    //}
                },
                {
                    name: '',//填值
                    type:'map',
                    mapType: container.attr("MapName"),//填值
                    roam: container.attr("IsZoom") == "true",//填值
                    label: {
                        normal: {
                            formatter: '{b}',
                            position: 'right',
                            show: true
                        }
                    },
                    selectedMode: 'single',
                    //itemStyle: {
                    //    normal: {
                    //        color: ''//填值
                    //    }
                    //}
                 },
            ],
        };
    },

    deepClone:function(obj)
    {
        var newO = {};

        if (obj instanceof Array) {
            newO = [];
        }
        for (var key in obj) {
            var val = obj[key];
            newO[key] = typeof val === 'object' ? arguments.callee(val) : val;
        }
        return newO;
    },
};