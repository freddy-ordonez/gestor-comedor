const urlBase = API_BASE_URL

$(document).ready(function () {
    const token = sessionStorage.getItem("token");
    if (token) {
        location.href = "/Home"
    }
});


$("#frmLogin").on("submit", function (e) {
    e.preventDefault();
    $("#btnLoginSubmit").prop("disabled", true);

    const formData = {
        email: $("#txtEmailLogin").val(),
        password: $("#txtPasswordLogin").val()
    }

    $.ajax({
        url: urlBase + "/auth/login",
        method: "POST",
        data: JSON.stringify(formData),
        contentType: "application/json",
        success: (res) => {
            const user = res.data;
            sessionStorage.setItem("token", user.token);
            sessionStorage.setItem("userId", user.userId);
            sessionStorage.setItem("userName", user.firstName + " " + user.lastName);
            Swal.fire({
                position: 'center-center',
                icon: 'success',
                title: "Inicio de Sesión",
                text: "Bienvenido de nuevo " + user.firstName + " " + user.lastName,
                showConfirmButton: false,
                timer: 3000,
                timerProgressBar: true
            });
            setTimeout(function () {
                location.href = "/Home"
                $("#btnLoginSubmit").prop("disabled", false);
            }, 3000)
        },
        error: (res, statusText) => {
            if (res.status == 400) {
                validationFormLogin(res.responseJSON)
            } else if (res.status == 404) {
                $("#errorLogin").text("Usuario o Contraseña incorrectos")
            } else {
                Swal.fire("Error en el sistema hable con el administrador...", "", "error");
            }

            $("#btnLoginSubmit").prop("disabled", false);
        }
    });
});

function validationFormLogin(errors = []) {

    const errorsClient = {};

    errors.forEach((e) => {
        errorsClient[e.campo] = e.mensaje
    });
    errorsClient.Email ? $("#errorEmailLogin").text(errorsClient.Email)
        : $("#errorEmailLogin").text("");
    errorsClient.Password ? $("#errorPasswordLogin").text(errorsClient.Password)
        : $("#errorPasswordLogin").text("");
}