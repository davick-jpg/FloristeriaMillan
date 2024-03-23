const botonesDesplegar = document.querySelectorAll('.desplegar-boton');
const botonesContraer = document.querySelectorAll('.contraer-boton');

for (let i = 0; i < botonesDesplegar.length; i++) {
    botonesDesplegar[i].addEventListener('click', function () {
        const contenedorBotonesFiltro = document.querySelectorAll('.contenedor-botones-filtro')[i];
        contenedorBotonesFiltro.style.display = "flex";

        botonesDesplegar[i].style.visibility = 'hidden';
        botonesDesplegar[i].style.position = 'absolute';

        botonesContraer[i].style.visibility = 'visible';
        botonesContraer[i].style.position = 'relative';
    });
}

for (let i = 0; i < botonesContraer.length; i++) {
    botonesContraer[i].addEventListener('click', function () {
        const contenedorBotonesFiltro = document.querySelectorAll('.contenedor-botones-filtro')[i];
        contenedorBotonesFiltro.style.display = "none";

        botonesContraer[i].style.visibility = 'hidden';
        botonesContraer[i].style.position = 'absolute';

        botonesDesplegar[i].style.visibility = 'visible';
        botonesDesplegar[i].style.position = 'relative';
    });
}

const botonesFiltro = document.querySelectorAll('.boton-filtro');

for (let i = 0; i < botonesFiltro.length; i++) {
    botonesFiltro[i].addEventListener('click', function myfunction() {
        if (botonesFiltro[i].id == "activo") {
            botonesFiltro[i].id = "inactivo";
        } else {
            botonesFiltro[i].id = "activo";
        }
    });
}

const lis_contenedores = document.querySelectorAll('.contenedor-clase-filtros');
const lis_divisores = document.querySelectorAll('.divisor-categoria');
const boton_desplegar = document.querySelector('#desplegar-cont-boton');
const boton_contraer = document.querySelector('#contraer-cont-boton');

if (lis_contenedores.length > 0) {
    boton_desplegar.addEventListener('click', function myfunction() {

        boton_desplegar.style.display = "none";
        boton_contraer.style.display = "flex";

        for (var i = 0; i < lis_contenedores.length; i++) {
            lis_contenedores[i].style.display = "flex";
        }

        for (var i = 0; i < lis_divisores.length; i++) {
            lis_divisores[i].style.display = "flex";
        }
    });

    boton_contraer.addEventListener('click', function myfunction() {

        boton_desplegar.style.display = "flex";
        boton_contraer.style.display = "none";

        for (var i = 0; i < lis_contenedores.length; i++) {
            lis_contenedores[i].style.display = "none";
        }

        for (var i = 0; i < lis_divisores.length; i++) {
            lis_divisores[i].style.display = "none";
        }
    });
}

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

function filtrar_catalogo(boton) {

    var formulario = boton.form;

    formulario.submit();
}
