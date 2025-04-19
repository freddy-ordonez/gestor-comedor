$(document).ready(function () {
    let pageName = location.pathname.split('/').pop();
    if (pageName === "Donor") {
        getAllDonors();
    } else if (pageName == "Update") {
        const donorId = location.search.split("=").pop()
        getDonorById(donorId);
    }
});

const listInputDonors = ["txtNameDonor", "txtLastNameDonor", "txtDonorType", "txtPhoneDonor", "txtAddressDonor"]



//Http Get
function handleResponseGetAllDonors(res, textStatus, resHttp) {

    switch (textStatus) {
        case "success":
            let tableHtml = loadTableDonors(res.data)
            $("#tblDonors").html(tableHtml);
            paginar("#tblDonors");
            break;
        case "nocontent":
            $("#tblDonors").html(loadTableDonors());
            paginar("#tblDonors");
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

function handleResponseGetDonorById(res, textStatus, resHttp) {
    let donor = res.data
    switch (textStatus) {
        case "success":
            $("#txtNameDonor").val(donor.firstName);
            $("#txtLastNameDonor").val(donor.lastName);
            $("#txtDonorType").val(donor.donorType);
            $("#txtPhoneDonor").val(donor.phone);
            $("#txtAddressDonor").val(donor.address);
            break;
        case "error":
            if (res.status === 401) {
                notAuthorize();
            } else if (res.status === 404) {
                notFound("No se encuentra ningun registro de el donante seleccionado...",
                    "/Donor");
            }
            else {
                errorSystem();
            }
            break;
        default:
            errorSystem();
    }
}

function getAllDonors() {
    requestGet("/donors", handleResponseGetAllDonors);
}

function getDonorById(donorId) {
    requestGet("/donors/" + donorId, handleResponseGetDonorById);
}



//Http Delete
function handleDeleteDonor(res, textStatus, resHttp) {
    switch (textStatus) {
        case "nocontent":
            Swal.fire("Se a eliminado con exito el donante...", "", "success");
            getAllDonors();
            break;
        case "error":
            if (res.status == 400) {
                Swal.fire("El donante no se puede eliminar ya que esta haciendo utilizada por otro registro en el sistema...", "", "info");
            } else if (res.status == 401) {
                notAuthorize();
            } else if (res.status == 404) {
                Swal.fire("No se encontro el donante en el sistema...", "", "error");
            } else {
                errorSystem()
            }
            break;
        default:
            errorSystem();
    }
}
function deleteDonor(donorId, name) {
    Swal.fire({
        title: "Quieres eliminar el donante: '" + name + "' con id: " + donorId,
        showCancelButton: true,
        confirmButtonText: "Si",
        cancelButtonText: "No",
    }).then((result) => {
        if (result.isConfirmed) {
            requestDelete("/donors/" + donorId, handleDeleteDonor);
        }
    });
}

//Http Post
function handleCreateDonor(res, textStatus, resHttp) {
    switch (textStatus) {
        case "success":
            createToastSwal("success", "Se a creado el donante...");
            cleanFormDonor();
            break;
        case "error":
            if (res.status == 400) {
                validationFormDonor(res.responseJSON);
            } else if (res.status == 401) {
                notAuthorize();
            } else {
                errorSystem();
            }
            break;
    }
}


//Http Put
function handleUpdateDonor(res, textStatus, resHttp) {
    switch (textStatus) {
        case "nocontent":
            createToastSwal("success", "Se a actualizado el donante...");
            setTimeout(function () {
                location.href = "/Donor"
            }, 3000)
            break;
        case "error":
            if (res.status == 400) {
                validationFormDonor(res.responseJSON);
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
function updateDonor(donorId) {
    location.href = "/Donor/Update?id=" + donorId
}

//Form
$("#frmCreateDonor").on("submit", function (e) {
    e.preventDefault();
    $("#btnSubmitCreateDonor").prop("disabled", true);


    const formData = {
        firstName: $("#txtNameDonor").val(),
        lastName: $("#txtLastNameDonor").val(),
        donorType: $("#txtDonorType").val(),
        phone: $("#txtPhoneDonor").val(),
        address: $("#txtAddressDonor").val()
    }

    requestPost("/donors", JSON.stringify(formData), handleCreateDonor);
    setTimeout(function () {
        $("#btnSubmitCreateDonor").prop("disabled", false);
    }, 3000);
});

$("#frmUpdateDonor").on("submit", function (e) {
    e.preventDefault();
    $("#btnSubmitUpdateDonor").prop("disabled", true);

    const formData = {
        firstName: $("#txtNameDonor").val(),
        lastName: $("#txtLastNameDonor").val(),
        donorType: $("#txtDonorType").val(),
        phone: $("#txtPhoneDonor").val(),
        address: $("#txtAddressDonor").val()
    }

    let donorId = location.search.split("=").pop();
    requestPut("/donors/" + donorId, JSON.stringify(formData), handleUpdateDonor);
    setTimeout(function () {
        $("#btnSubmitUpdateDonor").prop("disabled", false);
    }, 3000);
});


//validations
function validationFormDonor(errors = []) {

    const errorsClient = {};

    errors.forEach((e) => {
        errorsClient[e.campo] = e.mensaje
    });
    errorsClient.FirstName ? $("#errorNameDonor").text(errorsClient.FirstName)
        : $("#errorNameDonor").text("");
    errorsClient.LastName ? $("#errorLastNameDonor").text(errorsClient.LastName)
        : $("#errorLastNameDonor").text("");
    errorsClient.DonorType ? $("#errorDonorType").text(errorsClient.DonorType)
        : $("#errorDonorType").text("");
    errorsClient.Phone ? $("#errorPhoneDonor").text(errorsClient.Phone)
        : $("#errorPhoneDonor").text("");
    errorsClient.Address ? $("#errorAddressDonor").text(errorsClient.Address)
        : $("#errorAddressDonor").text("");
}


//tables
function loadTableDonors(data = []) {
    var table = "<thead>" +
        "<tr>" +
        "<th>Nombre</th>" +
        "<th style='text-align:left'>Tipo de donante</th>" +
        "<th style='text-align:left'>Telefono</th>" +
        "<th style='text-align:left'>Direccion</th>" +
        "<th style='text-align:center'>Actualizar</th>" +
        "<th style='text-align:center'>Donaciones</th>" +
        "<th style='text-align:center'>Eliminar</th>" +
        "</tr>" +
        "</thead>" +
        "<tbody>";

    if (data != null || data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            var donor = data[i];

            table += "<tr>" +
                "<td>" + donor.firstName + " " + donor.lastName + "</td > " +
                "<td>" + donor.donorType + "</td>" +
                "<td style='text-align:left'>" + donor.phone + "</td>" +
                "<td style='text-align:left'>" + donor.address + "</td>" +
                "<td style='text-align:center'>" +
                "<i class='bi bi-pencil-square' onclick='javascript:updateDonor(" + donor.donorId + ")' style='cursor:pointer;'></i>" +
                "</td>" +
                "<td style='text-align:center'>" +
                "<i class='bi bi-card-list' onclick='javascript:loadDonations(" + donor.donorId + ",\"" + donor.firstName + " " + donor.lastName + "\")' style='cursor:pointer;'></i>" +
                "</td>" +
                "<td style='text-align:center'>" +
                "<i class='bi bi-trash' onclick='javascript:deleteDonor(" + donor.donorId + ",\"" + donor.firstName + "\")' style='cursor:pointer;'></i>" +
                "</td>" +
                "</tr>";
        }
    }

    table += "</tbody>";
    return table;
}

function loadDonations(donorId, name) {
    location.href = "/Donor/Donation?id=" + donorId + "&name=" + name
}


//others
function cleanFormDonor() {
    validationFormDonor()
    listInputDonors.forEach(d => {
        $("#" + d).val("");
    });
}