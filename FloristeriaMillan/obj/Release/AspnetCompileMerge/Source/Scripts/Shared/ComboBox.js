var button_orden = document.getElementById("button-orden");
var label_orden = document.getElementById("orden");
var span_orden = document.getElementById("icono-orden");
var contenedor_tipos_orden = document.getElementById("contenedor-tipos-orden");

if (contenedor_tipos_orden != null) {
    contenedor_tipos_orden.style.visibility = "hidden";

    button_orden.addEventListener('click', function myfunction() {

        if (contenedor_tipos_orden.style.visibility == "hidden") {
            contenedor_tipos_orden.style.visibility = "visible";
        } else {
            contenedor_tipos_orden.style.visibility = "hidden";
        }

    });

    document.addEventListener("click", function (event) {
        if (!button_orden.contains(event.target)) {

            if (contenedor_tipos_orden.style.visibility == "visible") {
                contenedor_tipos_orden.style.visibility = "hidden";
            }
        }
    });

    const buttons = document.querySelectorAll('.contenedor-tipos-orden button');
    const labels = document.querySelectorAll('.button-tipo-orden label');
    const label_orden_actual = document.getElementById("orden");

    for (let i = 0; i < buttons.length; i++) {
        buttons[i].addEventListener('click', function myfunction() {
            label_orden_actual.innerHTML = labels[i].innerHTML;
        });
    }
}