/*global jQuery, Global*/


(function ($) {
    'use strict';

    function PersonIndex() {

        var $this = this, formAddPerson, personGrid, personId, formEditPerson, isSaveClick, isPreview,
            noteGrid, agencyGrid,voucherGrid,myReferralGridfamily,familyparcelGrid;

        function initializeDatePicker() {
            $(".datepicker").datepicker({
                format: "dd/mm/yyyy"
            }).on('hide', function (e) {
                // set focus outss
                $(".datepicker").blur();
            });

            $(".datepicker").inputmask("dd/mm/yyyy", { "placeholder": "dd/mm/yyyy" });

            $(".date-today").on("click", function () {
                var date = new Date();
                var currentDate = new Date(date.getYear(), date.getMonth(), date.getDate());
                var todaydate = date.getDate() + "/" + (date.getMonth() + 1) + "/" + date.getFullYear();
                $(this).closest(".form-group").find("input[type=text]").datepicker('setDate', todaydate);
                $(this).closest(".form-group").find("input[type=text]").datepicker('update');
            });
            initializeTimePicker();
        }

        function initializeTimePicker() {
            $('.timepicker').inputmask("hh:mm", {
                placeholder: "HH:MM",
                insertMode: false,
                showMaskOnHover: false,
                hourFormat: "24"
            });
        }

        function attachEventCKEditor(instance) {
            CKEDITOR.instances[instance].on("instanceReady", function () {
                this.on("change", function () {
                    CKEDITOR.instances[instance].updateElement();
                });
            });

          
        }

        function relationshipMemberSelectEvent() {
            
        }

        function relationshipMemberselectGridEvent() {
          


        }

        function initializeCreatePersonModalWithForm() {


            $('.modal').on('hidden.bs.modal', function (e) {
                if ($('.modal').hasClass('in')) {
                    $('body').addClass('modal-open');
                }
            });

            $(".modal").removeAttr("tabindex");

          
            


          

          


            
            
          


            

           

           

          

           
            $("#modal-edit-person-name").on('loaded.bs.modal', function () {
           
           
                formUpdatePersonName = new Global.FormHelper($("#frm-edit-person-name form"),
                    { updateTargetId: "validation-summary-update-person-name", validateSettings: { ignore: "" } },
                    function (result) {
               
                    
                        if (result.isSuccess) {
                            alertify.dismissAll();
                            Global.ShowMessage(result.data, Global.MessageType.Success);
                            var updatedPerson = $("#PersonID").val();
                           
                            $(".img-loading-div").hide();
                           
                                editGetPerson(updatedPerson);
                           

                            $("#modal-edit-person-name").modal("hide");
                            Global.ModalClear($("#modal-edit-person-name"));
                        } else {
                            alertify.dismissAll();
                            Global.ShowMessage(result.data, Global.MessageType.Error);
                        }

                    });
            }).on('hidden.bs.modal', function () {
                Global.ModalClear($(this));
              
            });
            $("#modal-feedback-viewdetails").on('loaded.bs.modal', function () {

            }).on('hidden.bs.modal', function () {
                Global.ModalClear($(this));
            });
            $("#modal-feedback-viewdetails").on('hidden.bs.modal', function () {
                $(this).removeData('bs.modal');
            });
            $("#modal-edit-familyaddress").on('loaded.bs.modal', function () {
              
                formUpdatePersonName = new Global.FormHelper($("#frm-edit-person-name form"),
                    { updateTargetId: "validation-summary-update-person-name", validateSettings: { ignore: "" } },
                    function (result) {
                 
                       
                        if (result.isSuccess) {
                            alertify.dismissAll();
                            Global.ShowMessage(result.data, Global.MessageType.Success);
                        var updatedPerson = $("#PersonID").val();
                        
                            $(".img-loading-div").hide();
                  
                      /*  setTimeout(function () {*/
                            editGetPerson(updatedPerson);
                    /*    }, 600);*/

                            $("#modal-edit-familyaddress").modal("hide");
                            Global.ModalClear($("#modal-edit-familyaddress"));
                        }
                        else {
                            alertify.dismissAll();
                            Global.ShowMessage(result.data, Global.MessageType.Error);
                        }

                    });

                    
            }).on('hidden.bs.modal', function () {
                Global.ModalClear($(this));
            });
            $("#modal-edit-familymember").on('loaded.bs.modal', function () {
              
                formUpdatePersonName = new Global.FormHelper($("#frm-edit-person-name form"),
                    { updateTargetId: "validation-summary-update-person-name", validateSettings: { ignore: "" } },
                    function (result) {
                      
                        if (result.isSuccess) {
                            alertify.dismissAll();
                            Global.ShowMessage(result.data, Global.MessageType.Success);
                            var updatedPerson = $("#PersonID").val();
                          
                            $(".img-loading-div").hide();
                          
                          /*  setTimeout(function () {*/
                                editGetPerson(updatedPerson);
                           /* }, 600);*/

                            $("#modal-edit-familymember").modal("hide");
                            Global.ModalClear($("#modal-edit-familymember"));
                        }
                        else {
                            alertify.dismissAll();
                            Global.ShowMessage(result.data, Global.MessageType.Error);
                        }

                    });
            }).on('hidden.bs.modal', function () {
                Global.ModalClear($(this));
            });
           

            $("#modal-create-edit-note").on('loaded.bs.modal', function () {
                initializeDatePicker();

                CKEDITOR.replace('Comment');
                attachEventCKEditor('Comment');

                CKEDITOR.on('dialogDefinition', function (evt) {
                    var dialog = evt.data;

                    if (dialog.name == 'table') {
                        // Get dialog definition.
                        var def = evt.data.definition;

                        def.onShow = function () {
                            var select = this.getContentElement('info', 'selHeaders');
                            select.disable();
                        }
                    }
                });

                formCreateEditNote = new Global.FormHelper($("#frm-create-edit-note form"),
                    { updateTargetId: "validation-summary-note", validateSettings: { ignore: "" } },
                    function (result) {
                        if (result.isSuccess) {
                            reinitializeNoteTabGrid();
                            $("#modal-create-edit-note").modal("hide");
                            Global.ModalClear($("#modal-create-edit-note"));
                            Global.ShowMessage(result.data, Global.MessageType.Success);
                        } else {
                            Global.ShowMessage(result.data, Global.MessageType.Error);
                        }
                    });
            }).on('hidden.bs.modal', function () {
                Global.ModalClear($(this));
            });

            $("#modal-delete-note").on('loaded.bs.modal', function () {
                formDeleteNote = new Global.FormDeleteHelper($("#frm-delete form"), {}, function (result) {
                    $("#modal-delete-note").modal("hide");
                    if (result.indexOf("Success") > -1) {
                        reinitializeNoteTabGrid();
                        alertify.dismissAll();
                        Global.ShowMessage("selected note deleted successfully.", Global.MessageType.Success);
                    }
                    else {
                        alertify.dismissAll();
                        Global.ShowMessage(result, Global.MessageType.Error);
                    }
                   
                });
            }).on('hidden.bs.modal', function () {
                Global.ModalClear($(this));
            });

            $("#modal-delete-family").on('loaded.bs.modal', function () {
                formDeleteNote = new Global.FormDeleteHelper($("#frm-delete form"), {}, function (result) {
                    $("#modal-delete-family").modal("hide");
                    if (result.indexOf("Success") > -1) {
                        alertify.dismissAll();
                        Global.ShowMessage('Family deleted successfully', Global.MessageType.Success);
                        var updatedPerson = $("#PersonID").val();

                        $(".img-loading-div").hide();

                        editGetPerson(updatedPerson);
                    }
                    else {
                        alertify.dismissAll();
                        Global.ShowMessage(result.data, Global.MessageType.Error);
                    }
                });
            }).on('hidden.bs.modal', function () {
                Global.ModalClear($(this));
            });
            $("#modal-accept-family").on('loaded.bs.modal', function () {
                formDeleteNote = new Global.FormDeleteHelper($("#frm-delete form"), {}, function (result) {
                    $("#modal-accept-family").modal("hide");
                    if (result.indexOf("Success") > -1) {
                        alertify.dismissAll();
                        Global.ShowMessage('Family accepted successfully', Global.MessageType.Success);
                        var updatedPerson = $("#PersonID").val();

                        $(".img-loading-div").hide();

                        editGetPerson(updatedPerson);
                    }
                    else {
                        alertify.dismissAll();
                        Global.ShowMessage(result.data, Global.MessageType.Error);
                    }
                });
            }).on('hidden.bs.modal', function () {
                Global.ModalClear($(this));
            });
            $("#modal-reject-family").on('loaded.bs.modal', function () {
                formDeleteNote = new Global.FormDeleteHelper($("#frm-delete form"), {}, function (result) {
                    $("#modal-reject-family").modal("hide");
                    if (result.indexOf("Success") > -1) {
                        alertify.dismissAll();
                        Global.ShowMessage('Family rejected successfully', Global.MessageType.Success);
                        var updatedPerson = $("#PersonID").val();

                        $(".img-loading-div").hide();

                        editGetPerson(updatedPerson);
                    }
                    else {
                        alertify.dismissAll();
                        Global.ShowMessage(result.data, Global.MessageType.Error);
                    }
                });
            }).on('hidden.bs.modal', function () {
                Global.ModalClear($(this));
            });


           


          


          



            formEditPerson = new Global.FormHelper($("#form-edit-person"), { updateTargetId: "validation-summary" }, function (result) {
                if (result.isSuccess) {

                    if (autoSaveDonor == 'True' && personId > 0) {
                        editGetPerson(personId);
                    } else {
                        editGetPerson(result.data);
                    }

                    Global.ShowMessage("Record Saved Successfully", Global.MessageType.Success);
                }
                else {
                    if (result.data != undefined && result.data != "") {
                        setTimeout(function () { $(".img-loading-div").hide(); Global.ShowMessage(result.data, Global.MessageType.Error); }, 2000)
                    }
                }
            });
        }

       
       

       

   

        

        

        function initializePersonEvents() {

            ControlVisibilityMGOReference();

            $("div#frm-create-person #IsMGO").on("click", function () {
                ControlVisibilityMGOReference();
            });

            $(".select2-required").select2();

            if ($("div#frm-create-person #CentralOfficeID option").length > 1) {
                $('div#frm-create-person #CentralOfficeID').val($('#hdnCentralOfficeID').val());
                $("div#frm-create-person #CentralOfficeID").select2("destroy");
                $("div#frm-create-person #CentralOfficeID").select2();
                setTimeout(function () { $("div#frm-create-person #CentralOfficeID").change(); }, 100);
            }

            if ($("div#frm-create-person #CharityID option").length > 0) {
                $('div#frm-create-person #CharityID').val($('#CharityFilter').val());
                $("div#frm-create-person #CharityID").select2("destroy");
                $("div#frm-create-person #CharityID").select2();
                setTimeout(function () { $("div#frm-create-person #CharityID").change(); }, 100);
            }

            if ($("div#frm-create-person #BranchID option").length > 0) {
                $('div#frm-create-person #BranchID').val($('#BranchFilter').val());
                $("div#frm-create-person #BranchID").select2("destroy");
                $("div#frm-create-person #BranchID").select2();
                setTimeout(function () { $("div#frm-create-person #BranchID").change(); }, 100);
            }


            if ($("div#frm-create-person #CharityID option").length && $('div#frm-create-person #CharityID option').length == 2) {
                $('div#frm-create-person #CharityID').val($('div#frm-create-person #CharityID option:last').val());
                setTimeout(function () { $("div#frm-create-person #CharityID").change(); }, 100);
                $('div#frm-create-person #CharityID option:eq(0)').remove();
            }

            if ($('div#frm-create-person #BranchID option').length && $('div#frm-create-person #BranchID option').length == 1) {
                $('div#frm-create-person #BranchID').val($('div#frm-create-person #BranchID option:last').val());
                setTimeout(function () { $("div#frm-create-person #BranchID").change(); }, 100);
            }
            else if ($('div#frm-create-person #BranchID option').length && $('div#frm-create-person #BranchID > option:selected').length > 0) {
                setTimeout(function () { $("div#frm-create-person #BranchID").change(); }, 100);
            }

            if ($("div#frm-create-person #CharityID option").length) {
                $("div#frm-create-person #CharityID").select2();
            }

            $('div#frm-create-person .postcode').on("focusout", function () {
                if ($(this).val() !== "") {
                    SearchBegin(this);
                }
                else {
                    $(this).parent().find("div#frm-create-person label.errormsg").text("");
                }
            });

            $('div#frm-create-person .postcode').on("keyup", function () {
                if ($(this).val() == "") {
                    $(this).parent().find("div#frm-create-person label.errormsg").text("");
                }
            });

            $('div#frm-create-person .postcode').trigger("focusout");
            setTimeout(function () { $("div#frm-create-person #Title").focus(); }, 500); //changes on 4-8-2016

            $('#Title').bind('keypress', function (event) {
                switch (event.keyCode) {
                    case 8:  // Backspace
                    case 9:  // Tab
                    case 13: // Enter
                    case 32: // space
                    case 37: // Left
                    case 38: // Up
                    case 39: // Right
                    case 40: // Down
                    case 46: // delete
                    case 110: // decimal point
                    case 190: // period
                        break;
                    default:
                        var regex = new RegExp("^[a-zA-Z0-9]+$");
                        var key = event.key;
                        if (!regex.test(key)) {
                            event.preventDefault();
                            return false;
                        }
                        break;
                }
            });

            $("#Forenames").on("blur", function () {
                var foreName = $(this).val();
                if (foreName != "") {
                    var initails = "";
                    $.each(foreName.split(' '), function (index, value) {
                        initails = initails + value.substring(0, 1) + " ";
                    });

                    $("#Initials").val(initails.toUpperCase());
                    Global.capitalize("Forenames", $("#Forenames").val());
                }
            });

            $("#Title").on("blur", function () {
                Global.capitalize("Title", $("#Title").val());
            });

            $("#Surname").on("blur", function () {
                Global.capitalize("Surname", $("#Surname").val());
            });

            $("#Middlename").on("blur", function () {
                Global.capitalize("Middlename", $("#Middlename").val());
            });


            $("div#frm-create-person #CentralOfficeID").off("change").on("change", function () {
                $("div#frm-create-person #CharityID").html("<option value=''>Select Charity</option>");
                $("div#frm-create-person #CharityID").select2("destroy");
                $("div#frm-create-person #CharityID").select2();
                $("div#frm-create-person #BranchID").html("<option value=''>Select Branch</option>");
                $("div#frm-create-person #BranchID").select2("destroy");
                $("div#frm-create-person #BranchID").select2();
                if ($(this).val() != "") {
                    var html = "<option value=''>Select Charity</option>";
                    $.get(Global.DomainName + "FoodBank/FamilyRecord/bindcharities", { organisationID: $(this).val() }, function (data) {
                        if (data.data.length > 1) {
                            $.each(data.data, function (index, item) {
                                html = html + "<option value='" + item.value + "'>" + item.text + "</option>";
                            });
                            $("div#frm-create-person #CharityID").html(html);

                            if ($("div#frm-create-person #CharityID option").length > 0) {
                                $('div#frm-create-person #CharityID').val($('#CharityFilter').val());
                                $("div#frm-create-person #CharityID").select2("destroy");
                                $("div#frm-create-person #CharityID").select2();
                                setTimeout(function () { $("div#frm-create-person #CharityID").change(); IsMGOBranch(); }, 100);
                            }
                        }
                        else {
                            $.each(data.data, function (index, item) {
                                html = "<option value='" + item.value + "'>" + item.text + "</option>";
                                $.get(Global.DomainName + "FoodBank/FamilyRecord/bindbranches", { charityID: item.value }, function (data) {
                                    var branchhtml = "";
                                    $.each(data.data, function (index, item) {
                                        branchhtml = branchhtml + "<option value='" + item.value + "'>" + item.text + "</option>";
                                    });
                                    $("div#frm-create-person #BranchID").html(branchhtml);
                                    $("div#frm-create-person #BranchID").select2();
                                    IsMGOBranch();
                                    //$("div#frm-create-person #CharityGroup").html(branchhtml).multipleSelect("refresh");
                                    //$("div#frm-create-person #BranchIDs").val("");

                                    if ($("div#frm-create-person #BranchID option").length > 0) {
                                        $('div#frm-create-person #BranchID').val($('#BranchFilter').val());
                                        $("div#frm-create-person #BranchID").select2("destroy");
                                        $("div#frm-create-person #BranchID").select2();
                                        setTimeout(function () { $("div#frm-create-person #BranchID").change(); IsMGOBranch(); }, 100);
                                    }
                                });
                            });
                            $("div#frm-create-person #CharityID").html(html);
                            $("div#frm-create-person #CharityID").select2();
                        }
                    });


                }
            });

            $("div#frm-create-person #CharityID").off("change").on("change", function () {
                $("div#frm-create-person #BranchID").html("<option value=''>Select Branch</option>");
                $("div#frm-create-person #BranchID").select2("destroy");
                $("div#frm-create-person #BranchID").select2();
                if ($(this).val() != "") {
                    $.get(Global.DomainName + "FoodBank/FamilyRecord/bindbranches", { charityID: $(this).val() }, function (data) {
                        var html = "<option value=''>Select Branch</option>";
                        $.each(data.data, function (index, item) {
                            html = html + "<option value='" + item.value + "'>" + item.text + "</option>";
                        });

                        $("div#frm-create-person #BranchID").html(html);
                        if ($('div#frm-create-person #BranchID option').length == 2) {
                            $('div#frm-create-person #BranchID option:eq(0)').remove();
                            $('div#frm-create-person #BranchID').val($('div#frm-create-person #BranchID option:first').val());
                            $("div#frm-create-person #BranchID").select2();
                            IsMGOBranch();

                        } else {
                            if ($("div#frm-create-person #BranchID option").length > 0) {
                                $('div#frm-create-person #BranchID').val($('#BranchFilter').val());
                                $("div#frm-create-person #BranchID").select2("destroy");
                                $("div#frm-create-person #BranchID").select2();
                                setTimeout(function () { $("div#frm-create-person #BranchID").change(); IsMGOBranch(); }, 100);
                            }
                        }

                    });
                }
            });

        

        }

        function IsMGOBranch() {
          

        }

       

        function initializeGrid() {
            //$("#skill-certificate-sections").hide();
       //     
            personGrid = new Global.GridAjaxHelper('#grid-person', {
                "bLengthChange": false,
                "scrollY": "1000px",
                "scrollCollapse": true,
                "oLanguage": { "sSearch": "" },
                "paging": false,
                "aoColumns": [{
                    "sName": "Family.FamilyName", "mRender": function (data, type, row) {
                        return row[2] + ' <span class="rowChangeFilter" id=' + row[1] + '>' + row[0] + '</span>';
                    }
                }],
                "aoColumnDefs": [{ 'visible': false, 'aTargets': [1] }],
                "bDestroy": true,
            }, "FoodBank/FamilyRecord/getpersons" + ($("#hdnCentralOfficeID").val() === "0" ? "/0" : "/" + $("#hdnCentralOfficeID").val()), function () {
                $("#grid-person tbody").off('click', 'tr');
                $("#grid-person tbody").on('click', 'tr', function (event, isSubmit) {
                    if ($("#form-edit-person").valid() == false) {
                        return;
                    }
                    isSaveClick = false;
                    isPreview = false;
                    personId = $('td span', this).attr('id');
                    $("#PersonIDNew").val(personId);
                    $(this).closest('table').find('tr').removeClass("bgcolorgreen");
                    $('#grid-person tr').removeClass('bgcolorgreen');
                    $(this).addClass('bgcolorgreen');
                    editGetPerson(personId);
                    //userfieldsTabEventfeedback();
                    //userfieldsvoucherTabEvent();
                    //noteTabEvent();
                    //agencyTabEvent();
                    //pacelsTabEvent();
                    if (isSubmit !== false && autoSaveDonor == 'True') {
                        $("#form-edit-person").submit();
                        //if (ValidateReferenceType()) {
                        //    $("#form-edit-person").submit();
                        //    //$(".img-loading-div").hide();
                        //}
                        //else {
                        //    $(".img-loading-div").hide();
                        //}
                    }
                    else
                    {
                        editGetPerson(personId);
                        //userfieldsTabEventfeedback();
                        //userfieldsvoucherTabEvent();
                        //noteTabEvent();
                        //agencyTabEvent();
                        //pacelsTabEvent();
                    }


                });

                $("#grid-person_filter input").attr("placeholder", "Enter Name");
                if ($("#hdnPersonID").val() != "") {
                    if ($("#grid-person tbody").find("span[id='" + $("#hdnPersonID").val() + "']").length > 0) {
                        $('#grid-person').find('tr').removeClass("bgcolorgreen");
                        $("#grid-person tbody").find("span[id='" + $("#hdnPersonID").val() + "']").closest("tr").addClass("bgcolorgreen");
                        $("#grid-person tbody").find("span[id='" + $("#hdnPersonID").val() + "']").closest("tr").trigger("click", [false]);
                        $("#hdnPersonID").val("");
                    }
                    else {

                        if ($("#grid-person tbody").find(".dataTables_empty").length > 0) {
                            $("#frm-edit").html("<h2>No person available.</h2>");
                            $('.create-person[data-target="#modal-person"]').attr("data-target", "#modal-create-person").attr("href", Global.DomainName + "FoodBank/FamilyRecord/CreatePersonFamily/");
                            $("#skill-certificate-sections").hide();

                        } else {
                            personId = $("#grid-person tbody tr:first td span").attr("id");
                            editGetPerson(personId);
                        }

                    }
                }
                else {
                    if ($("#grid-person tbody tr:first td span").length > 0) {
                        if (personId != null) {
                            if ($("#grid-person tbody").find("span[id='" + personId + "']").length > 0) {
                                $('#grid-person').find('tr').removeClass("bgcolorgreen");
                                $("#grid-person tbody").find("span[id='" + personId + "']").closest("tr").addClass("bgcolorgreen");
                                $("#grid-person tbody").find("span[id='" + personId + "']").closest("tr").trigger("click", [false]);
                            }
                            else {
                                if ($("#grid-person tbody").find(".dataTables_empty").length > 0) {
                                    $("#frm-edit").html("<h2>No person available.</h2>");
                                    $('.create-person[data-target="#modal-person"]').attr("data-target", "#modal-create-person").attr("href", Global.DomainName + "FoodBank/FamilyRecord/CreatePersonFamily/");
                                    $("#skill-certificate-sections").hide();

                                } else {
                                    personId = $("#grid-person tbody tr:first td span").attr("id");
                                    editGetPerson(personId);
                                }

                            }
                        }
                        else {

                            if ($("#grid-person tbody").find(".dataTables_empty").length > 0) {
                                $("#frm-edit").html("<h2>No person available.</h2>");
                                $('.create-person[data-target="#modal-person"]').attr("data-target", "#modal-create-person").attr("href", Global.DomainName + "FoodBank/FamilyRecord/CreatePersonFamily/");
                                $("#skill-certificate-sections").hide();

                            } else {
                                personId = $("#grid-person tbody tr:first td span").attr("id");
                                editGetPerson(personId);
                            }

                        }
                    }
                    else {
                        $("#frm-edit").html("<h2>No person available.</h2>");
                        $('.create-person[data-target="#modal-person"]').attr("data-target", "#modal-create-person").attr("href", Global.DomainName + "FoodBank/FamilyRecord/CreatePersonFamily/");
                        $("#skill-certificate-sections").hide();
                    }
                }

                $("#grid-person_filter").css("float", "left");
                $("#grid-person_filter").parent().css("width", "100%");
            });

            personGrid.on('search.dt', function () {
                Global.DataServer.dataURL = "FoodBank/FamilyRecord/getpersons" + ($("#hdnCentralOfficeID").val() === "0" ? "/0" : "/" + $("#hdnCentralOfficeID").val());
              
                //Global.DataServer.multisearch = [];
                //Global.DataServer.dataURL = '';
            });
        }

        function editGetPerson(personid, callback) {
            //
            if (personid) {
                $(".img-loading-div").hide();
                $('#grid-person').find('tr').removeClass("bgcolorgreen");
                $("#grid-person tbody").find("span[id='" + personid + "']").closest("tr").addClass("bgcolorgreen");
                //
                $.ajax({
                    url: Global.DomainName + "FoodBank/FamilyRecord/editperson",
                    data: { id: personid },
                    type: "Get",
                    success: function (data) {
                     //   
                        $("#frm-edit").html(data);
                        $('#PersonID').val(personid);
                    
                        //reinitializeNoteTabGrid();
                        userfieldsTabEventfeedback();
                        userfieldsvoucherTabEvent();
                        noteTabEvent();
                        agencyTabEvent();
                        pacelsTabEvent();
                      
                     
                    },
                    complete: function () {
                        //
                       
                        setTimeout(function () { $(".img-loading-div").hide(); }, 200)
                    }
                });
            }
        }

        function personalTabEvent() {
           
        }

        function detailsTabEvent() {

            $("#form-edit-person #EditIsMGO").on("click", function () {
                ControlVisibilityMGO();
            });

            $(document).off('click', '#editPersonDonarLastReference').on('click', '#editPersonDonarLastReference', function () {
                var referenceNumber = $(this).data("ref");
                Global.CopyToClipboard(referenceNumber, "#form-edit-person");
            });

            $("#form-edit-person #LastReference").off("click").on("click", function () {
                var branchvalue = $("#form-edit-person #EditBranchID").val();

                if (branchvalue == "") {
                    Global.Alert("Warning !", "Please select a branch.");
                }
                else {
                    var $thisBtn = $(this);
                    $thisBtn.find('i').removeClass("fa fa-arrow-circle-right");
                    $thisBtn.find('i').addClass("fa fa-spinner fa-spin");
                    $thisBtn.find('i').prop('disabled', true);

                }
            });

            $('input[type=checkbox][name=IsVisitingTeamMember]').change(function () {
                if ($('input[type=checkbox][name=IsVisitingTeamMember]:checked').val() != "true") {
                    $("#VisitorComments").attr("readonly", true);
                } else {
                    $("#VisitorComments").removeAttr("readonly");
                }
            });

            if ($('input[type=checkbox][name=IsVisitingTeamMember]:checked').val() != "true") {
                $("#VisitorComments").attr("readonly", true);
            } else {
                $("#VisitorComments").removeAttr("readonly");
            }

            $("#btn-reactivate-member").off("click").on("click", function () {
                $(".img-loading-div").show();
                $.ajax({
                    url: Global.DomainName + "FoodBank/FamilyRecord/reactivatemember",
                    data: { id: $(this).attr('data-memberId') },
                    type: "Post",
                    success: function (result) {
                        if (result.isSuccess) {
                            editGetPerson($("#PersonID").val());
                            Global.ShowMessage(result.data, Global.MessageType.Success);
                        } else {
                            Global.ShowMessage(result.data, Global.MessageType.Error);
                        }
                    },
                    complete: function () {
                        $(".img-loading-div").hide();
                    }
                });
            });


            if ($("#EditCharityID option").length && $('#EditCharityID option').length == 2) {
                $('#EditCharityID').val($('#EditCharityID option:last').val());
                setTimeout(function () { $("#EditCharityID").change(); }, 100);
                $('#EditCharityID option:eq(0)').remove();
            }

            if ($('#EditBranchID option').length && $('#EditBranchID option').length == 1) {
                $('#EditBranchID').val($('#EditBranchID option:last').val());
                setTimeout(function () { $("#EditBranchID").change(); }, 100);
            }
            else if ($('#EditBranchID option').length && $('#EditBranchID > option:selected').length > 0) {
                setTimeout(function () { $("#EditBranchID").change(); }, 100);
            }

            if ($("#EditCharityID option").length) {
                $("#EditCharityID").select2();
            }

            if ($("#EditBranchID option").length) {
                $("#EditBranchID").select2();
            }


            $("#EditCentralOfficeID").off("change").on("change", function () {
                $("#EditCharityID").html("<option value=''>Select Charity</option>");
                if ($("#EditCharityID").data('select2')) {
                    $("#EditCharityID").select2("destroy");
                }
                $("#EditCharityID").select2();
                $("#EditBranchID").html("<option value=''>Select Branch</option>");
                if ($("#EditBranchID").data('select2')) {
                    $("#EditBranchID").select2("destroy");
                }
                $("#EditBranchID").select2();
                $("#MemberShipType").html("<option value=''>Select Membership Type</option>");
                if ($("#MemberShipType").data('select2')) {
                    $("#MemberShipType").select2("destroy");
                }
                $("#MemberShipType").select2();
                if ($(this).val() != "") {
                    var html = "<option value=''>Select Charity</option>";
                    $.get(Global.DomainName + "FoodBank/FamilyRecord/bindcharities", { organisationID: $(this).val() }, function (data) {
                        if (data.data.length > 1) {
                            $.each(data.data, function (index, item) {
                                html = html + "<option value='" + item.value + "'>" + item.text + "</option>";
                            });
                            $("#EditCharityID").html(html);
                        }
                        else {
                            html = "<option value=''>Select Branch</option>";
                            $.each(data.data, function (index, item) {
                                html = "<option value='" + item.value + "'>" + item.text + "</option>";
                                $.get(Global.DomainName + "FoodBank/FamilyRecord/bindbranches", { charityID: item.value }, function (data) {
                                    var branchhtml;
                                    $.each(data.data, function (index, item) {
                                        branchhtml = branchhtml + "<option value='" + item.value + "'>" + item.text + "</option>";
                                    });
                                    $("#EditBranchID").html(branchhtml);
                                    $("#EditBranchID").select2();
                                    //$("#CharityGroup").html(branchhtml).multipleSelect("refresh");
                                    //$("#BranchIDs").val("");
                                });
                            });
                            $("#EditCharityID").html(html);
                            $("#EditCharityID").select2();
                        }
                    });

                
                }
            });

            $("#EditCharityID").off("change").on("change", function () {
                //$("#CharityGroup").html("").multipleSelect("refresh");
                $("#EditBranchID").html("<option value=''>Select Branch</option>");
                if ($("#EditBranchID").data('select2')) {
                    $("#EditBranchID").select2("destroy");
                }
                $("#EditBranchID").select2();
                $("#MemberShipType").html("<option value=''>Select Membership Type</option>");
                if ($("#MemberShipType").data('select2')) {
                    $("#MemberShipType").select2("destroy");
                }
                $("#MemberShipType").select2();
                if ($(this).val() != "") {
                    $.get(Global.DomainName + "FoodBank/FamilyRecord/bindbranches", { charityID: $(this).val() }, function (data) {
                        var html;
                        $.each(data.data, function (index, item) {
                            html = html + "<option value='" + item.value + "'>" + item.text + "</option>";
                        });
                        $("#EditBranchID").html(html);
                        if ($('#EditBranchID option').length == 1) {
                            setTimeout(function () { $("#EditBranchID").change(); }, 100);
                        }
                    });

                    $.get(Global.DomainName + "FoodBank/FamilyRecord/bindmebershiptype", { organisationID: $("#EditCentralOfficeID").val(), charityID: $(this).val() }, function (data) {
                        var mebershiptypehtml;
                        $.each(data.data, function (index, item) {
                            mebershiptypehtml = mebershiptypehtml + "<option value='" + item.value + "'>" + item.text + "</option>";
                        });
                        $("#MemberShipType").html(mebershiptypehtml);
                        $("#MemberShipType").select2();
                    });
                }
            });

            $("#btn-reactivate-family").off("click").on("click", function () {
                $(".img-loading-div").show();
                $.ajax({
                    url: Global.DomainName + "FoodBank/FamilyRecord/reactivatefamily",
                    data: { id: $(this).attr('data-houseHoldId') },
                    type: "Post",
                    success: function (result) {
                        if (result.isSuccess) {
                            editGetPerson($("#PersonID").val());
                            Global.ShowMessage(result.data, Global.MessageType.Success);
                        } else {
                            Global.ShowMessage(result.data, Global.MessageType.Error);
                        }
                    },
                    complete: function () {
                        $(".img-loading-div").hide();
                    }
                });
            });
        }

        function familyTabEvent() {
        
        }

        function phoneAndInternetTabEvent() {
          
        }

        function reinitializePhoneTabGrid() {
         
        }

        function initializePhoneTabGrid() {
          
        }

        function relationshipTabEvent() {
          
        }

        function reinitializeRelationshipTabGrid() {
           
        }


        function userfieldsTabEventfeedback() {
            $("a[href=#userfields]").off("click").on("click", function () {
                //
                
                //$(".img-loading-div").show();
                //$(".userfields-tab-body").html('');
                //   // 
                //    $.ajax({
                //        url: Global.DomainName + "FoodBank/Familyrecord/FeedbackFamilyView",
                //        data: { Familyid: $("#PersonID").val() },
                //        type: "Get",
                //        success: function (data) {
                //            $(".userfields-tab-body").html(data);
                //            
                //            MyFeedbacklistIndex($("#PersonID").val());
                         
                //       },
                //        complete: function () {
                            
                //            setTimeout(function () { $(".img-loading-div").hide(); }, 200)
                //        }
                //    });
                MyFeedbacklistIndex($("#PersonID").val());
                });
         
        }
        function userfieldsvoucherTabEvent() {
            $("a[href=#userfieldsvoucher]").off("click").on("click", function () {
              //  $(".img-loading-div").show();
              //  $(".userfieldsvoucher-tab-body").html('');
                
                //$.ajax({
                //    url: Global.DomainName + "FoodBank/Familyrecord/VoucherFamilyView",
                //    data: { Familyid: $("#PersonID").val() },
                //    type: "Get",
                //    success: function (data) {
                        
                //        $(".userfieldsvoucher-tab-body").html(data);
                       
                //    },
                //    complete: function () {
                      

                //        setTimeout(function () { $(".img-loading-div").hide(); }, 200)
                //    }
                //});
                
                VoucherIndex($("#PersonID").val());
            });

        }
        function noteTabEvent() {
          //  
            $("a[href=#note]").off("click").on("click", function () {
               // 
                $(".note-tab-body").html('');
                $.ajax({
                    url: Global.DomainName + "FoodBank/notes/NoteView",
                    data: { personId: $("#PersonID").val() },
                    type: "Get",
                    success: function (data) {
                        $(".note-tab-body").html(data);

                        CKEDITOR.replace('txtNote');
                        CKEDITOR.instances['txtNote'].on("instanceReady", function (ev) {
                            $(".cke_top").hide();
                            $(".cke_bottom").hide();
                            ev.editor.setReadOnly(true);
                            ev.editor.resize('100%', '500');
                        });
                      //  
                        Global.DataServer.dataURL = '';
                        initializePersonNoteTable($("#PersonID").val(), 0);
                        $(document).off("click", "#grid-person-note tr");
                        if ($("#grid-person-note").find(".dataTables_empty").length == 0) {
                            $(document).on("click", "#grid-person-note tr", function () {
                                if ($(this).find(".dataTables_empty").length == 0) {
                                    $(this).closest('table').find('tr').removeClass("selected");
                                    $(this).addClass("selected");
                                   
                                    CKEDITOR.instances['txtNote'].setData($(this).find('.content').val());
                                    if ($(this).find("input.note-privacy-type").val() == "false") {
                                        $("#EditNote").prop("href", Global.DomainName + "FoodBank/notes/createedit?personId=" + $("#PersonID").val() + "&id=" + $(this).find("input.contact-id").val());
                                        $("#DeleteNote").prop("href", Global.DomainName + "FoodBank/notes/delete?personId=" + $("#PersonID").val() + "&id=" + $(this).find("input.contact-id").val());
                                        $("#EditNote,#DeleteNote").removeAttr("disabled");
                                      

                                    } else {
                                        if ($("#hdnCurrentUserId").data("rolename") == "SuperAdmin" || $("#hdnCurrentUserId").val() == $(this).find("input.note-created-userid").val()) {
                                            $("#EditNote").prop("href", Global.DomainName + "FoodBank/notes/createedit?personId=" + $("#PersonID").val() + "&id=" + $(this).find("input.contact-id").val());
                                            $("#DeleteNote").prop("href", Global.DomainName + "FoodBank/notes/delete?personId=" + $("#PersonID").val() + "&id=" + $(this).find("input.contact-id").val());
                                            $("#EditNote,#DeleteNote").removeAttr("disabled");
                                      
                                        } else {
                                            $("#EditNote").prop("href", "");
                                            $("#DeleteNote").prop("href", "");
                                            $("#EditNote,#DeleteNote").attr("disabled", "disabled");
                                   
                                        }
                                    }
                                }
                            });
                        }
                        else {
                            CKEDITOR.instances['txtNote'].setData('');
                            $("#EditNote,#DeleteNote").attr("disabled", "disabled");
                        }
                    }
                });
            });
        }
        function agencyTabEvent() {
            
            $("a[href=#agency]").off("click").on("click", function () {
                
               // $(".agency-tab-body").html('');
                //$(".img-loading-div").show();
                //$.ajax({
                //    url: Global.DomainName + "FoodBank/familyrecord/AgencyView",
                //    data: { personId: $("#PersonID").val() },
                //    type: "get",
                //    success: function (data) {
                        
                //        $(".agency-tab-body").html(data);
                    
                //    },
                //    complete: function (data) {
                        
                //        $(".img-loading-div").hide();
                //    }
                //});
                AgencyIndex($("#PersonID").val());
            });
            
            
        }
        function pacelsTabEvent() {
            //
            $("a[href=#parcels]").off("click").on("click", function () {
                   
                //$(".parcels-tab-body").html('');
                //$(".img-loading-div").show();
                //$.ajax({
                //    url: Global.DomainName + "FoodBank/familyrecord/PacelView",
                //    data: { personId: $("#PersonID").val() },
                //    type: "get",
                //    success: function (data) {
                        
                //        $(".parcels-tab-body").html(data);
                //        FamilyParcelIndex($("#PersonID").val());
                //    },
                //    complete: function (data) {
                        
                //        $(".img-loading-div").hide();
                //    }
                //});
                
                FamilyParcelIndex($("#PersonID").val());
            });
        }
        function initializePersonNoteTable(personId, noteId) {
            alertify.dismissAll();
            noteGrid = new Global.GridAjaxHelper('#grid-person-note', {
                "bLengthChange": false,
                "bInfo": false,
                "bFilter": false,
                "scrollY": "400px",
                "scrollCollapse": true,
                //"scrollX": true,
                "aoColumns": [{
                    "sName": "NoteDate", "mRender": function (data, type, row) {
                        //return '<span>' + row[0] + '</span><input type="hidden" value="' + row[2] + '"><input type="hidden" value="' + row[3] + '" class="contact-id" >';
                        return '<span>' + row[0] + '</span><input type="hidden" value="' + row[2] + '" class="note-privacy-type" ><textarea class="content" cols="20" rows="2" style="display:none;">' + row[3] + '</textarea><input type="hidden" value="' + row[4] + '" class="note-created-userid" ><input type="hidden" value="' + row[5] + '" class="contact-id" >';
                    }
                },
                { "sName": "Description" }],
                "bDestroy": true
            }, "FoodBank/notes/GetNotesByPersonId?personId=" + personId, function () {
                $("#AddNote").prop("href", Global.DomainName + "FoodBank/notes/createedit?personId=" + personId);
                if ($("#grid-person-note").find(".dataTables_empty").length == 0) {
                    if (noteId > 0) {
                        $("#grid-person-note tr").each(function (index, elem) {
                            if ($(elem).find(".contact-id").val() == noteId) {
                                $(elem).click();
                            }
                        });
                    }
                    else {
                        $('#grid-person-note tr:eq(1)').removeClass('selected');
                        $('#grid-person-note tr:eq(1)').addClass('bgcolorgreen');
                        $("#grid-person-note tr:eq(1)").click();
                    }
                }
                else {
                    //$(".note").val('');
                    CKEDITOR.instances['txtNote'].setData('');
                    $("#EditNote,#DeleteNote").attr("disabled", "disabled");
                }


            });

            $('#grid-person-note').off('click', 'tr');
            $('#grid-person-note').on('click', 'tr', function () {
                $('#grid-person-note tr').removeClass('selected');
                $('#grid-person-note tr').removeClass('bgcolorgreen');
                $(this).addClass('bgcolorgreen');
                //$(this).trigger('click');
            });

        }

        function reinitializeNoteTabGrid() {
          //  
            Global.DataServer.dataURL = "";
            Global.DataServer.dataURL = "FoodBank/notes/GetNotesByPersonId?personId=" + $("#PersonID").val();
            Global.DataServer.multisearch = [];
            noteGrid.fnDraw(false);
            Global.DataServer.dataURL = "";
           // $("a[href=#note]").trigger("click");
        }

        function MyFeedbacklistIndex(hnd_feedbackfamilyid) {
    
                //$('#grid-voucher-master').DataTable().destroy();
                //$('#grid-my-referral-family').DataTable().destroy();
                //$('#grid-family-parcel-master').DataTable().destroy();
                //$('#grid-person-agency').DataTable().destroy();
                if ($.fn.DataTable.isDataTable(myReferralGridfamily)) {
                    
                    $(myReferralGridfamily).DataTable().destroy();
            }
            
                Global.DataServer.dataURL = '';
                myReferralGridfamily = new Global.GridAjaxHelper('#grid-my-referral-family', {
                    "aoColumns": [
                        { "sName": "SNo" },
                        { "sName": "DateCompletd" },
                        { "sName": "Family.FamilyName" },
                        { "sName": "Parcel.DeliveryDate" },
                        { "sName": "Parcel.ParcelType.Name" },
                        { "sName": "Parcel.PackedDate" },

                        {
                            render: function (data, item, row, meta) {
                                return "<a  data-toggle='modal'  href='/FoodBank/Familyrecord/FeedbackListViewone/" + row[6] + "' data-target='#modal-feedback-viewdetails' ><img src='/Content/images/eye-icon.png' /></a>";
                            }
                        },

                    ],
                    "bStateSave": true,
                    "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [0, 6] }],
                }, "Foodbank/Familyrecord/GetFeedbackByfamilyid?familyid=" + hnd_feedbackfamilyid
                );
                $("#grid-my-referral-family").parent("div").parent("div").addClass("table-responsive");
                myReferralGridfamily.on('search.dt', function () {
                   
                   
                    Global.DataServer.dataURL = "";
                    Global.DataServer.dataURL = "Foodbank/Familyrecord/GetFeedbackByfamilyid?familyid=" + hnd_feedbackfamilyid;
                    Global.DataServer.multisearch = [];
                    //myReferralGridfamily.fnDraw(false);
                    Global.DataServer.dataURL = "";
                    // myReferralGridfamily.fnDraw(false);
                    Global.DataServer.multisearch = [];
               
                    Global.DataServer.dataURL = "";
                    
                });
            
        }
        function VoucherIndex(hnd_feedbackfamilyid) {


                //$('#grid-voucher-master').DataTable().destroy();
                //$('#grid-my-referral-family').DataTable().destroy();
                //$('#grid-family-parcel-master').DataTable().destroy();
                //$('#grid-person-agency').DataTable().destroy();
                if ($.fn.DataTable.isDataTable(voucherGrid)) {
                    $(voucherGrid).DataTable().destroy();
            }
            Global.DataServer.dataURL = '';
                voucherGrid = new Global.GridAjaxHelper('#grid-voucher-master', {
                    "aoColumns": [
                        { "sName": "VoucherId" },
                        { "sName": "S.NO" },
                        { "sName": "Referrer.Name" },
                        { "sName": "Family.FamilyName" },
                        { "sName": "AddedDate" },
                        { "sName": "RedeemedDate" },
                        { "sName": "VoucherToken" },

                    ],
                    "bStateSave": false,
                    "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [1] }, { 'visible': false, 'aTargets': [0] }],
                }, "Foodbank/familyrecord/BindVoucherListByFamily?familyid=" + hnd_feedbackfamilyid+ "",
                );
                $("#grid-voucher-master").parent("div").parent("div").addClass("table-responsive");
                voucherGrid.on('search.dt', function () {
                 
                    Global.DataServer.dataURL = "";
                    Global.DataServer.dataURL = "Foodbank/familyrecord/BindVoucherListByFamily?familyid=" + hnd_feedbackfamilyid + "";
                    Global.DataServer.multisearch = [];
                    //voucherGrid.fnDraw(false);
                    Global.DataServer.dataURL = "";
                });
                voucherGrid.on('length.dt', function () {
                    Global.DataServer.dataURL = "";
                    Global.DataServer.dataURL = "Foodbank/familyrecord/BindVoucherListByFamily?familyid=" + hnd_feedbackfamilyid + "";
                    Global.DataServer.multisearch = [];
                   // voucherGrid.fnDraw(false);
                    Global.DataServer.dataURL = "";
                });
            
        }
        function AgencyIndex(hnd_feedbackfamilyid) {

            
            Global.DataServer.dataURL = '';

            //$('#grid-voucher-master').DataTable().destroy();
            //$('#grid-my-referral-family').DataTable().destroy();
            //$('#grid-family-parcel-master').DataTable().destroy();
            //$('#grid-person-agency').DataTable().destroy();
                if ($.fn.DataTable.isDataTable(agencyGrid)) {
                    $(agencyGrid).DataTable().destroy();
                }
                agencyGrid = new Global.GridAjaxHelper('#grid-person-agency', {
                    "aoColumns": [
                        { "sName": "AgenciesId" },
                        { "sName": "S.NO" },
                        { "sName": "Agency.Name" },
                        { "sName": "Agency.Contact.Email" },
                        { "sName": "Agency.Contact.Mobile" },
                        {
                            "sName": ""
                        },
                    ],
                    "bStateSave": false,
                    "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [1, 5, 5] }, { 'visible': false, 'aTargets': [0] }],
                }, "FoodBank/FamilyRecord/GetAgencyByfamilyId?personId=" + hnd_feedbackfamilyid,
                );
                $("#grid-person-agency").parent("div").parent("div").addClass("table-responsive");
            agencyGrid.on('search.dt', function () {
                
                 
                Global.DataServer.dataURL = "";
                Global.DataServer.dataURL = "FoodBank/FamilyRecord/GetAgencyByfamilyId?personId=" + hnd_feedbackfamilyid;
                Global.DataServer.multisearch = [];
               // agencyGrid.fnDraw(false);
                Global.DataServer.dataURL = "";
                });
                agencyGrid.on('length.dt', function () {

                    
                    Global.DataServer.dataURL = "";
                    Global.DataServer.dataURL = "FoodBank/FamilyRecord/GetAgencyByfamilyId?personId=" + hnd_feedbackfamilyid;
                    Global.DataServer.multisearch = [];
                   // agencyGrid.fnDraw(false);
                    Global.DataServer.dataURL = "";
                });
          
        }
        function FamilyParcelIndex(hnd_feedbackfamilyid) {

            Global.DataServer.dataURL = '';
            //$('#grid-voucher-master').DataTable().destroy();
            //$('#grid-my-referral-family').DataTable().destroy();
            //$('#grid-family-parcel-master').DataTable().destroy();
            //$('#grid-person-agency').DataTable().destroy();
                if ($.fn.DataTable.isDataTable(familyparcelGrid)) {
                    $(familyparcelGrid).DataTable().destroy();
            }
            Global.DataServer.dataURL = "";
                familyparcelGrid = new Global.GridAjaxHelper('#grid-family-parcel-master', {
                    "aoColumns": [
                        { "sName": "ParcelTypeId" },
                        { "sName": "S.NO" },
                        {
                            "sName": "ID"
                        },
                        { "sName": "Family.FamilyName" },
                        { "sName": "DeliveredDate" },
                        { "sName": "DeliveryDate" },
                        { "sName": "Status" },

                    ],
                    "bStateSave": false,
                    "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [1] }, { 'visible': false, 'aTargets': [0] }],
                }, "Foodbank/familyrecord/GetParcelByfamilyId?personId=" + hnd_feedbackfamilyid,
                );
                $("#grid-family-parcel-master").parent("div").parent("div").addClass("table-responsive");
                familyparcelGrid.on('search.dt', function () {
                    Global.DataServer.dataURL = "Foodbank/familyrecord/GetParcelByfamilyId?personId=" + hnd_feedbackfamilyid;
                 //  familyparcelGrid.fnDraw(false);
                    Global.DataServer.multisearch = [];

                    Global.DataServer.dataURL = "";
                });
                familyparcelGrid.on('length.dt', function () {
                    Global.DataServer.dataURL = "Foodbank/familyrecord/GetParcelByfamilyId?personId=" + hnd_feedbackfamilyid;
                // familyparcelGrid.fnDraw(false);
                    Global.DataServer.multisearch = [];

                    Global.DataServer.dataURL = "";
                });
          
        }
        //Filter Start

        function inirializeFilters() {

            $("#btnCancelFilter").off("click");

            $("#btnCancelFilter").on("click", function () {
                window.location.reload();
            });

            if ($('#CharityFilter option').length == 2) {
                $('#CharityFilter').val($('#CharityFilter option:last').val());
                setTimeout(function () { $("#CharityFilter").change(); }, 100);
                $('#CharityFilter option:eq(0)').remove();
            }

            if ($('#BranchFilter option').length == 2) {
                $('#BranchFilter').val($('#BranchFilter option:last').val());
                setTimeout(function () { $("#BranchFilter").change(); }, 100);
                $('#BranchFilter option:eq(0)').remove();
            }

            if ($("#CharityFilter option").length)
                $("#CharityFilter").select2();

            if ($("#BranchFilter option").length)
                $("#BranchFilter").select2();


            
            $("#CharityFilter").off("change");
            $("#CharityFilter").on("change", function () {
               
                $("#BranchFilter").html("<option value=''>Select branch</option>");
                if ($("#BranchFilter").data('select2')) {
                   $("#BranchFilter").select2("destroy");
                }
                $("#BranchFilter").select2();

                Global.DataServer.multisearch = $.grep(Global.DataServer.multisearch, function (elem, index) {
                    return elem.column != "BranchID";
                });
                Global.DataServer.dataURL = "FoodBank/FamilyRecord/getpersons" + ($("#hdnCentralOfficeID").val() === "0" ? "" : "/" + $("#hdnCentralOfficeID").val());
                Global.DataServer.multisearch = $.grep(Global.DataServer.multisearch, function (elem, index) {
                    return elem.column != "CharityID";
                });

                Global.DataServer.multisearch.push({ "column": "CharityID", "filter": Global.FilterType.Equals, "value": $(this).val() });
                personGrid.fnDraw();
                personId = "";
                $("#hdnPersonID").val('');
                
                $.get(Global.DomainName + "FoodBank/FamilyRecord/BindBranchesForFilter", { orgId: $("#hdnCentralOfficeID").val(), charityID: $(this).val() }, function (data) {
                    var html = "<option value=''>Select branch</option>";
                    if (data.data.length > 1) {
                        
                        $.each(data.data, function (index, item) {
                            html  = html + "<option value='" + item.value + "'>" + item.text + "</option>";
                        });
                        $("#BranchFilter").html(html);
                    
                    }
                    else {
                        $.each(data.data, function (index, item) {
                            html = "<option value='" + item.value + "'>" + item.text + "</option>";
                        });
                        $("#BranchFilter").html(html);
                        $("#BranchFilter").select2();
                        $("#BranchFilter").change();
          
                    }
                    //Global.DataServer.multisearch = [];
                   // Global.DataServer.dataURL = "";
                  
                });
               //Global.DataServer.multisearch = [];
             //   Global.DataServer.dataURL = "";
            });

            $("#BranchFilter").off("change");
            $("#BranchFilter").on("change", function () {
                Global.DataServer.dataURL = "FoodBank/FamilyRecord/getpersons" + ($("#hdnCentralOfficeID").val() === "0" ? "" : "/" + $("#hdnCentralOfficeID").val());
                Global.DataServer.multisearch = $.grep(Global.DataServer.multisearch, function (elem, index) {
                    return elem.column != "BranchID";
                });

                Global.DataServer.multisearch.push({ "column": "BranchID", "filter": Global.FilterType.Equals, "value": $(this).val() });
                personGrid.fnDraw();
                //userfieldsTabEventfeedback();
                //userfieldsvoucherTabEvent();
                //noteTabEvent();
                //agencyTabEvent();
                //pacelsTabEvent();
                personId = "";
                $("#hdnPersonID").val('');
               // Global.DataServer.multisearch = [];
                //Global.DataServer.dataURL = "";
      
            });

           

            $(".alphabet").off("click");
            $(".alphabet").on("click", function () {
               
                $(".alphabet").each(function () {
                    $(this).removeClass("active");
                });
                $(this).addClass("active");
                $("#hdnAlphabet").val($(this).val());
                Global.DataServer.dataURL = "FoodBank/FamilyRecord/getpersons" + ($("#hdnCentralOfficeID").val() === "0" ? "" : "/" + $("#hdnCentralOfficeID").val());
                Global.DataServer.multisearch = $.grep(Global.DataServer.multisearch, function (elem, index) {
                    return elem.column !== "Family.FamilyName" && elem.filter !== Global.FilterType.StartsWith;
                });

                Global.DataServer.multisearch.push({ "column": "Family.FamilyName", "filter": Global.FilterType.StartsWith, "value": $(this).val() });
                personGrid.fnDraw();
                //userfieldsTabEventfeedback();
                //userfieldsvoucherTabEvent();
                //noteTabEvent();
                //agencyTabEvent();
                //pacelsTabEvent();
                personId = "";
                $("#hdnPersonID").val('');
              //  Global.DataServer.multisearch = [];
              //  Global.DataServer.dataURL = "";
            });

        }

        


        function bindFiltersOnDonorListLoad() {
           
        }



        
        $this.init = function () {
            initializeDatePicker();
            inirializeFilters();
            initializeCreatePersonModalWithForm();
            initializeGrid();
           // personalTabEvent();
          //  detailsTabEvent();
            userfieldsTabEventfeedback();
            userfieldsvoucherTabEvent();
            noteTabEvent();
            agencyTabEvent();
            pacelsTabEvent();
        };
    }

    $(function () {
        var self = new PersonIndex();
        self.init();
    });
}(jQuery));