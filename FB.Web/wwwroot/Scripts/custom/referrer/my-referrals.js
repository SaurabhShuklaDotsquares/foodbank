(function ($) {
    function MyReferralIndex() {
        var $this = this, myReferralGrid;

        function initializeGridMyReferral() {
            myReferralGrid = new Global.GridAjaxHelper('#grid-my-referral', {
                "aoColumns": [
                    { "sName": "SNo" },
                    { "sName": "Family.AddedDate" },
                    { "sName": "Family.FamilyName" },
                    { "sName": "Family.Contactno" },
                    { "sName": "Status" },
                    { "sName": "" },
                    {
                        render: function (data, item, row, meta) {
                            return "<a href='/referrer/ViewMyReferrals/" + row[6] + "' class='view_btn'><img src='/Content/images/eye-icon.png' /></a>";
                        }
                    },
                ],
                "bStateSave": false,
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [0,4,5,6] }],
            }, "Referrer/MyReferrals",
            );
            $("#grid-my-referral").parent("div").parent("div").addClass("table-responsive");
            myReferralGrid.on('search.dt', function () {
                Global.DataServer.dataURL = "Referrer/MyReferrals";
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