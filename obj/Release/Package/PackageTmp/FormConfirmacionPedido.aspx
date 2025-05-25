<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormConfirmacionPedido.aspx.cs" Inherits="PedidosWebForm.FormConfirmacionPedido" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Formulario Modal</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
       <form id="form1" runat="server">
        <div>
            <h4>Confirmar Pedido</h4>
            <p>ID seleccionado: <asp:Label ID="lblID" runat="server" Text="ID"></asp:Label></p>
            <!-- Formulario con las acciones -->
            <asp:DropDownList ID="ddlAccion" runat="server" CssClass="form-select">
                <asp:ListItem Text="Anular" Value="Anular"></asp:ListItem>
                <asp:ListItem Text="Confirmar como Pedido" Value="Confirmar"></asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" CssClass="btn btn-primary" />
        </div>
    </form>
</body>
</html>
