function ir_categoria(categoria) {
    const xhr = new XMLHttpRequest();
    const datos = new FormData();
    datos.append('categoria', categoria);

    xhr.open('POST', '/HomePrincipal/Index');
    xhr.onload = function () {
        if (xhr.status === 200) {
            window.location.href = "/HomePrincipal/Categoria";
        } else {
            console.error('Error en la solicitud');
        }
    };
    xhr.send(datos);
}