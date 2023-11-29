(function ($) {
    function MyReferralIndex() {
        var $this = this, dashboardGrid, formDeleteUserDefinedField;

        function initializeGridMyReferral() {
            if ($.fn.DataTable.isDataTable($this.dashboardGrid)) {
                $($this.dashboardGrid).DataTable().destroy();
            }
            dashboardGrid = new Global.GridAjaxHelper('#grid-stock-list', {
                "aoColumns": [
                    { "sName": "Event" },
                    { "sName": "Id" },
                    { "sName": "Food.Name" },
                    { "sName": "TotalQuantity,Unit" },
                ],
                "bStateSave": false,
                "searching": false,
                "bPaginate": false, //hide pagination
                "bFilter": false, //hide Search bar
                "bInfo": false, // hide showing entries
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [3] }, { 'visible': false, 'aTargets': [0] }],


            }, "Foodbank/Dashboard/StockList",
                Global.DeleteMasters);
            $("#grid-stock-list").parent("div").parent("div").addClass("table-responsive");
            dashboardGrid.on('search.dt', function () {
                Global.DataServer.dataURL = "Foodbank/Dashboard/StockList";
            });
        }

        function dashboardCount(monthyear, year) {
            $.ajax({
                url: Global.DomainName + "Foodbank/Dashboard/DashboardCount",
                type: "post",
                success: function (response) {
                    $("#foodparcels").text(response.foodparcelscount);
                    $("#parcelsdelivered").text(response.parcelsdeliveredcount);
                }
            });
        }

        $(document).off('change', '#ddlMonth').on('change', '#ddlMonth', function () {
            if ($(this).val() != '') {
                dashboardCount($(this).val(), $("#ddlYear").val());
            }
        });

        $(document).off('change', '#ddlYear').on('change', '#ddlYear', function () {
            if ($(this).val() != '') {
                dashboardCount($("#ddlMonth").val(), $(this).val());
            }
        });

        $this.init = function () {
            initializeGridMyReferral();
            dashboardCount($("#ddlMonth").val(), $("#ddlYear").val());
        }
    }

    $(
        function () {
            var self = new MyReferralIndex();
            self.init();
        }
    )
})(jQuery)