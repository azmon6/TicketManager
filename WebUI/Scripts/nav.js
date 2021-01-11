
var whichColumn;
var ascOrder;

function RefreshTable() {
    $.ajax(
        {
            url: '/Admin/GetTicketTable',
            data: { page: 1 },
            success: function (data) {
                $('#ShowTicketsPage').parent().replaceWith(data);
            }
        } 
    );
}

function orderColumn(value1) {

    $.ajax(
        {
            url: '/Cookie/TicketOrder',
            data: {column: value1},
            success: function (data) {
                whichColumn = data.col;
                ascOrder = data.direc;
                RefreshTable();
            }
        }
    );
}

function BuyTicket(data) {
    $.ajax(
        {
            url: "/Cart/BuyTicket",
            data: { tickId: data },
            success: function (test) {
                if (test == "") {
                    GetSideCart();
                }
                else {
                    window.location.href = test.redirectToUrl;
                }
            }
        }
    );
}

// TODO Move to Cart JS
function GetSideCart() {
    $.ajax(
        {
            url: "/Cart/GetSideCart",
            data: { },
            success: function (result) {
                $(".dynamicCart").html(result);
            },
            complete: function () {
                $("#openIcon").css("visibility", "visible");
            }
        }
    );
}


// TODO Move to Cart JS
function DeleteSideCartItem(tickId) {
    $.ajax(
        {
            url: "/Cart/RemoveLine",
            data: { tickId: tickId , ajax: true },
            success: function () {
                GetSideCart();
            }
        }
    );  
}

function DropActions(dropID) {
    $('#' + dropID).toggleClass("show");
}

window.onclick = function (event) {
    if (!event.target.matches('.dropbtn')) {
        var dropdowns = document.getElementsByClassName("dropdownActions");
        var i;
        for (i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
    }
}