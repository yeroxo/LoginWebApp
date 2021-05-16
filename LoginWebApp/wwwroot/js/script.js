window.onload = function () {
    $(".item__check-btn").click((e) => {
        var btn = $(e.currentTarget);
        var postId = btn.attr("attr");
        var xhr = new XMLHttpRequest();
        xhr.open("PATCH", "/Account/" + "PatchEvent/" + postId)
        xhr.responseType = "json"
        xhr.send();
        window.location.reload();
    });


    $(".item__delete-btn").click((e) => {
        var btn = $(e.currentTarget);
        var postId = btn.attr("attr");
        var xhr = new XMLHttpRequest();
        xhr.open("DELETE", "/Account/" + "DeleteEvent/" + postId)
        xhr.send();
        // $.post("/item/" + postId + "/delete");
        $("#" + postId + "-item").remove();
        window.location.reload();
    });

    /*
    $("#item_add").click((e) => {
        $(e.currentTarget).css('display', 'none');
        $(".add-new-item-form").css('display', 'block');
    });
    */
};