//$(function () {
//    $('body').on('click', '.open-modal', function (e) {
//        e.preventDefault();
//        $(this).attr('data-target', '#myModal');
//        $(this).attr('data-toggle', 'modal');
//    });

//    $('body').on('click', '.close-modal', function () {
//        $('#myModal').modal('hide');
//    });

//    $('#myModal').on('hidden.bs.modal', function () {
//        $(this).removeData('bs.modal');
//    });
//});

$(function () {
    $.ajaxSetup({ cache: false });
    $("a[data-modal]").on("click", function (e) {
        $("#modalContent").load(this.href, function () {
            $("#myModal").modal({
                keyboard: true
            }, "show");

            bindForm(this);
        });
        return false;
    });
});


$(function () {
    $.ajaxSetup({ cache: false });
    $('body').on("click", ".open-modal", function (event) {

        event.preventDefault();

        var url = $(this).attr("href");

        $.get(url, function (data) {
            $("#modalContent").html(data);

            $("#myModal").modal("show");
        });

    });
});

$(function () {
    $.ajaxSetup({ cache: false });
    $("#exampleIn").on("click", ".open-modal", function (event) {

        event.preventDefault();

        var url = $(this).attr("href");

        $.get(url, function (data) {
            $("#modalContent").html(data);

            $("#myModal").modal("show");
        });

    });
});

function bindForm(dialog) {
    $("form", dialog).submit(function () {
        $("#progress").show();
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    $('#myModal').modal("hide");
                    ////$('#progress').hide();
                    location.reload();
                } else {
                    $("#progress").hide();
                    $("#modalContent").html(result);
                    bindForm();
                }
            }
        });
        return false;
    });
}
