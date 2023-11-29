/*commn window, $*/
var Common = {
    StandardCommentsArray: []
};

Common.GeneratePassword = function () {
    $.get(Global.DomainName + "base/GetRandomPassword", function (result) {
        if (result.isSuccess == true) {
            var bodyText = "Password : <b>" + result.data + "</b>";
            bodyText += "<br/><br/>Please make a note of this password as you will not be able to view it later.";
            Global.Confirm("Random Password", bodyText, function () {
                $('.random-password').val(result.data);
            }, function () { return false; }, "Continue", "Cancel");
        }
        else {
            Global.ShowMessage(result.data, Global.MessageType.Error);
        }
    });
}

Common.CharitiesByOrganisation1 = function (orgID, parentDiv, callback) {
    var html = "<option value=''>Select Charity...</option>";
    $.get(Global.DomainName + "base/BindDynamicViewBags", { type: "Charity", value: orgID }, function (data) {
        if (data.data.length > 0) {
            $.each(data.data, function (index, item) {
                html = html + "<option value='" + item.value + "'>" + item.text + "</option>";
            });
        }

        $((parentDiv + " #CharityID").trim()).html(html);
        $((parentDiv + " #CharityID").trim()).select2();

        if (callback)
            callback();
    });
}

Common.BranchesByCharity1 = function (charityID, parentDiv, callback) {
    var html = "<option value=''>Select Branch...</option>";
    $.get(Global.DomainName + "base/BindDynamicViewBags", { type: "Branch", value: charityID }, function (data) {
        if (data.data.length > 0) {
            $.each(data.data, function (index, item) {
                html = html + "<option value='" + item.value + "'>" + item.text + "</option>";
            });
        }

        $((parentDiv + " #BranchID").trim()).html(html);
        $((parentDiv + " #BranchID").trim()).select2();

        if (callback)
            callback();
    });
}



//Common.StandardComments = function (orgID, charityID, branchID) {
//    if ((($userType.toLowerCase() == "superadmin" || $userType.toLowerCase() == "internal") && orgID != undefined && orgID != null && orgID != '' && orgID > 0) || ($userType.toLowerCase() != "superadmin" && $userType.toLowerCase() != "internal")) {
//        $.get(Global.DomainName + "base/GetStandardComments", { orgID: orgID, charityID: charityID, branchID: branchID }, function (result) {
//            if (result.isSuccess && result.data != "") {
//                Common.StandardCommentsArray = result.data.split('#@*##');
//                $(".standardcomment-auto").typeahead({
//                    source: Common.StandardCommentsArray
//                });
//            }
//        });
//    }
//}

//Common.GetDonorDefaultValues = function (donorID, callback) {
//    $.get(Global.DomainName + "base/GetDonor", { donorID: donorID }, function (result) {
//        if (result.isSuccess && result.data != "") {
//            if (callback)
//                callback(result);
//        }
//    });
//}

//Common.CharitiesByOrganisation = function (orgID, parentDiv, callback, isBranch, isDonor) {
//    var html = "<option value=''>Select Charity...</option>";
//    $.get(Global.DomainName + "base/BindDynamicViewBags", { type: "Charity", value: orgID }, function (data) {
//        if (data.data.length > 1) {
//            $.each(data.data, function (index, item) {
//                html = html + "<option value='" + item.value + "'>" + item.text + "</option>";
//            });
//            $((parentDiv + " #CharityID").trim()).html(html);
//            if (callback)
//                callback();
//        }
//        else {
//            $.each(data.data, function (index, item) {
//                html = "<option value='" + item.value + "'>" + item.text + "</option>";
//                if (isBranch) {
//                    Common.BranchesByCharity(item.value, parentDiv, callback, isDonor);
//                }
//            });
//            $((parentDiv + " #CharityID").trim()).html(html);
//            $((parentDiv + " #CharityID").trim()).select2();
//        }
//    });
//}

//Common.BranchesByCharity = function (charityID, parentDiv, callback, isDonor) {
//    var html = "<option value=''>Select Branch...</option>";
//    $.get(Global.DomainName + "base/BindDynamicViewBags", { type: "Branch", value: charityID }, function (data) {
//        if (data.data.length > 1) {
//            $.each(data.data, function (index, item) {
//                html = html + "<option value='" + item.value + "'>" + item.text + "</option>";
//            });
//            $((parentDiv + " #BranchID").trim()).html(html);
//        }
//        else {
//            $.each(data.data, function (index, item) {
//                html = "<option value='" + item.value + "'>" + item.text + "</option>";
//                if (isDonor) {
//                    Common.DonorsByBranch(item.value, parentDiv, callback);
//                }
//            });
//            $((parentDiv + " #BranchID").trim()).html(html);
//            $((parentDiv + " #BranchID").trim()).select2();
//        }

//        if (callback)
//            callback();
//    });
//}

//Common.DonorsByBranch = function (branchID, parentDiv, callback) {
//    var html = "<option value=''>Select Donor...</option>";
//    $.get(Global.DomainName + "base/BindDynamicViewBags", { type: "Donors", value: branchID }, function (data) {
//        if (data.data.length > 1) {
//            $.each(data.data, function (index, item) {
//                html = html + "<option value='" + item.value + "'>" + item.text + "</option>";
//            });
//            $((parentDiv + " #Person").trim()).html(html);
//        }
//        else {
//            $.each(data.data, function (index, item) {
//                html = "<option value='" + item.value + "'>" + item.text + "</option>";
//            });
//            $((parentDiv + " #Person").trim()).html(html);
//            $((parentDiv + " #Person").trim()).select2();
//        }

//        if (callback)
//            callback();
//    });
//}



//Common.CharitiesByOrganisation2 = function (orgID, parentDiv, callback) {
//    var html = "";
//    $.get(Global.DomainName + "base/BindDynamicViewBags", { type: "Charity", value: orgID }, function (data) {
//        if (data.data.length > 0) {
//            $.each(data.data, function (index, item) {
//                html = html + "<option value='" + item.value + "'>" + item.text + "</option>";
//            });
//        }

//        $((parentDiv + " #CharityID").trim()).html(html);

//        if (callback)
//            callback();
//    });
//}



//Common.DialogBox = function (modalID, title, message, yesCallback, noCallback, cancelCallback, btnYesText, btnNoText, btnCancelText) {   

//    if (yesCallback) {
//        $(document).off("click", "#" + modalID + " #btnYes").on("click", "#" + modalID + " #btnYes", function () {
//            yesCallback();
//        });
//    }

//    if (noCallback) {
//        $(document).off("click", "#" + modalID + " #btnNo").on("click", "#" + modalID + " #btnNo", function () {
//            noCallback();
//        });
//    }

//    if (cancelCallback) {
//        $(document).off("click", "#" + modalID + " #btnCancel").on("click", "#" + modalID + " #btnCancel", function () {
//            cancelCallback();
//        });
//    }

//    var modalHtml = "";
//    modalHtml += '<div class="modal-header">';
//    modalHtml += '<button type="button" id="btnClose" class="close" data-dismiss="modal">&times;</button>';
//    modalHtml += '<h4 class="modal-title">' + title + '</h4>';
//    modalHtml += '</div>';
//    modalHtml += '<div class="modal-body">';
//    modalHtml += '<p>' + message +'</p>';
//    modalHtml += '</div>';
//    modalHtml += '<div class="modal-footer">';

//    if (yesCallback) {
//        var btnYes = btnYesText !== "" && btnYesText !== "undefined" && btnYesText !== undefined ? btnYesText : "Yes";
//        modalHtml += '<button type="button" id="btnYes" class="btn btn-primary">' + btnYes + '</button>';
//    }

//    if (noCallback) {
//        var btnNo = btnNoText !== "" && btnNoText !== "undefined" && btnNoText !== undefined ? btnNoText : "No";
//        modalHtml += '<button type="button" id="btnNo" class="btn btn-warning">' + btnNo + '</button>';
//    }

//    if (cancelCallback) {
//        var btnCancel = btnCancelText !== "" && btnCancelText !== "undefined" && btnCancelText !== undefined ? btnCancelText : "Cancel";
//        modalHtml += '<button type="button" id="btnCancel" class="btn btn-danger">' + btnCancel + '</button>';
//    }

//    modalHtml += '</div>';
 
//    $("#" + modalID).find(".modal-content").html(modalHtml);
//    $("#" + modalID).modal("show");
//}