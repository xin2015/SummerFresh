﻿; (function () {
    $.datepicker.regional["zh"] = { // Default regional settings
        closeText: "关闭", // Display text for close link
        prevText: "上一月", // Display text for previous month link
        nextText: "下一月", // Display text for next month link
        currentText: "今天", // Display text for current month link
        monthNames: ["一月", "二月", "三月", "四月", "五月", "六月",
            "七月", "八月", "九月", "十月", "十一月", "十二月"], // Names of months for drop-down and formatting
        monthNamesShort: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"], // For formatting
        dayNames: ["星期天", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"], // For formatting
        dayNamesShort: ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"], // For formatting
        dayNamesMin: ["日", "一", "二", "三", "四", "五", "六"], // Column headings for days starting at Sunday
        weekHeader: "Wk", // Column header for week of the year
        dateFormat: "yy-mm-dd", // See format options on parseDate
        firstDay: 0, // The first day of the week, Sun = 0, Mon = 1, ...
        isRTL: false, // True if right-to-left language, false if left-to-right
        showMonthAfterYear: false, // True if the year select precedes month, false for month then year
        yearSuffix: "" // Additional text to append to the year in the month headers
    };
})();