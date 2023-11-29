
(function ($) {
    'use strict';
    function UserDefinedFieldIndex() {
        var $this = this, userDefinedFieldGrid, formAddEditUserDefinedField, formDeleteUserDefinedField;

       

        function initializeGrid() {
         
            $("#btn-submit").click(function (e) {
                
             
                $('form').valid();
                var dynamicString = "";
                $('.dynamic-control').each(function () {

                    if ($(this)) {
                        
                        if ($(this).attr('type') == "checkbox") {
                            dynamicString = dynamicString.concat("#$#;", $(this).attr('data-FieldId') + '_' + $(this)[0].checked);}
                        else {
                            if ($(this).val() != null && $(this).val() != '') {
                                
                                dynamicString = dynamicString.concat("#$#;", $(this).attr('data-FieldId') + '_' + $(this).val().replace(/[\n\r]+/g, " "));
                            }
                        }
                    }
                });

               ;
                $('#dynamicString').val(dynamicString);
                $('form').submit();
            });
        }
        $this.init = function () {
            initializeGrid();
            
        };
    }

    $(function () {
        var self = new UserDefinedFieldIndex();
        self.init();
    });
}(jQuery));