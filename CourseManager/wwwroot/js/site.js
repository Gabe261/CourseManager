$(document).ready(function () {
    // set up the jQueryui datepicker componet
    $('input[type=datetime]').datepicker({
        dateFormat: 'mm/dd/yy',
        changeMonth: true,
        changeYear: true,
        yearRange: "-100:+100"
    });
});