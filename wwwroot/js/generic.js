function mostrarModal(titulo = "¿Desea guardar los cambios?",
    texto ="¿Deseas registrar los cambios en la base de datos?") {

    return Swal.fire({
           title: titulo,
            text: texto,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Si!'
    })
}

//funcion imprimir
function Imprimir() {
    //obtener el html que tiene el id tcheckbox
    var tcheckbox = document.getElementById("tcheckbox").outerHTML;
    //obtengo todo el contenido del la tabla
    var tabla = "<h1>Reporte a Imprimir</h1>"
    tabla += document.getElementById("tabla").outerHTML;
    //elimino todo lo que tiene un id tcheckbox
    tabla = tabla.replace(tcheckbox, "");

    //todo el contenido de la www
    var pagina = window.document.body;
    var ventana = window.open();
    //en la nueva venta agrego la tabla
    ventana.document.write(tabla);
    ventana.print();
    ventana.close();
    //devuelta a la pagina
    window.document.body = pagina;

}

// funcion table.js
window.onload = function () {
    $(document).ready(function () {
        $('#tabla').DataTable();
    });

}


//funcion para exportar excel
function ExportarExcel() {
    document.getElementById("tipoReporte").value = "Excel";
    var frmReporte = document.getElementById("frmReporte");
    frmReporte.submit();
}

//funcion para exportar word
function ExportarWord() {
    document.getElementById("tipoReporte").value = "Word";
    var frmReporte = document.getElementById("frmReporte");
    frmReporte.submit();
}

//funcion para exportar Pdf
function ExportarPdf() {
    document.getElementById("tipoReporte").value = "PDF";
    var frmReporte = document.getElementById("frmReporte");
    frmReporte.submit();
}