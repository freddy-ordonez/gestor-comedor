function getAllAssigmentsByActivity(activityId) {
    requestGet("/assignment-activities/activities/" + activityId, handleGetAllAssigmentsByActivity)
}


//Http Get
function handleGetAllAssigmentsByActivity(res, textStatus, resHttp) {
    switch (textStatus) {
        case "success":
            let tableHtml = loadTableAssignmentActivities(res.data)
            $("#tblAssignments").html(tableHtml);
            paginar("#tblAssignments");
            break;
        case "nocontent":
            $("#tblAssignments").html(loadTableAssignmentActivities());
            paginar("#tblAssignments");
            break;
        case "error":
            if (resHttp.status === 401) {
                notAuthorize();
            } else {
                errorSystem();
            }
            break;
        default:
            errorSystem();
    }
};

//Http Delete
function handleDeleteAssigments(res, textStatus, resHttp) {
    switch (textStatus) {
        case "nocontent":
            Swal.fire("Se a eliminado con exito la asignacion...", "", "success");
            closeModal("#modalAssignments");
            break;
        case "error":
            if (res.status == 401) {
                notAuthorize();
            } else if (res.status == 404) {
                Swal.fire("No se encontro la asignacion de la actividad en el sistema...", "", "error");
            } else {
                errorSystem()
            }
            break;
        default:
            errorSystem();
    }
}


function deleteAssignment(assignmetnId) {
    console.log(assignmetnId)
    Swal.fire({
        title: "Quieres eliminar la assignacion con id: " + assignmetnId,
        showCancelButton: true,
        confirmButtonText: "Si",
        cancelButtonText: "No",
    }).then((result) => {
        if (result.isConfirmed) {
            requestDelete("/assignment-activities/" + assignmetnId, handleDeleteAssigments);
        }
    });
}

function loadTableAssignmentActivities(data = null) {

    var table = "<thead>" +
        "<tr>" +
        "<th>Nombre Voluntario</th>" +
        "<th>Telefono Voluntario</th>" +
        "<th>Fecha Inicio</th>" +
        "<th style='text-align:center'>Eliminar</th>" +
        "</tr>" +
        "</thead>" +
        "<tbody>";

    if (data != null) {
        for (var i = 0; i < data.length; i++) {
            let assignment = data[i];

            table += "<tr>" +
                "<td>" + assignment.nameVolunteer + "</td>" +
                "<td>" + assignment.phoneVolunteer + "</td>" +
                "<td>" + assignment.startDate + "</td>" +
                "<td style='text-align:center'>" +
                "<i class='bi bi-trash' onclick='javascript:deleteAssignment(" + assignment.assignmentId + ")' style='cursor:pointer;'></i>" +
                "</td>" +
                "</tr>";
        }
    }

    table += "</tbody>";
    return table;
}