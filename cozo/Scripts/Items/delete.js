//let itemId;

//function openConfirmationModal(id, name,price,quantity) {
//    let itemName = name;
//    itemId = id;
//    let Itemprice = price;
//    let Itemquantatity = quantity

//    $("#itemNameText").text(itemName);
//    $("#itemPrice").text(Itemprice);
//    $("#itemQuantity").text(Itemquantatity);
//    console.log(itemName, Itemprice, Itemquantatity, itemId);

//    $("#exampleModal").modal("show");
//}

//function confirmDelete() {
//    $.ajax(
//        {
//            url: "/Items/Delete/" + itemId,
//            type: 'POST',
//            success: function (result) {
//                $("#" + itemId).remove();
//                $("#exampleModal").modal("toggle");
//            },
//            error: function () {
//                alert("Failed to delete.");
//            }
//        }
//    );
//}


let itemId;
let formItemId;

// Called from Index table
function openConfirmationModal(id, name, price, quantity) {
    itemId = id;
    $("#itemNameText").text(name);
    $("#itemPrice").text(price);
    $("#itemQuantity").text(quantity);
    $("#exampleModal").modal("show");
}

// Called from Form view
function openFormDeleteModal(id, name, price, quantity) {
    formItemId = id;
    $("#formItemNameText").text(name);
    $("#formItemPrice").text(price);
    $("#formItemQuantity").text(quantity);
    $("#formDeleteModal").modal("show");
}

// AJAX delete from Index
function confirmDelete() {
    $.ajax({
        url: "/Items/Delete/" + itemId,
        type: 'POST',
        success: function () {
            $("#" + itemId).remove();
            $("#exampleModal").modal("toggle");
        },
        error: function () {
            alert("Failed to delete.");
        }
    });
}

// AJAX delete from Form (redirect after)
function confirmFormDelete() {
    $.ajax({
        url: "/Items/Delete/" + formItemId,
        type: 'POST',
        success: function () {
            window.location.href = "/Items/Index"; // Redirect after successful delete
        },
        error: function () {
            alert("Failed to delete.");
        }
    });
}
