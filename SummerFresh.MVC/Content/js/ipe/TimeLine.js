(function (factory) {
    if (typeof define === 'function' && define.amd) {
        define([], factory);
    } else if (typeof module !== 'undefined' && module.exports) {
        module.exports = factory;
    } else {
        window.TimeLine = factory();
    }
}(function () {
    var TimeLine = _.inherit(Basic, {
        $el: null, //容器
        template: null, //模板
        data: null, //数据
        dur: 2000,  // 播放间隔时间
        timeSplit: 60 * 60 * 1000, //时间间隔，两个相隔 之前的时间 值  毫秒数，用于定时计算内部进度条宽度
        hideTimeOut: 2000, //隐藏 延迟时间
        animateTime: 500, //滑动时间
        currentIndex: 0, // 当前的 数据下标， 红色的进度条颜色 会到这一格
        slideIndex: -1, //滑动到 元素的下标
        inter: null, //组件内部使用 定时器
        preObj: null, //组件内部使用 播放的前一个 对象
        onComplete: null, //完成的回调
        render: function () {
            var self = this;
            if (this.template) {
                var tplFn = _.getTemplateByHtml(this.template);
                var html = _.renderTemplate(tplFn, { data: self.data });
                this.$el.html(html);

            } else {
                self.data = new Array(this.$el.find("li").length);
                self.currentIndex = this.$el.find("li").length - 1;
                //self.slideIndex = -1;
            }

            this.initElement();
        },
        initElement: function () {
            var self = this;

            this.$con = this.$el.find(".tl-con");
            this.$main = this.$el.find(".tl-main");
            this.$bg = this.$el.find(".tl-bg");
            this.$li = this.$el.find(".tl-events > ul > li");
            this.$process = this.$el.find(".tl-process");
            this.$processInner = this.$process.find(".tl-process-inner");
            this.$controlBtn = this.$el.find(".control-btn");
            this.$prompt = this.$el.find(".tl-prompt");
            this.$promptP = this.$prompt.find("p");


            //设置背景颜色 宽度
            this.pt = this.$main.outerHeight() - parseInt(this.$main.css("padding-top"));
            this.setBgHeight(this.pt);
            //最小 宽度
            var win = this.$main.width();
            var $ul = this.$ul = this.$main.find("ul");
            var pl = parseInt($ul.css("padding-left"));
            var pr = parseInt($ul.css("padding-right"));
            $ul.css({ "min-width": win - pl - pr });

            //进度条 宽度
            var ulWidth = $ul.outerWidth();
            this.$process.css({ width: ulWidth - pl - pr });

            self.setProcessWidth();
            //一个小时 变动一次 时间 轴 内宽度
            this.currentInter = window.setInterval(function () {
                self.currentIndex++;
                self.setProcessWidth();
                if (self.currentIndex >= self.data.length - 1) {
                    window.clearInterval(self.currentInter);
                }
            }, self.timeSplit);
            //标记位置
            this.$main.scrollLeft(0);
            this.offset = this.$li.eq(0).position().left;
            //this.setActive(this.$li.eq(this.slideIndex));

            //移动到 最 右侧

            this.scroll(this.$li.last());
            this.$con.removeClass("hover");
            this.setPlayDisabled(true);
        },
        setProcessWidth: function () {
            var self = this;
            var theme = "disabled";
            var $curLi = self.$li.eq(self.currentIndex);
            $curLi.removeClass(theme).prevAll().removeClass(theme);
            $curLi.nextAll().addClass(theme);
            self.setProcess(self.getWidthByCurrentLi($curLi));
        },
        setProcess: function (width) {
            this.$processInner.stop().animate({ width: width }, self.animateTime);
        },
        getWidthByCurrentLi: function ($li) {
            var width = $li.width();
            var index = $li.index();
            width = width * (index + 1);
            return width;
        },
        setBgHeight: function (height) {
            //this.$bg.css({height:height});
        },
        slide: function (height) {
            var self = this;
            self.$li.css({ "height": height, "line-height": height + "px" });
            self.setBgHeight(height);
        },
        //格式化 提示的文字
        formatPromptText: function (param) {
            return param;
        },
        //设置 提示的文字
        setPromptText: function (text) {
            this.$promptP.text(text);
        },
        //更新提示
        updatePrompt: function () {
            var self = this;
            var $li = self.$li.eq(self.slideIndex);
            var $span = $li.find(".tl-date");
            var text = self.formatPromptText($span.attr("data-val"));
            self.setPromptText(text);
        },
        setActive: function ($li) {
            var self = this;
            var width = $li.width();
            var index = $li.index();
            self.slideIndex = index;
            if (self.slideIndex == (self.$li.length - 1)) {
                self.setPlayDisabled(true);
            } else {
                self.setPlayDisabled(false);
            }
            var left = self.offset + width * index;
            var $span = $li.find(".tl-date");
            var text = self.formatPromptText($span.attr("data-val"));
            self.setPromptText(text);
            self.$prompt.show().stop().animate({ left: left }, self.animateTime, function () {
                if (_.isFunction(self.onactive)) {
                    self.onactive(self.slideIndex, self.data[self.slideIndex], $li);
                }
            });
            //设置下方的 红色进度条
            self.currentIndex = index;
            self.setProcessWidth();
        },
        onactive: null,
        isPlay: false,
        playComplete: false,
        setPlayDisabled: function (flag) {
            this.playComplete = flag;
            var theme = "disabled";
            if (flag) {
                this.$controlBtn.addClass(theme);
            } else {
                this.$controlBtn.removeClass(theme);
            }

        },
        //播放 滚动到 可视区域
        scroll: function ($li) {
            var scrollLeft = this.$main.scrollLeft();
            var mainWidth = this.$main.width();
            var pl = parseInt(this.$ul.css("padding-left"));
            var width = $li.width();
            var index = $li.index();
            var left = pl + this.offset + width * (index + 1);
            if (scrollLeft > left) {
                var sl = left - width * 2;
                this.$main.scrollLeft(sl);
            } else if ((mainWidth + scrollLeft) <= left) {//
                var sl = left - width * 2;
                this.$main.scrollLeft(sl);
            } else {

            }
        },
        play: function () {
            var self = this;
            if (self.playComplete) {
                return;
            }
            if (self.inter) {
                window.clearInterval(self.inter);
            }
            if (!self.data || !self.data.length) {
                return;
            }

            self.show();
            self.isPlay = true;
            var theme = "pause";
            self.$controlBtn.removeClass(theme);
            var show = function (index) {
                /*if(self.preObj){
                    self.preObj.find(".tl-prompt").hide();
                }*/
                var $curEle = self.$li.eq(index);
                self.setActive($curEle);
                self.scroll($curEle);
                //self.preObj = $curEle;
            }
            this.inter = window.setInterval(function () {
                self.slideIndex++;
                if (self.slideIndex >= self.data.length /*|| self.slideIndex > self.currentIndex*/) {
                    //self.slideIndex = 0;
                    self.pause();
                    self.setPlayDisabled(true);
                    self.triggerEvent("onplayComplete");
                    return;
                }
                show(self.slideIndex);
            }, self.dur);
            self.triggerEvent("onplay");
        },
        //回调
        onplay: null,
        onplayComplete: null,
        onpause: null,
        pause: function () {
            var self = this;
            if (self.inter) {
                window.clearInterval(self.inter);
            }
            self.isPlay = false;
            var theme = "pause";
            self.$controlBtn.addClass(theme);
            self.$prompt.stop();
            self.triggerEvent("onpause");
        },
        click: function ($li) {
            var self = this;
            self.pause();
            self.setActive($li);
            self.triggerEvent("onclick");
        },
        //单击 回调
        onclick: null,
        show: function () {
            this.$con.addClass("hover");
        },
        hide: function () {
            this.$con.removeClass("hover");
        },
        bindEvents: function () {
            //移动到上面 缓慢显示
            var self = this;
            var timeout;
            var theme = "hover";
            var hide = function () {
                if (timeout) {
                    window.clearTimeout(timeout);
                }
                if (self.isPlay) {
                    timeout = window.setTimeout(function () {
                        hide();
                    }, self.hideTimeOut);
                    return;
                }
                timeout = window.setTimeout(function () {
                    self.$con.removeClass(theme);
                    self.hide();
                }, self.hideTimeOut);
            }
            this.$con.off("mouseover").on("mouseover", function (e) {
                if (timeout) {
                    window.clearTimeout(timeout);
                }
                self.show();
            }).off("mouseout").on("mouseout", function () {
                hide();
            })
            //播放 暂停
            self.$controlBtn.off("click").on("click", function () {
                var theme = "pause";
                if (self.$controlBtn.hasClass(theme)) {
                    self.play();
                } else {
                    self.pause();
                }
            })
            //点击事件
            self.$li.off("click").on("click", function (e) {
                var $li = $(this);

                /*var theme = "disabled";
                if(!$li.hasClass(theme)){
                    self.click($li);
                }*/
                self.click($li);
            })
        },
        init: function () {
            this.bindEvents();
            if (_.isFunction(this.onComplete)) {
                this.onComplete(this);
            }
        }
    });
    return TimeLine;
}));