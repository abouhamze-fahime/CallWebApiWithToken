
function StartLoading(element = 'body') {
    $(element).waitMe({
        effect: 'bounce',
        text: 'Please wait',
        bg: 'rgba(255, 255, 255, 0.7)',
        color: '#000'

    });
}


function StopLoading(element = 'body') {
    $(element).waitMe('hide');
}



function LoadProductBody(prodId) {
    $.ajax({
        url: "/load-product-modal-body",
        type: "get",
        data: {
            id: prodId
        },
        beforeSend: function () {
            StartLoading();
        },
        success: function (response) {
            StopLoading();
            $("#ProductModalContent").html(response);
            $("#Productform").data('validator', null);
            $.validator.unobtrusive.parse('#Productform');
            $("#ProductModal").modal("show");
        },
        error: function () {
            StopLoading();
        }
    })
}


function ProductFormSubmited(response) {
    StopLoading();
    if (response.status == "Success") {
        swal("Done", "Operation has done successfully", "success");
        $("#ProductModal").modal("hide");
        $("#ProductDiv").load(location.href + "#ProductDiv");
    }
    else {
        swal("Error", "Something went wrong please try again", "error");
    }
}




function DeleteProduct(procId) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover this Product!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    url: "/delete-product",
                    type: "Get",
                    data: {

                        productId: procId
                    },
                    beforeSend: function () {
                        StartLoading();
                    },
                    success: function (response) {
                        StopLoading();
                        if (response.status === "Success") {
                            swal("Success", "Operation has done successfully", "success");
                            $(`#Product-${procId}`).remove();
                        }
                        else {
                            swal("Error", "Something went wrong please try again", "error");
                        }
                    },
                    error: function () {
                        StopLoading();
                    }
                })
            }
        });
}