using System;
using System.Configuration;
using System.Web;

using System.Web.Routing;
using System.Web.UI; // Asegúrate de incluir esta línea si vas a usar ScriptManager
using DAL;

namespace PedidosWebForm
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Desencriptar la cadena de conexión
            string wConnString = DAL.SQL.Decrypt(ConfigurationManager.AppSettings["ConnectionISeries"].ToString(), "mlmweb");

            // Cargar las librerías nativas de SQL Server Types
           

            // Registrar jQuery para la validación no intrusiva
            ScriptManager.ScriptResourceMapping.AddDefinition("jquery",
                new ScriptResourceDefinition
                {
                    Path = "~/Scripts/jquery-3.7.1.min.js", // Asegúrate de que el archivo esté en la ruta correcta
                    DebugPath = "~/Scripts/jquery-3.7.1.js",
                    CdnPath = "https://ajax.aspnetcdn.com/ajax/jQuery/jquery-3.7.1.min.js",
                    CdnDebugPath = "https://ajax.aspnetcdn.com/ajax/jQuery/jquery-3.7.1.js"

                }
            );
        }
    }
}
