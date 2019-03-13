﻿/// <reference path="underscore.mixin.js" />
(function(factory){
    if (typeof define === 'function' && define.amd) {
        define([],factory);
    } else if (typeof module !== 'undefined' && module.exports) {
        module.exports = factory;
    } else {
        factory();
    }
}(function(){
    var ua = navigator.userAgent;

    var ieReg = /; MSIE (\d+)\./;
    _.mixin({
        //对象继承
        inherit: function (parent, protoProps, staticProps) {
            //var parent = this;
            var child;

            // The constructor function for the new subclass is either defined by you
            // (the "constructor" property in your `extend` definition), or defaulted
            // by us to simply call the parent's constructor.
            if (protoProps && _.has(protoProps, 'constructor')) {
                child = protoProps.constructor;
            } else {
                child = function () {
                    return parent.apply(this, arguments);
                };
            }

            // Add static properties to the constructor function, if supplied.
            _.extend(child, parent, staticProps);

            // Set the prototype chain to inherit from `parent`, without calling
            // `parent`'s constructor function.
            var Surrogate = function () {
                this.constructor = child;
            };
            Surrogate.prototype = parent.prototype;
            child.prototype = new Surrogate;

            // Add prototype properties (instance properties) to the subclass,
            // if supplied.
            if (protoProps) _.extend(child.prototype, protoProps);

            // Set a convenience property in case the parent's prototype is needed
            // later.
            child.__super__ = parent.prototype;

            return child;
        },
        proxy: function (func, context, args) {
            var fn;
            if (typeof func == "function") {
                fn = _.bind(func, context, args);
                return fn();
            }
        },
        getQueryString: function (param, url) {
            var reg = new RegExp("(^|&)" + param + "=([^&]*)(&|$)", "i");
            url = url || window.location.search;
            var r = url.substr(1).match(reg);
            if (r != null) return r[2];
            return null;
        },
        getTemplateById: function (templateID) {
            return _.template($("#" + templateID).html());
        },
        getTemplateByHtml: function (html) {
            return _.template(html);
        },
        renderTemplate: function (tplFn, data) {
            //var tplFn = _.getTemplateById(templateID);
            return tplFn(data);
        },
        //removeParam:function(url,param){
        //    var value = _.getQueryString(param);
        //    var reg = new RegExp("(^|&)" + param + "=([^&]*)(&|$)", "i");
        //    url = url.replace(reg,"");
        //    return url;
        //},
        replaceParam: function (url, key, value) {
            var oldValue = _.getQueryString(key, url);
            var urlArr, _url;
            if (oldValue) {
                var reg = new RegExp("\\?" + key + "=" + oldValue);
                if (reg.test(url)) {
                    urlArr = url.split("?" + key + "=" + oldValue);
                    _url = urlArr[0] + "?" + key + "=" + value;
                } else {
                    urlArr = url.split("&" + key + "=" + oldValue);
                    _url = urlArr[0] + "&" + key + "=" + value;
                }
                if (urlArr[1] !== undefined) {
                    _url += urlArr[1];
                }
            } else {
                if (url.match(/\?/)) {
                    _url = url + "&" + key + "=" + value;
                } else {
                    _url = url + "?" + key + "=" + value;
                }
            }
            return _url;
        },
        //UA:navigator.userAgent,
        isPad: function () {
            return (/iPad/i).test(ua);
        },
        isIE:function(){
            return (/msie/im).test(ua);
            //return navigator.appName == "Microsoft Internet Explorer";
        },
        //获取IE版本
        ieVersion:function(){
            if(_.isIE()){
                return ieReg.exec(navigator.appVersion)[1] - 0;
            }
            return null;
        },
        //IE9以下
        isIElt9:function(){
            var version = _.ieVersion();
            return version && version < 9;
        },
        //判断是否是手持设备
        isHandheldDevice:function(){
            return (/(iPhone|iPod|iPad|ios|Android|Windows Phone|SymbianOS)/i).test(ua);
        },
        //格式 日期
        formatDate:function(date){
            var year = date.getFullYear();
            var month = (date.getMonth()+1) > 9 ? (date.getMonth()+1):"0" + (date.getMonth()+1);
            var d = date.getDate() > 9 ? date.getDate():"0" + date.getDate();
            var hour = date.getHours() > 9 ? date.getHours():"0" + date.getHours();
            var minutes = date.getMinutes() > 9 ? date.getMinutes():"0" + date.getMinutes();
            return {
                "yymmdd_en":year + "/" + month + "/" + d,
                "yymmdd":year + "-" + month + "-" + d,
                "yymmddhh":year + "-" + month + "-" + d + " " + hour,
                "yymmdd_hh00":year + "-" + month + "-" + d + " " + hour+":00",
                "mmdd":month + "-" + d,
                "yymm":year + "-" + month,
                "mmdd_zh":month + "月" + d + "日",
                "hhmm":hour + "-" + minutes,
                "yy":year,
                "hh":hour
            };
        }
    });
}));