function buscar_producto_sugerencia() {
    var input = document.querySelector(".buscador input")

    var dato = {
        cadena: (input).value
    };


    if ((input).value.trim() == "") {
        eliminar_sugerencias();

        var input = document.querySelector(".buscador input")
        input.value = "";
    } else {
        var xhr = new XMLHttpRequest();
        xhr.open('POST', '/HomePrincipal/buscar_sugerencias', true);
        xhr.setRequestHeader('Content-Type', 'application/json');
        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4) {
                if (xhr.status === 200) {
                    var result = JSON.parse(xhr.responseText);
                    eliminar_sugerencias();
                    for (var i = 0; i < result.sugerencias.length; i++) {
                        var elemento = result.sugerencias[i];
                        agregar_boton_sugerencia(elemento);
                    }
                } else {
                    console.log('error');
                }
            }
        };
        xhr.send(JSON.stringify(dato));
    }
}

function agregar_boton_sugerencia(dato) {
    var contenedor_sugerencias = document.querySelector(".sugerencias");
    contenedor_sugerencias.style.display = "flex"


    var codigoHTML = `<li class="sugerencia">
                                    <button class="boton-sugerencia" onclick="ir_articulo('${dato}')">
                                        <label class="texto-sugerencia">
                                            ${dato}
                                        </label>
                                    </button>
                     </li>
                     <i class="divisor"></i>
                     `;

    var tempDiv = document.createElement('div');
    tempDiv.innerHTML = codigoHTML;

    for (var i = 0; i < tempDiv.childNodes.length; i++) {
        var elemento = tempDiv.childNodes[i];

        contenedor_sugerencias.appendChild(elemento);
    }
}

function eliminar_sugerencias() {
    var contenedor_sugerencias = document.querySelector(".sugerencias");
    contenedor_sugerencias.style.display = "none"

    var sugerencias = document.querySelectorAll(".sugerencia");

    for (var i = 0; i < sugerencias.length; i++) {
        sugerencias[i].remove();
    }

    var divisores = document.querySelectorAll(".sugerencias .divisor");

    for (var i = 0; i < divisores.length; i++) {
        divisores[i].remove();
    }
}

document.addEventListener('click', function (event) {
    var contenedor = document.querySelector('.buscador');

    var targetElement = event.target;

    if (contenedor != null && !contenedor.contains(targetElement)) {
        eliminar_sugerencias();

        var input = document.querySelector(".buscador input")
        input.value = "";
    }
});