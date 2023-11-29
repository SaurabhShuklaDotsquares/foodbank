(function ($) {
    function GrantorIndex() {
        var $this = this, grantorGrid;

        function initializeGrantorGrid() {
            if ($.fn.DataTable.isDataTable($this.grantorGrid)) {
                $($this.grantorGrid).DataTable().destroy();
            }
            $this.grantorGrid = new Global.GridAjaxHelper('#grid-grantor-master', {
                "aoColumns": [
                    { "sName": "GrantorId" },
                    { "sName": "S.NO" },
                    { "sName": "ForeName,SurName" },
                    { "sName": "TotalAmount" },
                    { "sName": "Contact.Mobile" },
                    { "sName": "" },
                    {
                        "sName": ""
                    },
                ],
                "bStateSave": false,
                "aoColumnDefs": [{ 'bSortable': true, 'aTargets': [1, 5, 6] }, { 'visible': false, 'aTargets': [0] }],
            }, "Foodbank/Grantor/BindGrantorList",
            );
            $("#grid-grantor-master").parent("div").parent("div").addClass("table-responsive");
            $this.grantorGrid.on('search.dt', function () {
                Global.DataServer.dataURL = "Foodbank/Grantor/BindGrantorList";
            });
            $this.grantorGrid.on('length.dt', function () {
                Global.DataServer.dataURL = "Foodbank/Grantor/BindGrantorList";
            });
        }

        $this.init = function () {
            initializeGrantorGrid();
        }
    }

    $(
        function () {
            var self = new GrantorIndex();
            self.init();
        }
    )
})(jQuery)