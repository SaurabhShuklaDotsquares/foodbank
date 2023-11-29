/*global jQuery, Global*/
(function ($) {
    'use strict';
    function UserIndex() {

        var $this = this, userGrid, formAddEditUser, formAddEditUserDataAccess, formAddEditMSUser;

        function initializeGrid() {

            if (userGrid)
                userGrid.fnDestroy();

            userGrid = new Global.GridAjaxHelper('#grid-user', {
                "aoColumns": [{ "sName": "UserName" }, { "sName": "FirstName" }, { "sName": "LastName" }, { "sName": "Email" }, { "sName": "PrimaryMobile" }, { "sName": "Active" }],
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [5][6] }, { 'visible': false, 'aTargets': [7] }],
                "bDestroy": true
            }, "user/getusers", function () {
                initGridEvent();
            });
            $('#grid-user').parent("div").parent("div").addClass("table-responsive");
        }

        function reInitializeGrid() {
            Global.DataServer.dataURL = "user/getusers?orgID=" + $("div.filter-data #CentralOfficeID").val() + "&charitID=" + $("div.filter-data #CharityID").val() + "&BranchID=" + $("div.filter-data #BranchID").val();
            Global.DataServer.multisearch = [];
            userGrid.fnDraw();
        }

        $("#Overseas").off("change").on("change", function () {
            
            if ($(this).is(":checked")) {
                //$('#OldPostCode').val($("#PostCode").val());
                $("#Postcode").attr("disabled", "disabled");
            }
            else {
                //$('#OldPostCode').val("");
                $("#PostCode").removeAttr("disabled");
            }
        });

        function initializeOrgCharityFilter() {
            $("div.filter-data #CentralOfficeID").select2();
            $("div.filter-data #CharityID").select2();
            $("div.filter-data #BranchID").select2();
            $("div.filter-data #CentralOfficeID").off("change").on("change", function () {
                $("div.filter-data #CharityID").html("<option value=''>Select Charity...</option>");
                $('div.filter-data #BranchID').html("<option value=''>Select Branch...</option>");
                if ($(this).val() != "") {
                    var html = "<option value=''>Select Charity...</option>";
                    var param = $('#div.filter-data select#CentralOfficeID option:selected').val();
                    $.get('user/BindCharities', { organisationID: param }, function (data) {
                        if (data.data.length > 1) {
                            $.each(data.data, function (index, item) {
                                html = html + "<option value='" + item.value + "'>" + item.text + "</option>";
                            });
                            $("div.filter-data #CharityID").html(html);
                        }
                        else {

                            $.each(data.data, function (index, item) {
                                $('#div.filter-data CharityID').removeAttr('disabled');
                                html = "<option value='" + item.value + "'>" + item.text + "</option>";

                                $.get('user/BindBranches', { charityID: item.value }, function (data) {
                                    var htmlBranch = "<option value=''>Select Branch...</option>";
                                    if (data.data.length > 1) {
                                        $.each(data.data, function (index, item) {
                                            htmlBranch = htmlBranch + "<option value='" + item.value + "'>" + item.text + "</option>";
                                        });
                                        $("div.filter-data #BranchID").html(htmlBranch);
                                    }
                                    else {
                                        $('div.filter-data #BranchID').removeAttr('disabled');
                                        $.each(data.data, function (index, item) {
                                            htmlBranch = "<option value='" + item.value + "'>" + item.text + "</option>";
                                        });
                                        $("div.filter-data #BranchID").html(htmlBranch);
                                        $("div.filter-data #BranchID").select2();

                                    }

                                });

                            });
                            $("div.filter-data #CharityID").html(html);
                            $("div.filter-data #CharityID").select2();
                        }
                    });
                }
                reInitializeGrid();
            });
            $('div.filter-data #CharityID').off("change").on('change', function () {
                
                $('div.filter-data #BranchID').html("<option value=''>Select Branch...</option>");

                if ($(this).val() != "") {
                    var html = "<option value=''>Select Branch...</option>";
                    var param = $('div.filter-data select#CharityID option:selected').val();

                    $.get('user/BindBranches', { charityID: param }, function (data) {

                        if (data.data.length > 1) {


                            $.each(data.data, function (index, item) {
                                html = html + "<option value='" + item.value + "'>" + item.text + "</option>";
                            });
                            $("div.filter-data #BranchID").html(html);
                        }
                        else {
                            $('div.filter-data #BranchID').removeAttr('disabled');
                            $.each(data.data, function (index, item) {
                                html = "<option value='" + item.value + "'>" + item.text + "</option>";
                            });
                            $("div.filter-data #BranchID").html(html);
                            $("div.filter-data #BranchID").select2();
                        }
                    });
                }
                reInitializeGrid();
            });
            $('div.filter-data #BranchID').off("change").on('change', function () {
                reInitializeGrid();
            });
        }

        function initGridEvent() {
            $('.switchBox').each(function (index, element) {
                if ($(element).data('bootstrapSwitch')) {
                    $(element).off('switch-change');
                    $(element).bootstrapSwitch('destroy');
                }

                $(element).bootstrapSwitch()
                    .on('switch-change', function () {
                        var switchElement = this;
                        $.post('user/active', { id: this.value }, function (result) {
                            if (!result.isSuccess) {
                                $(switchElement).bootstrapSwitch('toggleState', true);
                                Global.ShowMessage("Failed due to some internal error", Global.MessageType.Error);
                                // Global.Alert(result.data);
                            }

                        });
                    });
            });
        }

        function initializeModalWithForm() {
            $("#modal-create-edit-user").on('loaded.bs.modal', function () {
                $('.postcode').on("focusout", function () {
                    if ($(this).val() !== "") {
                        SearchBegin(this);
                    }
                });
                //$('.postcode').trigger("focusout");

                $('#btnGeneratePassword').off("click").on("click", function () {
                    Common.GeneratePassword();
                });

                formAddEditUser = new Global.FormHelper($("#frm-create-edit-user form"), { updateTargetId: "validation-summary" }, null, null, function () {

                    if ($('#divCharity').is(':visible') && $('#CharityID').val() == "") {
                        Global.ShowMessage("Please select charity of user.", Global.MessageType.Error);
                        return false;
                    }
                    else if ($('#divBranch').is(':visible') && $('#BranchID').val() == "") {
                        Global.ShowMessage("Please select branch of user.", Global.MessageType.Error);
                        return false;
                    }
                    else {
                        return true;
                    }

                }
                );
                initializeModalControlWithEvents();

            }).on('hidden.bs.modal', function () {
                Global.ModalClear($(this));
            });

            $("#modal-delete-user").on('hidden.bs.modal', function () {
                Global.ModalClear($(this));
            });

            $("#modal-create-edit-ms-user").on('loaded.bs.modal', function () {

                $('#btnGeneratePassword').off("click").on("click", function () {
                    Common.GeneratePassword();
                });

                ControlVisibilityChangePassword();

                $("#IsPasswordChange").on("click", function () {
                    ControlVisibilityChangePassword();
                });

                formAddEditMSUser = new Global.FormHelper($("#frm-create-edit-ms-user form"), { updateTargetId: "validation-summary" }, function (result) {
                    if (result.isSuccess) {
                        Global.ModalClear($(this));
                        $("#modal-create-edit-ms-user").modal("hide");
                        Global.ShowMessage(result.data, Global.MessageType.Success);
                        initializeGrid();
                    }
                    else {
                        Global.ShowMessage(result.data, Global.MessageType.Error);
                    }
                });
            }).on('hidden.bs.modal', function () {
                $(this).removeData('bs.modal');
            });
        }

        function initializeModalControlWithEvents() {
            $("#modal-create-edit-user #CentralOfficeID").hide();
            $("#CharityID").hide();
            $("#BranchID").hide();

            $("#lblCentralOfficeID").hide();
            $("#lblCharityID").hide();
            $("#divCharity").hide();
            $("#lblBranchID").hide();
            $("#divBranch").hide();
            $("#divCustomRoles").hide();

            var role = $('#RoleID option:selected').text();
            if (role === 'Organisation' || role === 'Input') {
                $('#lblCentralOfficeID').show();
                $("#modal-create-edit-user #CentralOfficeID").show();
            }
            else if (role == "Custom") {
                $('#lblCentralOfficeID').show();
                $("#modal-create-edit-user #CentralOfficeID").show();
                $("#divCustomRoles").show();
            }
            else if (role === 'Charity') {
                $('#lblCentralOfficeID').show();
                $("#modal-create-edit-user #CentralOfficeID").show();
                $("#lblCharityID").show();
                $("#divCharity").show();
                $("#CharityID").show();
                $("#CharityID").select2();
            }
            else if (role === 'Branch') {
                $("#modal-create-edit-user #CentralOfficeID").show();
                $("#CharityID").show();
                $("#BranchID").show();
                $("#CharityID").select2();
                $("#BranchID").select2();

                $("#lblCentralOfficeID").show();
                $("#lblCharityID").show();
                $("#divCharity").show();
                $("#lblBranchID").show();
                $("#divBranch").show();
            }
            else {
                if (typeof ($("#CentralOffice").val()) != "undefined")
                    $("#lblCentralOfficeID").css("display", "block");
                else {
                    $("#lblCentralOfficeID").hide();
                    $("#modal-create-edit-user #CentralOfficeID").hide();
                }

                if (typeof ($("#Charity").val()) != "undefined") {
                    $("#lblCharityID").show();
                    $("#divCharity").show();
                }
                else {
                    $("#lblCharityID").hide();
                    $("#divCharity").hide();
                    $("#CharityID").hide();
                }
                $("#BranchID").hide();
                $("#lblBranchID").hide();
                $("#divBranch").hide();
            }


            $('#RoleID').off('change').on('change', function () {
                var role = $('#RoleID option:selected').text();
                $("#frm-create-edit-user form").validate().resetForm();
                $("#modal-create-edit-user #CentralOfficeID").hide();
                $("#CharityID").hide();
                $("#BranchID").hide();

                $("#lblCentralOfficeID").hide();
                $("#lblCharityID").hide();
                $("#divCharity").hide();
                $("#lblBranchID").hide();
                $("#divBranch").hide();

                $("#divCustomRoles").hide();

                //$("#CharityID").html("<option value=''>Select Charity...</option>");
                //$('#BranchID').html("<option value=''>Select Branch...</option>");

                if (role == 'SuperAdmin' || role == 'Internal' || role == 'TechnicalSupport' || role == 'Organisation' || role == 'Input' || role == 'Agent' || role == 'Charity' || role == 'Foodbank' || role == 'FoodbankStaff' || role == 'Branch' || role == 'Donor') {
                    $("#userMessage").text('');
                }
                else if (role.indexOf("select") > -1 || role.indexOf("Select") > -1) {
                    $("#userMessage").text('');
                    if (typeof ($("#CentralOffice").val()) != "undefined")
                        $("#lblCentralOfficeID").show();
                    else {
                        $("#lblCentralOfficeID").hide();
                        $("#modal-create-edit-user #CentralOfficeID").hide();
                    }
                }
                else {
                    $("#userMessage").text("By default this user will have no access to any records. Once you have created and saved this user please click on 'Edit data access' and assign them to the relevant Foodbank");
                }


                $('#CharityID').val("");
                $('#BranchID').val("");

                var param = $('#RoleID option:selected').text();

                if ($("#hdnRoleName").val().toLowerCase() == "superadmin") {
                    if (param == 'Organisation' || param == 'Input' || param == 'Charity' || param == 'Branch' || param == 'Custom') {
                        if (param === 'Organisation' || param == 'Input') {
                            $('#lblCentralOfficeID').show();
                            $("#modal-create-edit-user #CentralOfficeID").show();
                        }
                        else if (param == "Custom") {
                            $('#lblCentralOfficeID').show();
                            $("#modal-create-edit-user #CentralOfficeID").show();
                            $("#divCustomRoles").show();
                        }
                        else if (param === 'Charity') {
                            $('#lblCentralOfficeID').show();
                            $("#modal-create-edit-user #CentralOfficeID").show();
                            $("#lblCharityID").show();
                            $("#divCharity").show();
                            $("#CharityID").show();
                            $("#CharityID").select2("destroy");
                            $("#CharityID").select2();
                        }
                        else if (param === 'Branch') {
                            $("#modal-create-edit-user #CentralOfficeID").show();
                            $("#CharityID").show();
                            $("#BranchID").show();
                            $("#lblCentralOfficeID").show();
                            $("#lblCharityID").show();
                            $("#divCharity").show();
                            $("#lblBranchID").show();
                            $("#divBranch").show();
                            $("#CharityID").select2("destroy");
                            $("#CharityID").select2();
                            $("#BranchID").select2("destroy");
                            $("#BranchID").select2();
                        }

                        if ($('#modal-create-edit-user #CentralOfficeID option').length <= 1) {
                            $("#modal-create-edit-user #CentralOfficeID").html('');
                            $.get('user/BindOrganisations', null, function (data) {
                                var html = "<option value=''>Select Organisation...</option>";
                                if (data.data.length > 0) {
                                    $.each(data.data, function (index, item) {
                                        html = html + "<option value='" + item.value + "'>" + item.text + "</option>";
                                    });
                                }
                                else {
                                    if ($('#CentralOffice').length > 0) {
                                        $('#modal-create-edit-user #CentralOfficeID').change();
                                    }
                                }
                                $("#modal-create-edit-user #CentralOfficeID").html(html);
                                $('#modal-create-edit-user #CentralOfficeID').change();
                            });
                        }
                        else
                            $('#modal-create-edit-user #CentralOfficeID').change();
                    }
                }
                else if ($("#hdnRoleName").val().toLowerCase() === "organisation" || $("#hdnRoleName").val().toLowerCase() === "input") {

                    if (param === "Charity") {
                        $("#lblCentralOfficeID").show();
                        $("#CharityID").show();
                        $("#lblCharityID").show();
                        $("#divCharity").show();
                        $("#CharityID").select2("destroy");
                        $("#CharityID").select2();
                    }
                    else if (param == "Custom") {
                        $('#lblCentralOfficeID').show();
                        $("#divCustomRoles").show();
                    }
                    else if (param === "Branch") {
                        $("#lblCentralOfficeID").show();
                        $("#CharityID").show();
                        $("#BranchID").show();
                        $("#lblCharityID").show();
                        $("#divCharity").show();
                        $("#lblBranchID").show();
                        $("#divBranch").show();
                        $("#CharityID").select2("destroy");
                        $("#CharityID").select2();
                        $("#BranchID").select2("destroy");
                        $("#BranchID").select2();
                    }

                    if ($('#CharityID option').length == 2) {
                        $('#CharityID').val($('#CharityID option:last').val());
                        setTimeout(function () { $("#CharityID").change(); }, 100);
                    }

                }
                else if ($("#hdnRoleName").val().toLowerCase() === "charity") {
                    $("#lblBranchID").show();
                    $("#divBranch").show();
                    $("#BranchID").show();
                    $("#BranchID").rules("add", { required: true, messages: { required: "*required" } });
                    $("#lblCharityID").show();
                    $("#divCharity").show();
                    $("#lblCentralOfficeID").show();
                    $("#BranchID").select2("destroy");
                    $("#BranchID").select2();

                    if ($('#BranchID option').length == 2) {
                        $('#BranchID').val($('#BranchID option:last').val());
                    }

                }
                else {

                    if (param === "Charity") {
                        $("#BranchID").show();
                        $("#lblBranchID").show();
                        $("#divBranch").show();
                        $("#BranchID").select2("destroy");
                        $("#BranchID").select2();
                    }
                    else if (param == "Custom") {
                        $('#lblCentralOfficeID').show();
                        $("#modal-create-edit-user #CentralOfficeID").show();
                        $("#divCustomRoles").show();
                    }
                    else if (param === "Branch") {
                        $("#CharityID").show();
                        $("#BranchID").show();
                        $("#lblCharityID").show();
                        $("#divCharity").show();
                        $("#lblBranchID").show();
                        $("#divBranch").show();
                        $("#CharityID").select2("destroy");
                        $("#CharityID").select2();
                        $("#BranchID").select2("destroy");
                        $("#BranchID").select2();
                    }
                }

                if($('#RoleID option:selected').text() == "Custom")
                    {
                        var html = "<option value=''>Select Custom Role...</option>";
                        var param = $('#modal-create-edit-user select#CentralOfficeID option:selected').val();
                        $.get('base/GetCustomRoles', { orgID: param }, function (data) {
                            if (data.data.length > 0) {
                                $.each(data.data, function (index, item) {
                                    html = html + "<option value='" + item.value + "'>" + item.text + "</option>";
                                });
                            }
                            $("#CustomRoleID").html(html);
                        });
                    }
            });

            $('#modal-create-edit-user #CentralOfficeID').off("change").on('change', function () {
                $("#CharityID").html("<option value=''>Select Charity...</option>");
                $('#BranchID').html("<option value=''>Select Branch...</option>");

                if ($('#RoleID option:selected').text() == "Charity") {
                    $("#CharityID").select2("destroy");
                    $("#CharityID").select2();
                }

                if ($('#RoleID option:selected').text() == "Branch") {
                    $("#CharityID").select2("destroy");
                    $("#CharityID").select2();
                    $("#BranchID").select2("destroy");
                    $("#BranchID").select2();
                }

                if ($(this).val() != "") {
                    if ($('#RoleID option:selected').text() == "Charity" || $('#RoleID option:selected').text() == "Branch") {
                        //$("#CharityID").html("");
                        var html = "<option value=''>Select Charity...</option>";
                        var param = $('#modal-create-edit-user select#CentralOfficeID option:selected').val();
                        $.get('user/BindCharities', { organisationID: param }, function (data) {
                            //var html = "";
                            //$("#BranchID").html("");
                            if (data.data.length > 1) {
                                //html = "<option value=''>Select Charity...</option>";

                                //$('#CharityID').removeAttr('disabled');
                                //$('#CharityID').rules("add", { required: true, messages: { required: "*required" } })

                                $.each(data.data, function (index, item) {
                                    html = html + "<option value='" + item.value + "'>" + item.text + "</option>";
                                });
                                //$('#BranchID').attr('disabled', 'disabled').html("<option value=''>Select Branch...</option>");
                                $("#CharityID").html(html);
                            }
                            else {
                                //$('#BranchID').attr('disabled', 'disabled').html("<option value=''>Select Branch...</option>");
                                $.each(data.data, function (index, item) {
                                    $('#CharityID').removeAttr('disabled');
                                    html = "<option value='" + item.value + "'>" + item.text + "</option>";
                                    if ($('#RoleID option:selected').text() == "Branch") {
                                        $.get('user/BindBranches', { charityID: item.value }, function (data) {
                                            var htmlBranch = "<option value=''>Select Branch...</option>";
                                            if (data.data.length > 1) {

                                                //$('#BranchID').removeAttr('disabled');
                                                //$('#BranchID').rules("add", { required: true, messages: { required: "*required" } })

                                                $.each(data.data, function (index, item) {
                                                    htmlBranch = htmlBranch + "<option value='" + item.value + "'>" + item.text + "</option>";
                                                });
                                                $("#BranchID").html(htmlBranch);
                                            }
                                            else {
                                                $('#BranchID').removeAttr('disabled');
                                                $.each(data.data, function (index, item) {
                                                    htmlBranch = "<option value='" + item.value + "'>" + item.text + "</option>";
                                                });
                                                $("#BranchID").html(htmlBranch);
                                                $("#BranchID").select2();
                                                //$('#BranchID').attr('disabled', 'disabled').html("<option value=''>Select Branch...</option>");
                                            }

                                        });
                                    }
                                });
                                //$('#CharityID').attr('disabled', 'disabled').html("<option value=''>Select Charity...</option>");
                                //$('#BranchID').attr('disabled', 'disabled').html("<option value=''>Select Branch...</option>");
                                $("#CharityID").html(html);
                                $("#CharityID").select2();
                            }

                        });
                    }
                    else if ($('#RoleID option:selected').text() == "Custom") {
                        var html = "<option value=''>Select Custom Role...</option>";
                        var param = $('#modal-create-edit-user select#CentralOfficeID option:selected').val();
                        $.get('base/GetCustomRoles', { orgID: param }, function (data) {
                            if (data.data.length > 0) {
                                $.each(data.data, function (index, item) {
                                    html = html + "<option value='" + item.value + "'>" + item.text + "</option>";
                                });
                            }
                            $("#CustomRoleID").html(html);
                        });
                    }
                }

            });

            $('#CharityID').off("change").on('change', function () {
                $('#BranchID').html("<option value=''>Select Branch...</option>");
                if ($('#RoleID option:selected').text() == "Branch") {
                    $("#BranchID").select2("destroy");
                    $("#BranchID").select2();
                }
                if ($(this).val() != "") {
                    if ($('#RoleID option:selected').text() == "Branch") {
                        //$("#BranchID").html("");
                        var html = "<option value=''>Select Branch...</option>";
                        var param = $('select#CharityID option:selected').val();

                        $.get('user/BindBranches', { charityID: param }, function (data) {

                            if (data.data.length > 1) {

                                //$('#BranchID').removeAttr('disabled');
                                //$('#BranchID').rules("add", { required: true, messages: { required: "*required" } })

                                $.each(data.data, function (index, item) {
                                    html = html + "<option value='" + item.value + "'>" + item.text + "</option>";
                                });
                                $("#BranchID").html(html);
                            }
                            else {
                                $('#BranchID').removeAttr('disabled');
                                $.each(data.data, function (index, item) {
                                    html = "<option value='" + item.value + "'>" + item.text + "</option>";
                                });
                                $("#BranchID").html(html);
                                $("#BranchID").select2();
                                //$('#BranchID').attr('disabled', 'disabled').html("<option value=''>Select Branch...</option>");
                            }

                        });
                    }
                }

            });

            $("#OnlinePassword").val("");
            ControlVisibilityChangePassword();

            $("#IsPasswordChange").on("click", function () {
                ControlVisibilityChangePassword();
            });
            //Check user availability on UserName textBox blur
            $("#modal-create-edit-user #UserName").blur(function () {
                $.get('user/UserAvailability', { userName: $(this).val() }, function (data) {
                    if (data.length > 0) {
                        $("#divError").html(data);
                        $("#divError").parent("div").css({ "display": "block", "color": "red" })
                    }
                    else
                        $("#divError").parent("div").css("display", "none")
                });
            });

            $("#LastName").on("blur", function () {
                Global.capitalize("LastName", $("#LastName").val());
            });
            $("#FirstName").on("blur", function () {
                Global.capitalize("FirstName", $("#FirstName").val());
            });
        };

        function initializeUserDataAccessModalWithForm() {
            $("#modal-user-accessdata-user").on('loaded.bs.modal', function () {
                formAddEditUserDataAccess = new Global.FormHelper($("#frm-user-data-access form"), { updateTargetId: "validation-summary", validateSettings: { ignore: "" } }, null, null);
                initializeMultiselectDropdown();

                $('#IsFullAccess').change(function () {

                    if ($(this).prop('checked')) {
                        $('#CharityGroupNotAccess, #CharityGroupAccess').multipleSelect("disable");
                    }
                    else {
                        $('#CharityGroupNotAccess, #CharityGroupAccess').removeAttr("disabled");
                        $('#CharityGroupNotAccess, #CharityGroupAccess').multipleSelect("enable");
                    }
                });

                $('#btn-submit').off("click").on("click", function () {

                    var branches = [];
                    $.each($('#CharityGroupAccess option'), function () {
                        branches.push($(this).val());
                    });
                    $("#BranchesId").val(branches.join(','));

                    if (!$('#IsFullAccess').prop('checked') && $('#BranchesId').val() == "") {
                        Global.Confirm("Warning !", "Are you sure not to give any data access to this user !", function () { formAddEditUserDataAccess.submit(); }, function () { return false; })
                        return false;
                    }
                })

                return formAddEditUserDataAccess;
            }).on('hidden.bs.modal', function () {
                Global.ModalClear($(this));
                $("#BranchesId").val("");
                $("#CharityGroupNotAccess, #CharityGroupAccess").html("").multipleSelect("refresh");
            });
        }

        function initializeMultiselectDropdown() {
            $('#CharityGroupNotAccess').multipleSelect({
                width: '100%',
                placeholder: "Select Charity or Branches",
                onClose: function () {
                    if ($('#CharityGroupNotAccess').val() != null && $('#CharityGroupNotAccess').val().length > 0) {
                        $.each($('#CharityGroupNotAccess').val(), function (index, value) {
                            var option = $('#CharityGroupNotAccess option').filter("[value='" + value + "']");
                            var optgroup = $(option).parent();
                            if ($('#CharityGroupAccess optgroup').filter("[label='" + $(optgroup).attr("label") + "']").length > 0) {
                                $("#CharityGroupAccess optgroup[label='" + $(optgroup).attr("label") + "']").append(option);
                            }
                            else {
                                $("<optgroup></optgroup>").attr("label", $(optgroup).attr("label")).appendTo($('#CharityGroupAccess'));
                                $("#CharityGroupAccess optgroup[label='" + $(optgroup).attr("label") + "']").append(option);
                                //$("<optgroup></optgroup>").attr("value", $(optgroup).attr("value")).attr("label", $(optgroup).attr("label")).appendTo($('#CharityGroupAccess'));
                                //$("#CharityGroupAccess optgroup[value='" + $(optgroup).attr("value") + "']").append(option);
                            }
                            $("#CharityGroupNotAccess option").filter("[value='" + value + "']").remove();
                            $("#CharityGroupNotAccess optgroup").filter(function () {
                                return ($(this).find("option").length == 0);
                            }).remove();
                        });
                        $('#CharityGroupNotAccess, #CharityGroupAccess').multipleSelect("refresh");
                    }
                },
            });

            $('#CharityGroupAccess').multipleSelect({
                width: '100%',
                placeholder: "Select Charity or Branches",
                onClose: function () {
                    if ($('#CharityGroupAccess option:not(:selected)').length > 0) {
                        $.each($('#CharityGroupAccess option:not(:selected)'), function (index, obj) {
                            var optgroup = $(obj).parent();
                            if ($('#CharityGroupNotAccess optgroup').filter("[label='" + $(optgroup).attr("label") + "']").length > 0) {
                                $("#CharityGroupNotAccess optgroup[label='" + $(optgroup).attr("label") + "']").append(obj);
                            }
                            else {
                                $("<optgroup></optgroup>").attr("label", $(optgroup).attr("label")).appendTo($('#CharityGroupNotAccess'));
                                $("#CharityGroupNotAccess optgroup[label='" + $(optgroup).attr("label") + "']").append(obj);
                            }

                            $("#CharityGroupAccess option").find("[value='" + $(obj).val() + "']").remove();
                            $("#CharityGroupAccess optgroup").filter(function () {
                                return ($(this).find("option").length == 0);
                            }).remove();
                        });
                        $("#CharityGroupNotAccess, #CharityGroupAccess").multipleSelect("refresh");
                    }
                },
            });
        }

        function ControlVisibilityChangePassword() {
            if ($("#IsPasswordChange").is(":checked"))
                $("#change-password-section").show();
            else
                $("#change-password-section").hide();
        }

        $this.init = function () {
            initializeGrid();
            initializeModalWithForm();
            initializeUserDataAccessModalWithForm();
            initializeOrgCharityFilter();
        };

    }

    $(function () {
        var self = new UserIndex();
        self.init();
    });

}(jQuery));