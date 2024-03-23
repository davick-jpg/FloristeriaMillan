function finalizar_factura(boton) {
    var tarjeta_pedido = ((boton.parentNode).parentNode).parentNode;

    var contenedor_resumen = tarjeta_pedido.childNodes[3];

    var idFac = parseFloat((contenedor_resumen.childNodes[1].innerText).split('#')[1].trim());

    var dato = {
        idFac: idFac
    };

    $.ajax({
        url: '/HomeStaff/finalizar_factura',
        type: 'POST',
        dataType: 'json',
        data: dato,
        success: function (result) {
            window.location.href = '/HomeStaff/Index';
        },
        error: function (xhr, status, error) {
            console.log("error")
        }
    });
}

function cancelar_factura(boton) {
    var tarjeta_pedido = ((boton.parentNode).parentNode).parentNode;

    var contenedor_resumen = tarjeta_pedido.childNodes[3];

    var idFac = parseFloat((contenedor_resumen.childNodes[1].innerText).split('#')[1].trim());

    var dato = {
        idFac: idFac
    };

    $.ajax({
        url: '/HomeStaff/cancelar_factura',
        type: 'POST',
        dataType: 'json',
        data: dato,
        success: function (result) {
            window.location.href = '/HomeStaff/Index';
        },
        error: function (xhr, status, error) {
            console.log("error")
        }
    });
}

function filtrar_pedidos() {
    var select = document.querySelector(".clase");

    var dato = {
        filtro: select.value
    };

    $.ajax({
        url: '/HomeStaff/filtrar_pedidos',
        type: 'Get',
        data: dato,
        success: function (result) {
            $('#contenedor-opcion').html(result);
        },
        error: function (xhr, status, error) {
            console.log("error")
        }
    });
}