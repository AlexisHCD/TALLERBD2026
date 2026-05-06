<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LocRegi.aspx.cs" Inherits="PWebJS.LocRegi" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--
        Página de mantenimiento (CRUD) de Regiones.
        Muestra una tabla con las regiones y un modal para crear/editar.
        El flujo (cargar grilla, abrir modal, guardar) se realiza con JavaScript (JS/LocRegi.js)
        que consume WebMethods definidos en el code-behind.
    --%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- Contenedor principal con Bootstrap. --%>
    <div class="container-fluid mt-4">
        <div class="row">
            <div class="col-sm-12">
                <div class="card">
                    <div class="card-header bg-primary text-white">
                        <h5 class="mb-0">Gestión de Regiones</h5>
                    </div>
                    <div class="card-body">
                        <div class="row mb-3">
                            <div class="col-sm-2">
                                <%-- Botón para abrir el modal en modo "nueva región" (manejado por JS). --%>
                                <button id="btnNuevo" type="button" class="btn btn-sm btn-success">
                                    <i class="fas fa-plus"></i> Nuevo
                                </button>
                            </div>
                        </div>
                        <hr />
                        <div class="row mt-3">
                            <div class="col-sm-12">
                                <%--
                                    Tabla donde se listan las regiones.
                                    El <tbody> se llena dinámicamente (AJAX) desde JS/LocRegi.js.
                                --%>
                                <table id="Grid" class="table table-striped table-bordered nowrap" style="width:100%">
                                    <thead class="table-dark">
                                        <tr>
                                            <th style="width: 5%">#</th>
                                            <th>Región</th>
                                            <th style="width: 15%">Acciones</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer text-muted">
                        <small>Total de registros: <span id="totalRegistros">0</span></small>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%-- Modal (Bootstrap) para crear/editar regiones. --%>
    <div class="modal fade" id="modalGrid" tabindex="-1" role="dialog" aria-labelledby="modalTitle" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header bg-info text-white">
                    <h5 class="modal-title" id="modalTitle">Nueva Región</h5>
                    <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <form id="formRegion">
                        <%--
                            Id oculto:
                            - 0 significa "nuevo"
                            - otro valor significa "editar"
                        --%>
                        <input id="textId" type="hidden" value="0" />
                        <div class="form-group">
                            <label for="TextNombre" class="form-label">Nombre Región:</label>
                            <input type="text" class="form-control form-control-sm" id="TextNombre" placeholder="Ingrese nombre región">
                        </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <%-- Cerrar solo oculta el modal. Guardar dispara la llamada AJAX desde el JS. --%>
                    <button type="button" class="btn btn-sm btn-secondary" data-dismiss="modal">Cerrar</button>
                    <button id="btnGuardarCambios" type="button" class="btn btn-sm btn-primary">Guardar Cambios</button>
                </div>
            </div>
        </div>
    </div>

    <%-- Contenedor de depuración/errores para mostrar mensajes desde JS si algo falla. --%>
    <div id="debugRegion" class="alert alert-danger mt-2" style="display:none;"></div>

    <%-- Script de prueba: deja una marca en consola para confirmar que la página cargó. --%>
    <script>
        console.log('LocRegi.aspx loaded');
    </script>
    <%-- Script principal con la lógica de la pantalla (carga grilla, CRUD, eventos). --%>
    <script src="<%= ResolveUrl("~/JS/LocRegi.js") %>" onerror="console.error('No se pudo cargar JS/LocRegi.js');"></script>
</asp:Content>
