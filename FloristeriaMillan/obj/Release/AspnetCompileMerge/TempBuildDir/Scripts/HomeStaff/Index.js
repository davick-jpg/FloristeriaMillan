function cargarVistaParcial(nombreVista, id) {
    var lista_botones = document.querySelectorAll('.boton-opcion')

    lista_botones.forEach(function (boton) {
        boton.className = "boton-opcion"
    });

    lista_botones[id].className = "boton-opcion boton-activo"


    $.ajax({
        url: '/HomeStaff/' + nombreVista,
        type: 'GET',
        success: function (data) {
            $('#contenedor-opcion').html(data);

            if (nombreVista == "Productos") {
                $.ajax({
                    url: '/HomeStaff/generar_descripcion',
                    type: 'POST',
                    dataType: 'json',
                    success: function (result) {
                        for (var i = 0; i < result.descripciones.length; i++) {
                            var elemento = document.querySelectorAll(".descripcion");

                            if (elemento.length > 0) {
                                elemento[i].innerText = result.descripciones[i];
                            }
                        }
                    },
                    error: function (xhr, status, error) {
                        console.log("error");
                    }
                });
            }
        }
    });
}

function ir_a(direccion) {
    window.location.href = direccion;
}