
var tableautocompute =
{
    columnsum: function (table, cell)
    {
        var colindexArr = $(cell).attr("colindexs").split(",");
        var $trArr = $(cell).parent().children();
        var result = 0;
        var tdIndex;
        for (i = 0; i < colindexArr.length; i++)
        {
            tdIndex=colindexArr [i];
            result += parseFloat(   $($trArr[ tdIndex ]).text()  );
        }
        $(cell).text(result);
    },
    columnavg: function (table, cell)
    {
        var colindexArr = $(cell).attr("colindexs").split(",");
        var $trArr = $(cell).parent().children();
        var result = 0;
        var tdIndex;
        for (i = 0; i < colindexArr.length; i++) {
            tdIndex = colindexArr[i];
            result += parseFloat($($trArr[tdIndex]).text());
        }
        $(cell).text(result/colindexArr.length);
    },
    columnmax: function (table, cell)
    {
        var colindexArr = $(cell).attr("colindexs").split(",");
        var $trArr = $(cell).parent().children();
        var result = Number.MIN_VALUE;
        var tdIndex;
        var cur;
        for (i = 0; i < colindexArr.length; i++) {
            tdIndex = colindexArr[i];
            cur = parseFloat($($trArr[tdIndex]).text());
            if (cur > result)
                result = cur;
        }
        $(cell).text(result);
    },
    columnmin: function (table, cell)
    {
        var colindexArr = $(cell).attr("colindexs").split(",");
        var $trArr = $(cell).parent().children();
        var result = Number.MAX_VALUE;
        var tdIndex;
        var cur;
        for (i = 0; i < colindexArr.length; i++) {
            tdIndex = colindexArr[i];
            cur = parseFloat($($trArr[tdIndex]).text());
            if (cur < result)
                result = cur;
        }
        $(cell).text(result);
    }
};