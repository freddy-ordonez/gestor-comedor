//Const
const inventoryId = location.search.split("=").pop()
const listInputsInventories = ["txtExpiryDateInventory", "txtNameInventory", "txtDescriptionInventory", "txtQuantityInventory"];

$(document).ready(function () {
    let pageName = location.pathname.split('/').pop();
    if (pageName === "Inventory") {
        getAllInventories();
    } else if (pageName == "Update") {
        getInventoryById(inventoryId);
    }
});


//Http Get
function handleResponseGetAllInventories(res, textStatus, resHttp) {

    switch (textStatus) {
        case "success":
            let tableHtml = loadTableInventories(res.data)
            $("#tblInventories").html(tableHtml);
            paginar("#tblInventories");
            break;
        case "nocontent":
            $("#tblInventories").html(loadTableInventories());
            paginar("#tblInventories");
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

function handleResponseGetInventoryById(res, textStatus, resHttp) {
    let inventory = res.data
    switch (textStatus) {
        case "success":
            $("#txtNameInventory").val(inventory.productName);
            $("#txtDescriptionInventory").val(inventory.description);
            $("#txtQuantityInventory").val(inventory.quantity);
            $("#txtEntryDateInventory").val(inventory.entryDate.split("T")[0]);
            $("#txtExpiryDateInventory").val(inventory.expiryDate.split("T")[0]);
            break;
        case "error":
            if (res.status === 401) {
                notAuthorize();
            } else if (res.status === 404) {
                notFound("No se encuentra ningun registro de el inventario seleccionado...",
                    "/Inventory");
            }
            else {
                errorSystem();
            }
            break;
        default:
            errorSystem();
    }
}

function getAllInventories() {
    requestGet("/inventories", handleResponseGetAllInventories);
}

function getInventoryById(inventoryId) {
    requestGet("/inventories/" + inventoryId, handleResponseGetInventoryById);
}


//Http Delete
function handleDeleteInventory(res, textStatus, resHttp) {
    switch (textStatus) {
        case "nocontent":
            Swal.fire("Se a eliminado con exito el inventario...", "", "success");
            getAllInventories();
            break;
        case "error":
            if (res.status == 400) {
                Swal.fire("El inventario no se puede eliminar ya que esta haciendo utilizada por otro registro en el sistema...", "", "info");
            } else if (res.status == 401) {
                notAuthorize();
            } else if (res.status == 404) {
                Swal.fire("No se encontro el inventario en el sistema...", "", "error");
            } else {
                errorSystem()
            }
            break;
        default:
            errorSystem();
    }
}

function deleteInventory(inventoryId, name) {
    Swal.fire({
        title: "Quieres eliminar el inventario de: '" + name + "' con id: " + inventoryId,
        showCancelButton: true,
        confirmButtonText: "Si",
        cancelButtonText: "No",
    }).then((result) => {
        if (result.isConfirmed) {
            requestDelete("/inventories/" + inventoryId, handleDeleteInventory);
        }
    });
}


//Http Post
function handleCreateInventory(res, textStatus, resHttp) {
    switch (textStatus) {
        case "success":
            createToastSwal("success", "Se a creado el inventario...");
            cleanFormInventory();
            break;
        case "error":
            if (res.status == 400) {
                validationFormInventory(res.responseJSON);
            } else if (res.status == 401) {
                notAuthorize();
            } else {
                errorSystem();
            }
            break;
    }
}


//Http Put
function handleUpdateInventory(res, textStatus, resHttp) {
    switch (textStatus) {
        case "nocontent":
            createToastSwal("success", "Se a actualizado el inventario...");
            setTimeout(function () {
                location.href = "/Inventory"
            }, 3000)
            break;
        case "error":
            if (res.status == 400) {
                validationFormInventory(res.responseJSON);
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

function updateInventory(inventoryId) {
    location.href = "/Inventory/Update?id=" + inventoryId
}


//Forms
$("#frmCreateInventory").on("submit", function (e) {
    e.preventDefault();
    $("#btnSubmitCreateInventory").prop("disabled", true);

    const expiryDate = $("#txtExpiryDateInventory").val() === "" ? null : $("#txtExpiryDateInventory").val();
    const quantity = $("#txtQuantityInventory").val() ? $("#txtQuantityInventory").val() : 0;

    const formData = {
        productName: $("#txtNameInventory").val(),
        description: $("#txtDescriptionInventory").val(),
        quantity,
        expiryDate,
        entryDate: new Date().toISOString()
    }

    requestPost("/inventories", JSON.stringify(formData), handleCreateInventory);
    setTimeout(function () {
        $("#btnSubmitCreateInventory").prop("disabled", false);
    }, 3000);
});

$("#frmUpdateInventory").on("submit", function (e) {
    e.preventDefault();
    $("#btnSubmitUpdateInventory").prop("disabled", true);

    const expiryDate = $("#txtExpiryDateInventory").val() === "" ? null : $("#txtExpiryDateInventory").val();

    const formData = {
        productName: $("#txtNameInventory").val(),
        description: $("#txtDescriptionInventory").val(),
        quantity: $("#txtQuantityInventory").val(),
        expiryDate,
        entryDate: $("#txtEntryDateInventory").val()
    }

    requestPut("/inventories/" + inventoryId, JSON.stringify(formData), handleUpdateInventory);
    setTimeout(function () {
        $("#btnSubmitUpdateInventory").prop("disabled", false);
    }, 3000);
});


//Tables
function loadTableInventories(data = []) {

    var table = "<thead>" +
        "<tr>" +
        "<th style='text-align:left'>Nombre</th>" +
        "<th style='text-align:left'>Descripción</th>" +
        "<th style='text-align:left'>Cantidad</th>" +
        "<th style='text-align:left'>Fecha de Entrada</th>" +
        "<th style='text-align:left'>Fecha de Expiracion</th>" +
        "<th style='text-align:center'>Actualizar</th>" +
        "<th style='text-align:center'>Eliminar</th>" +
        "</tr>" +
        "</thead>" +
        "<tbody>";

    if (data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            var inventory = data[i];

            table += "<tr>" +
                "<td style='text-align:left'>" + inventory.productName + "</td>" +
                "<td style='text-align:left'>" + inventory.description + "</td>" +
                "<td style='text-align:left'>" + inventory.quantity + "</td>" +
                "<td style='text-align:left'>" + inventory.entryDate.split("T")[0] + "</td>" +
                "<td style='text-align:left'>" + inventory.expiryDate.split("T")[0] + "</td>" +
                "<td style='text-align:center'>" +
                "<i class='bi bi-pencil-square' onclick='javascript:updateInventory(" + inventory.inventoryId + ")' style='cursor:pointer;'></i>" +
                "</td>" +
                "<td style='text-align:center'>" +
                "<i class='bi bi-trash' onclick='javascript:deleteInventory(" + inventory.inventoryId + ",\"" + inventory.productName + "\")' style='cursor:pointer;'></i>" +
                "</td>" +
                "</tr>";
        }
    }

    table += "</tbody>";
    return table;
}

//Others
function validationFormInventory(errors = []) {

    const errorsClient = {};

    errors.forEach((e) => {
        errorsClient[e.campo] = e.mensaje
    });
    errorsClient.ProductName ? $("#errorNameInventory").text(errorsClient.ProductName)
        : $("#errorNameActivity").text("");
    errorsClient.Description ? $("#errorDescriptionInventory").text(errorsClient.Description)
        : $("#errorDescriptionInventory").text("");
    errorsClient.Quantity ? $("#errorQuantityInventory").text(errorsClient.Quantity)
        : $("#errorQuantityInventory").text("")
    errorsClient.ExpiryDate ? $("#errorExpiryDateInventory").text(errorsClient.ExpiryDate)
        : $("#errorExpiryDateInventory").text("");
}

function cleanFormInventory() {
    validationFormInventory()
    listInputsInventories.forEach(a => {
        if (a === "txtQuantityInventory") {
            $("#" + a).val(0);
        } else {
            $("#" + a).val("");
        }
    });
}