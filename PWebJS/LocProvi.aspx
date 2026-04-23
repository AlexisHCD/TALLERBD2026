<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LocProvi.aspx.cs" Inherits="PWebJS.LocProvi" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid mt-4">
        <div class="row">
            <div class="col-sm-12">
                <div class="card">
                    <div class="card-header bg-primary text-white">
                        <h5 class="mb-0">Gestión de Provincias</h5>
                    </div>
                    <div class="card-body">
                        <div class="row mb-3">
                            <div class="col-sm-2">
                                <button id="btnNuevo" type="button" class="btn btn-sm btn-success">
                                    <i class="fas fa-plus"></i> Nuevo
                                </button>
                            </div>
                        </div>
                        <hr />
                        <div class="row mt-3">
                            <div class="col-sm-12">
                                <table id="Grid" class="table table-striped table-bordered nowrap" style="width:100%">
                                    <thead class="table-dark">
                                        <tr>
                                            <th style="width: 5%">#</th>
                                            <th>Provincia</th>
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

    <!-- Modal CRUD -->
    <div class="modal fade" id="modalGrid" tabindex="-1" role="dialog" aria-labelledby="modalTitle" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header bg-info text-white">
                    <h5 class="modal-title" id="modalTitle">Nueva Provincia</h5>
                    <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <form id="formProvincia">
                        <input id="textId" type="hidden" value="0" />
                        <div class="form-group">
                            <label for="ComboReg" class="form-label">Región:</label>
                            <select class="form-control form-control-sm" id="ComboReg">
                                <option value="">-- Seleccione Región --</option>
                            </select>
                        </div>
                        <div class="form-group">
                            <label for="TextNombre" class="form-label">Nombre Provincia:</label>
                            <input type="text" class="form-control form-control-sm" id="TextNombre" placeholder="Ingrese nombre provincia">
                        </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-sm btn-secondary" data-dismiss="modal">Cerrar</button>
                    <button id="btnGuardarCambios" type="button" class="btn btn-sm btn-primary">Guardar Cambios</button>
                </div>
            </div>
        </div>
    </div>

    <script src="JS/LocProvi.js"></script>
</asp:Content>
