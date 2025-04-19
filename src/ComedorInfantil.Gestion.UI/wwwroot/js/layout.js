
function handleGetModulesByUser(res, textStatus, resHttp) {
    switch (textStatus) {
        case "success":
            fillNav(res.data);
            break;
        case "nocontent":
            fillNav();
            break;
    }
}

function loadNav() {
    const userId = sessionStorage.getItem("userId");

    requestGet("/module-by-users/users/" + userId, handleGetModulesByUser)
}

function fillNav(data = []) {
    let navOptions = "<a id='Home' class='nav-link' href='/Home'>" +
        "<div class='sb-nav-link-icon'>" +
        "<i class='bi bi-speedometer'></i>" +
        "</div>" +
        "Panel Inicio" +
        "</a>";

    if (data.length > 0) {
        data.forEach(x => {
            navOptions += "<a id='" + x.link.split("/")[1] + "' class='nav-link' href='" + x.link + "'>" +
                "<div class='sb-nav-link-icon'>" +
                "<i class='" + x.classCSS + "'></i>" +
                "</div>" +
                x.moduleName +
                "</a>";
        });
    }

    $("#navFill").html(navOptions);
}