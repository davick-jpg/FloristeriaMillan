function ir_a(direccion) {
    window.location.href = direccion;
}

function enviar() {
    var formData = $('form').serialize();

    $.ajax({
        url: '/HomeUsuario/Perfil',
        type: 'POST',
        data: formData,
        success: function (response) {
            window.location.reload(true);
        },
        error: function (xhr, status, error) {
        }
    });
}

function recargar() {
    window.location.reload(true);
}