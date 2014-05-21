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
            });

            return false;
        }
        $(document).on('submit', "form[data-schemesys-ajax='true']", ajaxSemesterFormSubmit);
})(jQuery);
