<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AltaCotizacion.aspx.cs" Inherits="PedidosWebForm.AltaCotizacion" MasterPageFile="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Alta de Cotizaciòn
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Estilos y scripts -->
    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script type="text/javascript">
        Sys.Application.add_load(function () {
            $('.select2-autocompletar').select2({
                placeholder: 'Seleccioná una opción...',
                allowClear: true,
                width: '100%'
            });
        });
    </script>

    <style>
        .titulo-seccion {
            font-size: 1.25rem;
            color: #0d6efd;
            font-weight: bold;
            margin-bottom: 15px;
        }

        .bg-section {
            background: linear-gradient(145deg, #ffffff, #e0e6ed);
            border-radius: 20px;
            padding: 20px;
            margin-bottom: 30px;
            animation: fadeInUp 0.6s ease-in-out;
        }

        .card {
            border-radius: 12px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.05);
            margin-bottom: 20px;
            transition: transform 0.3s ease-in-out;
        }

            .card:hover {
                transform: scale(1.01);
            }
    </style>

    <asp:UpdatePanel ID="MainUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <!-- Historial del Cliente -->
            <asp:UpdatePanel ID="UpdatePanelHistorial" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="card shadow-sm mt-4 bg-section">
                        <div class="card-header bg-light">
                            <h6 class="mb-0 text-primary fw-semibold">
                                <i class="fas fa-chart-line me-2 text-primary"></i>Historial del Cliente
                            </h6>
                        </div>
                        <div class="card-body">
                            <div class="table-responsive">
                                <asp:GridView ID="gvHistorialCliente" runat="server"
                                    CssClass="table table-bordered table-hover table-sm text-center gv-compact"
                                    AutoGenerateColumns="False">
                                    <Columns>
                                        <%--<asp:BoundField DataField="RazonSocial" HeaderText="Razón Social">
                                <ItemStyle HorizontalAlign="Left" Width="180px" />
                            </asp:BoundField>--%>
                                        <asp:BoundField DataField="TotalPedidos" HeaderText="Total Pedidos">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FechaUltimoPedido" HeaderText="Último Pedido">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PromedioMontoPedidos" HeaderText="Promedio Monto">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TotalHistoricoComprado" HeaderText="Total Histórico">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Más Frecuente">
                                            <ItemTemplate>
                                                <span title='<%# Eval("ArticuloMasFrecuente") %>'>
                                                    <%# Eval("ArticuloMasFrecuente") %>
                                                </span>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="140px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CantidadArticuloMasFrecuente" HeaderText="Cantidad">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UltimoPrecioArticuloFrecuente" HeaderText="Último Precio">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PromedioDiasEntrePedidos" HeaderText="Días entre Ped.">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>



                        </div>
                    </div>








                </ContentTemplate>





                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlRazonSocial" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>



            <div class="card shadow-sm mb-4 bg-section">
                <div class="card-header bg-light">
                    <h6 class="mb-0 text-primary fw-semibold">
                        <i class="fas fa-chart-line me-2 text-primary"></i>Gráfico de Compras
                    </h6>
                </div>
                <div class="card-body">
                    <canvas id="graficoVentas" style="width: 100%; height: 300px;"></canvas>
                </div>
            </div>







            <div class="container p-4" style="width: 100%; max-width: inherit; padding: 20px; background-color: #f8f9fa; border: 1px solid #dee2e6; border-radius: 8px;">
                <!-- Primera fila con todos los campos y botón Buscar alineados en una línea -->
                <asp:UpdatePanel ID="UpdatePanelCliente" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <h6 class="titulo-seccion mb-3">
                            <i class="fas fa-user-tie me-2 text-primary"></i>Razón Social
                        </h6>
                        <div class="card shadow-sm mb-4">
                            <div class="card-header bg-light">
                                <div class="card-header bg-light">
                                    <div class="d-flex flex-wrap align-items-end gap-2">

                                        <!-- Tipo Documento -->
                                        <div style="width: 160px;">
                                            <label for="txtTipoDocumento" class="form-label text-secondary small mb-1">Tipo Doc</label>
                                            <asp:TextBox ID="txtTipoDocumento" runat="server"
                                                CssClass="form-control form-control-sm text-primary shadow-sm" />
                                        </div>

                                        <!-- Código Cliente -->
                                        <div style="width: 100px;">
                                            <label for="txtCodCliente" class="form-label text-secondary small mb-1">Código</label>
                                            <asp:TextBox ID="txtCodCliente" runat="server"
                                                CssClass="form-control form-control-sm shadow-sm"
                                                MaxLength="10"
                                                AutoPostBack="true"
                                                OnTextChanged="txtCodCliente_TextChanged"
                                                onkeypress="return validateNumericInput(event)" />
                                        </div>

                                        <!-- Fecha Pedido -->
                                        <div style="width: 140px;">
                                            <label for="txtFechaPedido" class="form-label text-secondary small mb-1">Fecha</label>
                                            <asp:TextBox ID="txtFechaPedido" runat="server"
                                                CssClass="form-control form-control-sm shadow-sm text-end"
                                                placeholder="dd/mm/aaaa"
                                                Style="border-color: #6c757d;" />
                                        </div>

                                        <div style="width: 140px;">
                                            <label for="ddlEstado" class="form-label text-secondary small mb-1">Estado</label>
                                            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control form-control-sm shadow-sm" />
                                        </div>


                                          <div style="width: 140px;">
                                            <label for="ddlFormaPago" class="form-label text-secondary small mb-1">Forma de Pago</label>
                                            <asp:DropDownList ID="ddlFormaPagos" runat="server" CssClass="form-control form-control-sm shadow-sm" />
                                        </div>


                                            <div style="width: 140px;">
                                            <label for="ddlPlazoEntrega" class="form-label text-secondary small mb-1">Plazo Entrega</label>
                                            <asp:DropDownList ID="ddlPlazoEntrega" runat="server" CssClass="form-control form-control-sm shadow-sm" />
                                        </div>




                                        <!-- Botón Buscar alineado a la derecha -->
                                        <%--<div class="ms-auto" style="width: 200px;">
                                            <label for="ddlTipoCotizacion" class="form-label text-secondary small mb-1">Tipo</label>


                                            <asp:DropDownList
                                                ID="ddlTipoCotizacion"
                                                runat="server"
                                                CssClass="form-control form-control-sm shadow-sm"
                                                AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlOpciones_SelectedIndexChanged">
                                            </asp:DropDownList>





                                            <label class="form-label d-block invisible">Buscar</label>
                                            <asp:Button ID="btnBuscarCliente" runat="server"
                                                Text="Buscar"
                                                CssClass="btn btn-outline-primary btn-sm w-100 shadow-sm"
                                                OnClientClick="$('#clientesModal').modal('show'); return false;" />
                                        </div>--%>
                                        
                                        <asp:UpdatePanel ID="updTipoCotizacion" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="ms-auto" style="width: 200px;">
                                                    <label for="ddlTipoCotizacion" class="form-label text-secondary small mb-1">Tipo</label>

                                                    <asp:DropDownList
                                                        ID="ddlTipoCotizacion"
                                                        runat="server"
                                                        CssClass="form-control form-control-sm shadow-sm"
                                                        AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlOpciones_SelectedIndexChanged">
                                                    </asp:DropDownList>

                                                    <label class="form-label d-block invisible">Buscar</label>
                                                    <asp:Button ID="btnBuscarCliente" runat="server"
                                                        Text="Buscar"
                                                        CssClass="btn btn-outline-primary btn-sm w-100 shadow-sm"
                                                        OnClientClick="$('#clientesModal').modal('show'); return false;" />
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlTipoCotizacion" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>









                                    </div>
                                </div>







                                <div class="card-body">
                                    <div class="row g-3 align-items-end">

                                        <!-- Tipo Documento -->


                                        <!-- Razón Social -->
                                        <!-- Razón Social + Fecha -->
                                        <div class="col-md-8">
                                            <label for="ddlRazonSocial" class="form-label fw-semibold text-secondary">Razón Social</label>
                                            <asp:UpdatePanel ID="UpdatePanelRazonSocial" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList
                                                        ID="ddlRazonSocial"
                                                        runat="server"
                                                        CssClass="form-control select2-autocompletar"
                                                        AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlRazonSocial_SelectedIndexChanged" />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlRazonSocial" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>




                                        <!-- Dirección -->
                                        <div class="col-md-4">
                                            <label for="txtDireccion" class="form-label fw-semibold text-secondary">Dirección</label>
                                            <asp:TextBox ID="txtDireccion" runat="server"
                                                CssClass="form-control form-control-sm shadow-sm text-primary"
                                                MaxLength="200" />

                                        </div>

                                        <!-- CUIT -->
                                        <%-- <div class="col-md-2">
                                        <label for="txtCUIT" class="form-label fw-semibold text-secondary">CUIT</label>
                                        <asp:TextBox ID="txtCUIT" runat="server" CssClass="form-control shadow-sm" />
                                    </div>--%>

                                        <!-- Estado -->





                                    </div>
                                </div>
                            </div>
                    </ContentTemplate>
                </asp:UpdatePanel>




                
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="card shadow-sm mt-4 mb-5 bg-section">
                            <div class="card-header bg-light">
                                <h6 class="mb-0 text-primary fw-semibold">
                                    <i class="fas fa-map-marker-alt me-2 text-primary"></i>Datos de Entrega
                                </h6>
                            </div>
                            <div class="card-body">
                                <div class="row gy-3 gx-4">
                                    <!-- Provincia (oculta) -->
                                    <div class="col-md-3 col-sm-6" runat="server" visible="false">
                                        <label for="TextPciaEntrega" class="form-label text-secondary fw-semibold small">Provincia</label>
                                        <asp:TextBox ID="TextPciaEntrega" runat="server"
                                            CssClass="form-control form-control-sm shadow-sm input-focus-anim text-primary" />
                                    </div>


                                      <!-- Dirección -->
                                    <div class="col-md-4 col-sm-12">
                                        <label class="form-label">Dirección de Entrega</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtDireccionEntrega" runat="server"
                                                CssClass="form-control form-control-sm shadow-sm input-focus-anim text-primary"
                                                MaxLength="100"
                                                ClientIDMode="Static" />
                                            <button type="button" class="btn btn-outline-secondary"
                                                onclick="reconocerYAsignar('txtDireccionEntrega')">
                                                <i class="fas fa-microphone"></i>
                                            </button>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-12">
                                        <label class="form-label">Localidad</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="TextIdLocalidadEntrega" runat="server"
                                                CssClass="form-control form-control-sm shadow-sm input-focus-anim text-primary"
                                                ClientIDMode="Static" />
                                            <button type="button" class="btn btn-outline-secondary"
                                                onclick="reconocerYAsignar('TextIdLocalidadEntrega')">
                                                <i class="fas fa-microphone"></i>
                                            </button>
                                        </div>
                                    </div>

                                  

                                    <!-- Contacto -->
                                    <div class="col-md-4 col-sm-12">
                                        <label class="form-label">Contacto</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="TextContacto" runat="server"
                                                CssClass="form-control form-control-sm shadow-sm input-focus-anim text-primary"
                                                ClientIDMode="Static" />
                                            <button type="button" class="btn btn-outline-secondary"
                                                onclick="reconocerYAsignar('TextContacto')">
                                                <i class="fas fa-microphone"></i>
                                            </button>
                                        </div>
                                    </div>
                                </div>
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
                                        <asp:TextBox ID="txtBuscarRazonSocial" runat="server" CssClass="form-control shadow-sm" Placeholder="Razón Social" Style="border-color: #6c757d;" />
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
                <div class="card shadow-sm mt-4">
                    <div class="card-header bg-light">
                        <h6 class="mb-0 text-primary fw-semibold">Agregar Artículo</h6>
                    </div>
                    <div class="card-body">
                        <asp:UpdatePanel ID="UpdatePanelArticulos" runat="server">
                            <ContentTemplate>
                                <div class="row g-3 align-items-end">
                                    <!-- Cantidad -->
                                    <div class="col-md-2">
                                        <label for="txtCantidad" class="form-label fw-semibold text-secondary">Cantidad</label>

                                        <asp:TextBox ID="txtCantidad" runat="server"
                                            CssClass="form-control form-control-sm shadow-sm"
                                            Style="border-color: #6c757d;"
                                            onkeyup="soloDecimal(this)"
                                            ValidationGroup="AgregarArticulo" />



                                        <asp:RequiredFieldValidator ID="rfvCantidad" runat="server"
                                            ControlToValidate="txtCantidad"
                                            ErrorMessage="* Requerido"
                                            ForeColor="Red"
                                            Display="Dynamic"
                                            ValidationGroup="AgregarArticulo" />

                                        <asp:RegularExpressionValidator ID="revCantidad" runat="server"
                                            ControlToValidate="txtCantidad"
                                            ValidationExpression="^\d+(\.\d{1,2})?$"
                                            ErrorMessage="* Solo números decimales"
                                            ForeColor="Red"
                                            Display="Dynamic"
                                            ValidationGroup="AgregarArticulo" />
                                    </div>


                                    <!-- Unidad -->
                                    <div class="col-md-2">
                                        <label for="ddlunidad" class="form-label fw-semibold text-secondary">Unidad</label>
                                        <asp:DropDownList ID="ddlunidad" runat="server"
                                            CssClass="form-control select2-autocompletar"
                                            Style="border-color: #6c757d;" />
                                    </div>

                                    <!-- Descripción -->
                                    <div class="col-md-4">
                                        <label for="ddldescripcion" class="form-label fw-semibold text-secondary">Descripción</label>
                                        <asp:DropDownList ID="ddldescripcion" runat="server"
                                            CssClass="form-control select2-autocompletar" />
                                    </div>

                                    <!-- Espesor -->
                                    <div class="col-md-2">
                                        <label for="ddlEspesor" class="form-label fw-semibold text-secondary">Espesor</label>
                                        <asp:DropDownList ID="ddlEspesor" runat="server"
                                            CssClass="form-control select2-autocompletar" />
                                    </div>

                                    <!-- Ancho -->
                                    <div class="col-md-2">
                                        <label for="ddlAncho" class="form-label fw-semibold text-secondary">Ancho</label>
                                        <asp:DropDownList ID="ddlAncho" runat="server"
                                            CssClass="form-control select2-autocompletar" />
                                    </div>

                                    <!-- Largo -->
                                    <div class="col-md-2">
                                        <label for="ddlLargo" class="form-label fw-semibold text-secondary">Largo</label>
                                        <asp:DropDownList ID="ddlLargo" runat="server"
                                            CssClass="form-control select2-autocompletar" />
                                    </div>


                                    <%--<!-- Observaciones + Botón en la misma línea -->
<div class="col-md-10 col-sm-8">
    <label for="txtObservaciones" class="form-label fw-semibold text-secondary">Observaciones</label>
    <asp:TextBox ID="txtObservaciones" runat="server"
        CssClass="form-control form-control-sm shadow-sm input-focus-anim" />
</div>--%>






                                    <!-- Precio Unitario -->
                                    <div class="col-md-2">
                                        <label for="txtPrecioUnitario" class="form-label fw-semibold text-secondary">Precio Unitario</label>
                                        <asp:TextBox ID="txtPrecioUnitario" runat="server"
                                            AutoPostBack="True"
                                            OnTextChanged="txtpreciounitario_Changed"
                                            CssClass="form-control form-control-sm shadow-sm"
                                            MaxLength="10"
                                            Style="border-color: #6c757d;" />

                                    </div>
                                    <!-- Observaciones + Botón Agregar en la misma línea, con el botón alineado a la derecha -->
                                    <%--<div class="col-12 d-flex flex-wrap gap-2">
   <div class="col-md-8">
    <label class="form-label">Observaciones</label>
    <div class="input-group">
        <asp:TextBox ID="txtObservaciones" runat="server"
            CssClass="form-control form-control-sm shadow-sm input-focus-anim"
            ClientIDMode="Static" />
        <button type="button" class="btn btn-outline-secondary"
            onclick="reconocerYAsignar('txtObservaciones')">
            <i class="fas fa-microphone"></i>
        </button>
    </div>
</div>--%>

                                    <div class="col-12 d-flex align-items-end gap-2">
                                        <div class="flex-grow-1">
                                            <label for="txtObservaciones" class="form-label">Observaciones</label>
                                            <div class="input-group">

                                                <button type="button" class="btn btn-outline-secondary"
                                                    onclick="reconocerYAsignar('txtObservaciones')">
                                                    <i class="fas fa-microphone"></i>
                                                </button>
                                                <asp:TextBox ID="txtObservaciones" runat="server"
                                                    CssClass="form-control form-control-sm shadow-sm input-focus-anim"
                                                    ClientIDMode="Static" />

                                            </div>
                                        </div>

                                        <%-- <div style="min-width: 120px;">
        <asp:Button ID="btnAgregar" runat="server" Text="Agregar"
            CssClass="btn btn-outline-primary shadow-sm px-4"
            OnClientClick="return validarYHablar();" 
            OnClick="btnAgregar_Click"
            CausesValidation="true"
            ValidationGroup="AgregarArticulo" />
    </div>--%>
                                    </div>


                                    <div style="min-width: 120px;" class="d-flex align-items-end">
                                        <!-- Botón con validación hablada -->
                                        <asp:Button ID="btnAgregar" runat="server" Text="Agregar"
                                            CssClass="btn btn-outline-primary shadow-sm px-4"
                                            OnClientClick="return validarArticuloYHablar();"
                                            OnClick="btnAgregar_Click"
                                            CausesValidation="true"
                                            ValidationGroup="AgregarArticulo" />
                                    </div>
                                </div>


                                <%--<div class="col-md-2 col-sm-4 d-flex align-items-end">
    <asp:Button ID="btnAgregar" runat="server" Text="Agregar"
        CssClass="btn btn-outline-primary w-100 shadow-sm"
        OnClick="btnAgregar_Click"
        CausesValidation="true"
        ValidationGroup="AgregarArticulo" />
</div>--%>
                                <!-- Precio Total oculto -->
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtPrecioTotal" runat="server"
                                        CssClass="form-control shadow-sm"
                                        MaxLength="10"
                                        Visible="false"
                                        ReadOnly="True"
                                        Style="border-color: #6c757d;" />
                                </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>


                <!-- agreuge -->












                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="bg-section mt-4 p-3 rounded shadow-sm">
                            <h6 class="titulo-seccion">
                                <i class="fas fa-layer-group me-2 text-primary"></i>Artículos
                            </h6>
                            <div class="table-responsive">
                                <asp:GridView ID="gvArticulos" runat="server"
                                    CssClass="table table-bordered table-hover text-center table-sm table-articulos"
                                    AutoGenerateColumns="False"
                                    AllowPaging="True" PageSize="4"
                                    OnPageIndexChanging="gvArticulos_PageIndexChanging"
                                    DataKeyNames="IdEspesor,IdAncho,Idlargo,IdUnidad,IdTasa"
                                    OnRowCommand="gvArticulos_RowCommand"
                                    AlternatingRowStyle-CssClass="row-alt">


                                    <Columns>
                                        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                        <asp:BoundField DataField="IdEspesor" HeaderText="IdEspesor" Visible="false" />
                                        <asp:BoundField DataField="Espesor" HeaderText="Espesor" />
                                        <asp:BoundField DataField="IdAncho" HeaderText="IdAncho" Visible="false" />
                                        <asp:BoundField DataField="Ancho" HeaderText="Ancho" />
                                        <asp:BoundField DataField="Idlargo" HeaderText="IdLargo" Visible="false" />
                                        <asp:BoundField DataField="Largo" HeaderText="Largo" />
                                        <asp:BoundField DataField="IdUnidad" HeaderText="IdUnidad" Visible="false" />
                                        <asp:BoundField DataField="Unidad" HeaderText="Unidad" />
                                        <asp:BoundField DataField="IdTasa" HeaderText="IdTasa" Visible="false" />
                                        <asp:BoundField DataField="Tasa" HeaderText="Tasa" Visible="false" />
                                        <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" />
                                        <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" />
                                        <asp:BoundField DataField="PrecioTotal" HeaderText="Precio Total" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnBorrar" runat="server"
                                                    Text="Borrar"
                                                    CssClass="btn btn-outline-danger btn-sm"
                                                    CommandName="DeleteRow"
                                                    CommandArgument='<%# Container.DataItemIndex %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>



                <!-- Panel de sumas y botón Aceptar -->
                <asp:UpdatePanel ID="UpdatePanelSumas" runat="server">
                    <ContentTemplate>
                        <div class="mt-4 d-flex justify-content-end flex-wrap w-100">

                            <div class="resumen-importes-card">
                                <div class="card-body">
                                    <h6 class="titulo-seccion mb-3">
                                        <i class="fas fa-coins me-2 text-primary"></i>Importes
                                    </h6>

                                    <asp:GridView ID="gvSumas" runat="server"
                                        CssClass="table table-bordered table-hover table-sm text-end tabla-sumas"
                                        AutoGenerateColumns="False" GridLines="None">
                                        <Columns>
                                            <asp:BoundField HeaderText="Cantidades" DataField="CantidadTotal" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="SubTotal" DataField="Subtotal" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="P.BsAs" DataField="pbsas" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="P.Caba" DataField="pcaba" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="P.Misiones" DataField="pmisiones" ItemStyle-HorizontalAlign="Right" />

                                              <asp:BoundField HeaderText="IVA" DataField="IVA" ItemStyle-HorizontalAlign="Right" />



                                            <asp:BoundField HeaderText="TOTAL" DataField="Total" ItemStyle-HorizontalAlign="Right">
                                                <ItemStyle CssClass="fw-bold text-primary fs-5" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>

                                    <%-- <div class="text-end mt-3">
                        <asp:Button ID="btnAceptar" runat="server"
                            Text="Aceptar"
                            CssClass="btn btn-outline-primary btn-lg px-4 fw-semibold shadow-sm"
                             onclick="reconocerYAsignar('txtDireccionEntrega')"
                           
                            OnClientClick="return validarConfirmarPedido();" />
                    </div>--%>

                                    <asp:Button ID="btnAceptar" runat="server"
                                        Text="Aceptar"
                                        CssClass="btn btn-outline-primary btn-lg px-4 fw-semibold shadow-sm"
                                        OnClick="btnAceptar_Click"
                                        OnClientClick="return validarConfirmacionYHablar();" />

                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>



            </div>






        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlRazonSocial" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtCodCliente" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtDireccionEntrega" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtCantidad" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtPrecioUnitario" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnAgregar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnFiltrarClientes" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvArticulos" EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="gvArticulos" EventName="PageIndexChanging" />
            <asp:AsyncPostBackTrigger ControlID="btnAceptar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>








    <script type="text/javascript">
        let chartInstance = null;

        Sys.Application.add_load(function () {
            $('.select2-autocompletar').select2({
                placeholder: 'Seleccioná una opción...',
                allowClear: true,
                width: '100%'
            });

            // Desvinculamos y volvemos a vincular para evitar duplicados
            $('#<%= ddlRazonSocial.ClientID %>').off('change').on('change', function () {
                const idcliente = $(this).val();
                if (idcliente) {
                    cargarGrafico(idcliente);
                }
            });

            // Si ya hay un cliente seleccionado, mostrar el gráfico
            const seleccionado = $('#<%= ddlRazonSocial.ClientID %>').val();
            if (seleccionado) {
                cargarGrafico(seleccionado);
            }
        });

        function reconocerYAsignar(idControl, tipo = "text") {
            if (!('webkitSpeechRecognition' in window)) {
                alert("Tu navegador no soporta reconocimiento de voz.");
                return;
            }

            const recog = new webkitSpeechRecognition();
            recog.lang = 'es-AR';
            recog.interimResults = false;
            recog.maxAlternatives = 1;
            recog.start();

            recog.onresult = function (event) {
                let resultado = event.results[0][0].transcript.trim();
                const control = document.getElementById(idControl);

                if (tipo === "number") {
                    resultado = resultado.replace(",", ".").replace(/[^\d.]/g, "");
                }

                if (control) control.value = resultado;
            };

            recog.onerror = function (event) {
                console.error("Error reconocimiento de voz:", event.error);
                alert("Error en el reconocimiento de voz: " + event.error);
            };
        }

        function reconocimientoParaDropdown(dropdownId) {
            const recog = new webkitSpeechRecognition();
            recog.lang = 'es-AR';
            recog.interimResults = false;
            recog.maxAlternatives = 1;
            recog.start();

            recog.onresult = function (event) {
                const texto = event.results[0][0].transcript.toLowerCase().trim();
                const ddl = document.getElementById(dropdownId);
                let encontrado = false;

                for (let i = 0; i < ddl.options.length; i++) {
                    const opcion = ddl.options[i].text.toLowerCase().trim();
                    if (opcion.includes(texto)) {
                        ddl.selectedIndex = i;
                        encontrado = true;
                        break;
                    }
                }

                if (!encontrado) {
                    alert("No se encontró una opción que coincida con: " + texto);
                }
            };
        }




        function cargarGrafico(idcliente) {
            $.ajax({
                type: "POST",
                url: "AltaCotizacion.aspx/ObtenerDatosGrafico",
                data: JSON.stringify({ idcliente: idcliente }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    const datos = response.d;

                    if (!Array.isArray(datos) || datos.length === 0) {
                        // Si no hay datos, destruimos el gráfico anterior si existe
                        if (chartInstance) {
                            chartInstance.destroy();
                            chartInstance = null;
                        }
                        return;
                    }

                    const fechas = datos.map(x => x.fecha);
                    const totales = datos.map(x => x.total);

                    const canvas = document.getElementById("graficoVentas");
                    if (!canvas) return;

                    const ctx = canvas.getContext("2d");

                    if (chartInstance) {
                        chartInstance.destroy();
                    }

                    chartInstance = new Chart(ctx, {
                        type: 'line',
                        data: {
                            labels: fechas,
                            datasets: [{
                                label: 'Total por Fecha',
                                data: totales,
                                borderColor: 'rgba(75, 192, 192, 1)',
                                borderWidth: 2,
                                fill: false,
                                tension: 0.3
                            }]
                        },
                        options: {
                            responsive: true,
                            maintainAspectRatio: false,
                            scales: {
                                y: {
                                    beginAtZero: true
                                }
                            }
                        }
                    });
                }
            });
        }
    </script>

    <script type="text/javascript">
        function hablar(texto) {
            const mensaje = new SpeechSynthesisUtterance(texto);
            mensaje.lang = "es-AR";
            window.speechSynthesis.speak(mensaje);
        }

   <%--function validarYHablar() {
        let errores = [];

        const unidad = document.getElementById("<%= txtCantidad.ClientID %>").value;
        const precio = document.getElementById("<%= txtPrecioUnitario.ClientID %>").value.trim();

        if (!unidad) {
            errores.push("Error en Cantidad.");
        }

        if (!precio) {
            errores.push("Error en  precio unitario.");
        }

        if (errores.length > 0) {
            hablar(errores.join(" "));
            return false; // evita postback si hay errores
        }

        return true; // permite postback si todo está bien--%>
        function validarArticuloYHablar() {
            let errores = [];

            const unidad = document.getElementById("<%= txtCantidad.ClientID %>").value;
           <%-- const precio = document.getElementById("<%= txtPrecioUnitario.ClientID %>").value.trim();--%>

            if (!unidad) {
                errores.push("Error en Cantidad.");
            }

            //if (!precio) {
            //    errores.push("Error en precio unitario.");
            //}

            if (errores.length > 0) {
                hablar(errores.join(" "));
                return false;
            }

            return true;
        }
        function validarConfirmacionYHablar() {
            let errores = [];

            const codCliente = document.getElementById("<%= txtCodCliente.ClientID %>").value.trim();
            if (!codCliente) {
                errores.push("Código de cliente faltante.");
            }

            const grilla = document.getElementById("<%= gvArticulos.ClientID %>");
            if (!grilla || grilla.rows.length <= 1) {
                errores.push("Debe cargar al menos un artículo.");
            }

            if (errores.length > 0) {
                hablar(errores.join(" "));
                return false;
            }

            hablar("Cotización confirmada");
            return true;
        }


    </script>




















    <div class="mt-4">
        <label for="selectVoz" class="form-label fw-bold text-primary">Seleccionar voz:</label>
        <select id="selectVoz" class="form-select w-auto mb-2"></select>

        <button class="btn btn-outline-primary" onclick="probarVozSeleccionada()">Probar Voz</button>
    </div>

    <script>
        let vocesCargadas = [];

        function cargarVoces() {
            vocesCargadas = window.speechSynthesis.getVoices();
            const select = document.getElementById("selectVoz");
            select.innerHTML = ""; // Limpia

            vocesCargadas.forEach((voz, index) => {
                const opcion = document.createElement("option");
                opcion.value = index;
                opcion.text = `${voz.name} (${voz.lang})`;
                select.appendChild(opcion);
            });

            console.log("📢 Voces disponibles:", vocesCargadas);
        }

        function probarVozSeleccionada() {
            const select = document.getElementById("selectVoz");
            const index = parseInt(select.value);
            const voz = vocesCargadas[index];

            const mensaje = new SpeechSynthesisUtterance("Esta es una prueba de voz.");
            mensaje.voice = voz;
            mensaje.lang = voz.lang;
            window.speechSynthesis.speak(mensaje);
        }

        // Asegurarse de que se cargan las voces correctamente
        if (typeof speechSynthesis !== "undefined") {
            speechSynthesis.onvoiceschanged = cargarVoces;
            setTimeout(cargarVoces, 200); // fallback
        }
    </script>










</asp:Content>



