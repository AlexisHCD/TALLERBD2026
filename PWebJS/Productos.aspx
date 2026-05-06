<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="PWebJS.Productos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--
        Página de mantenimiento (CRUD) de Productos.
        La vista muestra una tabla con productos y un modal para crear/editar.
        La lógica de interacción (listar, abrir modal, guardar, eliminar) se hace desde JS/Productos.js
        consumiendo WebMethods del code-behind (Productos.aspx.cs).
    --%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- Contenedor principal usando Bootstrap (card + grilla). --%>
    <div class="row">
        <div class="col-sm-12">
            <div class="card">
                <div class="card-header">
                    Informacion sobre Productos
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-sm-2">
                            <%-- Botón para abrir el modal en modo "nuevo producto" (lo maneja el JS). --%>
                            <button id="btnNuevo" type="button" class="btn btn-sm btn-success">Nuevo</button>
                        </div>
                    </div>
                    <hr />
                    <div class="row mt-3">
                        <div class="col-sm-12">
                            <%--
                                Tabla donde se listan los productos.
                                El contenido del <tbody> se carga dinámicamente con JavaScript (AJAX).
                            --%>
                            <table id="Grid" class="table table-striped table-bordered nowrap" style="width: 100%">
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Nombre</th>
                                        <th>Fecha de incorporacion</th>
                                        <th>Cantidad inicial</th>
                                        <th>Cantidad actual</th>
                                        <th>Cantidad arrendada</th>
                                        <th>Total actual</th>
                                        <th>Valor arriendo</th>
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
        Modal (Bootstrap) para crear/editar un producto.
        Se reutiliza: el JS decide si corresponde a ingresar o actualizar según el Id.
    --%>
    <div class="modal fade" id="modalGrid" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Productos</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <form>
                        <%--
                            Id oculto del producto:
                            - 0 para nuevo
                            - id real para edición
                        --%>
                        <input id="textId" class="model" name="IdProd" value="0" type="hidden" />

                        <%-- Campos del producto. La clase "model" normalmente se usa para mapear los inputs desde JS. --%>
                        <div class="form-group">
                            <label for="TextNombre" class="col-form-label">Nombre:</label>
                            <input type="text" class="form-control form-control-sm model" id="TextNombre" name="Nombre">
                        </div>

                        <div class="form-group">
                            <label for="TextFInc" class="col-form-label">Fecha de incorporacion:</label>
                            <input type="text" class="form-control form-control-sm model" id="TextFInc" name="FInc">
                        </div>

                        <div class="form-group">
                            <label for="TextCInc" class="col-form-label">Cantidad inicial:</label>
                            <input type="text" class="form-control form-control-sm model" id="TextCInc" name="CInc">
                        </div>

                        <div class="form-group">
                            <label for="TextCAct" class="col-form-label">Cantidad actual:</label>
                            <input type="text" class="form-control form-control-sm model" id="TextCAct" name="CAct">
                        </div>

                        <div class="form-group">
                            <label for="TextCArr" class="col-form-label">Cantidad arrendada:</label>
                            <input type="text" class="form-control form-control-sm model" id="TextCArr" name="CArr">
                        </div>

                        <div class="form-group">
                            <label for="TextTAct" class="col-form-label">Total actual:</label>
                            <input type="text" class="form-control form-control-sm model" id="TextTAct" name="TAct">
                        </div>

                        <div class="form-group">
                            <label for="TextVArr" class="col-form-label">Valor del arriendo:</label>
                            <input type="text" class="form-control form-control-sm model" id="TextVArr" name="VArr">
                        </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <%-- Cerrar solo oculta el modal. Guardar dispara la llamada AJAX desde Productos.js. --%>
                    <button type="button" class="btn btn-sm btn-secondary" data-dismiss="modal">Cerrar</button>
                    <button id="btnGuardarCambios" type="button" class="btn btn-sm btn-primary">Guardar Cambios</button>
                </div>
            </div>
        </div>
    </div>

    <%-- Script específico de esta pantalla (grilla, eventos, CRUD, llamadas a WebMethods). --%>
    <script src="JS/Productos.js"></script>
</asp:Content>
