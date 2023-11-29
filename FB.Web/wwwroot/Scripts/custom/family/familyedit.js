
(function ($) {
    'use strict';

    function PersonIndex() {
        var $this = this;
        function initializeModalWithForm() {
            $(".datepicker").datepicker({ format: "dd/mm/yyyy", endDate: '-0m' }).inputmask("dd/mm/yyyy", { "placeholder": "dd/mm/yyyy" });
            $(".datepicker2").datepicker({ format: "dd/mm/yyyy"}).inputmask("dd/mm/yyyy", { "placeholder": "dd/mm/yyyy" });

        }
        function InitializeOnLoadSection() {
            $(".family-allery").select2({
                placeholder: "Select allergy"
            });

            $("#drpPostcodeAddr").off("change").on("change", function () {
                SearchIdBegin(this);
                setTimeout(function () { BindHMRCAddress(); }, 1000);
            });
            $("#Overseas").off("change").on("change", function () {
                if ($(this).is(":checked")) {
                    $('#OldPostCode').val($("#PostCode").val());
                    $("#PostCode").attr("disabled", "disabled");
                }
                else {
                    $('#OldPostCode').val("");
                    $("#PostCode").removeAttr("disabled");
                }
            });

            $("#CountryID").off("change").on("change", function () {
                if ($("#CountryID option:selected").text().toLowerCase().trim() == "united kingdom") {
                    $("#Overseas").prop("checked", false);
                } else {
                    $("#Overseas").prop("checked", true);
                }

                $("#Overseas").trigger("change");
            });

            $('body').delegate('.addRow', 'click', function () {
                var totalfamily = $("#TotalFamily option:selected").val();
                var subfamilyname = $('input[name="subfamilyname"]').map(function () {
                    return this.value
                }).get();
                var subfamilydob = $('input[name="subfamilydob"]').map(function () {
                    return this.value
                }).get();
                var subfamilyisadult = $('input[name="subfamilyisadult"]').map(function () {
                    return this.value
                }).get();
                var SubFamilyAllergry = $('Select[name="SubFamilyAllergry"]').map(function () {
                    return this.value
                }).get();
               
                if (subfamilyname.length > 0) {
                    for (var i = 0; i < subfamilyname.length; i++) {
                        if (subfamilyname[i].trim().length == 0 || subfamilydob[i].length == 0 || SubFamilyAllergry[i].length == 0) {
                            Global.Alert("Warning", "Please fill name, dob and allergy.");
                            return false;
                        } else {


                        }
                    }
                }
                if (subfamilyname.length >= parseInt(totalfamily)) {

                    Global.Alert("Warning", "The number of adults and children specified does not match the number of members for which you have provided the details for, please check the details you have provided and contact us on 01902 714030 if the issue persists");
                    return false;
                }
                
                // Destroy Select2
                $('.family-allery').select2('destroy');
                // Unbind the event
                $('.family-allery').off('select2:select');
                var html555 = ' ';
                html555 += '   <tr>';
                html555 += '<td><input class="form-control" placeholder="Name" name="subfamilyname" type="text" value=""></td>';
                html555 += '  <td><input class="form-control  datepicker" placeholder="DD/MM/YYYY" name="subfamilydob" type="text" value=""></td>';
                html555 += '    <td style="">';
                html555 += '          <select name="SubFamilyAllergry" class="form-control family-allery select2" multiple="multiple">' + listallerys + '</select>';
                html555 += '</td>';
                html555 += '<td style="">';
                html555 += '  <input class="control-label checkboxauldt" placeholder="Is Adult" name="subfamilyisadult" type="checkbox" value=""> IsAdult';
                html555 += '</td>';
                html555 += '<td style="">';
                html555 += '  <a class="deleterow" style="display:block;">';
                html555 += '       <img src="/Content/images/delete.png" width="22px"/>';
                html555 += '    </a>';
                html555 += ' </td>';
                html555 += ' </tr>';
                $("#subjecttable").append(html555);
                $(".datepicker").datepicker({ format: "dd/mm/yyyy", endDate: '-0m' }).inputmask("dd/mm/yyyy", { "placeholder": "dd/mm/yyyy" });
                $(".family-allery").select2({
                    placeholder: "Select allergy"
                });
            });
            $('body').delegate('.deleterow', 'click', function () {
                
                var id = $(this).attr("data-id");
                if (id != undefined) {
                    if (id.length > 0) {
                        $.get(Global.DomainName + "FoodbankReferrer/DeleteFamilyMember", { id: id }, function (data) {
                            alertify.dismissAll();
                            alertify.error("Family member deleted successfully");
                        });
                    }
                }
                var empTab = document.getElementById('tablefamily');
                empTab.deleteRow(this.parentNode.parentNode.rowIndex);       // BUTTON -> TD -> TR.
                var subfamilyname = $('input[name="subfamilyname"]').map(function () {
                    return this.value
                }).get();
                if (subfamilyname.length == 0) {
                    $(".addRow").trigger("click");
                }
            });

            $("#btn-submit").click(function (e) {
                $('form').valid();
                
                //if ($('.postcode').val() !== "") {
                //    if ($('.postcode').val() == "") {

                //        Global.Alert("Invalid Postcode", "Please enter postcode.");

                //    }
                //    if ($('.postcode').val().trim().indexOf(' ') < 0) {
                //        Global.Alert("Invalid Postcode", "The postocde you have entered is invalid. Please ensure that you have entered space between the postcode.");
                //        return false;
                //    }
                //    else {
                //        if ($('.postcode').val() == "") {

                //            Global.Alert("Invalid Postcode", "Please enter postcode.");

                //        }
                        
                        var totalfamily = $("#TotalFamily option:selected").val();
                        var TotalAdults = $("#TotalAdults option:selected").val();
                        var TotalChild = $("#TotalChild option:selected").val();
                        var totalchilcount = 0;
                        var subfamilyname = $('input[name="subfamilyname"]').map(function () {
                            return this.value
                        }).get();
                        var subfamilydob = $('input[name="subfamilydob"]').map(function () {
                            return this.value
                        }).get();
                        var subfamilyisadult = $('input[name="subfamilyisadult"]').map(function () {
                            return this
                        }).get();

                        var SubFamilyAllergry = $('Select[name="SubFamilyAllergry"]').map(function () {
                            return this
                        }).get();
                        if (subfamilyname.length > 0) {

                            for (var i = 0; i < subfamilyname.length; i++) {
                                if (subfamilyname[i].trim().length == 0 || subfamilydob[i].length == 0 || SubFamilyAllergry[i].selectedOptions.length == 0) {
                                    Global.Alert("Warning", "Please fill name, dob and allergy.");
                                    return false;
                                }
                                else {
                                }
                            }
                        }
                        
                        var contact = ''; var SubFamilyAllergrystring = '';
                        for (var i = 0; i < subfamilyisadult.length; i++) {
                            contact += subfamilyisadult[i].checked + ',';

                            var sub = '';

                            for (var j = 0; j < SubFamilyAllergry[i].selectedOptions.length; j++) {
                                sub += SubFamilyAllergry[i].selectedOptions[j].value + '%';
                            }
                            SubFamilyAllergrystring += sub + ',';
                            if (subfamilyisadult[i].checked == false) {
                                totalchilcount = totalchilcount + 1;
                            }
                        }
                        if (subfamilyname.length != parseInt(totalfamily)) {
                            Global.Alert("Warning", "The number of adults and children specified does not match the number of members for which you have provided the details for, please check the details you have provided and contact us on 01902 714030 if the issue persists");
                            return false;
                        }
                        if ((parseInt(TotalChild) + parseInt(TotalAdults)) != parseInt(totalfamily)) {
                            Global.Alert("Warning", "The number of adults and children specified does not match the number of members for which you have provided the details for, please check the details you have provided and contact us on 01902 714030 if the issue persists");
                            return false;
                        }
                        if ((parseInt(totalchilcount)) != parseInt(TotalChild)) {
                            Global.Alert("Warning", "The number of adults and children specified does not match the number of members for which you have provided the details for, please check the details you have provided and contact us on 01902 714030 if the issue persists");
                            return false;
                        }
                        //return false;
                        $('#subfamilyisadult2').val(contact);
                        $('#SubFamilyAllergries').val(SubFamilyAllergrystring);
                        $('form').submit();
                    
              /*  }*/
            })


        }

        function BindHMRCAddress() {
            if (!$("#HMRCAddressOverride").is(":checked")) {

                var housename = $(".house-name").val();
                var housenumber = $(".house-number").val();

                var streetname = $(".street-name").val();
                var postcode = $(".postcode").val();
                var fulladdr = "";

                if (housename.trim() != "") {
                    fulladdr = housename;
                }

                if (housenumber.trim() != "" || streetname.trim() != "") {
                    fulladdr = fulladdr != "" ? fulladdr + ", " : "";
                    fulladdr = (fulladdr + housenumber + " " + streetname).trim();
                }

                if (postcode.trim() != "") {
                    fulladdr = fulladdr != "" ? fulladdr + ", " : "";
                    fulladdr = fulladdr + postcode;
                }

                $("#HMRCAddress").val(fulladdr.trim());
            }
        }

        $this.init = function () {
            InitializeOnLoadSection();
            initializeModalWithForm();
        };
    }

    $(function () {
        var self = new PersonIndex();
        self.init();
    });
}(jQuery));
