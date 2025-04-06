<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AltaCotizacion.aspx.cs" Inherits="PedidosWebForm.AltaCotizacion" MasterPageFile="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Alta de Cotizaciòn
</asp:Content>





<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Referencias a jQuery y Bootstrap -->
    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>


    <script type="text/javascript">
        Sys.Application.add_load(function () {
            $('.select2-autocompletar').select2({
                placeholder: 'Seleccioná una opción...',
                allowClear: true,
                width: '100%'
            });
        });
    </script>

    <asp:UpdatePanel ID="MainUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <div class="container p-4" style="width: 100%; max-width: inherit; padding: 20px; background-color: #f8f9fa; border: 1px solid #dee2e6; border-radius: 8px;">
                <!-- Primera fila con todos los campos y botón Buscar alineados en una línea -->
                <asp:UpdatePanel ID="UpdatePanelCliente" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
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



                                        <!-- Botón Buscar alineado a la derecha -->
                                        <div class="ms-auto" style="width: 100px;">
                                            <label class="form-label d-block invisible">Buscar</label>
                                            <asp:Button ID="btnBuscarCliente" runat="server"
                                                Text="Buscar"
                                                CssClass="btn btn-outline-primary btn-sm w-100 shadow-sm"
                                                OnClientClick="$('#clientesModal').modal('show'); return false;" />
                                        </div>

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
                                            <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control shadow-sm fw-bold text-primary" MaxLength="200" />
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
                                <asp:TextBox ID="txtDireccionEntrega" runat="server" CssClass="form-control shadow-sm" MaxLength="100" />
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
                                            CssClass="form-control shadow-sm"
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

                                    <!-- Precio Unitario -->
                                    <div class="col-md-2">
                                        <label for="txtPrecioUnitario" class="form-label fw-semibold text-secondary">Precio Unitario</label>
                                        <asp:TextBox ID="txtPrecioUnitario" runat="server"
                                            AutoPostBack="True"
                                            OnTextChanged="txtpreciounitario_Changed"
                                            CssClass="form-control shadow-sm"
                                            MaxLength="10"
                                            Style="border-color: #6c757d;" />
                                    </div>

                                    <!-- Botón Agregar -->
                                    <div class="col-md-2">
                                        <asp:Button ID="btnAgregar" runat="server" Text="Agregar"
                                            CssClass="btn btn-outline-primary w-100 shadow-sm"
                                            OnClick="btnAgregar_Click"
                                            CausesValidation="true"
                                            ValidationGroup="AgregarArticulo" />
                                    </div>

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


                <!-- Grilla de artículos -->
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="table-responsive mt-3">
                            <asp:GridView ID="gvArticulos" runat="server" CssClass="table table-hover table-bordered"
                                AutoGenerateColumns="False" AllowPaging="True" PageSize="4" OnPageIndexChanging="gvArticulos_PageIndexChanging"
                                DataKeyNames="IdEspesor,IdAncho,Idlargo,IdUnidad,IdTasa" OnRowCommand="gvArticulos_RowCommand">
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
    body {
        background-color: #f0f4f8;
        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        color: #343a40;
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

    .card-header {
        background-color: #0d6efd;
        color: white;
        font-size: 1rem;
        font-weight: 600;
    }

    .form-label {
        font-weight: 600;
        color: #0d6efd;
    }

    .form-control, .select2-container--default .select2-selection--single {
        border-radius: 0.5rem;
        border-color: #ced4da;
    }

    .form-control:focus {
        border-color: #0d6efd;
        box-shadow: 0 0 0 0.2rem rgba(13, 110, 253, 0.25);
    }

    .btn-outline-primary {
        border-radius: 30px;
        font-weight: 600;
    }

    .btn-outline-primary:hover {
        background-color: #0d6efd;
        color: white;
    }

    .table thead {
        background-color: #0d6efd;
        color: white;
    }

    .badge-info {
        background-color: #20c997;
        font-size: 0.75rem;
    }

    .bg-section {
        background: linear-gradient(145deg, #ffffff, #e0e6ed);
        border-radius: 20px;
        padding: 20px;
        margin-bottom: 30px;
    }

    .titulo-seccion {
        font-size: 1.25rem;
        color: #0d6efd;
        font-weight: bold;
        margin-bottom: 15px;
    }
</style>



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

</asp:Content>








