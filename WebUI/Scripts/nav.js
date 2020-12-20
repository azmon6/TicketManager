
function test(myurl) {
    $.ajax(
        {
            url: myurl,
            data: { page: 1 },
            success: function (data) {
                $('#ShowTicketsPage').parent().replaceWith(data);
            }
        } 
    );
    document.cookie = "test=hue";
    document.cookie = "testing=pe";
    console.log(document.cookie);
}