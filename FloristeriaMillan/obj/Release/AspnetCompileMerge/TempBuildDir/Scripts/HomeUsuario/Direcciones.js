function mandar_datos(boton) {
    var formData = $('form').serialize();

    $.ajax({
        url: '/HomeUsuario/Direcciones',
        type: 'POST',
        data: formData,
        success: function (response) {
            window.location.reload(true);
        },
        error: function (xhr, status, error) {
        }
    });
}

function eliminar_direccion(boton) {
    var contenedor = ((boton.parentNode.childNodes[1]).childNodes[1]).childNodes[1];

    var datos = {
        identificador: contenedor.innerText
    }

    $.ajax({
        url: '/HomeUsuario/eliminar_direccion',
        type: 'POST',
        data: datos,
        success: function (result) {
            window.location.reload(true);
        },
        error: function (xhr, status, error) {

        }
    });
}

function buscar_informacion(boton) {
    var contenedor = boton.childNodes[1].childNodes[1];

    var datos = {
        identificador: contenedor.innerText
    }

    $.ajax({
        url: '/HomeUsuario/buscar_direccion',
        type: 'POST',
        dataType: 'json',
        data: datos,
        success: function (result) {
            inputs_usuario = document.querySelectorAll('.formulario-direccion input')

            inputs_usuario[0].value = result.identificador;
            inputs_usuario[1].value = result.calle;
            inputs_usuario[2].value = result.colonia;
            inputs_usuario[3].value = result.municipio;
            inputs_usuario[4].value = result.codigo_postal;

        },
        error: function (xhr, status, error) {

        }
    });
}