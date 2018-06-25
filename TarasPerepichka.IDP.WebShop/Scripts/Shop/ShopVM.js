var Shop = Shop || {};

(function () {
    var self = this;
    self.GetProductListUrl = '';
    self.GetShoppingCartUrl = '';
    self.AddProductToCartUrl = '';
    self.ViewModel = {};
    self.Viewport = {};

    function ShopVM(authenticated) {
        var inner = this;
        inner.Authenticated = authenticated;
        inner.Products = ko.observableArray([]);
        inner.ShoppingCart = ko.observableArray([]);
        inner.RowLegth = ko.observable();

        inner.GetProducts = function () {
            $.getJSON(self.GetProductListUrl)
                .then(function (result) {
                    result.data.forEach(function (product, index) {
                        product.AddClearFix = ko.computed(function () {
                            var add = (index + 1) % inner.RowLegth() === 0;
                            console.log(index, inner.RowLegth(), add);
                            return add;
                        });
                    });
                    inner.Products(result.data);
                });
        }

        inner.AddProductToCart = function (product) {
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

        inner.GetShoppingCart = function ()
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

                inner.ShoppingCart(orders());
            });        
        }

        inner.CartTotal = ko.computed(function () {
            var total = 0;
            inner.ShoppingCart().forEach(function (order) {
                total += order.Summary();
            });
            return total === 0 ? 'Empty' : total;
        });

        inner.SaveShoppingCart = function () {
            $.ajax({
                url: self.SaveShoppingCartUrl,
                contentType: "application/json; charset=utf-8",
                type: "POST",
                data: ko.toJSON(inner.ShoppingCart),
            }).then(function () {
                $('#orders_modal').modal('hide');
            })
        }
    }

    self.Init = function (authenticated) {
        self.ViewModel = new ShopVM(authenticated);

        $('#orders_modal').off('hidden.bs.modal').on('hidden.bs.modal', function () {
            self.ViewModel.ShoppingCart.splice(0, self.ViewModel.ShoppingCart().length);
        });

        $('#orders_modal').off('show.bs.modal').on('show.bs.modal', function () {
            self.ViewModel.GetShoppingCart();
        });

        self.Viewport = ResponsiveBootstrapToolkit;

        //debugger;
        if (self.Viewport.is('<=sm')) { self.ViewModel.RowLegth(self.DisplaySize.SMALL); }
        else if (self.Viewport.is('>md')) { self.ViewModel.RowLegth(self.DisplaySize.LARGE); }
        else { self.ViewModel.RowLegth(self.DisplaySize.MEDIUM); }

        $(window).resize(self.Viewport.changed(function () {
            if (self.Viewport.is('<=sm')) { self.ViewModel.RowLegth(self.DisplaySize.SMALL); }
            else if (self.Viewport.is('>md')) { self.ViewModel.RowLegth(self.DisplaySize.LARGE); }
            else { self.ViewModel.RowLegth(self.DisplaySize.MEDIUM); }
        }));
        //$(window).trigger('resize');
        self.ViewModel.GetProducts();
        ko.applyBindings(self.ViewModel, $('#shopping_cart')[0]);
    }
    
    self.DisplaySize = {
        SMALL: 2,
        MEDIUM: 3,
        LARGE: 4
    }
}).apply(Shop);