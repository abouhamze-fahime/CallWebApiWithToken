
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



function LoadCustomerBody(custId) {
    $.ajax({
        url: "/load-customer-modal-body",
        type: "get",
        data: {
            customerId: custId
        },
        beforeSend: function () {
            StartLoading();
        },
        success: function (response) {
            StopLoading();
            $("#ProductModalContent").html(response);
            $("#Customerform").data('validator', null);
            $.validator.unobtrusive.parse('#Customerform');
            $("#ProductModal").modal("show");
        },
        error: function () {
            StopLoading();
        }
    })
}


function CustomerFormSubmited(response) {
    StopLoading();
    if (response.status == "Success") {
        swal("Done", "Operation has done successfully", "success");
        $("#ProductModal").modal("hide");
        $("#CustomerDiv").load(location.href + "#CustomerDiv");
    }
    else {
        swal("Error" , "Something went wrong please try again" , "error");
    }
}


function DeleteCustomer(cusId) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover this customer!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
     .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    url: "/DeleteCustomer/" + `${cusId}`,
                    type: "Get",
                    data: {
                
                        customerId: cusId
                    },
                    beforeSend: function () {
                        StartLoading();
                    },
                    success: function (response) {
                        StopLoading();
                        if (response.status === "Success") {
                            swal("Success", "Operation has done successfully", "success");
                            $(`#Customer-${cusId}`).remove();
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