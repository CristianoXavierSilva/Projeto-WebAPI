/* $(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = 'https://connect.facebook.net/pt_BR/sdk.js#xfbml=1&version=v2.12&appId=360523434355908&autoLogAppEvents=1';
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk')); */

 var ViewModel = function () {
    var self = this;
    self.clientes = ko.observableArray();
    self.error = ko.observable();
    self.detail = ko.observable();

    var clientesURI = '/api/clientes/';

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
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

    function getClientesDetail(item) {
        ajaxHelper(clientesURI + item.Id, 'GET').done(function (data) {
            self.detail(data);
        });
    }

    getAllClientes();
};

ko.applyBindings(new ViewModel());

/* angular.module("listar-clientes-produtos", []).controller("cli-prod-controller", function ($scope, $http) { 

    var carregarClientes = function () {

        $http({
            method: 'GET',
            url: 'api/clientes'
        }).then(function (response) {

            $scope.cliente = response.data.cliente;

        }, function (response) {
            console.error(response);
        });
    };

    carregarClientes();
}); */