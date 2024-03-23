function mostrar_vista_parcial() {
    var vista_parcial = document.querySelector(".vista-parcial")
    vista_parcial.style.display = "flex"
}

function cerrarventana_productos() {
    $.ajax({
        url: '/HomeStaff/Productos',
        type: 'GET',
        success: function (result) {
            $('#contenedor-opcion').html(result);
        },
        error: function (xhr, status, error) {
            console.log("error")
        }
    });
}

function limpiar_datos() {
    window.location.href = '/HomeStaff/Index';
}

function img() {
    var input = document.querySelector("#imagen-articulo")
    var imagen = document.querySelectorAll(".galeria img")

    if (input.files.length == 4) {
        for (var i = 0; i < input.files.length; i++) {
            archivo = input.files[i];

            if (archivo != null) {
                const extension = archivo.name.split('.').pop().toLowerCase();

                const extensionesValidas = ['png'];

                if (extensionesValidas.includes(extension)) {
                    const url = URL.createObjectURL(archivo);
                    imagen[i].src = url
                }
            }
        }
    }
}

function enviar_datos_productos(boton) {
    var formData = new FormData();
    var files = $('#imagen-articulo')[0].files;

    for (var i = 0; i < files.length; i++) {
        formData.append('fotoArt', files[i]);
    }

    formData.append('nombrePro', $('input[name="nombrePro"]').val());
    formData.append('nombreCla', $('select[name="nombreCla"]').val());
    formData.append('nombreCat', $('select[name="nombreCat"]').val());

    $.ajax({
        url: '/HomeStaff/Productos',
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
        success: function (result) {
            $('#contenedor-opcion').html(result);
        },
        error: function (xhr, status, error) {
        }
    });
}

function eliminar_producto(boton) {
    var tarjeta = (boton.parentNode).parentNode;
    var dato = {
        nombrePro: (tarjeta.childNodes[3]).textContent
    };

    $.ajax({
        url: '/HomeStaff/eliminar_producto',
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

function agregar_componente(boton) {
    var nombre_articulo = document.querySelector(".informacion-articulo .nombre-articulo");
    var cantidad = document.querySelector(".informacion-articulo .spinner-cantidad .cantidad");

    if (nombre_articulo.textContent.trim() != "") {
        var numero = parseInt(cantidad.textContent);

        var dato = {
            nombre: nombre_articulo.innerText,
            cantidad: numero
        };

        var xhr = new XMLHttpRequest();
        xhr.open('POST', '/HomeStaff/agregar_componente', true);
        xhr.setRequestHeader('Content-Type', 'application/json');
        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4) {
                if (xhr.status === 200) {
                    var responseText = xhr.responseText.trim();
                    var result = JSON.parse(responseText);
                    if (result.nombreArt !== undefined) {
                        agregar_tarjeta(result);
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

function eliminar_componente(boton) {
    var tarjeta = boton.parentNode;

    var contenedor_informacion = tarjeta.childNodes[3]

    var nombreArt = contenedor_informacion.childNodes[1];

    if (nombreArt.textContent.trim() != "") {

        var dato = {
            nombre: nombreArt.innerText
        };

        var xhr = new XMLHttpRequest();
        xhr.open('POST', '/HomeStaff/eliminar_componente', true);
        xhr.setRequestHeader('Content-Type', 'application/json');
        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4) {
                if (xhr.status === 200) {
                    var responseText = xhr.responseText.trim();
                    var result = JSON.parse(responseText);
                    var estatus = result.estatus;
                    tarjeta.remove();
                } else {
                    console.log('error');
                }
            }
        };
        xhr.send(JSON.stringify(dato));
    }
}

function agregar_tarjeta(datos) {
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
            <button class="aumentar" type="button" onclick="aumentar(this)">
              <span class="material-icons-outlined">
                expand_less
              </span>
            </button>
            <button class="disminuir" type="button" onclick="disminuir(this)">
              <span class="material-icons-outlined">
                expand_more
              </span>
            </button>
          </div>
        </div>
      </div>
      <button class="eliminar-imagen" type="button" onclick="eliminar_componente(this)">
        <span class="material-icons-outlined">
          clear
        </span>
      </button>
  `;

    codigoFragmento.innerHTML = codigoHTML;
    contenedorComponentes.appendChild(codigoFragmento);
}

function buscar_producto() {
    var contenedor_sugerencias = document.querySelector(".sugerencias");
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
        xhr.open('POST', '/HomeStaff/buscar_articulos', true);
        xhr.setRequestHeader('Content-Type', 'application/json');
        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4) {
                if (xhr.status === 200) {
                    var result = JSON.parse(xhr.responseText);
                    eliminar_sugerencias();
                    for (var i = 0; i < result.resultados.length; i++) {
                        var elemento = result.resultados[i];
                        agregar_boton(elemento);
                    }
                } else {
                    console.log('error');
                }
            }
        };
        xhr.send(JSON.stringify(dato));
    }
}

function agregar_boton(dato) {
    var contenedor_sugerencias = document.querySelector(".sugerencias");
    contenedor_sugerencias.style.display = "flex"


    var codigoHTML = `
      <button class="sugerencia" type="button" onclick="buscar_informacion_articulo(this)">
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

function buscar_informacion_articulo(boton) {
    var nombre = boton.childNodes[1].innerText;
    var dato = {
        nombre: nombre,
        cantidad: 1
    };

    $.ajax({
        url: '/HomeStaff/mostrar_informacion',
        type: 'POST',
        dataType: 'json',
        data: dato,
        success: function (result) {
            eliminar_sugerencias();

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

function mostrar_informacion(datos, boton) {
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

document.addEventListener('click', function (event) {
    var contenedor = document.querySelector('.contenedor-productos .buscador');

    var targetElement = event.target;

    if (contenedor != null && !contenedor.contains(targetElement)) {
        eliminar_sugerencias();

        var input = document.querySelector(".buscador input")
        input.value = "";
    }
});

function eliminar_sugerencias() {
    var contenedor_sugerencias = document.querySelector(".sugerencias");
    contenedor_sugerencias.style.display = "none"

    var sugerencias = document.querySelectorAll(".sugerencia");

    for (var i = 0; i < sugerencias.length; i++) {
        sugerencias[i].remove();
    }
}

function aumentar(boton) {
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
            url: '/HomeStaff/mostrar_informacion',
            type: 'POST',
            dataType: 'json',
            data: dato,
            success: function (result) {
                mostrar_informacion(result, boton);
            },
            error: function (xhr, status, error) {
                console.log("error")
            }
        });
    }
}

function disminuir(boton) {
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
            url: '/HomeStaff/mostrar_informacion',
            type: 'POST',
            dataType: 'json',
            data: dato,
            success: function (result) {
                mostrar_informacion(result, boton);
            },
            error: function (xhr, status, error) {
                console.log("error")
            }
        });
    }
}

function buscar_clases(nombreCat) {
    eliminar_clases();

    var dato = {
        categoria: nombreCat
    };

    var xhr = new XMLHttpRequest();
    xhr.open('POST', '/HomeStaff/buscar_clases', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4) {
            if (xhr.status === 200) {
                var result = JSON.parse(xhr.responseText);
                for (var i = 0; i < result.clases.length; i++) {
                    agregar_opcion(result.clases[i]);
                }
            } else {
                console.log('error');
            }
        }
    };
    xhr.send(JSON.stringify(dato));
}

function agregar_opcion(clase) {
    var select = document.querySelector(".clases");

    var option = document.createElement("option");
    option.className = "clase";
    option.textContent = clase;

    select.appendChild(option);
}

function eliminar_clases() {
    var option = document.querySelectorAll(".clases option")

    for (var i = 1; i < option.length; i++) {
        option[i].remove();
    }
}