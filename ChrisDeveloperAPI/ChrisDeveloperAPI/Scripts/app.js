var ClienteView = function () {
    var self = this;
    self.clientes = ko.observableArray();
    self.error = ko.observable();

    var clientesURI = '/api/clientes/';

    function ajaxHelper(uri, method, data) {
        self.error('Erro na solicitação de serviço...'); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAllClientes() {
        ajaxHelper(clientesURI, 'GET').done(function (data) {
            self.clientes(data);
        });
    }

    getAllClientes();
};

ko.applyBindings(new ClienteView());