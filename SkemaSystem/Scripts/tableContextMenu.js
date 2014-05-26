$(document).ready(function(){
    $.contextMenu({
        selector: '#cell',
        callback: function (key, options) {
            // This will pull the data that is stored in the data attribute
            console.log("You clicked on %s, which has the data-cell-id of: %s", key, $(this).data('cellId'));
        },
        items: {
            "move": { name: "Flyt lokale"},
            "delete": { name: "Slet lokale"}
        }
    });
});