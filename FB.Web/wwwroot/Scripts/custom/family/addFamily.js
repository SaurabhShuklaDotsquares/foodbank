
(function ($) {
    'use strict';

    function PersonIndex() {
        var $this = this;
        function initializeModalWithForm() {
            $(".datepicker").datepicker({ format: "dd/mm/yyyy", endDate: '-0m' }).inputmask("dd/mm/yyyy", { "placeholder": "dd/mm/yyyy" });
            $('.datepicker').datepicker("setDate", new Date());

            $(".ParcelDeliver").datepicker({ format: "dd/mm/yyyy", startDate: '-0m' }).inputmask("dd/mm/yyyy", { "placeholder": "dd/mm/yyyy" });
            $('.ParcelDeliver').datepicker("setDate", new Date());
        }
        function InitializeOnLoadSection() {
            $("#drpPostcodeAddr").off("change").on("change", function () {
                SearchIdBegin(this);
                setTimeout(function () { BindHMRCAddress(); }, 1000);
            });
            $("#SelfReffered").off("change").on("change", function () {
                if ($("#SelfReffered").val() == "2")
                {
                    $("#ReferralId").attr("style","display:block");
                }
                else
                {
                    $("#ReferralId").attr("style", "display:none");
                }
            });
            $("#Overseas").off("change").on("change", function () {
                if ($(this).is(":checked")) {
                    $('#OldPostCode').val($("#PostCode").val());
                    $("#PostCode").attr("readonly", "readonly");
                }
                else {
                    $('#OldPostCode').val("");
                    $("#PostCode").removeAttr("readonly");
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
                        }
                    }
                }
                if (subfamilyname.length >= parseInt(totalfamily)) {
                    Global.Alert("Warning", "There is a limit to the total number of families");
                    return false;
                }
                var html555 = '';
                html555 += '<tr >';
                html555 += '                 <td>Name*</td>';
                html555 += '               <td><input class="form-control" placeholder="Name" name="subfamilyname" type="text" value=""></td>';
                html555 += '                         <td>DOB*</td>';
                html555 += '                         <td><input class="form-control  datepicker" placeholder="DD/MM/YYYY" name="subfamilydob" type="text" value=""></td>';
                html555 += '                     </tr>';
                html555 += '                     <tr >';
                html555 += '                         <td style="border-top:0px">Allergy</td>';
                html555 += '                         <td style="border-top:0px">';
                html555 += '                             <select name="SubFamilyAllergry" class="form-control ">' + listallerys + '</select>';
                html555 += '                         </td>';
                html555 += '                         <td style="border-top:0px">';
                html555 += '                             <input class="control-label checkboxauldt" placeholder="Is Adult" name="subfamilyisadult" type="checkbox" value=""> IsAdult';
                html555 += '                         </td>';
                html555 += '                         <td style="border-top:0px">';
                html555 += '                             <a class="addRow">';
                html555 += '                                 <img src="/Content/images/plus-icon.png" />';
                html555 += '                             </a>';
                //html555 += '                             <a class="deleterow" style="padding-left:50px"  onclick="removeRow(this);" >';
                //html555 += '                                 <img src="/Content/images/delete.png" />';
                //html555 += '                             </a>';
                html555 += '                         </td>';
                html555 += '                     </tr>';
                $("#subjecttable").append(html555);
                $(".datepicker").datepicker({ format: "dd/mm/yyyy", endDate: '-0m' }).inputmask("dd/mm/yyyy", { "placeholder": "dd/mm/yyyy" });

            });
            $("#TotalFamily").off("click").on("click", function () {
                var html555 = '';
                html555 += '<tr >';
                html555 += '                 <td>Name*</td>';
                html555 += '               <td><input class="form-control" placeholder="Name" name="subfamilyname" type="text" value=""></td>';
                html555 += '                         <td>DOB*</td>';
                html555 += '                         <td><input class="form-control  datepicker" placeholder="DD/MM/YYYY" name="subfamilydob" type="text" value=""></td>';
                html555 += '                     </tr>';
                html555 += '                     <tr >';
                html555 += '                         <td style="border-top:0px">Allergy</td>';
                html555 += '                         <td style="border-top:0px">';
                html555 += '                             <select name="SubFamilyAllergry" class="form-control ">' + listallerys + '</select>';
                html555 += '                         </td>';
                html555 += '                         <td style="border-top:0px">';
                html555 += '                             <input class="control-label checkboxauldt" placeholder="Is Adult" name="subfamilyisadult" type="checkbox" value=""> IsAdult';
                html555 += '                         </td>';
                html555 += '                         <td style="border-top:0px">';
                html555 += '                             <a class="addRow">';
                html555 += '                                 <img src="/Content/images/plus-icon.png" />';
                html555 += '                             </a>';
                //html555 += '                             <a class="deleterow" style="display:none;padding-left:50px"  onclick="removeRow(this);">';
                //html555 += '                                 <img src="/Content/images/delete.png" />';
                //html555 += '                             </a>';
                html555 += '                         </td>';
                html555 += '                     </tr>';
                $("#subjecttable").html(html555);
                $(".datepicker").datepicker({ format: "dd/mm/yyyy", endDate: '-0m' }).inputmask("dd/mm/yyyy", { "placeholder": "dd/mm/yyyy" });
                $('.datepicker').datepicker("setDate", new Date());
            });
            $("#btn-submit").click(function (e) {
                $('form').valid();
                if ($('.postcode').val() !== "") {
                    if ($('.postcode').val().trim().indexOf(' ') < 0) {
                        Global.Alert("Invalid Postcode", "The postocde you have entered is invalid. Please ensure that you have entered space between the postcode.");
                        return false;
                    }
                    else {
                        //
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
                            return this.value
                        }).get();
                        if (subfamilyname.length > 0) {

                            for (var i = 0; i < subfamilyname.length; i++) {
                                if (subfamilyname[i].trim().length == 0 || subfamilydob[i].length == 0 || SubFamilyAllergry[i].length == 0) {
                                    Global.Alert("Warning", "Please fill name, dob and allergy.");
                                    return false;
                                }

                            }

                        }
                        //
                        var contact = ''; var SubFamilyAllergrystring = '';
                        for (var i = 0; i < subfamilyisadult.length; i++) {
                            contact += subfamilyisadult[i].checked + ',';
                            SubFamilyAllergrystring += SubFamilyAllergry[i] + ',';
                            if (subfamilyisadult[i].checked == false) {
                                totalchilcount = totalchilcount + 1;
                            }
                        }
                        if (subfamilyname.length != parseInt(totalfamily)) {
                            Global.Alert("Warning", "Number of family member must be equal to total Number of family member.");
                            return false;
                        }
                        if ((parseInt(TotalChild) + parseInt(TotalAdults)) != parseInt(totalfamily)) {
                            Global.Alert("Warning", "Total adult and child is equal to Number of family member.");
                            return false;
                        }
                        if ((parseInt(totalchilcount)) != parseInt(TotalChild)) {
                            Global.Alert("Warning", "Family Member details of child must be equal to of Total Child.");
                            return false;
                        }
                        $('#subfamilyisadult2').val(contact);
                        $('#SubFamilyAllergries').val(SubFamilyAllergrystring);
                        $('form').submit();
                    }
                }
            })

            $("#CentralOfficeID").off("change").on("change", function () {
                $("#CharityID").html("<option value=''>Select Charity...</option>");
                //$("#CharityID").select2("destroy");
                //$("#CharityID").select2();
                $("#CharityGroup").html("");
                if ($(this).val() != "") {
                    var html = "<option value=''>Select Charity...</option>";
                    $.get(Global.DomainName + "account/bindcharities", { organisationID: $(this).val() }, function (data) {
                        if (data.data.length > 1) {
                            $.each(data.data, function (index, item) {
                                html = html + "<option value='" + item.value + "'>" + item.text + "</option>";
                            });
                            $("#CharityID").html(html);
                        }
                        else {
                            $.each(data.data, function (index, item) {
                                html = "<option value='" + item.value + "'>" + item.text + "</option>";
                                $.get(Global.DomainName + "account/bindbranches", { charityID: item.value }, function (data) {
                                    var branchhtml;
                                    $.each(data.data, function (index, item) {
                                        branchhtml = branchhtml + "<option value='" + item.value + "'>" + item.text + "</option>";
                                    });
                                    $("#BranchID").html(branchhtml);
                                    $("#BranchIDs").val($("#BranchID").val());
                                });
                            });
                            $("#CharityID").html(html);
                            //$("#CharityID").select2();
                        }
                    });
                }
            });

            $("#CharityID").off("change").on("change", function () {
                if ($(this).val() != "") {
                    $.get(Global.DomainName + "account/bindbranches", { charityID: $(this).val() }, function (data) {
                        var html;
                        $.each(data.data, function (index, item) {
                            html = html + "<option value='" + item.value + "'>" + item.text + "</option>";
                        });
                        $("#BranchID").html(html);
                        $("#BranchIDs").val($("#BranchID").val());

                        if ($('#BranchID option').length == 1) {
                            $('#BranchID').val($('#BranchID option:last').val());
                            $("#BranchID").trigger('change');
                        }

                    });
                }

            });

            $('#BranchID').off("change").on("change", function () {
                $("#BranchIDs").val($('#BranchID').val());
                var branchvalue = $("#BranchIDs").val();
                $('.textReference').val('');
                if (branchvalue != "") {
                    $('.textReference').removeAttr("readonly");
                    if (branchvalue.indexOf(",") < 0) {
                        ReferenceType = GetReferenceType(branchvalue);
                    }
                    else {
                        $('#lastrefmsg').text("Last Reference");
                    }
                }
                else {
                    $('#lastrefmsg').text("Last Reference");
                    $('.textReference').attr("readonly", "readonly");
                }

            });
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
