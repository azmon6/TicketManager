
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