<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MacleodyDeveloper._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>ASP.NET</h1>
        <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.</p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h2 class="panel-title">Clientes</h2>
                </div>
                <div class="panel-body">
                    <ul class="list-unstyled" data-bind="foreach: clientes">
                        <li>
                        <span data-bind="text: cliente_id"></span> : <strong><span data-bind="text: nome"></span></strong>
                        <small class="navbar-right"><a runat="server" href="~/Detail" data-bind="click: $parent.getClientesDetail">Detalhes</a></small>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="alert alert-danger" data-bind="visible: error"><p data-bind="text: error"></p></div>
        </div>
        <div class="col-md-4">
            <h2>Get more libraries</h2>
            <p>
                NuGet is a free Visual Studio extension that makes it easy to add, remove, and update libraries and tools in Visual Studio projects.
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301949">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Web Hosting</h2>
            <p>
                You can easily find a web hosting company that offers the right mix of features and price for your applications.
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301950">Learn more &raquo;</a>
            </p>
        </div>
    </div>

    <script src="Scripts/knockout-3.4.2.js"></script>
    <script src="Scripts/app.js"></script>
</asp:Content>
