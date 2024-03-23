function desplegar() {
    var div_titulo = document.querySelector(".titulo-despegable");
    var div_menu = document.querySelector(".menu");
    var boton_contraer = document.querySelector(".boton-contraer");

    if (div_menu.style.display == "none" || div_menu.style.display == "") {
        div_menu.style.display = "flex";
        div_titulo.style.display = "flex";
        boton_contraer.style.display = "flex";
    }
}

function contraer() {
    var div_titulo = document.querySelector(".titulo-despegable");
    var div_menu = document.querySelector(".menu");
    var boton_contraer = document.querySelector(".boton-contraer");

    if (div_menu.style.display == "flex") {
        div_menu.style.display = "none";
        div_titulo.style.display = "none";
        boton_contraer.style.display = "none";
    }
}

var div_menu_usuario = document.querySelector(".menu-usuario");

if (div_menu_usuario != null) {
    document.addEventListener("click", function (event) {

        if (div_menu_usuario.style.display != "none" && !div_menu_usuario.contains(event.target) && window.innerWidth < 750) {
            var div_menu = document.querySelector(".menu");
            var boton_contraer = document.querySelector(".boton-contraer");
            div_menu.style.display = "none";
            boton_contraer.style.display = "none";
        }
    });

    window.addEventListener('resize', function myfunction() {
        if (window.innerWidth > 750) {
            var div_menu = document.querySelector(".menu");
            var boton_contraer = document.querySelector(".boton-contraer");
            var div_titulo = document.querySelector(".titulo-despegable");
            div_menu.style.display = "flex";
            div_titulo.style.display = "none";
            boton_contraer.style.display = "none";
        }

        if (window.innerWidth < 750) {
            var div_menu = document.querySelector(".menu");
            var boton_contraer = document.querySelector(".boton-contraer");
            var div_titulo = document.querySelector(".titulo-despegable");
            div_menu.style.display = "none";
            div_titulo.style.display = "none";
            boton_contraer.style.display = "none";
        }
    });
}

var buscador = document.querySelector(".buscador");

if (buscador != null && window.innerWidth < 750) {
    var inicio = document.querySelector(".header-logo");
    var menu_usuario = document.querySelector(".menu-usuario");

    window.addEventListener('click', function (event) {
        if (buscador.contains(event.target) && window.innerWidth < 750) {
            buscador.className = "buscador grande";
            inicio.style.display = "none";
            menu_usuario.style.display = "none";
        } else {
            buscador.className = "buscador";
            inicio.style.display = "flex";
            menu_usuario.style.display = "flex";
        }
    });
}

function ir_a(direccion) {
    window.location.href = direccion;
}

