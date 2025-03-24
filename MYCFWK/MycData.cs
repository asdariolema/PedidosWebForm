using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using Myc.data.Conversiones;
using Myc.data.Log;

namespace Myc.data
{
}

namespace Myc.data.SQLServer
{
	/// <summary>
	/// Acciones de DAC (Data Access Component) específicas para SQL Server.
	/// </summary>
	public class MycData : MycConvert
	{
		/// <summary>
		/// Método Constructor
		/// </summary>
		public MycData()
		{
		}

		#region ====== Conexión

		/// <summary>
		/// Crea un objeto Conexión, pasándole un string de conexión determinado.
		/// </summary>
		/// <param name="pStrConn">String de conexión</param>
		/// <returns>El objeto conexión se devuelve ya abierto.</returns>
		public SqlConnection GetConnection(string pStrConn)
		{
			//usa un string de conexión específico
			//#line 100
			SqlConnection connection = null;
			try
			{
				connection = new SqlConnection(pStrConn);
				connection.Open();
			}
			catch (Exception e)
			{
				throw new Exception(string.Format("No se pudo crear la conexión con el string del parámetro] {0}", e.Message));
			}
			return connection;
		}


		/// <summary>
		/// Crea un objeto Conexión, tomando el string de conexión determinado del archivo de configuración.
		/// El String en el archivo de configuración debe estar encriptado mediante la clase NCrypto.Security.Cryptography
		/// </summary>
		/// <remarks>
		/// El string en el archivo de configuración debe aparecer bajo una clave con nombre "ConnString".
		/// </remarks>
		/// <returns>El objeto conexión se devuelve ya abierto.</returns>
		public SqlConnection GetConnection()
		{
			//usa el string de conexión tomado de la configuración
			SqlConnection connection = null;
			//#line 200
			try
			{
				//string wPassw = "SiSeM"; //crear una clave de encriptación por default.

				//string strConn = MycConvert.Decrypt(ConfigurationSettings.AppSettings["ConnString"], wPassw);

				string wPassw = "%#0" + "4?0" + "1*19" + "63!"; //crear una clave de encriptación por default.

				string strConn = MycConvert.Decrypt(ConfigurationManager.AppSettings["ConnString"], wPassw);

				//string strConn = ConfigurationSettings.AppSettings["ConnString"];
				connection = new SqlConnection(strConn);
				connection.Open();
			}
			catch (Exception e)
			{
				throw new Exception(string.Format("[No se pudo crear la conexión con el string de la configuración] {0}", e.Message));
			}
			return connection;
		}

		#endregion

		#region ====== Propósito Gral.

		#region Ejecuta un SP de propósito general pasándole los parámetros en un objeto XML. Devuelve un DATASET.

		/// <summary>
		/// Ejecuta un Stored Procedure de propósito general (para uso general). Los parámetros se pasan en un OBJETO XML.
		/// </summary>
		/// <remarks>
		/// El string de conexión lo toma del archivo de configuración, cuya clave es "ConnString".
		/// </remarks>
		/// <param name="pSPname">Nombre del Stored Procedure</param>
		/// <param name="pXMLParam">Un objeto XML conteniendo los parámetros a pasarle al Stored Procedure y sus valores. El nombre del nodo debe llamase como el parámetro, 
		/// y su contenido como valor.</param>
		/// <returns></returns>
		public void ExecuteSP(string pSPname, XmlDocument pXMLParam)
		{
			//#line 300
			try
			{
				XmlNode root = pXMLParam.FirstChild;
				if (root.HasChildNodes)
				{
					int cantNodos = root.ChildNodes.Count;
					int cantItems = root.ChildNodes[0].ChildNodes.Count;

					SqlCommand cmd = new SqlCommand();

					SqlParameter[] parametros = new SqlParameter[cantItems];

					for (int i = 0; i < cantNodos; i++)
					{
						string nombreParam = null;
						string valorParam = null;
						nombreParam = "@" + root.ChildNodes[i].Name;
						valorParam = root.ChildNodes[i].InnerText;
						if (valorParam == "")
							valorParam = null;

						cmd.Parameters.AddWithValue(nombreParam, valorParam);
					}
					SqlConnection conn = GetConnection();
					cmd.Connection = conn;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					cmd.ExecuteNonQuery();

					if (conn != null)
						cmd.Connection.Dispose();
				}
			}
			catch (Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", pSPname, e.Message));
			}
		}

		#endregion

		#region Ejecuta un SP de propósito general pasándole los parámetros en un string XML.

		/// <summary>
		/// Ejecuta un Stored Procedure de propósito general (para uso general). Los parámetros se pasan en un STRING XML.
		/// </summary>
		/// <remarks>
		/// El string de conexión lo toma del archivo de configuración, cuya clave es "ConnString".
		/// El string XML con los parámetros debe estar bien formado y debe tener un solo nivel de hijos.
		/// </remarks>
		/// <param name="pSPname">Nombre del Stored Procedure</param>
		/// <param name="pXMLParam">Un string XML conteniendo los parámetros a pasarle al Stored Procedure y sus valores. El nombre del nodo debe llamase como el parámetro, 
		/// y en su contenido va el valor.</param>
		public void ExecuteSP(string pSPname, string pStrXMLParam)
		{
			XmlDocument xDoc = new XmlDocument();
			xDoc.LoadXml(pStrXMLParam);
			//#line 400
			try
			{
				XmlNode root = xDoc.FirstChild;
				if (root.HasChildNodes)
				{
					int cantNodos = root.ChildNodes.Count;
					int cantItems = root.ChildNodes[0].ChildNodes.Count;

					SqlCommand cmd = new SqlCommand();

					SqlParameter[] parametros = new SqlParameter[cantItems];

					for (int i = 0; i < cantNodos; i++)
					{
						string nombreParam = null;
						string valorParam = null;
						nombreParam = "@" + root.ChildNodes[i].Name;
						valorParam = root.ChildNodes[i].InnerText;
						if (valorParam.Length == 0)
							valorParam = null;

						cmd.Parameters.AddWithValue(nombreParam, valorParam);
					}
					SqlConnection conn = GetConnection();
					cmd.Connection = conn;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					cmd.ExecuteNonQuery();

					if (conn != null)
						conn.Dispose();
				}
			}
			catch (Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", pSPname, e.Message));
			}
		}

		#endregion

		#region Ejecuta un SP de propósito general pasándole los parámetros en un OBJETO XML y el string de conexión. Devuelve un DATASET.

		/// <summary>
		/// Ejecuta un Stored Procedure de propósito general (para uso general). Los parámetros se pasan en un OBJETO XML.
		/// </summary>
		/// <remarks>
		/// El string de conexión lo toma del archivo de configuración, cuya clave es "ConnString".
		/// </remarks>
		/// <param name="pSPname">Nombre del Stored Procedure</param>
		/// <param name="pXMLParam">Un objeto XML conteniendo los parámetros a pasarle al Stored Procedure y sus valores. El nombre del nodo debe llamase como el parámetro, 
		/// y su contenido como valor.</param>
		/// <param name="pStrConn">El string de conexión.</param>
		/// <returns></returns>
		public DataSet ExecuteSP(string pSPname, XmlDocument pXMLParam, string pStrConn)
		{
			DataSet dsResult = new DataSet(); //DataSet con resultado
			//#line 500
			try
			{
				XmlNode root = pXMLParam.FirstChild;
				if (root.HasChildNodes)
				{
					int cantNodos = root.ChildNodes.Count;
					int cantItems = root.ChildNodes[0].ChildNodes.Count;

					SqlCommand cmd = new SqlCommand();

					SqlParameter[] parametros = new SqlParameter[cantItems];

					for (int i = 0; i < cantNodos; i++)
					{
						string nombreParam = null;
						string valorParam = null;
						nombreParam = "@" + root.ChildNodes[i].Name;
						valorParam = root.ChildNodes[i].InnerText;
						if (valorParam == "")
							valorParam = null;

						cmd.Parameters.AddWithValue(nombreParam, valorParam);
					}
					SqlConnection conn = GetConnection(pStrConn);
					cmd.Connection = conn;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					cmd.ExecuteNonQuery();
					SqlDataAdapter da = new SqlDataAdapter(cmd);
					da.Fill(dsResult);

					if (conn != null)
						cmd.Connection.Dispose();
				}
			}
			catch (Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", pSPname, e.Message));
			}
			return dsResult;
		}

		#endregion

		#region Ejecuta un SP de propósito general pasándole los parámetros en un string XML y el string de conexión. Devuelve un DATASET.

		/// <summary>
		/// Ejecuta un Stored Procedure de propósito general devolviendo un DataSet con los datos.
		/// Los parámetros se pasan en un STRING XML.
		/// </summary>
		/// <param name="pSPname">Nombre del Stored Procedure</param>
		/// <param name="pStrXMLParam">Un string XML conteniendo los parámetros a pasarle al Stored Procedure y sus valores. El nombre del nodo debe llamase como el parámetro, 
		/// y su contenido como valor.</param>
		/// <param name="pStrConn">El string de conexión.</param>
		/// <returns></returns>
		public DataSet ExecuteSP(string pSPname, string pStrXMLParam, string pStrConn)
		{
			DataSet dsResult = new DataSet(); //DataSet con resultado
			//#line 600			
			XmlDocument xDoc = new XmlDocument();
			xDoc.LoadXml(pStrXMLParam);

			try
			{
				XmlNode root = xDoc.FirstChild;
				if (root.HasChildNodes)
				{
					int cantNodos = root.ChildNodes.Count;
					int cantItems = root.ChildNodes[0].ChildNodes.Count;

					SqlCommand cmd = new SqlCommand();

					SqlParameter[] parametros = new SqlParameter[cantItems];

					for (int i = 0; i < cantNodos; i++)
					{
						string nombreParam = null;
						string valorParam = null;
						nombreParam = "@" + root.ChildNodes[i].Name;
						valorParam = root.ChildNodes[i].InnerText;
						if (valorParam == "")
							valorParam = null;

						cmd.Parameters.AddWithValue(nombreParam, valorParam);
					}
					SqlConnection conn = GetConnection(pStrConn);
					cmd.Connection = conn;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					cmd.ExecuteNonQuery();
					SqlDataAdapter da = new SqlDataAdapter(cmd);
					da.Fill(dsResult);

					if (conn != null)
						cmd.Connection.Dispose();
				}
			}
			catch (Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", pSPname, e.Message));
			}
			return dsResult;
		}

		#endregion

		#endregion

		#region ====== Query

		#region *Ejecuta un SP de consulta pasándole los parámetros en un objeto XML. Devuelve un DATASET.

		/// <summary>
		/// Ejecuta un Stored Procedure de consulta devolviendo un DataSet con los datos. 
		/// Los parámetros se pasan en un OBJETO XML.
		/// El string de conexión lo toma del archivo de configuración, cuya clave es "ConnString".
		/// </summary>
		/// <param name="pSPname">Nombre del Stored Procedure</param>
		/// <param name="pXMLParam">Un objeto XML conteniendo los parámetros a pasarle al Stored Procedure y sus valores. El nombre del nodo debe llamase como el parámetro, 
		/// y su contenido como valor.</param>
		/// <returns></returns>
		public DataSet ExecuteSpReader(string pSPname, XmlDocument pXMLParam)
		{
			DataSet dsResult = new DataSet(); //DataSet con resultado
			try
			{
				//#line 700
				XmlNode root = pXMLParam.FirstChild;
				if (root.HasChildNodes)
				{
					int cantNodos = root.ChildNodes.Count;
					int cantItems = root.ChildNodes[0].ChildNodes.Count;

					SqlCommand cmd = new SqlCommand();

					SqlParameter[] parametros = new SqlParameter[cantItems];

					for (int i = 0; i < cantNodos; i++)
					{
						string nombreParam = null;
						string valorParam = null;
						nombreParam = "@" + root.ChildNodes[i].Name;
						valorParam = root.ChildNodes[i].InnerText;


						if (valorParam.Length > 0)
						{
							string wTipoDatoStruct = root.ChildNodes[i].Attributes["dataType"].Value.ToUpper();
							switch (wTipoDatoStruct)
							{
								case "NUMERIC":
									valorParam = MycConvert.AdaptToDecimalDB(valorParam);
									break;
								case "DATETIME":
									valorParam = MycConvert.AdaptToDateDB(valorParam);
									break;
								case "DATE":
									valorParam = MycConvert.AdaptToDateDB(valorParam);
									break;
								case "STRING":
									valorParam = MycConvert.AdaptToStringDB(valorParam);
									break;
							}

							cmd.Parameters.AddWithValue(nombreParam, valorParam);
						}
					}

					SqlConnection conn = GetConnection();
					cmd.Connection = conn;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					cmd.ExecuteNonQuery();
					SqlDataAdapter da = new SqlDataAdapter(cmd);
					da.Fill(dsResult);

					if (conn != null)
						cmd.Connection.Dispose();
				}
			}
			catch (Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", pSPname, e.Message));
			}
			return dsResult;
		}

		#endregion

		#region *Ejecuta un SP de consulta pasándole los parámetros en un string XML. Devuelve un DATASET.

		/// <summary>
		/// Ejecuta un Stored Procedure de consulta devolviendo un DataSet con los datos.
		/// Los parámetros se pasan en un STRING XML.
		/// El string de conexión lo toma del archivo de configuración, cuya clave es "ConnString".
		/// </summary>
		/// <param name="pSPname">Nombre del Stored Procedure</param>
		/// <param name="pXMLParam">Un string XML conteniendo los parámetros a pasarle al Stored Procedure y sus valores. El nombre del nodo debe llamase como el parámetro, 
		/// y su contenido como valor.</param>
		/// <returns></returns>
		public DataSet ExecuteSpReader(string pSPname, string pStrXMLParam)
		{
			DataSet dsResult = new DataSet(); //DataSet con resultado
			//#line 800

			XmlDocument xDoc = new XmlDocument();
			xDoc.LoadXml(pStrXMLParam);

			try
			{
				XmlNode root = xDoc.FirstChild;
				if (root.HasChildNodes)
				{
					int cantNodos = root.ChildNodes.Count;
					int cantItems = root.ChildNodes[0].ChildNodes.Count;

					SqlCommand cmd = new SqlCommand();

					SqlParameter[] parametros = new SqlParameter[cantItems];

					for (int i = 0; i < cantNodos; i++)
					{
						string nombreParam = null;
						string valorParam = null;
						nombreParam = "@" + root.ChildNodes[i].Name;
						valorParam = root.ChildNodes[i].InnerText;

						if (valorParam != string.Empty)
						{
							string wTipoDatoStruct = root.ChildNodes[i].Attributes["dataType"].Value.ToUpper();
							switch (wTipoDatoStruct)
							{
								case "NUMERIC":
									valorParam = MycConvert.AdaptToDecimalDB(valorParam);
									break;
								case "DATETIME":
									valorParam = MycConvert.AdaptToDateDB(valorParam);
									break;
								case "DATE":
									valorParam = MycConvert.AdaptToDateDB(valorParam);
									break;
								case "STRING":
									valorParam = MycConvert.AdaptToStringDB(valorParam);
									break;
							}

							cmd.Parameters.AddWithValue(nombreParam, valorParam);
						}
					}
					SqlConnection conn = GetConnection();
					cmd.Connection = conn;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					cmd.ExecuteNonQuery();
					SqlDataAdapter da = new SqlDataAdapter(cmd);
					da.Fill(dsResult);

					if (conn != null)
						cmd.Connection.Dispose();
				}
			}
			catch (Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", pSPname, e.Message));
			}
			return dsResult;
		}

		#endregion

		#region *Ejecuta un SP de consulta pasándole los parámetros en un objeto XML y el string de conexión. Devuelve un DATASET.

		/// <summary>
		/// Ejecuta un Stored Procedure de consulta devolviendo un DataSet con los datos.
		/// Los parámetros se pasan en un OBJETO XML.
		/// </summary>
		/// <param name="pSPname">Nombre del Stored Procedure</param>
		/// <param name="pXMLParam">Un objeto XML conteniendo los parámetros a pasarle al Stored Procedure y sus valores. El nombre del nodo debe llamase como el parámetro, 
		/// y su contenido como valor.</param>
		/// <param name="pStrConn">String de Conexión</param>
		/// <returns></returns>
		public DataSet ExecuteSpReader(string pSPname, XmlDocument pXMLParam, string pStrConn)
		{
			DataSet dsResult = new DataSet(); //DataSet con resultado

			try
			{
				//#line 900
				XmlNode root = pXMLParam.FirstChild;
				if (root.HasChildNodes)
				{
					int cantNodos = root.ChildNodes.Count;
					int cantItems = root.ChildNodes[0].ChildNodes.Count;

					SqlCommand cmd = new SqlCommand();

					SqlParameter[] parametros = new SqlParameter[cantItems];

					for (int i = 0; i < cantNodos; i++)
					{
						string nombreParam = null;
						string valorParam = null;
						nombreParam = "@" + root.ChildNodes[i].Name;
						valorParam = root.ChildNodes[i].InnerText;

						if (valorParam.Length > 0)
						{
							string wTipoDatoStruct = root.ChildNodes[i].Attributes["dataType"].Value.ToUpper();
							switch (wTipoDatoStruct)
							{
								case "NUMERIC":
									valorParam = MycConvert.AdaptToDecimalDB(valorParam);
									break;
								case "DATETIME":
									valorParam = MycConvert.AdaptToDateDB(valorParam);
									break;
								case "DATE":
									valorParam = MycConvert.AdaptToDateDB(valorParam);
									break;
								case "STRING":
									valorParam = MycConvert.AdaptToStringDB(valorParam);
									break;
							}

							cmd.Parameters.AddWithValue(nombreParam, valorParam);
						}
					}

					SqlConnection conn = GetConnection(pStrConn);
					cmd.Connection = conn;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					cmd.ExecuteNonQuery();
					SqlDataAdapter da = new SqlDataAdapter(cmd);
					da.Fill(dsResult);

					if (conn != null)
						cmd.Connection.Dispose();
				}
			}
			catch (Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", pSPname, e.Message));
			}
			return dsResult;
		}

		#endregion

		#region *Ejecuta un SP de consulta pasándole los parámetros en un string XML y el string de conexión. Devuelve un DATASET.

		/// <summary>
		/// Ejecuta un Stored Procedure de consulta devolviendo un DataSet con los datos.
		/// Los parámetros se pasan en un STRING XML.
		/// </summary>
		/// <param name="pSPname">Nombre del Stored Procedure</param>
		/// <param name="pXMLParam">Un string XML conteniendo los parámetros a pasarle al Stored Procedure y sus valores. El nombre del nodo debe llamase como el parámetro, 
		/// y su contenido como valor.</param>
		/// <param name="pStrConn">String de Conexión</param>
		/// <returns></returns>
		public DataSet ExecuteSpReader(string pSPname, string pStrXMLParam, string pStrConn)
		{
			DataSet dsResult = new DataSet(); //DataSet con resultado
			//#line 1000

			XmlDocument xDoc = new XmlDocument();
			xDoc.LoadXml(pStrXMLParam);

			try
			{
				XmlNode root = xDoc.FirstChild;
				if (root.HasChildNodes)
				{
					int cantNodos = root.ChildNodes.Count;
					int cantItems = root.ChildNodes[0].ChildNodes.Count;

					SqlCommand cmd = new SqlCommand();

					SqlParameter[] parametros = new SqlParameter[cantItems];

					for (int i = 0; i < cantNodos; i++)
					{
						string nombreParam = null;
						string valorParam = null;
						nombreParam = "@" + root.ChildNodes[i].Name;
						valorParam = root.ChildNodes[i].InnerText;

						if (valorParam.Length > 0)
						{
							string wTipoDatoStruct = root.ChildNodes[i].Attributes["dataType"].Value.ToUpper();
							switch (wTipoDatoStruct)
							{
								case "NUMERIC":
									valorParam = MycConvert.AdaptToDecimalDB(valorParam);
									break;
								case "DATETIME":
									valorParam = MycConvert.AdaptToDateDB(valorParam);
									break;
								case "DATE":
									valorParam = MycConvert.AdaptToDateDB(valorParam);
									break;
								case "STRING":
									valorParam = MycConvert.AdaptToStringDB(valorParam);
									break;
							}

							cmd.Parameters.AddWithValue(nombreParam, valorParam);
						}
					}
					SqlConnection conn = GetConnection(pStrConn);
					cmd.Connection = conn;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					cmd.ExecuteNonQuery();
					SqlDataAdapter da = new SqlDataAdapter(cmd);
					da.Fill(dsResult);

					if (conn != null)
						cmd.Connection.Dispose();
				}
			}
			catch (Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", pSPname, e.Message));
			}
			return dsResult;
		}

		#endregion

		#region Ejecuta una consulta SQL literal

		public DataSet ExecuteSQLquery(string pSQL)
		{
			DataSet dsResult = new DataSet(); //DataSet con resultado
			try
			{
				SqlCommand cmd = new SqlCommand(pSQL);

				SqlConnection conn = GetConnection();
				cmd.Connection = conn;
				//cmd.ExecuteNonQuery();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(dsResult);

				if (conn != null)
					cmd.Connection.Dispose();

			}
			catch (Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", pSQL, e.Message));
			}
			return dsResult;
		}

		#endregion

		#region Consulta el valor máximo de un campo.  

		/// <summary>
		/// Consulta el valor máximo de un campo. Se puede utilizar para obtener el último ID de un registro insertado.
		/// </summary>
		/// <param name="p_Table"></param>
		/// <param name="p_Field"></param>
		/// <remarks>Si este mtodo se utiliza para obtener el último ID, se debe 
		/// tener cuidado de que el valor leído corresponda al registro insertado por el
		/// mismo usuario y no por otro.</remarks>
		/// <returns>El valor máximo encontrado. Si no hay datos, devuelve un string vacío.</returns>
		public string getMaxValue(string p_Table, string p_Field)
		{
			string w_Result = string.Empty;
			string w_SQL = string.Format("SELECT MAX({0}) FROM {1}", p_Field, p_Table);
			DataSet ds = new DataSet();

			try
			{
				SqlCommand cmd = new SqlCommand(w_SQL);

				SqlConnection conn = GetConnection();
				cmd.Connection = conn;
				cmd.ExecuteNonQuery();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(ds);

				if (conn != null)
					cmd.Connection.Dispose();

				if (ds.Tables[0].Rows.Count == 1)
					w_Result = ds.Tables[0].Rows[0][0].ToString();

			}
			catch (Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", w_SQL, e.Message));
			}

			return w_Result;
		}

		#endregion

		#region ==== Ejecuta un SP y recibe como parametro un DataSet

		/// <summary>
		/// Ejecuta un SP y recibe como parametro un DataSet
		/// </summary>
		///<param name="p_SPname">El nombre del stored procedure a ejecutar</param>
		/// <param name="p_DSdbTable">DataSet con los datos. El datatset corresponde a una tabla en la base de datos.
		/// Los nombres de los datarows corresponden a los parámetros del SP.</param>
		/// <returns>
		/// SI LA CONSULTA RESULTÓ EXITOSA:
		/// La tabla "DATA" devuelve los valores del resultado de la consulta.
		/// La tabla "RESULT" devuelve un datarow "OK" con TRUE.
		/// La tabla "RESULT" devuelve un datarow "DESC" con la cantidad de registros leidos(*).
		/// SI FALLÓ LA CONSULTA:
		/// La tabla "RESULT" devuelve un datarow "OK" con FALSE.
		/// La tabla "RESULT" devuelve un datarow "DESC" con el mensaje de error.
		/// La tabla "DATA" devuelve datarows nombrados con los parámetros pasados a la función, y su valor.
		/// (*) Solamente devuelve la cantidad de registros leidos si la consulta se trata de un SELECT.
		/// Si se tratara de un UPDATE o DELETE, devuelve 0 en este campo
		/// 
		/// </returns>
		public DataSet ExecuteSPquery(string p_SPname, DataSet p_DSdbTable)
		{
			//int w_RecsAffected = 0;

			DataSet dsResult = new DataSet("QUERY_RESULT"); //DataSet con resultado

			DataTable dtMain = new DataTable("DATA");
			DataTable dtRes = new DataTable("RESULT");
			dtRes.Columns.Add("OK");
			dtRes.Columns.Add("DESC");
			DataRow dr = dtRes.NewRow();

			try
			{
				try
				{
					SqlCommand cmd = new SqlCommand();

					DataRow drIn = p_DSdbTable.Tables[0].Rows[0];
					DataTable dtIn = p_DSdbTable.Tables[0];

					for (int i = 0; i < dtIn.Columns.Count; i++)
					{
						//parámetros para el SP
						string w_ColumnName = dtIn.Columns[i].ColumnName;
						string nombreParam = "@" + w_ColumnName;
						cmd.Parameters.AddWithValue(nombreParam, drIn[i]);
					}


					SqlConnection conn = GetConnection();
					cmd.Connection = conn;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = p_SPname;
					SqlDataAdapter da = new SqlDataAdapter(cmd);
					da.Fill(dsResult, "DATA");


					if (conn != null)
						cmd.Connection.Dispose();

					//Todo bien. Devolver en la tabla de resultados la cantidad de registros afectados.
					dr["OK"] = true;
					if (dsResult.Tables.Count > 0) //Por si el SP no devuelve nada (al menos debería devolver la cant de registros afectados)
						dr["DESC"] = dsResult.Tables["DATA"].Rows.Count;
					else
						dr["DESC"] = 0;

					dtRes.Rows.Add(dr);
					dsResult.Tables.Add(dtRes);

				}
				catch (SqlException sqlEx)
				{
					//Si hubo error, la tabla de datos en lugar de los datos devuelve los parámetros recibidos y sus valores.
					DataRow drFail = dtMain.NewRow();
					dtMain.Columns.Add("p_SPname");
					dtMain.Columns.Add("p_DSdbTable");
					drFail["p_SPname"] = p_SPname;
					drFail["p_DSdbTable"] = p_DSdbTable;
					dtMain.Rows.Add(drFail);
					dsResult.Tables.Add(dtMain);

					//La tabla de resultado devuelve el error
					dtRes.Rows.Add(dr);
					dr["OK"] = false;
					dr["DESC"] = sqlEx.Message;
					dsResult.Tables.Add(dtRes);
				}
			}

			catch (Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", p_SPname, e.Message));
			}

			return dsResult;
		}

		#endregion

		#region ==== Ejecuta un SP y recibe como parametro un DataSet y el string de conexión

		/// <summary>
		/// Ejecuta un SP y recibe como parametro un DataSet y el string de conexión
		/// </summary>
		///<param name="p_SPname">El nombre del stored procedure a ejecutar</param>
		/// <param name="p_DSdbTable">DataSet con los datos. El datatset corresponde a una tabla en la base de datos.
		/// Los nombres de los datarows corresponden a los parámetros del SP.</param>
		/// <param name="p_StrConn">String de conexión a la base de datos</param>
		/// <returns>
		/// SI LA CONSULTA RESULTÓ EXITOSA:
		/// La tabla "DATA" devuelve los valores del resultado de la consulta.
		/// La tabla "RESULT" devuelve un datarow "OK" con TRUE.
		/// La tabla "RESULT" devuelve un datarow "DESC" con la cantidad de registros leidos(*).
		/// SI FALLÓ LA CONSULTA:
		/// La tabla "RESULT" devuelve un datarow "OK" con FALSE.
		/// La tabla "RESULT" devuelve un datarow "DESC" con el mensaje de error.
		/// La tabla "DATA" devuelve datarows nombrados con los parámetros pasados a la función, y su valor.
		/// (*) Solamente devuelve la cantidad de registros leidos si la consulta se trata de un SELECT.
		/// Si se tratara de un UPDATE o DELETE, devuelve 0 en este campo
		/// 
		/// </returns>
		public DataSet ExecuteSPquery(string p_SPname, DataSet p_DSdbTable, string p_StrConn)
		{
			//int w_RecsAffected = 0;

			DataSet dsResult = new DataSet("QUERY_RESULT"); //DataSet con resultado

			DataTable dtMain = new DataTable("DATA");
			DataTable dtRes = new DataTable("RESULT");
			dtRes.Columns.Add("OK");
			dtRes.Columns.Add("DESC");
			DataRow dr = dtRes.NewRow();

			try
			{
				try
				{
					SqlCommand cmd = new SqlCommand();

					DataRow drIn = p_DSdbTable.Tables[0].Rows[0];
					DataTable dtIn = p_DSdbTable.Tables[0];

					for (int i = 0; i < dtIn.Columns.Count; i++)
					{
						//parámetros para el SP
						string w_ColumnName = dtIn.Columns[i].ColumnName;
						string nombreParam = "@" + w_ColumnName;
						cmd.Parameters.AddWithValue(nombreParam, drIn[i]);
					}


					SqlConnection conn = GetConnection(p_StrConn);
					cmd.Connection = conn;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = p_SPname;
					SqlDataAdapter da = new SqlDataAdapter(cmd);
					da.Fill(dsResult, "DATA");


					if (conn != null)
						cmd.Connection.Dispose();

					//Todo bien. Devolver en la tabla de resultados la cantidad de registros afectados.
					dr["OK"] = true;
					if (dsResult.Tables.Count > 0) //Por si el SP no devuelve nada (al menos debería devolver la cant de registros afectados)
						dr["DESC"] = dsResult.Tables["DATA"].Rows.Count;
					else
						dr["DESC"] = 0;

					dtRes.Rows.Add(dr);
					dsResult.Tables.Add(dtRes);

				}
				catch (SqlException sqlEx)
				{
					//Si hubo error, la tabla de datos en lugar de los datos devuelve los parámetros recibidos y sus valores.
					DataRow drFail = dtMain.NewRow();
					dtMain.Columns.Add("p_SPname");
					dtMain.Columns.Add("p_DSdbTable");
					drFail["p_SPname"] = p_SPname;
					drFail["p_DSdbTable"] = p_DSdbTable;
					dtMain.Rows.Add(drFail);
					dsResult.Tables.Add(dtMain);

					//La tabla de resultado devuelve el error
					dtRes.Rows.Add(dr);
					dr["OK"] = false;
					dr["DESC"] = sqlEx.Message;
					dsResult.Tables.Add(dtRes);
				}
			}

			catch (Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", p_SPname, e.Message));
			}

			return dsResult;
		}

		#endregion

		#endregion

		#region ====== Insert

		#region ===== Ejecuta un SP de INSERT pasándole un DataSet y retorna un DataSet

		/// <summary>
		/// Ejecuta un SP de INSERT (_ins) pasándole un DATASET.
		/// El string de conexión lo toma del archivo de configuración, cuya clave es "ConnString".
		/// </summary>
		/// <param name="pSPname">Nombre del Stored Procedure</param>
		/// <param name="p_DSdbTable">DataSet con los datos. El datatset corresponde a una tabla en la base de datos.
		/// Los nombres de los datarows corresponden a los parámetros del SP.</param>
		/// <returns>
		/// SI LA CONSULTA RESULTÓ EXITOSA:
		/// La tabla "DATA" devuelve los valores del resultado de la consulta.
		/// La tabla "RESULT" devuelve un datarow "OK" con TRUE.
		/// La tabla "RESULT" devuelve un datarow "DESC" con el último ID insertado. Si el SP no devuelve el @@IDENTITY, 
		/// porque no tiene ID autonumérico, entonces en lugar del último ID se retorna -1.
		/// SI FALLÓ LA CONSULTA:
		/// La tabla "RESULT" devuelve un datarow "OK" con FALSE.
		/// La tabla "RESULT" devuelve un datarow "DESC" con el mensaje de error.
		/// La tabla "DATA" devuelve datarows nombrados con los parámetros pasados a la función, y su valor.
		/// (*) El SP debe finalizar con SELECT @@IDENTITY.
		/// </returns>
		public DataSet ExecuteSpInsert_DS(string p_SPname, DataSet p_DSdbTable)
		{
			//int w_UltimoID = 0;

			DataSet dsResult = new DataSet("INSERT_RESULT"); //DataSet con resultado

			DataTable dtMain = new DataTable("DATA");
			DataTable dtRes = new DataTable("RESULT");
			dtRes.Columns.Add("OK");
			dtRes.Columns.Add("DESC");
			DataRow dr = dtRes.NewRow();
            dsResult.Tables.Add(dtMain);//Nuevo
            dsResult.Tables.Add(dtRes);//Nuevo

            try
            {
                //#line 1100
                try
                {
                 dsResult = FillCommand(0,p_DSdbTable, dsResult, p_SPname);//Nuevo

#region Viejo
//                 //#line 1100
//                 try
//                 {
//                     DataRow drIn = p_DSdbTable.Tables[0].Rows[0];
//                     DataTable dtIn = p_DSdbTable.Tables[0];

//                     SqlCommand cmd = new SqlCommand();

//                     for (int i = 0; i < dtIn.Columns.Count; i++)
//                     {
//                         string nombreParam = "@" + dtIn.Columns[i].ColumnName;
//                         cmd.Parameters.AddWithValue(nombreParam, drIn[i]);

//                         if (dtIn.Columns[i].DataType.ToString() == "System.Byte[]")
//                             cmd.Parameters[nombreParam].SqlDbType = SqlDbType.Image;
//                     }

//                     SqlConnection conn = GetConnection();
//                     cmd.Connection = conn;
//                     cmd.CommandType = CommandType.StoredProcedure;
//                     cmd.CommandText = p_SPname;

//                     SqlDataAdapter da = new SqlDataAdapter(cmd);
//                     da.Fill(dsResult, "DATA");
//                     try
//                     {
//                         if (dsResult.Tables[0].Rows.Count > 0)
//                             w_UltimoID = Convert.ToInt32(dsResult.Tables[0].Rows[0][0]);
//                     }
//                     catch
//                     {
//                         w_UltimoID = -1;
//                     }

//                     if (conn != null)
//                         conn.Dispose();

//                     //Todo bien. Devolver en la tabla de resultados el ID insertado.
//                     dr["OK"] = true;
//                     if (dsResult.Tables.Count > 0) //Por si el SP no devuelve nada (al menos debería devolver la cant de registros afectados)
//                         dr["DESC"] = w_UltimoID;
//                     else
//                         dr["DESC"] = 0;

//                     dtRes.Rows.Add(dr);
//                     dsResult.Tables.Add(dtRes);
#endregion
				}
                catch (SqlException sqlEx)
                {
                    //Si hubo error, la tabla de datos en lugar de los datos devuelve los parámetros recibidos y sus valores.
                    DataRow drFail = dsResult.Tables["DATA"].NewRow();
                    dsResult.Tables["DATA"].Columns.Add("p_SPname");
                    dsResult.Tables["DATA"].Columns.Add("p_DSdbTable");
                    drFail["p_SPname"] = p_SPname;
                    drFail["p_DSdbTable"] = p_DSdbTable;
                    dsResult.Tables["DATA"].Rows.Add(drFail);
                    //dsResult.Tables.Add(dtMain);

                    //La tabla de resultado devuelve el error

                    //dsResult.Tables["RESULT"].Rows.Add(dr);
                    //dtRes.Rows.Add(dr);
                    dr["OK"] = false;
                    dr["DESC"] = sqlEx.Message;
                    dsResult.Tables["RESULT"].Rows.Add(dr);


                }
			}


			catch (Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", p_SPname, e.Message));
			}

			return dsResult;
		}

		#endregion
        private DataSet FillCommand(int p_Row, DataSet p_DSdbTable, DataSet p_ds, string p_SPname)
        {
            try
            {
                int w_UltimoID = 0;
                //DataTable dtRes = new DataTable("RESULT");
                //dtRes.Columns.Add("OK");
                //dtRes.Columns.Add("DESC");
                DataRow dr = p_ds.Tables["RESULT"].NewRow();

                SqlCommand cmd = new SqlCommand();
                foreach (DataTable dt in p_DSdbTable.Tables)
                {
                    if (dt.Rows.Count > 0)
                    {

                        if (p_Row < dt.Rows.Count)
                        {
                            DataRow dr1 = p_DSdbTable.Tables[dt.TableName].Rows[p_Row];

                            foreach (DataColumn dc in dt.Columns)
                            {
                               
                                cmd.Parameters.AddWithValue("@" + dc.ColumnName, dr1[dc]);
                                //Chris. Agregue esta linea porque cuando es un Campo Imagen el .Net pone como tipo de Datos "nvarchar".
                                //Aca pregunto si es un System.Byte le asigno a la columna el tipo de Datos del Sql. "Image".
                                if (dc.DataType.ToString() == "System.Byte[]")
                                    cmd.Parameters["@" + dc.ColumnName].SqlDbType = SqlDbType.Image;
                                //Chris
                            }

                        }
                        else
                        {


                            return p_ds;


                        }
                    }

                }
                SqlConnection conn = GetConnection();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = p_SPname;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(p_ds, "DATA");
                try
                {
                    if (p_ds.Tables["DATA"].Rows.Count > 0)
                        w_UltimoID = Convert.ToInt32(p_ds.Tables["DATA"].Rows[0][0]);
                }
                catch
                {
                    w_UltimoID = -1;
                }

                if (conn != null)
                    conn.Dispose();
                //Todo bien. Devolver en la tabla de resultados el ID insertado.
                dr["OK"] = true;
                if (p_ds.Tables.Count > 0) //Por si el SP no devuelve nada (al menos debería devolver la cant de registros afectados)
                    dr["DESC"] = w_UltimoID;
                else
                    dr["DESC"] = 0;
                p_ds.Tables["RESULT"].Rows.Add(dr);
                //p_ds.Tables.Add(dtRes);
                p_Row++;

                FillCommand(p_Row, p_DSdbTable, p_ds, p_SPname);
                return p_ds;
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
                
            }
        }

		#region ===== Ejecuta un SP de INSERT pasándole un DataSet y el String de Conexión y retorna un DataSet

		/// <summary>
		/// Ejecuta un SP de INSERT (_ins) pasándole un DATASET y el String de Conexión.
		/// </summary>
		/// <param name="pSPname">Nombre del Stored Procedure</param>
		/// <param name="p_DSdbTable">DataSet con los datos. El datatset corresponde a una tabla en la base de datos.
		/// <param name="p_StrConn">String de conexión a la base de datos.</param>
		/// Los nombres de los datarows corresponden a los parámetros del SP.</param>
		/// <returns>
		/// SI LA CONSULTA RESULTÓ EXITOSA:
		/// La tabla "DATA" devuelve los valores del resultado de la consulta.
		/// La tabla "RESULT" devuelve un datarow "OK" con TRUE.
		/// La tabla "RESULT" devuelve un datarow "DESC" con el último ID insertado. Si el SP no devuelve el @@IDENTITY, 
		/// entonces en lugar del último ID se retorna -1.
		/// SI FALLÓ LA CONSULTA:
		/// La tabla "RESULT" devuelve un datarow "OK" con FALSE.
		/// La tabla "RESULT" devuelve un datarow "DESC" con el mensaje de error.
		/// La tabla "DATA" devuelve datarows nombrados con los parámetros pasados a la función, y su valor.
		/// (*) El SP debe finalizar con SELECT @@IDENTITY.
		/// </returns>
		public DataSet ExecuteSpInsert_DS(string p_SPname, DataSet p_DSdbTable, string p_StrConn)
		{
			int w_UltimoID = 0;

			DataSet dsResult = new DataSet("INSERT_RESULT"); //DataSet con resultado

			DataTable dtMain = new DataTable("DATA");
			DataTable dtRes = new DataTable("RESULT");
			dtRes.Columns.Add("OK");
			dtRes.Columns.Add("DESC");
			DataRow dr = dtRes.NewRow();

			try
			{
				//#line 1100
				try
				{
					DataRow drIn = p_DSdbTable.Tables[0].Rows[0];
					DataTable dtIn = p_DSdbTable.Tables[0];

					SqlCommand cmd = new SqlCommand();

					for (int i = 0; i < dtIn.Columns.Count; i++)
					{
						string nombreParam = "@" + dtIn.Columns[i].ColumnName;
						cmd.Parameters.AddWithValue(nombreParam, drIn[i]);
					}

					SqlConnection conn = GetConnection(p_StrConn);
					cmd.Connection = conn;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = p_SPname;

					SqlDataAdapter da = new SqlDataAdapter(cmd);
					DataSet ds = new DataSet();
					da.Fill(dsResult, "DATA");
					try
					{
						if (dsResult.Tables[0].Rows.Count > 0)
							w_UltimoID = Convert.ToInt32(dsResult.Tables[0].Rows[0][0]);
					}
					catch
					{
						w_UltimoID = -1;
					}

					if (conn != null)
						conn.Dispose();


					//Todo bien. Devolver en la tabla de resultados la cantidad de registros afectados, o el último ID, según devuelva el SP.
					dr["OK"] = true;
					if (dsResult.Tables.Count > 0) //Por si el SP no devuelve nada (al menos debería devolver la cant de registros afectados)
						dr["DESC"] = w_UltimoID;
					else
						dr["DESC"] = 0;

					dtRes.Rows.Add(dr);
					dsResult.Tables.Add(dtRes);
				}
				catch (SqlException sqlEx)
				{
					//Si hubo error, la tabla de datos en lugar de los datos devuelve los parámetros recibidos y sus valores.
					DataRow drFail = dtMain.NewRow();
					dtMain.Columns.Add("p_SPname");
					dtMain.Columns.Add("p_DSdbTable");
					dtMain.Columns.Add("p_StrConn");
					drFail["p_SPname"] = p_SPname;
					drFail["p_DSdbTable"] = p_DSdbTable;
					drFail["p_StrConn"] = p_StrConn;
					dtMain.Rows.Add(drFail);
					dsResult.Tables.Add(dtMain);

					//La tabla de resultado devuelve el error
					dtRes.Rows.Add(dr);
					dr["OK"] = false;
					dr["DESC"] = sqlEx.Message;
					dsResult.Tables.Add(dtRes);
				}
			}
			catch (Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", p_SPname, e.Message));
			}

			return dsResult;
		}

		#endregion

		/// <summary>
		/// Ejuecuta una inserción mediante un SQL literal.
		/// </summary>
		/// <param name="pSQL">String SQL de inserción.</param>
		/// <returns>1 si insertó y 0 si no pudo insertar.</returns>
		public int ExecuteSQLinsert(string pSQL)
		{
			int w_Result = 0;
			try
			{
				SqlCommand cmd = new SqlCommand(pSQL);

				SqlConnection conn = GetConnection();
				cmd.Connection = conn;
				w_Result = cmd.ExecuteNonQuery();

				if (conn != null)
					cmd.Connection.Dispose();

			}
			catch
			{
			}

			return w_Result;
		}

		#region *Ejecuta un SP de INSERT pasándole un string XML.

		/// <summary>
		/// Ejecuta un SP de INSERT (_ins) pasándole un DATASET.
		/// El string de conexión lo toma del archivo de configuración, cuya clave es "ConnString".
		/// </summary>
		/// <param name="pSPname">Nombre del Stored Procedure</param>
		/// <param name="pStrXMLParam">Un string XML conteniendo los parámetros a pasarle al Stored Procedure y sus valores. El nombre del nodo debe llamase como el parámetro, 
		/// y su contenido como valor.</param>		
		public int ExecuteSpInsert(string pSPname, string pStrXMLparam)
		{
			int wUltimoID = 0;

			try
			{
				//#line 1200
				XmlDocument xDoc = new XmlDocument();
				xDoc.LoadXml(pStrXMLparam);

				XmlNode root = xDoc.FirstChild;
				if (root.HasChildNodes)
				{
					int cantNodos = root.ChildNodes.Count;
					int cantItems = root.ChildNodes[0].ChildNodes.Count;

					SqlCommand cmd = new SqlCommand();
					SqlParameter[] parametros = new SqlParameter[cantItems];


					for (int i = 0; i < cantNodos; i++)
					{
						string nombreParam = null;
						string valorParam = null;
						nombreParam = "@" + root.ChildNodes[i].Name;
						valorParam = root.ChildNodes[i].InnerText;

						if (valorParam.Length > 0)
						{
							string wTipoDatoStruct = root.ChildNodes[i].Attributes["dataType"].Value.ToUpper();
							switch (wTipoDatoStruct)
							{
								case "NUMERIC":
									valorParam = MycConvert.AdaptToDecimalDB(valorParam);
									break;
								case "DATETIME":
									valorParam = MycConvert.AdaptToDateDB(valorParam);
									break;
								case "DATE":
									valorParam = MycConvert.AdaptToDateDB(valorParam);
									break;
								case "STRING":
									valorParam = MycConvert.AdaptToStringDB(valorParam);
									break;
							}

							cmd.Parameters.AddWithValue(nombreParam, valorParam);
						}
					}

					SqlConnection conn = GetConnection();
					cmd.Connection = conn;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					SqlDataAdapter da = new SqlDataAdapter(cmd);
					DataSet ds = new DataSet();
					da.Fill(ds);
					if (ds.Tables[0].Rows.Count > 0)
						wUltimoID = Convert.ToInt32(ds.Tables[0].Rows[0][0]);

					if (conn != null)
						cmd.Connection.Dispose();

				}
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", pSPname, ex.Message));
			}
			return wUltimoID;
		}

		#endregion

		#region Ejecuta un SP de INSERT pasándole un objeto XML.

		/// <summary>
		/// Ejecuta un SP de INSERT (_ins) pasándole un DataSet y el string de conexión.
		/// Los parámetros se pasan en un OBJETO XML.
		/// El string de conexión lo toma del archivo de configuración, cuya clave es "ConnString".
		/// </summary>
		/// <param name="pSPname">Nombre del Stored Procedure</param>
		/// <param name="pXMLParam">Un objeto XML conteniendo los parámetros a pasarle al Stored Procedure y sus valores. El nombre del nodo debe llamase como el parámetro, 
		/// y su contenido como valor.</param>		
		public int ExecuteSpInsert(string pSPname, XmlDocument pXMLparam)
		{
			int wUltimoID = 0;

			try
			{
				//#line 1300
				XmlNode root = pXMLparam.FirstChild;
				if (root.HasChildNodes)
				{
					int cantNodos = root.ChildNodes.Count;
					int cantItems = root.ChildNodes[0].ChildNodes.Count;

					SqlCommand cmd = new SqlCommand();
					SqlParameter[] parametros = new SqlParameter[cantItems];

					for (int i = 0; i < cantNodos; i++)
					{
						string nombreParam = null;
						string valorParam = null;
						nombreParam = "@" + root.ChildNodes[i].Name;
						valorParam = root.ChildNodes[i].InnerText;
						cmd.Parameters.AddWithValue(nombreParam, valorParam);
					}

					SqlConnection conn = GetConnection();
					cmd.Connection = conn;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					SqlDataAdapter da = new SqlDataAdapter(cmd);
					DataSet ds = new DataSet();
					da.Fill(ds);
					if (ds.Tables[0].Rows.Count > 0)
						wUltimoID = Convert.ToInt32(ds.Tables[0].Rows[0][0]);

					if (conn != null)
						cmd.Connection.Dispose();

				}
			}
			catch (Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", pSPname, e.Message));
			}
			return wUltimoID;
		}

		#endregion

		#region Ejecuta un SP de INSERT pasándole un DataSet.

		/// <summary>
		/// Ejecuta un SP de INSERT (_ins) pasándole un DATASET.
		/// El string de conexión lo toma del archivo de configuración, cuya clave es "ConnString".
		/// </summary>
		/// <param name="pSPname">Nombre del Stored Procedure</param>
		/// <param name="pDStableBD">DataSet con los datos. Los nombres de los campos corresponden a los parámetros del SP.</param>
		public int ExecuteSpInsert(string pSPname, DataSet pDStableBD)
		{
			int wUltimoID = 0;

			try
			{
				//#line 1100
				DataRow dr = pDStableBD.Tables[0].Rows[0];
				DataTable dt = pDStableBD.Tables[0];

				SqlCommand cmd = new SqlCommand();

				for (int i = 0; i < dt.Columns.Count; i++)
				{
					string nombreParam = "@" + dt.Columns[i].ColumnName;
					cmd.Parameters.AddWithValue(nombreParam, dr[i]);
				}

				SqlConnection conn = GetConnection();
				cmd.Connection = conn;
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = pSPname;

				SqlDataAdapter da = new SqlDataAdapter(cmd);
				DataSet ds = new DataSet();
				da.Fill(ds);
				if (ds.Tables[0].Rows.Count > 0)
					wUltimoID = Convert.ToInt32(ds.Tables[0].Rows[0][0]);

				if (conn != null)
					conn.Dispose();
			}
			catch (Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", pSPname, e.Message));
			}
			return wUltimoID;
		}

		#endregion

		#region Ejecuta un SP de INSERT pasándole un DataSet y el string de conexión.

		/// <summary>
		/// Ejecuta un SP de INSERT (_ins) pasándole un DATASET y el string de conexión.
		/// </summary>
		/// <param name="pSPname">Nombre del Stored Procedure</param>
		/// <param name="pDStableBD">DataSet con los datos. Los nombres de los campos corresponden a los parámetros del SP.
		/// Si el ID de la tabla es clave primaria autonumético (Identity) no se debe pasar este parámetro.</param>
		/// <param name="pStrConn">String de conexión</param>
		public int ExecuteSpInsert(string pSPname, DataSet pDStableBD, string pStrConn)
		{
			int wUltimoID = 0;

			try
			{
				//#line 1400
				DataRow dr = pDStableBD.Tables[0].Rows[0];
				DataTable dt = pDStableBD.Tables[0];

				SqlCommand cmd = new SqlCommand();

				for (int i = 0; i < dt.Columns.Count; i++)
				{
					string nombreParam = "@" + dt.Columns[i].ColumnName;
					cmd.Parameters.AddWithValue(nombreParam, dr[i]);
				}

				SqlConnection conn = GetConnection(pStrConn);
				cmd.Connection = conn;
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = pSPname;
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				DataSet ds = new DataSet();
				da.Fill(ds);
				if (ds.Tables[0].Rows.Count > 0)
					wUltimoID = Convert.ToInt32(ds.Tables[0].Rows[0][0]);

				if (conn != null)
					cmd.Connection.Dispose();
			}
			catch (Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", pSPname, e.Message));
			}
			return wUltimoID;
		}

		#endregion

		#region Ejecuta un SP de INSERT pasándole un objeto XML y el string de conexión.

		/// <summary>
		/// Ejecuta un SP de INSERT (_ins) pasándole un DataSet y el string de conexión.
		/// Los parámetros se pasan en un OBJETO XML.
		/// </summary>
		/// <param name="pSPname">Nombre del Stored Procedure</param>
		/// <param name="pXMLParam">Un objeto XML conteniendo los parámetros a pasarle al Stored Procedure y sus valores. El nombre del nodo debe llamase como el parámetro, 
		/// y su contenido como valor.
		/// Si el ID de la tabla es clave primaria autonumético (Identity) no se debe pasar este parámetro.</param>
		/// <param name="pStrConn">String de conexión</param>
		public int ExecuteSpInsert(string pSPname, XmlDocument pXMLparam, string pStrConn)
		{
			int wUltimoID = 0;

			try
			{
				//#line 1500
				XmlNode root = pXMLparam.FirstChild;
				if (root.HasChildNodes)
				{
					int cantNodos = root.ChildNodes.Count;
					int cantItems = root.ChildNodes[0].ChildNodes.Count;

					SqlCommand cmd = new SqlCommand();
					SqlParameter[] parametros = new SqlParameter[cantItems];

					for (int i = 0; i < cantNodos; i++)
					{
						string nombreParam = null;
						string valorParam = null;
						nombreParam = "@" + root.ChildNodes[i].Name;
						valorParam = root.ChildNodes[i].InnerText;
						cmd.Parameters.AddWithValue(nombreParam, valorParam);
					}

					SqlConnection conn = GetConnection(pStrConn);
					cmd.Connection = conn;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					SqlDataAdapter da = new SqlDataAdapter(cmd);
					DataSet ds = new DataSet();
					da.Fill(ds);
					if (ds.Tables[0].Rows.Count > 0)
						wUltimoID = Convert.ToInt32(ds.Tables[0].Rows[0][0]);

					if (conn != null)
						cmd.Connection.Dispose();

				}
			}
			catch (Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", pSPname, e.Message));
			}
			return wUltimoID;
		}

		#endregion

		#region Ejecuta un SP de INSERT pasándole un string XML y el string de conexión.

		/// <summary>
		/// Ejecuta un SP de INSERT (_ins) pasándole un DATASET y el string de conexión.
		/// Los parámetros se pasan en un STRING XML.
		/// </summary>
		/// <param name="pSPname">Nombre del Stored Procedure</param>
		/// <param name="pStrXMLParam">Un string XML conteniendo los parámetros a pasarle al Stored Procedure y sus valores. El nombre del nodo debe llamase como el parámetro, 
		/// y su contenido como valor.
		/// Si el ID de la tabla es clave primaria autonumético (Identity) no se debe pasar este parámetro.</param>		
		/// <param name="pStrConn">String de conexión</param>
		public int ExecuteSpInsert(string pSPname, string pStrXMLparam, string pStrConn)
		{
			int wUltimoID = 0;

			try
			{
				//#line 1600
				XmlDocument xDoc = new XmlDocument();
				xDoc.LoadXml(pStrXMLparam);

				XmlNode root = xDoc.FirstChild;
				if (root.HasChildNodes)
				{
					int cantNodos = root.ChildNodes.Count;
					int cantItems = root.ChildNodes[0].ChildNodes.Count;

					SqlCommand cmd = new SqlCommand();
					SqlParameter[] parametros = new SqlParameter[cantItems];

					for (int i = 0; i < cantNodos; i++)
					{
						string nombreParam = null;
						string valorParam = null;
						nombreParam = "@" + root.ChildNodes[i].Name;
						valorParam = root.ChildNodes[i].InnerText;
						if (valorParam == "")
							valorParam = null;

						cmd.Parameters.AddWithValue(nombreParam, valorParam);
					}

					SqlConnection conn = GetConnection(pStrConn);
					cmd.Connection = conn;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					SqlDataAdapter da = new SqlDataAdapter(cmd);
					DataSet ds = new DataSet();
					da.Fill(ds);
					if (ds.Tables[0].Rows.Count > 0)
						wUltimoID = Convert.ToInt32(ds.Tables[0].Rows[0][0]);

					if (conn != null)
						cmd.Connection.Dispose();

				}
			}
			catch (Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", pSPname, e.Message));
			}
			return wUltimoID;
		}

		#endregion

		#endregion

		#region ====== Delete

		#region Ejecuta un Stored Procedure que elimina un registro. 

		/// <summary>
		/// Ejecuta un Stored Procedure que elimina un registro. Se pasa el ID del registro y el nombre del Stored Procedure.
		/// </summary>
		/// <remarks>El nombre del parámetro del a pasar al Stored Procedure se toma del nombre pasado como parámetro. 
		/// Si el parámetro se llama "@ID_Precio", el nombre del Stored Procedure a pasar deberá llamarse "spPrecio_del".
		/// El nombre del parámetro siempre debe comenzar con "@ID_".
		/// </remarks>
		/// <param name="pSPname">string. Nombre del Stored Procedure</param>
		/// <param name="IDrow">ID del registro a eliminar</param>
		public void ExecuteSpDelete(string pSPname, int IDrow)
		{
			//#line 1700
            string parametro = pSPname.Replace("spMYC_", "@ID_").ToUpper();
			int ctrlString = parametro.IndexOf("_DEL");
			parametro = parametro.Replace("_DEL", "");

			if (parametro.IndexOf("@ID_") < 0 || ctrlString == 0)
			{
				throw new Exception(string.Format("Nombre de Stored Procedure incorrecto. Imposible eliminar el registro. [Param = {0}; SP = {1}]", parametro, pSPname));
			}
			try
			{
				SqlConnection conn = GetConnection();
				SqlCommand cmd = new SqlCommand();
				cmd.Parameters.AddWithValue(parametro, IDrow);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = pSPname;
				cmd.Connection = conn;
				cmd.ExecuteNonQuery();
				if (conn != null)
					conn.Dispose();
			}
			catch (Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", pSPname, e.Message));
			}
		}

		#endregion

		#region Ejecuta un Stored Procedure que elimina un registro, pasando el nombre del parámetro.

		/// <summary>
		/// Ejecuta un Stored Procedure que elimina un registro. Se pasa el nombre del Stored Procedure, 
		/// el nombre del parámetro que identifica el ID del registro a eliminar y el ID del registro.
		/// </summary>
		/// <remarks>
		/// Utilizar este método cuando el ID del registro a eliminar no coincida con el nombre de SP. 
		/// </remarks>
		/// <param name="pSPname">string. Nombre del Stored Procedure</param>
		/// <param name="pNombreParam">Nombre del parámetro del registro a eliminar</pNombreParam>
		/// <param name="IDrow">ID del registro a eliminar</param>
		public void ExecuteSpDelete(string pSPname, string pNombreParam, int IDrow)
		{
			//#line 1750
			string parametro = "@" + pNombreParam.Replace("@", "").ToUpper();

			try
			{
				SqlConnection conn = GetConnection();
				SqlCommand cmd = new SqlCommand();
				cmd.Parameters.AddWithValue(parametro, IDrow);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = pSPname;
				cmd.Connection = conn;
				cmd.ExecuteNonQuery();
				if (conn != null)
					conn.Dispose();
			}
			catch (Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", pSPname, e.Message));
			}
		}

		#endregion

		#region Ejecuta un Stored Procedure que elimina un registro, pasando el string de conexión.

		/// <summary>
		/// Ejecuta un Stored Procedure que elimina un registro. Se pasa el ID del regitro, el nombre del Stored Procedure
		/// y el string de conexión.
		/// </summary>
		/// <remarks>El nombre del parámetro del a pasar al Stored Procedure se toma del nombre pasado como parámetro. 
		/// Si el parámetro se llama "@ID_Precio", el nombre del Stored Procedure a pasar deberá llamarse "spPrecio_del".
		/// El nombre del parámetro siempre debe comenzar con "@ID_".
		/// </remarks>
		/// <param name="pSPname">string. Nombre del Stored Procedure</param>
		/// <param name="IDrow">int. ID del registro a eliminar</param>
		/// <param name="pSPname">string. String de Conexión</param>
		public void ExecuteSpDelete(string pSPname, int IDrow, string StrConn)
		{
			//#line 1800
			string parametro = pSPname.Replace("spMYC_", "@ID_").ToUpper();
			int ctrlString = parametro.IndexOf("_DEL");
			parametro = parametro.Replace("_DEL", "");

			if (parametro.IndexOf("@ID_") < 0 || ctrlString == 0)
			{
				throw new Exception(string.Format("Nombre de Stored Procedure incorrecto. Imposible eliminar el registro. [Param = {0}; SP = {1}]", parametro, pSPname));
			}
			try
			{
				SqlConnection conn = GetConnection(StrConn);
				SqlCommand cmd = new SqlCommand();
				cmd.Parameters.AddWithValue(parametro, IDrow);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = pSPname;
				cmd.Connection = conn;
				cmd.ExecuteNonQuery();
				if (conn != null)
					conn.Dispose();
			}
			catch (Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", pSPname, e.Message));
			}
		}

		#endregion

		#endregion

		#region ====== Update

		#region Ejecuta un SP de UPDATE pasándole un DataSet y retorna un DataSet.	

		/// <summary>
		/// Ejecuta un Stored Procedure de UPDATE (_UPD) pasándole un DataSet.
		/// El string de conexión lo toma del archivo de configuración, cuya clave es "ConnString".
		/// 
		/// El método devuelve los campos a modificarse con los valores de los campos antes de su modificación por el SP de update.
		/// Solamente son devueltos aquellos valores que hayan sido modificados por update.
		/// </summary>
		/// <param name="p_SPname">Nombre del Stored Procedure de actualización</param>
		/// <param name="p_DSdbTable">DataSet con los datos. El datatset corresponde a una tabla en la base de datos.
		/// Los campos primary key del DataSet son tomados como referencia para realizar la consulta de los datos antes 
		/// de que sean actualizados por el update.</param>
		/// <returns>
		/// 
		/// SI LA CONSULTA RESULTÓ EXITOSA:
		/// La tabla "DATA" trae los campos con los valores antes de ser modificados por el update.
		/// Solo se devuelven los campos si su valor ha sido modificado, es decir que solo se retornarán aquellos valores que hayan
		/// sido alterados (Si el valor actual es igual al nuevo luego del update, entonces no se devuelve el valor del campo).
		/// La tabla "RESULT" devuelve un DataRow "OK" con TRUE.
		/// La tabla "RESULT" devuelve un DataRow "DESC" con alguno de los siguientes valores:
		/// DESC = 0 (cero) significa que la consulta no afectó ningún registro. No se devuelve la tabla DATA en el DataSet resultante.
		/// DESC = 1 (uno) significa que el registro correspondiente a la condición definida por las claves primarias del DataSet pasado
		/// como parámetro (p_DSdbTable), fue modificado. Este valor (DESC = 1) es el esperado normalmente, y devolverá la tabla DATA con los valores 
		/// de los campos que hayan sido modificados.
		/// Como tercera opción, si DESC fuera mayor a uno, entonces este valor corresponde a la cantidad de registros resultantes de la consulta creada para determinar 
		/// cuáles campos fueron afectados. Este valor NO CORRESPONDE a la cantidad de registros afectados por el SP de update, por lo que no debe ser tomado
		/// como cantidad de registros afectados por la consulta.
		///	Si esto sucede, entonces es probable que el DataSet pasado como parámetro (p_DSdbTable) no contenga ningún campo definido como primary key.
		///	
		/// SI FALLÓ LA CONSULTA:
		/// La tabla "RESULT" devuelve un DataRow "OK" con FALSE.
		/// La tabla "RESULT" devuelve un DataRow "DESC" con el mensaje de error.
		/// La tabla "DATA" devuelve DataRows nombrados con los parámetros pasados a la función, y su valor.
		/// </returns>
		/// <remarks>El DataSet con los datos a modificar (p_DSdbTable) debe tener al menos un campo definido como primary key.
		/// Si el DataSet no contiene ninguna clave primaria, la tabla DATA del DataSet resultante no será devuelta.
		/// Si la consulta no afectó ningún registros (cero registros afectados), el DataSet resultantre
		/// tampoco devuelve la tabla DATA.</remarks>
		public DataSet ExecuteSPupdate_DS(string p_SPname, DataSet p_DSdbTable)
        {
            #region viejo mucci
            //int w_RecsAffected = 0;

            //DataSet dsResult = new DataSet("UPDATE_RESULT"); //DataSet con resultado
            //DataSet dsDataBefore = new DataSet(); //Datase con los datos antes del update

            //DataTable dtMain = new DataTable("DATA");
            //DataTable dtRes = new DataTable("RESULT");
            //dtRes.Columns.Add("OK");
            //dtRes.Columns.Add("DESC");
            //DataRow dr = dtRes.NewRow();
            //try
            //{
            //    StringBuilder w_SQLdataBefore = new StringBuilder("SELECT");

            //    try
            //    {
            //        //#line 1900
            //        StringBuilder w_WhereClause = new StringBuilder(" WHERE (1=1)");

            //        DataRow drIn = p_DSdbTable.Tables[0].Rows[0];
            //        DataTable dtIn = p_DSdbTable.Tables[0];

            //        SqlCommand cmd = new SqlCommand();

            //        for (int i = 0; i < dtIn.Columns.Count; i++)
            //        {
            //            //parámetros para el SP
            //            string w_ColumnName = dtIn.Columns[i].ColumnName;
            //            string nombreParam = "@" + w_ColumnName;
            //            cmd.Parameters.AddWithValue(nombreParam, drIn[i]);
                      
            //            if (dtIn.Columns[i].DataType.ToString() == "System.Byte[]")
            //               cmd.Parameters[nombreParam].SqlDbType = SqlDbType.Image;

            //            //campos del select
            //            if (i > 0) w_SQLdataBefore.Append(",");
            //            w_SQLdataBefore.Append(" " + w_ColumnName);

            //            //cláusula where, tomando los campos primary key
            //            if (dtIn.Columns[i].Unique)
            //                if (dtIn.Columns[i].DataType.ToString() == "System.String")
            //                {
            //                    w_WhereClause.Append(string.Format(" AND {0} = '{1}'", w_ColumnName, drIn[i]));
            //                }
            //                else
            //                {
            //                    w_WhereClause.Append(string.Format(" AND {0} = {1}", w_ColumnName, drIn[i]));
            //                }

            //        }

            //        w_SQLdataBefore.Append(string.Format(" FROM {0} {1}", dtIn.TableName, w_WhereClause));

            //        SqlConnection conn = GetConnection();
            //        cmd.Connection = conn;

            //        //Ejecutar las acciones en el entorno transaccional de SQL Server
            //        SqlTransaction w_Trans = conn.BeginTransaction();
            //        cmd.Transaction = w_Trans;

            //        try
            //        {
            //            //Consulta de datos antes de la actualización
            //            cmd.CommandText = w_SQLdataBefore.ToString();
            //            cmd.ExecuteNonQuery();

            //            //datset resultado con los campos de la consulta antes del update
            //            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //            da.Fill(dsDataBefore);


            //            //Actualización de los datos (ejecución del UPDATE mediante el SP)
            //            cmd.CommandType = CommandType.StoredProcedure;
            //            cmd.CommandText = p_SPname;
            //            cmd.ExecuteNonQuery();

            //            //Dejar solor los valores modifcados
            //            if (dsDataBefore.Tables[0].Rows.Count == 1)
            //            {
            //                DataTable dtQuery = p_DSdbTable.Tables[0];
            //                DataRow drResult = dtMain.NewRow();

            //                for (int i = 0; i < dtQuery.Columns.Count; i++)
            //                {
            //                    string w_ViejoVal = dsDataBefore.Tables[0].Rows[0][dtQuery.Columns[i].ColumnName].ToString();
            //                    string w_NuevoVal = dtQuery.Rows[0][dtQuery.Columns[i].ColumnName].ToString();
            //                    if (w_ViejoVal.CompareTo(w_NuevoVal) != 0)
            //                    {
            //                        dtMain.Columns.Add(dtQuery.Columns[i].ColumnName);
            //                        drResult[dtQuery.Columns[i].ColumnName] = w_ViejoVal;
            //                    }
            //                }
            //                dtMain.Rows.Add(drResult);
            //                dsResult.Tables.Add(dtMain);
            //            }

            //            try
            //            {
            //                if (dsDataBefore.Tables[0].Rows.Count > 0)
            //                    w_RecsAffected = Convert.ToInt32(dsDataBefore.Tables[0].Rows.Count);
            //            }
            //            catch
            //            {
            //                w_RecsAffected = -1;
            //            }


            //            //Todo bien. Devolver en la tabla de resultados la cantidad de registros afectados.
            //            dr["OK"] = true;
            //            dr["DESC"] = w_RecsAffected;

            //            dtRes.Rows.Add(dr);
            //            dsResult.Tables.Add(dtRes);

            //            w_Trans.Commit();

            //        }
            //        catch (SqlException sqlEx)
            //        {
            //            w_Trans.Rollback();
            //            //propagar el error
            //            throw sqlEx;
            //        }
            //        finally
            //        {
            //            if (conn != null)
            //                cmd.Connection.Dispose();
            //        }
            //    }
            //    catch (SqlException sqlEx)
            //    {
            //        //Si hubo error, la tabla de datos en lugar de los datos devuelve los parámetros recibidos y sus valores.
            //        //Tembien develve el query utilizado para ver los valores antes de la actualización.
            //        DataRow drFail = dtMain.NewRow();
            //        dtMain.Columns.Add("p_SPname");
            //        dtMain.Columns.Add("p_DSdbTable");
            //        dtMain.Columns.Add("SQLqueryDataBeforeUpdate");
            //        drFail["p_SPname"] = p_SPname;
            //        drFail["p_DSdbTable"] = p_DSdbTable;
            //        drFail["SQLqueryDataBeforeUpdate"] = w_SQLdataBefore.ToString();
            //        dtMain.Rows.Add(drFail);
            //        dsResult.Tables.Add(dtMain);

            //        //La tabla de resultado devuelve el error
            //        dtRes.Rows.Add(dr);
            //        dr["OK"] = false;
            //        dr["DESC"] = sqlEx.Message;
            //        dsResult.Tables.Add(dtRes);
            //    }
            //}
            //catch (Exception e)
            //{
            //    throw new Exception(string.Format("[SP: {0}] {1}", p_SPname, e.Message));
            //}

            //return dsResult;
            #endregion

            int w_RecsAffected = 0;

            DataSet dsResult = new DataSet("UPDATE_RESULT"); //DataSet con resultado
            DataSet dsDataBefore = new DataSet(); //Datase con los datos antes del update

            DataTable dtMain = new DataTable("DATA");
            DataTable dtRes = new DataTable("RESULT");
            dtRes.Columns.Add("OK");
            dtRes.Columns.Add("DESC");
            DataRow dr = dtRes.NewRow();
            try
            {
                StringBuilder w_SQLdataBefore = new StringBuilder("SELECT");

                try
                {
                    //#line 1900
                    StringBuilder w_WhereClause = new StringBuilder(" WHERE (1=1)");
                    SqlCommand cmd = new SqlCommand();
                    //DataRow drIn = p_DSdbTable.Tables[0].Rows[0];
                    //DataTable dtIn = p_DSdbTable.Tables[0];


                    foreach (DataTable dt in p_DSdbTable.Tables)
                    {
                        DataRow dr1 = p_DSdbTable.Tables[dt.TableName].Rows[0];
                        int i = 0;
                        //w_SQLdataBefore.Append(" " + dt.Columns[0].ColumnName);
                        foreach (DataColumn dc in dt.Columns)
                        {
                            cmd.Parameters.AddWithValue("@" + dc.ColumnName, dr1[dc]);
                            //i = i++;
                            string w_ColumnName = dc.ColumnName;
                            string nombreParam = "@" + w_ColumnName;
                         

                            //SI CAMBIAN ALGO AGREGUEN ESTAS 2 LINEAS DE NUEVO; HIJOS DE PUTA!!!
                            if (dt.Columns[i].DataType.ToString() == "System.Byte[]")
                                cmd.Parameters[nombreParam].SqlDbType = SqlDbType.Image;
                            //

                            if (i > 0) w_SQLdataBefore.Append(",");

                            w_SQLdataBefore.Append(" " + w_ColumnName);

                            


                            //cláusula where, tomando los campos primary key
                            if (dc.Unique)
                                if (dc.DataType.ToString() == "System.String")
                                {
                                    w_WhereClause.Append(string.Format(" AND {0} = '{1}'", w_ColumnName, dr1[dc]));
                                }
                                else
                                {
                                    w_WhereClause.Append(string.Format(" AND {0} = {1}", w_ColumnName, dr1[dc]));
                                }
                            i++;
                        }
                        w_SQLdataBefore.Append(string.Format(" FROM {0} {1}", dt.TableName, w_WhereClause));

                    }
                    //for (int i = 0; i < dtIn.Columns.Count; i++)
                    //{
                    //    //parámetros para el SP
                    //    string w_ColumnName = dtIn.Columns[i].ColumnName;
                    //    string nombreParam = "@" + w_ColumnName;
                    //    cmd.Parameters.Add(nombreParam, drIn[i]);

                    //    //campos del select
                    //    if (i > 0) w_SQLdataBefore.Append(",");
                    //    w_SQLdataBefore.Append(" " + w_ColumnName);

                    //    //cláusula where, tomando los campos primary key
                    //    if (dtIn.Columns[i].Unique)
                    //        if (dtIn.Columns[i].DataType.ToString() == "System.String")
                    //        {
                    //            w_WhereClause.Append(string.Format(" AND {0} = '{1}'", w_ColumnName, drIn[i]));
                    //        }
                    //        else
                    //        {
                    //            w_WhereClause.Append(string.Format(" AND {0} = {1}", w_ColumnName, drIn[i]));
                    //        }

                    //}

                    //w_SQLdataBefore.Append(string.Format(" FROM {0} {1}", dt.TableName, w_WhereClause));

                    SqlConnection conn = GetConnection();
                    cmd.Connection = conn;

                    //Ejecutar las acciones en el entorno transaccional de SQL Server
                    SqlTransaction w_Trans = conn.BeginTransaction();
                    cmd.Transaction = w_Trans;

                    try
                    {
                        //Consulta de datos antes de la actualización
                        //cmd.CommandText = w_SQLdataBefore.ToString();
                        //cmd.ExecuteNonQuery();

                        //datset resultado con los campos de la consulta antes del update
                        //SqlDataAdapter da = new SqlDataAdapter(cmd);
                        //da.Fill(dsDataBefore);


                        //Actualización de los datos (ejecución del UPDATE mediante el SP)
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = p_SPname;
                        cmd.ExecuteNonQuery();

                        //Dejar solor los valores modifcados
                        //if (dsDataBefore.Tables[0].Rows.Count == 1)
                        //{
                        //    DataTable dtQuery = p_DSdbTable.Tables[0];
                        //    DataRow drResult = dtMain.NewRow();

                        //    for (int i = 0; i < dtQuery.Columns.Count; i++)
                        //    {
                        //        string w_ViejoVal = dsDataBefore.Tables[0].Rows[0][dtQuery.Columns[i].ColumnName].ToString();
                        //        string w_NuevoVal = dtQuery.Rows[0][dtQuery.Columns[i].ColumnName].ToString();
                        //        if (w_ViejoVal.CompareTo(w_NuevoVal) != 0)
                        //        {
                        //            dtMain.Columns.Add(dtQuery.Columns[i].ColumnName);
                        //            drResult[dtQuery.Columns[i].ColumnName] = w_ViejoVal;
                        //        }
                        //    }
                        //    dtMain.Rows.Add(drResult);
                        //    dsResult.Tables.Add(dtMain);
                        //}

                        try
                        {
                            if (dsDataBefore.Tables[0].Rows.Count > 0)
                                w_RecsAffected = Convert.ToInt32(dsDataBefore.Tables[0].Rows.Count);
                        }
                        catch
                        {
                            w_RecsAffected = -1;
                        }


                        //Todo bien. Devolver en la tabla de resultados la cantidad de registros afectados.
                        dr["OK"] = true;
                        dr["DESC"] = w_RecsAffected;

                        dtRes.Rows.Add(dr);
                        dsResult.Tables.Add(dtRes);

                        w_Trans.Commit();

                    }
                    catch (SqlException sqlEx)
                    {
                        w_Trans.Rollback();
                        //propagar el error
                        throw sqlEx;
                    }
                    finally
                    {
                        if (conn != null)
                            cmd.Connection.Dispose();
                    }
                }
                catch (SqlException sqlEx)
                {
                    //Si hubo error, la tabla de datos en lugar de los datos devuelve los parámetros recibidos y sus valores.
                    //Tembien develve el query utilizado para ver los valores antes de la actualización.
                    DataRow drFail = dtMain.NewRow();
                    dtMain.Columns.Add("p_SPname");
                    dtMain.Columns.Add("p_DSdbTable");
                    dtMain.Columns.Add("SQLqueryDataBeforeUpdate");
                    drFail["p_SPname"] = p_SPname;
                    drFail["p_DSdbTable"] = p_DSdbTable;
                    drFail["SQLqueryDataBeforeUpdate"] = w_SQLdataBefore.ToString();
                    dtMain.Rows.Add(drFail);
                    dsResult.Tables.Add(dtMain);

                    //La tabla de resultado devuelve el error
                    dtRes.Rows.Add(dr);
                    dr["OK"] = false;
                    dr["DESC"] = sqlEx.Message;
                    dsResult.Tables.Add(dtRes);
                }
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("[SP: {0}] {1}", p_SPname, e.Message));
            }

            return dsResult;

          
        }

		#endregion

		#region Ejecuta un SP de UPDATE pasándole un DataSet y un String de Conexión y retorna un DataSet.	

		/// <summary>
		/// Ejecuta un Stored Procedure de UPDATE (_UPD) pasándole un DataSet.
		/// 
		/// El método devuelve los campos a modificarse con los valores de los campos antes de su modificación por el SP de update.
		/// Solamente son devueltos aquellos valores que hayan sido modificados por update.
		/// </summary>
		/// <param name="p_SPname">Nombre del Stored Procedure de actualización</param>
		/// <param name="p_DSdbTable">DataSet con los datos. El datatset corresponde a una tabla en la base de datos.
		/// Los campos primary key del DataSet son tomados como referencia para realizar la consulta de los datos antes 
		/// de que sean actualizados por el update.</param>
		/// <param name="p_StrConn">String de Conexión a la base de datos</param>
		/// <returns>
		/// 
		/// SI LA CONSULTA RESULTÓ EXITOSA:
		/// La tabla "DATA" trae los campos con los valores antes de ser modificados por el update.
		/// Solo se devuelven los campos si su valor ha sido modificado, es decir que solo se retornarán aquellos valores que hayan
		/// sido alterados (Si el valor actual es igual al nuevo luego del update, entonces no se devuelve el valor del campo).
		/// La tabla "RESULT" devuelve un DataRow "OK" con TRUE.
		/// La tabla "RESULT" devuelve un DataRow "DESC" con alguno de los siguientes valores:
		/// DESC = 0 (cero) significa que la consulta no afectó ningún registro. No se devuelve la tabla DATA en el DataSet resultante.
		/// DESC = 1 (uno) significa que el registro correspondiente a la condición definida por las claves primarias del DataSet pasado
		/// como parámetro (p_DSdbTable), fue modificado. Este valor (DESC = 1) es el esperado normalmente, y devolverá la tabla DATA con los valores 
		/// de los campos que hayan sido modificados.
		/// Como tercera opción, si DESC fuera mayor a uno, entonces este valor corresponde a la cantidad de registros resultantes de la consulta creada para determinar 
		/// cuáles campos fueron afectados. Este valor NO CORRESPONDE a la cantidad de registros afectados por el SP de update, por lo que no debe ser tomado
		/// como cantidad de registros afectados por la consulta.
		///	Si esto sucede, entonces es probable que el DataSet pasado como parámetro (p_DSdbTable) no contenga ningún campo definido como primary key.
		///	
		/// SI FALLÓ LA CONSULTA:
		/// La tabla "RESULT" devuelve un DataRow "OK" con FALSE.
		/// La tabla "RESULT" devuelve un DataRow "DESC" con el mensaje de error.
		/// La tabla "DATA" devuelve DataRows nombrados con los parámetros pasados a la función, y su valor.
		/// </returns>
		/// <remarks>El DataSet con los datos a modificar (p_DSdbTable) debe tener al menos un campo definido como primary key.
		/// Si el DataSet no contiene ninguna clave primaria, la tabla DATA del DataSet resultante no será devuelta.
		/// Si la consulta no afectó ningún registros (cero registros afectados), el DataSet resultantre
		/// tampoco devuelve la tabla DATA.</remarks>
		public DataSet ExecuteSPupdate_DS(string p_SPname, DataSet p_DSdbTable, string p_StrConn)
		{
			int w_RecsAffected = 0;

			DataSet dsResult = new DataSet("UPDATE_RESULT"); //DataSet con resultado
			DataSet dsDataBefore = new DataSet(); //Datase con los datos antes del update

			DataTable dtMain = new DataTable("DATA");
			DataTable dtRes = new DataTable("RESULT");
			dtRes.Columns.Add("OK");
			dtRes.Columns.Add("DESC");
			dtRes.Columns.Add("SQL_DS");
			DataRow dr = dtRes.NewRow();
			try
			{
				StringBuilder w_SQLdataBefore = new StringBuilder("SELECT");

				try
				{
					//#line 1900
					StringBuilder w_WhereClause = new StringBuilder(" WHERE (1=1)");

					DataRow drIn = p_DSdbTable.Tables[0].Rows[0];
					DataTable dtIn = p_DSdbTable.Tables[0];

					SqlCommand cmd = new SqlCommand();

					for (int i = 0; i < dtIn.Columns.Count; i++)
					{
						//parámetros para el SP
						string w_ColumnName = dtIn.Columns[i].ColumnName;
						string nombreParam = "@" + w_ColumnName;
						cmd.Parameters.AddWithValue(nombreParam, drIn[i]);

						//campos del select
						if (i > 0) w_SQLdataBefore.Append(",");
						w_SQLdataBefore.Append(" " + w_ColumnName);

						//cláusula where, tomando los campos primary key
						if (dtIn.Columns[i].Unique)
						{
							if (dtIn.Columns[i].DataType.ToString() == "System.String")
							{
								w_WhereClause.Append(string.Format(" AND {0} = '{1}'", w_ColumnName, drIn[i]));
							}
							else
							{
								w_WhereClause.Append(string.Format(" AND {0} = {1}", w_ColumnName, drIn[i]));
							}
						}

					}

					w_SQLdataBefore.Append(string.Format(" FROM {0} {1}", dtIn.TableName, w_WhereClause));

					SqlConnection conn = GetConnection(p_StrConn);
					cmd.Connection = conn;

					//Ejecutar las acciones en el entorno transaccional de SQL Server
					SqlTransaction w_Trans = conn.BeginTransaction();
					cmd.Transaction = w_Trans;

					try
					{
						//Consulta de datos antes de la actualización
						cmd.CommandText = w_SQLdataBefore.ToString();
						cmd.ExecuteNonQuery();

						//datset resultado con los campos de la consulta antes del update
						SqlDataAdapter da = new SqlDataAdapter(cmd);
						da.Fill(dsDataBefore);


						//Actualización de los datos (ejecución del UPDATE mediante el SP)
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.CommandText = p_SPname;
						cmd.ExecuteNonQuery();

						da = new SqlDataAdapter(cmd);
						DataSet dsAuditQry = new DataSet();
						da.Fill(dsAuditQry);

						//Dejar solor los valores modifcados
						if (dsDataBefore.Tables[0].Rows.Count == 1)
						{
							DataTable dtQuery = p_DSdbTable.Tables[0];
							DataRow drResult = dtMain.NewRow();

							for (int i = 0; i < dtQuery.Columns.Count; i++)
							{
								string w_ViejoVal = dsDataBefore.Tables[0].Rows[0][dtQuery.Columns[i].ColumnName].ToString();
								string w_NuevoVal = dtQuery.Rows[0][dtQuery.Columns[i].ColumnName].ToString();
								if (w_ViejoVal.CompareTo(w_NuevoVal) != 0)
								{
									dtMain.Columns.Add(dtQuery.Columns[i].ColumnName);
									drResult[dtQuery.Columns[i].ColumnName] = w_ViejoVal;
								}
							}
							dtMain.Rows.Add(drResult);
							dsResult.Tables.Add(dtMain);
						}

						try
						{
							if (dsDataBefore.Tables[0].Rows.Count > 0)
								w_RecsAffected = Convert.ToInt32(dsDataBefore.Tables[0].Rows.Count);
						}
						catch
						{
							w_RecsAffected = -1;
						}


						//Todo bien. Devolver en la tabla de resultados la cantidad de registros afectados.
						dr["OK"] = true;
						dr["DESC"] = w_RecsAffected;
						dr["SQL_DS"] = dsAuditQry.Tables[0].Rows[0][0];

						dtRes.Rows.Add(dr);
						dsResult.Tables.Add(dtRes);

						w_Trans.Commit();

					}
					catch (SqlException sqlEx)
					{
						w_Trans.Rollback();
						//propagar el error
						throw sqlEx;
					}
					finally
					{
						if (conn != null)
							cmd.Connection.Dispose();
					}
				}
				catch (SqlException sqlEx)
				{
					//Si hubo error, la tabla de datos en lugar de los datos devuelve los parámetros recibidos y sus valores.
					//Tembien develve el query utilizado para ver los valores antes de la actualización.
					DataRow drFail = dtMain.NewRow();
					dtMain.Columns.Add("p_SPname");
					dtMain.Columns.Add("p_DSdbTable");
					dtMain.Columns.Add("p_StrConn");
					dtMain.Columns.Add("SQLqueryDataBeforeUpdate");
					drFail["p_SPname"] = p_SPname;
					drFail["p_DSdbTable"] = p_DSdbTable;
					drFail["p_StrConn"] = p_StrConn;
					drFail["SQLqueryDataBeforeUpdate"] = w_SQLdataBefore.ToString();
					dtMain.Rows.Add(drFail);
					dsResult.Tables.Add(dtMain);

					//La tabla de resultado devuelve el error
					dtRes.Rows.Add(dr);
					dr["OK"] = false;
					dr["DESC"] = sqlEx.Message;
					dsResult.Tables.Add(dtRes);
				}
			}
			catch (Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", p_SPname, e.Message));
			}

			return dsResult;
		}

		#endregion

		#region Ejecuta un SP de UPDATE pasándole un DataSet.	

		/// <summary>
		/// Ejecuta un SP de UPDATE (_upd) pasándole un DataSet.
		/// </summary>
		/// <param name="pSPname">Nombre del Stored Procedure.</param>
		/// <param name="pDStableBD">DataSet con los datos actualizar. Los nombres de los campos corresponden a los parámetros del SP.</param>
		/// <returns>Cantidad de registros afectados.</returns>
		public int ExecuteSPupdate(string pSPname, DataSet pDStableBD)
		{
			DataSet ds = new DataSet();
			try
			{
				//#line 1900
				DataRow dr = pDStableBD.Tables[0].Rows[0];
				DataTable dt = pDStableBD.Tables[0];

				SqlCommand cmd = new SqlCommand();

				for (int i = 0; i < dt.Columns.Count; i++)
				{
					string nombreParam = "@" + dt.Columns[i].ColumnName;
					cmd.Parameters.AddWithValue(nombreParam, dr[i]);
				}

				SqlConnection conn = GetConnection();
				cmd.Connection = conn;
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = pSPname;
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(ds);

				if (conn != null)
					cmd.Connection.Dispose();
			}
			catch (Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", pSPname, e.Message));
			}
			if (ds.Tables[0].Rows.Count > 0)
				return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
			else
				return 0;
		}

		#endregion

		#region Ejecuta un SP de UPDATE pasándole un DataSet y el string de conexión.

		/// <summary>
		/// Ejecuta un SP de UPDATE (_upd) pasándole un DataSet y el string de conexión.
		/// </summary>
		/// <param name="pSPname">Nombre del Stored Procedure.</param>
		/// <param name="pDStableBD">DataSet con los datos actualizar. Los nombres de los campos corresponden a los parámetros del SP.</param>
		/// <returns>Cantidad de registros afectados.</returns>		/// <param name="strConn">String de conexión.</param>
		public int ExecuteSPupdate(string pSPname, DataSet pDStableBD, string strConn)
		{
			DataSet ds = new DataSet();
			try
			{
				//#line 2000
				DataRow dr = pDStableBD.Tables[0].Rows[0];
				DataTable dt = pDStableBD.Tables[0];

				SqlCommand cmd = new SqlCommand();

				for (int i = 0; i < dt.Columns.Count; i++)
				{
					string nombreParam = "@" + dt.Columns[i].ColumnName;
					cmd.Parameters.AddWithValue(nombreParam, dr[i]);
				}

				SqlConnection conn = GetConnection(strConn);
				cmd.Connection = conn;
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = pSPname;
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(ds);

				if (conn != null)
					cmd.Connection.Dispose();
			}
			catch (Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", pSPname, e.Message));
			}
			if (ds.Tables[0].Rows.Count > 0)
				return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
			else
				return 0;
		}

		#endregion

		#region *Ejecuta un SP de UPDATE pasándole un string XML.	

		/// <summary>
		/// Ejecuta un SP de UPDATE (_upd) pasándole un string XML.
		/// </summary>
		/// <param name="pSPname">Nombre del Stored Procedure.</param>
		/// <param name="pStrXMLParam">Un string XML conteniendo los parámetros a pasarle al Stored Procedure y sus valores. El nombre del nodo debe llamase como el parámetro, 
		/// <returns>Cantidad de registros afectados.</returns>	
		public int ExecuteSPupdate(string pSPname, string pStrXMLparam)
		{
			int wUltimoID = 0;

			try
			{
				//#line 1200
				XmlDocument xDoc = new XmlDocument();
				xDoc.LoadXml(pStrXMLparam);

				XmlNode root = xDoc.FirstChild;
				if (root.HasChildNodes)
				{
					int cantNodos = root.ChildNodes.Count;
					int cantItems = root.ChildNodes[0].ChildNodes.Count;

					SqlCommand cmd = new SqlCommand();
					SqlParameter[] parametros = new SqlParameter[cantItems];

					for (int i = 0; i < cantNodos; i++)
					{
						string nombreParam = null;
						string valorParam = null;
						nombreParam = "@" + root.ChildNodes[i].Name;
						valorParam = root.ChildNodes[i].InnerText;

						if (valorParam.Length > 0)
						{
							string wTipoDatoStruct = root.ChildNodes[i].Attributes["dataType"].Value.ToUpper();
							switch (wTipoDatoStruct)
							{
								case "NUMERIC":
									valorParam = MycConvert.AdaptToDecimalDB(valorParam);
									break;
								case "DATETIME":
									valorParam = MycConvert.AdaptToDateDB(valorParam);
									break;
								case "DATE":
									valorParam = MycConvert.AdaptToDateDB(valorParam);
									break;
								case "STRING":
									valorParam = MycConvert.AdaptToStringDB(valorParam);
									break;
							}

							cmd.Parameters.AddWithValue(nombreParam, valorParam);
						}
					}


					SqlConnection conn = GetConnection();
					cmd.Connection = conn;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					SqlDataAdapter da = new SqlDataAdapter(cmd);
					DataSet ds = new DataSet();
					da.Fill(ds);
					if (ds.Tables[0].Rows.Count > 0)
						wUltimoID = Convert.ToInt32(ds.Tables[0].Rows[0][0]);

					if (conn != null)
						cmd.Connection.Dispose();

				}
			}
			catch (Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", pSPname, e.Message));
			}
			return wUltimoID;
		}

		#endregion

		#region Ejecuta un SP de UPDATE pasándole un string XML y el string de conexión.	

		/// <summary>
		/// Ejecuta un SP de UPDATE (_upd) pasándole un string XML y el string de conexión
		/// </summary>
		/// <param name="pSPname">Nombre del Stored Procedure.</param>
		/// <param name="pStrXMLParam">Un string XML conteniendo los parámetros a pasarle al Stored Procedure y sus valores. El nombre del nodo debe llamase como el parámetro, 
		/// <param name="pStrConn">String de conexión</param>
		/// <returns>Cantidad de registros afectados.</returns>	
		public int ExecuteSPupdate(string pSPname, string pStrXMLparam, string pStrConn)
		{
			int wUltimoID = 0;

			try
			{
				////#line 1300
				XmlDocument xDoc = new XmlDocument();
				xDoc.LoadXml(pStrXMLparam);

				XmlNode root = xDoc.FirstChild;
				if (root.HasChildNodes)
				{
					int cantNodos = root.ChildNodes.Count;
					int cantItems = root.ChildNodes[0].ChildNodes.Count;

					SqlCommand cmd = new SqlCommand();
					SqlParameter[] parametros = new SqlParameter[cantItems];

					for (int i = 0; i < cantNodos; i++)
					{
						string nombreParam = null;
						string valorParam = null;
						nombreParam = "@" + root.ChildNodes[i].Name;
						valorParam = root.ChildNodes[i].InnerText;
						if (valorParam == "")
							valorParam = null;

						cmd.Parameters.AddWithValue(nombreParam, valorParam);
					}

					SqlConnection conn = GetConnection(pStrConn);
					cmd.Connection = conn;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					SqlDataAdapter da = new SqlDataAdapter(cmd);
					DataSet ds = new DataSet();
					da.Fill(ds);
					if (ds.Tables[0].Rows.Count > 0)
						wUltimoID = Convert.ToInt32(ds.Tables[0].Rows[0][0]);

					if (conn != null)
						cmd.Connection.Dispose();

				}
			}
			catch (Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", pSPname, e.Message));
			}
			return wUltimoID;
		}

		#endregion

		#endregion

		#region Ejecuta varios SP de forma transaccional.	

		/// <summary>
		/// Agrega una tarea transaccional a la DataTable a pasar a ExecTRANS_SP.
		/// </summary>
		/// <param name="pTableTransaccTasks">Tabla con las transacciones a la que se le agregará la nueva tarea.</param>
		/// <param name="pXMLdata">set de datos XML de la operación.</param>
		/// <param name="pSP">Nombre del Stored Procedure que ejecutará la operación.</param>
		/// <returns></returns>
		public static DataTable adTransacctionTask(DataTable pTableTransaccTasks, string pXMLdata, string pSP)
		{
			if (pTableTransaccTasks.Columns.Count == 0)
			{
				pTableTransaccTasks.Columns.Add(new DataColumn());
				pTableTransaccTasks.Columns.Add(new DataColumn());
			}

			DataRow dr = pTableTransaccTasks.NewRow();
			dr[0] = pSP; //nombre del SP
			dr[1] = pXMLdata; //String XML a procesar (datos a guardar)
			pTableTransaccTasks.Rows.Add(dr); //agregar la fila

			return pTableTransaccTasks;

		}

		/// <summary>
		/// Ejecuta varias acciones contra la base de datos, en un entorno transaccional.
		/// Si la transacción falla, arroja una excepción.
		/// </summary>
		/// <param name="pSP_XML">DataTable, en cuya primera columna va el nombre del SP a ejecutar
		/// y en la segunda un string XML con los parámetros.</param>
		public void ExecTRANS_SP(DataTable pSP_XML)
		{
			string pSPname = null;
			XmlDocument xDoc = new XmlDocument();

			SqlCommand cmd = new SqlCommand();
			SqlConnection conn = GetConnection();
			cmd.Connection = conn;
			cmd.CommandType = CommandType.StoredProcedure;
			SqlTransaction wTrans = conn.BeginTransaction();
			cmd.Transaction = wTrans;
			string nombreParam = null;
			string valorParam = null;
			string wTipoDatoStruct = null;

			try
			{
				#region ejecución de consultas (sp) sobre la BD

				foreach (DataRow dr in pSP_XML.Rows)
				{
					pSPname = dr[0].ToString();
					xDoc.LoadXml(dr[1].ToString());

					XmlNode root = xDoc.FirstChild;
					if (root.HasChildNodes)
					{
						int cantNodos = root.ChildNodes.Count;
						//	int cantItems = root.ChildNodes[0].ChildNodes.Count;

						//	SqlParameter[] parametros = new SqlParameter[cantItems];

						#region carga de parámetros		

						for (int i = 0; i < cantNodos; i++)
						{
							nombreParam = "@" + root.ChildNodes[i].Name;
							valorParam = root.ChildNodes[i].InnerText;

							if (valorParam.Length > 0)
							{
								wTipoDatoStruct = root.ChildNodes[i].Attributes["dataType"].Value.ToUpper();
								switch (wTipoDatoStruct)
								{
									case "NUMERIC":
										valorParam = MycConvert.AdaptToDecimalDB(valorParam);
										break;
									case "DATETIME":
										valorParam = MycConvert.AdaptToDateDB(valorParam);
										break;
									case "DATE":
										valorParam = MycConvert.AdaptToDateDB(valorParam);
										break;
									case "STRING":
										valorParam = MycConvert.AdaptToStringDB(valorParam);
										break;
								}

								cmd.Parameters.AddWithValue(nombreParam, valorParam);
							}

						}

						#endregion

						cmd.CommandText = pSPname;
						cmd.ExecuteNonQuery();
						cmd.Parameters.Clear();

					}
				}

				#endregion

				wTrans.Commit();
			}
			catch (SqlException sqlEx)
			{
				wTrans.Rollback();

				string wMensaje = string.Empty;
				MycEventLog.LogEventType wTipoError;

				switch (sqlEx.Number)
				{
					case 2627:
						wMensaje = "El registro ya existe en la base de datos";
						wTipoError = MycEventLog.LogEventType.Warning;
						break;

					case 2601:
						wMensaje = "Registro duplicado.";
						wTipoError = MycEventLog.LogEventType.Warning;
						break;

					case 8114:
						wMensaje = "Error en conversión del tipo de dato a pasar en el parámetro";
						wTipoError = MycEventLog.LogEventType.Error;
						break;

					default:
						wMensaje = string.Format("Error inesperado. SqlException{0}", sqlEx.Message);
						wTipoError = MycEventLog.LogEventType.Error;
						break;
				}

				MycEventLog.LogEvent("ExecTRANS_SP", string.Format("{0} - SP:{1}", wMensaje, sqlEx.Procedure), wTipoError);

				throw new Exception(string.Format("Error en operación transaccional ({0}). Info adicional: SP:{1}; Param:{2}; valor:{3}; Tipo según estructura:{4} [{5}]",
				                                  wMensaje, pSPname, nombreParam, valorParam, wTipoDatoStruct, sqlEx.Message));

			}

			finally
			{
				if (conn != null)
					cmd.Connection.Dispose();
			}
		}

		#endregion

		#region ====== GetRecorID 

		/// <summary>
		/// Consulta a la tabla TBL_NUMEROS y retorna un entero, indicando el RecordId para la insercion. 
		/// </summary>
		/// <param name="p_SPname">Nombre del Stored Procedure a ejecurar</param>
		/// <param name="p_NombreTabla">Tabla de la cual se quiere obtener el RecordID a insertar</param>
		/// <param name="p_NombreCampo">Nombre del parametro que utilisa el Soted Procedure para identificar a la tabla por su nombre para obtener el RecordID</param>
		/// <returns>Retorna un entrero 
		/// Si es mayor o igual a 0, es el RecordID
		/// Si es -1 indica que hubo un error</returns>
		public int ExecuteSpGetRecorID(string p_SPname, string p_NombreTabla, string p_NombreCampo)
		{
			int wResult = 0;

			try
			{
				DataSet dsResult = new DataSet();
				SqlCommand cmd = new SqlCommand();
				SqlConnection conn = GetConnection();
				cmd.Connection = conn;
				cmd.CommandType = CommandType.StoredProcedure;
				string nombreParam = "@" + p_NombreCampo;
				cmd.Parameters.AddWithValue(nombreParam, p_NombreTabla);

				cmd.CommandText = p_SPname;

				SqlDataAdapter da = new SqlDataAdapter(cmd);

				da.Fill(dsResult);
				try
				{
					if (Convert.ToInt32(dsResult.Tables[0].Rows[0][0]) > 0)
					{
						wResult = Convert.ToInt32(dsResult.Tables[0].Rows[0][0]);
					}
				}
				catch
				{
					wResult = -1;
				}

				if (conn != null)
					conn.Dispose();
			}
			catch
			{
				wResult = -1;
			}
			return wResult;
		}

		/// <summary>
		/// Consulta a la tabla TBL_NUMEROS y retorna un entero, indicando el RecordId para la insercion. 
		/// </summary>
		/// <param name="p_SPname">Nombre del Stored Procedure a ejecurar</param>
		/// <param name="p_NombreTabla">Tabla de la cual se quiere obtener el RecordID a insertar</param>
		/// <param name="p_NombreCampo">Nombre del parametro que utilisa el Soted Procedure para identificar a la tabla por su nombre para obtener el RecordID</param>
		/// <param name="p_strConn">String de conexión a la base de datos</param>
		/// <returns>Retorna un entrero 
		/// Si es mayor o igual a 0, es el RecordID
		/// Si es -1 indica que hubo un error</returns>
		public int ExecuteSpGetNumber(string p_SPname, string p_NombreTabla, string p_NombreCampo, string p_strConn)
		{
			int wResult = 0;
			DataSet dsResult = new DataSet("GET_NUMBER_RESULT");
			try
			{
				SqlCommand cmd = new SqlCommand();
				SqlConnection conn = GetConnection(p_strConn);
				cmd.Connection = conn;
				cmd.CommandType = CommandType.StoredProcedure;
				string nombreParam = "@" + p_NombreCampo;
				cmd.Parameters.AddWithValue(nombreParam, p_NombreTabla);

				cmd.CommandText = p_SPname;

				SqlDataAdapter da = new SqlDataAdapter(cmd);

				da.Fill(dsResult);
				try
				{
					if (Convert.ToInt32(dsResult.Tables[0].Rows[0][0]) > 0)
					{
						wResult = Convert.ToInt32(dsResult.Tables[0].Rows[0][0]);
					}
				}
				catch
				{
					wResult = -1;
				}

				if (conn != null)
					conn.Dispose();
			}
			catch
			{
				wResult = -1;
			}
			return wResult;
		}

		#endregion
	}

}