$(document).ready(function () {
    let pageName = location.pathname.split('/').pop();
    if (pageName === "Activity") {
        getAllActivities();
    } else if (pageName == "Update") {
        const activityId = location.search.split("=").pop()        
        getActivityById(activityId);
    }
});

const listInputActivities = ["txtNameActivity", "txtDescriptionActivity", "txtStartDateActivity", "txtEndDateActivity"]


function getAllActivities() {
    requestGet("/activities", handleResponseGetAllActivies);
}

function getActivityById(activityId) {
    requestGet("/activities/" + activityId, handleResponseGetActivityById);
}


//Http Get
function handleResponseGetAllActivies(res, textStatus, resHttp) {

    switch (textStatus) {
        case "success":
            let tableHtml = loadTableActivities(res.data ?? null)
            $("#tblActivities").html(tableHtml);
            paginar("#tblActivities");
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

function handleResponseGetActivityById(res, textStatus, resHttp) {
    let activity = res.data
    switch (textStatus) {
        case "success":
            $("#txtNameActivity").val(activity.name);
            $("#txtDescriptionActivity").val(activity.description);
            $("#txtStartDateActivity").val(activity.startDate.split("T")[0]);
            $("#txtEndDateActivity").val(activity.endDate.split("T")[0]);
            break;
        case "error":
            if (res.status === 401) {
                notAuthorize();
            } else if (res.status === 404) {
                notFound("No se encuentra ningun registro de la actividad seleccionad",
                "/Activity");
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
function handleDeleteActivity(res, textStatus, resHttp) {
    switch (textStatus) {
        case "nocontent":
            Swal.fire("Se a eliminado con exito la actividad...", "", "success");
            getAllActivities();
            break;
        case "error":
            if (res.status == 400) {
                Swal.fire("La actividad no se puede eliminar ya que esta haciendo utilizada por otro registro en el sistema...", "", "info");
            } else if (res.status == 401) {
                notAuthorize();
            } else if (res.status == 404) {
                Swal.fire("No se encontro la actividad en el sistema...", "", "error");
            } else {
                errorSystem()
            }
            break;
        default:
            errorSystem();
    }
}

//Http Post
function handleCreateActivity(res, textStatus, resHttp) {
    switch (textStatus) {
        case "success":
            createToastSwal("success", "Se a creado la actividad...");
            cleanForm();
            break;
        case "error":
            if (res.status == 400) {
                validationForm(res.responseJSON);
            } else if (res.status == 401) {
                notAuthorize();
            } else {
                errorSystem();
            }
            break;
    }
}

//Http Put
function handleUpdateActivity(res, textStatus, resHttp) {
    console.log(res, textStatus, resHttp)
    switch (textStatus) {
        case "nocontent":
            createToastSwal("success", "Se a actualizado la actividad...");
            setTimeout(function () {
                location.href = "/Activity"
            }, 3000)
            break;
        case "error":
            if (res.status == 400) {
                validationForm(res.responseJSON);
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



$("#frmCreateActivity").on("submit", function (e) {
    e.preventDefault();
    $("#btnSubmitCreateActivity").prop("disabled", true);

    const startDate = $("#txtStartDateActivity").val() === "" ? null : $("#txtStartDateActivity").val();
    const endDate = $("#txtEndDateActivity").val() === "" ? null : $("#txtStartDateActivity").val();

    const formData = {
        name: $("#txtNameActivity").val(),
        description: $("#txtDescriptionActivity").val() ,
        startDate,
        endDate
    }

    requestPost("/activities", JSON.stringify(formData), handleCreateActivity);
    setTimeout(function () {
    $("#btnSubmitCreateActivity").prop("disabled", false);
    }, 3000);
});

$("#frmUpdateActivity").on("submit", function (e) {
    e.preventDefault();
    $("#btnSubmitUpdateActivity").prop("disabled", true);

    const startDate = $("#txtStartDateActivity").val() === "" ? null : $("#txtStartDateActivity").val();
    const endDate = $("#txtEndDateActivity").val() === "" ? null : $("#txtStartDateActivity").val();

    const formData = {
        name: $("#txtNameActivity").val(),
        description: $("#txtDescriptionActivity").val(),
        startDate,
        endDate
    }

    let activityId = location.search.split("=").pop();
    requestPut("/activities/" + activityId, JSON.stringify(formData), handleUpdateActivity);
    setTimeout(function () {
        $("#btnSubmitUpdateActivity").prop("disabled", false);
    }, 3000);
});

function validationForm(errors = []) {

    const errorsClient = {};

    errors.forEach((e) => {
        errorsClient[e.campo] = e.mensaje
    });
    errorsClient.Name ? $("#errorNameActivity").text(errorsClient.Name)
        : $("#errorNameActivity").text("");
    errorsClient.Description ? $("#errorDescriptionActivity").text(errorsClient.Description)
        : $("#errorDescriptionActivity").text("");
    errorsClient.StartDate ? $("#errorStartDateActivity").text(errorsClient.StartDate)
        : $("#errorStartDateActivity").text("")
    errorsClient.EndDate ? $("#errorEndDateActivity").text(errorsClient.EndDate)
        : $("#errorEndDateActivity").text("");
}

function loadTableActivities(data = []) {
    var table ="<thead>" +
        "<tr>" +
        "<th>Nombre</th>" +
        "<th>Descripción</th>" +
        "<th style='text-align:left'>Fecha Inicio</th>" +
        "<th style='text-align:left'>Fecha Finalización</th>" +
        "<th>Actualizar</th>" +
        "<th style='text-align:center'>Asignaciones</th>" +
        "<th style='text-align:center'>Eliminar</th>" +
        "</tr>" +
        "</thead>" +
        "<tbody>";

    if (data != null || data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            var activity = data[i];

            table += "<tr>" +
                "<td>" + activity.name + "</td>" +
                "<td>" + activity.description + "</td>" +
                "<td style='text-align:left'>" + activity.startDate.split("T")[0] + "</td>" +
                "<td style='text-align:left'>" + activity.endDate.split("T")[0] + "</td>" +
                "<td style='text-align:center'>" +
                "<i class='bi bi-pencil-square' onclick='javascript:updateActivity(" + activity.activityId + ")' style='cursor:pointer;'></i>" +
                "</td>" +
                "<td style='text-align:center'>" +
                "<i class='bi bi-card-list' onclick='javascript:loadAssignments(" + activity.activityId + ")' style='cursor:pointer;'></i>" +
                "</td>" +
                "<td style='text-align:center'>" +
                "<i class='bi bi-trash' onclick='javascript:deleteActivity(" + activity.activityId + ",\"" + activity.name + "\")' style='cursor:pointer;'></i>" +
                "</td>" +
                "</tr>";
        }
    }

    table += "</tbody>";
    return table;
}

function loadAssignments(activityId) {
    getAllAssigmentsByActivity(activityId);
    openModal("#modalAssignments");
/*    $("#btnModalAssignments").click();*/
}

function updateActivity(activityId) {
    location.href = "/Activity/Update?id=" + activityId
}

function deleteActivity(activityId, name) {
    Swal.fire({
        title: "Quieres eliminar la actividad: '" + name + "' con id: " + activityId,
        showCancelButton: true,
        confirmButtonText: "Si",
        cancelButtonText: "No",
    }).then((result) => {
        if (result.isConfirmed) {
            requestDelete("/activities/" + activityId, handleDeleteActivity);
        }
    });
}

function cleanForm() {
    validationForm()
    listInputActivities.forEach(a => {
        $("#" + a).val("");
    });
}


