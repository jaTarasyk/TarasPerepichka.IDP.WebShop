var Articles = Articles || {};

(function () {
    var self = this;
    var viewModel = {};
    self.GetArticlesUrl = '';

    self.Initialize = function (authenticated) {
        $.getJSON(self.GetArticlesUrl).then(function (result) {
            viewModel = new ArticlesVM(result.data, authenticated);
            ko.applyBindings(viewModel, $('#articles_list')[0]);
        })
    }

    function ArticlesVM(articles, authenticated) {
        var inner = this;

        inner.Rows = [];
        inner.Authenticated = ko.observable(authenticated);

        inner.AddItem = function (item) {
            $(document).trigger('order.add', item);
        }

        for (var i = 0; i < articles.length; i+=3) {
            inner.Rows.push(articles.slice(i, i + 3));
        }
    }
}).apply(Articles);