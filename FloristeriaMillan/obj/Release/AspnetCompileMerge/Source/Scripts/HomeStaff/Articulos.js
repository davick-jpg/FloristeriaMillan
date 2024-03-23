function mostrar_vista_parcial() {
    var vista_parcial = document.querySelector(".vista-parcial")
    vista_parcial.style.display = "flex"
}

function cerrarventana_articulo() {
    $.ajax({
        url: '/HomeStaff/Articulos',
        type: 'GET',
        success: function (result) {
            $('#contenedor-opcion').html(result);
        },
        error: function (xhr, status, error) {
            console.log("error")
        }
    });
}

function cargar_imagen() {
    var input = document.getElementById('imagen-articulo')
    var imagen = document.getElementById("contenedor-imagen")

    archivo = input.files[0];

    if (archivo != null) {
        const extension = archivo.name.split('.').pop().toLowerCase();

        const extensionesValidas = ['png'];

        if (extensionesValidas.includes(extension)) {
            const url = URL.createObjectURL(archivo);
            imagen.src = url
        }
    }
}

function eliminar_articulo(boton) {
    var tarjeta = (boton.parentNode).parentNode;
    var dato = {
        nombre: (tarjeta.childNodes[3]).textContent
    };

    $.ajax({
        url: '/HomeStaff/eliminar_articulo',
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

function enviar_datos_articulo(boton) {
    var formData = new FormData($('form')[0]);

    $.ajax({
        url: '/HomeStaff/Articulos_formulario',
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

function buscar_articulo(celda) {
    datos_articulos = celda.parentNode
    definir_datos_articulos(datos_articulos);
    var vista_parcial = document.querySelector(".vista-parcial");
    vista_parcial.style.display = "flex";
}

function definir_datos_articulos(datos_articulos) {
    datos = datos_articulos.children;

    var dato = {
        nombre: datos[1].innerText
    }

    $.ajax({
        url: '/HomeStaff/buscar_articulo',
        type: 'POST',
        dataType: 'json',
        data: dato,
        success: function (result) {
            inputs_articulo = document.querySelectorAll('.inputbox input')
            img_articulo = document.querySelector('#contenedor-imagen')
            combobox = document.querySelector('.clase')

            inputs_articulo[0].removeAttribute("required")

            inputs_articulo[1].value = result.nombreArt;
            inputs_articulo[2].value = result.precioArt;
            inputs_articulo[3].value = result.precioDis;
            inputs_articulo[4].value = result.precioCom;
            inputs_articulo[5].value = result.stockMin;
            inputs_articulo[6].value = result.stockArt;

            combobox.value = result.nombreDis
            img_articulo.src = result.fotoArt
        },
        error: function (xhr, status, error) {
            console.log("error");
        }
    });
}