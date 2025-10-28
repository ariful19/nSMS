$(document).ready(function () {
    load();
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        load();
    }
});
function load() {
    $("#btnSave,#btnEdit,#btnSchoolInfo").on('click', function () {
        $(".message").slideDown('slow');
        $(".message").delay(5000).fadeOut(2000);
    });
    var lan;
    var cookie = getCookieValue("CurrentLanguage");
    if (cookie == 'bn-BD')
    {
        lan = "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Bangla.json";
    }
    else
    {
        lan = "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/English.json";
    }
    var table = $('#example1').DataTable().destroy();
    table = $('#example1').DataTable({
        "language": {
            "url": lan
        },
        "paging": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": true
    });
}

function getCookieValue(name) {
    cookieList = document.cookie.split('; ');
    cookies = {};
    for (i = cookieList.length - 1; i >= 0; i--) {
        cookie = cookieList[i].split('=');
        cookies[cookie[0]] = cookie[1];
    }
    return cookies[name];
}

$('li a').each(function () {
    if ($($(this))[0].href == String(window.location)) {
        $(this).parent().addClass('active');
    }
});
$('li ul li a').each(function () {
    if ($($(this))[0].href == String(window.location)) {
        $(this).parent().addClass('active');
        $(this).parent().parent().show();
    }
});
