$("#openIcon").click(GetSideCart);

$(document).ready(function () {
    GetSideCart();
});

function openNav() {
    document.getElementById("mySidepanel").style.width = "300px";
    document.getElementById("mySidepanel").style.height = "auto";
    document.getElementById("mySidepanel").style.border = "3px solid black"
    var temp = document.getElementById("openIcon");
    temp.onclick = closeNav;
    $(temp).toggleClass("fa-angle-double-left fa-angle-double-right");
}

function closeNav() {
    document.getElementById("mySidepanel").style.height = $("#cartSidePanel").height() + "px";
    document.getElementById("mySidepanel").style.width = "0";
    document.getElementById("mySidepanel").style.border = "0px solid black"
    var temp = document.getElementById("openIcon");
    temp.onclick = openNav;
    $(temp).toggleClass("fa-angle-double-left fa-angle-double-right");
}

function DeleteSideCartItem(tickId) {
    $.ajax(
        {
            url: "/Cart/RemoveLine",
            data: { tickId: tickId, ajax: true },
            success: function () {
                GetSideCart();
            }
        }
    );
}

function GetSideCart() {
    $.ajax(
        {
            url: "/Cart/GetSideCart",
            data: {},
            success: function (result) {
                $(".dynamicCart").html(result);
            },
            complete: function () {
                $("#openIcon").css("visibility", "visible");
            }
        }
    );
}