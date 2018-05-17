var Orders = Orders || {};

(function () {
    var self = this;
    self.SaveOrdersUrl = '';
    self.AddOrderUrl = '';
    self.GetOrdersUrl = '';
    self.ViewModalUrl = '';
    self.Orders = {};
    var viewModel = {};
    var modalVM = {};

    self.Initialize = function () {

        $(document).on('order.add', function (e, article) {
            var order = { Key: article.Id, Value: 1 };
            var orders = [];
            orders.push(order);
            $.ajax({
                url: self.AddOrderUrl,
                contentType: "application/json; charset=utf-8",
                type: "POST",
                data: ko.toJSON(orders),
                success: function (result) {
                    if (result.success) {
                        viewModel.Orders.push(ko.mapping.fromJS(result.data))
                        console.log(result.message);
                    }
                }
            });
        });

        $.getJSON(self.GetOrdersUrl).then(function (orders) {
            self.Orders = orders;
            viewModel = new OrdersVM(orders);
            viewModel.Init();
            ko.applyBindings(viewModel, $('#user_orders_btn')[0]);
        });
    }

    function ModalVM() {
        var inner = this;

    }

    function OrdersVM(orders) {
        var inner = this;

        inner.Orders = ko.mapping.fromJS(orders);

        inner.Init = function () {
            inner.Orders.forEach(function (order) {
                order().addedQuantity = ko.observable(0);

                order.currentQuantity = ko.computed(function () {
                    return order.Quantity() + order.addedQuantity();
                });

                order.Summary = ko.computed(function () {
                    return order.Quantity() * order.Article.Price();
                })

                order.addItem = function () {
                    order.addedQuantity(order.addedQuantity() + 1);
                }

                order.removeItem = function () {
                    if (order.Quantity() + order.addedQuantity() > 0) {
                        order.addedQuantity(order.addedQuantity() - 1);
                    }
                }
            });
        }

        

        inner.Display = ko.computed(function () {
            var display = 0;
            inner.Orders().forEach(function (order) {
                display += order.Summary();
            });
            return display === 0 ? 'Empty' : display;
        });

        inner.OpenModal = function () {
            var modalArr = $('#orders_modal');
            if (modalArr.length === 0) {
                $.ajax({
                    url: self.ViewModalUrl,
                    cache: false,
                }).done(function (html) {
                    $('#orders_modal_container').html(html);
                    });
                //ko.applyBindings(inner, $('#orders_modal_container')[0]);
            }
            $('#orders_modal').modal('show');
        }

        inner.Save = function () {
            var ordersToSave = [];
            inner.Orders().forEach(function (order) {
                if (order.addedQuantity !== 0) {
                    ordersToSave.push({Key: order.Article.Id, Value: order.Quantity});
                }
            });
            $.ajax({
                url: self.SaveOrdersUrl,
                contentType: "application/json; charset=utf-8",
                type: "POST",
                data: ko.toJSON(ordersToSave)
            }).then(function (request) {
                let orderToRemove = [];
                inner.Orders().forEach(function (order) {
                    var q = order.Quantity(order.Quantity() + order.addedQuantity());
                    if (q === 0) {
                        orderToRemove.push(order);
                    }
                    else {
                        order.Quantity(q);
                    }
                    orderToRemove.forEach(function (o) {
                        inner.Orders().splice(inner.Orders().indexOf(o), 1);
                    })
                });

                $('#orders_modal').modal('show');
            });
        }
    }
}).apply(Orders);