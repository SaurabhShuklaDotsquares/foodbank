(function ($) {

    $.fn.textResult = function (typeOfClass, e) {

        var excludekeyCode = [8, 9, 20, 33, 34, 35, 36, 37, 38, 39, 40, 46, 37];
        var res = true;

        if (excludekeyCode.indexOf(e.keyCode) == -1) {

            var classarr = typeOfClass.split(" ");

            temp = [];
            classes = [];
            lastclasses = ["lower", "upper", "title-case"];

            for (var i = 0; i < classarr.length; i++) {
                if (lastclasses.indexOf(classarr[i].toLowerCase().trim()) !== -1)
                    temp.push(classarr[i].toLowerCase().trim());
                else
                    classes.push(classarr[i].toLowerCase().trim());
            }

            for (var i = 0; i < temp.length; i++) {
                classes.push(temp[i].toLowerCase().trim());
            }

            for (i = 0; i < classes.length; i++) {
                if (classes[i].toLowerCase().trim() === "integer-pos") {
                    if (((e.keyCode < 48 || e.keyCode > 57) && (e.keyCode < 96 || e.keyCode > 105)) || e.shiftKey) {
                        res = false;
                        break;
                    }
                }
                else if (classes[i].toLowerCase().trim() === "integer") {
                    if (this.val().trim() === "" && !e.shiftKey && (e.keyCode === 173 || e.keyCode === 189 || e.keyCode === 109))
                        continue;
                    else {
                        if (((e.keyCode < 48 || e.keyCode > 57) && (e.keyCode < 96 || e.keyCode > 105)) || e.shiftKey) {
                            res = false;
                            break;
                        }
                    }
                }
                else if (classes[i].toLowerCase().trim() === "decimal-pos") {
                    if ((e.keyCode === 190 || e.keyCode === 110) && !e.shiftKey && (this.val().trim() === "" || (this.val().trim() !== "" && this.val().indexOf('.') === -1)))
                        continue;
                    else {
                        if (((e.keyCode < 48 || e.keyCode > 57) && (e.keyCode < 96 || e.keyCode > 105)) || e.shiftKey) {
                            res = false;
                            break;
                        }
                    }
                }
                else if (classes[i].toLowerCase().trim() === "decimal") {
                    if (this.val().trim() === "" && !e.shiftKey && (e.keyCode === 173 || e.keyCode === 189 || e.keyCode === 109))
                        continue;
                    else {
                        if ((e.keyCode === 190 || e.keyCode === 110) && !e.shiftKey && (this.val().trim() === "" || (this.val().trim() !== "" && this.val().indexOf('.') === -1)))
                            continue;
                        else {
                            if (((e.keyCode < 48 || e.keyCode > 57) && (e.keyCode < 96 || e.keyCode > 105)) || e.shiftKey) {
                                res = false;
                                break;
                            }
                        }
                    }
                }
                else if (classes[i].toLowerCase().trim() === "alpha-numeric") {
                    var regex = new RegExp("^[a-zA-Z0-9_ ]+$");
                    var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                    if (!regex.test(str)) {
                        res = false;
                        break;
                    }
                }
                else if (classes[i].toLowerCase().trim() === "alphabets") {
                    if (((e.keyCode < 65 || e.keyCode > 90) || e.shiftKey) && e.keyCode !== 32) {
                        res = false;
                        break;
                    }
                }
                else if (classes[i].toLowerCase().trim().indexOf('length-') !== -1) {
                    var len = classes[i].toLowerCase().trim().split('-')[1];
                    if (parseInt(this.val().length) + 1 > len) {
                        res = false;
                        break;
                    }
                }
                else if (classes[i].toLowerCase().trim() === "lower") {
                    return this.val(this.val().toLowerCase());
                }
                else if (classes[i].toLowerCase().trim() === "upper") {
                    return this.val(this.val().toUpperCase());
                }
                else if (classes[i].toLowerCase().trim() === "title-case") {
                    var lcStr = this.val().toLowerCase();
                    return this.val(lcStr.replace(/(?:^|\s)\w/g, function (match) {
                        return match.toUpperCase();
                    }));
                }
            }
        }

        if (!res)
            e.preventDefault();

        return res;
    };

}(jQuery));

$(function () {
    $('input[type=text]').on("keydown keyup", function (event) {

        if ($(this).attr('class') != undefined) {
            var classarr = $(this).attr("class").split(" ");
            if (classarr.length > 0) {
                var classes = ["lower", "upper", "title-case", "alphabets", "alpha-numeric", "decimal", "decimal-pos", "integer", "integer-pos"];
                var common = $.grep(classes, function (element) {
                    return $.inArray(element, classarr) !== -1;
                });                
                if (common.length > 0 || $(this).attr("class").indexOf('length-') !== -1) {
                    if (event.ctrlKey && (event.keyCode == 88 || event.keyCode == 67 || event.keyCode == 86))
                        return false;
                    else
                        return $(this).textResult($(this).attr("class"), event);
                }               
            }            
        }
    });
});