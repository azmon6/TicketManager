var whichColumn = "EventTime";

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

function orderColumn(value1 , value2) {
    $.ajax(
        {
            url: '/Cookie/TicketOrder',
            data: {column: value1 , asc: value2},
            success: function (data) {
                console.log("TicketOrder Ajax has completed.");
                whichColumn = data;
                RefreshTable();
            }
        }
    );
}