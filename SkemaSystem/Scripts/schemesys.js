(function ($) {
    $(document).ready(function () {
        jQuery.fn.extend({
            toggleVisibility: function () {
                return this.each(function () {
                    if ($(this).css('visibility') == 'hidden') {
                        $(this).css('visibility', 'visible');
                    } else {
                        $(this).css('visibility', 'hidden');
                    }
                });
            }
        });

        var ajaxSemesterFormSubmit = function () {

            var $form = $(this);
            var options = {
                url: $form.attr("action"),
                type: $form.attr("method"),
                data: $form.serialize()
            };

            $.ajax(options).done(function (data) {
                var $target = $($form.attr("data-schemesys-target"));
                $target.html(data);
                $form.find("input[type=text]").val("");
            });

            

            return false;
        }
        $(document).on('submit', "form[data-schemesys-ajax='true']", ajaxSemesterFormSubmit);

        $(document).on('submit', 'form#scheme-selector', function () {
            var form = $(this);

            $.ajax({
                url: form.attr("data-action-subject"),
                type: form.attr("method"),
                data: form.serialize()
            }).done(function (response) {
                $('#subject-selector').html(response);
            });

            $.ajax({
                url: form.attr("data-action-scheme"),
                type: form.attr("method"),
                data: form.serialize()
            }).done(function (response) {
                $('#schemes').html(response);
            });

            return false;
        });

        $(document).ajaxError(function (event, jqxhr, settings, exception) {
            if (jqxhr.status == 302) {
                window.location.href = '/admin/login';
            }
        });
    });
})(jQuery);