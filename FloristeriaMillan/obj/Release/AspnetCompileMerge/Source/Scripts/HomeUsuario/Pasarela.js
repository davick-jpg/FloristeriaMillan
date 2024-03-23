function generar_checkout() {
    var radios = document.getElementsByName('direccionSeleccionada');

    var valorSeleccionado

    var datos_fecha = generar_fecha();

    if (datos_fecha == null) {
        return false;
    }

    for (var i = 0; i < radios.length; i++) {
        if (radios[i].checked) {
            valorSeleccionado = radios[i].parentNode.childNodes[3].childNodes[3];

            break;
        }
    }

    if (valorSeleccionado != undefined) {
        return enviar_informacion(valorSeleccionado.innerText, datos_fecha.fecha, datos_fecha.hora);
    }

    var contenedor = document.getElementsByClassName('contenedor-direccion')[0];

    if (contenedor != null) {
        var camposDireccion = contenedor.getElementsByTagName('input');
        for (var j = 0; j < camposDireccion.length; j++) {
            if (camposDireccion[j].value.trim() === '') {

                camposDireccion[j].focus();

                return false;
            }
        }
    }

    var inputs = document.querySelectorAll('.formulario-direccion input');
    var valores = [];

    inputs.forEach(function (input) {
        valores.push(input.value.trim());
    });

    var concatenado = valores.join(', ');

    if (concatenado != null) {
        enviar_informacion(concatenado, datos_fecha.fecha, datos_fecha.hora);
    }
}

var radioButtons = document.querySelectorAll('.radio-direccion');

if (radioButtons != null) {
    radioButtons.forEach(function (button) {
        button.addEventListener('click', function () {
            var radio = this.querySelector('input');
            var estaSeleccionado = radio.checked;

            if (estaSeleccionado) {
                radio.checked = false;
            } else {
                radioButtons.forEach(function (button) {
                    button.querySelector('input').checked = false;
                });

                radio.checked = true;
            }
        });
    });
}

function enviar_informacion(direccion, fecha, hora) {
    var dato = {
        direccion: direccion,
        fecha: fecha,
        hora: hora
    };

    $.ajax({
        url: '/HomeUsuario/generar_checkout',
        type: 'POST',
        dataType: 'json',
        data: dato,
        success: function (result) {
            generar_pago();
        },
        error: function (xhr, status, error) {
            console.log("error")
        }
    });
}

function generar_pago() {
    var formulario = document.querySelector('.checkout');
    formulario.submit();
}

function generar_fecha() {
    var fecha = document.querySelector(".contenedor-fecha input").value;
    var hora;

    if (fecha == null) {
        return null;
    }

    var rango1 = document.getElementById("rango1");
    var rango2 = document.getElementById("rango2");
    var rango3 = document.getElementById("rango3");

    if (rango1.checked) {
        hora = "1";
    } else if (rango2.checked) {
        hora = "2";
    } else if (rango3.checked) {
        hora = "3";
    } else {
        return null;
    }

    var datos = {
        fecha: fecha,
        hora: hora
    };

    return datos;
}

document.addEventListener('DOMContentLoaded', function () {
    var fechaInput = document.querySelector(".contenedor-fecha input");
    if (fechaInput != null) {
        var tomorrow = new Date();
        tomorrow.setDate(tomorrow.getDate() + 1);
        var formattedDate = tomorrow.toISOString().split('T')[0];
        fechaInput.min = formattedDate;
    }
});

function mostrar_horas() {
    var contenedor_hora = document.querySelector(".contenedor-hora");
    contenedor_hora.style.display = "flex";
}

