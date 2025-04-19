const donorId = location.search.split("=")[1].split("&")[0];
const userName = decodeURIComponent(location.search.split("&")[1].split("=")[1]);

$(document).ready(function () {
    $("#nameDonorDonations").text(userName);
    $("#nameDonorMoneyDonations").text(userName);
    getAllInKindDonationsByDonor(donorId);
    getAllMoneyDonations(donorId);
    getDataSelectProduct();
});


//Http Get
function handleResponseGetInKinDonations(res, textStatus, resHttp) {
    switch (textStatus) {
        case "success":
            let tableHtml = loadTableInKinDonations(res.data);
            $("#tblInKindDonations").html(tableHtml);
            paginar("#tblInKindDonations");
            break;
        case "nocontent":
            $("#tblInKindDonations").html(loadTableInKinDonations());
            paginar("#tblInKindDonations");
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

function handleResponseGetMoneyDonations(res, textStatus, resHttp) {
    switch (textStatus) {
        case "success":
            let tableHtml = loadTableMoneyDonations(res.data)
            $("#tblMoneyDonations").html(tableHtml);
            paginar("#tblMoneyDonations");
            break;
        case "nocontent":
            $("#tblMoneyDonations").html(loadTableMoneyDonations());
            paginar("#tblMoneyDonations");
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

function getAllInKindDonationsByDonor(donorId) {
    requestGet("/in-kind-donations/donors/" + donorId, handleResponseGetInKinDonations);
}

function getAllMoneyDonations(donorId) {
    requestGet("/money-donations/donors/" + donorId, handleResponseGetMoneyDonations);
}

function getDataSelectProduct() {
    $.ajax({
        url: urlBase + "/in-kind-donations",
        method: "GET",
        success: function (resIn) {
            $.ajax({
                url: urlBase + "/inventories",
                method: "GET",
                success: function (res) {
                    fillSelectProduct(resIn.data, res.data)
                }
            });
        }
    });
}


//Http Delete
function handleDeleteInKindDonations(res, textStatus, resHttp) {
    switch (textStatus) {
        case "nocontent":
            Swal.fire("Se a eliminado con exito la donacion en especie...", "", "success");
            getAllInKindDonationsByDonor(donorId);
            getDataSelectProduct();
            break;
        case "error":
             if (res.status == 401) {
                notAuthorize();
            } else if (res.status == 404) {
                Swal.fire("No se encontro la donacion en el sistema...", "", "error");
            } else {
                errorSystem()
            }
            break;
        default:
            errorSystem();
    }
}

function handleDeleteMoneyDonations(res, textStatus, resHttp) {
    switch (textStatus) {
        case "nocontent":
            Swal.fire("Se a eliminado con exito la donacion monetaria...", "", "success");
            getAllMoneyDonations(donorId);
            break;
        case "error":
             if (res.status == 401) {
                notAuthorize();
            } else if (res.status == 404) {
                Swal.fire("No se encontro la donacion en el sistema...", "", "error");
            } else {
                errorSystem()
            }
            break;
        default:
            errorSystem();
    }
}

function deleteMoneyDonation(moneyDonationId, name) {
    Swal.fire({
        title: "Quieres eliminar la donacion monetaria: '" + name + "' con id: " + moneyDonationId,
        showCancelButton: true,
        confirmButtonText: "Si",
        cancelButtonText: "No",
    }).then((result) => {
        if (result.isConfirmed) {
            requestDelete("/money-donations/" + moneyDonationId, handleDeleteMoneyDonations);
        }
    });
}

function deleteInKindDonation(inKindDonationId, name) {
    Swal.fire({
        title: "Quieres eliminar la donacion en especie: '" + name + "' con id: " + inKindDonationId,
        showCancelButton: true,
        confirmButtonText: "Si",
        cancelButtonText: "No",
    }).then((result) => {
        if (result.isConfirmed) {
            requestDelete("/in-kind-donations/" + inKindDonationId, handleDeleteInKindDonations);
        }
    });
}


//Http Post
function handleCreateInKindDonation(res, textStatus, resHttp) {
    switch (textStatus) {
        case "success":
            createToastSwal("success", "Se a creado la donacion...");
            $("#txtProductId").val("");
            $("#errorProductId").text("");
            $("#btnCreateInKindDonation").click();
            getAllInKindDonationsByDonor(donorId);
            break;
        case "error":
            if (res.status == 400) {
                $("#errorProductId").text("Selecciona una opcion");
            } else if (res.status == 401) {
                notAuthorize();
            } else {
                errorSystem();
            }
            break;
    }
}

function handleCreateMoneyDonation(res, textStatus, resHttp) {
    switch (textStatus) {
        case "success":
            createToastSwal("success", "Se a creado la donacion...");
            cleanFormMoneyDonation();
            $("#btnCreateMoneyDonation").click();
            getAllMoneyDonations(donorId);
            break;
        case "error":
            if (res.status == 400) {
                validationFormMoneyDonation(res.responseJSON);
            } else if (res.status == 401) {
                notAuthorize();
            } else {
                errorSystem();
            }
            break;
    }
}

//forms
$("#frmCreateInKindDonation").on("submit", function (e) {
    e.preventDefault();

    const kindDonation = {
        donorId,
        productId: $("#txtProductId").val(),
        donationDate: new Date().toISOString()
    }
    requestPost("/in-kind-donations", JSON.stringify(kindDonation), handleCreateInKindDonation);
    
});

$("#frmCreateMoneyDonation").on("submit", function (e) {
    e.preventDefault();
    console.log($("#txtAmountMoney").val())
    const moneyDonation = {
        donorId,
        amount: $("#txtAmountMoney").val() ? $("#txtAmountMoney").val() : 0,
        porpuse: $("#txtPorpuseMoney").val(),
        donationDate: new Date().toISOString()
    }
    console.log(moneyDonation)
    requestPost("/money-donations", JSON.stringify(moneyDonation), handleCreateMoneyDonation);

});

//tables
function loadTableMoneyDonations(data = []) {
    var table = "<thead>" +
        "<tr>" +
        "<th>Proposito</th>" +
        "<th style='text-align:left'>Cantidad</th>" +
        "<th style='text-align:left'>Dia Donacion</th>" +
        "<th style='text-align:center'>Eliminar</th>" +
        "</tr>" +
        "</thead>" +
        "<tbody>";

    if (data != null || data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            var moneyDonation = data[i];

            let amount = moneyDonation.amount.toLocaleString('es-CR', {
                style: 'currency',
                currency: 'CRC'
            });

            table += "<tr>" +
                "<td style='text-align:left'>" + moneyDonation.porpuse + "</td > " +
                "<td style='text-align:left'>" + amount + "</td>" +
                "<td style='text-align:left'>" + moneyDonation.donationDate.split("T")[0] + "</td>" +
                "<td style='text-align:center'>" +
                "<i class='bi bi-trash' onclick='javascript:deleteMoneyDonation(" + moneyDonation.moneyDonationId + ",\"" + moneyDonation.porpuse + "\")' style='cursor:pointer;'></i>" +
                "</td>" +
                "</tr>";
        }
    }

    table += "</tbody>";
    return table;
}

function loadTableInKinDonations(data = []) {
    var table = "<thead>" +
        "<tr>" +
        "<th>Nombre Producto</th>" +
        "<th style='text-align:left'>Cantidad</th>" +
        "<th style='text-align:left'>Dia Donacion</th>" +
        "<th style='text-align:center'>Eliminar</th>" +
        "</tr>" +
        "</thead>" +
        "<tbody>";

    if (data != null || data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            var inKindDonation = data[i];
            table += "<tr>" +
                "<td>" + inKindDonation.productName + "</td >" +
                "<td style='text-align:left'>" + inKindDonation.productQuantity + "</td>" +
                "<td style='text-align:left'>" + inKindDonation.donationDate.split("T")[0] + "</td>" +
                "<td style='text-align:center'>" +
                "<i class='bi bi-trash' onclick='javascript:deleteInKindDonation(" + inKindDonation.inKindDonationId + ",\"" + inKindDonation.productName + "\")' style='cursor:pointer;'></i>" +
                "</td>" +
                "</tr>";
        }
    }

    table += "</tbody>";
    return table;
}

//buttoms actions
$("#btnCreateInKindDonation").click(function () {
    $("#toggleCreateInKindDonation").slideToggle("slow");
})

$("#btnCreateMoneyDonation").click(function () {
    $("#toggleCreateMoneyDonation").slideToggle("slow");
})

//others
function fillSelectProduct(inKindDonations = [], inventories = []) {

    let selectOptions = ""
    if (inventories.length == 0 || inKindDonations.length === 0) {
        selectOptions = "<option value='' selected>No hay productos</option>";
        $("#txtProductId").html(selectOptions);
        $("#btnSubmitCreateInKindDonation").prop("disabled", true);
        return
    }
    selectOptions = "<option value='' selected>Selecciona el producto</option>";

    let filterInKindDonation = inventories

    inKindDonations.forEach(x => {
        filterInKindDonation = filterInKindDonation.filter(i => i.inventoryId !== x.productId)
    });
    filterInKindDonation.forEach(x => {
        selectOptions += "<option value='" + x.inventoryId + "'>" + x.productName + "</option > ";
    });
   
    $("#txtProductId").html(selectOptions);
}

function validationFormMoneyDonation(errors = []) {
    const errorsClient = {};

    errors.forEach((e) => {
        errorsClient[e.campo] = e.mensaje
    });
    errorsClient.Porpuse ? $("#errorPorpuseMoney").text(errorsClient.Porpuse)
        : $("#errorPorpuseMoney").text("");
    errorsClient.Amount ? $("#errorAmountMoney").text(errorsClient.Amount)
        : $("#errorAmountMoney").text("");
}

function cleanFormMoneyDonation() {
    $("#errorPorpuseMoney").text("");
    $("#errorAmountMoney").text("");

    $("#txtPorpuseMoney").val("");
    $("#txtAmountMoney").val(0);
}


