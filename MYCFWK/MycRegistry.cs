//using System;
using Microsoft.Win32;

namespace Myc.General
{
	/// <summary>
	/// Acciones con la registry del sistema operativo.
	/// </summary>
	/// 
	public class MycRegistry
	{
		/// <summary>
		/// Busca el valor de una clave en la registry y lo devuelve.
		/// </summary>
		/// <param name="pKey">Clave a buscar</param>
		/// <param name="pNombre">Nombre de la clave</param>
		/// <returns>El valor leído. Si la clave no existe, retorna NULL.</returns>
		public static string readReg(string pKey, string pNombre)
		{
			string w_retVal = null;
			try
			{
				RegistryKey pRegKey = Registry.LocalMachine;
				pRegKey = pRegKey.OpenSubKey("Software\\" + pKey);
				w_retVal = pRegKey.GetValue(pNombre).ToString();
			}
			catch
			{
			}

			return w_retVal;

		}

		/// <summary>
		/// Busca el valor de una clave en la registry y lo devuelve.
		/// </summary>
		/// <param name="p_Path">Rama que continúa a "Software\" en la registry</param>
		/// <param name="p_Key">Clave a buscar</param>
		/// <param name="p_Nombre">Nombre de la clave</param>
		/// <returns>El valor leído. Si la clave no existe, retorna NULL.</returns>
		public static string readReg(string p_SubPath, string p_Key, string p_Nombre)
		{
			string w_retVal = null;
			try
			{
				RegistryKey pRegKey = Registry.LocalMachine;
				pRegKey = pRegKey.OpenSubKey(string.Format("Software\\{0}\\{1}", p_SubPath, p_Key));
				w_retVal = pRegKey.GetValue(p_Nombre).ToString();
			}
			catch
			{
			}

			return w_retVal;

		}


		/// <summary>
		/// Escribe una clave y su valor en la registry.
		/// </summary>
		/// <param name="pKey">Key (rama de la clave)</param>
		/// <param name="pNombre">Nombre de la clave</param>
		/// <param name="pValor">Valor de la clave</param>
		/// <returns>False si se produjo un error y True si todo fue bien.</returns>
		public static bool writeReg(string pKey, string pNombre, string pValor)
		{	
			try
			{
				RegistryKey key = Registry.LocalMachine.OpenSubKey("Software", true);
				RegistryKey newkey = key.CreateSubKey(pKey);
				newkey.SetValue(pNombre, pValor);
				return true;
			}
			catch
			{
				return false;
			}
		}
		

		/// <summary>
		/// Escribe una clave y su valor en la registry.
		/// </summary>
		/// <param name="p_Path">Rama que continúa a "Software\" en la registry</param>
		/// <param name="p_Key">Key (rama de la clave)</param>
		/// <param name="p_Nombre">Nombre de la clave</param>
		/// <param name="p_Valor">Valor de la clave</param>
		/// <returns>False si se produjo un error y True si todo fue bien.</returns>
		public static bool writeReg(string p_SubPath, string p_Key, string p_Nombre, string p_Valor)
		{	
			try
			{
				Registry.LocalMachine.CreateSubKey(string.Format("Software\\{0}", p_SubPath));
				RegistryKey key = Registry.LocalMachine.OpenSubKey(string.Format("Software\\{0}", p_SubPath), true);
				RegistryKey newkey = key.CreateSubKey(p_Key);
				newkey.SetValue(p_Nombre, p_Valor);
				return true;
			}
			catch
			{
				return false;
			}
		}
		

		/// <summary>
		/// Elimina una clave de la registry.
		/// </summary>
		/// <param name="pKey">Nombre de la clave a eliminar</param>
		/// <returns></returns>
		public static bool deleteReg(string pKey)
		{
			try
			{
				//			RegistryKey delKey = Registry.LocalMachine.OpenSubKey("Software");
				//			delKey.DeleteValue("Myc");

				RegistryKey delKey = Registry.LocalMachine.OpenSubKey("Software", true);
				delKey.DeleteSubKey(pKey);
				return true;
			}
			catch
			{
				return false;
			}
		}


	}

}
