
using DAL;
using DAL.BDL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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



        [System.Web.Services.WebMethod]
        public static object ObtenerDatosGrafico(string idcliente)
        {
            var vistas = new Vistas();
            DataTable dt = vistas.getestadisticasVentasGrafico(idcliente);

            var lista = new List<object>();

            foreach (DataRow row in dt.Rows)
            {
                // Validaciones seguras
                string fecha = row["FECHA"] != DBNull.Value
                    ? Convert.ToDateTime(row["FECHA"]).ToString("yyyy-MM-dd")
                    : null;

                decimal? total = row["TOTAL"] != DBNull.Value
                    ? Convert.ToDecimal(row["TOTAL"])
                    : (decimal?)null;

                lista.Add(new
                {
                    fecha = fecha,
                    total = total
                });
            }

            return lista;
        }















        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ID_USUARIO"] == null)
            {
                // Si no hay usuario en sesión, redirige a la página de login
                Response.Redirect("Login.aspx");
            }


            if (!IsPostBack)
            {

                string parametroId = Request.QueryString["id"];

                if (!string.IsNullOrEmpty(parametroId))
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
                    CargarRazonSocial();
                    CargarTBL_TIPO_COMPROBANTE();
                    // Inicializar la tabla de artículos si es la primera vez que se carga la página
                    if (ViewState["Articulos"] == null)
                    {
                        DataTable dtArticulos = new DataTable();
                        dtArticulos.Columns.Add("Cantidad");
                        dtArticulos.Columns.Add("Descripcion");
                        dtArticulos.Columns.Add("IdEspesor");
                        dtArticulos.Columns.Add("Espesor");
                        dtArticulos.Columns.Add("IdAncho");
                        dtArticulos.Columns.Add("Ancho");
                        dtArticulos.Columns.Add("IdLargo");
                        dtArticulos.Columns.Add("Largo");
                        dtArticulos.Columns.Add("IdUnidad");
                        dtArticulos.Columns.Add("Unidad");
                        dtArticulos.Columns.Add("IDTasa");
                        dtArticulos.Columns.Add("Tasa");
                        dtArticulos.Columns.Add("Observaciones");
                        dtArticulos.Columns.Add("PrecioUnitario");
                        dtArticulos.Columns.Add("PrecioTotal");
                        ViewState["Articulos"] = dtArticulos;
                    }





                    gvArticulos.DataSource = ViewState["Articulos"];
                    gvArticulos.DataBind();
                }
            }
            else
            {
                // 🔥 Esta parte asegura que la grilla se muestre luego de un postback
                if (Session["ID_CLIENTE"] != null)
                {
                    CargarHistorialCliente(Session["ID_CLIENTE"].ToString());
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
                CargarArtidulosDescripcion();
                CargarUnidMedEspesor();
                CargarUnidMedLargo();
                CargarUnidMedAncho();
                CargarRazonSocial();

                DAL.Cotizacion cotizacion = new DAL.Cotizacion();
                cotizacion.idCotiz = id;
                DataTable ds = cotizacion.GETpedidos();
                txtFechaPedido.Text = ds.Rows[0]["fechaalta"].ToString();
                txtCodCliente.Text = ds.Rows[0]["NU_CLI_CODIGO"].ToString();
                Session["ID_CLIENTE"] = ds.Rows[0]["idcliente"].ToString();
                ddlEstado.SelectedValue = ds.Rows[0]["estado"].ToString();

                llenardatoscliente(Session["ID_CLIENTE"].ToString());

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
            // llenardatoscliente(txtCodCliente.Text );



        }
       

        protected void ddlRazonSocial_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["ID_CLIENTE"] = ddlRazonSocial.SelectedValue;
            Cliente cliente = new Cliente();
            cliente.ID_CLIENTE = Session["ID_CLIENTE"].ToString();
            DataTable ds = cliente.GETcLIENTE();

            txtDireccion.Text = ds.Rows[0]["DS_CLI_DIRECCION"].ToString();
            txtCodCliente.Text = ds.Rows[0]["NU_CLI_CODIGO"].ToString();
            CargarHistorialCliente(Session["ID_CLIENTE"].ToString());

            ScriptManager.RegisterStartupScript(
           this,
           this.GetType(),
           "cargarGraficoCliente",
           $"setTimeout(function(){{ cargarGrafico('{Session["ID_CLIENTE"]}'); }}, 200);",
           true
       );
        }
        private void CargarHistorialCliente(string   cliente)
            {

           DAL.Vistas ObtenerResumenCliente = new DAL.Vistas();
            DataTable dt = ObtenerResumenCliente.getestadisticasVentas(cliente); //

            gvHistorialCliente.DataSource = dt;
            gvHistorialCliente.DataBind();
        }

     



        private void llenardatoscliente(string id_cliente)
        {
            Cliente cli = new Cliente();
            cli.ID_CLIENTE = id_cliente;

            DataTable ds = cli.GETcLIENTE();

            if (ds.Rows.Count > 0)
            {
               
                string idCliente = ds.Rows[0]["id_cliente"].ToString();
                ddlRazonSocial.SelectedValue = idCliente;




                txtDireccion.Text = ds.Rows[0]["DS_CLI_DIRECCION"].ToString();

            }
            else
            {
                ddlRazonSocial.Text = "";
                txtDireccion.Text = "";

            }



        }









        private void llenardatosdelpedido(DataTable datos)
        {

            DataTable dtArticulos = ViewState["Articulos"] as DataTable;

            if (dtArticulos == null)
            {
                dtArticulos = new DataTable();
                dtArticulos.Columns.Add("Cantidad");
                dtArticulos.Columns.Add("Descripcion");
                dtArticulos.Columns.Add("IdEspesor");
                dtArticulos.Columns.Add("Espesor");
                dtArticulos.Columns.Add("IdAncho");
                dtArticulos.Columns.Add("Ancho");
                dtArticulos.Columns.Add("IdLargo");
                dtArticulos.Columns.Add("Largo");
                dtArticulos.Columns.Add("IdUnidad");
                dtArticulos.Columns.Add("Unidad");
                dtArticulos.Columns.Add("IDTasa");
                dtArticulos.Columns.Add("Tasa");
                dtArticulos.Columns.Add("Observaciones");
               
            dtArticulos.Columns.Add("PrecioUnitario");
                dtArticulos.Columns.Add("PrecioTotal");
            }


            foreach (DataRow row in datos.Rows)
            {

                DataRow dr = dtArticulos.NewRow();
                dr["Cantidad"] = row["Cantidad"];
                dr["Descripcion"] = row["Descripcion"];
                dr["IdEspesor"] = row["IdEspesor"];
                dr["Espesor"] = row["Espesor"];
                dr["IdAncho"] = row["IdAncho"];
                dr["Ancho"] = row["Ancho"];

                dr["IdLargo"] = row["IdLargo"];
                dr["Largo"] = row["Largo"];
                dr["IdUnidad"] = row["IdUnidad"];
                dr["Unidad"] = row["Unidad"];
                dr["IDTasa"] = row["IDTasa"];
                dr["Tasa"] = row["Tasa"];
                dr["PrecioUnitario"] = row["PrecioUnitario"];
                dr["PrecioTotal"] = row["PrecioTotal"];








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



        private void CargarTBL_TIPO_COMPROBANTE()
        {
            DAL.TBL_TIPO_COMPROBANTE ESTADO = new DAL.TBL_TIPO_COMPROBANTE();
            ESTADO.CD_TIP_STATUS = "1";
            ESTADO.DS_TIP_CODIGO = "COT";
            DataTable dt = ESTADO.GETTipos();

            ddlTipoCotizacion.DataSource = dt;
            ddlTipoCotizacion.DataTextField = "DS_TIP_DESCRIPCION";
            ddlTipoCotizacion.DataValueField = "ID_TIPO_COMPROBANTE";
            ddlTipoCotizacion.DataBind();
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




        private void ddlRazonSocial_SelectedIndexChanged()



        {
            DAL.Cliente cliente = new DAL.Cliente();


            DataTable dt = cliente.GETcLIENTE();

            ddlRazonSocial.DataSource = dt;
            ddlRazonSocial.DataTextField = "DS_CLI_RAZON_SOCIAL";
            ddlRazonSocial.DataValueField = "ID_CLIENTE";
            ddlRazonSocial.DataBind();

            //Session["ID_CLIENTE"] = ddlRazonSocial.SelectedValue;

            // Añadir un ítem predeterminado
            //ddlunidad.Items.Insert(0, new ListItem("", "0"));
        }







        private void CargarRazonSocial()
        {
            DAL.Cliente cliente = new DAL.Cliente();


            DataTable dt = cliente.GETcLIENTE();

            ddlRazonSocial.DataSource = dt;
            ddlRazonSocial.DataTextField = "DS_CLI_RAZON_SOCIAL";
            ddlRazonSocial.DataValueField = "ID_CLIENTE";
            ddlRazonSocial.DataBind();

            //Session["ID_CLIENTE"] = ddlRazonSocial.SelectedValue;

            // Añadir un ítem predeterminado
            ddlRazonSocial.Items.Insert(0, new ListItem("", "0"));

            ddlRazonSocial.SelectedValue = "0";
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
                if (Session["ID_CLIENTE"] == null || string.IsNullOrWhiteSpace(Session["ID_CLIENTE"].ToString()))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "swal", "Swal.fire({ icon: 'warning', title: 'Atención', text: 'Debe seleccionar un Cliente.', confirmButtonText: 'Aceptar' });", true);
                    return;
                }
                if (gvArticulos.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "swal", "Swal.fire({ icon: 'warning', title: 'Atención', text: 'Debe agregar al menos un artículo antes de continuar.', confirmButtonText: 'Aceptar' });", true);
                    return;
                }



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

        private void EDICION()
        {
            try
            {

                // Insertar el pedido
                DAL.Cotizacion Cotizacion = new DAL.Cotizacion();
                Cotizacion.idCotiz = ViewState["parametro"].ToString();
                Cotizacion.nombreCliente = ddlRazonSocial.Text;
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


                CotizCont.IDCOTIZ = (ViewState["parametro"].ToString());
                CotizCont.CotizCont_del();

                foreach (GridViewRow row in gvArticulos.Rows)
                {
                    CotizCont.IDCOTIZ = ViewState["parametro"].ToString();
                    CotizCont.CANT = row.Cells[0].Text;
                    CotizCont.DESCRIPCION = HttpUtility.HtmlDecode(row.Cells[1].Text);
                    CotizCont.ID_ESPESOR = (row.Cells[2].Text);
                    CotizCont.ESPESOR = HttpUtility.HtmlDecode(row.Cells[3].Text);
                    CotizCont.ID_ANCHO = (row.Cells[4].Text);
                    CotizCont.ANCHO = HttpUtility.HtmlDecode(row.Cells[5].Text);
                    CotizCont.ID_LARGO = HttpUtility.HtmlDecode((row.Cells[6].Text));
                    CotizCont.LARGO = row.Cells[7].Text;
                    CotizCont.ID_UNIDAD = (row.Cells[8].Text);
                    CotizCont.UNIDAD = HttpUtility.HtmlDecode(row.Cells[9].Text);
                    CotizCont.ID_TASA = (row.Cells[10].Text);
                    CotizCont.TASA = row.Cells[11].Text;
                    CotizCont.PRECIOUNITARIO = row.Cells[12].Text;
                    CotizCont.PRECIOTOTAL = row.Cells[13].Text;
                    CotizCont.CotizCont_INS();
                }

                // Mostrar mensaje de éxito

                // Blanquear los campos
                txtCodCliente.Text = string.Empty;
                ddlRazonSocial.SelectedIndex = 0;
                txtDireccion.Text = string.Empty;

                ddlEstado.SelectedIndex = 0;
                limpiarcampos();
                limpiarentradatotal();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "swal", "Swal.fire({ title: '¡Éxito!', text: 'La Cotización fue ingresada.', icon: 'success', confirmButtonText: 'Aceptar' });", true);

            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", $"alert('Error: {ex.Message}');", true); // si es WebForms
            }

        }



        private void NUEVO()
        {

            try
            {

                // Insertar el pedido
                DAL.Cotizacion Cotizacion = new DAL.Cotizacion();
                Cotizacion.nombreCliente = ddlRazonSocial.SelectedItem.ToString();

                Cotizacion.fechaAlta = txtFechaPedido.Text;
                Cotizacion.idCliente = Session["ID_CLIENTE"].ToString();
                Cotizacion.idCotizacion = "1";
                Cotizacion.estado = ddlEstado.SelectedValue;

                Cotizacion.direccionEntrega = txtDireccionEntrega.Text;
                Cotizacion.localidadentrega = TextIdLocalidadEntrega.Text;
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

                for (int i = 0; i < gvArticulos.Rows.Count; i++)
                {
                    GridViewRow row = gvArticulos.Rows[i];

                    COTIZCONT.IDCOTIZ = ds.Rows[0][0].ToString();
                    COTIZCONT.CANT = row.Cells[0].Text;
                    COTIZCONT.DESCRIPCION = HttpUtility.HtmlDecode(row.Cells[1].Text);

                    // Usar DataKeys para acceder a valores ocultos
                    COTIZCONT.ID_ESPESOR = gvArticulos.DataKeys[i]["IdEspesor"].ToString();
                    COTIZCONT.ID_ANCHO = gvArticulos.DataKeys[i]["IdAncho"].ToString();
                    COTIZCONT.ID_LARGO = gvArticulos.DataKeys[i]["Idlargo"].ToString();
                    COTIZCONT.ID_UNIDAD = gvArticulos.DataKeys[i]["IdUnidad"].ToString();
                    COTIZCONT.ID_TASA = gvArticulos.DataKeys[i]["IdTasa"].ToString();

                    // Las columnas visibles se siguen leyendo normalmente
                    COTIZCONT.ESPESOR = HttpUtility.HtmlDecode(row.Cells[3].Text);
                    COTIZCONT.ANCHO = HttpUtility.HtmlDecode(row.Cells[5].Text);
                    COTIZCONT.LARGO = HttpUtility.HtmlDecode(row.Cells[7].Text);
                    COTIZCONT.UNIDAD = HttpUtility.HtmlDecode(row.Cells[9].Text);
                    COTIZCONT.TASA = row.Cells[11].Text;
                    COTIZCONT.OBSERVACIONES= HttpUtility.HtmlDecode( row.Cells[12].Text);

                    COTIZCONT.PRECIOUNITARIO = row.Cells[13].Text;
                    COTIZCONT.PRECIOTOTAL = row.Cells[14].Text;

                    COTIZCONT.CotizCont_INS();
                }




                // Blanquear los campos
                txtCodCliente.Text = string.Empty;
                ddlRazonSocial.SelectedIndex = 0;
                txtDireccion.Text = string.Empty;

                ddlEstado.SelectedIndex = 0;


                limpiarcampos();
                limpiarentradatotal();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "swal", "Swal.fire({ title: '¡Éxito!', text: 'La Cotización fue ingresada.', icon: 'success', confirmButtonText: 'Aceptar' });", true);
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", $"alert('Error: {ex.Message}');", true); // si es WebForms
            }

        }



        private void limpiarentradatotal()
        {
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
            Session["ID_CLIENTE"] = null;


        }




        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            DataTable dtArticulos = ViewState["Articulos"] as DataTable;

            if (dtArticulos == null)
            {

                dtArticulos = new DataTable();
                dtArticulos.Columns.Add("Cantidad");
                dtArticulos.Columns.Add("Descripcion");
                dtArticulos.Columns.Add("IdEspesor");
                dtArticulos.Columns.Add("Espesor");
                dtArticulos.Columns.Add("IdAncho");
                dtArticulos.Columns.Add("Ancho");
                dtArticulos.Columns.Add("IdLargo");
                dtArticulos.Columns.Add("Largo");
                dtArticulos.Columns.Add("IdUnidad");
                dtArticulos.Columns.Add("Unidad");
                dtArticulos.Columns.Add("IDTasa");
                dtArticulos.Columns.Add("Tasa");
                dtArticulos.Columns.Add("Observaciones");
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

            dr["Cantidad"] = txtCantidad.Text;
            dr["Descripcion"] = ddldescripcion.SelectedItem.Text;
            dr["IdEspesor"] = ddlEspesor.SelectedValue;
            dr["Espesor"] = ddlEspesor.SelectedItem.Text;
            dr["IdAncho"] = ddlAncho.SelectedValue;
            dr["Ancho"] = ddlAncho.SelectedItem.Text;
            dr["IdLargo"] = ddlLargo.SelectedValue;
            dr["Largo"] = ddlLargo.SelectedItem.Text;
            dr["IdUnidad"] = ddlunidad.SelectedValue;
            dr["Unidad"] = ddlunidad.SelectedItem.Text;
            dr["Observaciones"] = txtObservaciones.Text ;
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
            txtObservaciones.Text = string.Empty;



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

                Boolean valor = EsDecimalValido(row["CANTIDAD"]?.ToString());
                valor = EsDecimalValido(row["PRECIOUNITARIO"]?.ToString());
                if (EsDecimalValido(row["CANTIDAD"].ToString()) && EsDecimalValido(row["PRECIOUNITARIO"].ToString()))

                {
                    totalCantidad += Convert.ToDecimal(row["CANTIDAD"]);
                    subtotal += Convert.ToDecimal(row["PRECIOTOTAL"]);
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
            
            string inputText = txtPrecioUnitario.Text;

            string convertedText = inputText.Replace('.', ',');

         
            txtPrecioUnitario.Text = convertedText;
        }






        protected void txtCantidad_Changed(object sender, EventArgs e)
        {
         
            string inputText = txtCantidad.Text;

          
            string convertedText = inputText.Replace('.', ',');

         
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
                txtCodCliente.Text = row.Cells[0].Text.Trim();  // Código Cliente
                ddlRazonSocial.Text = row.Cells[1].Text.Trim(); // Razón Social
                txtDireccion.Text = row.Cells[2].Text.Trim();   // Dirección

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





