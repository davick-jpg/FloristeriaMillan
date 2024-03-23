
function buscar_producto_venta() {
    var contenedor_sugerencias = document.querySelector(".sugerencias");
    var input = document.querySelector(".buscador input")

    var dato = {
        cadena: (input).value
    };


    if ((input).value.trim() == "") {
        eliminar_sugerencias_venta();

        var input = document.querySelector(".buscador input")
        input.value = "";
    } else {
        var xhr = new XMLHttpRequest();
        xhr.open('POST', '/HomeStaff/buscar_articulos_venta', true);
        xhr.setRequestHeader('Content-Type', 'application/json');
        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4) {
                if (xhr.status === 200) {
                    var result = JSON.parse(xhr.responseText);
                    eliminar_sugerencias_venta();
                    for (var i = 0; i < result.resultados.length; i++) {
                        var elemento = result.resultados[i];
                        agregar_boton_venta(elemento);
                    }
                } else {
                    console.log('error');
                }
            }
        };
        xhr.send(JSON.stringify(dato));
    }
}

function limpiar_datos_articulo() {
    var img = document.querySelector(".informacion-articulo img");
    var nombreArt = document.querySelector(".informacion-articulo .nombre-articulo");
    var precioArt = document.querySelector(".informacion-articulo .precio-articulo");
    var cantidad = document.querySelector(".informacion-articulo .cantidad");

    img.src = "https://localhost:44394/Imagenes/logo.png";
    nombreArt.innerText = "";
    precioArt.innerText = "";
    cantidad.innerText = 1;
}

function agregar_boton_venta(dato) {
    var contenedor_sugerencias = document.querySelector(".sugerencias");
    contenedor_sugerencias.style.display = "flex"


    var codigoHTML = `
      <button class="sugerencia" type="button" onclick="buscar_informacion_articulo_venta(this)">
        <span>
          ${dato}
        </span>
      </button>
  `;

    var tempDiv = document.createElement('div');
    tempDiv.innerHTML = codigoHTML;
    var elemento = tempDiv.childNodes[1];

    contenedor_sugerencias.appendChild(elemento);
}

function eliminar_sugerencias_venta() {
    var contenedor_sugerencias = document.querySelector(".sugerencias");
    contenedor_sugerencias.style.display = "none"

    var sugerencias = document.querySelectorAll(".sugerencia");

    for (var i = 0; i < sugerencias.length; i++) {
        sugerencias[i].remove();
    }
}


function buscar_informacion_articulo_venta(boton) {
    var nombre = boton.childNodes[1].innerText;

    var dato = {
        nombre: nombre,
        cantidad: 1
    };

    $.ajax({
        url: '/HomeStaff/mostrar_informacion_venta',
        type: 'POST',
        dataType: 'json',
        data: dato,
        success: function (result) {
            eliminar_sugerencias_venta();

            var img = document.querySelector(".informacion-articulo img");
            var nombreArt = document.querySelector(".informacion-articulo .nombre-articulo");
            var precioArt = document.querySelector(".informacion-articulo .precio-articulo");

            img.src = result.fotoArt;
            nombreArt.textContent = result.nombreArt;
            precioArt.textContent = "$" + result.precioArt;
        },
        error: function (xhr, status, error) {
            console.log("error")
        }
    });
}

function aumentar_venta(boton) {
    var nombre = (((boton.parentNode).parentNode).parentNode).childNodes[1];
    var cantidad = ((boton.parentNode).parentNode).childNodes[1];

    if (nombre.textContent.trim() != "") {
        var numero = parseInt(cantidad.textContent);

        numero++;

        cantidad.innerText = numero;

        var dato = {
            nombre: nombre.innerText,
            cantidad: numero
        };

        $.ajax({
            url: '/HomeStaff/mostrar_informacion_venta',
            type: 'POST',
            dataType: 'json',
            data: dato,
            success: function (result) {
                mostrar_informacion_venta(result, boton);
                calcular_precio_venta();
            },
            error: function (xhr, status, error) {
                console.log("error")
            }
        });
    }
}

function disminuir_venta(boton) {
    var nombre = (((boton.parentNode).parentNode).parentNode).childNodes[1];
    var cantidad = ((boton.parentNode).parentNode).childNodes[1];

    var numero = parseInt(cantidad.textContent);

    if (nombre.textContent.trim() != "" && numero > 1) {

        numero--;

        cantidad.innerText = numero;

        var dato = {
            nombre: nombre.innerText,
            cantidad: numero
        };

        $.ajax({
            url: '/HomeStaff/mostrar_informacion_venta',
            type: 'POST',
            dataType: 'json',
            data: dato,
            success: function (result) {
                mostrar_informacion_venta(result, boton);
                calcular_precio_venta();
            },
            error: function (xhr, status, error) {
                console.log("error")
            }
        });
    }
}

function mostrar_informacion_venta(datos, boton) {
    var contenedor_sugerencias = document.querySelector(".sugerencias");
    contenedor_sugerencias.style.display = "none"

    var contenedor_informacion = (((boton.parentNode).parentNode).parentNode).parentNode;

    var contenedor_datos = contenedor_informacion.childNodes[3];

    var img = contenedor_informacion.childNodes[1];
    var nombreArt = contenedor_datos.childNodes[1];
    var precioArt = contenedor_datos.childNodes[3];

    img.src = datos.fotoArt;
    nombreArt.innerText = datos.nombreArt;
    precioArt.innerText = "$" + datos.precioArt;
}

document.addEventListener('click', function (event) {
    var contenedor = document.querySelector('.contenedor-ventas .buscador');

    var targetElement = event.target;

    if (contenedor != null && !contenedor.contains(targetElement)) {
        eliminar_sugerencias();

        var input = document.querySelector(".buscador input")
        input.value = "";
    }
});

function agregar_componente_venta(boton) {
    var nombre_articulo = document.querySelector(".informacion-articulo .nombre-articulo");
    var cantidad = document.querySelector(".informacion-articulo .spinner-cantidad .cantidad");

    if (nombre_articulo.textContent.trim() != "") {
        var numero = parseInt(cantidad.textContent);

        var dato = {
            nombre: nombre_articulo.innerText,
            cantidad: numero
        };

        var xhr = new XMLHttpRequest();
        xhr.open('POST', '/HomeStaff/agregar_componente_venta', true);
        xhr.setRequestHeader('Content-Type', 'application/json');
        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4) {
                if (xhr.status === 200) {
                    var responseText = xhr.responseText.trim();
                    var result = JSON.parse(responseText);
                    if (result.nombreArt !== undefined) {
                        agregar_tarjeta_venta(result);
                        calcular_precio_venta();
                        limpiar_datos_articulo();
                    } else {
                        console.log('Respuesta vacía');
                    }
                } else {
                    console.log('error');
                }
            }
        };
        xhr.send(JSON.stringify(dato));
    }
}

function eliminar_componente_venta(boton) {
    var tarjeta = boton.parentNode;

    var contenedor_informacion = tarjeta.childNodes[3]

    var nombreArt = contenedor_informacion.childNodes[1];

    if (nombreArt.textContent.trim() != "") {

        var dato = {
            nombre: nombreArt.innerText
        };

        var xhr = new XMLHttpRequest();
        xhr.open('POST', '/HomeStaff/eliminar_componente_venta', true);
        xhr.setRequestHeader('Content-Type', 'application/json');
        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4) {
                if (xhr.status === 200) {
                    var responseText = xhr.responseText.trim();
                    var result = JSON.parse(responseText);
                    var estatus = result.estatus;
                    tarjeta.remove();
                    calcular_precio_venta();
                } else {
                    console.log('error');
                }
            }
        };
        xhr.send(JSON.stringify(dato));
    }
}

function agregar_tarjeta_venta(datos) {
    var contenedorComponentes = document.querySelector(".contenedor-componentes");

    var codigoFragmento = document.createElement("div")
    codigoFragmento.className = "tarjeta-componente"

    var codigoHTML = `
      <img src="${datos.fotoArt}" alt="Alternate Text" />
      <div class="contenedor-datos">
        <i class="nombre-articulo">${datos.nombreArt}</i>
        <i class="precio-articulo">$${datos.precioArt}</i>
        <div class="spinner-cantidad">
          <i class="cantidad">${datos.cantidad}</i>
          <div class="contenedor-botones">
            <button class="aumentar" type="button" onclick="aumentar_venta(this)">
              <span class="material-icons-outlined">
                expand_less
              </span>
            </button>
            <button class="disminuir" type="button" onclick="disminuir_venta(this)">
              <span class="material-icons-outlined">
                expand_more
              </span>
            </button>
          </div>
        </div>
      </div>
      <button class="eliminar-imagen" type="button" onclick="eliminar_componente_venta(this)">
        <span class="material-icons-outlined">
          clear
        </span>
      </button>
  `;

    codigoFragmento.innerHTML = codigoHTML;
    contenedorComponentes.appendChild(codigoFragmento);
}

function calcular_precio_venta() {
    var xhr = new XMLHttpRequest();
    xhr.open('POST', '/HomeStaff/calcular_precio_venta', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4) {
            if (xhr.status === 200) {
                var responseText = xhr.responseText.trim();
                var result = JSON.parse(responseText);
                if (result.estatus == 1) {
                    ajustar_precio(result);
                }
            } else {
                console.log('error');
            }
        }
    };
    xhr.send();
}

function ajustar_precio(result) {
    var subtotal = document.querySelector(".subtotal");
    var envio = document.querySelector(".envio");
    var iva = document.querySelector(".iva");
    var total = document.querySelector(".total");

    subtotal.innerText = "$" + result.subtotal;
    envio.innerText = "$" + result.envio;
    iva.innerText = "$" + result.impuestos;
    total.innerText = "$" + result.total;
}

function realizar_venta() {
    $.ajax({
        url: '/HomeStaff/realizar_venta',
        type: 'GET',
        success: function (result) {
            $('#contenedor-opcion').html(result);
        },
        error: function (xhr, status, error) {
            console.log("error")
        }
    });
}

function cancelar_venta() {
    $.ajax({
        url: '/HomeStaff/Ventas',
        type: 'GET',
        success: function (result) {
            $('#contenedor-opcion').html(result);
        },
        error: function (xhr, status, error) {
            console.log("error")
        }
    });
}