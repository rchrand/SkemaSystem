(function ($) {

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

    // TODO Only backend - move!
    $(document).on('click', '.scheme tbody td', function (e) {
        var cell = $(this);

        if (cell.children().length == 0) {
            // schedule
            var schemeId = $('#scheme-selector select option:selected').val();
            var subjectId = $('#subject-selector select option:selected').val();
            var roomId = $('#room-selector select option:selected').val();
            var date = cell.closest('tbody').prev('thead').find('> tr > th:eq(' + cell.index() + ')').attr('data-datetime');
            var blockNumber = cell.parent().parent().children().index(this.parentNode);

            $.ajax({
                type: 'POST',
                url: 'http://localhost:49415/admin/scheduling/lesson',
                data: 'schemeId=' + schemeId +
                    '&subjectId=' + subjectId +
                    '&roomId=' + roomId + 
                    '&date=' + date +
                    '&blockNumber=' + blockNumber,
                success: function (response) {
                    cell.replaceWith(response);
                }
            });
        } else {
            // select for bulk actions
            cell.toggleClass('selected');
        }
    });

    $(document).on('submit', '#bulk-selector form', function (e) {
        var action = $(this).find('select option:selected').val();
        var cells = $('.scheme tbody td.selected');
        var ids = [];

        $.each(cells, function (index, element) {
            ids.push($(element).attr('data-lesson-id'));
        });

        if (action == 'delete') {

        } else if (action == 'move') {

        }
        console.log(ids);

        $(this).find('select option:first').prop('selected', 'selected');

        cells.removeClass('selected');

        return false;
    });
})(jQuery);