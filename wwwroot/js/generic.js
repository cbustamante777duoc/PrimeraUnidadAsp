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

    //var frm = new FormData();
    //var ckecks = document.getElementsByName("nombrePropiedades");
    //var nckecks = ckecks.length;
    //for (var i = 0; i < nckecks; i++) {
    //    if (ckecks[i].checked == true) {

    //        frm.append("nombrePropiedades[]", ckecks[i].value)
    //    }
    //}

    //$.ajax({
    //    type: "POST",
    //    url: "Especialidad/exportarPDFDatos",
    //    //data viene del servido lo que retorna
    //    data: frm,
    //    contentType: false,
    //    processData: false,
    //    success: function (data) {
    //        var a = document.createElement("a");
    //        a.href = data;
    //        a.download = "reporte.pdf";
    //        a.click();

    //    }
    //})
}

//url la hacer la llamada a localhost
//campos array para poder personalizar
//propiedadId=id
//nombreController = nombre del contralor
function pintar(url, campos, propiedadId, nombreController) {

    var contenido = "";
    var tbody = document.getElementById("tbDatos");
    //declaracion de las variables para ser usadas en el for
    var nombreCampo;
    var objetoActual;


    $.get(url, function (data) {
        for (var i = 0; i < data.length; i++) {

            contenido += "<tr>";
            for (var j = 0; j < campos.length; j++) {
                //para obtern los nombres de los campos
                nombreCampo = campos[j];
                objetoActual = data[i];
                contenido += "<td>" + objetoActual[nombreCampo] + "</td>";

            }
            //contenido += "<td>" + data[i].iidespecilidad + "</td>";
            //contenido += "<td>" + data[i].nombre + "</td>";
            //contenido += "<td>" + data[i].descripcion + "</td>";
            contenido += `
                <td>
                        
                        <i class="fa fa-trash btn btn-danger" aria-hidden="true"
                           onclick="EliminarEspecialidad(${objetoActual[propiedadId]})">
                        </i>

                        
                        <a  
                           href="${nombreController}/Editar/${objetoActual[propiedadId]}"
                         >
                           <i class="fa fa-edit btn btn-primary" aria-hidden="true" ></i>
                        </a>
                    </td>
                `

            contenido += "</tr>";


        }

        tbody.innerHTML = contenido;
    })

}