
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

function BuyTicket(ticketID) {
    $.ajax(
        {
            url: "/Cart/BuyTicket",
            data: { ticketToBuy: ticketID },
            success: function (data) {
                if (data == "") {
                    GetSideCart();
                }
                else {
                    window.location.href = data.redirectToUrl;
                }
            },
            error: function () {
                alert("Ticket was unable to be bought.");
            }
        }
    );
}

function DropActions(dropID) {
    var dropdowns = document.getElementsByClassName("dropdownActions");
    var i;
    for (i = 0; i < dropdowns.length; i++) {
        var openDropdown = dropdowns[i];
        openDropdown.classList.remove('show');
    }
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