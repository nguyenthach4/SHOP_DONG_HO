var cart = {
    intit: function () {
        cart.loadData();
        cart.registerEvents();
    },
    registerEvents: function () {
        $('#frmPayment').validate({
            rules: {
                name: "required",
                address: "required",
                email: {
                    required: true,
                    email: true
                },
                phone: {
                    required: true,
                    number: true
                }
            },
            messages: {
                name: "Yêu cầu nhập tên",
                address: "Yêu cầu nhập địa chỉ",
                email: {
                    required: "Yêu cầu nhập email",
                    email: "Định dạng email chưa đúng"
                },
                phone: {
                    required: "Yêu cầu nhập số điện thoại",
                    number: "Sô điện thoại phải là số"
                }
            }
        })
        $('.btnDeleteItem').off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));;
            cart.deleteItem(productId);
        });
        $('.txtQuantity').off('keyup').on('keyup', function () {
            var quantity = parseInt($(this).val());
            var productid = parseInt($(this).data('id'));
            var price = parseFloat($(this).data('price'));
            if (isNaN(quantity) == false) {
                var amount = quantity * price;

                $('#amount_' + productid).text(numeral(amount).format('0,0'));
            }
            else {
                $('#amount_' + productid).text(0);
            }
            $('#lblTotalOrder').text(numeral(cart.getTotalOrder()).format('0,0'));
        });
        $('#btnContinue').off('click').on('click', function (e) {
            e.preventDefault();
            window.location.href = '/';
            cart.loadData();

        });
        $('#btnDeleteAll').off('click').on('click', function (e) {
            e.preventDefault();
            cart.deleteAll();
        });
        $('#btnCheckout').off('click').on('click', function (e) {
            e.preventDefault();

            $('#divCheckout').toggle();
        });
        $('#chkUserLoginInfo').off('click').on('click', function (e) {
            if ($(this).prop('checked'))
                cart.getLoginUser();
            else {
                $('#txtName').val('');
                $('#txtAddress').val('');
                $('#txtEmail').val('');
                $('#txtPhone').val('');
                $('#txtName').val('');
            }
        });
        $('#btnCreateOrder').off('click').on('click', function (e) {
            e.preventDefault();
            var isValid = $('#frmPayment').valid();
            if (isValid) {
                cart.createOrder();
            }

        });
        $('input[name="paymentMethod"]').off('click').on('click', function () {
            if ($(this).val() == 'NL') {
                $('.boxContent').hide();
                $('#nganluongContent').show();
            } else if ($(this).val() == 'ATM_ONLINE') {
                $('.boxContent').hide();
                $('#bankContent').show();
            } else {
                $('.boxContent').hide();
            }
        });


    },
    createOrder: function () {
        let quantiy = $('#quantiy').val();
        console.log(quantiy);
        var order = {
            CustomerName: $('#txtName').val(),
            CustomerAddress: $('#txtName').val(),
            CustomerEmail: $('#txtName').val(),
            CustomerMobile: $('#txtName').val(),
            CustomerMessage: $('#txtName').val(),
            PaymentMethod: $('input[name="paymentMethod"]:checked').val(),
            //     BankCode: $('input[groupName="bankcode"]:checked').prop('id'),
            Status: false
        }
        $.ajax({
            url: '/ShoppingCart/CreateOrder',
            data: {
                //    quantity: quantiy,
                orderViewModel: JSON.stringify(order)
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    if (response.urlCheckOut != undefined && response.urlCheckOut != '') {
                        window.location.href = response.urlCheckOut;
                        
                    }
                    else {
                        $('#divCheckout').hide();
                        cart.deleteAll();
                        swal('Đặt hành thành công !', "Nhấn OK để tiếp tục !", "info");
                    }
                    if (response.url != undefined && response.url != '') {
                        window.location.href = response.url;

                    }
                }
                else
                    swal(response.message, "Nhấn OK để tiếp tục !", "info");
            }

        })
    },
    getTotalOrder: function () {
        var listTextBox = $('.txtQuantity');
        var total = 0;
        $.each(listTextBox, function (i, item) {
            total += parseInt($(item).val()) * parseFloat($(item).data('price'));
        })
        return total;
    },
    getLoginUser: function () {
        $.ajax({
            url: '/ShoppingCart/GetUser',
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var user = response.data;
                    $('#txtName').val(user.FullName);
                    $('#txtAddress').val(user.Address);
                    $('#txtEmail').val(user.Email);
                    $('#txtPhone').val(user.PhoneNumber);
                    $('#txtName').val(user.FullName);
                }
            }

        })
    },
    deleteItem: function (productId) {
        console.log(productId);
        $.ajax({
            url: '/ShoppingCart/DeleteItem',
            data: {
                productId: productId
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    cart.loadData();
                }

            }

        })
    },
    deleteAll: function () {
        $.ajax({
            url: '/ShoppingCart/DeleteAll',
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    cart.loadData();
                }
            }

        })
    },
    updateAll: function () {
        let cartList = [];
        $.each(('txtQuantity'), function (i, item) {
            cartList.push({
                ProductId: $(item).data('id'),
                Quantity: $(item).val()
            })
        }),
            $.ajax({
                url: '/ShoppingCart/Update',
                data: {
                    carData: JSON.stringify(cartList)
                },
                type: 'POST',
                dataType: 'json',
                success: function (response) {
                    if (response.status) {
                        cart.loadData();
                        console.log('Update Ok !');
                    }
                }

            })
    },
    loadData: function () {
        $.ajax({
            url: '/ShoppingCart/GetAll',
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    let template = $('#templateCart').html();
                    let html = '';
                    let data = res.data;
                    $.each(data, function (i, item) {
                        countItemCart = i + 1;
                        html += Mustache.render(template, {
                            ProductId: item.ProductId,
                            ProductName: item.Product.Name,
                            Image: item.Product.Image,
                            Price: item.Product.Price,
                            PriceF: numeral(item.Product.Price).format('0,0'),
                            Quantity: item.Quantity,
                            Amount: numeral(item.Quantity * item.Product.Price).format('0,0')
                        });

                    });

                    $('#cartBody').html(html);
                    if (html == '') {
                        $('#cartContent').html('<h3>Không có sản phẩm nào trong giỏ hàng !</h3>');
                        $('#divCheckout').hide();
                    }
                    $('#lblTotalOrder').text(numeral(cart.getTotalOrder()).format('0,0'));
                    cart.registerEvents();
                }
            }
        })
    }
}
cart.intit();