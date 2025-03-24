using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Web.UI;

namespace Myc.Web
{
	/// <summary>
	/// Clase de Web Form Base para la aplicaci�n de funcionalidades diversas, adicionales a System.Web.UI.Page 
	/// (por ejemplo, funciones de localizaci�n mediante el uso de una cultura espec�fica).
	/// </summary>
	public class MycWebForm : Page
	{

		public static ResourceManager _rm;

		override protected void OnInit(EventArgs e)
		{
			// --- Para utilizar un solo archivo de recursos para toda la aplicaci�n 
			// --- (el archivo nombredepagina.resx queda sin efecto, y por default se utiliza el definido aqu�)
			//string w_ResFile = "MycAccesos.Content";

			// --- Para utilizar un archivo de recursos por p�gina.
			// --- (el archivo nombredepagina.resx es el default)
			string w_ResFile = this.Page.GetType().BaseType.FullName;

			SetCurrentCulture(CultureInfo.CurrentCulture.ToString(), w_ResFile, this.Page.GetType().BaseType.Assembly);
		}
		

		/// <summary>
		/// Establece la cultura para el sistema multilenguaje (localizaci�n) usando Globalization con los archivos de recursos.
		/// </summary>
		/// <param name="p_CultureCode">Nombre de la cultura, en formato est�ndar RFC 1766 ([languagecode]-[country/regioncode], 
		/// donde [languagecode] son las dos letras (en min�sculas) del c�digo de lenguaje correspondientes al est�ndar ISO 639-1, y 
		/// [country/regioncode] son las dos letras (en may�sculas) correspondientes al est�ndar ISO 3166.
		/// <code>ej: 
		/// "en-US" (ingl�s Estados Unidos); "es-AR" (espa�ol Argentina)</code>
		/// </param>
		/// <param name="p_BaseName">Nombre del objeto (p�gina) para aplicar al ResourceManager 
		/// (generalmente: this.Page.GetType().BaseType.FullName)
		/// </param>
		/// <param name="p_Assembly">Assempbly de las p�ginas 
		/// (generalmente: this.Page.GetType().BaseType.Assembly)
		/// </param>
		/// <remarks>Los archivos de recursos generan los Satellite Assemblies necesarios para la localizaci�n</remarks>
		public static void SetCurrentCulture(string p_CultureCode, string p_BaseName, Assembly p_Assembly)
		{
			if(p_CultureCode.Length == 0 || p_CultureCode == null)
			{
				CultureInfo w_cult = CultureInfo.CreateSpecificCulture("");
				p_CultureCode = w_cult.ToString();
			}
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(p_CultureCode);
			_rm = new ResourceManager(p_BaseName, p_Assembly);
		}

		private static void SetCurrentCulture(string p_BaseName, Assembly p_Assembly)
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(CultureInfo.CurrentCulture.ToString());
			_rm = new ResourceManager(p_BaseName, p_Assembly);
		}
	}
}
