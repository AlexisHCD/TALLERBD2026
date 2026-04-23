<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LocComu.aspx.cs" Inherits="PWebJS.LocComu" %>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
            <div class="col-sm-12">
               <div class="card">
                  <div class="card-header">
                    Información sobre Comunas
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
                                <table id="Grid" class="table table-striped table-bordered nowrap" style="width:100%">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Comuna</th>
                                            <th>Provincia</th>
                                            <th>Región</th>
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
    <!-- Modal -->
    <div class="modal fade" id="modalGrid" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
      <div class="modal-dialog" role="document">
      <%--<div class="modal-dialog modal-dialog-centered" role="document">--%>
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">Comunas</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <form>
               <div class="form-group row">
                <label for="inputPassword" class="col-sm-2 col-form-label col-form-label-sm">Región</label>
                <div class="col-sm-10">
                    <select class="form-control form-control-sm model" id="ComboIngModReg" name="Region">
                    </select>
                </div>
              </div>
               <div class="form-group row">
                <label for="inputPassword" class="col-sm-2 col-form-label col-form-label-sm">Provincia</label>
                <div class="col-sm-10">
                    <select class="form-control form-control-sm model" id="ComboIngModPro" name="Provincia">
                    </select>
                </div>
              </div>
              <input id="textId" class="model" name="IdCom" value="0" type="hidden" />
              <div class="form-group">
                <label for="TextIngMod" class="col-form-label">Nombre:</label>
                <input type="text" class="form-control form-control-sm model" id="TextIngMod" name="Nombre">
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
    <script src="JS/LocComu.js"></script>
</asp:Content>
