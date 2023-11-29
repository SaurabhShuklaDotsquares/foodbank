(function ($) {
    function MyReferralIndex() {
        var $this = this, myReferralGrid, formDeleteUserDefinedField;

        function initializeGridMyReferral() {
            if ($.fn.DataTable.isDataTable($this.myReferralGrid)) {
                $($this.myReferralGrid).DataTable().destroy();
            }
            myReferralGrid = new Global.GridAjaxHelper('#grid-my-donor', {
                "aoColumns": [
                    { "sName": "Event" },
                    { "sName": "DonorId" },
                    { "sName": "Donor.Title,Donor.Forenames,Donor.Surname" },
                    { "sName": "Donor.Email" },
                    { "sName": "Donor.CentralOffice.OrganisationName" },
                    { "sName": "Donor.Charity.CharityName" },
                    { "sName": "Donor.Reference" },
                   
                  
                ],
                "bStateSave": false,
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [8] }, { 'visible': false, 'aTargets': [0] }],
                 
            }, "Foodbank/Donor/DonorList?charitID=" + $("#CharityID").val() + "&BranchID=" + $("#BranchID").val() ,
            );
            $("#grid-my-donor").parent("div").parent("div").addClass("table-responsive");
            myReferralGrid.on('search.dt', function () {
                Global.DataServer.dataURL = "Foodbank/Donor/DonorList?charitID=" + $("#CharityID").val() + "&BranchID=" + $("#BranchID").val();
            });


        }
        function reInitializeGrid() {
            Global.DataServer.dataURL = "Foodbank/Donor/DonorList?charitID=" + $("#CharityID").val() + "&BranchID=" + $("#BranchID").val();
            Global.DataServer.multisearch = [];
            myReferralGrid.fnDraw();
        }
        $this.init = function () {
            initializeGridMyReferral();
            initializeModalWithForm();
        }
        function initializeModalWithForm() {
            $('#CharityID').off("change").on('change', function () {
                
                $('#BranchID').html("<option value=''>Select Branch</option>");

                if ($(this).val() != "") {
                    $("#BranchID").html('');
                    var html = "<option value=''>Select Branch</option>";
                    var param = $('select#CharityID option:selected').val();

                    $.get('BindDonorList', { charityID: param }, function (data) {

                        if (data.data.length > 1) {


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
                            //$("#BranchID").select2();
                        }
                    });
                }
                reInitializeGrid();
            });
            $('#BranchID').off("change").on('change', function () {
                reInitializeGrid();
            });

            $("#modal-delete-donor").on('loaded.bs.modal', function () {
                formDeleteUserDefinedField = new Global.FormDeleteHelper($("#frm-delete form"), {}, function (result) {
                    $("#modal-delete-donor").modal("hide");

                    if (result.indexOf("Success") > -1) {
                        $('#grid-my-donor').find("tr.selected").remove();
                        Global.ShowMessage("Donor deleted successfully.", Global.MessageType.Success);
                    }
                    else {
                        Global.ShowMessage("You can't delete this Donor  because it something.", Global.MessageType.Error);
                    }
                });
            }).on('hidden.bs.modal', function () {
                Global.ModalClear($(this));
            });

            $("#modal-delete-membership-type").on('hidden.bs.modal', function () {
                $(this).removeData('bs.modal');
            });
        }
    }
   

    $(
        function () {
            var self = new MyReferralIndex();
            self.init();
          //  initializeModalWithForm();
        }
    )
})(jQuery)