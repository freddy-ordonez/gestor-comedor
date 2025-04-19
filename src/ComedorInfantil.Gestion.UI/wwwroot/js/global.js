const urlBase = API_BASE_URL

$(document).ready(function () {
    $("#spUserName").text(sessionStorage.getItem("userName"));
    const token = sessionStorage.getItem('token')
    if (token == null) {
        notAuthorize();
    } else {
        $.ajaxSetup({
            headers: {
                'Authorization': 'Bearer ' + token
            }
        });
        loadNav();
        setTimeout(function () {
            isAuthorize();
        }, 2000)
    }
});

//Http Request
function requestGet(url, handle) {
    $.ajax({
        url: urlBase + url,
        method: "GET",
        success: (data, textStatus, res) => {
            handle(data, textStatus, res);
        },
        error: (res, textStatus, error) => {
            handle(res, textStatus, error);
        }
    });
};

function requestPost(url, data, handle) {
    $.ajax({
        url: urlBase + url,
        method: "POST",
        contentType: "application/json",
        data,
        success: (data, textStatus, res) => {
            handle(data, textStatus, res);
        },
        error: (res, textStatus, error) => {
            handle(res, textStatus, error);
        }
    });
};

function requestPut(url, data, handle) {
    $.ajax({
        url: urlBase + url,
        data,
        contentType: "application/json",
        method: "PUT",
        success: (data, textStatus, res) => {
            handle(data, textStatus, res);
        },
        error: (res, textStatus, error) => {
            handle(res, textStatus, error);
        }
    });
};

function requestDelete(url, handle) {
    $.ajax({
        url: urlBase + url,
        method: "DELETE",
        success: (data, textStatus, res) => {
            handle(data, textStatus, res);
        },
        error: (res, textStatus, error) => {
            handle(res, textStatus, error);
        }
    });
}


//Tables
function paginar(elemento) {
    var table;

    if ($.fn.DataTable.isDataTable(elemento)) {

        table = $(elemento).DataTable({
            paging: true,
            destroy: true,
            searching: false,
            "iDisplayLength": 5,
            "aLengthMenu": [[5, 10, 50, 100], [5, 10, 50, 100]],
            "oLanguage":
            {
                "sLengthMenu": " Mostrar _MENU_  registros por p&aacute;gina",
                "sProcessing": "Procesando...",
                "sZeroRecords": "No se encontraron resultados",
                "sEmptyTable": "Ningún dato disponible en esta tabla",
                "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                "sInfoPostFix": "",
                "sSearch": "Filtrar:",
                "sUrl": "",
                "sInfoThousands": ",",
                "sLoadingRecords": "Cargando...",
                "oPaginate": {
                    "sFirst": "Primero",
                    "sLast": "Último",
                    "sNext": "Siguiente",
                    "sPrevious": "Anterior"
                }
            },
        });
    }
    else {
        table = $(elemento).DataTable({

            "iDisplayLength": 5,
            "aLengthMenu": [[5, 10, 50, 100], [5, 10, 50, 100]],
            "oLanguage":
            {
                "sLengthMenu": " Mostrar _MENU_  registros por p&aacute;gina",
                "sProcessing": "Procesando...",
                "sZeroRecords": "No se encontraron resultados",
                "sEmptyTable": "Ningún dato disponible en esta tabla",
                "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                "sInfoPostFix": "",
                "sSearch": "Filtrar:",
                "sUrl": "",
                "sInfoThousands": ",",
                "sLoadingRecords": "Cargando...",
                "oPaginate": {
                    "sFirst": "Primero",
                    "sLast": "Último",
                    "sNext": "Siguiente",
                    "sPrevious": "Anterior"
                }
            },
            paging: true,
            destroy: true
        });

    }

};


//Errors
function notAuthorize() {
    Swal.fire({
        icon: "error",
        title: "Error en la conexión...",
        text: "No se ha podido validar la información del usuario. Por favor, inicie Sesión en el Sistema.",
        timer: 3500,
        showConfirmButton: false,
        timerProgressBar: true
    });
    setTimeout(function () {
        location.href = "/Login"
    }, 3500);
}

function errorSystem() {
    Swal.fire({
        icon: "error",
        title: "Error en el sistema",
        text: "Error en el sistema.Por favor, contacte al administrador del sistema.",
        timer: 3500,
        showConfirmButton: false,
        timerProgressBar: true
    });
    setTimeout(function () {
        location.href = "/Home"
    },3500)
}

function notFound(message, location) {
    Swal.fire({
        icon: "info",
        title: "Registro en el sistema",
        text: message,
        timer: 3500,
        showConfirmButton: false,
        timerProgressBar: true
    });
    setTimeout(function () {
        location.href = location
    }, 3500)
}


//Swal
function createToastSwal(icon, message) {
    const Toast = Swal.mixin({
        toast: true,
        position: "top-end",
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true,
        didOpen: (toast) => {
            toast.onmouseenter = Swal.stopTimer;
            toast.onmouseleave = Swal.resumeTimer;
        }
    });
    Toast.fire({
        icon: icon,
        title: message
    });
}

function openModal(name) {
    const modalEl = $(name)[0];
    const modal = bootstrap.Modal.getOrCreateInstance(modalEl);
    modal.show();
}

function closeModal(name) {
    let modal = bootstrap.Modal.getOrCreateInstance($(name)[0]);
    modal.hide();
    $('.modal-backdrop').remove();
    $('body').removeClass('modal-open');
    $('body').css('padding-right', '');
}

function logout() {
    sessionStorage.removeItem("token");
    sessionStorage.removeItem("userName");
    sessionStorage.removeItem("userId");

    Swal.fire({
        position: 'center-center',
        icon: 'success',
        title: "Cerrando Sesión",
        text: "Cerrando sesión espera un momento...",
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true
    });
    setTimeout(function () {
        location.href = "/Login"
    }, 3000)
}

function isAuthorize() {
    let pageName = location.pathname.split('/')[1];
    let isExistModule = $("#navFill a[href='/" + pageName + "']").length;
    if (isExistModule == 0) {
        Swal.fire({
            position: 'center-center',
            icon: 'error',
            title: "Sin Autorizacion",
            text: "No tienes autorizacion para este modulo...",
            showConfirmButton: false,
            timer: 3000,
            timerProgressBar: true
        });
        setTimeout(function () {
            location.href = "/Home"
        }, 3000)
    }
}

