﻿(function (factory) {
    if (typeof define === 'function' && define.amd) {
        define(["Basic"], factory);
    } else if (typeof module !== 'undefined' && module.exports) {
        module.exports = factory;
    } else {
        window.BasicView = factory(Basic);
    }
}(function (Basic) {
    var BasicView = _.inherit(Basic, {
        $el: $("#container"),
        headerTemplate: null,
        headerTemplateData: null,
        footerTemplate: null,
        footerTemplateData: null,
        $scrollEle: null,
        pagerDefaultSizes: [15, 20, 50, 100],
        initElement: function () {
            this.$els.$header = this.$el.find("#common_header");
            this.$els.$footer = this.$el.find("#common_footer");
        },
        renderHeader: function () {
            if (this.headerTemplate) {
                var tplFn = _.getTemplateByHtml(this.headerTemplate);
                var html = _.renderTemplate(tplFn, this.headerTemplateData);
                this.$els.$header.html(html);
            }
        },
        renderFooter: function () {
            if (this.footerTemplate) {
                var tplFn = _.getTemplateByHtml(this.footerTemplate);
                var html = _.renderTemplate(tplFn, this.footerTemplateData);
                this.$els.$footer.html(html);
            }
        },
        renderScrollTop: function () {
            if (!this.$scrollEle) {
                this.$scrollEle = $('<div class="scroll-wrap"><a class="scroll-top" href="javascript:void(0);"></a></div>').appendTo("body");
            }
        },
        bindScrollTopEvent: function () {
            var self = this;
            var $link = $(self.$scrollEle).find(".scroll-top");
            $(window).bind("scroll.tb", function () {
                var $stWrap = self.$scrollEle;
                var scrooltop = $(this).scrollTop();
                if (scrooltop > 0) {
                    $stWrap.show();
                } else {
                    $stWrap.hide();
                }
            });
            $link.on("click", function (e) {
                $('html,body').stop().animate({ scrollTop: 0 }, 200);
            });
        },
        //是否是手持设备
        isHandheldDevice: function () {
            return _.isHandheldDevice();
        },
        isIE: function () {
            return _.isIE();
        },
        isIElt9: function () {
            return _.isIElt9();
        },
        //适配
        fit: function () {
            var $html = $("html");
            if (this.isHandheldDevice()) {
                $html.addClass("handheld");
            } else {
                $html.addClass("pc");
            }
            if (this.isIE()) {
                $html.addClass("ie");
            }
            if (this.isIElt9()) {
                $html.addClass("ielt9");
            }
        },
        bindHeaderEvents: function () {

        },
        oninit: null,
        init: function () {
            this.renderHeader();
            this.renderFooter();
            this.renderScrollTop();
            this.bindScrollTopEvent();
            this.fit();
            this.bindHeaderEvents();
            this.triggerEvent("oninit");
        }
    });
    return BasicView;
}));