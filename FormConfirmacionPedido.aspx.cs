using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PedidosWebForm
{
    public partial class FormConfirmacionPedido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnGuardar_Click();

        }

        protected void btnGuardar_Click()
        {
            //string accionSeleccionada = ddlAccion.SelectedValue;

            //// Procesar la acción seleccionada
            //if (accionSeleccionada == "Anular")
            //{
            //    // Lógica para Anular
            //    Response.Write("<script>alert('Se seleccionó Anular.');</script>");
            //}
            //else if (accionSeleccionada == "Confirmar")
            //{
            //    // Lógica para Confirmar como Pedido
            //    Response.Write("<script>alert('Se seleccionó Confirmar como Pedido.');</script>");
            //}
        }
    }

}