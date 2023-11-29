
(function ($) {
    function DonationIndex() {
        var $this = this, formAddEditDonation, foodDonationGrid, paymentDonationGrid;
        function InitlizePage() {
            
            

        }
        function initializeModalWithForm() {
          

           
            $("#modal-add-edit-donations").on('loaded.bs.modal', function (e) {
                
                formForgotCredentials = new Global.FormHelper($("#frm-forgot-cred form"), { updateTargetId: "validation-summary" }, function (result) {
                    if (result.isSuccess) {
                        Global.Alert("Info", result.data, function () {
                            $("#modal-forgot-credentials").modal("hide");
                        });
                    }
                    else {
                        Global.Alert("Error", result.data);
                    }
                }) ;
            }).on('hidden.bs.modal', function (e) {
                $(this).removeData('bs.modal');
            });

           
        }

        function initializeGrid() {
            foodDonationGrid = new Global.GridAjaxHelper('#grid-volunteer-list', {
                "aoColumns": [
                    { "sName": "ForeName" },
                    { "sName": "Mobile" },
                    { "sName": "IndividualCouple" },
                    { "sName": "Packingordelivery" },
                    { "sName": "" },
                ],

                "bStateSave": true, "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [0, 4] }],
            }, "volunteer/VolunteerList",
                Global.DeleteMasters);
            $("#grid-volunteer-list").parent("div").parent("div").addClass("table-responsive");
            foodDonationGrid.on('search.dt', function () {
                Global.DataServer.dataURL = "volunteer/VolunteerList";
            });
        }
        function initializeGridPayment() {
            paymentDonationGrid = new Global.GridAjaxHelper('#grid-volunteer-delivery-list', {
                "aoColumns": [
                    { "sName": "ForeName" },
                    { "sName": "Mobile" },
                    { "sName": "IndividualCouple" },
                    { "sName": "Packingordelivery" },
                    { "sName": "" },
                ],
                "bStateSave": true,
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [2] }],
            }, "volunteer/VolunteerDeliveryList",
                Global.DeleteMasters);
            $("#grid-volunteer-delivery-list").parent("div").parent("div").addClass("table-responsive");
            foodDonationGrid.on('search.dt', function () {
                Global.DataServer.dataURL = "volunteer/VolunteerDeliveryList";
            });
            
        }

        
        $this.init = function () {
            initializeGrid();
            initializeGridPayment();
            initializeModalWithForm();
            InitlizePage();
        }
    }

    $(
        function () {
            var self = new DonationIndex();
            self.init();
        }
    )

})(jQuery)

function readURL(input) {
    
    if (window.FormData !== undefined) {
        var files = $("#fileuploader")[0].files;//input.files;
        var fileData = new FormData();
        for (var i = 0; i < files.length; i++) {
            fileData.append(files[i].name, files[i]);
        }

        //$.ajax({
        //    url: Global.DomainName + "Volunteer/uploadfile",
        //    type: "POST",
        //    contentType: false, // Not to set any content header  
        //    processData: false, // Not to process data  
        //    data: fileData,
        //    success: function (result) {
        //        var fileName = result.fileName;
        //        $("#OrganisationName").val(result.savedFileName);

        //    },
        //    error: function (err) {
        //        alert(err.statusText);
        //    }
        //});
        
        console.log(files); // I just check here and in browser I can see file name and size
        console.log(fileData); // I expect to see the same here, but here it almost shows empty
        $.ajax({
            type: "POST",
            url: "/Volunteer/uploadfile",
            data: fileData, // In the controller it receives IFormFile image
            processData: false,
            contentType: false,
            success: function (result) {
                alertify.dismissAll();
                alertify.success(result.msg);
                $("#modal-add-edit-donations").modal("hide");
                var fileName = result.fileName;
                window.location.reload();
                initializeGrid();
                $("#OrganisationName").val(result.savedFileName);
            },
            error: function (errorMessage) {
                
                console.log(errorMessage);
            }
        });
    }
}
