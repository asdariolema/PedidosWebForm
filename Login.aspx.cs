using DAL;
using System;
using System.Configuration;
using System.Data;
using System.Web.Caching;
using System.Web.Security;
using System.Web.UI;


namespace PedidosWebForm
{
    public partial class Login : System.Web.UI.Page
    {
        public string Msj, Msj1;
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //this.Window1.Hide();
            if (validarUsuario())
            {
                if (Session["REQUEST_PAGE"] == null)
                {
                    FormsAuthentication.RedirectFromLoginPage("A", false);
                    Response.Redirect("~/Menu.aspx");
                }
                else
                {
                   // Response.Redirect(Session["REQUEST_PAGE"].ToString());
                    Response.Redirect("~/Menu.aspx");
                    Session["REQUEST_PAGE"] = null;
                }
                //Response.Redirect("~/WebForm1.aspx");
            }
            else
            {
                //this.ltrMensaje.Text = "<b>Error en el usuario o contraseña</b><br />Verifique los datos ingresados ";
                //pnlMensaje.Visible = true;
            }
        }
        protected void btnCambiarPass_Click(object sender, EventArgs e)
        {
            pnlMensaje.Visible = false;
            wnNuevaPass.Visible = true;
            Window1.Visible = false;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ltrMensaje.Text = "LOGIN CANCELADO";
            pnlMensaje.Visible = true;
        }
        protected void btnCancelarNew_Click(object sender, EventArgs e)
        {
            wnNuevaPass.Visible = false;
            Window1.Visible = true;
            lblMessage.Visible = false;
            ltrMensaje.Visible = false;
            pnlMensaje.Visible = false;
        }
        protected void btnAceptarNew_Click(object sender, EventArgs e)
        {
            this.Window1.Visible = false;
            //if (cambiarPass())
            //{
            //    //Config cf = new MessageBox.Config();
            //    //cf.Message = "La contraseña fue modificada correctamente. Presione Acpetar y vuela a identificarse";
            //    //cf.Buttons = MessageBox.Button.OK;
            //    //cf.Closable = true;
            //    //cf.Modal = true;
            //    //cf.Title = "Cambio de Contraseña";
            //    //Ext.Net.ExtNet.Msg.Show(cf);

            //    ltrMensaje.Text = "La contraseña fue modificada correctamente";
            //    pnlMensaje.Visible = true;
            //    Window1.Visible = true;
            //    wnNuevaPass.Visible = false;
            //}
            //else
            //{
            //    string template = "<b>Error en el usuario o contraseña</b><br />Verifique los datos ingresados ";
            //    this.ltrMensaje.Text = template;
            //    pnlMensaje.Visible = true;
            //}
        }
        private bool validarUsuario()
        {
            try
            {
               DAL.CMUSER objU = new DAL.CMUSER();
                // objU.LOGIN(txtUsername.Value.ToUpper(), txtPassword.Value);
                //objU.NOMBRE = txtUsername.Value;
                objU.USRDESCRIP = txtUsername.Value.ToUpper();
                objU.USRPASS = DAL.SQL.Encrypt(txtPassword.Value.ToUpper(), "mlmweb");
                objU.USRUSUALT = "1";

                DataTable dtUser = objU.getCMUSER();

                if (dtUser.Rows.Count == 1)
                {
                    //MATY - OBTIENE LA OFICINA DEL USUARIO MUNICIPAL QUE INGRESA AL SISTEMA DE SEH - USUARIO SEH(22) O USUARIO PYP(25)
                    Session["USUARIO_OFICINA"] = dtUser.Rows[0]["IDOFICINA"].ToString();

                    string sistema = DAL.SQL.Decrypt(ConfigurationManager.AppSettings["ConnectionISeries"].ToString(), "mlmweb");
                    sistema = sistema.Replace("Dsn=", "");
                    sistema = sistema.Replace(";", "");

                  
                    /*------------------------------------ HAGO LA VALIDACION SIN CACHE ------------------------------*/
                    Session["ID_USUARIO"] = dtUser.Rows[0]["IDUSUARIO"].ToString();
                    Session["NOMBRE_USUARIO"] = txtUsername.Value;
                    Session["PASSWORD"] = txtPassword.Value;
                    Session["ID_PERFIL"] = dtUser.Rows[0]["IDPERFIL"].ToString();

                    DataTable dtRec = objU.GETRECURSOUSER(dtUser.Rows[0]["IDUSUARIO"].ToString(), ConfigurationManager.AppSettings["idSISTEMA"].ToString());

                    if (dtRec.Rows.Count > 0)
                    {
                        Session["dsRecursos"] = dtRec;
                        return true;
                    }
                    else
                    {
                        this.ltrMensaje.Text = "<b>El usuario ingresado no Posee Recursos para acceder a este sistema</b>";
                        pnlMensaje.Visible = true;
                        return false;
                    }

                }

                else
                {
                    this.ltrMensaje.Text = "<b>Error en el usuario o contraseña</b><br />Verifique los datos ingresados ";
                    pnlMensaje.Visible = true;
                    return false;
                }
            }
            catch (Exception ex)
            {
                string template = "<b>Error en el proceso de login</b>";
                //  this.ltrMensaje.Text = "<b>Error en el usuario o contraseña</b><br />Verifique los datos ingresados <br />" + ex.Message ;
                this.ltrMensaje.Text = ex.Message.Substring(ex.Message.IndexOf("-") + 1, ex.Message.Length - (ex.Message.IndexOf("-") + 1));
                pnlMensaje.Visible = true;
                return false;
            }
        }
        //private bool cambiarPass()
        //{
        //    try
        //    {
        //        DALSQLServer.CMUSER objU = new DALSQLServer.CMUSER();
        //        objU.CHGPWD(txtNewUsuario.Value, txtActual.Value, txtNewPassConf.Value);
        //        objU.USRDESCRIP = txtNewUsuario.Value;
        //        objU.USRESTADO = "1";
        //        DataTable dtUser = objU.getCMUSER();

        //        if (dtUser.Rows.Count == 1)
        //        {
        //            Session["ID_USUARIO"] = dtUser.Rows[0]["IDUSUARIO"].ToString();
        //            Session["NOMBRE_USUARIO"] = txtUsername.Value;
        //            Session["PASSWORD"] = txtPassword.Value;
        //            Session["ID_PERFIL"] = dtUser.Rows[0]["IDPERFIL"].ToString();
        //            Session["dsRecursos"] = objU.GETRECURSOUSER(dtUser.Rows[0]["IDUSUARIO"].ToString(), ConfigurationManager.AppSettings["idSISTEMA"].ToString());
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        msErrores(ex);
        //        return false;
        //    }
        //}
        private void msErrores(Exception ex)
        {
            //Msj = "Error en el sistema: " + ex.Message + " " + DateTime.Now.ToShortDateString();
            //hfMsj.Value = Msj;

            //Coolite.Ext.Web.WindowListeners listeners = new Coolite.Ext.Web.WindowListeners();

            //listeners.BeforeShow.Handler = string.Concat(BarLabel.ClientID, ".setText('Enviar email:');");

            //Coolite.Ext.Web.Ext.Notification.Show(new Coolite.Ext.Web.Notification.Config
            //{
            //    Title = "Error",
            //    Icon = Coolite.Ext.Web.Icon.Information,
            //    Height = 150,
            //    AutoHide = false,
            //    CloseVisible = true,
            //    ContentEl = "customEl",
            //    Listeners = listeners
            //});
        }
        protected void lnkRepPerf_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Reporte", "<script language='JavaScript'>window.open('ReportP.aspx', 'Reporte', 'top=0,left=0,width=800,height=600,status=no,resizable=yes,scrollbars=no')</script>", false);
        }
    }
}
