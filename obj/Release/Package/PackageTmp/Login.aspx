<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PedidosWebForm.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Dommarco</title>

    <!-- Bootstrap core CSS -->
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" type="text/css" />

    <!-- CSS de Animate.css (si es necesario para animaciones) -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" rel="stylesheet" type="text/css" />

    
  
    <style type="text/css">
        html, body {
            height: 100%;
            width: 100%;
            margin: 0;
            padding: 0;
            background: url('images/fondo2.jpg') no-repeat center center fixed;
            background-size: cover;
            font-family: 'Raleway', sans-serif;
        }

        .login-form input {
            margin: 0 auto 7px;
            display: block;
            width: 97% !important;
            background: #d6d6d6;
            border: none;
            color: #6c6c6c;
            padding: 8px;
            font-family: 'Raleway', sans-serif;
        }

        .btn {
            margin-bottom: 14px;
            padding: 8px 16px;
            background-color: #428bca;
            border-color: #357ebd;
            color: #fff;
            width: 100%;
        }

        .login-links a {
            font-family: 'Lato', sans-serif;
            color: #464343;
            font-size: 15px;
        }

        .login-links a:hover {
            text-decoration: none;
            color: #1E5B8E;
        }

        .login-box {
            max-width: 480px;
            background: rgba(255, 255, 255, 0.8);
            border-radius: 8px;
            padding: 20px;
            box-shadow: 0px 0px 1pc #000;
            margin: 60px auto;
        }

        .login-box hr {
            margin: 10px auto 20px;
            width: 70%;
            border-top: 1px solid #C5C5C5;
        }

        .alert {
            margin: 10px auto;
            width: 90%;
            border-radius: 4px;
            font-family: 'Lato', sans-serif;
            box-shadow: 0px 0px 1pc #000;
        }

        .login-logo a:hover {
            opacity: 0.7;
        }

        .login-logo a {
            opacity: 0.6;
        }

        #lblsist {
            display: inline-block;
            max-width: 100%;
            margin-bottom: 5px;
            font-weight: bold;
            font-family: 'Lato', sans-serif;
            font-size: 30px;
            padding-left: 47px;
            color: #444343;
        }
    </style>

   
</head>
<body>
    <!-- start Login box -->
    <form id="login" runat="server">
        <asp:Panel ID="Window1" runat="server" Style="font-size: small">
            <div class="container">
                <div class="row">
                    <div class="col-md-6 offset-md-3">
                        <div class="login-box clearfix animate__animated animate__flipInX">
                            <div class="login-logo text-center">
                                <a href="#">
                                    <img src="images/logo-dommarco.png" style="max-width: 150px;"/>
                                </a>
                            </div>
                            <hr />
                            <div class="login-form form-horizontal">
                                <div class="input-group mb-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text" id="basic-addon1"><i class="glyphicon glyphicon-user"></i></span>
                                    </div>
                                    <input id="txtUsername" class="form-control" runat="server" type="text" placeholder="Usuario" aria-label="Usuario" aria-describedby="basic-addon1" />
                                    <asp:RequiredFieldValidator ID="rfvUS_NAME" runat="server" ControlToValidate="txtUsername"
                                        ErrorMessage="Ingrese Usuario" ForeColor="red" ValidationGroup="a">&nbsp</asp:RequiredFieldValidator>
                                </div>
                                <div class="input-group mb-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text" id="basic-addon2"><i class="glyphicon glyphicon-lock"></i></span>
                                    </div>
                                    <input id="txtPassword" class="form-control" runat="server" type="password" placeholder="Contraseña" aria-label="Contraseña" aria-describedby="basic-addon2" />
                                    <asp:RequiredFieldValidator ID="rfvUS_NAME0" runat="server" ControlToValidate="txtPassword"
                                        ErrorMessage="Ingrese Contraseña" ForeColor="red" ValidationGroup="a">&nbsp</asp:RequiredFieldValidator>
                                </div>
                                <div>
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ForeColor="Red" ShowMessageBox="True"
                                        ShowSummary="False" Style="height: auto" ValidationGroup="a" />
                                </div>
                                <div class="d-flex justify-content-between">
                                    <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary" Text="Aceptar"
                                        OnClick="btnLogin_Click" ValidationGroup="a" OnClientClick="validador.validar();" />
                                    <asp:Button ID="btnCambiarPass" runat="server" CssClass="btn btn-secondary" OnClick="btnCambiarPass_Click"
                                        Text="Cambiar Contraseña" />
                                </div>
                                <div class="login-links mt-3 text-center">
                                    <%-- <asp:LinkButton ID="lnkRepPerf" runat="server" OnClick="lnkRepPerf_Click">Reporte de Perfiles</asp:LinkButton> --%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <!-- Panel para cambiar contraseña -->
        <asp:Panel ID="wnNuevaPass" runat="server" Visible="false" Style="font-size: small">
            <div class="container">
                <div class="row">
                    <div class="col-md-6 offset-md-3">
                        <div class="login-box clearfix animate__animated animate__flipInX">
                            <div class="login-form">
                                <br />
                                <input id="txtNewUsuario" class="form-control" runat="server" type="text" placeholder="Usuario" />
                                <asp:RequiredFieldValidator ID="rfvUS_NAME1" runat="server" ControlToValidate="txtNewUsuario"
                                    ErrorMessage="Ingrese Usuario" ForeColor="red" ValidationGroup="b">&nbsp</asp:RequiredFieldValidator>
                                <input id="txtActual" class="form-control" runat="server" type="password" placeholder="Contraseña" />
                                <asp:RequiredFieldValidator ID="rfvUS_NAME2" runat="server" ControlToValidate="txtActual"
                                    ErrorMessage="Ingrese Contraseña" ForeColor="Black" ValidationGroup="b">&nbsp</asp:RequiredFieldValidator>
                                <input id="txtNewPass" class="form-control" runat="server" type="password" placeholder="Nueva Contraseña" />
                                <asp:RequiredFieldValidator ID="rfvUS_NAME3" runat="server" ControlToValidate="txtNewPass"
                                    ErrorMessage="Ingrese Nueva Contraseña" ForeColor="Black" ValidationGroup="b">&nbsp</asp:RequiredFieldValidator>
                                <input id="txtNewPassConf" class="form-control" runat="server" type="password" placeholder=" Confirmar Nueva Contraseña" />
                                <asp:RequiredFieldValidator ID="rfvUS_NAME4" runat="server" ControlToValidate="txtNewPassConf"
                                    ErrorMessage="Ingrese Confirmación de Nueva Contraseña" ForeColor="Black" ValidationGroup="b">&nbsp</asp:RequiredFieldValidator>
                                <div>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtNewPass"
                                        ControlToValidate="txtNewPassConf" ErrorMessage="Las contraseñas no coinciden"
                                        ForeColor="Black" Style="color: #CC0000" CssClass="concipass">Las contraseñas no coinciden</asp:CompareValidator>
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ShowMessageBox="True"
                                        ShowSummary="False" Style="height: auto" ValidationGroup="b" />
                                </div>
                                <div class="d-flex justify-content-between">
                                    <asp:Button ID="btnAceptarNew" runat="server" CssClass="btn btn-primary" OnClick="btnAceptarNew_Click"
                                        OnClientClick="validador.validar();" Text="Aceptar" ValidationGroup="b" />
                                    <asp:Button ID="btnCancelarNew" runat="server" CssClass="btn btn-secondary" OnClick="btnCancelarNew_Click"
                                        Text="Cancelar" CausesValidation="False" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <!-- Panel de mensaje de error -->
        <asp:Panel ID="pnlMensaje" runat="server" CssClass="alert alert-danger mensaje-error"
            Visible="false">
            <asp:Literal ID="ltrMensaje" runat="server" Text="Este es un texto de prueba...."></asp:Literal>
        </asp:Panel>
        <asp:Label ID="lblMessage" runat="server" />
        <!-- End Login box -->
    </form>
</body>
</html>
