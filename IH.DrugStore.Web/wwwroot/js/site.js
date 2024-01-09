// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {

    // This is for data tables, anything with class .data-tables will be converted in to dataTables
    $('.data-tables').DataTable();


    // This is for Select2, anything with class .select2 will be converted in to Select2
    $('.select2').select2();
});

    
