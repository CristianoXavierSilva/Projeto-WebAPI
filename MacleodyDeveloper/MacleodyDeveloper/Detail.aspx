<%@ Page Title="Detail" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="MacleodyDeveloper.Detail" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="col-md-4">
        <div class="panel panel-default">
          <div class="panel-heading">
            <h2 class="panel-title">Detalhes</h2>
          </div>
          <table class="table">
            <tr><td>ID</td><td data-bind="text: detail().cliente_id"></td></tr>
            <tr><td>Nome</td><td data-bind="text: detail().nome"></td></tr>
            <tr><td>E-mail</td><td data-bind="text: detail().email"></td></tr>
            <tr><td>Data de Cadastro</td><td data-bind="text: detail().dataCadastro"></td></tr>
          </table>
        </div>
    </div>

</asp:Content>