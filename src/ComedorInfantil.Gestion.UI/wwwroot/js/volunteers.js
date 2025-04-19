//Const
const volunteerId = location.search.split("=").pop()
const listInputsVolunteer = ["txtFirstNameVolunteer", "txtLastNameVolunteer",
    "txtIdentificationVolunteer", "txtPhoneVolunteer", "txtAvailabilityVolunteer"
    , "txtStatusVolunteer", "txtTypeIdentificationVolunteer"];

$(document).ready(function () {
    let pageName = location.pathname.split('/').pop();
    if (pageName === "Volunteer") {
        getAllVolunteers();
    } else if (pageName == "Update") {
        getTypeIdentification();
        getVolunteerById(volunteerId);
    } else if (pageName == "Create") {
        getTypeIdentification();
    } else if (pageName == "Assignment") {
        getAllActivitiesAndAssigmentsForVolunteers();
    }
});


//Http Get
function handleResponseGetAllVolunteers(res, textStatus, resHttp) {

    switch (textStatus) {
        case "success":
            let tableHtml = loadTableVolunteers(res.data)
            $("#tblVolunteers").html(tableHtml);
            paginar("#tblVolunteers");
            break;
        case "nocontent":
            $("#tblVolunteers").html(loadTableVolunteers());
            paginar("#tblVolunteers");
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

function handleResponseGetVolunteerById(res, textStatus, resHttp) {
    let volunteer = res.data
    switch (textStatus) {
        case "success":
            $("#txtFirstNameVolunteer").val(volunteer.firstName);
            $("#txtLastNameVolunteer").val(volunteer.lastName);
            $("#txtIdentificationVolunteer").val(volunteer.identification);
            $("#txtPhoneVolunteer").val(volunteer.phone);
            $("#txtAvailabilityVolunteer").val(volunteer.availability);
            $("#txtStatusVolunteer").val(volunteer.status);
            $("#txtTypeIdentificationVolunteer").val(volunteer.typeIdentification);
            break;
        case "error":
            if (res.status === 401) {
                notAuthorize();
            } else if (res.status === 404) {
                notFound("No se encuentra ningun registro de el voluntario seleccionado...",
                    "/Volunteer");
            }
            else {
                errorSystem();
            }
            break;
        default:
            errorSystem();
    }
}

function handleResponseGetTypeIdentifications(res, textStatus, resHttp) {
    switch (textStatus) {
        case "success":
            fillSelectTypeIdentification(res.data)
            break;
        case "nocontent":
            notFound("No se encontraron tipos de identificacion en el sistema. No se puede crear voluntarios..."
                , "/Volunteer");
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

function getAllVolunteers() {
    requestGet("/volunteers", handleResponseGetAllVolunteers);
}

function getVolunteerById(volunteerId) {
    requestGet("/volunteers/" + volunteerId, handleResponseGetVolunteerById);
}

function getTypeIdentification() {
    requestGet("/type-identifications", handleResponseGetTypeIdentifications)
}

function getAllActivitiesAndAssigmentsForVolunteers() {
    const volunteerIdQuery = location.search.split("=")[1].split("&")[0];
    const nombreVolunterArray = location.search.split("=")[2].split("%20");
    let nombreVolunter = ""
    nombreVolunterArray.forEach(x => { nombreVolunter += " " + x });
    $("#nameVolunteer").text(decodeURIComponent(nombreVolunter.trim()))

    $.ajax({
        url: urlBase + "/activities",
        method: "GET",
        success: (resActivities) => {
            $.ajax({
                url: urlBase + "/assignment-activities/volunteers/" + volunteerIdQuery,
                method: "GET",
                success: (res) => {
                    const activities = resActivities.data ?? [];
                    const assigmetsVolunteer = res == undefined ? [] : res.data;
                    let fillActivities = activities;

                    assigmetsVolunteer.forEach(a => {
                        fillActivities = fillActivities.filter(f => f.name !== a.nameActivity)
                    })

                    $("#tblAssignmentsVolunteer").html(loadTableAssignmentsVolunteers(assigmetsVolunteer));
                    paginar("#tblAssignmentsVolunteer");

                    $("#tblActivitiesVolunteer").html(loadTableActivitiesAvailable(fillActivities));
                    paginar("#tblActivitiesVolunteer");
                                  
                }
            });
        }
    });
}


//Http Delete
function handleDeleteVolunteer(res, textStatus, resHttp) {
    switch (textStatus) {
        case "nocontent":
            Swal.fire("Se a eliminado con exito el voluntario...", "", "success");
            getAllVolunteers();
            break;
        case "error":
            if (res.status == 400) {
                Swal.fire("El voluntario no se puede eliminar ya que esta haciendo utilizada por otro registro en el sistema...", "", "info");
            } else if (res.status == 401) {
                notAuthorize();
            } else if (res.status == 404) {
                Swal.fire("No se encontro el voluntario en el sistema...", "", "error");
            } else {
                errorSystem()
            }
            break;
        default:
            errorSystem();
    }
}

function handleDeleteAssigmentVolunteer(res, textStatus, resHttp) {
    switch (textStatus) {
        case "nocontent":
            Swal.fire("Se a eliminado con exito la asignacion...", "", "success");
            getAllActivitiesAndAssigmentsForVolunteers();
            break;
        case "error":
            if (res.status == 401) {
                notAuthorize();
            } else if (res.status == 404) {
                Swal.fire("No se encontro la asignacion en el sistema...", "", "error");
            } else {
                errorSystem()
            }
            break;
        default:
            errorSystem();
    }
}

function deleteVolunteer(volunteerId, name) {
    Swal.fire({
        title: "Quieres eliminar el voluntario: '" + name + "' con id: " + volunteerId,
        showCancelButton: true,
        confirmButtonText: "Si",
        cancelButtonText: "No",
    }).then((result) => {
        if (result.isConfirmed) {
            requestDelete("/volunteers/" + volunteerId, handleDeleteVolunteer);
        }
    });
}

function deleteAssignmentVolunteer(assignmentId, name) {
    Swal.fire({
        title: "Quieres eliminar la asignacion: '" + name + "' con id: " + assignmentId,
        showCancelButton: true,
        confirmButtonText: "Si",
        cancelButtonText: "No",
    }).then((result) => {
        if (result.isConfirmed) {
            requestDelete("/assignment-activities/" + assignmentId, handleDeleteAssigmentVolunteer);
        }
    });
}


//Http Post
function handleCreateVolunteer(res, textStatus, resHttp) {
    switch (textStatus) {
        case "success":
            createToastSwal("success", "Se a creado el voluntario...");
            cleanFormVolunteer();
            break;
        case "error":
            if (res.status == 400) {
                validationFormVolunteers(res.responseJSON);
            } else if (res.status == 401) {
                notAuthorize();
            } else {
                errorSystem();
            }
            break;
    }
}

function addActivityVolunteer(activityId) {
    const data = {
        volunteerId: location.search.split("=")[1].split("&")[0],
        activityId,
        assignmentDate: new Date().toISOString()
    }

    $.ajax({
        url: urlBase + "/assignment-activities",
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify(data),
        success: (res, textStatus, resHttp) =>{
            if (textStatus == "success") {
                createToastSwal("success", "Se a asignado la actividad el voluntario...");
                getAllActivitiesAndAssigmentsForVolunteers();
            }
        },
        error: () => {
            createToastSwal("error", "Algo a salido mal contacta con el administrador del sistema...");
        }
    })
}


//Http Put
function handleUpdateVolunteer(res, textStatus, resHttp) {
    switch (textStatus) {
        case "nocontent":
            createToastSwal("success", "Se a actualizado el voluntario...");
            setTimeout(function () {
                location.href = "/Volunteer"
            }, 3000)
            break;
        case "error":
            if (res.status == 400) {
                validationFormVolunteers(res.responseJSON);
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

function updateVolunteer(volunteerId) {
    location.href = "/Volunteer/Update?id=" + volunteerId
}


//Forms
$("#frmCreateVolunteer").on("submit", function (e) {
    e.preventDefault();
    $("#btnSubmitCreateVolunteer").prop("disabled", true);

    const formData = {
        firstName: $("#txtFirstNameVolunteer").val(),
        lastName: $("#txtLastNameVolunteer").val(),
        identification: $("#txtIdentificationVolunteer").val(),
        phone: $("#txtPhoneVolunteer").val(),
        availability: $("#txtAvailabilityVolunteer").val(),
        status: "Activo",
        typeIdentification: Number.parseInt($("#txtTypeIdentificationVolunteer").val())
    }

    requestPost("/volunteers", JSON.stringify(formData), handleCreateVolunteer);
    setTimeout(function () {
        $("#btnSubmitCreateVolunteer").prop("disabled", false);
    }, 3000);
});

$("#frmUpdateVolunteer").on("submit", function (e) {
    e.preventDefault();
    $("#btnSubmitUpdateVolunteer").prop("disabled", true);

    const formData = {
        firstName: $("#txtFirstNameVolunteer").val(),
        lastName: $("#txtLastNameVolunteer").val(),
        identification: $("#txtIdentificationVolunteer").val(),
        phone: $("#txtPhoneVolunteer").val(),
        availability: $("#txtAvailabilityVolunteer").val(),
        status: $("#txtStatusVolunteer").val(),
        typeIdentification: Number.parseInt($("#txtTypeIdentificationVolunteer").val())
    }

    requestPut("/volunteers/" + volunteerId, JSON.stringify(formData), handleUpdateVolunteer);
    setTimeout(function () {
        $("#btnSubmitUpdateVolunteer").prop("disabled", false);
    }, 3000);
});


//Tables
function loadTableVolunteers(data = []) {

    var table = "<thead>" +
        "<tr>" +
        "<th style='text-align:left'>Nombre</th>" +
        "<th style='text-align:left'>Identificacion</th>" +
        "<th style='text-align:left'>Telefono</th>" +
        "<th style='text-align:left'>Disponibilidad</th>" +
        "<th style='text-align:left'>Asignaciones</th>" +
        "<th style='text-align:left'>Actualizar</th>" +
        "<th style='text-align:center'>Eliminar</th>" +
        "</tr>" +
        "</thead>" +
        "<tbody>";

    if (data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            var volunteer = data[i];

            table += "<tr>" +
                "<td style='text-align:left'>" + volunteer.firstName + " " + volunteer.lastName + "</td>" +
                "<td style='text-align:left'>" + volunteer.identification + "</td>" +
                "<td style='text-align:left'>" + volunteer.phone + "</td>" +
                "<td style='text-align:left'>" + volunteer.availability + "</td>" +
                "<td style='text-align:center'>" +
                "<i class='bi bi-card-list' onclick='javascript:loadAssignments(" + volunteer.volunteerId + ",\"" + volunteer.firstName + " " + volunteer.lastName + "\")' style='cursor:pointer;'></i>" +
                "</td>" +
                "<td style='text-align:center'>" +
                "<i class='bi bi-pencil-square' onclick='javascript:updateVolunteer(" + volunteer.volunteerId + ")' style='cursor:pointer;'></i>" +
                "</td>" +
                "<td style='text-align:center'>" +
                "<i class='bi bi-trash' onclick='javascript:deleteVolunteer(" + volunteer.volunteerId + ",\"" + volunteer.firstName + "\")' style='cursor:pointer;'></i>" +
                "</td>" +
                "</tr>";
        }
    }

    table += "</tbody>";
    return table;
}

function loadTableAssignmentsVolunteers(data = []) {

    var table = "<thead>" +
        "<tr>" +
        "<th style='text-align:left'>Nombre Actividad</th>" +
        "<th style='text-align:left'>Descripcion</th>" +
        "<th style='text-align:left'>Dia de Inicio</th>" +
        "<th style='text-align:center'>Eliminar</th>" +
        "</tr>" +
        "</thead>" +
        "<tbody>";

    if (data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            var assignmentVolunteer = data[i];

            table += "<tr>" +
                "<td style='text-align:left'>" + assignmentVolunteer.nameActivity + "</td>" +
                "<td style='text-align:left'>" + assignmentVolunteer.descriptionActivity + "</td>" +
                "<td style='text-align:left'>" + assignmentVolunteer.startDate.split("T")[0] + "</td>" +
                "<td style='text-align:center'>" +
                "<i class='bi bi-trash' onclick='javascript:deleteAssignmentVolunteer(" + assignmentVolunteer.assignmentId + ",\"" + assignmentVolunteer.nameActivity + "\")' style='cursor:pointer;'></i>" +
                "</td>" +
                "</tr>";
        }
    }

    table += "</tbody>";
    return table;
}

function loadTableActivitiesAvailable(data = []) {

    var table = "<thead>" +
        "<tr>" +
        "<th style='text-align:left'>Nombre Actividad</th>" +
        "<th style='text-align:left'>Descripcion</th>" +
        "<th style='text-align:left'>Dia de Inicio</th>" +
        "<th style='text-align:center'>Agregar</th>" +
        "</tr>" +
        "</thead>" +
        "<tbody>";

    if (data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            var activity = data[i];

            table += "<tr>" +
                "<td style='text-align:left'>" + activity.name + "</td>" +
                "<td style='text-align:left'>" + activity.description + "</td>" +
                "<td style='text-align:left'>" + activity.startDate.split("T")[0] + "</td>" +
                "<td style='text-align:center'>" +
                "<i class='bi bi-plus-square' onclick='javascript:addActivityVolunteer(" + activity.activityId + ")' style='cursor:pointer;'></i>" +
                "</td>" +
                "</tr>";
        }
    }

    table += "</tbody>";
    return table;
}


//Others
function validationFormVolunteers(errors = []) {

    const errorsClient = {};

    errors.forEach((e) => {
        errorsClient[e.campo] = e.mensaje
    });
    errorsClient.FirstName ? $("#errorFirstNameVolunteer").text(errorsClient.FirstName)
        : $("#errorFirstNameVolunteer").text("");
    errorsClient.LastName ? $("#errorLastNameVolunteer").text(errorsClient.LastName)
        : $("#errorLastNameVolunteer").text("");
    errorsClient.Identification ? $("#errorIdentificationVolunteer").text(errorsClient.Identification)
        : $("#errorIdentificationVolunteer").text("")
    errorsClient.Phone ? $("#errorPhoneVolunteer").text(errorsClient.Phone)
        : $("#errorPhoneVolunteer").text("");
    errorsClient.Availability ? $("#errorAvailabilityVolunteer").text(errorsClient.Availability)
        : $("#errorAvailabilityVolunteer").text("");
    errorsClient.Status ? $("#errorStatusVolunteer").text(errorsClient.Status)
        : $("#errorStatusVolunteer").text("");
    errorsClient.TypeIdentification ? $("#errorTypeIdentificationVolunteer").text(errorsClient.TypeIdentification)
        : $("#errorTypeIdentificationVolunteer").text("");
}

function cleanFormVolunteer() {
    validationFormVolunteers()
    listInputsVolunteer.forEach(a => {
        $("#" + a).val("");
    });
}

function fillSelectTypeIdentification(data = []) {
    let selectTypeIdentification = "<option value='' selected>Selecciona una opcion</option>"

    data.forEach(x => {
        selectTypeIdentification += "<option value='" + x.typeIdentificationId + "'>" + x.typeIdentification + "</option>"
    });

    $("#txtTypeIdentificationVolunteer").html(selectTypeIdentification);
}

function loadAssignments(volunteerId, name) {
    location.href = "/Volunteer/Assignment?" + "id=" + volunteerId + "&name=" + name;
}