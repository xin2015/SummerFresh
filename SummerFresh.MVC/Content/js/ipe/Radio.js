﻿(function (factory) {
    if (typeof define === 'function' && define.amd) {
        define([], factory);
    } else if (typeof module !== 'undefined' && module.exports) {
        module.exports = factory;
    } else {
        window.Radio = factory();
    }
}(function () {
    var DEFAULTCLASSNAME = "icon";
    var UNCHECKEDCLASSNAME = "icon-radio";
    var CHECKEDCLASSNAME = "icon-radio-checked";

    var Radio = _.inherit(Basic, {
        events: {
            onchange: function ($radio) { }
        },
        onchange: null,
        getName: function () {
            return this.$el.attr("name");
        },
        getLabel: function () {
            return this.$el.closest(".radio");
        },
        _sync: function () {
            var $label = this.getLabel();
            var $icon = $label.find("." + DEFAULTCLASSNAME);

            $icon.attr("class", DEFAULTCLASSNAME + " " + UNCHECKEDCLASSNAME);
            if (this.el.checked) {
                $icon.attr("class", DEFAULTCLASSNAME + " " + CHECKEDCLASSNAME);
            }
            if (this.el.disabled || this.el.readonly) {
                $icon.attr("class", DEFAULTCLASSNAME + " " + UNCHECKEDCLASSNAME);
            }
        },
        init: function () {
            var self = this;
            this._sync();
            this.$el.on("change", function (e) {
                self.triggerEvent("onchange", self.$el);
                Radio.syncRadioByName(self.getName());
            });
        }
    }, {
        ins: {},
        init: function (name, options) {
            var self = this;
            var $checkbox = $("input[type='radio'][name='" + name + "']");

            $checkbox.each(function () {
                var settings = $.extend({

                }, options, {
                    $el: $(this)
                });
                var ins = new Radio(settings);
                self.add(name, ins);
            });
        },
        has: function (name) {
            return this.ins.hasOwnProperty(name);
        },
        add: function (name, ins) {
            if (!this.has(name)) {
                this.ins[name] = [];
            }
            this.ins[name].push(ins);
        },
        syncRadioByName: function (name) {
            var ins = this.ins[name];
            _.each(ins, function (n, i) {
                n._sync();
            });
        }
    });

    window.Radio = Radio;
    return Radio;
}));
