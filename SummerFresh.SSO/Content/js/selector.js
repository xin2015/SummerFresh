var param = summerFresh.getQueryString();
var values = [];
var texts = [];
var tableId = "";
(function () {
    var v = $("#" + param.id, window.parent.document).val();
    if (v && v != '') {
        values = v.split(',');
    }
    var t = $("#" + param.text, window.parent.document).val();
    if (t && t != '') {
        texts = t.split(',');
    }
})()
function initCk() {
    var cks = $("#" + tableId).find("input[type='checkbox']");
    cks.bind("click", function () {
        onCkClick(this, this.checked);
    });
    if (!(param.multiple == "true")) {
        $("#btnCheckAll").hide();
        $("#btnCancelCheckAll").hide();
        cks.each(function () {
            $(this).attr("type", "radio");
        });
    }
    else {
        cks.each(function () {
            for (var i = 0; i < values.length; i++) {
                if (values[i] == $(this).val()) {
                    $(this).attr("checked", true);
                    break;
                }
            }
        });
    }
}

function onCkClick(ck, checked) {
    if (!(param.multiple == "true")) {
        //单选
        if (checked) {
            values = [$(ck).val()];
            texts = [$(ck).attr("txt")];
        }
    }
    else {
        //多选
        if (checked) {
            if (values.indexOf($(ck).val()) == -1) {
                values.push($(ck).val());
                texts.push($(ck).attr("txt"));
            }
        }
        else {
            for (var i = 0; i < values.length; i++) {
                if (values[i] == $(ck).val()) {
                    values.splice(i, 1);
                    texts.splice(i, 1);
                    break;
                }
            }
        }
    }
}
function onOK() {
    $("#" + param.id, window.parent.document).val(values.join(','));
    $("#" + param.text, window.parent.document).val(texts.join(','));
    var onchangeFun = $("#" + param.text, window.parent.document).attr("onchange");
    if (onchangeFun && onchangeFun.length > 0)
    {
        eval("window.parent." + onchangeFun);
    }
    $("#" + param.text + "-slide", window.parent.document).hide();
}

function checkAll(chk) {
    var cks = $("#" + tableId).find("input[type='checkbox']");
    cks.each(function () {
        this.checked = chk;
        onCkClick(this, chk);
    });
}

function onClose() {
    var param = summerFresh.getQueryString();
    $("#" + param.text + "-slide", window.parent.document).hide();
}

