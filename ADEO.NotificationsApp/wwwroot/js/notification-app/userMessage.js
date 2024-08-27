$(document).ready(() => {

    toastr.options = {
        closeButton: true,
        debug: false,
        newestOnTop: true,
        progressBar: true,
        positionClass: "toast-top-right",
        preventDuplicates: false,
        onclick: null,
        showDuration: "300",
        hideDuration: "1000",
        timeOut: "5000",
        extendedTimeOut: "1000",
        showEasing: "swing",
        hideEasing: "linear",
        showMethod: "fadeIn",
        hideMethod: "fadeOut",
    };

    setTimeout(function () {
        
        var userMessagesList = $("#userMessagesList").DataTable({ 
            responsive: true,
            order: [[2, 'desc']],
            sort: false
        });

        var userHistoryList = $("#userHistoryList").DataTable({
            responsive: true,
            order: [[2, 'desc']],
            sort: false
        });
    }, 2000); 
});