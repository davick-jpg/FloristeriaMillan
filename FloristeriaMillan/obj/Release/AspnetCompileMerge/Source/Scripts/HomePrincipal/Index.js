const contenedor_banners = document.getElementById("carrusel-banner-promociones");

if (contenedor_banners != null) {
    var banner = 3;

    const boton_retroceder = document.getElementById("boton-retroceder-banner-promociones");
    const boton_avanzar = document.getElementById("boton-avanzar-banner-promociones");

    if (banner == 3) {
        boton_retroceder.style.visibility = "hidden";
    }

    if (banner == 7) {
        boton_avanzar.style.visibility = "hidden";
    }

    function avanzar_banner() {

        boton_avanzar.style.visibility = "visible";
        boton_retroceder.style.visibility = "visible";

        var banner_activo = contenedor_banners.childNodes[banner];

        banner_activo.style.visibility = "hidden";
        banner_activo.style.position = "absolute";

        var nuevo_banner = contenedor_banners.childNodes[banner + 2];

        nuevo_banner.style.visibility = "visible";
        nuevo_banner.style.position = "relative";

        banner = banner + 2;

        if (banner == 7) {
            boton_avanzar.style.visibility = "hidden";
        }
    }

    function retroceder_banner() {

        boton_avanzar.style.visibility = "visible";
        boton_retroceder.style.visibility = "visible";

        var banner_activo = contenedor_banners.childNodes[banner];

        banner_activo.style.visibility = "hidden";
        banner_activo.style.position = "absolute";

        var nuevo_banner = contenedor_banners.childNodes[banner - 2];

        nuevo_banner.style.visibility = "visible";
        nuevo_banner.style.position = "relative";

        banner = banner - 2;

        if (banner == 3) {
            boton_retroceder.style.visibility = "hidden";
        }
    }
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