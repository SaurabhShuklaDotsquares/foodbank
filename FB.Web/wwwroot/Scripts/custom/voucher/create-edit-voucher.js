(function ($) {
    function CreateEditVoucher() {
        var $this = this;
        function initLoadFunction() {
            $(".ddlreferrer").off("change").on("change", function () {
                if ($(this).val() != "") {

                  
                    if ( $(this).val() != '0') {
                        
                        ShowLoader();
                        $(".ddlfamily").html('');
                        $.get(Global.DomainName + "Voucher/GetFamilyList", { referrerId: $(this).val() }, function (data) {
                            var html;
                            html = '<option value="" >Select</option>';
                            $.each(data.data, function (index, item) {
                                html = html + "<option value='" + item.value + "'>" + item.text + "</option>";
                            });
                          
                            $(".ddlfamily").append(html);
                            if ($("#hdnFamilyId").val() != '' && $("#hdnFamilyId").val() != '0')
                            {
                                debugger;
                                    $(".ddlfamily").val($("#hdnFamilyId").val());
                                }
                            //$(".ddlfamily").val($("#hdnFamilyId").val());
                            $(".ddlfamily").trigger('change');
                            HideLoader();

                        });
                    }
                }
                    });
                
           
       

            $("#modal-download-qrcode").on('hidden.bs.modal', function () {
                $("#modal-download-qrcode").find(".modal-content").html("");
                $(this).removeData('bs.modal');
            });
        }

        $this.init = function () {
            initLoadFunction();
            $(".ddlreferrer").trigger('change');
        }
    }

    $(
        function () {
            var self = new CreateEditVoucher();
            self.init();
        }
    )
})(jQuery)