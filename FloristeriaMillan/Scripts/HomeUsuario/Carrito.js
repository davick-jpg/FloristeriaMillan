$(document).ready(function () {
    if (window.location.pathname === '/HomeUsuario/carrito' || window.location.pathname === '/HomeUsuario/Carrito') {
        $.ajax({
            url: '/HomeUsuario/generar_descripcion',
            type: 'POST',
            dataType: 'json',
            success: function (result) {
                for (var i = 0; i < result.descripciones.length; i++) {
                    var elemento = document.querySelectorAll(".descripcion-articulo");

                    if (elemento.length > 0) {
                        elemento[i].innerText = "\t" + result.descripciones[i];
                    }
                }
            },
            error: function (xhr, status, error) {
                console.log("error");
            }
        });
    }
});

function aumentar(boton) {
    var nombre = (((boton.parentNode).parentNode).parentNode).childNodes[1].childNodes[1];
    var cantidad = ((boton.parentNode).parentNode).childNodes[3];

    if (nombre.textContent.trim() != "") {
        var numero = parseInt(cantidad.textContent);

        numero++;

        cantidad.innerText = numero;

        var dato = {
            nombre: nombre.innerText,
            cantidad: numero
        };

        $.ajax({
            url: '/HomeUsuario/editar_informacion',
            type: 'POST',
            dataType: 'json',
            data: dato,
            success: function (result) {
                location.reload();
            },
            error: function (xhr, status, error) {
                console.log("error")
            }
        });
    }
}

function disminuir(boton) {
    var nombre = (((boton.parentNode).parentNode).parentNode).childNodes[1].childNodes[1];
    var cantidad = ((boton.parentNode).parentNode).childNodes[3];

    var numero = parseInt(cantidad.textContent);

    if (nombre.textContent.trim() != "" && numero > 1) {

        numero--;

        cantidad.innerText = numero;

        var dato = {
            nombre: nombre.innerText,
            cantidad: numero
        };

        $.ajax({
            url: '/HomeUsuario/editar_informacion',
            type: 'POST',
            dataType: 'json',
            data: dato,
            success: function (result) {
                location.reload();
            },
            error: function (xhr, status, error) {
                console.log("error")
            }
        });
    }
}