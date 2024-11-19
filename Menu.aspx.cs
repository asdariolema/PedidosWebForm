using System;

public partial class Menu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["ID_USUARIO"] == null)
        {
            // Si no hay usuario en sesión, redirige a la página de login
            Response.Redirect("Login.aspx");
        }
        // Código para manejar la carga de la página si es necesario
    }
}
