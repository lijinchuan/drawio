/**

 * Main Script of Ucenter & Market WordPress Plugin

 *

 * @package   Ucenter & Market

 * @version   1.0

 * @date      2015.4.6

 * @author    Zhiyan <chinash2010@gmail.com>

 * @site      Zhiyanblog <www.zhiyanblog.com>

 * @copyright Copyright (c) 2015-2015, Zhiyan

 * @license   http://opensource.org/licenses/gpl-2.0.php GPL v2 or later

 * @link      http://www.zhiyanblog.com/wordpress-plugin-ucenter-and-market.html

**/



/* AJAX sign */

; !function (a) { "function" == typeof define && define.amd ? define(["jquery"], a) : a(jQuery) }(function (a) { a.extend(a.fn, { validate: function (b) { if (!this.length) return void (b && b.debug && window.console && console.warn("Nothing selected, can't validate, returning nothing.")); var c = a.data(this[0], "validator"); return c ? c : (this.attr("novalidate", "novalidate"), c = new a.validator(b, this[0]), a.data(this[0], "validator", c), c.settings.onsubmit && (this.validateDelegate(":submit", "click", function (b) { c.settings.submitHandler && (c.submitButton = b.target), a(b.target).hasClass("cancel") && (c.cancelSubmit = !0), void 0 !== a(b.target).attr("formnovalidate") && (c.cancelSubmit = !0) }), this.submit(function (b) { function d() { var d, e; return c.settings.submitHandler ? (c.submitButton && (d = a("<input type='hidden'/>").attr("name", c.submitButton.name).val(a(c.submitButton).val()).appendTo(c.currentForm)), e = c.settings.submitHandler.call(c, c.currentForm, b), c.submitButton && d.remove(), void 0 !== e ? e : !1) : !0 } return c.settings.debug && b.preventDefault(), c.cancelSubmit ? (c.cancelSubmit = !1, d()) : c.form() ? c.pendingRequest ? (c.formSubmitted = !0, !1) : d() : (c.focusInvalid(), !1) })), c) }, valid: function () { var b, c; return a(this[0]).is("form") ? b = this.validate().form() : (b = !0, c = a(this[0].form).validate(), this.each(function () { b = c.element(this) && b })), b }, removeAttrs: function (b) { var c = {}, d = this; return a.each(b.split(/\s/), function (a, b) { c[b] = d.attr(b), d.removeAttr(b) }), c }, rules: function (b, c) { var d, e, f, g, h, i, j = this[0]; if (b) switch (d = a.data(j.form, "validator").settings, e = d.rules, f = a.validator.staticRules(j), b) { case "add": a.extend(f, a.validator.normalizeRule(c)), delete f.messages, e[j.name] = f, c.messages && (d.messages[j.name] = a.extend(d.messages[j.name], c.messages)); break; case "remove": return c ? (i = {}, a.each(c.split(/\s/), function (b, c) { i[c] = f[c], delete f[c], "required" === c && a(j).removeAttr("aria-required") }), i) : (delete e[j.name], f) }return g = a.validator.normalizeRules(a.extend({}, a.validator.classRules(j), a.validator.attributeRules(j), a.validator.dataRules(j), a.validator.staticRules(j)), j), g.required && (h = g.required, delete g.required, g = a.extend({ required: h }, g), a(j).attr("aria-required", "true")), g.remote && (h = g.remote, delete g.remote, g = a.extend(g, { remote: h })), g } }), a.extend(a.expr[":"], { blank: function (b) { return !a.trim("" + a(b).val()) }, filled: function (b) { return !!a.trim("" + a(b).val()) }, unchecked: function (b) { return !a(b).prop("checked") } }), a.validator = function (b, c) { this.settings = a.extend(!0, {}, a.validator.defaults, b), this.currentForm = c, this.init() }, a.validator.format = function (b, c) { return 1 === arguments.length ? function () { var c = a.makeArray(arguments); return c.unshift(b), a.validator.format.apply(this, c) } : (arguments.length > 2 && c.constructor !== Array && (c = a.makeArray(arguments).slice(1)), c.constructor !== Array && (c = [c]), a.each(c, function (a, c) { b = b.replace(new RegExp("\\{" + a + "\\}", "g"), function () { return c }) }), b) }, a.extend(a.validator, { defaults: { messages: {}, groups: {}, rules: {}, errorClass: "error", validClass: "valid", errorElement: "label", focusCleanup: !1, focusInvalid: !0, errorContainer: a([]), errorLabelContainer: a([]), onsubmit: !0, ignore: ":hidden", ignoreTitle: !1, onfocusin: function (a) { this.lastActive = a, this.settings.focusCleanup && (this.settings.unhighlight && this.settings.unhighlight.call(this, a, this.settings.errorClass, this.settings.validClass), this.hideThese(this.errorsFor(a))) }, onfocusout: function (a) { this.checkable(a) || !(a.name in this.submitted) && this.optional(a) || this.element(a) }, onkeyup: function (a, b) { (9 !== b.which || "" !== this.elementValue(a)) && (a.name in this.submitted || a === this.lastElement) && this.element(a) }, onclick: function (a) { a.name in this.submitted ? this.element(a) : a.parentNode.name in this.submitted && this.element(a.parentNode) }, highlight: function (b, c, d) { "radio" === b.type ? this.findByName(b.name).addClass(c).removeClass(d) : a(b).addClass(c).removeClass(d) }, unhighlight: function (b, c, d) { "radio" === b.type ? this.findByName(b.name).removeClass(c).addClass(d) : a(b).removeClass(c).addClass(d) } }, setDefaults: function (b) { a.extend(a.validator.defaults, b) }, messages: { required: "This field is required.", remote: "Please fix this field.", email: "Please enter a valid email address.", url: "Please enter a valid URL.", date: "Please enter a valid date.", dateISO: "Please enter a valid date ( ISO ).", number: "Please enter a valid number.", digits: "Please enter only digits.", creditcard: "Please enter a valid credit card number.", equalTo: "Please enter the same value again.", maxlength: a.validator.format("Please enter no more than {0} characters."), minlength: a.validator.format("Please enter at least {0} characters."), rangelength: a.validator.format("Please enter a value between {0} and {1} characters long."), range: a.validator.format("Please enter a value between {0} and {1}."), max: a.validator.format("Please enter a value less than or equal to {0}."), min: a.validator.format("Please enter a value greater than or equal to {0}.") }, autoCreateRanges: !1, prototype: { init: function () { function b(b) { var c = a.data(this[0].form, "validator"), d = "on" + b.type.replace(/^validate/, ""), e = c.settings; e[d] && !this.is(e.ignore) && e[d].call(c, this[0], b) } this.labelContainer = a(this.settings.errorLabelContainer), this.errorContext = this.labelContainer.length && this.labelContainer || a(this.currentForm), this.containers = a(this.settings.errorContainer).add(this.settings.errorLabelContainer), this.submitted = {}, this.valueCache = {}, this.pendingRequest = 0, this.pending = {}, this.invalid = {}, this.reset(); var c, d = this.groups = {}; a.each(this.settings.groups, function (b, c) { "string" == typeof c && (c = c.split(/\s/)), a.each(c, function (a, c) { d[c] = b }) }), c = this.settings.rules, a.each(c, function (b, d) { c[b] = a.validator.normalizeRule(d) }), a(this.currentForm).validateDelegate(":text, [type='password'], [type='file'], select, textarea, [type='number'], [type='search'] ,[type='tel'], [type='url'], [type='email'], [type='datetime'], [type='date'], [type='month'], [type='week'], [type='time'], [type='datetime-local'], [type='range'], [type='color'], [type='radio'], [type='checkbox']", "focusin focusout keyup", b).validateDelegate("select, option, [type='radio'], [type='checkbox']", "click", b), this.settings.invalidHandler && a(this.currentForm).bind("invalid-form.validate", this.settings.invalidHandler), a(this.currentForm).find("[required], [data-rule-required], .required").attr("aria-required", "true") }, form: function () { return this.checkForm(), a.extend(this.submitted, this.errorMap), this.invalid = a.extend({}, this.errorMap), this.valid() || a(this.currentForm).triggerHandler("invalid-form", [this]), this.showErrors(), this.valid() }, checkForm: function () { this.prepareForm(); for (var a = 0, b = this.currentElements = this.elements(); b[a]; a++)this.check(b[a]); return this.valid() }, element: function (b) { var c = this.clean(b), d = this.validationTargetFor(c), e = !0; return this.lastElement = d, void 0 === d ? delete this.invalid[c.name] : (this.prepareElement(d), this.currentElements = a(d), e = this.check(d) !== !1, e ? delete this.invalid[d.name] : this.invalid[d.name] = !0), a(b).attr("aria-invalid", !e), this.numberOfInvalids() || (this.toHide = this.toHide.add(this.containers)), this.showErrors(), e }, showErrors: function (b) { if (b) { a.extend(this.errorMap, b), this.errorList = []; for (var c in b) this.errorList.push({ message: b[c], element: this.findByName(c)[0] }); this.successList = a.grep(this.successList, function (a) { return !(a.name in b) }) } this.settings.showErrors ? this.settings.showErrors.call(this, this.errorMap, this.errorList) : this.defaultShowErrors() }, resetForm: function () { a.fn.resetForm && a(this.currentForm).resetForm(), this.submitted = {}, this.lastElement = null, this.prepareForm(), this.hideErrors(), this.elements().removeClass(this.settings.errorClass).removeData("previousValue").removeAttr("aria-invalid") }, numberOfInvalids: function () { return this.objectLength(this.invalid) }, objectLength: function (a) { var b, c = 0; for (b in a) c++; return c }, hideErrors: function () { this.hideThese(this.toHide) }, hideThese: function (a) { a.not(this.containers).text(""), this.addWrapper(a).hide() }, valid: function () { return 0 === this.size() }, size: function () { return this.errorList.length }, focusInvalid: function () { if (this.settings.focusInvalid) try { a(this.findLastActive() || this.errorList.length && this.errorList[0].element || []).filter(":visible").focus().trigger("focusin") } catch (b) { } }, findLastActive: function () { var b = this.lastActive; return b && 1 === a.grep(this.errorList, function (a) { return a.element.name === b.name }).length && b }, elements: function () { var b = this, c = {}; return a(this.currentForm).find("input, select, textarea").not(":submit, :reset, :image, [disabled], [readonly]").not(this.settings.ignore).filter(function () { return !this.name && b.settings.debug && window.console && console.error("%o has no name assigned", this), this.name in c || !b.objectLength(a(this).rules()) ? !1 : (c[this.name] = !0, !0) }) }, clean: function (b) { return a(b)[0] }, errors: function () { var b = this.settings.errorClass.split(" ").join("."); return a(this.settings.errorElement + "." + b, this.errorContext) }, reset: function () { this.successList = [], this.errorList = [], this.errorMap = {}, this.toShow = a([]), this.toHide = a([]), this.currentElements = a([]) }, prepareForm: function () { this.reset(), this.toHide = this.errors().add(this.containers) }, prepareElement: function (a) { this.reset(), this.toHide = this.errorsFor(a) }, elementValue: function (b) { var c, d = a(b), e = b.type; return "radio" === e || "checkbox" === e ? a("input[name='" + b.name + "']:checked").val() : "number" === e && "undefined" != typeof b.validity ? b.validity.badInput ? !1 : d.val() : (c = d.val(), "string" == typeof c ? c.replace(/\r/g, "") : c) }, check: function (b) { b = this.validationTargetFor(this.clean(b)); var c, d, e, f = a(b).rules(), g = a.map(f, function (a, b) { return b }).length, h = !1, i = this.elementValue(b); for (d in f) { e = { method: d, parameters: f[d] }; try { if (c = a.validator.methods[d].call(this, i, b, e.parameters), "dependency-mismatch" === c && 1 === g) { h = !0; continue } if (h = !1, "pending" === c) return void (this.toHide = this.toHide.not(this.errorsFor(b))); if (!c) return this.formatAndAdd(b, e), !1 } catch (j) { throw this.settings.debug && window.console && console.log("Exception occurred when checking element " + b.id + ", check the '" + e.method + "' method.", j), j } } if (!h) return this.objectLength(f) && this.successList.push(b), !0 }, customDataMessage: function (b, c) { return a(b).data("msg" + c.charAt(0).toUpperCase() + c.substring(1).toLowerCase()) || a(b).data("msg") }, customMessage: function (a, b) { var c = this.settings.messages[a]; return c && (c.constructor === String ? c : c[b]) }, findDefined: function () { for (var a = 0; a < arguments.length; a++)if (void 0 !== arguments[a]) return arguments[a]; return void 0 }, defaultMessage: function (b, c) { return this.findDefined(this.customMessage(b.name, c), this.customDataMessage(b, c), !this.settings.ignoreTitle && b.title || void 0, a.validator.messages[c], "<strong>Warning: No message defined for " + b.name + "</strong>") }, formatAndAdd: function (b, c) { var d = this.defaultMessage(b, c.method), e = /\$?\{(\d+)\}/g; "function" == typeof d ? d = d.call(this, c.parameters, b) : e.test(d) && (d = a.validator.format(d.replace(e, "{$1}"), c.parameters)), this.errorList.push({ message: d, element: b, method: c.method }), this.errorMap[b.name] = d, this.submitted[b.name] = d }, addWrapper: function (a) { return this.settings.wrapper && (a = a.add(a.parent(this.settings.wrapper))), a }, defaultShowErrors: function () { var a, b, c; for (a = 0; this.errorList[a]; a++)c = this.errorList[a], this.settings.highlight && this.settings.highlight.call(this, c.element, this.settings.errorClass, this.settings.validClass), this.showLabel(c.element, c.message); if (this.errorList.length && (this.toShow = this.toShow.add(this.containers)), this.settings.success) for (a = 0; this.successList[a]; a++)this.showLabel(this.successList[a]); if (this.settings.unhighlight) for (a = 0, b = this.validElements(); b[a]; a++)this.settings.unhighlight.call(this, b[a], this.settings.errorClass, this.settings.validClass); this.toHide = this.toHide.not(this.toShow), this.hideErrors(), this.addWrapper(this.toShow).show() }, validElements: function () { return this.currentElements.not(this.invalidElements()) }, invalidElements: function () { return a(this.errorList).map(function () { return this.element }) }, showLabel: function (b, c) { var d, e, f, g = this.errorsFor(b), h = this.idOrName(b), i = a(b).attr("aria-describedby"); g.length ? (g.removeClass(this.settings.validClass).addClass(this.settings.errorClass), g.html(c)) : (g = a("<" + this.settings.errorElement + ">").attr("id", h + "-error").addClass(this.settings.errorClass).html(c || ""), d = g, this.settings.wrapper && (d = g.hide().show().wrap("<" + this.settings.wrapper + "/>").parent()), this.labelContainer.length ? this.labelContainer.append(d) : this.settings.errorPlacement ? this.settings.errorPlacement(d, a(b)) : d.insertAfter(b), g.is("label") ? g.attr("for", h) : 0 === g.parents("label[for='" + h + "']").length && (f = g.attr("id").replace(/(:|\.|\[|\])/g, "\\$1"), i ? i.match(new RegExp("\\b" + f + "\\b")) || (i += " " + f) : i = f, a(b).attr("aria-describedby", i), e = this.groups[b.name], e && a.each(this.groups, function (b, c) { c === e && a("[name='" + b + "']", this.currentForm).attr("aria-describedby", g.attr("id")) }))), !c && this.settings.success && (g.text(""), "string" == typeof this.settings.success ? g.addClass(this.settings.success) : this.settings.success(g, b)), this.toShow = this.toShow.add(g) }, errorsFor: function (b) { var c = this.idOrName(b), d = a(b).attr("aria-describedby"), e = "label[for='" + c + "'], label[for='" + c + "'] *"; return d && (e = e + ", #" + d.replace(/\s+/g, ", #")), this.errors().filter(e) }, idOrName: function (a) { return this.groups[a.name] || (this.checkable(a) ? a.name : a.id || a.name) }, validationTargetFor: function (b) { return this.checkable(b) && (b = this.findByName(b.name)), a(b).not(this.settings.ignore)[0] }, checkable: function (a) { return /radio|checkbox/i.test(a.type) }, findByName: function (b) { return a(this.currentForm).find("[name='" + b + "']") }, getLength: function (b, c) { switch (c.nodeName.toLowerCase()) { case "select": return a("option:selected", c).length; case "input": if (this.checkable(c)) return this.findByName(c.name).filter(":checked").length }return b.length }, depend: function (a, b) { return this.dependTypes[typeof a] ? this.dependTypes[typeof a](a, b) : !0 }, dependTypes: { "boolean": function (a) { return a }, string: function (b, c) { return !!a(b, c.form).length }, "function": function (a, b) { return a(b) } }, optional: function (b) { var c = this.elementValue(b); return !a.validator.methods.required.call(this, c, b) && "dependency-mismatch" }, startRequest: function (a) { this.pending[a.name] || (this.pendingRequest++, this.pending[a.name] = !0) }, stopRequest: function (b, c) { this.pendingRequest--, this.pendingRequest < 0 && (this.pendingRequest = 0), delete this.pending[b.name], c && 0 === this.pendingRequest && this.formSubmitted && this.form() ? (a(this.currentForm).submit(), this.formSubmitted = !1) : !c && 0 === this.pendingRequest && this.formSubmitted && (a(this.currentForm).triggerHandler("invalid-form", [this]), this.formSubmitted = !1) }, previousValue: function (b) { return a.data(b, "previousValue") || a.data(b, "previousValue", { old: null, valid: !0, message: this.defaultMessage(b, "remote") }) } }, classRuleSettings: { required: { required: !0 }, email: { email: !0 }, url: { url: !0 }, date: { date: !0 }, dateISO: { dateISO: !0 }, number: { number: !0 }, digits: { digits: !0 }, creditcard: { creditcard: !0 } }, addClassRules: function (b, c) { b.constructor === String ? this.classRuleSettings[b] = c : a.extend(this.classRuleSettings, b) }, classRules: function (b) { var c = {}, d = a(b).attr("class"); return d && a.each(d.split(" "), function () { this in a.validator.classRuleSettings && a.extend(c, a.validator.classRuleSettings[this]) }), c }, attributeRules: function (b) { var c, d, e = {}, f = a(b), g = b.getAttribute("type"); for (c in a.validator.methods) "required" === c ? (d = b.getAttribute(c), "" === d && (d = !0), d = !!d) : d = f.attr(c), /min|max/.test(c) && (null === g || /number|range|text/.test(g)) && (d = Number(d)), d || 0 === d ? e[c] = d : g === c && "range" !== g && (e[c] = !0); return e.maxlength && /-1|2147483647|524288/.test(e.maxlength) && delete e.maxlength, e }, dataRules: function (b) { var c, d, e = {}, f = a(b); for (c in a.validator.methods) d = f.data("rule" + c.charAt(0).toUpperCase() + c.substring(1).toLowerCase()), void 0 !== d && (e[c] = d); return e }, staticRules: function (b) { var c = {}, d = a.data(b.form, "validator"); return d.settings.rules && (c = a.validator.normalizeRule(d.settings.rules[b.name]) || {}), c }, normalizeRules: function (b, c) { return a.each(b, function (d, e) { if (e === !1) return void delete b[d]; if (e.param || e.depends) { var f = !0; switch (typeof e.depends) { case "string": f = !!a(e.depends, c.form).length; break; case "function": f = e.depends.call(c, c) }f ? b[d] = void 0 !== e.param ? e.param : !0 : delete b[d] } }), a.each(b, function (d, e) { b[d] = a.isFunction(e) ? e(c) : e }), a.each(["minlength", "maxlength"], function () { b[this] && (b[this] = Number(b[this])) }), a.each(["rangelength", "range"], function () { var c; b[this] && (a.isArray(b[this]) ? b[this] = [Number(b[this][0]), Number(b[this][1])] : "string" == typeof b[this] && (c = b[this].replace(/[\[\]]/g, "").split(/[\s,]+/), b[this] = [Number(c[0]), Number(c[1])])) }), a.validator.autoCreateRanges && (null != b.min && null != b.max && (b.range = [b.min, b.max], delete b.min, delete b.max), null != b.minlength && null != b.maxlength && (b.rangelength = [b.minlength, b.maxlength], delete b.minlength, delete b.maxlength)), b }, normalizeRule: function (b) { if ("string" == typeof b) { var c = {}; a.each(b.split(/\s/), function () { c[this] = !0 }), b = c } return b }, addMethod: function (b, c, d) { a.validator.methods[b] = c, a.validator.messages[b] = void 0 !== d ? d : a.validator.messages[b], c.length < 3 && a.validator.addClassRules(b, a.validator.normalizeRule(b)) }, methods: { required: function (b, c, d) { if (!this.depend(d, c)) return "dependency-mismatch"; if ("select" === c.nodeName.toLowerCase()) { var e = a(c).val(); return e && e.length > 0 } return this.checkable(c) ? this.getLength(b, c) > 0 : a.trim(b).length > 0 }, email: function (a, b) { return this.optional(b) || /^([a-zA-Z0-9]+[_|/_|/.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|/_|/.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/.test(a) }, url: function (a, b) { return this.optional(b) || /^(https?|s?ftp):\/\/(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$/i.test(a) }, date: function (a, b) { return this.optional(b) || !/Invalid|NaN/.test(new Date(a).toString()) }, dateISO: function (a, b) { return this.optional(b) || /^\d{4}[\/\-](0?[1-9]|1[012])[\/\-](0?[1-9]|[12][0-9]|3[01])$/.test(a) }, number: function (a, b) { return this.optional(b) || /^-?(?:\d+|\d{1,3}(?:,\d{3})+)?(?:\.\d+)?$/.test(a) }, digits: function (a, b) { return this.optional(b) || /^\d+$/.test(a) }, creditcard: function (a, b) { if (this.optional(b)) return "dependency-mismatch"; if (/[^0-9 \-]+/.test(a)) return !1; var c, d, e = 0, f = 0, g = !1; if (a = a.replace(/\D/g, ""), a.length < 13 || a.length > 19) return !1; for (c = a.length - 1; c >= 0; c--)d = a.charAt(c), f = parseInt(d, 10), g && (f *= 2) > 9 && (f -= 9), e += f, g = !g; return e % 10 === 0 }, minlength: function (b, c, d) { var e = a.isArray(b) ? b.length : this.getLength(b, c); return this.optional(c) || e >= d }, maxlength: function (b, c, d) { var e = a.isArray(b) ? b.length : this.getLength(b, c); return this.optional(c) || d >= e }, rangelength: function (b, c, d) { var e = a.isArray(b) ? b.length : this.getLength(b, c); return this.optional(c) || e >= d[0] && e <= d[1] }, min: function (a, b, c) { return this.optional(b) || a >= c }, max: function (a, b, c) { return this.optional(b) || c >= a }, range: function (a, b, c) { return this.optional(b) || a >= c[0] && a <= c[1] }, equalTo: function (b, c, d) { var e = a(d); return this.settings.onfocusout && e.unbind(".validate-equalTo").bind("blur.validate-equalTo", function () { a(c).valid() }), b === e.val() }, remote: function (b, c, d) { if (this.optional(c)) return "dependency-mismatch"; var e, f, g = this.previousValue(c); return this.settings.messages[c.name] || (this.settings.messages[c.name] = {}), g.originalMessage = this.settings.messages[c.name].remote, this.settings.messages[c.name].remote = g.message, d = "string" == typeof d && { url: d } || d, g.old === b ? g.valid : (g.old = b, e = this, this.startRequest(c), f = {}, f[c.name] = b, a.ajax(a.extend(!0, { url: d, mode: "abort", port: "validate" + c.name, dataType: "json", data: f, context: e.currentForm, success: function (d) { var f, h, i, j = d === !0 || "true" === d; e.settings.messages[c.name].remote = g.originalMessage, j ? (i = e.formSubmitted, e.prepareElement(c), e.formSubmitted = i, e.successList.push(c), delete e.invalid[c.name], e.showErrors()) : (f = {}, h = d || e.defaultMessage(c, "remote"), f[c.name] = g.message = a.isFunction(h) ? h(b) : h, e.invalid[c.name] = !0, e.showErrors(f)), g.valid = j, e.stopRequest(c, j) } }, d)), "pending") } } }), a.format = function () { throw "$.format has been deprecated. Please use $.validator.format instead." }; var b, c = {}; a.ajaxPrefilter ? a.ajaxPrefilter(function (a, b, d) { var e = a.port; "abort" === a.mode && (c[e] && c[e].abort(), c[e] = d) }) : (b = a.ajax, a.ajax = function (d) { var e = ("mode" in d ? d : a.ajaxSettings).mode, f = ("port" in d ? d : a.ajaxSettings).port; return "abort" === e ? (c[f] && c[f].abort(), c[f] = b.apply(this, arguments), c[f]) : b.apply(this, arguments) }), a.extend(a.fn, { validateDelegate: function (b, c, d) { return this.bind(c, function (c) { var e = a(c.target); return e.is(b) ? d.apply(e, arguments) : void 0 }) } }) }); jQuery(document).ready(function (a) { var b = a("#sign"); a("#register-active").on("click", function () { b.removeClass("um_sign").addClass("register") }), a("#login-active").on("click", function () { b.removeClass("register").addClass("um_sign") }), a(".user-login,.user-reg").on("click", function (c) { a("div.overlay").length <= 0 ? a("body").prepend('<div class="overlay"></div>') : a("div.overlay").show(), a("body").addClass("fadeIn"), 1 == a(this).attr("data-sign") ? b.removeClass("um_sign").addClass("register") : b.removeClass("register").addClass("um_sign"), a("div.overlay, form a.close").on("click", function () { return a("body").removeClass("fadeIn"), b.removeAttr("class"), a("div.overlay").remove(), !1 }), c.preventDefault() }), a("form#login, form#register").on("submit", function (b) { return a(this).valid() ? (a("p.status", this).show().text(um.loadingmessage), "login" == a(this).attr("id") ? (action = "ajax_login", username = a("form#login #username").val(), password = a("form#login #password").val(), email = "", security = a("form#login #security").val(), remember = "checked" == a("#rememberme").attr("checked") ? a("form#login #rememberme").val() : "", um_captcha = "", a("form#login .submit").attr("disabled", !0).addClass("disabled").val("登录中...")) : "register" == a(this).attr("id") && (action = "ajax_register", username = a("#user_name").val(), password = a("#user_pass").val(), email = a("#user_email").val(), security = a("#user_security").val(), remember = "", um_captcha = a("#um_captcha").val(), a("form#register .submit").attr("disabled", !0).addClass("disabled").val("注册中...")), _this = a(this), a.ajax({ type: "POST", dataType: "json", url: um.ajax_url, data: { action: action, username: username, password: password, email: email, remember: remember, security: security, um_captcha: um_captcha }, success: function (b) { a("p.status", _this).html(b.message), 1 == b.loggedin && (document.location.href = um.redirecturl) }, complete: function () { a("form#login .submit").removeAttr("disabled").removeClass("disabled").val("登录"), a("form#register .submit").removeAttr("disabled").removeClass("disabled").val("注册") } }), b.preventDefault(), void 0) : !1 }), jQuery.validator.addMethod("isEnglish", function (a, b) { return this.optional(b) || /^[a-zA-Z][A-Za-z0-9_]+$/.test(a) }, "只允许英文开头字符可加数字"), jQuery("#login").length && jQuery("#login").validate({ rules: { username: { required: !0, minlength: 2 }, password: { required: !0, minlength: 6 } }, messages: { username: { required: "请输入用户名", minlength: a.validator.format("用户名不能少于{0}个字符") }, password: { required: "请输入密码", minlength: a.validator.format("密码不能小于{0}个字符") } } }), jQuery("#register").length && jQuery("#register").validate({ rules: { user_name: { required: !0, isEnglish: !0, minlength: 3, maxlength: 15 }, user_email: { required: !0, email: !0 }, user_pass: { required: !0, minlength: 6 }, user_pass2: { required: !0, minlength: 6, equalTo: "#user_pass" }, um_captcha: { required: !0, minlength: 4, maxlength: 4 } }, messages: { user_name: { required: "请输入英文用户名", minlength: a.validator.format("用户名不能少于{0}个字符"), maxlength: a.validator.format("用户名不能多于{0}个字符") }, um_captcha: { required: "请输入验证码", minlength: a.validator.format("验证码长度{0}个字符"), maxlength: a.validator.format("验证码长度{0}个字符") }, user_email: { required: "请输入Email地址", email: "请输入正确的email地址" }, user_pass: { required: "请输入密码", minlength: a.validator.format("密码不能小于{0}个字符") }, user_pass2: { required: "请输入确认密码", minlength: a.validator.format("确认密码不能小于{0}个字符"), equalTo: "两次输入的密码不一致" } } }) });

var um = { "ajax_url": "/api/UserAuthentication/Process", "admin_url": "", "wp_url": "", "um_url": "", "uid": 0, "is_admin": 0, "redirecturl": "", "loadingmessage": "\u6b63\u5728\u8bf7\u6c42\u4e2d\uff0c\u8bf7\u7a0d\u7b49...", "paged": 1, "cpage": 1, "timthumb": "" };

// Login check

function um_check_login() {

	if (um.uid > 0) return true;

	if ($("div.overlay").length <= 0) $("body").append('<div class="overlay"></div>');

	$("div.overlay").show(), $("body").addClass("fadeIn");

	$('#sign').removeClass("register").addClass("um_sign");

	$("div.overlay, form a.close").bind("click", function () { return $("body").removeClass("fadeIn"), $('#sign').removeAttr("class"), $("div.overlay").remove(); });

	return false;

};



// 清理百度分享多余代码

window.onload = function () {

	$('#bdshare_s').html('');

};



// Qrcode fade

$('.as-weixin,.as-donate').bind('mouseover', function () {

	$(this).children('.as-qr').css('display', 'block').stop().animate({

		bottom: 36,

		opacity: 1

	}, 500);

}).bind('mouseleave', function () {

	$(this).children('.as-qr').hide().css('bottom', 60);

});



$('.as-donate').bind('click', function () {

	if ($(this).parent().children('form#alipay-gather').length > 0) $(this).parent().children('form#alipay-gather').submit();

});



// Like action

$(".like-btn").click(function () {

	var _this = $(this);

	var pid = _this.attr('pid');

	var cookie_name = 'um_post_like_' + pid;

	var vote_cookie = umGetCookie(cookie_name);

	if (_this.hasClass('love-yes') || vote_cookie.length > 0) return;

	$.ajax({ type: 'POST', xhrFields: { withCredentials: true }, dataType: 'json', url: um.ajax_url, data: 'action=like&pid=' + pid, cache: false, success: function (data) { var num = _this.children("span").text(); _this.children("span").text(Number(num) + 1); _this.addClass("love-yes").attr("title", "已喜欢"); _this.children('i').attr('class', 'fa fa-heart'); umSetCookie(cookie_name, 1, 3600, um.wp_url); if (data.get === 1) umAlert('参与文章互动获取' + data.credit + '积分'); } });

});



// Collect action

$('.collect-btn').click(function (e) {
	e.preventDefault();
	if (!um_check_login()) return;

	var _this = $(this);

	var pid = Number(_this.attr('pid'));

	var collect = Number(_this.children("span").text());

	if (_this.attr('uid') && !_this.hasClass('collect-yes')) {

		var uid = Number(_this.attr('uid'));

		$.ajax({ type: 'POST', xhrFields: { withCredentials: true }, dataType: 'html', url: um.ajax_url, data: 'action=collect&uid=' + uid + '&pid=' + pid + '&act=add', cache: false, success: function (response) { if (response != 0) _this.children("span").text(collect + 1); _this.removeClass("collect-no").addClass("collect-yes").attr("title", "已收藏"); _this.children('i').attr('class', 'fa fa-star'); } });

		return false;

	} else if (_this.attr('uid') && _this.hasClass('collect-yes') && _this.hasClass('remove-collect')) {

		var uid = Number(_this.attr('uid'));

		$.ajax({ type: 'POST', xhrFields: { withCredentials: true }, dataType: 'html', url: um.ajax_url, data: 'action=collect&uid=' + uid + '&pid=' + pid + '&act=remove', cache: false, success: function (response) { if (response != 0) _this.children("span").text(collect - 1); _this.removeClass("collect-yes").addClass("collect-no").attr("title", "点击收藏"); _this.children('i').attr('class', 'fa fa-star-o'); } });

		return false;

	} else {

		return;

	}

});



// Close pop-up

$(".alert_close, .alert_cancel .btn").click(function () {

	$('.umAlert').fadeOut();

});



// Alert template

function umAlert($msg) {

	var $content = '<div class="umalert"><div class="alert_title"><h4>来自网页的提醒</h4></div><div class="alert_content"><p>' + $msg + '</p></div><div class="alert_cancel"><button class="cancel-to-back btn btn-danger">确定</button></div><span class="alert_close"><i class="fa fa-close"></i></span></div>';

	$('body').append($content);

	$(".alert_close, .alert_cancel .btn").bind('click', function () {

		$('.umalert').fadeOut().remove();

	})

	$('.umalert').fadeIn();

};



// Ajax post basic

var umRefreshIcon = '<i class="fa fa-spinner fa-spin" style="margin-right:4px;"></i>';

function um_do_post(formid, posturl, postdata, contentid) {

	$(formid).find('[type="submit"]').addClass('disabled').prepend(umRefreshIcon);

	$.ajax({

		type: 'POST',

		url: posturl,

		data: postdata,

		success: function (response) {

			$(contentid).html($(response).find(contentid).html());

		},

		error: function () {

			//um_do_post(formid, posturl, postdata, contentid);

		}

	});

};



//Submit

$('#pmform').submit(function () {

	var formid = '#pmform';

	var p = $(formid);

	um_do_post(

		formid,

		location.href,

		{

			'pmNonce': p.find('[name="pmNonce"]').val(),

			'pm': p.find('[name="pm"]').val()

		},

		'.dashboard-main'

	);

	return false;

});

$('#creditform').submit(function () {

	var formid = '#creditform';

	var p = $(formid);

	var obj;

	var checked;

	obj = document.getElementsByName('creditChange');

	if (obj) {

		for (var i = 0; i < obj.length; i++) {

			if (obj[i].checked) {

				checked = obj[i].getAttribute('value');

			} else { checked = 'add'; }

		}

	} else { checked = 'add'; }

	um_do_post(

		formid,

		location.href,

		{

			'creditNonce': p.find('[name="creditNonce"]').val(),

			'creditChange': checked,

			'creditNum': p.find('[name="creditNum"]').val(),

			'creditDesc': p.find('[name="creditDesc"]').val()

		},

		'.dashboard-main'

	);

	return false;

});



// Add coupon code

$('#couponform').submit(function () {

	var formid = '#couponform';

	var p = $(formid);

	var obj;

	var checked;

	obj = document.getElementsByName('coupon_type');

	if (obj) {

		for (var i = 0; i < obj.length; i++) {

			if (obj[i].checked) {

				checked = obj[i].getAttribute('value');

			} else { checked = 'once'; }

		}

	} else { checked = 'once'; }

	um_do_post(

		formid,

		location.href,

		{

			'couponNonce': p.find('[name="couponNonce"]').val(),

			'coupon_code': p.find('[name="coupon_code"]').val(),

			'coupon_type': checked,

			'discount_value': p.find('[name="discount_value"]').val(),

			'expire_date': p.find('[name="expire_date"]').val()

		},

		'.dashboard-main'

	);

	return false;

});



// Delete coupon code

$('.delete_couponcode').on('click', function () {

	var p = $(this).parent('tr').children('input[name=coupon_id]');

	var coupon_id = p.val();

	var dcouponNonce = $('.coupon-table input[name=dcouponNonce]').val();

	$.ajax({

		type: 'POST',

		url: location.href,

		data: {

			'coupon_id': coupon_id,

			'dcouponNonce': dcouponNonce

		},

		success: function (response) {

			//$('#lc').html($(response).find('#lc').html());

			p.parent('tr').remove();

		},

		error: function () {

			umAlert('删除失败,请重新再试');

		}

	});

	return false;

});



// Cookie

// function set cookie

function umSetCookie(c_name, value, expire, path) {

	var exdate = new Date();

	exdate.setTime(exdate.getTime() + expire * 1000);

	document.cookie = c_name + "=" + escape(value) + ((expire == null) ? "" : ";expires=" + exdate.toGMTString()) + ((path == null) ? "" : ";path=" + path);

};

// function get cookie

function umGetCookie(c_name) {

	if (document.cookie.length > 0) {

		c_start = document.cookie.indexOf(c_name + "=");

		if (c_start != -1) {

			c_start = c_start + c_name.length + 1;

			c_end = document.cookie.indexOf(";", c_start);

			if (c_end == -1) c_end = document.cookie.length;

			return unescape(document.cookie.substring(c_start, c_end));

		}

	}

	return ""

};

// function set wp nonce cookie

function set_um_nonce() {

	$.ajax({

		type: 'POST', url: um.ajax_url, data: { 'action': 'um_create_nonce' },

		success: function (response) {

			umSetCookie('um_check_nonce', $.trim(response), 3600, um.home);

		},

		error: function () {

			set_um_nonce();

		}

	});

};

// var get wp nonce cookie

var wpnonce = umGetCookie('um_check_nonce');

// action set wp nonce cookie ( if wp nonce is null or empty )

if (wpnonce == null || wpnonce == "") set_um_nonce();



// Get aff name in url

function umGetQueryString(name) {

	var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");

	var r = window.location.search.substr(1).match(reg);

	if (r != null) return unescape(r[2]); return null;

};



//Upload avatar

$('#edit-umavatar').click(function () {

	$('#upload-input').slideToggle();

})

$('#upload-umavatar').click(function () {

	var file = $('#upload-input input[type=file]').val();

	if (file == '') {

		$('#upload-avatar-msg').html('请选择一个图片').slideDown();

		setTimeout(function () { $('#upload-avatar-msg').html('').slideUp(); }, 2000);

	} else {

		document.getElementById('info-form').enctype = "multipart/form-data";

		$('form#info-form').submit();

	}

})





//general popup

$("[data-pop]").on("click", function () {

	var b = $(this).attr("data-pop");

	$("div.overlay").length <= 0 ? $("body").append('<div class="overlay"></div>') : $("div.overlay").show();

	$(".popupbox").hide(), $("#" + b).fadeIn();

});



$("[data-top]").on("click", function () {

	var b = $(this).attr("data-top");

	"true" == b && $("body,html").animate({

		scrollTop: 0

	}, 0)

});



$('body').on("click", "div.overlay,a.popup-close", function () {

	$("div.popupbox, div.overlay").fadeOut();

});



// amount plus or minus

function calculate() {

	$("#total-price").find("strong").text("￥" + Number($("#order_quantity").val() * $("#order_price").val()).toFixed(2))

};

$("div.amount-number a").on("click", function (b) {

	b.preventDefault(), fieldName = $(this).attr("field"), fieldstyle = $(this).attr("id");

	var c = parseInt($("input[name=" + fieldName + "]").val());

	var d = parseInt($('li.summary-amount span.dt-num').text());

	"plus" == fieldstyle ? Number(c) >= d ? $("input[name=" + fieldName + "]").val(d) : isNaN(c) ? $("input[name=" + fieldName + "]").val(0) : $("input[name=" + fieldName + "]").val(c + 1) : "minus" == fieldstyle && (!isNaN(c) && c > 1 ? $("input[name=" + fieldName + "]").val(c - 1) : $("input[name=" + fieldName + "]").val(1))

}),

	$("input[name=amountquantity]").keyup(function () {

		var c = $(this).val();

		var d = parseInt($('li.summary-amount span.dt-num').text());

		if (!/^(\+|-)?\d+$/.test(c) || 0 >= c) $(this).val(1);

		if (d < c) $(this).val(d)

	}),

	$("a.buy-btn").on("click", function (b) {

		b.preventDefault(), $("input[name=order_quantity]").val($("input[name=amountquantity]").val()), calculate()

	}),

	$("a.inner-buy-btn").on("click", function (b) {

		b.preventDefault();

		if (um_check_login()) {

			calculate();

			$("div.overlay").length <= 0 ? $("body").append('<div class="overlay"></div>') : $("div.overlay").show();

			$(".popupbox").hide(), $("#order").fadeIn();

		}

	}),

	$("#order_quantity").keyup(function () {

		var c = $(this).val();

		"" == c ? $(this).val(1) : ($("#pay-submit").removeAttr("disabled"), (!/^(\+|-)?\d+$/.test(c) || 0 >= c) && $(this).val(1)), calculate()

	});



// create a order

$('#pay-submit').on('click', function create_order() {

	var $this = $(this);

	if ($this.hasClass('disabled')) return;

	$this.addClass('disabled').prepend(umRefreshIcon);

	var product_id = $('input#product_id').val(),

		coupon_code = $('input#coupon_code').val(),

		order_name = $('input#order_name').val(),

		order_quantity = $('input#order_quantity').val(),

		receive_name = $('input#receive_name').val(),

		receive_address = $('input#receive_address').val(),

		receive_zip = $('input#receive_zip').val(),

		receive_email = $('input#receive_email').val(),

		receive_phone = $('input#receive_phone').val(),

		receive_mobile = $('input#receive_mobile').val(),

		order_msg = $('input[name=order_body]').val(),

		aff_user_id = umGetCookie('um_aff'),

		wp_nonce = $('input[name=order_nonce]').val();

	if (receive_name == '' || receive_email == '') { alert('收货人姓名或邮箱不能为空'); $this.removeClass('disabled').text('立即付款'); return false; } else if (!receive_email.match('^([a-zA-Z0-9_-])+((\.)?([a-zA-Z0-9_-])+)+@([a-zA-Z0-9_-])+(\.([a-zA-Z0-9_-])+)+$')) { alert('收货人邮箱格式不正确'); $this.removeClass('disabled').text('立即付款'); return false; }

	$.ajax({

		type: 'POST',

		dataType: 'json',

		async: false,

		url: um.ajax_url,

		data: {

			'action': 'create_order',

			'product_id': product_id,

			'coupon_code': coupon_code,

			'order_name': order_name,

			'order_quantity': order_quantity,

			'receive_name': receive_name,

			'receive_address': receive_address,

			'receive_zip': receive_zip,

			'receive_email': receive_email,

			'receive_phone': receive_phone,

			'receive_mobile': receive_mobile,

			'order_msg': order_msg,

			'aff_user_id': aff_user_id,

			'wp_nonce': wp_nonce

		},

		success: function (response) {

			//~ @action reset wp nonce ( if response invalid ) and try again

			if ($.trim(response.msg) == 'NonceIsInvalid') {

				umAlert('安全验证未通过,订单未被提交,请刷新页面再试');

			} else if (response.success == 0) {

				umAlert(response.msg);

				$this.removeClass('disabled').text('立即付款');

			} else if (response.redirect == 0) {

				location.replace(location.href);

			} else if (response.redirect == 1) {

				$('input#order_id').val(response.order_id);

				$('form#alipayment').submit();

			} else {

				$this.removeClass('disabled').text('立即付款');

				return;

			}

		},

	});

	return false;

});



// Close order

$('.close-order').bind('click', function () {

	var $this = $(this);

	var id = $this.attr('data');

	$.ajax({

		type: 'POST',

		dataType: 'json',

		async: false,

		url: um.ajax_url,

		data: {

			'action': 'closeorder',

			'id': id,

		},

		success: function (response) {

			//~ @action reset wp nonce ( if response invalid ) and try again

			if (response.success == 0) {

				umAlert(response.msg);

			} else {

				$this.parent('td').prev().text('交易关闭');

				$this.parent('td').html('');

			}

		}

	});

});



// Continue pay

$('.continue-pay').bind('click', function () {

	var $this = $(this);

	var id = $this.attr('data-id'),

		form = $('#continue-pay'),

		pid_obj = form.children('input#product_id'),

		oid_obj = form.children('input#order_id'),

		oname_obj = form.children('input#order_name');

	$.ajax({

		type: 'POST',

		dataType: 'json',

		url: um.ajax_url,

		data: {

			'action': 'continue_order',

			'id': id

		},

		success: function (response) {

			if (response.success == 1 && response.order.order_currency == 'credit') {

				$this.parent('td').html('交易成功');

				return false;

			} else if (response.success == 1 && response.order.order_currency == 'cash') {

				pid_obj.val(response.order.product_id);

				oid_obj.val(response.order.order_id);

				oname_obj.val(response.order.product_name);

				form.submit();

			} else { umAlert(response.msg); return false; }

		}

	});

});



//use coupon code

$('#coupon_code_apply').click(function () {

	var code = $('input#coupon_code').val();

	var total = Number($("#order_quantity").val() * $("#order_price").val()).toFixed(2);

	if (code == '') { return; } else {

		$.ajax({

			type: 'POST',

			dataType: 'json',

			async: false,

			url: um.ajax_url,

			data: {

				'action': 'use_coupon_code',

				'coupon_code': code,

				'order_total_price': total

			},

			success: function (response) {

				if (response.success == 1) {

					$('#coupon_code,#coupon_code_apply').css('display', 'none');

					$('#coupon').append('应用优惠码成功,请直接提交支付');

					$("#total-price").find("strong").text("￥" + response.total_price);

				} else { umAlert(response.msg); }

			},



		});

	}

});



//join vip

$('#joinvip-submit').click(function create_vip_order() {

	$('#joinvip-submit').addClass('disabled').prepend(umRefreshIcon);

	var obj;

	var product_id = -1;

	var aff_user_id = umGetCookie('um_aff'),

		obj = document.getElementsByName('product_id');

	if (obj) {

		for (var i = 0; i < obj.length; i++) {

			if (obj[i].checked) {

				product_id = obj[i].getAttribute('value');

			}

		}

	}

	$.ajax({

		type: 'POST',

		dataType: 'json',

		async: false,

		url: um.ajax_url,

		data: {

			'action': 'create_vip_order',

			'product_id': product_id,

			'aff_user_id': aff_user_id

		},

		success: function (response) {

			$('#joinvip-submit').removeClass('disabled');

			$('#joinvip-submit i').remove();

			if (response.success == 1) {

				$('input#order_id').val(response.order_id);

				$("form#joinvip").attr('onsubmit', '').submit();

			} else { umAlert(response.msg); }

		},

	});

	return false;

});



//credit recharge order

$('#creditrechargesubmit').on('click', function () {

	var obj = $('#creditrechargeform');

	obj.find('[type="submit"]').addClass('disabled').prepend(umRefreshIcon);

	var product_id = -5,

		creditrechargeNum = obj.find('input[name=creditrechargeNum]').val(),

		aff_user_id = umGetCookie('um_aff');

	$.ajax({

		type: 'POST',

		dataType: 'json',

		async: false,

		url: um.ajax_url,

		data: {

			'action': 'create_credit_recharge_order',

			'product_id': product_id,

			'creditrechargeNum': Number(creditrechargeNum),

			'aff_user_id': aff_user_id

		},

		success: function (response) {

			if (response.success == 1) {

				obj.find('input#order_id').val(response.order_id);

				obj.attr('onsubmit', '').submit();

			} else { umAlert(response.msg); }

		},

	});

	return false;

});



// Daily sign

$('a#daily_sign').bind('click', function () {

	var $this = $(this);

	$.ajax({

		type: 'POST',

		dataType: 'json',

		async: false,

		url: um.ajax_url,

		data: { 'action': 'daily_sign' },

		success: function (response) {

			if (response.success == 1) {

				$this.attr({ 'id': 'daily_signed', 'title': '今日已签到' }).text('已签到');

				if ($this.prev('a').length > 0) {

					var credit = Number($.trim($this.prev('a').text())) + Number(response.credits);

					$this.prev('a').text(' ' + credit);

				} else {

					umAlert(response.msg);

				}

			} else {

				umAlert(response.msg);

			}

		},

	});

});



// Ajax update traffic

function update_um_traffic(p) {

	$.ajax({

		type: 'POST',

		url: um.ajax_url,

		data: {

			'action': 'um_tracker_ajax',

			'pid': p,

			'wp_nonce': umGetCookie('um_check_nonce')

		},

		success: function (response) {

			//~ @action reset wp nonce ( if response invalid ) and try again

			if ($.trim(response) === 'NonceIsInvalid') {

				set_um_nonce();

				update_um_traffic(p);

			}

		},

		error: function () {

			//~ @action try again ( if error )

			//update_um_traffic(p);

		}

	});

};



// Ajax follow

$('.follow-btn').click(function () {

	if (um_check_login()) {

		var $this = $(this);

		var followed = $this.data('uid'),

			act = $this.data('act'),

			wp_nonce = umGetCookie('um_check_nonce');

		$.ajax({

			type: 'POST',

			url: um.ajax_url,

			dataType: 'json',

			data: {

				'action': 'follow',

				'followed': followed,

				'act': act,

				'wp_nonce': wp_nonce

			},

			success: function (response) {

				if ($.trim(response) === 'NonceIsInvalid') {

					set_um_nonce();

					umAlert('操作失败,请重试');

				} else if (response.success === 1) {

					switch (response.type) {

						case 1:

							$this.data('act', 'disfollow');

							$this.removeClass('unfollowed').removeClass('current').addClass('followed');

							$this.parent().children('.pm-btn').addClass('current');

							$this.html('<i class="fa fa-check"></i>已关注');

							break;

						case 2:

							$this.data('act', 'disfollow');

							$this.removeClass('unfollowed').removeClass('current').addClass('followed');

							$this.parent().children('.pm-btn').addClass('current');

							$this.html('<i class="fa fa-exchange"></i>互相关注');

							break;

						default:

							$this.data('act', 'follow');

							$this.removeClass('followed').addClass('current unfollowed');

							$this.parent().children('.pm-btn').removeClass('current');

							$this.html('<i class="fa fa-plus"></i>关 注');

					}

				} else {

					umAlert(response.msg);

				}

			},



		});

	}

});



// Avatar rotate

$("#main-wrap img.avatar").mouseover(function () {

	$(this).addClass("avatar-rotate");

});



$("#main-wrap img.avatar").mouseout(function () {

	$(this).removeClass("avatar-rotate");

});



// Withdraw

$('button#withdrawSubmit').click(function () {

	var $this = $(this);

	if ($this.hasClass('disabled')) return;

	var sum = $('input#balance').val();

	var money = $('input#withdrawNum').val();

	if (Number(money) > Number(sum)) { alert('提现金额不能大于余额'); return false; }

	$.ajax({

		type: "POST",

		dataType: 'json',

		url: um.ajax_url,

		data: { "action": "withdraw", "money": money },

		beforeSend: function () { $this.addClass('disabled').prepend(umRefreshIcon); },

		success: function (response) {

			if (response.success === 1) {

				$('#withdraw').html('<p>提现申请提交成功，请等待管理员处理，同时请完善个人信息中支付宝账号等信息以便收款</p>');

				setTimeout(function () { location.replace(location.href) }, 1000);

			} else {

				$this.removeClass('disabled').html('申请提现');

				umAlert(response.msg);

			}

		}

	});

	return false;

});



// Admin confirm payed the withdraw

$('a.confirm_payed_withdraw').click(function () {

	var $this = $(this),

		id = $this.attr('data-id');

	if ($this.hasClass('disabled')) return;

	if (!confirm('你确认已支付了该提现请求吗？')) return;

	$.ajax({

		type: "POST",

		dataType: 'json',

		url: um.ajax_url,

		data: { "action": "confirm_payed_withdraw", "id": id },

		beforeSend: function () { $this.addClass('disabled'); },

		success: function (response) {

			if (response.success === 1) {

				$this.parent('td').html('');

			} else {

				$this.removeClass('disabled');

				umAlert(response.msg);

			}

		}

	});

	return false;



});



// Refresh captcha

$('img.captcha_img').bind('click', function () {

	var captcha = um.um_url + '/template/captcha.php?' + Math.random();

	$(this).attr('src', captcha);

});



// Document ready

// -------------------- //

$(document).ready(function () {

	// follow and pm btn add class

	if ($('.fp-btns .follow-btn').hasClass('unfollowed')) { $('.fp-btns .follow-btn').addClass('current'); $('.fp-btns .pm-btn').removeClass('current'); } else { $('.fp-btns .follow-btn').removeClass('current'); $('.fp-btns .pm-btn').addClass('current'); };



	// action um affiliate url and trackback url

	$('.um_aff_url').click(function () {

		$(this).select();

	});



	// action set affiliate cookie ( credit )

	if (umGetQueryString('aff')) umSetCookie('um_aff', umGetQueryString('aff'), 86400, um.home);



	// action update traffic

	if (!(typeof (um.pid) == "undefined")) update_um_traffic(um.pid);



	// Store info area switch

	$(function () {

		var $wrapNav = $('#wrapnav ul.nav'),

			$wrapNavLis = $wrapNav.children('li');

		if ($wrapNav.length > 0) $wrapNav.each(function () {

			var $this = $(this);

			$this.children('li').first().addClass('active');

			$($this.find('a').attr('href')).show();

		});



		$wrapNavLis.on('click', function (e) {

			var $this = $(this);

			if ($this.hasClass('active')) return;

			$this.siblings().removeClass('active').end().addClass('active');

			$this.parent().parent().next().children('.wrapbox').stop(true, true).hide().siblings($this.find('a').attr('href')).fadeIn();

			e.preventDefault();

		})//.children( window.location.hash ? 'a[href=' + window.location.hash + ']' : 'a:first' ).trigger('click');



	});



	// Change cover

	$("#custom-cover").click(function (A) {

		A.preventDefault();

		A.returnValue = false;

		$("#cover-change").fadeIn(); return false

	});

	$("#cover-list ul li a.basic").click(function (A) {

		A.preventDefault();

		A.returnValue = false;

		if ($(this).hasClass("selected")) { return }

		F = $(this).children('img').attr('src');

		var C = $("#cover img"), B = F.replace('-small', '');

		C.attr("src", B);

		$("#cover-list ul li a.selected").removeClass("selected");

		$(this).addClass("selected"); return false

	});

	$("#cover-close,#cover-cancle").click(function (A) {

		A.preventDefault();

		A.returnValue = false;

		$("#cover-change").fadeOut();

		return false

	});

	$("#cover-sure").bind('click', function (A) {

		A.preventDefault();

		A.returnValue = false;

		var B = $(this).attr("curuserid");

		F = F !== '' ? F.replace('-small', '') : default_cover;

		$.ajax({

			type: "POST",

			dataType: 'json',

			url: um.ajax_url,

			data: { "action": "author_cover", "user": B, "cover": F },

			beforeSend: function () { $(this).addClass("loading") },

			success: function (C) {

				$(this).removeClass("loading");

				if (C.success === 1) { $("#cover-close").click() }

			}

		});

		return false; return false

	});



	// Upload cover

	$("a#upload-cover").click(function (G) {

		G.preventDefault();

		var I = $(this), B = $("a#uploaded-cover").children("img"), C = $("#cover img");

		formfield = B.attr("src");

		tb_show("", um.admin_url + "media-upload.php?type=image&TB_iframe=true");

		window.send_to_editor = function (D) {

			imgurl = $("img", D).attr("src");

			B.attr('src', um.timthumb + imgurl + "&w=240&h=64&zc=1&q=100"); C.attr('src', um.timthumb + imgurl + "&w=1000&h=265&zc=1&q=100");

			var F = um.timthumb + imgurl + "&w=1000&h=265&zc=1&q=100";

			var E = $("#cover-sure").attr("curuserid");

			$.ajax({

				type: "POST",

				dataType: 'json',

				url: um.ajax_url,

				data: { "action": "author_cover", "user": E, "cover": F },

				success: function (C) {

					if (C.success === 1) { $("#cover-close").click() }

				}

			});

			tb_remove();

		}; return false

	});



});