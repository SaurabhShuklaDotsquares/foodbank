(function ($) {
    function volunteerparceldetails() {
        var $this = this;

        function initializeVolunteerDeliverGrid() {
            if ($.fn.DataTable.isDataTable($this.familydeliverGrid)) {
                $($this.familydeliverGrid).DataTable().destroy();
            }
            $this.familydeliverGrid = new Global.GridAjaxHelper('#grid-family-parcel-Deliveries', {
                "aoColumns": [
                    { "sName": "ParcelTypeId" },//0
                    { "sName": "S.NO" },//1
                    { "sName": "ParcelType.Name" },//2
                    { "sName": "Family.FamilyName" },//3
                    { "sName": "DeliveredDate" },//4
                    /*{ "sName": "Status" },*/
                    {
                        "sName": ""//5
                    },
                ],
                "bStateSave": false,
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [1,4,5] }, { 'visible': false, 'aTargets': [0] }],
            }, "Foodbank/AdminVolunteer/VolunteerDeliveryDetails?VolunteerId=" + $("#hdnVolunteerId").val(),
            );
            $("#grid-family-parcel-Deliveries").parent("div").parent("div").addClass("table-responsive");
            $this.familydeliverGrid.on('search.dt', function () {
                Global.DataServer.dataURL = "Foodbank/AdminVolunteer/VolunteerDeliveryDetails?VolunteerId=" + $("#hdnVolunteerId").val();
            });
            $this.familydeliverGrid.on('length.dt', function () {
                Global.DataServer.dataURL = "Foodbank/AdminVolunteer/VolunteerDeliveryDetails?VolunteerId=" + $("#hdnVolunteerId").val();
            });
        }


        function initializeVolunteerParcelGrid() {
            if ($.fn.DataTable.isDataTable($this.familyparcelGrid)) {
                $($this.familyparcelGrid).DataTable().destroy();
            }
            $this.familyparcelGrid = new Global.GridAjaxHelper('#grid-family-parcel-Packing', {
                "aoColumns": [
                    { "sName": "ParcelTypeId" },//0
                    { "sName": "S.NO" },//1
                    { "sName": "ParcelType.Name" },//2
                    { "sName": "Family.FamilyName" },//3
                    { "sName": "Family.packondate" },//4
                    {
                        "sName": ""
                    },//5
                ],
                "bStateSave": false,
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [1,4,5] }, { 'visible': false, 'aTargets': [0] }],
            }, "Foodbank/AdminVolunteer/VolunteerParcelDetails?VolunteerId=" + $("#hdnVolunteerId").val(),
            );
            $("#grid-family-parcel-Packing").parent("div").parent("div").addClass("table-responsive");
            $this.familyparcelGrid.on('search.dt', function () {
                Global.DataServer.dataURL = "Foodbank/AdminVolunteer/VolunteerParcelDetails?VolunteerId=" + $("#hdnVolunteerId").val();
            });
            $this.familyparcelGrid.on('length.dt', function () {
                Global.DataServer.dataURL = "Foodbank/AdminVolunteer/VolunteerParcelDetailsVolunteerId=" + $("#hdnVolunteerId").val();
            });
        }

















        $this.init = function () {
            initializeVolunteerDeliverGrid();
            initializeVolunteerParcelGrid();

            
        };

    }

    $(
        function () {
            var self = new volunteerparceldetails();
            self.init();
        }
    )
})(jQuery)