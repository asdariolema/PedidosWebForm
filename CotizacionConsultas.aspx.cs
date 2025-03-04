using DAL;
using DAL.BDL;
using System;
using System.Data;
using System.Drawing;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

using static System.Net.Mime.MediaTypeNames;

namespace PedidosWebForm
{
    public partial class CotizacionConsultas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtTipoDocumento.Text = "Cotización Consulta";
                          

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
            DAL.Cotizacion Cotizacion = new DAL.Cotizacion();
            Cotizacion.idCliente = txtCodigoCliente.Text;
            Cotizacion.fechadesde = TextBox1.Text;
            Cotizacion.fechahasta = TextBox2.Text;
            Cotizacion.nombreCliente = txtRazonSocial.Text;
           BuscarPedidos(Cotizacion);
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

                    case "COTIZACION PASADA A PEDIDO ":
                        e.Row.Cells[5].ForeColor = System.Drawing.Color.Blue;
                        break;
                    // Puedes agregar más casos si es necesario
                    default:
                        // Color por defecto o manejo de otros casos
                        e.Row.Cells[5].ForeColor = System.Drawing.Color.Black;
                        break;
                }


                

              













            }
        }
        private void BuscarPedidos(DAL.Cotizacion Cotizacion)
        {



            Cotizacion.impresalegal = "1";
            Cotizacion.impresa = "1";
            Cotizacion.pedidoConCambios = "1";
            Cotizacion.retiraPedido = "1";
            // pedido.fechaAlta = txtValor.Text.Trim(); 

            DataTable dt = Cotizacion.GETCotizaSimple();


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
                    string cotizID = selectedRow.Cells[0].Text;

                    // Encriptamos el pedidoID


                    DAL.Encriptado OBJ = new DAL.Encriptado();

                    string valorid = OBJ.Encrypt(cotizID, "mlmwebSecKey2024");

                    



                    // Redirigimos a AltaPedidos.aspx con el parámetro encriptado
                    Response.Redirect($"AltaCotizacion.aspx?id={valorid}");
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



            else   if (e.CommandName == "Cambiar")
            {
                //// Capturar el ID del registro desde CommandArgument
               
                    // Captura el ID de la fila seleccionada desde CommandArgument
                    string idCotizacion = e.CommandArgument.ToString();
                string script = $"abrirModalConID('{idCotizacion}');";
                // Puedes usar el ID para realizar alguna lógica adicional, si es necesario
                // Registrar el script para abrir el modal


                Session["IdCotizacion"] = idCotizacion;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "abrirModalConID", script, true);
               


            }




        }

       
        public void  btnPasarPedido_Click(object sender, EventArgs e)
        {
            try
            {
                // Lógica para cambiar el estado en la base de datos
                // Por ejemplo, actualiza el estado según el ID
                string id = Session["IdCotizacion"].ToString();

                Console.WriteLine($"Cambiando estado de ID {e} ");

                Cotizacion pasarapedido = new     Cotizacion();
                pasarapedido.pasarapedido(id);

                // Aquí se realizaría la lógica real, como una llamada a la base de datos
                // CambiarEstadoEnBaseDeDatos(id, estado);

               
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine($"Error al cambiar el estado: {ex.Message}");
              
            }
        }

        //protected void btnCambiarEstado_Command(object sender, CommandEventArgs e)
        //{
        //    if (e.CommandName == "CambiarEstado")
        //    {
        //        string id = e.CommandArgument.ToString();

        //        // Almacenar el ID en una variable de sesión o construir directamente el enlace del modal
        //        string url = $"FormConfirmacionPedido.aspx?id={id}";

        //        // Generar un script para abrir el modal con el iframe cargando el formulario
        //        string script = $@"
        //    $('#modalFormulario .modal-body iframe').attr('src', '{url}');
        //    $('#modalFormulario').modal('show');";
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirModal", script, true);
        //    }
        //}

    }
}














