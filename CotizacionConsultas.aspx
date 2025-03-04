<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CotizacionConsultas.aspx.cs" Inherits="PedidosWebForm.CotizacionConsultas" MasterPageFile="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Búsqueda de Cotizaciones


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Contenedor principal similar a AltaPedidos.aspx -->
    <div class="container my-4 p-4 border rounded bg-light shadow-sm">
        <h2 class="mb-4">Consulta de Cotizaciones</h2>
        <button type="button" class="btn btn-primary" onclick="openModal('FormConfirmacionPedido.aspx?id=123');">Abrir Modal</button>
        <!-- Colocar todos los campos en la misma línea con estilo uniforme -->
        <div class="row mb-3 align-items-end">
            <div class="col-md-2">
                <div class="form-group">
                    <label for="txtTipoDocumento">Tipo Documento:</label>
                    <asp:TextBox ID="txtTipoDocumento" runat="server" CssClass="form-control" style="width:150px;" />
                </div>
            </div>
            <div class="col-md-2">
                <div class="form-group">
                    <label for="TextBox1">Fecha Desde:</label>
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-2">
                <div class="form-group">
                    <label for="TextBox2">Fecha Hasta:</label>
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-2">
                <div class="form-group">
                    <label for="txtCodigoCliente">Cód. Cliente:</label>
                    <asp:TextBox ID="txtCodigoCliente" runat="server" CssClass="form-control" MaxLength="10" size="10"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-2">
                <div class="form-group">
                    <label for="txtRazonSocial">Razón Social:</label>
                    <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="form-control" MaxLength="160" size="100"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-2 d-flex justify-content-end">
                <asp:Button ID="btnBuscarRazonSocial" runat="server" Text="Buscar" CssClass="btn btn-primary shadow-sm" OnClick="btnBuscarRazonSocial_Click" />
            </div>
        </div>

        <!-- Grilla de resultados estilizada -->
        <div class="scrollable-gridview mt-4">
            <asp:GridView ID="gvResultados" runat="server" CssClass="table table-bordered table-hover table-striped shadow-sm" AutoGenerateColumns="False" OnRowCommand="gvResultados_RowCommand" OnRowDataBound="gvResultados_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="idcotiz" HeaderText="Nro." />
                    <asp:BoundField DataField="FechaAlta" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="idCliente" HeaderText="Cod. Cliente" />
                    <asp:BoundField DataField="NombreCliente" HeaderText="Razón Social" />
                    <asp:BoundField DataField="DireccionEntrega" HeaderText="Dirección" />
                    <asp:BoundField DataField="Estado1" HeaderText="Estado" />
                    
                 
                   <asp:TemplateField>
    <ItemTemplate>
        <asp:Button ID="btnAbrir" runat="server" Text="Abrir" CommandName="Abrir" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn btn-info btn-sm" />
    </ItemTemplate>
</asp:TemplateField>

    
<asp:TemplateField HeaderText="Acción">
            <ItemTemplate>
                <asp:Button ID="btnCambiar" runat="server" Text="Cambiar" 
                    CommandName="Cambiar" 
                    CommandArgument='<%# Eval("idcotiz") %>' 
                    CssClass="btn btn-info btn-sm" />
            </ItemTemplate>
        </asp:TemplateField>


<asp:TemplateField>
    <ItemTemplate>
        <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" CommandName="Imprimir" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn btn-secondary btn-sm" />
    </ItemTemplate>
</asp:TemplateField>





                </Columns>
            </asp:GridView>
        </div>
    </div>

    <!-- Modal para el reporte de pedido -->
    <div id="myModal" class="modal fade" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-fullscreen" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="myModalLabel">Pedido</h5>
                    <button type="button" class="btn-close" onclick="closeModal()" aria-label="Close"></button>
                </div>
                <div class="modal-body p-0">
                    <iframe id="reportFrame" style="width: 100%; height: 100vh; border: none;"></iframe>
                </div>
            </div>
        </div>
    </div>




<!-- Modal -->
<div id="opcionesModal" class="modal fade" tabindex="-1" aria-labelledby="opcionesModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="opcionesModalLabel">Seleccione una Acción</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                <p>Operando con el ID:  <span id="idCotizacionModal" class="fw-bold"></span></p>
                <div class="d-flex justify-content-around">
                    <button type="button" class="btn btn-danger" onclick="accionSeleccionada('Anularse')">Anularse</button>
                    <button type="button" class="btn btn-success" onclick="accionSeleccionada('Confirmar')">Confirmar</button>
           <button type="button" class="btn btn-primary" runat="server" onserverclick="btnPasarPedido_Click">Pasar a Pedido</button>
     </div>
            </div>
        </div>
    </div>
</div>






    <script>
        function abrirModalConID(idCotizacion) {
            console.log(`Modal abierto con ID: ${idCotizacion}`);
            // Muestra el ID en el modal
            document.getElementById('idCotizacionModal').innerText = idCotizacion;

            // Inicializa y muestra el modal
            const modal = new bootstrap.Modal(document.getElementById('opcionesModal'));
            modal.show();
        }
</script>




    <script>
        //function abrirModalConID() {
        //    const modal = new bootstrap.Modal(document.getElementById('opcionesModal'));
        //    modal.show();
        //}

        function accionSeleccionada(accion) {
            // Maneja la acción seleccionada
            alert(`Acción seleccionada: ${accion}`);
        }
</script>






    <script>
        function openModal(reportUrl) {
            $('#reportFrame').attr('src', reportUrl);
            $('#myModal').modal('show');
        }

        function closeModal() {
            $('#myModal').modal('hide');
            $('#reportFrame').attr('src', '');
        }
    </script>















        <style>
    body {
        background-image: url('images/fondo.jpg');
        background-size: cover;
        background-repeat: no-repeat;
        background-attachment: fixed;
        background-position: center;
    }
</style>

    <!-- Librerías actualizadas de jQuery y Bootstrap -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>

</asp:Content>
