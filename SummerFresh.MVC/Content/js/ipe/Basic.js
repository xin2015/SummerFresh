﻿(function (factory) {
    if (typeof define === 'function' && define.amd) {
        define([], factory);
    } else if (typeof module !== 'undefined' && module.exports) {
        module.exports = factory;
    } else {
        window.Basic = factory();
    }
}(function () {
    function Basic(options) {
        this.reset();
        this.create();
        this.setOption(options);
        //指定$el时，一定是记忆模式
        if (!this._needToCreateEl()) {
            this.setMemory(true);
        }
        this._setContainer();
        this.render();
        //this.initElement();
        this.init();
    }

    Basic.prototype = {
        constructor: Basic,
        $container: $("body"),
        container: document.body,
        $els: null,
        $el: null,
        els: null,
        //指定默认事件
        events: null,
        reset: function () {
            this.$els = {};
            this.els = {};
        },
        template: null,
        setOption: function (options) {
            var self = this;
            _.each(options, function (value, key, list) {
                self[key] = value;
            });
        },

        //记忆模式
        memory: false,
        //设置是否是记忆模式
        setMemory: function (memory) {
            this.memory = memory;
        },
        //记忆show调用次数
        _showCount: 0,
        _clearShowCount: function () {
            this._showCount = 0;
        },

        _isInContainer: function () {
            return $.contains(this.container, this.el);
        },
        //清空container
        emptyContainer: function () {
            this.$container.html("");
        },
        _setContainer: function () {
            this.container = this.$container[0];
        },
        _setEl: function () {
            this.el = this.$el[0];
        },
        _theme: null,
        //添加一个主题样式
        theme: function (theme) {
            if (theme) {
                this._theme = theme;
            }
            this._theme && this.$el.addClass(this._theme);
        },
        create: function () {
            this.triggerEvent("onCreate");
        },
        onCreate: function () {

        },
        init: function () {

        },
        //渲染主要是设置$el和el、缓存元素（initElement）、添加主题
        //事件绑定建议在onRender时绑定
        //不需要拼接$el时，仅设置el
        render: function (force) {
            if (!this._needToCreateEl()) {
                this.triggerEvent("onBeforeRender");
                this._setEl();
                this.theme();
                this.initElement();
                this.triggerEvent("onRender");
            } else if (!this.$el || force) {
                force && this._removeEl();
                this.triggerEvent("onBeforeRender");
                this._clearShowCount();
                this._renderEl();
                this._setEl();
                this.theme();
                this.initElement();
                this.triggerEvent("onRender");
            }
        },
        onBeforeRender: function () {

        },
        onRender: function () {

        },
        //传入了template则意味着需要拼接$el
        //否则由options指定$el
        _needToCreateEl: function () {
            return this.template;
        },
        //拼接$el
        _renderEl: function () {
            if (this._needToCreateEl()) {
                var html = this.template(this.data);
                this._removeEl();
                this.$el = $($.parseHTML(html, document, true));
                if (this.$el.length > 1) {
                    this.$el = $("<div />").append(this.$el);
                }
            }
        },
        _removeEl: function () {
            this.$el && this.$el.remove();
            this.$el = null;
        },
        initElement: function () {

        },
        refresh: function () {
            this.triggerEvent("onBeforeRefresh");
            this._clearShowCount();
            this._renderEl();
            this.triggerEvent("onRefresh");
        },
        onBeforeRefresh: function () {

        },
        onRefresh: function () {

        },
        //显示
        //非记忆模式下二次打开时强制重新渲染
        show: function () {
            this.triggerEvent("onBeforeShow");
            if (!this.memory && this._showCount > 0) {
                this.render(true);
            }
            //将$el塞入$container
            if (!this._isInContainer()) {
                this.$container.append(this.$el);
            }
            this.$el.show();
            this._showCount++;
            this.triggerEvent("onShow");
        },
        onBeforeShow: function () {

        },
        onShow: function () {

        },
        //隐藏
        hide: function () {
            this.triggerEvent("onBeforeHide");
            this.$el.css("display", "none");
            this.triggerEvent("onHide");
        },
        onBeforeHide: function () {
        },
        onHide: function () {
        },
        onElementEvent: function (event, selector, fn) {
            if (_.isFunction(fn)) {
                this.$el.on(event, selector, $.proxy(fn, this));
            }
        },
        offElementEvent: function (event, selector, fn) {
            this.$el.off(event, selector, fn);
        },
        //添加一个回调句柄
        //主要用在覆盖组件默认事件
        on: function (event, fn) {
            this.events[event] = fn;
        },
        off: function (event) {
            this.events[event] = null;
        },
        //执行事件回调
        triggerEvent: function (event, args) {
            this.events && _.proxy(this.events[event], this, args);
            _.proxy(this[event], this, args);
        },
        destroy: function () {
            this.$el.remove();
        },
        own: function (key, value) {
            if (!(key in this) || this.hasOwnProperty(key)) {
                this[key] = value;
            }
        },
        getProperty: function (key) {
            return this[key];
        }
    };
    return Basic;
}));