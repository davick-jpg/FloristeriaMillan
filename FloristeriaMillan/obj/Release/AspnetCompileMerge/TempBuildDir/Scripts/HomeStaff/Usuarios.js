

function mostrar_vista_parcial() {
    var vista_parcial = document.querySelector(".vista-parcial")
    vista_parcial.style.display = "flex"
}

function cerrarventana_usuario() {
    $.ajax({
        url: '/HomeStaff/Usuarios',
        type: 'GET',
        success: function (result) {
            $('#contenedor-opcion').html(result);
        },
        error: function (xhr, status, error) {
            console.log("error")
        }
    });
}

function eliminar_usuario(boton) {
    var tarjeta = (boton.parentNode).parentNode;

    var dato = {
        nombre: (tarjeta.childNodes[3]).textContent
    };

    $.ajax({
        url: '/HomeStaff/eliminar_usuario',
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

function enviar_datos_usuario(boton) {
    var formData = $('form').serialize();

    $.ajax({
        url: '/HomeStaff/Usuarios_formulario',
        type: 'Get',
        data: formData,
        success: function (result) {
            $('#contenedor-opcion').html(result);
        },
        error: function (xhr, status, error) {
        }
    });
}

function buscar_usuario(celda) {
    datos_usuario = celda.parentNode
    definir_datos_usuario(datos_usuario);
    var vista_parcial = document.querySelector(".vista-parcial");
    vista_parcial.style.display = "flex";
}

function definir_datos_usuario(datos_usuario) {
    dato = datos_usuario.children;

    var datos = {
        correo: dato[1].innerText
    }

    $.ajax({
        url: '/HomeStaff/buscar_usuario',
        type: 'POST',
        dataType: 'json',
        data: datos,
        success: function (result) {
            inputs_usuario = document.querySelectorAll('.inputbox input')
            combobox = document.querySelector('.clase')

            inputs_usuario[0].value = result.nombreCli;
            inputs_usuario[1].value = result.emailCli;
            inputs_usuario[2].value = result.passwordCli;
            inputs_usuario[3].value = result.telefonoCli;

            combobox.value = result.rol;
        },
        error: function (xhr, status, error) {

        }
    });

}
