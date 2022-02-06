// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$("#BuscarEmpleado").on('keyup', function (e) {
    if (e.key === 'Enter' || e.keyCode === 13) {
        var valor = $("#BuscarEmpleado").val();
        var url = "Empleados/EmpleadosPartial";
        var data = { valor: valor }
        debugger;
        $.post(url, data).done(function (r) {
            $("#TblEmpleados").html("");
            $("#TblEmpleados").html(r);
        })
    }
});