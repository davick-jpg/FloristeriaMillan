const inputElement = document.getElementById('dedicatoria-input');

if (inputElement != null) {
    inputElement.addEventListener('input', function (event) {
        const text = event.target.value;
        const maxLength = 200;
        if (text.length > maxLength) {
            event.target.value = text.slice(0, maxLength);
        }
        event.target.setAttribute('rows', Math.max(text.split('\n').length, 2));
    });
}

var lista_botones = document.querySelectorAll('.contenedor-galeria-articulo button');
var lista_imagenes = document.querySelectorAll('.contenedor-galeria-articulo button img');

var boton_activo = document.querySelector('.buton-imagen-activo');
var imagen_activa = document.querySelector('.imagen-activa');

if (boton_activo != null) {
    for (let i = 0; i < lista_botones.length; i++) {
        lista_botones[i].addEventListener('click', function myfunction() {
            if (lista_botones[i].className == "boton-imagen") {
                lista_botones[i].className = "buton-imagen-activo";
                lista_imagenes[i].className = "imagen-activa";

                imagen_activa.className = "imagen-inactiva";
                boton_activo.className = "boton-imagen";

                boton_activo = lista_botones[i];
                imagen_activa = lista_imagenes[i];
            }
        });
    }
}

var boton_retroceder = document.querySelector('#boton-retroceder-articulos-recomendados');
var boton_avanzar = document.querySelector('#boton-avanzar-articulos-recomendados');
var lista_tarjetas = document.querySelectorAll('.contenedor-tarjeta');
var tarjeta_activa = 0;

if (boton_avanzar != null) {
    boton_retroceder.addEventListener('click', function myfunction() {
        boton_avanzar.style.visibility = "visible";
        lista_tarjetas[tarjeta_activa].id = "tarjeta-articulo-inactiva";
        lista_tarjetas[tarjeta_activa - 1].id = "tarjeta-articulo-activa";
        tarjeta_activa--;

        if (tarjeta_activa == 0) {
            boton_retroceder.style.visibility = "hidden";
        } else {
            boton_retroceder.style.visibility = "visible";
        }
    });
}

if (boton_avanzar != null) {
    boton_avanzar.addEventListener('click', function myfunction() {
        boton_retroceder.style.visibility = "visible";
        lista_tarjetas[tarjeta_activa].id = "tarjeta-articulo-inactiva";
        lista_tarjetas[tarjeta_activa + 1].id = "tarjeta-articulo-activa";
        tarjeta_activa++;

        if (tarjeta_activa == 2) {
            boton_avanzar.style.visibility = "hidden";
        } else {
            boton_avanzar.style.visibility = "visible";
        }
    });
}

function anadir_carrito() {
    var nombrePro = document.querySelector(".titulo-articulo").innerText;

    var textArea = document.querySelector("#dedicatoria-input");

    var dato = {
        nombrePro: nombrePro,
        dedicatoria: textArea.value
    };

    $.ajax({
        url: '/HomePrincipal/anadir_carrito',
        type: 'POST',
        dataType: 'json',
        data: dato,
        success: function (result) {
            efecto_carrito();
        },
        error: function (xhr, status, error) {
            console.log("error")
        }
    });
}

function efecto_carrito() {
    var boton_carrito = document.querySelector(".boton-carrito");

    boton_carrito.classList.add('clicked');

    setTimeout(function () {
        boton_carrito.classList.remove('clicked');
    }, 1000);
}

