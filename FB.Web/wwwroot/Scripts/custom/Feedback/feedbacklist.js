(function ($) {
    function MyReferralIndex() {
        var $this = this, myReferralGrid;

        function initializeGridMyReferral() {
            myReferralGrid = new Global.GridAjaxHelper('#grid-my-referral', {
                "aoColumns": [
                    { "sName": "SNo" },
                    { "sName": "ReferralDate" },
                    { "sName": "FamilyName" },
                    { "sName": "Mobile" },
                    { "sName": "Status" },
                    { "sName": "" },
                    {
                        render: function (data, item, row, meta) {
                            return "<a href='/Foodbank/Feedback/FeedbackDetails/" + row[6] + "' class='view_btn'><img src='/Content/images/eye-icon.png' /></a>";
                        }
                    },
                ],
                "bStateSave": false,
                "aoColumnDefs": [{ 'bSortable': true, 'aTargets': [3] } ],
            }, "Foodbank/Feedback/FeedbackList",
            );
            $("#grid-my-referral").parent("div").parent("div").addClass("table-responsive");
            myReferralGrid.on('search.dt', function () {
                Global.DataServer.dataURL = "Foodbank/Feedback/FeedbackList";
            });
        }

        $this.init = function () {
            initializeGridMyReferral();
        }
    }

    $(
        function () {
            var self = new MyReferralIndex();
            self.init();
        }
    )
})(jQuery)