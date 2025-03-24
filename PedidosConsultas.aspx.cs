using DAL;
using DAL.BDL;
using System;
using System.Data;
using System.Drawing;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

using static System.Net.Mime.MediaTypeNames;

namespace PedidosWebForm
{
    public partial class PedidosConsultas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtTipoDocumento.Text = "Pedidos Consulta";
                          

                if (Session["ID_USUARIO"] == null)
            {
                // Si no hay usuario en sesión, redirige a la página de login
                Response.Redirect("Login.aspx");
            }

        }

        protected void btnBuscarPorFechas_Click(object sender, EventArgs e)
        {

            buscarpedidos();
        }

        private void buscarpedidos ()
        {

            DAL.Pedidos pedido = new DAL.Pedidos();


            pedido.fechadesde = TextBox1.Text;
            pedido.fechahasta = TextBox2.Text;
            pedido.pbuscafechaalta = "1";
            // BuscarPedidos(pedido);

        }



        protected void btnAbrirReporte_Click(object sender, EventArgs e)
        {
            // Redireccionar al WebForm2 que mostrará el Crystal Report
            Response.Redirect("frmImprimir.aspx");
        }

        protected void btnBuscarCodigoCliente_Click(object sender, EventArgs e)
        {
            DAL.Pedidos pedido = new DAL.Pedidos();
            pedido.idCliente = txtCodigoCliente.Text;
          //  BuscarPedidos(pedido);
        // BuscarPedidos( txtValor.Text , txtValor.Text);
    }
        



       protected void btnBuscarRazonSocial_Click(object sender, EventArgs e)
        {
            DAL.Pedidos pedido = new DAL.Pedidos();
            pedido.idCliente = txtCodigoCliente.Text;
            pedido.fechadesde = TextBox1.Text;
            pedido.fechahasta = TextBox2.Text;
            pedido.nombreCliente = txtRazonSocial.Text;
           BuscarPedidos(pedido);
            // BuscarPedidos( txtValor.Text , txtValor.Text);
        }
        protected void gvResultados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Verificamos si la fila es una fila de datos (no es encabezado o pie)
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Obtenemos el valor del campo Estado1
                string estado = DataBinder.Eval(e.Row.DataItem, "Estado1").ToString();
               

               


                // Si el valor del campo es "ACEPTADO", cambiamos el color del texto a azul
                switch (estado)
                {
                    case "ACEPTADO":
                        e.Row.Cells[5].ForeColor = System.Drawing.Color.Blue;
                        break;
                    case "PENDIENTE":
                        e.Row.Cells[5].ForeColor = System.Drawing.Color.Green;
                        break;
                    // Puedes agregar más casos si es necesario
                    default:
                        // Color por defecto o manejo de otros casos
                        e.Row.Cells[5].ForeColor = System.Drawing.Color.Black;
                        break;
                }


                

              













            }
        }
        private void BuscarPedidos(DAL.Pedidos pedido)
        {



            pedido.impresalegal = "1";
            pedido.impresa = "1";
            pedido.pedidoConCambios = "1";
            pedido.retiraPedido = "1";
            // pedido.fechaAlta = txtValor.Text.Trim(); 

            DataTable dt = pedido.GETpedidosSimple();


            gvResultados.DataSource = dt;
            gvResultados.DataBind();

        }



        //protected void txtCodCliente_TextChanged(object sender, EventArgs e)
        //{
        //    string codCliente = txtCodigoCliente.Text.Trim();

        //    // Realizar la consulta SQL
        //    //if (!string.IsNullOrEmpty(codCliente))
        //    //{
        //    //    DAL.Cliente cliente = new DAL.Cliente();
        //    //    cliente.Codigo = codCliente;

        //    //    // Ejecutar el método para obtener el cliente de la base de datos
        //    //    DataTable dt = cliente.BuscarPorCodigo(); // Supongamos que este método ejecuta la consulta SQL

        //    //    if (dt.Rows.Count > 0)
        //    //    {
        //    //        // Si se encuentra el cliente, llena los campos correspondientes
        //    //        DataRow row = dt.Rows[0];
        //    //        txtRazonSocial.Text = row["RazonSocial"].ToString();
        //    //        txtDireccion.Text = row["Direccion"].ToString();
        //    //        txtCUIT.Text = row["CUIT"].ToString();
        //    //    }
        //    //    else
        //    //    {
        //    //        // Si no se encuentra, mostrar un mensaje o limpiar los campos
        //    //        txtRazonSocial.Text = string.Empty;
        //    //        txtDireccion.Text = string.Empty;
        //    //        txtCUIT.Text = string.Empty;
        //    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No se encontró el cliente.');", true);
        //    //    }
        //    //}
        //}


        //    protected void gvResultados_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "Ver")
        //    {
        //        // Obtén el índice de la fila seleccionada
        //        int index = Convert.ToInt32(e.CommandArgument);
        //        // Obtén el ID del pedido o cualquier otra información necesaria de la fila
        //        GridViewRow selectedRow = gvResultados.Rows[index];
        //        string pedidoID = selectedRow.Cells[0].Text; // Asumiendo que el ID del pedido está en la primera celda

        //        // Redirige a la página de detalles
        //        Response.Redirect($"AltaPedidos.aspx?id={pedidoID}");
        //    }
        //    else if (e.CommandName == "Editar")
        //    {
        //        // Obtén el índice de la fila seleccionada
        //        int index = Convert.ToInt32(e.CommandArgument);
        //        // Obtén el ID del pedido o cualquier otra información necesaria de la fila
        //        GridViewRow selectedRow = gvResultados.Rows[index];
        //        string pedidoID = selectedRow.Cells[0].Text; // Asumiendo que el ID del pedido está en la primera celda

        //        // Redirige a la página de edición
        //        Response.Redirect($"edicionPedidos.aspx?id={pedidoID}");
        //    }
        //}




        protected void gvResultados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Verificamos si el comando es uno de los tres posibles: Abrir, Imprimir o Cambiar
            if (e.CommandName == "Abrir" || e.CommandName == "Imprimir" || e.CommandName == "Cambiar")
            {
                // Obtenemos la fila que disparó el comando
                GridViewRow selectedRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;

                // Verificamos si la fila tiene celdas y si el valor que necesitamos está presente
                if (selectedRow != null && selectedRow.Cells.Count > 0)
                {
                    // Tomamos el valor de la primera celda (ID del pedido) para Abrir o Imprimir
                    string pedidoID = selectedRow.Cells[0].Text;

                    if (e.CommandName == "Abrir")
                    {
                        // Encriptamos el pedidoID
                        DAL.Encriptado OBJ = new DAL.Encriptado();
                        string valorid = OBJ.Encrypt(pedidoID, "mlmwebSecKey2024");

                        // Redirigimos a AltaPedidos.aspx con el parámetro encriptado
                        Response.Redirect($"AltaPedidos.aspx?id={valorid}");
                    }
                    else if (e.CommandName == "Imprimir")
                    {
                        // Generamos la URL del reporte
                        string reportUrl = $"ReporteForm.aspx?ReportName=Pedidos&idpedido={pedidoID}";

                        // Abrimos el reporte en una nueva ventana
                        string script = $"window.open('{reportUrl}', '_blank');";
                        ClientScript.RegisterStartupScript(this.GetType(), "OpenReport", script, true);
                    }
                    else if (e.CommandName == "Cambiar")
                    {
                        // Captura el ID de la fila seleccionada desde CommandArgument
                        string idpedido = e.CommandArgument.ToString();

                        // Si se necesita realizar alguna lógica adicional, se puede hacer aquí

                        // Registrar el ID en la sesión
                        Session["Idpedido"] = idpedido;

                        // Ejecutar el script para abrir el modal
                        string script = $"abrirModalConID('{idpedido}');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "abrirModalConID", script, true);
                    }
                }
                else
                {
                    // En caso de que no se encuentre la fila o no tenga las celdas necesarias
                    // Esto es solo un ejemplo de validación
                    // Puedes manejar este caso de acuerdo a las necesidades de tu aplicación
                    Response.Write("No se encontró la fila o las celdas necesarias.");
                }
            }
        }




        public void btnPasarPedido_Click(object sender, EventArgs e)
        {
            try
            {
                // Lógica para cambiar el estado en la base de datos
                // Por ejemplo, actualiza el estado según el ID
                string id = Session["IdPedido"].ToString();

                Console.WriteLine($"Cambiando estado de ID {e} ");

               DAL. Pedidos pasarapedido = new DAL.Pedidos();
                pasarapedido.estado= id;
                pasarapedido.idPedido = id;
                pasarapedido.pasarapedido();

                DAL.Pedidos pedido = new DAL.Pedidos();
                pedido.idCliente = txtCodigoCliente.Text;
                pedido.fechadesde = TextBox1.Text;
                pedido.fechahasta = TextBox2.Text;
                pedido.nombreCliente = txtRazonSocial.Text;
                BuscarPedidos(pedido);


            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine($"Error al cambiar el estado: {ex.Message}");

            }
        }






        //protected void gvResultados_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "Abrir" || e.CommandName == "Imprimir")
        //    {
        //        // Obtenemos la fila que disparó el comando
        //        GridViewRow selectedRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;

        //        // Tomamos el valor de la primera celda (ID del pedido)
        //        string pedidoID = selectedRow.Cells[0].Text;

        //        if (e.CommandName == "Abrir")
        //        {
        //            // Encriptamos el pedidoID
        //            DAL.Encriptado OBJ = new DAL.Encriptado();
        //            string valorid = OBJ.Encrypt(pedidoID, "mlmwebSecKey2024");

        //            // Redirigimos a AltaPedidos.aspx con el parámetro encriptado
        //            Response.Redirect($"AltaPedidos.aspx?id={valorid}");
        //        }
        //        else if (e.CommandName == "Imprimir")
        //        {
        //            // Generamos la URL del reporte
        //            string reportUrl = $"ReporteForm.aspx?ReportName=Pedidos&idpedido={pedidoID}";

        //            // Abrimos el reporte en una nueva ventana
        //            string script = $"window.open('{reportUrl}', '_blank');";
        //            ClientScript.RegisterStartupScript(this.GetType(), "OpenReport", script, true);
        //        }


        //        else if (e.CommandName == "Cambiar")
        //        {
        //            //// Capturar el ID del registro desde CommandArgument

        //            // Captura el ID de la fila seleccionada desde CommandArgument
        //            string idpedido = e.CommandArgument.ToString();
        //            string script = $"abrirModalConID('{idpedido}');";
        //            // Puedes usar el ID para realizar alguna lógica adicional, si es necesario
        //            // Registrar el script para abrir el modal


        //            Session["Idpedido"] = idpedido;

        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "abrirModalConID", script, true);



        //        }
        //    }
        //}




        //protected void gvResultados_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "Abrir")
        //    {
        //        // Validamos que el CommandArgument es un número válido
        //        if (int.TryParse(e.CommandArgument.ToString(), out int index))
        //        {
        //            // Obtenemos la fila seleccionada
        //            GridViewRow selectedRow = gvResultados.Rows[index];
        //            string pedidoID = selectedRow.Cells[0].Text;

        //            // Encriptamos el pedidoID


        //            DAL.Encriptado OBJ = new DAL.Encriptado();

        //            string valorid = OBJ.Encrypt(pedidoID, "mlmwebSecKey2024");





        //            // Redirigimos a AltaPedidos.aspx con el parámetro encriptado
        //            Response.Redirect($"AltaPedidos.aspx?id={valorid}");
        //        }
        //        else
        //        {
        //            // Manejo de error en caso de que CommandArgument no sea válido
        //            Response.Write("Error: CommandArgument no es un número válido.");
        //        }
        //    }
        //    else if (e.CommandName == "Imprimir")
        //    {
        //        // Validamos que el CommandArgument es un número válido
        //        if (int.TryParse(e.CommandArgument.ToString(), out int index))
        //        {
        //            // Obtenemos la fila seleccionada
        //            GridViewRow selectedRow = gvResultados.Rows[index];
        //            string pedidoID = selectedRow.Cells[0].Text;

        //            // Generamos la URL del reporte
        //            string reportUrl = $"ReporteForm.aspx?ReportName=Pedidos&idpedido={pedidoID}";

        //            // Abrimos el reporte en una nueva ventana
        //            string script = $"window.open('{reportUrl}', '_blank');";
        //            ClientScript.RegisterStartupScript(this.GetType(), "OpenReport", script, true);
        //        }
        //        else
        //        {
        //            // Manejo de error en caso de que CommandArgument no sea válido
        //            Response.Write("Error: CommandArgument no es un número válido.");
        //        }
        //    }
        //}



    }
}