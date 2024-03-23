
function mostrar_vista_parcial() {
    var vista_parcial = document.querySelector(".vista-parcial")
    vista_parcial.style.display = "flex"
}

function cerrarventana_proveedor() {
    $.ajax({
        url: '/HomeStaff/Proveedores',
        type: 'GET',
        success: function (result) {
            $('#contenedor-opcion').html(result);
        },
        error: function (xhr, status, error) {
            console.log("error")
        }
    });
}

function eliminar_proveedor(boton) {
    var tarjeta = (boton.parentNode).parentNode;
    var dato = {
        nombre: (tarjeta.firstElementChild).textContent
    };

    $.ajax({
        url: '/HomeStaff/eliminar_proveedor',
        type: 'GET',
        data: dato,
        success: function (result) {
            $('#contenedor-opcion').html(result);
        },
        error: function (xhr, status, error) {
            console.log("error")
        }
    });

}

function enviar_datos_proveedor(boton) {
    var formData = $('form').serialize();

    $.ajax({
        url: '/HomeStaff/Proveedores_formulario',
        type: 'Get',
        data: formData,
        success: function (result) {
            $('#contenedor-opcion').html(result);
        },
        error: function (xhr, status, error) {
        }
    });
}

function buscar_proveedor(celda) {
    datos_proveedor = celda.parentNode
    definir_datos_proveedor(datos_proveedor);
    var vista_parcial = document.querySelector(".vista-parcial");
    vista_parcial.style.display = "flex";
}

function definir_datos_proveedor(datos_proveedor) {
    dato = datos_proveedor.children;

    var datos = {
        nombre: dato[0].innerText
    }

    $.ajax({
        url: '/HomeStaff/buscar_proveedor',
        type: 'POST',
        dataType: 'json',
        data: datos,
        success: function (result) {
            inputs_empresa = document.querySelectorAll('.inputbox-empresa input')
            inputs_contacto = document.querySelectorAll('.inputbox-contacto input')

            inputs_empresa[0].value = result.nomEmpresa;
            inputs_empresa[1].value = result.dirEmpresa;
            inputs_empresa[2].value = result.telEmpresa;

            inputs_contacto[0].value = result.nomContacto;
            inputs_contacto[1].value = result.emailContacto;
            inputs_contacto[2].value = result.telContacto;
        },
        error: function (xhr, status, error) {

        }
    });
}