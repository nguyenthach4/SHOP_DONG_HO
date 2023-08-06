var common = {
    init: function () {
        common.registerEvents();
     //   common.loadDataIndex();
    },
    registerEvents: function () {
        $("#txtKeyWord").autocomplete({
            minLength: 0,
            source: function (request, response) {
                $.ajax({
                    url: "/Product/GetListProductByName",
                    dataType: "json",
                    data: {
                        keyword: request.term
                    },
                    success: function (res) {
                        response(res.data);
                    }
                });
            },
            focus: function (event, ui) {
                $("#txtKeyWord").val(ui.item.label);
                return false;
            },
            select: function (event, ui) {
                $("#txtKeyWord").val(ui.item.label);
                return false;
            }
        })
            .autocomplete("instance")._renderItem = function (ul, item) {
                return $("<li>")
                    .append("<div>" + item.label + "</div>")
                    .appendTo(ul);
            };
        $('.btnAddToCart').off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));

            $.ajax({
                url: '/ShoppingCart/Add',
                data: {
                    productId: productId
                },
                type: 'POST',
                dataType: 'json',
                success: function (response) {
                    if (response.status) {
                        swal("Thêm sản phẩm thành công !", "", "success");
                      //  common.loadDataIndex();
                    }
                    else {
                        swal(response.message, "", "error");
                    }


                }

            })

        });

        $('#btnLogout').off('click').on('click', function (e) {
            e.preventDefault();
            $('#frmLogout').submit();
        })

     
    },
    //loadDataIndex: function () {
    //    $.ajax({
    //        url: '/ShoppingCart/GetAll',
    //        type: 'GET',
    //        dataType: 'json',
    //        success: function (res) {
    //            if (res.status) {
    //                if (res.count > 0) {
    //                    $('#newCartItem').html('(' + res.count + 'SP)');
    //                }
                   
    //                common.registerEvents();
    //            }
    //        }
    //    })
    //}

}
common.init();