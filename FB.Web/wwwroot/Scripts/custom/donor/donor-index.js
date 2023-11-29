
(function ($) {
    'use strict';

    function DonorIndex() {
        var $this = this;

        function InitializeOnLoadSection() {
        }

        $this.init = function () {
            InitializeOnLoadSection();
        };
    }

    $(function () {
        var self = new DonorIndex();
        self.init();
    });
}(jQuery));