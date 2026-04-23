<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Proveedores.aspx.cs" Inherits="PWebJS.Proveedores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="card">
                <div class="card-header">
                    Información sobre Proveedores
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-sm-2">
                            <button id="btnNuevo" type="button" class="btn btn-sm btn-success">Nuevo</button>
                        </div>
                    </div>
                    <hr />
                    <div class="row mt-3">
                        <div class="col-sm-12">
                            <table id="Grid" class="table table-striped table-bordered nowrap" style="width: 100%">
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Nombre</th>
                                        <th>Rut</th>
                                        <th>Comuna</th>
                                        <th>Dirección</th>
                                        <th>Teléfono</th>
                                        <th>Email</th>
                                        <th>Giro</th>
                                        <th>Descripción</th>
                                        <th>Acciones</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="card-footer">
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalGrid" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Proveedores</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <form>
                        <input id="textId" class="model" name="IdProv" value="0" type="hidden" />

                        <div class="form-group">
                            <label for="TextNombre" class="col-form-label">Nombre:</label>
                            <input type="text" class="form-control form-control-sm model" id="TextNombre" name="Nombre">
                        </div>

                        <div class="form-group">
                            <label for="TextRut" class="col-form-label">Rut:</label>
                            <input type="text" class="form-control form-control-sm model" id="TextRut" name="Rut">
                        </div>

                        <div class="form-group row">
                            <label class="col-sm-2 col-form-label col-form-label-sm">Región</label>
                            <div class="col-sm-10">
                                <select class="form-control form-control-sm model" id="ComboReg" name="Region"></select>
                            </div>
                        </div>

                        <div class="form-group row">
                            <label class="col-sm-2 col-form-label col-form-label-sm">Provincia</label>
                            <div class="col-sm-10">
                                <select class="form-control form-control-sm model" id="ComboPro" name="Provincia"></select>
                            </div>
                        </div>

                        <div class="form-group row">
                            <label class="col-sm-2 col-form-label col-form-label-sm">Comuna</label>
                            <div class="col-sm-10">
                                <select class="form-control form-control-sm model" id="ComboCom" name="Comuna"></select>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="TextDireccion" class="col-form-label">Dirección:</label>
                            <input type="text" class="form-control form-control-sm model" id="TextDireccion" name="Direccion">
                        </div>

                        <div class="form-group">
                            <label for="TextTelefono" class="col-form-label">Teléfono:</label>
                            <input type="text" class="form-control form-control-sm model" id="TextTelefono" name="Tel">
                        </div>

                        <div class="form-group">
                            <label for="TextEmail" class="col-form-label">Email:</label>
                            <input type="text" class="form-control form-control-sm model" id="TextEmail" name="Email">
                        </div>

                        <div class="form-group">
                            <label for="TextGiro" class="col-form-label">Giro:</label>
                            <input type="text" class="form-control form-control-sm model" id="TextGiro" name="Giro">
                        </div>

                        <div class="form-group">
                            <label for="TextDescr" class="col-form-label">Descripción:</label>
                            <input type="text" class="form-control form-control-sm model" id="TextDescr" name="Descr">
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

    <script src="JS/Proveedores.js"></script>
</asp:Content>
