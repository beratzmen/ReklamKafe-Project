$(document).ready(function () {
    LoadMainNavSelectedLi();
    $("#aAdsWatch").click(function () {
        $("#adsModal").modal();
    });

    $("#btnAdsClose").click(function () {
        alert("Burada reklamı kapatırsa uyaracağız. Reklamdan geriye bir şeyler dönüyor mu ona bakalım.");
    });

    $(".price-filter").mouseup(function () {
        ProductFiltering();
    });

    $('#price-min').on('keydown', function (e) {
        if (e.which == 13) {
            ProductFiltering();
            e.preventDefault();
        }
    });

    $('#price-max').on('keydown', function (e) {
        if (e.which == 13) {
            ProductFiltering();
            e.preventDefault();
        }
    });
});

var addAntiForgeryToken = function (data) {
    data.__RequestVerificationToken = $("[name='__RequestVerificationToken']").val();
    return data;
};

function SendMail() {
    var contactInformation = {};
    contactInformation.Name = document.getElementById("contactName").value;
    contactInformation.Surname = document.getElementById("contactSurname").value;
    contactInformation.Email = document.getElementById("contactEmail").value;
    contactInformation.Message = document.getElementById("contactMessage").value;
    $.ajax({
        type: "POST",
        url: "/Contact/SendMail",
        data: addAntiForgeryToken({ contactInformation }),
        dataType: "html",
        success: function (response) {
            ShowStatusModal("Mailiniz gönderildi. En kısa sürede işleme alınacaktır, saygılarımızla.");
            window.setTimeout(function () {
                window.location.href = "../Home/Index"
            }, 3000);
        },
        error: function (response) {
            ShowStatusModal("Teknik bir sorun nedeniyle mailiniz gönderilemedi. Anlayışınız için teşekkürler.");
            window.setTimeout(function () {
                window.location.href = "../Home/Index"
            }, 3000);
        }
    });
}

function ShowStatusModal(message) {
    document.getElementById("divStatusModal").innerHTML = message;
    $("#status-modal").modal();
}

function LoadMainNavSelectedLi() {
    $("#ulMainNav li").removeClass('active');
    var items = document.getElementsByClassName("liMainNav");
    if (window.location.pathname == "/") {
        items[0].classList += " active";
    }
    else {
        for (var i = 0; i < items.length; i++) {
            if (items[i].getElementsByTagName("a")[0].getAttribute("href") == window.location.pathname) {
                items[i].classList += " active";
                break;
            }
        }
    }
}

function ProductFiltering() {
    var filters = {};
    filters.categoryList = [];
    $(".chkCategory:checkbox:checked").each(function () {
        filters.categoryList.push($(this).attr("categoryId"));
    });
    filters.minPrice = parseFloat(document.getElementById("price-min").value);
    filters.maxPrice = parseFloat(document.getElementById("price-max").value);
    $.ajax({
        type: "POST",
        url: "/Product/GetProductByFiltering",
        data: '{filters: ' + JSON.stringify(filters) + ' }',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            $("#divProductList").html(response);
        }
    });
}

function GetSelectedUserPP() {
    //$("#divUserPP > div").each(function () {
    //    if ($(this).hasClass("active")) {
    //        document.getElementById("userAccountPP").value = this.getElementsByTagName("img")[0].getAttribute("src");
    //    }
    //});
    $('#myCarousel').on('slid.bs.carousel', function () {
        document.getElementById("userAccountPP").value = $(this).find('.item.active').data('name');
    })
}