$(function () {
    $('#employeesTogether').DataTable({
        "order": [[3, "desc"]],
        "pageLength": 25,
        fixedHeader: true
    } );    
})