
using DAL;
using DAL.BDL;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using static System.Net.Mime.MediaTypeNames;


namespace PedidosWebForm
{
    public partial class AltaCotizacion : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ID_USUARIO"] == null)
            {
                // Si no hay usuario en sesión, redirige a la página de login
                Response.Redirect("Login.aspx");
            }


            if (!IsPostBack)
            {

                string parametroId =  Request.QueryString["id"];
                
                if (!string.IsNullOrEmpty(parametroId) )
                {
                    // Si el ID es válido y mayor que cero, procedemos con la edición
                    DAL.Encriptado obj = new DAL.Encriptado();

                    ViewState["parametro"] = obj.Decrypt(parametroId, "mlmwebSecKey2024"); 
                    ViewState["tipo"] = "EDICION";
                    txtTipoDocumento.Text = "EDICIÓN NRO: " + ViewState["parametro"];
                    editar(ViewState["parametro"].ToString());
                }
                else
                {

                    // Inicializar las variables de estado
                  
                    ViewState["tipo"] = "NUEVO"; // Por defecto, asumimos que es NUEVO

                    txtTipoDocumento.Text = "ALTA";

                    // Configura la fecha del pedido a la actual y carga los estados y unidades de medida
                    txtFechaPedido.Text = DateTime.Now.ToString("dd-MM-yyyy");
                    CargarEstados();
                    CargarUnidMed();
                    CargarArtidulosDescripcion();
                    CargarUnidMedEspesor();
                    CargarUnidMedLargo();
                    CargarUnidMedAncho();
                    // Inicializar la tabla de artículos si es la primera vez que se carga la página
                    if (ViewState["Articulos"] == null)
                    {
                        DataTable dtArticulos = new DataTable();
                        dtArticulos.Columns.Add("Cantidad");
                        dtArticulos.Columns.Add("Descripcion");
                        dtArticulos.Columns.Add("Detalle");
                        dtArticulos.Columns.Add("Unidad");
                        dtArticulos.Columns.Add("Tasa");
                        dtArticulos.Columns.Add("PrecioUnitario");
                        dtArticulos.Columns.Add("PrecioTotal");
                        ViewState["Articulos"] = dtArticulos;
                    }





                    gvArticulos.DataSource = ViewState["Articulos"];
                    gvArticulos.DataBind();
                }
            }

        }






        private void CargarReporte()
        {
            //try
            //{
            //    // Ruta del archivo .rpt (el reporte que diseñaste previamente)
            //    string rutaReporte = Server.MapPath("~/Pedidos.rpt");

            //    // Crear una instancia del ReportDocument
            //    ReportDocument reporte = new ReportDocument();
            //    reporte.Load(rutaReporte);

            //    // Si el reporte tiene parámetros, puedes pasarlos aquí (opcional)
            //    // reporte.SetParameterValue("NombreParametro", valor);

            //    // Configurar la conexión a la base de datos
            //    ConnectionInfo conexionInfo = new ConnectionInfo()
            //    {
            //        ServerName = "DESKTOP-RIH5NB8\\SQLEXPRESS",
            //        DatabaseName = "Dom",
            //        UserID = "sa",
            //        Password = "sasaSig"
            //    };

            //    // Asignar la información de conexión a cada tabla del reporte
            //    foreach (Table tabla in reporte.Database.Tables)
            //    {
            //        TableLogOnInfo infoLog = tabla.LogOnInfo;
            //        infoLog.ConnectionInfo = conexionInfo;
            //        tabla.ApplyLogOnInfo(infoLog);
            //    }

            //    // Establecer el reporte en el CrystalReportViewer para visualizar
            //    CrystalReportViewer1.ReportSource = reporte;

            //    // Nombre de la impresora (modifica por el nombre correcto en tu sistema)
            //    string printerName = "Microsoft Print to PDF"; // Cambia por tu impresora real

            //    // Verificar si la impresora está configurada
            //    if (!string.IsNullOrEmpty(printerName))
            //    {
            //        reporte.PrintOptions.PrinterName = printerName;

            //        // Intentar imprimir el informe
            //        reporte.PrintToPrinter(1, false, 0, 0);
            //    }
            //    else
            //    {
            //        // Enviar un mensaje si no se ha configurado la impresora
            //        Response.Write("La impresora no está configurada correctamente.");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // Manejar errores y mostrar mensaje en la página
            //    Response.Write("Ocurrió un error al cargar o imprimir el reporte: " + ex.Message);
            //}
        }





        private void editar(string id)
        {

            try
            {
                CargarEstados();
                CargarUnidMed();

                DAL.Cotizacion cotizacion = new DAL.Cotizacion();
                cotizacion.idCotiz = id;
                DataTable ds = cotizacion.GETpedidos();
                txtFechaPedido.Text = ds.Rows[0]["fechaalta"].ToString();
                txtCodCliente.Text = ds.Rows[0]["NU_CLI_CODIGO"].ToString();

                ddlEstado.SelectedValue = ds.Rows[0]["estado"].ToString();

                llenardatoscliente(ds.Rows[0]["NU_CLI_CODIGO"].ToString());

                llenardatosdelpedido(ds);
                CalcularSumasEdicion(ds);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Este pedido no tiene contenido: " + ex.Message + "');", true);
            }


        }


        protected void txtCodCliente_TextChanged(object sender, EventArgs e)
        {
            llenardatoscliente(txtCodCliente.Text );



        }


        private void llenardatoscliente(string codcliente)
        {
            Cliente cli = new Cliente();
            cli.NU_CLI_CODIGO = codcliente;

            DataTable ds = cli.GETcLIENTE();

            if (ds.Rows.Count > 0)
            {
                txtRazonSocial.Text = ds.Rows[0]["ds_cli_razon_social"].ToString();

                txtDireccion.Text = ds.Rows[0]["DS_CLI_DIRECCION"].ToString();
                txtCUIT.Text = ds.Rows[0]["DS_cli_cuit"].ToString();
            }
            else
            {
                txtRazonSocial.Text = "";
                txtDireccion.Text = "";
                txtCUIT.Text = "";
            }



        }









        private void llenardatosdelpedido( DataTable datos)
        {

            DataTable dtArticulos = ViewState["Articulos"] as DataTable;

            if (dtArticulos == null)
            {
                dtArticulos = new DataTable();
                dtArticulos.Columns.Add("Codigo");
                dtArticulos.Columns.Add("Detalle");
                dtArticulos.Columns.Add("Descripcion");
                dtArticulos.Columns.Add("Cantidad");
                dtArticulos.Columns.Add("Unidad");
                dtArticulos.Columns.Add("Tasa");
                dtArticulos.Columns.Add("PrecioUnitario");
                dtArticulos.Columns.Add("PrecioTotal");
            }



            foreach (DataRow row in datos.Rows)
            {

                DataRow dr = dtArticulos.NewRow();
                dr["Descripcion"] = row["desc1"];
                dr["Detalle"] = row["desc2"];
                dr["Cantidad"] = row["Cant"];
                dr["Unidad"] = row["medida"];
                dr["PrecioUnitario"] = row["punit"];
                dr["PrecioTotal"] = row["PTotal"];

                dtArticulos.Rows.Add(dr);
           


            }


            TextIdLocalidadEntrega.Text = datos.Rows[0]["localidad"].ToString();
            TextPciaEntrega.Text = datos.Rows[0]["provincia"].ToString();
            TextContacto.Text = datos.Rows[0]["contactoobra"].ToString();
            txtDireccionEntrega.Text = datos.Rows[0]["direccionEntrega"].ToString();
            ViewState["Articulos"] = dtArticulos;
            gvArticulos.DataSource = dtArticulos;
            gvArticulos.DataBind();



        }

        private void CargarArtidulosDescripcion()
        {
            DAL.Articulo articulo = new DAL.Articulo();
            
            DataTable dt = articulo.GETArticulo();

            ddldescripcion.DataSource = dt;
            ddldescripcion.DataTextField = "ART_DESCRIPCION";
            ddldescripcion.DataValueField = "ID_ARTICULO";
            ddldescripcion.DataBind();
        }


        private void CargarEstados()
        {
            DAL.Estados ESTADO = new DAL.Estados();
            ESTADO.estado = null;

            DataTable dt = ESTADO.GETESTADOS();

            ddlEstado.DataSource = dt;
            ddlEstado.DataTextField = "ESTADO";
            ddlEstado.DataValueField = "ID";
            ddlEstado.DataBind();
        }

        private void CargarUnidMed()
        {
            DAL.Estados UNIMED = new DAL.Estados();
            UNIMED.DESCRIPCION = null;

            DataTable dt = UNIMED.GETUNIMED();

            ddlunidad.DataSource = dt;
            ddlunidad.DataTextField = "DESCRIPCION";
            ddlunidad.DataValueField = "ID";
            ddlunidad.DataBind();

            // Añadir un ítem predeterminado
            //ddlunidad.Items.Insert(0, new ListItem("", "0"));
        }
        private void CargarUnidMedEspesor()
        {
            DAL.UnidadesMedida UNIMED = new DAL.UnidadesMedida();
            UNIMED.DESCRIPCION = null;

            DataTable dt = UNIMED.GETUNIMED();

            ddlEspesor.DataSource = dt;
            ddlEspesor.DataTextField = "DESCRIPCION";
            ddlEspesor.DataValueField = "ID";
            ddlEspesor.DataBind();

            // Añadir un ítem predeterminado
            //ddlunidad.Items.Insert(0, new ListItem("", "0"));
        }

        private void CargarUnidMedLargo()
        {
            DAL.UnidadesMedida UNIMED = new DAL.UnidadesMedida();
            UNIMED.DESCRIPCION = null;

            DataTable dt = UNIMED.GETUNIMED();

            ddlLargo.DataSource = dt;
            ddlLargo.DataTextField = "DESCRIPCION";
            ddlLargo.DataValueField = "ID";
            ddlLargo.DataBind();

            // Añadir un ítem predeterminado
            //ddlunidad.Items.Insert(0, new ListItem("", "0"));
        }
        private void CargarUnidMedAncho()
        {
            DAL.UnidadesMedida UNIMED = new DAL.UnidadesMedida();
            UNIMED.DESCRIPCION = null;

            DataTable dt = UNIMED.GETUNIMED();

            ddlAncho.DataSource = dt;
            ddlAncho.DataTextField = "DESCRIPCION";
            ddlAncho.DataValueField = "ID";
            ddlAncho.DataBind();

            // Añadir un ítem predeterminado
            //ddlunidad.Items.Insert(0, new ListItem("", "0"));
        }

        protected void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            // Abrir el modal
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "$('#clientesModal').modal('show');", true);
        }

        protected void btnFiltrarClientes_Click(object sender, EventArgs e)
        {
            // Filtrar clientes basados en la caja de texto en el modal
            DAL.Cliente CLIENTE = new DAL.Cliente();
            CLIENTE.DS_CLI_RAZON_SOCIAL = txtBuscarRazonSocial.Text;

            DataTable dt = CLIENTE.GETcLIENTE(); // Obtener datos reales de tu base de datos

            gvClientes.DataSource = dt;
            gvClientes.DataBind();

            // Mantener el modal abierto después de filtrar
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "$('#clientesModal').modal('show');", true);
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["tipo"].ToString() == "NUEVO")
                {
                    NUEVO();

                    
                   

                }

                if (ViewState["tipo"].ToString() == "EDICION")
                { 
                    EDICION();

                }




            }
            catch (Exception ex)
            {
                // Manejo de errores
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Ocurrió un error al ingresar el pedido: " + ex.Message + "');", true);
            }
        }

        private void EDICION ()
        {
            
                // Insertar el pedido
                DAL.Cotizacion Cotizacion = new DAL.Cotizacion();
            Cotizacion.idCotiz = ViewState["parametro"].ToString();
            Cotizacion.nombreCliente = txtRazonSocial.Text;
            Cotizacion.direccionEntrega = txtDireccion.Text;
            Cotizacion.fechaAlta = txtFechaPedido.Text;
            Cotizacion.idCliente = txtCodCliente.Text;
            Cotizacion.localidadentrega = TextIdLocalidadEntrega.Text;
            Cotizacion.provincia = TextPciaEntrega.Text;
            Cotizacion.contactoObra = TextContacto.Text;

            Cotizacion.estado = ddlEstado.SelectedValue;

                // Acceder al valor de la columna "Total" en el DataTable de la grilla de sumas
                DataTable dtSumas = ViewState["Sumas"] as DataTable;
                if (dtSumas != null && dtSumas.Rows.Count > 0)
                {
                    decimal totalPedido = Convert.ToDecimal(dtSumas.Rows[0]["Total"]);



                Cotizacion.importetotal = totalPedido.ToString("0.00", new System.Globalization.CultureInfo("es-AR"));

                }



                DataTable ds = Cotizacion.UpadateCotiz();



                DAL.CotizContenido CotizCont = new DAL.CotizContenido();


            CotizCont.IDCOTIZ= (ViewState["parametro"].ToString());
            CotizCont.CotizCont_del();

            foreach (GridViewRow row in gvArticulos.Rows)
                {
                CotizCont.IDCOTIZ = ViewState["parametro"].ToString();
                CotizCont.CANT = row.Cells[0].Text;
              

                CotizCont.DESC1 = HttpUtility.HtmlDecode(row.Cells[1].Text);
                CotizCont.DESC2 = HttpUtility.HtmlDecode(row.Cells[2].Text);
                CotizCont.MEDIDA = row.Cells[3].Text;
                CotizCont.PUNIT = row.Cells[4].Text;

                CotizCont.PTOTAL = row.Cells[5].Text;
                CotizCont.CotizCont_INS();
                }

                // Mostrar mensaje de éxito
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(La actualización de la cotización fue ingresada.');", true);

                // Blanquear los campos
                txtCodCliente.Text = string.Empty;
                txtRazonSocial.Text = string.Empty;
                txtDireccion.Text = string.Empty;
                txtCUIT.Text = string.Empty;
                ddlEstado.SelectedIndex = 0;
                limpiarcampos();

                // Limpiar las grillas
                DataTable dtArticulos = ViewState["Articulos"] as DataTable;
                if (dtArticulos != null)
                {
                    dtArticulos.Clear();
                    ViewState["Articulos"] = dtArticulos;
                }
                gvArticulos.DataSource = dtArticulos;
                gvArticulos.DataBind();

                // Limpiar la grilla de sumas
                gvSumas.DataSource = null;
                gvSumas.DataBind();


            }



            private void NUEVO ()
        {  // Insertar el pedido
            DAL.Cotizacion Cotizacion = new DAL.Cotizacion();
            Cotizacion.nombreCliente = txtRazonSocial.Text;
         
            Cotizacion.fechaAlta = txtFechaPedido.Text;
            Cotizacion.idCliente = txtCodCliente.Text;
            Cotizacion.idCotizacion = "1";
            Cotizacion.estado = ddlEstado.SelectedValue;

            Cotizacion.direccionEntrega = txtDireccionEntrega.Text;
            Cotizacion.localidadentrega =  TextIdLocalidadEntrega.Text;
            Cotizacion.provincia = TextPciaEntrega.Text;
            Cotizacion.contactoObra = TextContacto.Text;


            // Acceder al valor de la columna "Total" en el DataTable de la grilla de sumas
            DataTable dtSumas = ViewState["Sumas"] as DataTable;
            if (dtSumas != null && dtSumas.Rows.Count > 0)
            {
                decimal totalCotizacion = Convert.ToDecimal(dtSumas.Rows[0]["Total"]);



                Cotizacion.importetotal = totalCotizacion.ToString("0.00", new System.Globalization.CultureInfo("es-AR"));

            }



            DataTable ds = Cotizacion.InsertCotiz();

            DAL.CotizContenido COTIZCONT = new DAL.CotizContenido();

            foreach (GridViewRow row in gvArticulos.Rows)
            {

                

                COTIZCONT.IDCOTIZ = ds.Rows[0][0].ToString();
                COTIZCONT.CANT = row.Cells[0].Text;
                COTIZCONT.DESC1 = HttpUtility.HtmlDecode(row.Cells[1].Text);
                COTIZCONT.DESC2 = HttpUtility.HtmlDecode(row.Cells[2].Text);
                COTIZCONT.MEDIDA = row.Cells[3].Text;

                COTIZCONT.PUNIT = row.Cells[4].Text;
                //PEDCONT.TASA = row.Cells[4].Text;
                COTIZCONT.PTOTAL = row.Cells[5].Text;
                COTIZCONT.CotizCont_INS();
            }

            // Mostrar mensaje de éxito
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('La Cotización fue ingresado.');", true);

            // Blanquear los campos
            txtCodCliente.Text = string.Empty;
            txtRazonSocial.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtCUIT.Text = string.Empty;
            ddlEstado.SelectedIndex = 0;


            limpiarcampos();

            // Limpiar las grillas
            DataTable dtArticulos = ViewState["Articulos"] as DataTable;
            if (dtArticulos != null)
            {
                dtArticulos.Clear();
                ViewState["Articulos"] = dtArticulos;
            }
            gvArticulos.DataSource = dtArticulos;
            gvArticulos.DataBind();

            // Limpiar la grilla de sumas
            gvSumas.DataSource = null;
            gvSumas.DataBind();

        }

















        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            DataTable dtArticulos = ViewState["Articulos"] as DataTable;

            if (dtArticulos == null)
            {
                dtArticulos = new DataTable();
                dtArticulos.Columns.Add("Codigo");
                 dtArticulos.Columns.Add("Detalle");
                dtArticulos.Columns.Add("Descripcion");
                dtArticulos.Columns.Add("Cantidad");
                dtArticulos.Columns.Add("Unidad");
               
                dtArticulos.Columns.Add("PrecioUnitario");
                dtArticulos.Columns.Add("PrecioTotal");
            }

            decimal cantidad = 0;
            decimal precioUnitario = 0;
            decimal precioTotal = 0;

            if (decimal.TryParse(txtCantidad.Text, out cantidad) && decimal.TryParse(txtPrecioUnitario.Text, out precioUnitario))
            {
                precioTotal = cantidad * precioUnitario;
            }

            DataRow dr = dtArticulos.NewRow();
           // dr["Descripcion"] = txtDescripcion.Text;
           // dr["Detalle"] = ddl.Text;
            dr["Cantidad"] = txtCantidad.Text ;
            dr["Unidad"] = ddlunidad.SelectedItem.Text;
            dr["PrecioUnitario"] = txtPrecioUnitario.Text;
            dr["PrecioTotal"] = precioTotal.ToString("N2");

            dtArticulos.Rows.Add(dr);
            ViewState["Articulos"] = dtArticulos;

            gvArticulos.DataSource = dtArticulos;
            gvArticulos.DataBind();

            if (gvArticulos.PageCount > 1)
            {
                gvArticulos.PageIndex = gvArticulos.PageCount - 1;
                gvArticulos.DataBind();
            }

            // Calcular sumas
            CalcularSumas(dtArticulos);

            // Limpiar los campos
            limpiarcampos();
        }

        private void limpiarcampos()
        {
            //txtDescripcion.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            ddlunidad.SelectedIndex = 0;
            txtPrecioUnitario.Text = string.Empty;
            txtPrecioTotal.Text = string.Empty;
            //txtdetalle.Text = string.Empty;

        }
        private bool EsDecimalValido(string valor)
        {
            // Validar que no sea nulo ni vacío
            if (string.IsNullOrWhiteSpace(valor))
                return false;

            // Patrón mejorado para permitir punto (.) o coma (,)
            string patron = @"^\d+([.,]\d+)?$";

            // Validar el patrón
            return Regex.IsMatch(valor, patron);
        }

        private void CalcularSumasEdicion(DataTable dtArticulos)
        {
            decimal totalCantidad = 0;
            decimal subtotal = 0;
            decimal impuestos = 0;
            decimal total = 0;

            foreach (DataRow row in dtArticulos.Rows)
            {

                Boolean valor = EsDecimalValido(row["cant"]?.ToString());
                 valor = EsDecimalValido(row["PUNIT"]?.ToString());
                if (EsDecimalValido(row["cant"].ToString()) && EsDecimalValido(row["PUNIT"].ToString()))
                
                {
                    totalCantidad += Convert.ToDecimal(row["cant"]);
                subtotal += Convert.ToDecimal(row["PTotal"]);
                }
            }

            //impuestos = subtotal * 0.21m; // Supongamos un impuesto del 21%
            total = subtotal + impuestos;

            // Crear DataTable para la grilla de sumas
            DataTable dtSumas = new DataTable();
            dtSumas.Columns.Add("CantidadTotal");
            dtSumas.Columns.Add("Subtotal");
            dtSumas.Columns.Add("Impuestos");
            dtSumas.Columns.Add("Total");

            DataRow drSumas = dtSumas.NewRow();
            drSumas["CantidadTotal"] = totalCantidad.ToString("N2");
            drSumas["Subtotal"] = subtotal.ToString("N2");
            drSumas["Impuestos"] = "0";// impuestos.ToString("N2");
            drSumas["Total"] = total.ToString("N2");

            dtSumas.Rows.Add(drSumas);

            // Guardar el DataTable en ViewState
            ViewState["Sumas"] = dtSumas;

            gvSumas.DataSource = dtSumas;
            gvSumas.DataBind();
        }




    


        protected void txtpreciounitario_Changed(object sender, EventArgs e)
        {
            // Obtener el texto ingresado en el TextBox
            string inputText = txtPrecioUnitario.Text;

            // Reemplazar el signo punto por una coma
            string convertedText = inputText.Replace('.', ',');

            // Establecer el texto modificado de nuevo en el TextBox
            txtPrecioUnitario.Text = convertedText;
        }






        protected void txtCantidad_Changed(object sender, EventArgs e)
        {
            // Obtener el texto ingresado en el TextBox
            string inputText = txtCantidad.Text;

            // Reemplazar el signo punto por una coma
            string convertedText = inputText.Replace('.', ',');

            // Establecer el texto modificado de nuevo en el TextBox
            txtCantidad.Text = convertedText;
        }


        private void CalcularSumas(DataTable dtArticulos)
        {
            decimal totalCantidad = 0;
            decimal subtotal = 0;
            decimal impuestos = 0;
            decimal total = 0;

            foreach (DataRow row in dtArticulos.Rows)
            {

               
               // if (EsDecimalValido(row["cantidad"].ToString()) && EsDecimalValido(row["preciounitario"].ToString()))
                {
                    totalCantidad += Convert.ToDecimal(row["cantidad"]);
                subtotal += Convert.ToDecimal(row["PrecioTotal"]);
                }
            }

            //impuestos = subtotal * 0.21m; // Supongamos un impuesto del 21%
            total = subtotal + impuestos;

            // Crear DataTable para la grilla de sumas
            DataTable dtSumas = new DataTable();
            dtSumas.Columns.Add("CantidadTotal");
            dtSumas.Columns.Add("Subtotal");
            dtSumas.Columns.Add("Impuestos");
            dtSumas.Columns.Add("Total");

            DataRow drSumas = dtSumas.NewRow();
            drSumas["CantidadTotal"] = totalCantidad.ToString("N2");
            drSumas["Subtotal"] = subtotal.ToString("N2");
            drSumas["Impuestos"] = "0"; /*impuestos.ToString("N2");*/
            drSumas["Total"] = total.ToString("N2");

            dtSumas.Rows.Add(drSumas);

            // Guardar el DataTable en ViewState
            ViewState["Sumas"] = dtSumas;

            gvSumas.DataSource = dtSumas;
            gvSumas.DataBind();
        }

        protected void gvArticulos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteRow")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                DataTable dt = ViewState["Articulos"] as DataTable;

                if (dt != null && dt.Rows.Count > index)
                {
                    dt.Rows[index].Delete();
                    gvArticulos.DataSource = dt;
                    gvArticulos.DataBind();
                    CalcularSumas(dt);
                    ViewState["Articulos"] = dt;
                }
            }
        }

        protected void gvArticulos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvArticulos.PageIndex = e.NewPageIndex;
            gvArticulos.DataSource = ViewState["Articulos"];
            gvArticulos.DataBind();
        }












        protected void gvClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvClientes.Rows[index];

                // Accede a los valores en función de si usas BoundField o TemplateField en el GridView
                txtCodCliente.Text =  row.Cells[0].Text.Trim();  // Código Cliente
                txtRazonSocial.Text = row.Cells[1].Text.Trim(); // Razón Social
                txtDireccion.Text = row.Cells[2].Text.Trim();   // Dirección
                txtCUIT.Text = row.Cells[3].Text.Trim();        // CUIT
                UpdatePanelCliente.Update();
                // Cierra el modal después de seleccionar
                ScriptManager.RegisterStartupScript(this, this.GetType(), "closeModal", "$('#clientesModal').modal('hide');", true);
            }
        }


        //protected void gvClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "Select")
        //    {
        //        int index = Convert.ToInt32(e.CommandArgument);
        //        GridViewRow row = gvClientes.Rows[index];
        //        txtCodCliente.Text = row.Cells[0].Text;
        //        txtDireccion.Text = row.Cells[2].Text;
        //        txtRazonSocial.Text = row.Cells[1].Text;
        //        txtCUIT.Text = row.Cells[3].Text;

        //        // Cerrar el modal al seleccionar un cliente
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "$('#clientesModal').modal('hide');", true);
        //    }
        //}
    }
}





