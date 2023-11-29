(function ($) {
    function VolunteerPackingIndex() {
        var $this = this, PackingGrid;

        function initializePackingGrid() {
            PackingGrid = new Global.GridAjaxHelper('#grid-volunteer-packing-list', {
                "aoColumns": [{ "sName": "S.NO" },
                { "sName": "AssignedDate" },
                { "sName": "PacelType" },
                { "sName": "DueDateDelivery" },
                    { "sName": "Status" },
                    { "sName": "" }
                ],
                "bStateSave": true,
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [0,4,5] }],
            }, "volunteer/PackingListing",
                Global.DeleteMasters);
            $("#grid-volunteer-packing-list").parent("div").parent("div").addClass("table-responsive");
            PackingGrid.on('search.dt', function () {
                VolunteerFilter();
                Global.DataServer.dataURL = "volunteer/PackingListing";
            });
            PackingGrid.on('length.dt', function () {

                VolunteerFilter();
                Global.DataServer.dataURL = "volunteer/PackingListing";
            });
        }

        //function ReinitializePackingGrid() {
        //    Global.DataServer.dataURL = "";
        //    Global.DataServer.dataURL = "volunteer/PackingListing";
        //    PackingGrid.fnDraw(false);
        //    Global.DataServer.dataURL = "";
        //    Global.DataServer.multisearch = [];
        //}

        function VolunteerFilter() {
            Global.DataServer.multisearch = $.grep(Global.DataServer.multisearch, function (elem, index) {
                return elem.column != "VolunteerId";
            });

            if ($("#hdnPersonID").val() != "") {
                Global.DataServer.multisearch.push({ "column": "VolunteerId", "filter": Global.FilterType.Equals, "value": $("#hdnVolunteerId").val() });
            }
        }

        $this.init = function () {
            initializePackingGrid();
        }
    }

    $(
        function () {
            var self = new VolunteerPackingIndex();
            self.init();
        }
    )
})(jQuery)