var ShopVM = ShopVM || {};

(function () {
    var self = this;
    self.GetProductListUrl = '';
    self.GetShoppingCartUrl = '';
    self.AddProductToCartUrl = '';
    self.Authenticated = false;
    self.Products = ko.observableArray([]);
    self.ShoppingCart = ko.observableArray([]);
    
    self.Init = function (authenticated) {
        self.Authenticated = authenticated;
        $('#orders_modal').off('hidden.bs.modal').on('hidden.bs.modal', function () {
            self.ShoppingCart.splice(0, self.ShoppingCart().length);
        });

        $('#orders_modal').off('show.bs.modal').on('show.bs.modal', function () {
            self.GetShoppingCart();
        });
        
        self.GetProductList();
        ko.applyBindings(self, $('#shopping_cart')[0]);
    }

    self.ProductsInRows = ko.computed(function () {
        var rows = [];
        for (i = 0; i < self.Products().length; i += 4) {
            rows.push(self.Products.slice(i, i + 4));
        };
        return rows;
    });

    self.GetProductList = function () {
        $.getJSON(self.GetProductListUrl).then(function (result) {
            self.Products(result.data);
        });
    }

    self.AddProductToCart = function (product) {
        $.ajax({
            url: self.AddProductToCartUrl,
            contentType: "application/json; charset=utf-8",
            type: "POST",
            data: ko.toJSON({ articleId: product.Id }),
            success: function (result) {
                if (result.success) {
                    console.log(result.message);
                }
            }
        });
    }

    self.GetShoppingCart = function()
    {
        $.getJSON(self.GetShoppingCartUrl).then(function (result) {
            var orders = ko.mapping.fromJS(result);
            orders().forEach(function (order) {
                order.Summary = ko.computed(function () {
                    return order.Quantity() * order.Article.Price();
                })

                order.addItem = function () {
                    order.Quantity(order.Quantity() + 1);
                }

                order.removeItem = function () {
                    if (order.Quantity() > 1) {
                        order.Quantity(order.Quantity() - 1);
                    }
                }

                order.CartRemoveProd = function () {
                    order.Quantity(0);
                }
            });

            self.ShoppingCart(orders());
        });        
    }

    self.CartTotal = ko.computed(function () {
        var total = 0;
        self.ShoppingCart().forEach(function (order) {
            total += order.Summary();
        });
        return total === 0 ? 'Empty' : total;
    });

    self.SaveShoppingCart = function () {
        $.ajax({
            url: self.SaveShoppingCartUrl,
            contentType: "application/json; charset=utf-8",
            type: "POST",
            data: ko.toJSON(self.ShoppingCart),
        }).then(function () {
            $('#orders_modal').modal('hide');
        })
    }
}).apply(ShopVM);