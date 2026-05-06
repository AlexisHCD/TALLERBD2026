<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="PWebJS.Clientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--
        Esta página muestra y administra los clientes.
        La mayor parte del comportamiento (cargar datos, guardar, editar, eliminar)
        se hace desde JavaScript (ver JS/Clientes.js) consumiendo métodos WebMethod del code-behind.
    --%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--
        Contenedor principal (Bootstrap): usamos filas/columnas y una "card" para organizar la vista.
        Aquí solo está la estructura HTML; los datos se renderizan dinámicamente en la tabla.
    --%>
    <div class="row">
        <div class="col-sm-12">
            <div class="card">
                <div class="card-header">
                    Informacion sobre Clientes
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-sm-2">
                            <%-- Botón para abrir el modal en modo "nuevo cliente" (lo maneja el JS). --%>
                            <button id="btnNuevo" type="button" class="btn btn-sm btn-success">Nuevo</button>
                        </div>
                    </div>
                    <hr />
                    <div class="row mt-3">
                        <div class="col-sm-12">
                            <%--
                                Tabla donde se listan los clientes.
                                Normalmente se llena vía AJAX y se suele usar con DataTables u otra librería.
                                El <tbody> queda vacío porque se completa desde el script.
                            --%>
                            <table id="Grid" class="table table-striped table-bordered nowrap" style="width: 100%">
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Nombre</th>
                                        <th>Rut</th>
                                        <th>Comuna</th>
                                        <th>Direccion</th>
                                        <th>Telefono</th>
                                        <th>Email</th>
                                        <th>Giro</th>
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

    <%--
        Modal (Bootstrap) para crear/editar un cliente.
        Se reutiliza para ambas acciones: el JS carga los valores y decide si es "ingresar" o "actualizar".
    --%>
    <div class="modal fade" id="modalGrid" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Clientes</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <form>
                        <%--
                            Id oculto del cliente.
                            Se usa para saber si es un registro nuevo (0) o uno existente (id real).
                        --%>
                        <input id="textId" class="model" name="IdP_Cli" value="0" type="hidden" />

                        <%-- Campos de datos básicos del cliente. La clase "model" suele usarse para mapear/leer inputs desde JS. --%>
                        <div class="form-group">
                            <label for="TextNombre" class="col-form-label">Nombre:</label>
                            <input type="text" class="form-control form-control-sm model" id="TextNombre" name="Nombre">
                        </div>

                        <div class="form-group">
                            <label for="TextRut" class="col-form-label">Rut:</label>
                            <input type="text" class="form-control form-control-sm model" id="TextRut" name="Rut">
                        </div>

                        <%--
                            Combos de ubicación (Región -> Provincia -> Comuna).
                            Se llenan de forma dependiente: al elegir región se cargan provincias, y luego comunas.
                        --%>
                        <div class="form-group row">
                            <label class="col-sm-2 col-form-label col-form-label-sm">Region</label>
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
                            <label for="TextDireccion" class="col-form-label">Direccion:</label>
                            <input type="text" class="form-control form-control-sm model" id="TextDireccion" name="Direccion">
                        </div>

                        <div class="form-group">
                            <label for="TextTelefono" class="col-form-label">Telefono:</label>
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
                    </form>
                </div>
                <div class="modal-footer">
                    <%-- Cerrar solo oculta el modal. Guardar dispara el envío (AJAX) desde el script. --%>
                    <button type="button" class="btn btn-sm btn-secondary" data-dismiss="modal">Cerrar</button>
                    <button id="btnGuardarCambios" type="button" class="btn btn-sm btn-primary">Guardar Cambios</button>
                </div>
            </div>
        </div>
    </div>

    <%-- Script específico de esta pantalla. Contiene la lógica de: listar, abrir modal, validar y llamar a WebMethods. --%>
    <script src="JS/Clientes.js"></script>
</asp:Content>
