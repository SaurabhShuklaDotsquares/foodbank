
function AssignPermission() {
    var $this = this;

    $this.initializeRoleMenuWithEvent = function () {
        $(".chkMenu").off("change").on("change", function () {
            if ($(this).is(":checked")) {
                $(this).closest(".top-parent").find('.chkParent').prop("checked", true);
            }
        });

        $(".chkParent").off("change").on("change", function () {
            
            if ($(this).is(":checked")) {
                $(this).closest(".top-parent").find(".parent input[type=checkbox]").prop("checked", true);
            }
            else {
                $(this).closest(".top-parent").find(".parent input[type=checkbox]").prop("checked", false);
            }

        });

        $("#btnReadOnly").off("click").on("click", function () {
            $("input[type='checkbox']").prop("checked", false);
            var $this = $(".chkMenu[data-read-only='True']");
            $this.prop("checked", true);
            var $parent = $this.closest(".top-parent");
            $parent.find("input[type=checkbox].chkParent").prop("checked", true);
            $parent.parent(".parent").parent(".top-parent").find("input[type=checkbox].chkParent:eq(0)").prop("checked", true);
        });

    };

}
