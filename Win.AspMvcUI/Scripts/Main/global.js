$(document).ready(function () {
    LoadMainNavSelectedLi();
    $("#aAdsWatch").click(function () {
        $("#adsModal").modal();
    });

    $("#btnAdsClose").click(function () {
        alert("Burada reklamı kapatırsa uyaracağız. Reklamdan geriye bir şeyler dönüyor mu ona bakalım.");
    });

    $("#slider-range").mouseup(function () {
        ProductFiltering();
    });

    $("#ulCategory li a").click(function () {
        var items = document.getElementsByClassName("selectedCategory");
        for (i = 0; i < items.length; i++) {
            $(items[i]).removeClass("selectedCategory");
        }
        $(this).addClass("selectedCategory");
        ProductFiltering();
    });
    var monitor = setInterval(function () {
        var elem = document.activeElement;
        if (elem && elem.tagName == 'IFRAME') {
            clearInterval(monitor);
            $.ajax({
                type: "POST",
                url: "/Home/ReklamTest",
                dataType: "html",
                success: function (response) {
                    //alert("successsss" + response);
                },
                error: function (response) {
                    //alert("errrrrrrorrrrrr" + response);
                }
            });
        }
    }, 100);
});

function SetProductFavorite(val) {
    $.ajax({
        type: "POST",
        url: "/Product/FavoriteAdd",
        data: '{productId: ' + val + ' }',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            var obj = JSON.parse(response);
            var currentElement = document.getElementById("lnkFavorite" + obj.productId);
            if (obj.result == -1) {
                currentElement.classList.remove("favoriteActive");
            }
            else if (obj.result == 0) {
                window.location.href = "/giris";
            }
            else {
                currentElement.classList.add("favoriteActive");
            }
        }
    });
}

function CheckTerms() {
    if (document.getElementById('chkTerms').checked == false) {
        $('#completeOrder').removeClass('btnCompleteOrderActive');
        $('#completeOrder').addClass('btnCompleteOrderDisable');
    }
    else {
        $('#completeOrder').removeClass('btnCompleteOrderDisable');
        $('#completeOrder').addClass('btnCompleteOrderActive');
    }
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
    if (!StringIsNullOrEmptyOrWhiteSpace(contactInformation.Name) || !StringIsNullOrEmptyOrWhiteSpace(contactInformation.Surname) || !StringIsNullOrEmptyOrWhiteSpace(contactInformation.Email) || !StringIsNullOrEmptyOrWhiteSpace(contactInformation.Message) || !StringIsNullOrEmptyOrWhiteSpace(contactInformation.Message.replace("\n", ""))) {
        ShowStatusModal("Zorunlu alanları girip tekrar deneyin.");
    }
    else {
        $.ajax({
            type: "POST",
            url: "/Contact/SendMail",
            data: addAntiForgeryToken({ contactInformation }),
            dataType: "html",
            success: function (response) {
                ShowStatusModal("Mailiniz gönderildi. En kısa sürede işleme alınacaktır, saygılarımızla.");
                window.setTimeout(function () {
                    window.location.href = "/anasayfa";
                }, 3000);
            },
            error: function (response) {
                ShowStatusModal("Teknik bir sorun nedeniyle mailiniz gönderilemedi. Anlayışınız için teşekkürler.");
                window.setTimeout(function () {
                    window.location.href = "/anasayfa";
                }, 3000);
            }
        });
    }
}

function ShowStatusModal(message) {
    document.getElementById("divStatusModal").innerHTML = message;
    $("#status-modal").modal();
}

function ProductFiltering() {
    var filters = {};
    //filters.categoryList = [];
    //$(".chkCategory:checkbox:checked").each(function () {
    //    filters.categoryList.push($(this).attr("categoryId"));
    //});
    var e = document.getElementById("selectProductSort");
    filters.sortingValue = (parseInt(e.selectedIndex) + 1);
    var currentCategory = document.getElementsByClassName("selectedCategory");
    if (currentCategory != null) {
        filters.categoryId = parseInt(currentCategory[0].getAttribute("categoryId"));
    }
    var amountItem = document.getElementById("amount").value.split(" ");
    filters.minPrice = parseFloat(amountItem[0]);
    filters.maxPrice = parseFloat(amountItem[2]);
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
        document.getElementById("userAccountPP").value = $(this).find('.carousel-item.active').data('name');
    })
}

function ProductCheckout() {
    var userAdressInformation = {};
    userAdressInformation.Id = parseInt(document.getElementById("hdnUserAddressId").value);
    userAdressInformation.Name = document.getElementById("uName").value;
    userAdressInformation.Surname = document.getElementById("uSurname").value;
    userAdressInformation.Adress = document.getElementById("uAddress").value;
    userAdressInformation.City = document.getElementById("uCity").value;
    userAdressInformation.Phone = document.getElementById("uPhone").value;
    userAdressInformation.Note = document.getElementById("uNote").value;
    var data = JSON.stringify({
        'userAdressInformation': userAdressInformation,
        'productId': parseInt(document.getElementById("hdnProductId").value)
    });
    $.ajax({
        type: "POST",
        url: "/onay",
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (data) {
            if (data == '"index"') {
                window.location.href = "/urunler";
            }
            else if (data != null) {
                window.location.href = "/onay" + "?id=" + JSON.parse(data);
            }
        },
        error: function (data) {
            window.location.href = "/urunler";
        }
    });
}

function StringIsNullOrEmptyOrWhiteSpace(text) {
    if (!text || text === " ") {
        return false;
    }
    else {
        return true;
    }
}