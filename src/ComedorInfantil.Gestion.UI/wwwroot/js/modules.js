const userId = location.search.split("=")[1].split("&")[0];
const userName = decodeURIComponent(location.search.split("&")[1].split("=")[1]);

$(document).ready(function() {
    $("#nameUserModules").text(userName);
    getAllModules();
});

//Http Get
function getAllModules(activityId) {
    $.ajax({
        url: urlBase + "/module-by-users/users/" + userId,
        method: "GET",
        success: (res) => {
            $.ajax({
                url: urlBase + "/modules",
                method: "GET",
                success: (resModules) => {
                    let modulesUser = res === undefined ? [] : res.data;
                    let filterModulesAvailability = resModules === undefined ? [] : resModules.data;
                    fillDataTables(modulesUser, filterModulesAvailability);                   
                },
                error: () => {
                    fillDataTables();
                }
            })
        },
        error: () => {
            fillDataTables();
        }
    });
}

//Http Post
function handleAddModuleByUser(res, textStatus, resHttp) {
    switch (textStatus) {
        case "success":
            Swal.fire("Se a agregado con exito el modulo del usuario...", "", "success");
            getAllModules();
            break;
        case "error":
            if (res.status == 401) {
                notAuthorize();
            } else if (res.status == 404) {
                Swal.fire("No se encontro la asignacion de el modulo en el sistema...", "", "error");
            } else {
                errorSystem()
            }
            break;
        default:
            errorSystem();
    }
}

function addModuleByUser(moduleId) {
    const moduleByUser = {
        moduleId,
        userId
    }

    requestPost("/module-by-users", JSON.stringify(moduleByUser), handleAddModuleByUser)
}

//Http Delete
function handleDeleteModuleByUser(res, textStatus, resHttp) {
    switch (textStatus) {
        case "nocontent":
            Swal.fire("Se a eliminado con exito el modulo del usuario...", "", "success");
            getAllModules();
            break;
        case "error":
            if (res.status == 401) {
                notAuthorize();
            } else if (res.status == 404) {
                Swal.fire("No se encontro la asignacion de el modulo en el sistema...", "", "error");
            } else {
                errorSystem()
            }
            break;
        default:
            errorSystem();
    }
}

function deleteModuleByUser(moduleByUserId) {
    Swal.fire({
        title: "Quieres eliminar el modulo con id: " + moduleByUserId,
        showCancelButton: true,
        confirmButtonText: "Si",
        cancelButtonText: "No",
    }).then((result) => {
        if (result.isConfirmed) {
            requestDelete("/module-by-users/" + moduleByUserId, handleDeleteModuleByUser);
        }
    });
}


//Tables
function fillDataTables(modulesUser = [], filterModulesAvailability = []) {
    modulesUser.forEach(m => {
        filterModulesAvailability = filterModulesAvailability.filter(f => f.moduleId !== m.moduleId)
    })

    $("#tblModulesUser").html(loadTableModuleByUser(modulesUser));
    paginar("#tblModulesUser");

    $("#tblModulesAvailability").html(loadTableModuleAvailability(filterModulesAvailability));
    paginar("#tblModulesAvailability");
}

function loadTableModuleByUser(data = []) {

    var table = "<thead>" +
        "<tr>" +
        "<th>Nombre Modulo</th>" +
        "<th style='text-align:center'>Eliminar</th>" +
        "</tr>" +
        "</thead>" +
        "<tbody>";

    if (data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            let module = data[i];

            table += "<tr>" +
                "<td>" + module.moduleName + "</td>" +
                "<td style='text-align:center'>" +
                "<i class='bi bi-trash' onclick='javascript:deleteModuleByUser(" + module.moduleForUserId + ")' style='cursor:pointer;'></i>" +
                "</td>" +
                "</tr>";
        }
    }

    table += "</tbody>";
    return table;
}

function loadTableModuleAvailability(data = []) {

    var table = "<thead>" +
        "<tr>" +
        "<th>Nombre Modulo</th>" +
        "<th style='text-align:center'>Agregar</th>" +
        "</tr>" +
        "</thead>" +
        "<tbody>";

    if (data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            let module = data[i];

            table += "<tr>" +
                "<td>" + module.moduleName + "</td>" +
                "<td style='text-align:center'>" +
                "<i class='bi bi-plus-square' onclick='javascript:addModuleByUser(" + module.moduleId + ")' style='cursor:pointer;'></i>" +
                "</td>" +
                "</tr>";
        }
    }

    table += "</tbody>";
    return table;
}