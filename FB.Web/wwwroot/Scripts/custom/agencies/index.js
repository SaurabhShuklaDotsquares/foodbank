(function ($) {
    function AgencyIndex() {
        var $this = this;

        function initializeAgencyGrid() {
            if ($.fn.DataTable.isDataTable($this.agencyGrid)) {
                $($this.agencyGrid).DataTable().destroy();
            }
            $this.agencyGrid = new Global.GridAjaxHelper('#grid-agencies-master', {
                "aoColumns": [
                    { "sName": "AgenciesId" },
                    { "sName": "S.NO" },
                    { "sName": "Name" },
                    { "sName": "Contact.Email" },
                    { "sName": "Contact.Mobile" },
                    {
                        "sName": ""
                    },
                ],
                "bStateSave": false,
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [1, 4, 5] }, { 'visible': false, 'aTargets': [0] }],
            }, "Foodbank/Agencies/BindAgenciesList",
            );
            $("#grid-agencies-master").parent("div").parent("div").addClass("table-responsive");
            $this.agencyGrid.on('search.dt', function () {
                Global.DataServer.dataURL = "Foodbank/Agencies/BindAgenciesList";
            });
            $this.agencyGrid.on('length.dt', function () {
                Global.DataServer.dataURL = "Foodbank/Agencies/BindAgenciesList";
            });
        }

        $this.init = function () {
            initializeAgencyGrid();
        }
    }

    $(
        function () {
            var self = new AgencyIndex();
            self.init();
        }
    )
})(jQuery)