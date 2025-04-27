using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.UI;
using Newtonsoft.Json.Linq; // 🔥 IMPORTANTE: necesitarás instalar Newtonsoft.Json para parsear el JSON

namespace PedidosWebForm
{
    public partial class ConsultaMercadoLibre : Page
    {
        private static readonly string clientId = "3698307024392057";
        private static readonly string clientSecret = "UlL1TQlTW7DlXA8qgn1XsCJoGulnepkE";
        private static readonly string redirectUri = "https://icy-cloths-melt.loca.lt";

        // Acá podrías guardar el token, idealmente en session o en variable de clase
        private static string accessToken = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Request.QueryString["code"] != null)
            {
                string code = Request.QueryString["code"];
                _ = ObtenerToken(code); // no bloqueo el hilo principal
            }
        }

        protected void btnAutorizar_Click(object sender, EventArgs e)
        {
            string authUrl = $"https://auth.mercadolibre.com.ar/authorization?response_type=code&client_id={clientId}&redirect_uri={redirectUri}";
            Response.Redirect(authUrl);
        }

        private async Task ObtenerToken(string code)
        {
            using (var httpClient = new HttpClient())
            {
                var data = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "authorization_code"),
                    new KeyValuePair<string, string>("client_id", clientId),
                    new KeyValuePair<string, string>("client_secret", clientSecret),
                    new KeyValuePair<string, string>("code", code),
                    new KeyValuePair<string, string>("redirect_uri", redirectUri),
                });

                var response = await httpClient.PostAsync("https://api.mercadolibre.com/oauth/token", data);
                string result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    // Parseamos el JSON para sacar el access_token
                    var json = JObject.Parse(result);
                    accessToken = json["access_token"]?.ToString();

                    lblResultado.Text = "✅ Token obtenido correctamente.";
                }
                else
                {
                    lblResultado.Text = "❌ Error al obtener token: <br/>" + result;
                }
            }
        }

        protected async Task btnBuscar_Click(object sender, EventArgs e)

        {
            string query = txtBusqueda.Text.Trim();

            if (string.IsNullOrEmpty(query))
            {
                lblResultado.Text = "⚠️ Ingrese un texto para buscar.";
                return;
            }

            if (string.IsNullOrEmpty(accessToken))
            {
                lblResultado.Text = "⚠️ No hay token disponible. Autorícese primero.";
                return;
            }

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                string url = $"https://api.mercadolibre.com/sites/MLA/search?q={Uri.EscapeDataString(query)}";

                var response = await httpClient.GetAsync(url);
                string result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var json = JObject.Parse(result);
                    var items = json["results"];

                    var dataSource = new List<dynamic>();

                    foreach (var item in items)
                    {
                        dataSource.Add(new
                        {
                            Titulo = item["title"]?.ToString(),
                            Precio = item["price"]?.ToString(),
                            Link = item["permalink"]?.ToString()
                        });
                    }

                    gvResultados.DataSource = dataSource;
                    gvResultados.DataBind();
                }
                else
                {
                    lblResultado.Text = "❌ Error al buscar productos: <br/>" + result;
                }
            }
        }
    }
}
