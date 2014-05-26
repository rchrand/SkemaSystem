jQuery(function($) {
        var ajaxSemesterFormSubmit = function () {
            var $form = $(this);

            var options = {
                url: $form.attr("action"),
                type: $form.attr("method"),
                data: $form.serialize()
            };

            $.ajax(options).done(function (data) {
                var $target = $($form.attr("data-schemesys-target"));
                console.log($target);
                $target.html(data);
                $form.find("input[type=text]").val("");
            });

            

            return false;
        }
        $(document).on('submit', "form[data-schemesys-ajax='true']", ajaxSemesterFormSubmit);

        
})(jQuery);

function changeOptionalSubjectInfo() {
    var $yearDropdown = $("#year");
    var $semesterDropdown = $("#semester");

    if ($yearDropdown.val() != "" && $semesterDropdown.val() != "") {
        var options = {
        url: $yearDropdown.attr("action"),
        type: "get",
        data: $yearDropdown.serialize() + "&" + $semesterDropdown.serialize()
        };

        $.ajax(options).done(function (data) {
            $("#conflictSchemes").html(data);
        });
    }
    
}
