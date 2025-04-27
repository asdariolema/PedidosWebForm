<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeBehind="ConsultaMercadoLibre.aspx.cs" Inherits="PedidosWebForm.ConsultaMercadoLibre" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Consulta MercadoLibre</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnAutorizar" runat="server" Text="Autorizar MercadoLibre" OnClick="btnAutorizar_Click" />
            <br /><br />

            <asp:Label ID="lblResultado" runat="server" Text=""></asp:Label>
            <br /><br />

            <asp:TextBox ID="txtBusqueda" runat="server" Placeholder="Buscar productos..." Width="300px"></asp:TextBox>
          <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClickAsync="btnBuscar_Click" />

            <br /><br />

            <asp:GridView ID="gvResultados" runat="server" AutoGenerateColumns="true" />
        </div>
    </form>
</body>
</html>
