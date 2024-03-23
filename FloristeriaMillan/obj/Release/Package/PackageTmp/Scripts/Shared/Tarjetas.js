
const contenedor_tarjetas = document.getElementById("carrusel-productos-recomendados");
const tamaño_pantalla = window.innerWidth;

if (contenedor_tarjetas != null) {
    var tarjeta_mitad = 5;

    var tarjeta_derecha = contenedor_tarjetas.childNodes[tarjeta_mitad];

    tarjeta_derecha.id = "contenedor-tarjeta-central";

    const boton_retroceder = document.getElementById("boton-retroceder-productos-recomendados");
    const boton_avanzar = document.getElementById("boton-avanzar-productos-recomendados");

    if (tarjeta_mitad == 5) {
        boton_retroceder.style.visibility = "hidden";
    }

    if (tarjeta_mitad == 31) {
        boton_avanzar.style.visibility = "hidden";
    }

    function avanzar_tarjeta() {

        boton_avanzar.style.visibility = "visible";
        boton_retroceder.style.visibility = "visible";

        if (tarjeta_mitad == 27) {
            boton_avanzar.style.visibility = "hidden";
        }

        var nueva_tarjeta = contenedor_tarjetas.childNodes[tarjeta_mitad + 4];

        nueva_tarjeta.id = "contenedor-tarjeta-aparecer";

        var tarjeta_izquierda = contenedor_tarjetas.childNodes[tarjeta_mitad - 2];

        tarjeta_izquierda.id = "contenedor-tarjeta-desvanecer";

        var tarjeta_derecha = contenedor_tarjetas.childNodes[tarjeta_mitad + 2];

        tarjeta_derecha.id = "contenedor-tarjeta-central";

        var tarjeta_central = contenedor_tarjetas.childNodes[tarjeta_mitad];

        tarjeta_central.id = "contenedor-tarjeta";

        tarjeta_mitad = tarjeta_mitad + 2;
    }

    function retroceder_tarjeta() {

        boton_retroceder.style.visibility = "visible";
        boton_avanzar.style.visibility = "visible";

        if (tarjeta_mitad == 7) {
            boton_retroceder.style.visibility = "hidden";
        }

        if (tamaño_pantalla > 1100) {
            var tarjeta_izquierda = contenedor_tarjetas.childNodes[tarjeta_mitad - 2];

            tarjeta_izquierda.id = "contenedor-tarjeta-central";

            var nueva_tarjeta = contenedor_tarjetas.childNodes[tarjeta_mitad - 4];

            nueva_tarjeta.id = "contenedor-tarjeta-aparecer";

            var tarjeta_central = contenedor_tarjetas.childNodes[tarjeta_mitad];

            tarjeta_central.id = "contenedor-tarjeta";

            var tarjeta_derecha = contenedor_tarjetas.childNodes[tarjeta_mitad + 2];

            tarjeta_derecha.id = "contenedor-tarjeta-desvanecer";

            tarjeta_mitad = tarjeta_mitad - 2;
        } else {
            var tarjeta_izquierda = contenedor_tarjetas.childNodes[tarjeta_mitad - 2];

            tarjeta_izquierda.id = "contenedor-tarjeta-central";

            var nueva_tarjeta = contenedor_tarjetas.childNodes[tarjeta_mitad - 4];

            nueva_tarjeta.id = "contenedor-tarjeta";

            var tarjeta_central = contenedor_tarjetas.childNodes[tarjeta_mitad];

            tarjeta_central.id = "contenedor-tarjeta-aparecer";

            var tarjeta_derecha = contenedor_tarjetas.childNodes[tarjeta_mitad + 2];

            tarjeta_derecha.id = "contenedor-tarjeta-desvanecer";

            tarjeta_mitad = tarjeta_mitad - 2;
        }

    }
}

const contenedor_tarjetas_novedad = document.getElementById("carrusel-productos-novedad");

if (contenedor_tarjetas_novedad != null) {
    var tarjeta_mitad_nov = 5;

    var tarjeta_derecha = contenedor_tarjetas_novedad.childNodes[tarjeta_mitad_nov];

    tarjeta_derecha.id = "contenedor-tarjeta-central";

    const boton_retroceder = document.getElementById("boton-retroceder-productos-novedad");
    const boton_avanzar = document.getElementById("boton-avanzar-productos-novedad");

    if (tarjeta_mitad_nov == 5) {
        boton_retroceder.style.visibility = "hidden";
    }

    if (tarjeta_mitad_nov == 31) {
        boton_avanzar.style.visibility = "hidden";
    }

    function avanzar_tarjeta_novedad() {

        boton_avanzar.style.visibility = "visible";
        boton_retroceder.style.visibility = "visible";

        if (tarjeta_mitad_nov == 27) {
            boton_avanzar.style.visibility = "hidden";
        }

        var nueva_tarjeta = contenedor_tarjetas_novedad.childNodes[tarjeta_mitad_nov + 4];

        nueva_tarjeta.id = "contenedor-tarjeta-aparecer";

        var tarjeta_izquierda = contenedor_tarjetas_novedad.childNodes[tarjeta_mitad_nov - 2];

        tarjeta_izquierda.id = "contenedor-tarjeta-desvanecer";

        var tarjeta_derecha = contenedor_tarjetas_novedad.childNodes[tarjeta_mitad_nov + 2];

        tarjeta_derecha.id = "contenedor-tarjeta-central";

        var tarjeta_central = contenedor_tarjetas_novedad.childNodes[tarjeta_mitad_nov];

        tarjeta_central.id = "contenedor-tarjeta";

        tarjeta_mitad_nov = tarjeta_mitad_nov + 2;
    }

    function retroceder_tarjeta_novedad() {

        boton_retroceder.style.visibility = "visible";
        boton_avanzar.style.visibility = "visible";

        if (tarjeta_mitad_nov == 7) {
            boton_retroceder.style.visibility = "hidden";
        }

        if (tamaño_pantalla > 1100) {
            var tarjeta_izquierda = contenedor_tarjetas_novedad.childNodes[tarjeta_mitad_nov - 2];

            tarjeta_izquierda.id = "contenedor-tarjeta-central";

            var nueva_tarjeta = contenedor_tarjetas_novedad.childNodes[tarjeta_mitad_nov - 4];

            nueva_tarjeta.id = "contenedor-tarjeta-aparecer";

            var tarjeta_central = contenedor_tarjetas_novedad.childNodes[tarjeta_mitad_nov];

            tarjeta_central.id = "contenedor-tarjeta";

            var tarjeta_derecha = contenedor_tarjetas_novedad.childNodes[tarjeta_mitad_nov + 2];

            tarjeta_derecha.id = "contenedor-tarjeta-desvanecer";

            tarjeta_mitad_nov = tarjeta_mitad_nov - 2;
        } else {
            var tarjeta_izquierda = contenedor_tarjetas_novedad.childNodes[tarjeta_mitad_nov - 2];

            tarjeta_izquierda.id = "contenedor-tarjeta-central";

            var nueva_tarjeta = contenedor_tarjetas_novedad.childNodes[tarjeta_mitad_nov - 4];

            nueva_tarjeta.id = "contenedor-tarjeta";

            var tarjeta_central = contenedor_tarjetas_novedad.childNodes[tarjeta_mitad_nov];

            tarjeta_central.id = "contenedor-tarjeta-aparecer";

            var tarjeta_derecha = contenedor_tarjetas_novedad.childNodes[tarjeta_mitad_nov + 2];

            tarjeta_derecha.id = "contenedor-tarjeta-desvanecer";

            tarjeta_mitad_nov = tarjeta_mitad_nov - 2;
        }
    }
}

const botones_favoritos = document.querySelectorAll(".boton-favoritos");
const botones_unfavoritos = document.querySelectorAll(".boton-unfavoritos");

for (let i = 0; i < botones_favoritos.length; i++) {
    botones_favoritos[i].addEventListener('click', function myfunction() {
        botones_favoritos[i].style.display = "none";
        botones_unfavoritos[i].style.display = "flex";
    });
}

for (let j = 0; j < botones_unfavoritos.length; j++) {
    botones_unfavoritos[j].addEventListener('click', function () {
        botones_unfavoritos[j].style.display = "none";
        botones_favoritos[j].style.display = "flex";
    });
}

function ir_articulo(articulo) {
    const xhr = new XMLHttpRequest();
    const datos = new FormData();
    datos.append('articulo', articulo);

    xhr.open('POST', '/HomePrincipal/Categoria');
    xhr.onload = function () {
        if (xhr.status === 200) {
            window.location.href = "/HomePrincipal/Articulo";
        } else {
            console.error('Error en la solicitud');
        }
    };
    xhr.send(datos);
}


