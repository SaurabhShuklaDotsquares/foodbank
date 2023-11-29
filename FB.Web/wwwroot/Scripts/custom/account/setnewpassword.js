(function ($) {
    'use strict';
    function SetNewPassword() {
        var $this = this;

        $this.init = function () {
            $("form").validate();
        };
    }

    $(function () {
        var self = new SetNewPassword();
        self.init();
    });

}(jQuery));