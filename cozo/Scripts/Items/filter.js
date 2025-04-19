$(document).ready(function () {
    $('#SearchTerm').on('input', function () {
        let searchWord = $("#SearchTerm").val();
        let sortOption = $("#SortOrder").val();
       
        ajaxSearch(searchWord, sortOption);
    });

    $('#SortOrder').on('change', function () {
        let searchWord = $("#SearchTerm").val();
        let sortOption = $("#SortOrder").val();
      

        ajaxSearch(searchWord, sortOption);
    });

   
});

function ajaxSearch(searchWord, sortOption) {
    $("#itemTable").val();
    $.ajax(
        {
            url: "/Items/Index",
            data: { "SearchTerm": searchWord, "SortOrder": sortOption},
            success: function (result) {
                $("#itemTable").html(result)
            },
            error: function () {
                alert("Error loading items.");
            }
        }
    );
}

function ajaxPagination(page) {
    let searchWord = $("#SearchTerm").val();
    let sortOption = $("#SortOrder").val();

    $.ajax({
        url: "/Items/Index",
        data: { "SearchTerm": searchWord, "SortOrder": sortOption, "page": page },
        success: function (result) {
            $("#itemTable").html(result);
        },
        error: function () {
            alert("Error loading items.");
        }
    });
}
