$(document).ready(function () {
    let pageName = location.pathname.split('/').pop();
    if (pageName === "Beneficiary") {
        getAllBeneficiaries();
    } else if (pageName == "Update") {
        const activityId = location.search.split("=").pop()
        getBeneficiaryById(activityId);
    }
});

const listInputBeneficiaries = ["txtNameBeneficiary", "txtLastNameBeneficiary", "txtBirthDateBeneficiary", "txtStatusBeneficiary"]


function getAllBeneficiaries() {
    requestGet("/beneficiaries", handleResponseGetAllBeneficiaries);
}

function getBeneficiaryById(beneficiaryId) {
    requestGet("/beneficiaries/" + beneficiaryId, handleResponseGetBeneficiaryById);
}


//Http Get
function handleResponseGetAllBeneficiaries(res, textStatus, resHttp) {
    switch (textStatus) {
        case "success":
            let tableHtml = loadTableBeneficiaries(res.data)
            $("#tblBeneficiaries").html(tableHtml);
            paginar("#tblBeneficiaries");
            break;
        case "nocontent":
            $("#tblBeneficiaries").html(loadTableBeneficiaries());
            paginar("#tblBeneficiaries");
            break;
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

function handleResponseGetBeneficiaryById(res, textStatus, resHttp) {
    let beneficiary = res.data
    switch (textStatus) {
        case "success":
            $("#txtNameBeneficiary").val(beneficiary.firstName);
            $("#txtLastNameBeneficiary").val(beneficiary.lastName);
            $("#txtBirthDateBeneficiary").val(beneficiary.birthDate.split("T")[0]);
            $("#txtStatusBeneficiary").val(beneficiary.status)
            break;
        case "error":
            if (res.status === 401) {
                notAuthorize();
            } else if (res.status === 404) {
                notFound("No se encuentra ningun registro de el beneficiario seleccionado...",
                "/Beneficiary");
            }
            else
            {
                errorSystem();
            }
            break;
        default:
            errorSystem();
    }
}

//Http Delete
function handleDeleteBeneficiary(res, textStatus, resHttp) {
    switch (textStatus) {
        case "nocontent":
            Swal.fire("Se a eliminado con exito el beneficiario..", "", "success");
            getAllBeneficiaries();
            break;
        case "error":
            if (res.status == 401) {
                notAuthorize();
            } else if (res.status == 404) {
                Swal.fire("No se encontro el beneficiario en el sistema...", "", "error");
            } else {
                errorSystem()
            }
            break;
        default:
            errorSystem();
    }
}

//Http Post
function handleCreateBeneficiary(res, textStatus, resHttp) {
    switch (textStatus) {
        case "success":
            createToastSwal("success", "Se a creado el beneficiario...");
            cleanFormBeneficiary();
            break;
        case "error":
            if (res.status == 400) {
                console.log(res)
                validationFormBeneficiary(res.responseJSON);
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

//Http Put
function handleUpdateBeneficiary(res, textStatus, resHttp) {
    switch (textStatus) {
        case "nocontent":
            createToastSwal("success", "Se a actualizado el beneficiario...");
            setTimeout(function () {
                location.href = "/Beneficiary"
            }, 3000)
            break;
        case "error":
            if (res.status == 400) {
                validationFormBeneficiary(res.responseJSON);
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


//Form
$("#frmCreateBeneficiary").on("submit", function (e) {
    e.preventDefault();
    $("#btnSubmitCreateBeneficiary").prop("disabled", true);

    const birthDate = $("#txtBirthDateBeneficiary").val() === "" ? null : $("#txtBirthDateBeneficiary").val();

    const formData = {
        firstName: $("#txtNameBeneficiary").val(),
        lastName: $("#txtLastNameBeneficiary").val(),
        birthDate,
        status: $("#txtStatusBeneficiary").val()
    }

    requestPost("/beneficiaries", JSON.stringify(formData), handleCreateBeneficiary);
    setTimeout(function () {
        $("#btnSubmitCreateBeneficiary").prop("disabled", false);
    }, 3000);
});

$("#frmUpdateBeneficiary").on("submit", function (e) {
    e.preventDefault();
    $("#btnSubmitUpdateBeneficiary").prop("disabled", true);

    const birthDate = $("#txtBirthDateBeneficiary").val() === "" ? null : $("#txtBirthDateBeneficiary").val();

    const formData = {
        firstName: $("#txtNameBeneficiary").val(),
        lastName: $("#txtLastNameBeneficiary").val(),
        birthDate,
        status: $("#txtStatusBeneficiary").val()
    }

    let beneficiaryId = location.search.split("=").pop();
    requestPut("/beneficiaries/" + beneficiaryId, JSON.stringify(formData), handleUpdateBeneficiary);
    setTimeout(function () {
        $("#btnSubmitUpdateBeneficiary").prop("disabled", false);
    }, 3000);
});



function validationFormBeneficiary(errors = []) {

    const errorsClient = {};

    errors.forEach((e) => {
        errorsClient[e.campo] = e.mensaje
    });

    errorsClient.FirstName ? $("#errorNameBeneficiary").text(errorsClient.FirstName)
        : $("#errorNameBeneficiary").text("");
    errorsClient.LastName ? $("#errorLastNameBeneficiary").text(errorsClient.LastName)
        : $("#errorLastNameBeneficiary").text("");
    errorsClient.BirthDate ? $("#errorBirthDateBeneficiary").text(errorsClient.BirthDate)
        : $("#errorBirthDateBeneficiary").text("")
    errorsClient.Status ? $("#errorStatusBeneficiary").text(errorsClient.Status)
        : $("#errorStatusBeneficiary").text("");
}

function loadTableBeneficiaries(data = []) {
    var table ="<thead>" +
        "<tr>" +
        "<th>Nombre</th>" +
        "<th>Apellido</th>" +
        "<th style='text-align:left'>Fecha Cumpleaños</th>" +
        "<th>Estado</th>" +
        "<th style='text-align:center'>Actualizar</th>" +
        "<th style='text-align:center'>Eliminar</th>" +
        "</tr>" +
        "</thead>" +
        "<tbody>";

    if (data != null || data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            var beneficiary = data[i];

            table += "<tr>" +
                "<td>" + beneficiary.firstName + "</td>" +
                "<td>" + beneficiary.lastName + "</td>" +
                "<td style='text-align:left'>" + beneficiary.birthDate.split("T")[0] + "</td>" +
                "<td>" + beneficiary.status + "</td>" +
                "<td style='text-align:center'>" +
                "<i class='bi bi-pencil-square' onclick='javascript:updateBeneficiary(" + beneficiary.beneficiaryId + ")' style='cursor:pointer;'></i>" +
                "</td>" +
                "<td style='text-align:center'>" +
                "<i class='bi bi-trash' onclick='javascript:deleteBeneficiary(" + beneficiary.beneficiaryId + ",\"" + beneficiary.firstName + "\")' style='cursor:pointer;'></i>" +
                "</td>" +
                "</tr>";
        }
    }

    table += "</tbody>";
    return table;
}

function updateBeneficiary(beneficiaryId) {
    location.href = "/Beneficiary/Update?id=" + beneficiaryId
}

function deleteBeneficiary(beneficiaryId, name) {
    Swal.fire({
        title: "Quieres eliminar el benefiario: '" + name + "' con id: " + beneficiaryId,
        showCancelButton: true,
        confirmButtonText: "Si",
        cancelButtonText: "No",
    }).then((result) => {
        if (result.isConfirmed) {
            requestDelete("/beneficiaries/" + beneficiaryId, handleDeleteBeneficiary);
        }
    });
}

function cleanFormBeneficiary() {
    validationFormBeneficiary()
    listInputBeneficiaries.forEach(a => {
        $("#" + a).val("");
    });
}

