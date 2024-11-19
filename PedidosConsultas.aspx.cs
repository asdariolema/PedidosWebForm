using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DAL;
using System.Web.UI.WebControls;
using System.Web.UI;

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

            DAL.Pedidos pedido = new DAL.Pedidos();
            
            
            pedido . fechadesde= TextBox1.Text;
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
            if (e.CommandName == "Abrir")
            {
                // Validamos que el CommandArgument es un número válido
                if (int.TryParse(e.CommandArgument.ToString(), out int index))
                {
                    // Obtenemos la fila seleccionada
                    GridViewRow selectedRow = gvResultados.Rows[index];
                    string pedidoID = selectedRow.Cells[0].Text;

                    // Encriptamos el pedidoID
                    string valorid = DAL.SQL.Encrypt(pedidoID, "mlmweb");

                    // Redirigimos a AltaPedidos.aspx con el parámetro encriptado
                    Response.Redirect($"AltaPedidos.aspx?id={valorid}");
                }
                else
                {
                    // Manejo de error en caso de que CommandArgument no sea válido
                    Response.Write("Error: CommandArgument no es un número válido.");
                }
            }
            else if (e.CommandName == "Imprimir")
            {
                // Validamos que el CommandArgument es un número válido
                if (int.TryParse(e.CommandArgument.ToString(), out int index))
                {
                    // Obtenemos la fila seleccionada
                    GridViewRow selectedRow = gvResultados.Rows[index];
                    string pedidoID = selectedRow.Cells[0].Text;

                    // Generamos la URL del reporte
                    string reportUrl = $"ReporteForm.aspx?ReportName=Pedidos&idpedido={pedidoID}";

                    // Abrimos el reporte en una nueva ventana
                    string script = $"window.open('{reportUrl}', '_blank');";
                    ClientScript.RegisterStartupScript(this.GetType(), "OpenReport", script, true);
                }
                else
                {
                    // Manejo de error en caso de que CommandArgument no sea válido
                    Response.Write("Error: CommandArgument no es un número válido.");
                }
            }
        }



    }
}