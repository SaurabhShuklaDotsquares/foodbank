/*global window, $*/
var Global = {
    GridDataTable: {},
    DomainName: "bvnvb",
    DataServer: {
        multisearch: [],
        dataURL: ""
    },
    FilterType: {
        Contains: 0,
        Equals: 1,
        StartsWith: 2,
        LessThanOrEqual: 3,
        GreaterThanOrEqual: 4
    },
    Filter: {
        text: "",
        value: ""
    },
    MessageType: {
        Success: 0,
        Error: 1,
        Warning: 2,
        Info: 3
    },
    Model: null,
    ResultData: null
};

var Constants = {
    TransferedDonationAuditReport: "The gifts have been successfully transferred to the donor records, would you like to generate an audit report?",
    Braintree3DSecure: true
}

Global.FormHelper = function (formElement, options, onSucccess, onError, onBeforeSubmit) {
  
    "use strict";

    var decimalElement = formElement.find('.allow-decimal');
    Global.SetDecimal(decimalElement);

    var integerElement = formElement.find('.allow-integer');
    Global.SetInteger(integerElement);

    var alphanumericElement = formElement.find('.alpha-numeric');
    Global.SetAlphaNumeric(alphanumericElement);

    var settings = {};
    settings = $.extend({}, settings, options);
       
    $.validator.unobtrusive.parse(formElement)
    formElement.validate(settings.validateSettings);

    formElement.submit(function (e) {
        if (options && options.beforeSubmit) {
            if (!options.beforeSubmit()) {
                return false;
            }
        }
        var submitBtn = formElement.find(':submit');
        
        if (formElement.validate().valid()) {
            var isValid = true;
            if (onBeforeSubmit) {
                isValid = onBeforeSubmit();
            }
            if (isValid) {
                submitBtn.find('i').removeClass("fa fa-arrow-circle-right");
                submitBtn.find('i').addClass("fa fa-spinner fa-spin");
                submitBtn.prop('disabled', true);
                submitBtn.find('span').html('Submiting..');
                $.ajax(formElement.attr("action"), {
                    type: "POST",
                    data: formElement.serializeArray(),
                    success: function (result) {
                        if (onSucccess === null || onSucccess === undefined) {
                            if (result.isSuccess) {
                                window.location.href = result.redirectUrl;
                            } else {
                                if (settings.updateTargetId) {
                                    $("#" + settings.updateTargetId).html(result);
                                    Global.Scroll(settings.updateTargetId);
                                }
                            }
                        } else {
                            onSucccess(result);
                        }
                    },
                    error: function (jqXHR, status, error) {
                        if (onError !== null && onError !== undefined) {
                            onError(jqXHR, status, error);
                        }
                    },
                    complete: function () {
                        submitBtn.find('i').removeClass("fa fa-spinner fa-spin");
                        submitBtn.find('i').addClass("fa fa-arrow-circle-right");
                        submitBtn.find('span').html('Submit');
                        submitBtn.prop('disabled', false);
                    }
                });
            }
        }

        e.preventDefault();
    });

    return formElement;
};

Global.FormHelperWithFile = function (formElement, options, onSucccess, onError, loadingElementId, onComplete) {
    "use strict";
    var settings = {};
    settings = $.extend({}, settings, options);
    //$.validator.unobtrusive.parse(formElement);

    $.validator.unobtrusive.parse(formElement)
    formElement.validate(settings.validateSettings);

    if (settings.validateSettings !== null && settings.validateSettings !== undefined) {
        formElement.validate(settings.validateSettings);
    }

    formElement.off("submit").submit(function (e) {
        e.preventDefault();
        e.stopImmediatePropagation();

        var formdata = new FormData();
        formElement.find('input[type="file"]:not(:disabled)').each(function (i, elem) {
            if (elem.files && elem.files.length) {
                for (var i = 0; i < elem.files.length; i++) {
                    var file = elem.files[i];
                    formdata.append(elem.getAttribute('name'), file);
                }
            }
        });

        $.each(formElement.serializeArray(), function (i, item) {
            formdata.append(item.name, item.value);
        });

        if (options && options.updateFormData) {
            var updateformdata = options.updateFormData(formdata);
            if (updateformdata !== null && updateformdata !== undefined) {
                formdata = updateformdata;
            }
        }


        var submitBtn = formElement.find('.btn-primary');
        if (formElement.validate().valid() && formElement.valid()) {

            if (options && options.beforeSubmit) {
                if (!options.beforeSubmit()) {
                    return false;
                }
            }

            var $buttonI = submitBtn.find('i');
            submitBtn.attr({ "data-visible-class": submitBtn.attr("class") }, { "data-text": submitBtn.find("span").text() });
            //submitBtn.removeClass(submitBtn.attr("class"));
            submitBtn.addClass("spinning");
            //submitBtn.prop('disabled', true);
            submitBtn.attr('disabled', 'disabled');
            submitBtn.find('span').html('Submiting..');

            $.ajax(formElement.attr("action"), {
                type: "POST",
                data: formdata,
                contentType: false,
                processData: false,
                beforeSend: function () {
                    if (settings.loadingElementId != null || settings.loadingElementId != undefined) {
                        $("#" + settings.loadingElementId).show();
                        submitBtn.hide();
                    }
                },
                success: function (result) {
                    if (onSucccess === null || onSucccess === undefined) {
                        if (result.isSuccess) {
                            window.location.href = result.redirectUrl;
                        } else {
                            if (settings.updateTargetId) {
                                var datatresult = (result.message == null || result.message == undefined) ? ((result.data == null || result.data == undefined) ? result : result.data) : result.message;
                                $("#" + settings.updateTargetId).html(datatresult);
                            }
                        }
                    } else {
                        onSucccess(result);
                    }
                },
                error: function (jqXHR, status, error) {
                    if (onError !== null && onError !== undefined) {
                        onError(jqXHR, status, error);
                        $("#loadingElement").hide();
                    }
                },
                complete: function (result) {
                    if (onComplete === null || onComplete === undefined) {
                        if (settings.loadingElementId !== null || settings.loadingElementId !== undefined) {
                            $("#" + settings.loadingElementId).hide();
                        }
                        submitBtn.removeClass("spinning");
                        submitBtn.addClass(submitBtn.attr("data-visible-class"));
                        submitBtn.find('span').text(submitBtn.attr("data-text"));
                        //submitBtn.prop('disabled', false);
                        submitBtn.removeAttr('disabled');
                    } else {
                        onComplete(result);
                    }

                }
            });
        }

        e.preventDefault();
    });
    return formElement;
};

Global.FormHelperSubmit = function (formElement, options, onSucccess, onError, dataToPost) {
    "use strict";
    var settings = {};
    settings = $.extend({}, settings, options);
    if (formElement.validate().valid()) {
        $.ajax(formElement.attr("action"), {
            type: "POST",
            data: dataToPost != undefined && dataToPost != null && dataToPost.length > 0 ? dataToPost : formElement.serializeArray(),
            success: function (result) {
                if (onSucccess === null || onSucccess === undefined) {
                    if (result.isSuccess) {
                        window.location.href = result.redirectUrl;
                    } else {
                        if (settings.updateTargetId) {
                            $("#" + settings.updateTargetId).html(result);
                            Global.Scroll(settings.updateTargetId);
                        }
                    }
                } else {
                    onSucccess(result);
                }
            },
            error: function (jqXHR, status, error) {
                if (onError !== null && onError !== undefined) {
                    onError(jqXHR, status, error);
                }
            },
            complete: function () {
            }
        });
    }
};

Global.FormDeleteHelper = function (formElement, options, onSucccess, onError) {
    "use strict";
    formElement.submit(function (e) {
        var submitBtn = formElement.find(':submit');
        submitBtn.find('i').removeClass("fa fa-arrow-circle-right");
        submitBtn.find('i').addClass("fa fa-spinner fa-spin");
        submitBtn.prop('disabled', true);
        submitBtn.find('span').html('Submiting..');
        $.ajax(formElement.attr("action"), {
            type: "POST",
            data: formElement.serializeArray(),
            success: function (result) {
                onSucccess(result);
            },
            error: function (jqXHR, status, error) {
                if (onError !== null && onError !== undefined) {
                    onError(jqXHR, status, error);
                }
            },
            complete: function () {
                submitBtn.find('i').removeClass("fa fa-spinner fa-spin");
                submitBtn.find('i').addClass("fa fa-arrow-circle-right");
                submitBtn.find('span').html('Submit');
                submitBtn.prop('disabled', false);
            }
        });
        e.preventDefault();
    });
    return formElement;
};

Global.GridHelper = function (gridElement, options) {
    if ($(gridElement).find("thead tr th").length > 1) {
        var settings = {};
        settings = $.extend({}, settings, options);
        $(gridElement).dataTable(settings);
        return $(gridElement);
    }
};

Global.GridAjaxHelper = function (gridElement, options, serviceUrl, callback, dicSearchable, serverSide, callbackCreatedRow, errorCallback, completeCallback) {
    if ($(gridElement).find("thead tr th").length >= 1) {
        var settings = {
            "processing": true,
            "serverSide": serverSide == null || serverSide == undefined ? true : serverSide,
            "ajax": Global.DomainName + serviceUrl,
            "bLengthChange": true,
            "fnServerData": function (sSource, aoData, fnCallback) {
                var aoDataServer = {
                    start: 0,
                    draw: 0,
                    length: 0,
                    columns: [],
                    order: [],
                    search: {
                        regex: false,
                        value: ""
                    },
                    multisearch: []
                };

                for (var i = 0; i < aoData.length; i++) {
                    if (aoData[i].name == "start") {
                        aoDataServer.start = aoData[i].value;
                    } else if (aoData[i].name == "draw") {
                        aoDataServer.draw = aoData[i].value;
                    } else if (aoData[i].name == "length") {
                        aoDataServer.length = aoData[i].value;
                    } else if (aoData[i].name == "order") {
                        for (var j = 0; j < aoData[i].value.length; j++) {
                            aoDataServer.order.push({
                                column: aoData[i].value[j].column,
                                dir: aoData[i].value[j].dir
                            });
                        }
                    } else if (aoData[i].name == "search") {
                        aoDataServer.search.regex = aoData[i].value.regex;
                        aoDataServer.search.value = aoData[i].value.value;
                    } else if (aoData[i].name == "columns") {
                        for (var j = 0; j < aoData[i].value.length; j++) {
                            var isSearch = dicSearchable != undefined && dicSearchable[aoData[i].value[j].name] != undefined ? (dicSearchable[aoData[i].value[j].name].is(":checked") ? 1 : 0) : aoData[i].value[j].searchable;
                            aoDataServer.columns.push({
                                data: aoData[i].value[j].data,
                                name: aoData[i].value[j].name,
                                orderable: aoData[i].value[j].orderable,
                                search: { regex: aoData[i].value[j].search.regex, value: aoData[i].value[j].search.value },
                                searchable: isSearch
                            });
                        }
                    }
                }

                if (Global.DataServer != null && Global.DataServer.multisearch != null && Global.DataServer.dataURL == serviceUrl) {
                    aoDataServer.multisearch = Global.DataServer.multisearch;
                }

                if (Global.Filter != null) {
                    aoDataServer.filter = Global.Filter;
                }

                if (Global.FilterType != null) {
                    aoDataServer.filterType = Global.FilterType;
                }

                if (Global.Model != null) {
                    $.ajax({
                        "dataType": 'json',
                        "contentType": "application/json; charset=utf-8",
                        "type": "POST",
                        "cache": false,
                        "url": Global.DataServer.dataURL == "" ? Global.DomainName + serviceUrl : (Global.DataServer.dataURL == serviceUrl ? Global.DomainName + serviceUrl : Global.DomainName + Global.DataServer.dataURL),
                        "data": JSON.stringify({ model: aoDataServer, actionModel: Global.Model }),
                        "success": function (result) {
                            if (result != undefined && result != null && result.isSuccess != undefined && result.isSuccess != null && result.isSuccess == false) {
                                Global.ShowMessage(result.data, Global.MessageType.Error);
                                fnCallback({ "draw": 0, "recordsTotal": 0, "recordsFiltered": 0, "data": [] });
                            }
                            else {
                                Global.ResultData = result;
                                fnCallback(result);
                            }
                        },
                        error: function (xhr, textStatus, error) {
                            //if (typeof console == "object") {
                            //    console.log(xhr.status + "," + xhr.responseText + "," + textStatus + "," + error);
                            //}
                            if (errorCallback) {
                                errorCallback();
                            }
                        },
                        complete: function () {
                            if (completeCallback) {
                                completeCallback();
                            }
                        }
                    });
                }
                else {
                    $.ajax({
                        //"dataType": 'json',
                        //"contentType": "application/json; charset=utf-8",
                        "type": "POST",
                        "cache": false,
                        "url": Global.DataServer.dataURL == "" ? Global.DomainName + serviceUrl : (Global.DataServer.dataURL == serviceUrl ? Global.DomainName + serviceUrl : Global.DomainName + Global.DataServer.dataURL),
                        //"data": JSON.stringify(aoDataServer),
                        "data": aoDataServer,
                        "success": function (result) {
                            if (result != undefined && result != null && result.isSuccess != undefined && result.isSuccess != null && result.isSuccess == false) {
                                Global.ShowMessage(result.data, Global.MessageType.Error);
                                fnCallback({ "draw": 0, "recordsTotal": 0, "recordsFiltered": 0, "data": [] });
                            }
                            else {
                                Global.ResultData = result;
                                fnCallback(result);
                            }
                        },
                        error: function (xhr, textStatus, error) {
                            //if (typeof console == "object") {
                            //    console.log(xhr.status + "," + xhr.responseText + "," + textStatus + "," + error);
                            //}
                            if (errorCallback) {
                                errorCallback();
                            }
                        },
                        complete: function () {
                            if (completeCallback) {
                                completeCallback();
                            }
                        }
                    });
                }
            },
            "fnDrawCallback": function (oSettings) {
                if (callback) {
                    callback(Global.ResultData);
                }
            },
            "createdRow": function (row, data, dataIndex) {
                if (callbackCreatedRow) {
                    callbackCreatedRow(row, data, dataIndex);
                }
            }
        };
        settings = $.extend({}, settings, options);
        var gridTable = $(gridElement).dataTable(settings);
        Global.GridDataTable[gridElement] = gridTable;
        return gridTable;
    }
};

Global.GridAjaxHelperAsync = function (gridElement, options, serviceUrl, callback, dicSearchable, serverSide, callbackCreatedRow, errorCallback, completeCallback) {
    if ($(gridElement).find("thead tr th").length >= 1) {
        var settings = {
            "processing": true,
            "serverSide": serverSide == null || serverSide == undefined ? true : serverSide,
            "ajax": Global.DomainName + serviceUrl,
            "bLengthChange": true,
            "fnServerData": function (sSource, aoData, fnCallback) {
                var aoDataServer = {
                    start: 0,
                    draw: 0,
                    length: 0,
                    columns: [],
                    order: [],
                    search: {
                        regex: false,
                        value: ""
                    },
                    multisearch: []
                };

                for (var i = 0; i < aoData.length; i++) {
                    if (aoData[i].name == "start") {
                        aoDataServer.start = aoData[i].value;
                    } else if (aoData[i].name == "draw") {
                        aoDataServer.draw = aoData[i].value;
                    } else if (aoData[i].name == "length") {
                        aoDataServer.length = aoData[i].value;
                    } else if (aoData[i].name == "order") {
                        for (var j = 0; j < aoData[i].value.length; j++) {
                            aoDataServer.order.push({
                                column: aoData[i].value[j].column,
                                dir: aoData[i].value[j].dir
                            });
                        }
                    } else if (aoData[i].name == "search") {
                        aoDataServer.search.regex = aoData[i].value.regex;
                        aoDataServer.search.value = aoData[i].value.value;
                    } else if (aoData[i].name == "columns") {
                        for (var j = 0; j < aoData[i].value.length; j++) {
                            var isSearch = dicSearchable != undefined && dicSearchable[aoData[i].value[j].name] != undefined ? (dicSearchable[aoData[i].value[j].name].is(":checked") ? 1 : 0) : aoData[i].value[j].searchable;
                            aoDataServer.columns.push({
                                data: aoData[i].value[j].data,
                                name: aoData[i].value[j].name,
                                orderable: aoData[i].value[j].orderable,
                                search: { regex: aoData[i].value[j].search.regex, value: aoData[i].value[j].search.value },
                                searchable: isSearch
                            });
                        }
                    }
                }

                if (Global.DataServer != null && Global.DataServer.multisearch != null && Global.DataServer.dataURL == serviceUrl) {
                    aoDataServer.multisearch = Global.DataServer.multisearch;
                }

                if (Global.Filter != null) {
                    aoDataServer.filter = Global.Filter;
                }

                if (Global.FilterType != null) {
                    aoDataServer.filterType = Global.FilterType;
                }

                if (Global.Model != null) {
                    $.ajax({
                        "dataType": 'json',
                        "contentType": "application/json; charset=utf-8",
                        "type": "POST",
                        "async": false,
                        "cache": false,
                        "url": Global.DataServer.dataURL == "" ? Global.DomainName + serviceUrl : (Global.DataServer.dataURL == serviceUrl ? Global.DomainName + serviceUrl : Global.DomainName + Global.DataServer.dataURL),
                        "data": JSON.stringify({ model: aoDataServer, actionModel: Global.Model }),
                        "success": function (result) {
                            if (result != undefined && result != null && result.isSuccess != undefined && result.isSuccess != null && result.isSuccess == false) {
                                Global.ShowMessage(result.data, Global.MessageType.Error);
                                fnCallback({ "draw": 0, "recordsTotal": 0, "recordsFiltered": 0, "data": [] });
                            }
                            else {
                                Global.ResultData = result;
                                fnCallback(result);
                            }
                        },
                        error: function (xhr, textStatus, error) {
                            //if (typeof console == "object") {
                            //    console.log(xhr.status + "," + xhr.responseText + "," + textStatus + "," + error);
                            //}
                            if (errorCallback) {
                                errorCallback();
                            }
                        },
                        complete: function () {
                            if (completeCallback) {
                                completeCallback();
                            }
                        }
                    });
                }
                else {
                    $.ajax({
                        //"dataType": 'json',
                        //"contentType": "application/json; charset=utf-8",
                        "type": "POST",
                        "async": false,
                        "cache": false,
                        "url": Global.DataServer.dataURL == "" ? Global.DomainName + serviceUrl : (Global.DataServer.dataURL == serviceUrl ? Global.DomainName + serviceUrl : Global.DomainName + Global.DataServer.dataURL),
                        //"data": JSON.stringify(aoDataServer),
                        "data": aoDataServer,
                        "success": function (result) {
                            if (result != undefined && result != null && result.isSuccess != undefined && result.isSuccess != null && result.isSuccess == false) {
                                Global.ShowMessage(result.data, Global.MessageType.Error);
                                fnCallback({ "draw": 0, "recordsTotal": 0, "recordsFiltered": 0, "data": [] });
                            }
                            else {
                                Global.ResultData = result;
                                fnCallback(result);
                            }
                        },
                        error: function (xhr, textStatus, error) {
                            //if (typeof console == "object") {
                            //    console.log(xhr.status + "," + xhr.responseText + "," + textStatus + "," + error);
                            //}
                            if (errorCallback) {
                                errorCallback();
                            }
                        },
                        complete: function () {
                            if (completeCallback) {
                                completeCallback();
                            }
                        }
                    });
                }
            },
            "fnDrawCallback": function (oSettings) {
                if (callback) {
                    callback(Global.ResultData);
                }
            },
            "createdRow": function (row, data, dataIndex) {
                if (callbackCreatedRow) {
                    callbackCreatedRow(row, data, dataIndex);
                }
            }
        };
        settings = $.extend({}, settings, options);
        var gridTable = $(gridElement).dataTable(settings);
        Global.GridDataTable[gridElement] = gridTable;
        return gridTable;
    }
};

Global.EmptyDataTable = function (gridDatatable) {
    if (gridDatatable) {
        gridDatatable.empty();
        gridDatatable.fnDestroy();
        gridDatatable.dataTable().fnClearTable();
    }
};

Global.FormValidationReset = function (formElement, validateOption) {
    if ($(formElement).data('validator')) {
        $(formElement).data('validator', null);
    }

    $(formElement).validate(validateOption);

    return $(formElement);
};

Global.ModalClear = function (modalElement) {
    if ($(modalElement).length > 0) {
        $(modalElement).removeData('bs.modal');
        $(modalElement).find(".modal-content").html("");
    }
};

Global.SetDefaultValue = function (textElement, defaultValue) {
    $("#" + textElement).on("blur", function () {
        if ($(this).val() == "")
            $(this).val(defaultValue);
    });
};

Global.Tree = function (menu) {
    var _this = this;

    $("li a", $(menu)).on('click', function (e) {
        //Get the clicked link and the next element
        var $this = $(this);
        var checkElement = $this.next();

        //Check if the next element is a menu and is visible
        if ((checkElement.is('.treeview-menu')) && (checkElement.is(':visible'))) {
            //Close the menu
            checkElement.slideUp('normal', function () {
                checkElement.removeClass('menu-open');
                //Fix the layout in case the sidebar stretches over the height of the window
                //_this.layout.fix();
            });
            checkElement.parent("li").removeClass("active");
        }
        //If the menu is not visible
        else if ((checkElement.is('.treeview-menu')) && (!checkElement.is(':visible'))) {
            //Get the parent menu
            var parent = $this.parents('ul').first();
            //Close all open menus within the parent
            var ul = parent.find('ul:visible').slideUp('normal');
            //Remove the menu-open class from the parent
            ul.removeClass('menu-open');
            //Get the parent li
            var parent_li = $this.parent("li");

            //Open the target menu and add the menu-open class
            checkElement.slideDown('normal', function () {
                //Add the class active to the parent li
                checkElement.addClass('menu-open');
                parent.find('li.active').removeClass('active');
                parent_li.addClass('active');
            });
        }
        //if this isn't a link, prevent the page from being redirected
        if (checkElement.is('.treeview-menu')) {
            //e.preventDefault();
        }
    });
};

Global.init = function () {
    Global.Tree('.sidebar');
};

Global.ShowMessage = function (message, type) {
    if (type == Global.MessageType.Success)
        alertify.success(message);
    else if (type == Global.MessageType.Error)
        alertify.error(message);
    else if (type == Global.MessageType.Warning)
        alertify.warning(message);
    else if (type == Global.MessageType.Info)
        alertify.message(message);
};

Global.Alert = function (title, message, callback) {
    alertify.alert(title, message, function () {
        if (callback)
            callback();
    }).set({ transition: 'zoom' });
};

//Global.AlertDoNotClose = function (title, message, callback) {
//    alertify.alert(title, message, function () {
//        closable: false;
//        if (callback)
//            callback();
//    });
//};

Global.Confirm = function (title, message, okCallback, cancelCallback, btnOKText, btnCancelText) {

    var btnok = btnOKText !== "" && btnOKText !== "undefined" && btnOKText !== undefined ? btnOKText : "OK";
    var btncancel = btnCancelText !== "" && btnCancelText !== "undefined" && btnCancelText !== undefined ? btnCancelText : "Cancel";

    alertify.confirm(title, message, function () {
        if (okCallback)
            okCallback();
    }, function () {
        if (cancelCallback)
            cancelCallback();
    }).set('labels', { ok: btnok, cancel: btncancel });
};

Global.CheckUnCheckTableValue = function () {
    $(".check-all").on("click", function () {
        $(this).closest('.table-data').find('table').find(":checkbox").each(function () {
            this.checked = true;
        });
        return false;
    });

    $(".uncheck-all").on("click", function () {
        $(this).closest('.table-data').find('table').find(":checkbox").each(function () { this.checked = false; });
        return false;
    });
};

Global.Scroll = function (id) {
    // Remove "link" from the ID
    id = id.replace("link", "");
    // Scroll
    $('.modal-dialog').animate({
        scrollTop: $("#" + id).offset().top
    }, 'slow');
};

Global.ScrollTop = function () {
    $('html, body').animate({
        scrollTop: 0
    }, 'slow');
};

Global.ToDate = function (date, splitArg) {
    if (date.indexOf(":") > 0) {
        var dateParts = date.split(' ')[0].split(splitArg);
        var timeParts = date.split(' ')[1].split(':');
        return new Date(dateParts[2], dateParts[1] - 1, dateParts[0], timeParts[0], timeParts[1]);
    }
    else {
        var dateParts = date.split(splitArg);
        return new Date(dateParts[2], dateParts[1] - 1, dateParts[0]);
    }
};

Global.init();

$.validator.addMethod("decimal", function (value, element) {
    var regex = /^\d+(\.\d{0,0})?$/;
    return this.optional(element) || regex.test(value);
}, "The field must be a decimal value.");

Global.SetDecimal = function (element) {
    $(element).on('keydown', function (e) {
        var $txtBox = $(this);
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            // Allow: Ctrl+A, Command+A
            (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }

        // Ensure that there is only 2 digits after decimal points
        var index = $txtBox.val().indexOf('.');
        if (index >= 0) {
            var charAfterdot = $txtBox.val().split('.').length > 1 ? $txtBox.val().split('.')[1] : $txtBox.val().split('.')[0];
            if (parseFloat(charAfterdot) > 0 && charAfterdot.length > 1) {
                e.preventDefault();
            }
        }

    });

    $(element).on('blur', function (e) {
        if ($(this).val().split(".").length > 0) {

            if ($(this).val().split(".")[0] == "") {
                $(this).val("0" + $(this).val());
            }
            if ($(this).val().split(".").length == 1) {
                $(this).val($(this).val() + ".00");
            }
            else if ($(this).val().split(".").length == 2) {
                if ($(this).val().split(".")[1].length == 1) {
                    $(this).val($(this).val() + "0");
                }
                else if ($(this).val().split(".")[1].length == 0) {
                    $(this).val($(this).val() + "00");
                }
            }
        }
    });
}

Global.SetInteger = function (element) {
    $(element).on('keydown', function (event) {
        if (event.shiftKey == true) {
            event.preventDefault();
        }

        if ((event.keyCode >= 48 && event.keyCode <= 57) ||
            (event.keyCode >= 96 && event.keyCode <= 105) ||
            event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 37 ||
            event.keyCode == 39 || event.keyCode == 46 || event.keyCode == 190) {

        } else {
            event.preventDefault();
        }

        if ($(this).val().indexOf('.') !== -1 && event.keyCode == 190)
            event.preventDefault();
        //if a decimal has been added, disable the "."-button
    });
}

Global.SetAlphaNumeric = function (element) {
    $(element).on('keydown', function (event) {
        //if (event.shiftKey == true) {
        //    event.preventDefault();
        //}
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(event.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            // Allow: Ctrl+A, Command+A
            (event.keyCode == 65 && (event.ctrlKey === true || event.metaKey === true)) ||
            // Allow: home, end, left, right, down, up
            (event.keyCode >= 35 && event.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a alpha numeric and stop the keypress
        var regex = new RegExp("^[a-zA-Z0-9_ ]+$");
        var str = String.fromCharCode(!event.charCode ? event.which : event.charCode);
        if (!regex.test(str)) {
            event.preventDefault();
        }
    });
}

$.fn.dataTableExt.oApi.fnStandingRedraw = function (oSettings) {
    //redraw to account for filtering and sorting
    // concept here is that (for client side) there is a row got inserted at the end (for an add)
    // or when a record was modified it could be in the middle of the table
    // that is probably not supposed to be there - due to filtering / sorting
    // so we need to re process filtering and sorting
    // BUT - if it is server side - then this should be handled by the server - so skip this step
    if (oSettings.oFeatures.bServerSide === false) {
        var before = oSettings._iDisplayStart;
        oSettings.oApi._fnReDraw(oSettings);
        //iDisplayStart has been reset to zero - so lets change it back
        oSettings._iDisplayStart = before;
        oSettings.oApi._fnCalculateEnd(oSettings);
    }

    //draw the 'current' page
    oSettings.oApi._fnDraw(oSettings);
};

Global.DeleteMasters = function () {
    $(".delete-btn").on('click', function (e) {
        $(".delete-btn").each(function (i, elem) {
            $(elem).parents("tr").removeClass("selected");
        });
        $(this).parents("tr").addClass("selected");
    });
}

Global.capitalize = function (textboxid, str) {
    // string with alteast one character
    if (str && str.length >= 1) {
        var firstChar = str.charAt(0);
        var remainingStr = str.slice(1);
        str = firstChar.toUpperCase() + remainingStr;
    }
    document.getElementById(textboxid).value = str;
}

Global.StartLoader = function (element, faClass) {

    if (faClass != undefined && faClass != null && faClass != "") {
        $(element).find('i').removeClass(faClass);
    }
    else {
        $(element).find('i').removeClass("fa fa-arrow-circle-right");
    }

    $(element).find('i').addClass("fa fa-spinner fa-spin");
    $(element).prop('disabled', true);
}

Global.EndLoader = function (element, faClass) {

    $(element).find('i').removeClass("fa fa-spinner fa-spin");

    if (faClass != undefined && faClass != null && faClass != "") {
        $(element).find('i').addClass(faClass);
    }
    else {
        $(element).find('i').addClass("fa fa-arrow-circle-right");
    }

    $(element).prop('disabled', false);
}

$.fn.hasAttr = function (name) {
    return this.attr(name) !== undefined;
};

$.fn.trimChar = function (charToRemove) {
    while (this.val().charAt(0) == charToRemove) {
        this.val(this.val().substring(1));
    }

    while (this.val().charAt(this.val().length - 1) == charToRemove) {
        this.val(this.val().substring(0, this.val().length - 1));
    }
    return this.val();
};

Global.IsExist = function (param) {
    return param != undefined && param != null
};

Global.ErrorFocus = function () {

    var id = "";

    $("input.error, select.error").each(function () {
        if (id !== "")
            return false;
        else
            id = this.id;
    })

    if (id !== "") {
        Global.Scroll(id)
        $("#" + id).focus();
    }

};

Global.FilterDataByType = function (gridID) {
    Global.FilterType = $("#chkFilterEquals").is(":checked") ? '1' : '0';
    if (Global.GridDataTable != null && Global.GridDataTable["#" + gridID] != null) {
        Global.GridDataTable["#" + gridID].fnFilter(
            $('#inputFilterSearch').val());
    };

};

Global.CopyToClipboard = function (data, appendTo) {
    var $temp = $("<input type='text' id='copytoClip' value='" + data + "'>");
    if (appendTo != undefined && appendTo != null && appendTo != "" && $(appendTo).length) {
        $(appendTo).append($temp);
    }
    else {
        $("body").append($temp);
    }
    var element = $("#copytoClip");
    element.select();
    document.execCommand("copy", false, null);
    element.remove();
    if ($(".ajs-close").length) {
        $(".ajs-close").click();
    }
};

String.prototype.trimLeft = function (charlist) {
    if (charlist === undefined)
        charlist = "\s";

    return this.replace(new RegExp("^[" + charlist + "]+"), "");
};

String.prototype.trimRight = function (charlist) {
    if (charlist === undefined)
        charlist = "\s";

    return this.replace(new RegExp("[" + charlist + "]+$"), "");
};

String.prototype.trimChar = function (charlist) {
    return this.trimLeft(charlist).trimRight(charlist);
};

$.fn.hasDuplicateValue = function () {
    var preValues = [];
    var isDuplicate = false;
    this.each(function () {
        if (preValues.length > 0 && preValues.indexOf($(this).val().trim()) >= 0) {
            isDuplicate = true;
        }
        preValues.push($(this).val().trim());
    });
    return isDuplicate;
};

//function setDate(selector, date) {
//    var months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sept", "Oct", "Nov", "Dec"];
//    var dateValue = (date.getDate() < 10 ? '0' : '') + date.getDate() + "-" + months[date.getMonth()] + "-" + date.getFullYear() % 100;
//    var timeValue = (date.getHours() % 24).toString().replace(/^\d{1}$/, '0$&') + ":" + (date.getMinutes() % 60).toString().replace(/^\d{1}$/, '0$&')

//    $(selector).val(dateValue);
//    $('#' + $(selector).data('time')).val(timeValue);
//}



