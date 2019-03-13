/*
* Inline Form Validation Engine 1.7, jQuery plugin
* 
* Copyright(c) 2010, Cedric Dugas http://www.position-relative.net
* 
* Form validation engine allowing custom regex rules to be added. Thanks to Francois Duquette and Teddy Limousin and everyone helping me find bugs on the forum Licenced under the MIT Licence
*/

(function ($) {

    var gravity = $.fn.tipsy.autoNS;
    $.fn.validation = function (settings) {

        if ($.validationConfig) { // IS THERE A LANGUAGE LOCALISATION ?
            allRules = $.validationConfig.allRules;
        } else {
            $.validation.debug("Validation engine rules are not loaded check your external file");
        }

        settings = jQuery.extend({
            allrules: allRules,
            validationEventTriggers: "focusout",
            inlineValidation: true,
            returnIsValid: false,
            liveEvent: true,
            beforeSuccess: function () {
            },
            failure: function () {
            }
        }, settings);
        $.validation.settings = settings;


        $(this).find("[validator]").tipsy({ gravity: gravity, title: "errorInfo", trigger: 'manual' });
        //radiolist验证,checkboxlist验证，给子元素radio加验证属性
        $(this).find("[validator]").find(":radio,:checkbox").each(function () {
            if (!this.disabled) {
                $(this).attr("group", $(this).closest("[validator]").attr("id"));
                $(this).attr("validator", $(this).closest("[validator]").attr("validator"));
            }
        });

        if (settings.inlineValidation == true) { // Validating Inline ?
            if (!settings.returnIsValid) { // NEEDED FOR THE SETTING returnIsValid
                allowReturnIsvalid = false;
                if (settings.liveEvent) { // LIVE event, vast performance improvement over BIND
                    $(this).find("input[type!=checkbox][validator],input[type!=radio][validator],textarea[validator]").live(settings.validationEventTriggers, function (caller) {
                        _inlinEvent(this);
                    })
                    $(this).find("input[type=checkbox][validator],input[type=radio][validator]").live("click", function (caller) {
                        _inlinEvent(this);
                    })
                    //下拉框添加校验到改变
                    $(this).find("select[type=select-one][validator],select[type=select-multiple][validator]").live("change", function (caller) {
                        _inlinEvent(this);
                    });
                } else {
                    $(this).find("input[type!=checkbox][validator],input[type!=radio][validator],textarea[validator]").bind(settings.validationEventTriggers, function (caller) {
                        _inlinEvent(this);
                    })
                    $(this).find("input[type=checkbox][validator],input[type=radio][validator]").bind("click", function (caller) {
                        _inlinEvent(this);
                    })
                    //下拉框添加校验到改变
                    $(this).find("select[type=select-one][validator],select[type=select-multiple][validator]").bind("change", function (caller) {
                        _inlinEvent(this);
                    });
                }

                $(this).find("input[type=text][validator],textarea[validator]").each(function () {
                    var event = $(this).attr("event");
                    event = (event === false || event === 'false') ? null : (event || 'keyup');
                    if (event) {
                        $(this).bind(event, function (caller) {
                            _inlinEvent(this);
                        });
                    }
                });

                firstvalid = false;
            }

            function _inlinEvent(caller) {
                if ($(caller).attr("disabled") == 'disabled') {
                    return;
                }
                $.validation.settings = settings;
                if ($.validation.intercept == false || !$.validation.intercept) { // STOP INLINE VALIDATION THIS TIME ONLY
                    $.validation.onSubmitValid = false;
                    $.validation.loadValidation(caller);
                } else {
                    $.validation.intercept = false;
                }
            }
        }

        if (settings.returnIsValid) { // Do validation and return true or false, it bypass everything;
            return $.validation.validate(this, settings);
        }

        //        $(this).bind("submit", function (caller) {
        //            $.validation.onSubmitValid = true;
        //            if ($.validation.validate(this).isError) {
        //                settings.failure && settings.failure();
        //                return false;
        //            }
        //        })
    };

    $.validation = {
        defaultSetting: function (caller) { // NOT GENERALLY USED, NEEDED FOR THE API, DO NOT TOUCH
            if ($.validationConfig) {
                allRules = $.validationConfig.allRules;
            } else {
                $.validation.debug("Validation engine rules are not loaded check your external file");
            }
            settings = {
                allrules: allRules,
                validationEventTriggers: "blur",
                inlineValidation: true,
                returnIsValid: false,
                failure: function () {
                }
            }
            $.validation.settings = settings;
        },

        loadValidation: function (caller) { // GET VALIDATIONS TO BE EXECUTED
            var rules = new Array();
            if (!$.validation.settings)
                $.validation.defaultSetting()
            var getRules = $(caller).attr('validator');
            if (!getRules)
                return false;
            var ruleOptions = getRules.match(/\[[^\]]+(\]\]|\])/g);
            if (ruleOptions) {
                $.each(ruleOptions, function (index, value) {
                    getRules = getRules.replace(this, ("##" + index));
                });
            }

            getRules = getRules.split(",");
            $.each(getRules, function (index, value) {
                var ruleAndOption = this.split("##");
                if (ruleAndOption && ruleAndOption.length == 2) {
                    rules.push({
                        name: ruleAndOption[0],
                        options: ruleOptions[ruleAndOption[1]].replace(/^\[|\]$/g, "").split(",")
                    });
                } else {
                    rules.push({
                        name: ruleAndOption[0],
                        options: []
                    });
                }
            });

            return $.validation.validateCall(caller, rules)
        },

        validateCall: function (caller, rules) { // EXECUTE VALIDATION REQUIRED BY THE USER FOR THIS FIELD
            var promptText = "";

            //if (!$(caller).attr("id"))
            //	$.validation.debug("该字段必须设定ID属性: " + "name=" +$(caller).attr("name") + " validator=" + $(caller).attr("validator"));

            var callerName = $(caller).attr("name");
            $.validation.isError = false;
            callerType = $(caller).attr("type");

            $.each(rules, function (i, v) {
                var validator = $.validation.settings.allrules[this.name];
                if (validator) {
                    eval(validator.executor + "(caller,this)");
                } else {
                    $.validation.debug("验证器拼写有误: " + "name=" + $(caller).attr("id") + " validator=" + $(caller).attr("validator"));
                    return false;
                }
                if (promptText.length > 0) {
                    return false;
                }
            });

            groupInputHack();
            if ($.validation.isError == true) {
                $.validation.buildPrompt(caller, promptText);
            } else {
                $.validation.closePrompt(caller);
            }

            /* UNFORTUNATE RADIO AND CHECKBOX GROUP HACKS */
            /* As my validation is looping input with id's we need a hack for my validation to understand to group these inputs */
            function groupInputHack() {
                callerName = $(caller).attr("group");
                if ($("input[group='" + callerName + "']").size() > 1) { // Hack for radio/checkbox group button, the validation go the first radio/checkbox of the group
                    caller = $("input[group='" + callerName + "']");
                }
            }

            /* VALIDATION FUNCTIONS */
            function _required(caller, rule) { // VALIDATE BLANK FIELD 
                var callerType = $(caller).attr("type");
                if (caller.tagName == "TEXTAREA" || callerType == "text" || callerType == "password" || callerType == "file") {
                    if (!$.trim($(caller).val())) {
                        $.validation.isError = true;
                        promptText += _buildPromptText("该输入项必填", rule.options[0]);
                    }
                } else if (callerType == "radio" || callerType == "checkbox") {
                    callerName = $(caller).attr("group");
                    if ($("input[group='" + callerName + "']:checked").size() == 0) {
                        $.validation.isError = true;
                        if ($("input[group='" + callerName + "']").size() == 1) {
                            promptText += _buildPromptText("该选项为必选项", rule.options[0]);
                        } else {
                            promptText += _buildPromptText("必须选择一个选项", rule.options[0]);
                        }
                    }
                }
                else if (callerType == "select-one") { // added by paul@kinetek.net for select boxes, Thank you		
                    if (!$(caller).val()) {
                        $.validation.isError = true;
                        promptText += _buildPromptText("该选择项必选", rule.options[0]);
                    }
                } else if (callerType == "select-multiple") { // added by paul@kinetek.net for select boxes, Thank you	
                    if (!$(caller).find("option:selected").val()) {
                        $.validation.isError = true;
                        promptText += _buildPromptText("该选择项必选", rule.options[0]);
                    }
                }
            }

            function _customRegex(caller, rule) { // VALIDATE REGEX RULES suport custom[email[errorInfo]] or email[errorInfo]
                if (_isValueEmpty(caller)) {
                    return false;
                }

                var customRule = rule.name;
                if (customRule == "pattern") {
                    customRule = rule.options[0];
                }
                var customPT = customRule.match(/\[[^\]]+\]/g);
                if (customPT) {
                    customRule = customRule.replace(customPT[0], "");
                    customPT = customPT[0].replace(/^\[|\]$/g, "");
                }

                var pattern = $.validation.settings.allrules[customRule];
                if (!pattern) {
                    $.validation.debug("正则表达式:" + customRule + " 没有定义，请检查拼写是否正确");
                }
                pattern = eval(pattern.regex);

                if (!pattern.test($.trim($(caller).val()))) {
                    $.validation.isError = true;
                    promptText += _buildPromptText($.validation.settings.allrules[customRule].alertText, customPT);
                }
            }

            function _funcCall(caller, rule) { // VALIDATE CUSTOM FUNCTIONS OUTSIDE OF THE ENGINE SCOPE
                var funce = rule.options[0];

                var fn = window[funce];
                if (typeof (fn) === 'function') {
                    var fn_result = fn(caller);
                    if (fn_result.isError) {
                        $.validation.isError = true;
                        promptText += _buildPromptText(fn_result.errorInfo);
                    }
                }
            }

            function _confirm(caller, rule) { // VALIDATE FIELD MATCH
                var confirmField = rule.options[0];

                if ($(caller).val() != $("#" + confirmField).val()) {
                    $.validation.isError = true;
                    promptText += _buildPromptText($.validation.settings.allrules[rule.name].alertText, rule.options[1]);
                } else {
                    $.validation.closePrompt($("#" + confirmField));
                }
            }

            function _lessThan(caller, rule) {
                var callerValueType = typeof $(caller);
            }

            function _length(caller, rule) { // VALIDATE LENGTH
                if (_isValueEmpty(caller)) {
                    return false;
                }

                var minL = rule.options[0];
                var maxL = rule.options[1];
                var feildLength = $.trim($(caller).val()).length;

                if (feildLength < minL || feildLength > maxL) {
                    $.validation.isError = true;
                    promptText += _buildPromptText("输入值的长度必须在" + minL + "和" + maxL + "之间", rule.options[2]);
                }
            }

            function _range(caller, rule) {
                var min = rule.options[0];
                var max = rule.options[1];

                var callerType = $(caller).attr("type");
                if (callerType == "checkbox") {
                    var groupSize = $("input[group='" + $(caller).attr("group") + "']:checked").size();
                    if (groupSize < min || groupSize > max) {
                        $.validation.isError = true;
                        promptText += _buildPromptText("必须选择" + min + "到" + max + "选项", rule.options[2]);
                    }
                }
                else {
                    if (_isValueEmpty(caller)) {
                        return false;
                    }
                    var inputValue = parseFloat($.trim($(caller).val())) || 0;
                    if (inputValue < min || inputValue > max) {
                        $.validation.isError = true;
                        promptText += _buildPromptText("输入的值必须在" + min + "到" + max + "之间", rule.options[2]);
                    }
                }
            }

            function _buildPromptText(defaultPT, customPT) {
                return customPT ? customPT : defaultPT;
            }

            function _isValueEmpty(caller) {
                return !($(caller).val() && $.trim($(caller).val()).length > 0);
            }

            return ($.validation.isError) ? $.validation.isError : false;
        },

        showPrompt: function (caller) {
            if ($(caller).parent("td").hasClass("error")) {
                //alert($(caller).attr("errorInfo"));
            }
        },

        buildPrompt: function (caller, promptText) { // ERROR PROMPT CREATION AND DISPLAY WHEN AN ERROR OCCUR
            //$(caller).attr("errorInfo", promptText).parent("td").addClass("error");
            //alert( $(caller)[0].outerHTML) ;
            if ($(caller).data("validate_target")) {
                $(caller).data("validate_target").attr({ 'validate': true, errorInfo: promptText }); //validate_target
                $(caller).data("validate_target").tipsy({ gravity: gravity, title: "errorInfo", trigger: 'manual' });
            }
            //$(caller).parent("td").find('.tipsy-container').remove();
            if (!!$(caller).attr("group")) {
                caller = $("#" + $(caller).attr("group"));
            }
            $(caller).attr("errorInfo", promptText)//.parent("td").append("<span class='tipsy-container tipsy-error' title='" + promptText + "'></span>"); //.addClass("error");
            $(caller).removeClass("success");
            $(caller).addClass("error");

        },

        closePrompt: function (caller) { // CLOSE PROMPT WHEN ERROR CORRECTED
            if (!$.validation.settings) {
                $.validation.defaultSetting();
            }
            if ($(caller).data("validate_target")) {
                $(caller).data("validate_target").attr({ 'validate': undefined, errorInfo: '' }); //validate_target
            }
            //$(caller).parent("td").find('.tipsy-container').remove();
            if (!!$(caller).attr("group")) {
                caller = $("#" + $(caller).attr("group"));
            }
            $(caller).removeClass("error");
            $(caller).addClass("success");
            $(caller).removeAttr("errorInfo")//.parent("td").append("<span class='tipsy-container tipsy-success'></span>"); //.addClass("error");
        },

        debug: function (error) {
            if (!$("#debugMode")[0]) {
                $("body").append("<div id='debugMode'><div class='debugError'><strong>错误信息：</strong></div></div>");
            }
            $(".debugError").append("<div class='debugerror'>" + error + "</div>");
        },

        validate: function (caller) { // FORM SUBMIT VALIDATION LOOPING INLINE VALIDATION
            var stopForm = false;
            var errorInfo = "";
            $(caller).find("[validator]").not(":disabled").each(function () {
                var validationPass = $.validation.loadValidation(this);
                return (validationPass) ? stopForm = true : "";
            });
            if (stopForm) {
                $(caller).find("[errorInfo]").each(function () {
                    errorInfo += $(this).attr("errorInfo") + "\n";
                });
                //焦点在第一个上面
                $(caller).find("[errorInfo]").first().focus();
            }
            return { isError: stopForm, errorInfo: errorInfo };
        }
    }

    $.validationInit = function (jqueryObj, json4Options) {
        jqueryObj.validation(json4Options);
    }

})(jQuery);
