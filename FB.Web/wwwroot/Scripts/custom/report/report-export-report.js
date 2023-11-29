function ExportReport() {
    var $this = this, frmExportReport;

    function initializeModalWithForm() {

        $('#hdnexportbtn').val('');

        $('#btnExportReportSubmit').on("click", function () {
            $('#hdnexportbtn').val('');
        })

        frmExportReport = new Global.FormHelper($("#frm-export-report form"), { updateTargetId: "validation-summary" }, function (result) {
            if (result.isSuccess == true) {
                if (result.data != "") {
                    window.location.href = Global.DomainName + "Report/report/downloadfile?key=" + result.data;
                }
                else {
                    Global.Alert("Error!", "there are some issues to download report, please contact to technical support team.");
                }
            }
            else {
                Global.Alert("Error!", result.data);
            }
        });

        $('#btnPreview').on("click", function () {
            $('#hdnexportbtn').val('btnPreview');
            var submitBtn = frmExportReport.find('#btnPreview');
            submitBtn.find('i').removeClass("fa fa-arrow-circle-right");
            submitBtn.find('i').addClass("fa fa-spinner fa-spin");
            submitBtn.prop('disabled', true);
            submitBtn.find('span').html('Submiting..');
            var form = frmExportReport.serialize();

            $.ajax({
                url: Global.DomainName + "Report/report/export",
                type: "post",
                data: form,
                dataType: 'json',
                success: function (result) {
                    if (result.isSuccess) {
                        if (result.data != "") {
                            window.open(result.data);
                        }
                        else {
                            Global.Alert("Error!", "there are some issues to genrate preview, please contact to technical support team.");
                        }
                    }
                    else {
                        Global.Alert("Error!", result.data);
                    }
                },
                error: function (result) {
                },
                complete: function () {
                    submitBtn.find('i').removeClass("fa fa-spinner fa-spin");
                    submitBtn.find('i').addClass("fa fa-arrow-circle-right");
                    submitBtn.find('span').html('Submit');
                    submitBtn.prop('disabled', false);
                }
            });
        })
    }



    function initializeModalWithFormforRole() {

        $('#getTital').on("click", function () {
            var date = $(this).val();
            var tital = date;
            $("#TitalName").val(tital);
        });
        $('#hdnexportbtn').val('');

        $('#btnExportReportSubmit').off().on("click", function () {
            $('#hdnexportbtn').val('btnDownload');
            if ($("#TitalName").val() != "" && $("#FileName").val() != "") {
                frmExportReport = new Global.FormHelper($(".cmxform form"))
                $('#hdnexportbtn').val('btnDownload');
                var submitBtn = frmExportReport.find('#btnExportReportSubmit');
                submitBtn.find('i').removeClass("fa fa-arrow-circle-right");
                submitBtn.find('i').addClass("fa fa-spinner fa-spin");
                submitBtn.prop('disabled', true);
                submitBtn.find('span').html('Submiting..');
                var form = frmExportReport.serialize();
                $.ajax({
                    url: Global.DomainName + "Report/report/export",
                    type: "post",
                    data: form,
                    dataType: 'json',
                    success: function (result) {
                        if (result.isSuccess) {
                            if (result.data != "") {
                                window.location.href = Global.DomainName + "Report/report/downloadfile?key=" + result.data;
                            }
                            else {
                                Global.Alert("Error!", "there are some issues to download report, please contact to technical support team.");
                            }
                        }
                        else {
                            Global.Alert("Error!", result.data);
                        }
                    },
                    error: function (result) {
                    },
                    complete: function () {
                        submitBtn.find('i').removeClass("fa fa-spinner fa-spin");
                        submitBtn.find('i').addClass("fa fa-arrow-circle-right");
                        submitBtn.find('span').html('Submit');
                        submitBtn.prop('disabled', false);
                    }
                });
            }
            else
            {
                if ($("#TitalName").val() == "" && $("#FileName").val() == "") {
                    Global.Alert("Error!", "Please Enter a title & file Name.");
                }
                else if ($("#TitalName").val() == "") {
                    Global.Alert("Error!", "Please Enter a title.");
                }else if ($("#FileName").val() == "") {
                    Global.Alert("Error!", "Please Enter a File Name.");
                }
            }
           
        })

        $('#btnPreview').off().on("click", function () {
            $('#btnPreview').dblclick(false);
            frmExportReport = new Global.FormHelper($(".cmxform form"))
            $('#hdnexportbtn').val('btnPreview');
            var submitBtn = frmExportReport.find('#btnPreview');
            submitBtn.find('i').removeClass("fa fa-arrow-circle-right");
            submitBtn.find('i').addClass("fa fa-spinner fa-spin");
            submitBtn.prop('disabled', true);
            submitBtn.find('span').html('Submiting..');
            var form = frmExportReport.serialize();
            $.ajax({
                url: Global.DomainName + "Report/report/export",
                type: "post",
                data: form,
                dataType: 'json',
                success: function (result) {
                    if (result.isSuccess) {
                        if (result.data != "") {
                            window.open(result.data);
                        }
                        else {
                            Global.Alert("Error!", "there are some issues to genrate preview, please contact to technical support team.");
                        }
                    }
                    else {
                        Global.Alert("Error!", result.data);
                    }
                },
                error: function (result) {
                },
                complete: function () {
                    submitBtn.find('i').removeClass("fa fa-spinner fa-spin");
                    submitBtn.find('i').addClass("fa fa-arrow-circle-right");
                    submitBtn.find('span').html('Submit');
                    submitBtn.prop('disabled', false);
                }
            });
        })
    }


    $this.SetModelPopup = function (key, other) {
        $("#modal-export-report").off("loaded.bs.modal");
        $("#modal-export-report").on('loaded.bs.modal', function () {
            $("#Key").val(key);
            $("#FileName").val(key);
            if (other != undefined && other != null)
                $("#Other").val(other);
            initializeModalWithForm();
        }).on('hidden.bs.modal', function () {
            //if ($('#Key').val() == "SimpleListofPeople" || $('#Key').val() == "SimpleListofPeopleLabel" ) {
            //    $.get(Global.DomainName + "report/cleardata?key=" + $('#Key').val());
            //    Global.ModalClear($(this));
            //    window.location.href = Global.DomainName + "Report/SimpleListofPeople";
            //}
            //else if ($('#Key').val() == "SimpleListofFamily" || $('#Key').val() == "SimpleListofFamilyLabel") {
            //    $.get(Global.DomainName + "report/cleardata?key=" + $('#Key').val());
            //    Global.ModalClear($(this));
            //    window.location.href = Global.DomainName + "Report/SimpleListofFamily";
            //}
            //else
                Global.ModalClear($(this));
        });
    };

    $this.SetRolePage = function (key, other) {
        $("#Key").val(key);
        $("#FileName").val(key);
        if (other != undefined && other != null)
            $("#Other").val(other);
        initializeModalWithFormforRole();
    };

}