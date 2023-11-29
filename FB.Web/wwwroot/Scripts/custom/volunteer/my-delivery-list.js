(function ($) {
    function VolunteerIndex() {
        var $this = this, volunteerDeliveryGrid, PackingGrid;

        function initializeDeliveryGrid() {
            $('#div-volunteer-packing-list').hide();
            volunteerDeliveryGrid = new Global.GridAjaxHelper('#grid-volunteer-delivery-list', {
                "aoColumns": [{ "sName": "S.NO" },
                { "sName": "DeliveryDate" },
                { "sName": "Location.LocationId" },
                { "sName": "ParcelTypeId" },
                    { "sName": "Status" },
                   { "sName": "" }
                ],
                "bStateSave": true,
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [0, 4,5] }],
            }, "volunteer/mydelivery",
                Global.DeleteMasters);
            $("#grid-volunteer-delivery-list").parent("div").parent("div").addClass("table-responsive");
            volunteerDeliveryGrid.on('search.dt', function () {
                VolunteerFilter();
                Global.DataServer.dataURL = "volunteer/mydelivery";
            });
            volunteerDeliveryGrid.on('length.dt', function () {

                VolunteerFilter();
                Global.DataServer.dataURL = "volunteer/mydelivery";
            });
        }

        function VolunteerFilter() {
            Global.DataServer.multisearch = $.grep(Global.DataServer.multisearch, function (elem, index) {
                return elem.column != "VolunteerId";
            });

            if ($("#hdnPersonID").val() != "") {
                Global.DataServer.multisearch.push({ "column": "VolunteerId", "filter": Global.FilterType.Equals, "value": $("#hdnVolunteerId").val() });
            }
        }

        $this.init = function () {
            $('#my-delivery').attr('class','active');
            initializeDeliveryGrid();
        }
    }

    $(
        function () {
            var self = new VolunteerIndex();
            self.init();
        }
    )
})(jQuery)