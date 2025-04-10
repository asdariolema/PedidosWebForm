﻿<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AltaCotizacion.aspx.cs" Inherits="PedidosWebForm.AltaCotizacion" MasterPageFile="~/Site.master" %>

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




            
<asp:UpdatePanel ID="UpdatePanelHistorial" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="card shadow-sm mt-4 bg-section">
            <div class="card-header bg-light">
                <h6 class="mb-0 text-primary fw-semibold">
                    <i class="fas fa-chart-line me-2 text-primary"></i>Historial del Cliente
                </h6>
            </div>
            <div class="card-body">
                <asp:GridView ID="gvHistorialCliente" runat="server"
                    CssClass="table table-bordered table-hover table-sm text-center"
                    AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="RazonSocial" HeaderText="Razón Social" />
                        <asp:BoundField DataField="TotalPedidos" HeaderText="Total Pedidos" />
                        <asp:BoundField DataField="FechaUltimoPedido" HeaderText="Último Pedido" />
                        <asp:BoundField DataField="PromedioMontoPedidos" HeaderText="Promedio Monto" />
                        <asp:BoundField DataField="TotalHistoricoComprado" HeaderText="Total Histórico" />
                        <asp:BoundField DataField="ArticuloMasFrecuente" HeaderText="Más Frecuente" />
                        <asp:BoundField DataField="CantidadArticuloMasFrecuente" HeaderText="Cantidad" />
                        <asp:BoundField DataField="UltimoPrecioArticuloFrecuente" HeaderText="Último Precio" />
                        <asp:BoundField DataField="PromedioDiasEntrePedidos" HeaderText="Días entre Ped." />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddlRazonSocial" EventName="SelectedIndexChanged" />
    </Triggers>
</asp:UpdatePanel>--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AltaCotizacion.aspx.cs" Inherits="PedidosWebForm.AltaCotizacion" MasterPageFile="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Alta de Cotizaciòn
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Estilos y scripts -->
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



                <!-- Panel de sumas y botón Aceptar --><asp:UpdatePanel ID="UpdatePanelSumas" runat="server">
    <ContentTemplate>
        <div class="mt-4 d-flex justify-content-end">
            <div class="resumen-importes-card">
                <div class="card-body">
                    <h6 class="titulo-seccion mb-3">
                        <i class="fas fa-coins me-2 text-primary"></i>Importes
                    </h6>

                    <asp:GridView ID="gvSumas" runat="server"
                        CssClass="table table-bordered table-hover table-sm text-end tabla-sumas"
                        AutoGenerateColumns="False" GridLines="None">
                        <Columns>
                            <asp:BoundField HeaderText="CANTIDADES" DataField="CantidadTotal" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField HeaderText="SUBTOTAL" DataField="Subtotal" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField HeaderText="TOTAL" DataField="Total" ItemStyle-HorizontalAlign="Right">
                                <ItemStyle CssClass="fw-bold text-primary fs-5" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>

                    <div class="text-end mt-3">
                        <asp:Button ID="btnAceptar" runat="server"
                            Text="Aceptar"
                            CssClass="btn btn-outline-primary btn-lg px-4 fw-semibold shadow-sm"
                            OnClick="btnAceptar_Click"
                            OnClientClick="return validarConfirmarPedido();" />
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

                

            </div>


            <style>

                .gv-compact td, .gv-compact th {
    padding: 0.35rem 0.5rem;
    font-size: 0.85rem;
    white-space: nowrap;
}
.gv-compact td {
    overflow: hidden;
    text-overflow: ellipsis;
    max-width: 120px;
}


        body {
            background-color: #f0f4f8;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            color: #343a40;
        }

        .titulo-seccion {
            font-size: 1.25rem;
            color: #0d6efd;
            font-weight: bold;
            margin-bottom: 15px;
        }

        .resumen-importes-card {
    width: 100%;
    max-width: 480px;
    background: linear-gradient(145deg, #ffffff, #e3eaf4);
    border-radius: 16px;
    padding: 20px;
    box-shadow: 0 4px 12px rgba(0,0,0,0.08);
    animation: fadeInUp 0.6s ease-in-out;
    margin-left: auto; /* Alineado a la derecha */
}

@media (max-width: 767px) {
    .resumen-importes-card {
        max-width: 100%;
        margin: 0 auto;
    }
}

.tabla-sumas thead th {
    background-color: #e9f2fb;
    color: #0d6efd;
    font-weight: 600;
    font-size: 0.95rem;
}

.tabla-sumas td {
    font-size: 1rem;
    padding: 0.6rem;
    vertical-align: middle;
}

.tabla-sumas tr:hover {
    background-color: #f4faff;
    transition: 0.3s ease;
}

        .table-articulos thead {
            background-color: #0d6efd;
            color: white;
        }
    .resumen-sumas td {
    font-size: 1rem;
    padding: 0.5rem;
    color: #495057;
    border-bottom: 1px solid #dee2e6;
}

.resumen-sumas tr:last-child td {
    border-bottom: none;
}

        

        .table-articulos tbody tr:hover {
            background-color: #eef5ff;
            transition: 0.3s ease;
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

        .bg-section {
            background: linear-gradient(145deg, #ffffff, #e0e6ed);
            border-radius: 20px;
            padding: 20px;
            margin-bottom: 30px;
            animation: fadeInUp 0.6s ease-in-out;
        }

        @keyframes fadeInUp {
            0% {
                opacity: 0;
                transform: translateY(20px);
            }
            100% {
                opacity: 1;
                transform: translateY(0);
            }
        }

        /* Fondo celeste para el input del filtro en los dropdown Select2 */
.select2-container--default .select2-search--dropdown .select2-search__field {
    background-color: #dff1ff !important;
    border: 1px solid #0d6efd;
    color: #212529;
    padding: 6px 8px;
    border-radius: 0.4rem;
    font-size: 0.9rem;
}

/* Fondo de la opción resaltada al navegar */
.select2-container--default .select2-results__option--highlighted[aria-selected] {
    background-color: #cce5ff;
    color: #000;
}



.table-articulos tr:hover {
    background-color: #dceeff !important;
    transition: 0.3s ease;
}
        .input-focus-anim:focus {
            box-shadow: 0 0 0 0.2rem rgba(13, 110, 253, 0.25);
            transition: box-shadow 0.3s ease-in-out;
        }

    .row-normal {
    background-color: #f8f9fa;
}

/* Esto sí va a funcionar */
.row-alt td {
    background-color: #eaf0fb !important;
}



.table-articulos .row-alt {
    background-color: #eaf0fb;
}

.table-articulos tr:hover {
    background-color: #dceeff !important;
    transition: 0.3s ease;
}

    </style>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <div class="card shadow-sm mt-4 bg-section">
    <div class="card-header bg-light">
        <h6 class="mb-0 text-primary fw-semibold">
            <i class="fas fa-map-marker-alt me-2 text-primary"></i>Datos de Entrega
        </h6>
    </div>
    <div class="card-body">
        <div class="row g-3">
            <div class="col-md-3 col-sm-6">
                <label for="TextPciaEntrega" class="form-label">Provincia</label>
                <asp:TextBox ID="TextPciaEntrega" runat="server" CssClass="form-control form-control-sm shadow-sm input-focus-anim text-primary" />
            </div>
            <div class="col-md-3 col-sm-6">
                <label for="TextIdLocalidadEntrega" class="form-label">Localidad</label>
                <asp:TextBox ID="TextIdLocalidadEntrega" runat="server" CssClass="form-control form-control-sm shadow-sm input-focus-anim text-primary" />
            </div>
            <div class="col-md-4 col-sm-12">
                <label for="txtDireccionEntrega" class="form-label">Dirección de Entrega</label>
                <asp:TextBox ID="txtDireccionEntrega" runat="server" CssClass="form-control form-control-sm shadow-sm input-focus-anim" MaxLength="100" />
            </div>
            <div class="col-md-2 col-sm-6">
                <label for="TextContacto" class="form-label">Contacto</label>
                <asp:TextBox ID="TextContacto" runat="server" CssClass="form-control form-control-sm shadow-sm input-focus-anim" />
            </div>
        </div>
    </div>
</div>

    </ContentTemplate>
</asp:UpdatePanel>



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








