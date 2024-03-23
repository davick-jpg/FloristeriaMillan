function agregar_favorito(boton) {
    var boton_agregar = (boton.parentNode).childNodes[1];

    var boton_quitar = (boton.parentNode).childNodes[3];

    var contendor_tarjeta = (boton.parentNode).parentNode;

    var dato = {
        nombrePro: (contendor_tarjeta.childNodes[7]).childNodes[1].innerText
    };

    $.ajax({
        url: '/HomePrincipal/agregar_favorito',
        type: 'POST',
        dataType: 'json',
        data: dato,
        success: function (result) {
            boton_agregar.display = "none";

            boton_quitar.display = "flex";
        },
        error: function (xhr, status, error) {
            console.log("error")
        }
    });
}

function quitar_favorito(boton) {
    var boton_agregar = (boton.parentNode).childNodes[1];

    var boton_quitar = (boton.parentNode).childNodes[3];

    var contendor_tarjeta = (boton.parentNode).parentNode;

    var dato = {
        nombrePro: (contendor_tarjeta.childNodes[7]).childNodes[1].innerText
    };

    $.ajax({
        url: '/HomePrincipal/quitar_favorito',
        type: 'POST',
        dataType: 'json',
        data: dato,
        success: function (result) {
            boton_agregar.display = "flex";

            boton_quitar.display = "none";
        },
        error: function (xhr, status, error) {
            console.log("error")
        }
    });
}

function ir_categoria(categoria) {
    const xhr = new XMLHttpRequest();
    const datos = new FormData();
    datos.append('categoria', categoria);

    xhr.open('POST', '/HomePrincipal/Index');
    xhr.onload = function () {
        if (xhr.status === 200) {
            window.location.href = "/HomePrincipal/Categoria";
        } else {
            console.error('Error en la solicitud');
        }
    };
    xhr.send(datos);
}