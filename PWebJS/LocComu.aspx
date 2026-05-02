<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LocComu.aspx.cs" Inherits="PWebJS.LocComu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid mt-4">
        <!-- Contenedor principal usando Bootstrap (diseño responsivo) -->

        <div class="row">
            <div class="col-sm-12">
                <!-- Columna que ocupa todo el ancho -->

                <div class="card">
                    <!-- Componente visual tipo tarjeta -->

                    <div class="card-header bg-primary text-white">
                        <!-- Encabezado de la tarjeta -->
                        <h5 class="mb-0">Gestión de Comunas</h5>
                        <!-- Título de la funcionalidad -->
                    </div>

                    <div class="card-body">
                        <!-- Cuerpo de la tarjeta -->

                        <div class="row mb-3">
                            <div class="col-sm-2">
                                <!-- Botón para crear nuevo registro -->
                                <button id="btnNuevo" type="button" class="btn btn-sm btn-success">
                                    <i class="fas fa-plus"></i> Nuevo
                                </button>
                                <!-- 
                                Este botón será manejado por JavaScript (LocComu.js).
                                Generalmente abre el modal para crear una nueva comuna.
                                -->
                            </div>
                        </div>

                        <hr />
                        <!-- Línea separadora visual -->

                        <div class="row mt-3">
                            <div class="col-sm-12">

                                <table id="Grid" class="table table-striped table-bordered nowrap" style="width:100%">
                                    <!-- 
                                    Tabla principal donde se listan las comunas.
                                    - id="Grid": usada por JavaScript para cargar datos dinámicamente.
                                    - Clases Bootstrap: estilos visuales.
                                    -->

                                    <thead class="table-dark">
                                        <tr>
                                            <th style="width: 5%">#</th>
                                            <!-- Número o ID del registro -->

                                            <th>Comuna</th>
                                            <!-- Nombre de la comuna -->

                                            <th>Provincia</th>
                                            <!-- Provincia asociada -->

                                            <th>Región</th>
                                            <!-- Región asociada -->

                                            <th style="width: 15%">Acciones</th>
                                            <!-- Botones: editar, eliminar, etc. -->
                                        </tr>
                                    </thead>

                                    <tbody>
                                    </tbody>
                                    <!-- 
                                    El contenido se carga dinámicamente con JavaScript (AJAX).
                                    No hay datos estáticos aquí.
                                    -->

                                </table>

                            </div>
                        </div>
                    </div>

                    <div class="card-footer text-muted">
                        <!-- Pie de la tarjeta -->
                        <small>Total de registros: <span id="totalRegistros">0</span></small>
                        <!-- 
                        Contador dinámico de registros.
                        Se actualiza con JavaScript.
                        -->
                    </div>

                </div>
            </div>
        </div>
    </div>

    <!-- Modal CRUD -->
    <!-- 
    Ventana emergente (modal) para Crear, Leer, Actualizar (CRUD).
    Se usa para ingresar o editar datos sin cambiar de página.
    -->

    <div class="modal fade" id="modalGrid" tabindex="-1" role="dialog" aria-labelledby="modalTitle" aria-hidden="true">
        <!-- 
        Modal Bootstrap:
        - fade: efecto visual.
        - id="modalGrid": usado desde JavaScript para abrir/cerrar.
        -->

        <div class="modal-dialog" role="document">
            <div class="modal-content">

                <div class="modal-header bg-info text-white">
                    <h5 class="modal-title" id="modalTitle">Nueva Comuna</h5>
                    <!-- Título dinámico (crear o editar) -->

                    <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <!-- Botón para cerrar el modal -->
                </div>

                <div class="modal-body">

                    <form id="formComuna">
                        <!-- Formulario para ingresar datos -->

                        <input id="textId" type="hidden" value="0" />
                        <!-- 
                        Campo oculto para almacenar el ID.
                        - 0 = nuevo registro
                        - otro valor = edición
                        -->

                        <div class="form-group">
                            <label for="ComboReg" class="form-label">Región:</label>

                            <select class="form-control form-control-sm" id="ComboReg">
                                <option value="">-- Seleccione Región --</option>
                            </select>
                            <!-- 
                            ComboBox de regiones.
                            Se carga dinámicamente desde la base de datos vía JS/AJAX.
                            -->
                        </div>

                        <div class="form-group">
                            <label for="ComboPro" class="form-label">Provincia:</label>

                            <select class="form-control form-control-sm" id="ComboPro">
                                <option value="">-- Seleccione Provincia --</option>
                            </select>
                            <!-- 
                            Combo dependiente de región.
                            Se llena según la región seleccionada.
                            -->
                        </div>

                        <div class="form-group">
                            <label for="TextNombre" class="form-label">Nombre Comuna:</label>

                            <input type="text" class="form-control form-control-sm" id="TextNombre" placeholder="Ingrese nombre comuna">
                            <!-- Campo de texto para el nombre de la comuna -->
                        </div>

                    </form>

                </div>

                <div class="modal-footer">
                    <!-- Botones del modal -->

                    <button type="button" class="btn btn-sm btn-secondary" data-dismiss="modal">Cerrar</button>
                    <!-- Cierra el modal sin guardar -->

                    <button id="btnGuardarCambios" type="button" class="btn btn-sm btn-primary">Guardar Cambios</button>
                    <!-- 
                    Botón principal del CRUD.
                    Llama a JavaScript para:
                    - Validar datos
                    - Enviar información al backend (AJAX)
                    -->
                </div>

            </div>
        </div>
    </div>

    <script src="<%= ResolveUrl("~/JS/LocComu.js") %>" onerror="console.error('No se pudo cargar JS/LocComu.js');"></script>
    <!-- 
    Archivo JavaScript principal de esta vista.
    Aquí ocurre:
    - Carga de datos (AJAX)
    - Eventos (clicks, selects)
    - Lógica de interacción con el backend (Controlador / WebMethod / API)
    -->

</asp:Content>