const userId = location.search.split("=").pop()
const listInputUser = ["txtEmailUser", "txtPasswordUser", "txtFirstNameUser", "txtLastNameUser", "txtStatusUser"]

$(document).ready(function () {
    let pageName = location.pathname.split('/').pop();
    if (pageName === "User") {
        getAllUsers();
    } else if (pageName == "Update") {
        getUserById(userId);
    }
});

function getAllUsers() {
    requestGet("/users", handleResponseGetAllUsers);
}

function getUserById(userId) {
    requestGet("/users/" + userId, handleResponseGetUserById);
}


//Http Get
function handleResponseGetAllUsers(res, textStatus, resHttp) {

    switch (textStatus) {
        case "success":
            let tableHtml = loadTableUsers(res.data)
            $("#tblUsers").html(tableHtml);
            paginar("#tblUsers");
            break;
        case "nocontent":
            $("#tblUsers").html(loadTableUsers());
            paginar("#tblUsers");
        case "error":
            if (res.status === 401) {
                notAuthorize();
            } else {
                errorSystem();
            }
            break;
        default:
            errorSystem();
    }
}

function handleResponseGetUserById(res, textStatus, resHttp) {
    let user = res.data
    switch (textStatus) {
        case "success":
            $("#txtEmailUser").val(user.email);
            $("#txtFirstNameUser").val(user.firstName);
            $("#txtLastNameUser").val(user.lastName);
            $("#txtStatusUser").val(user.status);
            break;
        case "error":
            if (res.status === 401) {
                notAuthorize();
            } else if (res.status === 404) {
                notFound("No se encuentra ningun registro de el usuario seleccionado...",
                    "/User");
            }
            else {
                errorSystem();
            }
            break;
        default:
            errorSystem();
    }
}

//Http Delete
function handleDeleteUser(res, textStatus, resHttp) {
    switch (textStatus) {
        case "nocontent":
            Swal.fire("Se a eliminado con exito el usuario...", "", "success");
            getAllUsers();
            break;
        case "error":
            if (res.status == 401) {
                notAuthorize();
            } else if (res.status == 404) {
                Swal.fire("No se encontro el usuario en el sistema...", "", "error");
            } else {
                errorSystem()
            }
            break;
        default:
            errorSystem();
    }
}

//Http Post
function handleCreateUser(res, textStatus, resHttp) {
    switch (textStatus) {
        case "success":
            createToastSwal("success", "Se a creado el usuario...");
            cleanFormUser();
            break;
        case "error":
            if (res.status == 400) {
                if (res.responseJSON.message) {
                    $("#errorEmailUser").text("Este email ya esta registrado.")
                } else {
                    validationFormUser(res.responseJSON);
                }
            } else if (res.status == 401) {
                notAuthorize();
            } else {
                errorSystem();
            }
            break;
    }
}

//Http Put
function handleUpdateUser(res, textStatus, resHttp) {
    console.log(res, textStatus, resHttp)
    switch (textStatus) {
        case "nocontent":
            createToastSwal("success", "Se a actualizado el usuario...");
            setTimeout(function () {
                location.href = "/User"
            }, 3000)
            break;
        case "error":
            if (res.status == 400) {
                if (res.responseJSON.message) {
                    $("#errorEmailUser").text("Este email ya esta registrado.")
                } else {
                    validationFormUser(res.responseJSON);
                }
            } else if (res.status == 401) {
                notAuthorize();
            } else {
                errorSystem();
            }
            break;
        default:
            errorSystem();
    }
}

function handleUpdatePasswordUser(res, textStatus, resHttp) {
    switch (textStatus) {
        case "nocontent":
            createToastSwal("success", "Se a actualizado la contraseña...");
            $("#txtPasswordUser").val("");
            $("#errorPasswordUser").text("");
            $("#btnUpdatePasswordUser").click();
            break;
        case "error":
            if (res.status == 400) {
                createToastSwal("success", "Hubo un error al actualizar la contraseña hable con el administrador...");
            } else if (res.status == 401) {
                notAuthorize();
            } else {
                errorSystem();
            }
            break;
    }
}


//Form
$("#frmCreateUser").on("submit", function (e) {
    e.preventDefault();
    $("#btnSubmitCreateUser").prop("disabled", true);

    const formData = {
        email: $("#txtEmailUser").val(),
        password: $("#txtPasswordUser").val(),
        firstName: $("#txtFirstNameUser").val(),
        lastName: $("#txtLastNameUser").val(),
        status: "Activo",
    }

    requestPost("/users", JSON.stringify(formData), handleCreateUser);
    setTimeout(function () {
        $("#btnSubmitCreateUser").prop("disabled", false);
    }, 3000);
});

$("#frmUpdateUser").on("submit", function (e) {
    e.preventDefault();
    $("#btnSubmitUpdateUser").prop("disabled", true);

    const formData = {
        email: $("#txtEmailUser").val(),
        firstName: $("#txtFirstNameUser").val(),
        lastName: $("#txtLastNameUser").val(),
        status: $("#txtStatusUser").val(),
    }

    requestPut("/users/" + userId, JSON.stringify(formData), handleUpdateUser);
    setTimeout(function () {
        $("#btnSubmitUpdateUser").prop("disabled", false);
    }, 3000);
});

$("#frmUpdatePasswordUser").on("submit", function (e) {
    e.preventDefault();

    const password = $("#txtPasswordUser").val();

    if (password === "" || password.length < 8) {
        $("#errorPasswordUser").text("El minimo de caracteres es 8")
    } else {
        const user = {
            password
        }
        requestPost("/users/password/" + userId, JSON.stringify(user), handleUpdatePasswordUser);
    }
});

//Buttoms
$('#togglePassword').on('click', function () {
    const input = $('#txtPasswordUser');
    const icon = $('#iconPassword');

    const type = input.attr('type') === 'password' ? 'text' : 'password';
    input.attr('type', type);

    icon.toggleClass('bi-eye bi-eye-slash');
});

$("#btnUpdatePasswordUser").click(function () {
    $("#toggleUpdatePasswordUser").slideToggle("slow");
})

function validationFormUser(errors = []) {

    const errorsClient = {};

    errors.forEach((e) => {
        errorsClient[e.campo] = e.mensaje
    });
    errorsClient.Email ? $("#errorEmailUser").text(errorsClient.Email)
        : $("#errorEmailUser").text("");
    errorsClient.Password ? $("#errorPasswordUser").text(errorsClient.Password)
        : $("#errorPasswordUser").text("");
    errorsClient.FirstName ? $("#errorFirstNameUser").text(errorsClient.FirstName)
        : $("#errorFirstNameUser").text("")
    errorsClient.LastName ? $("#errorLastNameUser").text(errorsClient.LastName)
        : $("#errorLastNameUser").text("");
    errorsClient.Status ? $("#errorStatusUser").text(errorsClient.Status)
        : $("#errorStatusUser").text("");
}

function loadTableUsers(data = []) {
    var table = "<thead>" +
        "<tr>" +
        "<th>Nombre</th>" +
        "<th>Apellido</th>" +
        "<th style='text-align:left'>Correo</th>" +
        "<th style='text-align:left'>Estado</th>" +
        "<th style='text-align:center'>Modulos</th>" +
        "<th style='text-align:center'>Actualizar</th>" +
        "<th style='text-align:center'>Eliminar</th>" +
        "</tr>" +
        "</thead>" +
        "<tbody>";

    if (data != null || data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            var user = data[i];

            table += "<tr>" +
                "<td>" + user.firstName + "</td>" +
                "<td>" + user.lastName + "</td>" +
                "<td style='text-align:left'>" + user.email + "</td>" +
                "<td style='text-align:left'>" + user.status + "</td>" +
                "<td style='text-align:center'>" +
                "<i class='bi bi-journal-album' onclick='javascript:loadModules(" + user.userId + ",\"" + user.firstName + " " + user.lastName + "\")' style='cursor:pointer;'></i>" +
                "</td>" +
                "<td style='text-align:center'>" +
                "<i class='bi bi-pencil-square' onclick='javascript:updateUser(" + user.userId + ")' style='cursor:pointer;'></i>" +
                "</td>" +
                "<td style='text-align:center'>" +
                "<i class='bi bi-trash' onclick='javascript:deleteUser(" + user.userId + ",\"" + user.firstName + "\")' style='cursor:pointer;'></i>" +
                "</td>" +
                "</tr>";
        }
    }

    table += "</tbody>";
    return table;
}

function loadModules(userId, name) {
    location.href = "/User/Module?id=" + userId + "&" + "name=" + name
}

function updateUser(userId) {
    location.href = "/User/Update?id=" + userId
}

function deleteUser(userId, name) {
    Swal.fire({
        title: "Quieres eliminar el usuario: '" + name + "' con id: " + userId,
        showCancelButton: true,
        confirmButtonText: "Si",
        cancelButtonText: "No",
    }).then((result) => {
        if (result.isConfirmed) {
            requestDelete("/users/" + userId, handleDeleteUser);
        }
    });
}

function cleanFormUser() {
    validationFormUser()
    listInputUser.forEach(a => {
        $("#" + a).val("");
    });
}