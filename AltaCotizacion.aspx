<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AltaCotizacion.aspx.cs" Inherits="PedidosWebForm.AltaCotizacion" MasterPageFile="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Alta de Cotizaciòn
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Referencias a jQuery y Bootstrap -->
    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>

    <div class="container p-4" style="width: 100%; max-width: inherit; padding: 20px; background-color: #f8f9fa; border: 1px solid #dee2e6; border-radius: 8px;">
        <!-- Primera fila con todos los campos y botón Buscar alineados en una línea -->
        <asp:UpdatePanel ID="UpdatePanelCliente" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="d-flex align-items-end mb-4 gap-3">
                    <!-- Campo Tipo Documento -->
                    <div style="width: 250px;">
                        <label for="txtTipoDocumento" class="form-label fw-semibold text-secondary">Tipo Documento</label>
                        <asp:TextBox ID="txtTipoDocumento" runat="server" CssClass="form-control text-primary shadow-sm" />
                    </div>

                    <!-- Campo Código Cliente -->
                    <div style="width: 120px;">
                        <label for="txtCodCliente" class="form-label fw-semibold text-secondary">Código</label>
                        <asp:TextBox ID="txtCodCliente" runat="server" CssClass="form-control shadow-sm" MaxLength="10" AutoPostBack="true" OnTextChanged="txtCodCliente_TextChanged" onkeypress="return validateNumericInput(event)" />
                    </div>

                    <!-- Campo Razón Social -->
                    <div style="width: 220px;">
                        <label for="txtRazonSocial" class="form-label fw-semibold text-secondary">Razón Social</label>
                        <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="form-control shadow-sm fw-bold text-primary" MaxLength="200" />
                    </div>

                    <!-- Campo Dirección -->
                    <div style="width: 220px;">
                        <label for="txtDireccion" class="form-label fw-semibold text-secondary">Dirección</label>
                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control shadow-sm fw-bold text-primary" MaxLength="200" />
                    </div>

                    <!-- Campo CUIT -->
                    <div style="width: 130px;">
                        <label for="txtCUIT" class="form-label fw-semibold text-secondary">CUIT</label>
                        <asp:TextBox ID="txtCUIT" runat="server" CssClass="form-control shadow-sm" />
                    </div>

                    <!-- Campo Estado -->
                    <div style="width: 150px;">
                        <label for="ddlEstado" class="form-label fw-semibold text-secondary">Estado</label>
                        <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control shadow-sm" />
                    </div>

                    <!-- Campo Fecha Pedido -->
                    <div style="width: 130px;">
                        <label for="txtFechaPedido" class="form-label fw-semibold text-secondary">Fecha</label>
                        <asp:TextBox ID="txtFechaPedido" runat="server" CssClass="form-control shadow-sm" placeholder="Seleccione una fecha" style="width: 110px; border-color: #6c757d;" />
                    </div>

                    <!-- Botón Buscar alineado al final, con el script para abrir el modal -->
                    <div class="d-flex align-items-end">
                        <asp:Button ID="btnBuscarCliente" runat="server" Text="Buscar" CssClass="btn btn-outline-primary shadow-sm" OnClientClick="$('#clientesModal').modal('show'); return false;" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>




             <!-- agreuge -->

         <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
     <ContentTemplate>
         <div class="d-flex align-items-end mb-4 gap-3">
             <!-- Campo Tipo Documento -->

              <div style="width: 300px;">
                 <label for="txtPciaEntrega" class="form-label fw-semibold text-secondary">Provincia</label>
                 <asp:TextBox ID="TextPciaEntrega" runat="server" CssClass="form-control text-primary shadow-sm" />
             </div>


             <div style="width: 300px;">
                 <label for="txtlocalidEntrega" class="form-label fw-semibold text-secondary">Localidad</label>
                 <asp:TextBox ID="TextIdLocalidadEntrega" runat="server" CssClass="form-control text-primary shadow-sm" />
             </div>

             <!-- Campo Código Cliente -->
             <div style="width: 300px;">
                 <label for="txtDireccionEntreg" class="form-label fw-semibold text-secondary">Direccion de Entrega</label>
                 <asp:TextBox ID="txtDireccionEntrega" runat="server" CssClass="form-control shadow-sm" MaxLength="100" AutoPostBack="true" OnTextChanged="txtCodCliente_TextChanged" onkeypress="return validateNumericInput(event)" />
             </div>

             
             <!-- Campo CUIT -->
             <div style="width: 200px;">
                 <label for="txtcontac" class="form-label fw-semibold text-secondary">Contacto</label>
                 <asp:TextBox ID="TextContacto" runat="server" CssClass="form-control shadow-sm" />
             </div>

           
         </div>
     </ContentTemplate>
 </asp:UpdatePanel>


         <!-- agreuge -->























        <!-- Modal de búsqueda de clientes -->
        <div class="modal fade" id="clientesModal" tabindex="-1" aria-labelledby="clienteModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title fw-bold text-primary" id="clienteModalLabel">Buscar Cliente</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanelClientes" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-body">
                                <asp:TextBox ID="txtBuscarRazonSocial" runat="server" CssClass="form-control shadow-sm" Placeholder="Razón Social" style="border-color: #6c757d;" />
                                <asp:Button ID="btnFiltrarClientes" runat="server" Text="Buscar Cliente" CssClass="btn btn-outline-primary w-100 mt-3 shadow-sm" OnClick="btnFiltrarClientes_Click" UseSubmitBehavior="false" />
                                <asp:GridView ID="gvClientes" runat="server" CssClass="table table-hover table-bordered mt-3" AutoGenerateColumns="False" OnRowCommand="gvClientes_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="nu_cli_codigo" HeaderText="Código Cliente" />
                                        <asp:BoundField DataField="DS_CLI_RAZON_SOCIAL" HeaderText="Razón Social" />
                                        <asp:BoundField DataField="ds_cli_direccion" HeaderText="Dirección" />
                                        <asp:BoundField DataField="ds_cli_cuit" HeaderText="CUIT" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnSeleccionar" runat="server" CommandName="Select" CommandArgument='<%# Container.DataItemIndex %>' Text="Seleccionar" CssClass="btn btn-outline-primary btn-sm" UseSubmitBehavior="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnFiltrarClientes" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

        <!-- Panel para agregar artículos -->
        <div class="p-4 mt-4" style="background-color: #ffffff; border: 1px solid #dee2e6; border-radius: 8px;">
            <asp:UpdatePanel ID="UpdatePanelArticulos" runat="server">
                <ContentTemplate>
                    <div class="form-horizontal">
                        <div class="row g-3">
                            <div class="col-md-4">
                                <label for="descripcion" class="form-label fw-semibold text-secondary">Descripción</label>
                                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control shadow-sm" MaxLength="200" style="border-color: #6c757d;" />
                            </div>
                            <div class="col-md-4">
                                <label for="detalle" class="form-label fw-semibold text-secondary">Detalle</label>
                                <asp:TextBox ID="txtdetalle" runat="server" CssClass="form-control shadow-sm" MaxLength="200" style="border-color: #6c757d;" />
                            </div>
                            <div class="col-md-4 d-flex align-items-end">
                                <div class="me-3">
                                    <label for="cantidad" class="form-label fw-semibold text-secondary">Cantidad</label>
                                    <asp:TextBox ID="txtCantidad" runat="server" AutoPostBack="True" OnTextChanged="txtCantidad_Changed" CssClass="form-control shadow-sm" style="border-color: #6c757d;" />
                                </div>
                                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-outline-primary shadow-sm" OnClick="btnAgregar_Click" OnClientClick="return validarCampos();" />
                            </div>
                        </div>

                        <div class="row g-3 mt-2">
                            <!-- Campo Precio Unitario -->
                            <div class="col-md-4">
                                <label for="precio_unitario" class="form-label fw-semibold text-secondary">Precio Unitario</label>
                                <asp:TextBox ID="txtPrecioUnitario" runat="server" AutoPostBack="True" CssClass="form-control shadow-sm" MaxLength="10" OnTextChanged="txtpreciounitario_Changed" style="border-color: #6c757d;" />
                            </div>

                            <!-- Campo Unidad -->
                            <div class="col-md-4">
                                <label for="ddlunidad" class="form-label fw-semibold text-secondary">Unidad</label>
                                <asp:DropDownList ID="ddlunidad" runat="server" CssClass="form-control shadow-sm" style="width: 100px; border-color: #6c757d;" />
                            </div>

                            <!-- Campo Precio Total alineado a la derecha -->
                            <div class="col-md-4">
                                <label for="precio_total" class="form-label fw-semibold text-secondary">Precio Total</label>
                                <asp:TextBox ID="txtPrecioTotal" runat="server" CssClass="form-control shadow-sm" MaxLength="10" ReadOnly="True" style="border-color: #6c757d;" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <!-- Grilla de artículos -->
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="table-responsive mt-3">
                    <asp:GridView ID="gvArticulos" runat="server" CssClass="table table-hover table-bordered" AutoGenerateColumns="False" AllowPaging="True" PageSize="4" OnPageIndexChanging="gvArticulos_PageIndexChanging" OnRowCommand="gvArticulos_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                            <asp:BoundField DataField="Detalle" HeaderText="Detalle" />
                            <asp:BoundField DataField="Unidad" HeaderText="Unidad" />
                            <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" />
                            <asp:BoundField DataField="PrecioTotal" HeaderText="Precio Total" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnBorrar" runat="server" Text="Borrar" CssClass="btn btn-outline-danger btn-sm" CommandName="DeleteRow" CommandArgument='<%# Container.DataItemIndex %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <!-- Panel de sumas y botón Aceptar -->
        <asp:UpdatePanel ID="UpdatePanelSumas" runat="server">
            <ContentTemplate>
                <div class="mt-4">
                    <asp:GridView ID="gvSumas" runat="server" CssClass="table table-bordered table-striped text-end" AutoGenerateColumns="False" GridLines="None">
                        <Columns>
                            <asp:BoundField HeaderText="CANTIDADES" DataField="CantidadTotal" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField HeaderText="SUBTOTAL" DataField="Subtotal" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField HeaderText="TOTAL" DataField="Total" ItemStyle-HorizontalAlign="Right">
                                <ItemStyle CssClass="fw-bold text-primary" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <div class="text-end mt-2">
                        <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-outline-primary shadow-sm" OnClick="btnAceptar_Click" OnClientClick="return validarConfirmarPedido();" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <style>
        /* Estilos adicionales */
        .fw-semibold {
            font-weight: 600;
        }
        .text-secondary {
            color: #6c757d !important;
        }
    </style>


    <style>
    body {
        background-image: url('images/fondo.jpg');
        background-size: cover;
        background-repeat: no-repeat;
        background-attachment: fixed;
        background-position: center;
    }
</style>



</asp:Content>
