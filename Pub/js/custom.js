$(document).ready(function () {
    if ($('#example').length > 0) {
        $('#example').DataTable({

        });
    }
    $(".toggler").click(function (event) {
        event.preventDefault();
        $(this).parent(".panel-title").parent(".panel-heading").next(".panel-collapse").stop().slideToggle();
        $(this).parent(".panel-title").toggleClass("panel-active");
        $(this).children(".fa").toggleClass("fa-angle-down fa-angle-up");
    });
});