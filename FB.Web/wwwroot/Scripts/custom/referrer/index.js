
(function ($) {

    function indexReferral() {
        var $this = this, declarationGrid;

        function InitlizePage() {
            $(".datepicker").datepicker({ format: "dd/mm/yyyy" }).inputmask("dd/mm/yyyy", { "placeholder": "dd/mm/yyyy" });
            $('#sdate').datepicker("setDate", new Date());
            
            var date = new Date(); // Now
            date.setDate(date.getDate() + 30); // Set now + 30 days as the new date
            //console.log(date);
            $('#edate').datepicker("setDate", new Date(date));

        }

        $this.init = function () {
            InitlizePage();
        };
    }

    $(function () {
        var self = new indexReferral();
        self.init();
    });
}(jQuery));