using System;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using Myc.data.SQLServer;
using System.Xml;
using Myc.data.Conversiones;

namespace Myc.data.OLEDB
{
	/// <summary>
	/// Acciones de DAC (Data Access Component) para bases de datos que soporten stored procedures y conexión OLEDB.
	/// </summary>
	public class MycData : MycConvert
	{
		/// <summary>
		/// Método Constructor
		/// </summary>
		public MycData()
		{
//			string registroFWK = Myc.General.MycRegistry.readReg("SOFTWARE\\Myc\\FWK");
//			if(registroFWK != "OCT-F5E35E7B-2EDA-43cb-8A1E-0CB75BF8F147")
//				throw new Exception("Cannot access Myc Assembly - [Invalid Use]");
		}

		#region ====== conexión
		/// <summary>
		/// Crea un objeto Conexión, pasándole un string de conexión determinado.
		/// </summary>
		/// <param name="pStrConn">String de conexión</param>
		/// <returns>El objeto conexión se devuelve ya abierto.</returns>
		public OleDbConnection GetConnection(string pStrConn)
		{
			//usa un string de conexión específico
			//#line 100
			OleDbConnection connection = null;
			try
			{
				connection = new  OleDbConnection(pStrConn);
				connection.Open();
			}
			catch(Exception e)
			{
				throw new Exception(string.Format("No se pudo crear la conexión con el string del parámetro] {0}",e.Message));
			}
			return connection;
		}


		/// <summary>
		/// Crea un objeto Conexión, tomando el string de conexión determinado del archivo de configuración.
		/// </summary>
		/// <remarks>
		/// El string en el archivo de configuración debe aparecer bajo una clave con nombre "ConnString".
		/// </remarks>
		/// <returns>El objeto conexión se devuelve ya abierto.</returns>
		public OleDbConnection GetConnection()
		{	
			//usa el string de conexión tomado de la configuración
			OleDbConnection connection = null;
			//#line 200
			try
			{
                //string wPassw = "%#0" + "4?0" + "1*19" + "63!"; //crear una clave de encriptación por default.

                //string strConn = MycConvert.Decrypt(ConfigurationManager.AppSettings["ConnString"], wPassw);

				string strConn = ConfigurationManager.AppSettings["ConnString"];
				connection = new OleDbConnection(strConn);
				connection.Open();
			}
			catch(Exception e)
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
		public DataSet ExecuteSP(string pSPname, XmlDocument pXMLParam)
		{
			DataSet dsResult = new DataSet();	//DataSet con resultado
//#line 300
			try
			{
				XmlNode root = pXMLParam.FirstChild;
				if (root.HasChildNodes)
				{
					int cantNodos = root.ChildNodes.Count;
					int cantItems = root.ChildNodes[0].ChildNodes.Count;

					OleDbCommand cmd = new OleDbCommand();

					OleDbParameter[] parametros = new OleDbParameter[cantItems];
				
					for (int i=0; i<cantNodos; i++)
					{
						string nombreParam = null;
						string valorParam = null;
						nombreParam = "@" + root.ChildNodes[i].Name;
						valorParam = root.ChildNodes[i].InnerText;
						if (valorParam =="")
							valorParam = null;

						cmd.Parameters.AddWithValue(nombreParam, valorParam);
					}
					OleDbConnection conn = GetConnection();
					cmd.Connection = conn;	
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					cmd.ExecuteNonQuery();
					OleDbDataAdapter da = new OleDbDataAdapter(cmd);
					da.Fill(dsResult);

					if(conn != null)
						cmd.Connection.Dispose();
				}
			}
			catch(Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", pSPname, e.Message));
			}
			return dsResult;
		}
		#endregion

		#region Ejecuta un SP de propósito general pasándole los parámetros en un string XML. Devuelve un DATASET.
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
		/// <returns>DataSet</returns>
		public DataSet ExecuteSP(string pSPname, string pStrXMLParam)
		{
			DataSet dsResult = new DataSet();	//DataSet con resultado

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

					OleDbCommand cmd = new OleDbCommand();

					OleDbParameter[] parametros = new OleDbParameter[cantItems];
				
					for (int i=0; i<cantNodos; i++)
					{
						string nombreParam = null;
						string valorParam = null;
						nombreParam = "@" + root.ChildNodes[i].Name;
						valorParam = root.ChildNodes[i].InnerText;
						if (valorParam =="")
							valorParam = null;

						cmd.Parameters.AddWithValue(nombreParam, valorParam);
					}
					OleDbConnection conn = GetConnection();
					cmd.Connection = conn;	
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					cmd.ExecuteNonQuery();
					OleDbDataAdapter da = new OleDbDataAdapter(cmd);
					da.Fill(dsResult);

					if(conn != null)
						conn.Dispose();
				}
			}
			catch(Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", pSPname, e.Message));
			}
			return dsResult;
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
			DataSet dsResult = new DataSet();	//DataSet con resultado
//#line 500
			try
			{
				XmlNode root = pXMLParam.FirstChild;
				if (root.HasChildNodes)
				{
					int cantNodos = root.ChildNodes.Count;
					int cantItems = root.ChildNodes[0].ChildNodes.Count;

					OleDbCommand cmd = new OleDbCommand();

					OleDbParameter[] parametros = new OleDbParameter[cantItems];
			
					for (int i=0; i<cantNodos; i++)
					{
						string nombreParam = null;
						string valorParam = null;
						nombreParam = "@" + root.ChildNodes[i].Name;
						valorParam = root.ChildNodes[i].InnerText;
						if (valorParam == "")
							valorParam = null;

						cmd.Parameters.AddWithValue(nombreParam, valorParam);
					}
					OleDbConnection conn = GetConnection(pStrConn);
					cmd.Connection = conn;	
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					cmd.ExecuteNonQuery();
					OleDbDataAdapter da = new OleDbDataAdapter(cmd);
					da.Fill(dsResult);
				
					if(conn != null)
						cmd.Connection.Dispose();	
				}
			}
			catch(Exception e)
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
			//Ejecuta un Stored Procedure de propósito general devolviendo un DataSet con los datos
			DataSet dsResult = new DataSet();	//DataSet con resultado
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

					OleDbCommand cmd = new OleDbCommand();

					OleDbParameter[] parametros = new OleDbParameter[cantItems];
			
					for (int i=0; i<cantNodos; i++)
					{
						string nombreParam = null;
						string valorParam = null;
						nombreParam = "@" + root.ChildNodes[i].Name;
						valorParam = root.ChildNodes[i].InnerText;
						if (valorParam == "")
							valorParam = null;

						cmd.Parameters.AddWithValue(nombreParam, valorParam);
					}
					OleDbConnection conn = GetConnection(pStrConn);
					cmd.Connection = conn;	
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					cmd.ExecuteNonQuery();
					OleDbDataAdapter da = new OleDbDataAdapter(cmd);
					da.Fill(dsResult);
				
					if(conn != null)
						cmd.Connection.Dispose();			
				}
			}
			catch(Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", pSPname, e.Message));
			}
			return dsResult;
		}

		#endregion

		#region Ejecuta una consulta SQL literal
		public DataSet ExecuteSQLquery(string pSQL)
		{
			DataSet dsResult = new DataSet();	//DataSet con resultado
			try
			{

				OleDbCommand cmd = new OleDbCommand(pSQL);

				OleDbConnection conn = GetConnection();
				cmd.Connection = conn;	
				//cmd.ExecuteNonQuery();
				OleDbDataAdapter da = new OleDbDataAdapter(cmd);
				da.Fill(dsResult);
			
				if(conn != null)
					cmd.Connection.Dispose();		

			}
			catch(Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", pSQL, e.Message));
			}
			return dsResult;
		}

		#endregion

		#region Ejecuta una consulta SQL literal, pasándole el string de conexión
		public DataSet ExecuteSQLquery(string pSQL, string pStrConn)
		{
			DataSet dsResult = new DataSet();	//DataSet con resultado
			try
			{

				OleDbCommand cmd = new OleDbCommand(pSQL);

				OleDbConnection conn = GetConnection(pStrConn);
				cmd.Connection = conn;	
				cmd.ExecuteNonQuery();
				OleDbDataAdapter da = new OleDbDataAdapter(cmd);
				da.Fill(dsResult);
			
				if(conn != null)
					cmd.Connection.Dispose();		

			}
			catch(Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", pSQL, e.Message));
			}
			return dsResult;
		}

		#endregion

		#region Ejecuta una consulta SQL literal de actualización (Update)
		public int ExecuteSQLupdate(string pSQL)
		{

			int wResult = 0;
			try
			{

				OleDbCommand cmd = new OleDbCommand(pSQL);

				OleDbConnection conn = GetConnection();
				cmd.Connection = conn;	
				wResult = cmd.ExecuteNonQuery(); 
			
				if(conn != null)
					cmd.Connection.Dispose();		

			}
			catch(Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", pSQL, e.Message));
			}
			return wResult;
		}

		#endregion

		#region Ejecuta una consulta SQL literal de actualización (Update), pasándole el string de conexión
		public int ExecuteSQLupdate(string pSQL, string pStrConn)
		{
			int wResult = 0;
			try
			{

				OleDbCommand cmd = new OleDbCommand(pSQL);

				OleDbConnection conn = GetConnection(pStrConn);
				cmd.Connection = conn;	
				wResult = cmd.ExecuteNonQuery(); 
			
				if(conn != null)
					cmd.Connection.Dispose();		

			}
			catch(Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", pSQL, e.Message));
			}
			return wResult;
		}

		#endregion


		#region Ejecuta una consulta SQL literal de inserción (Insert)
		public int ExecuteSQLinsert(string pSQL)
		{

			int wResult = 0;
			try
			{

				OleDbCommand cmd = new OleDbCommand(pSQL);

				OleDbConnection conn = GetConnection();
				cmd.Connection = conn;	
				wResult = cmd.ExecuteNonQuery(); 
			
				if(conn != null)
					cmd.Connection.Dispose();		

			}
			catch(Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", pSQL, e.Message));
			}
			return wResult;
		}

		#endregion

		#region Ejecuta una consulta SQL literal de inserción (Insert), pasándole el string de conexión
		public int ExecuteSQLinsert(string pSQL, string pStrConn)
		{

			int wResult = 0;
			try
			{
				OleDbCommand cmd = new OleDbCommand(pSQL);
				
				OleDbConnection conn = GetConnection(pStrConn);
				cmd.Connection = conn;	
				wResult = cmd.ExecuteNonQuery();
			
				if(conn != null)
					cmd.Connection.Dispose();


			}
			catch(Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", pSQL, e.Message));
			}
			return wResult;
		}

		#endregion

		#region Ejecuta una consulta SQL literal de inserción (Insert), pasándole el string de conexión y el nombre de la tabla para devolver el ID
		public int ExecuteSQLinsert(string pSQL, string p_TableNameToGetID, string pStrConn)
		{

			int w_Result = 0;
			try
			{
				OleDbCommand cmd = new OleDbCommand(pSQL);
				
				OleDbConnection conn = GetConnection(pStrConn);
				cmd.Connection = conn;	
				cmd.ExecuteNonQuery();

				string w_IdName = p_TableNameToGetID.ToUpper().Replace("TBL_", "ID_");
				string w_SQL = string.Format("SELECT MAX({0}) AS LASTID FROM {1}", w_IdName, p_TableNameToGetID);

				cmd.CommandText = w_SQL;
				cmd.ExecuteNonQuery();
				DataSet ds = new DataSet();
				OleDbDataAdapter da = new OleDbDataAdapter(cmd);
				da.Fill(ds);

				if(ds.Tables[0].Rows.Count > 0)
					w_Result = Convert.ToInt32(ds.Tables[0].Rows[0]["LASTID"]);
				else
					w_Result = -1;
			
				if(conn != null)
					cmd.Connection.Dispose();


			}
			catch(Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", pSQL, e.Message));
			}
			return w_Result;
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
				OleDbCommand cmd = new OleDbCommand(w_SQL);

				OleDbConnection conn = GetConnection();
				cmd.Connection = conn;	
				cmd.ExecuteNonQuery();
				OleDbDataAdapter da = new OleDbDataAdapter(cmd);
				da.Fill(ds);
			
				if(conn != null)
					cmd.Connection.Dispose();	
	
				if(ds.Tables[0].Rows.Count == 1)
					w_Result = ds.Tables[0].Rows[0][0].ToString();

			}
			catch(Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", w_SQL, e.Message));
			}

			return w_Result;
		}

		#endregion


		//Es igual a getMaxValue. Se deja por compatibilidad. 
		//Utilizar getMaxValue en lugar de éste método.
		public int GetMayorID(string p_NombreTablaDB, string pStrConn)
		{
			string w_tablaID = p_NombreTablaDB.Replace("TBL_", "ID_");
			string w_SQL = string.Format("SELECT MAX({0}) FROM {1}", w_tablaID, p_NombreTablaDB);
			int w_Result = -1;

			DataSet dsResult = new DataSet();	//DataSet con resultado
			try
			{

				OleDbCommand cmd = new OleDbCommand(w_SQL);

				OleDbConnection conn = GetConnection(pStrConn);
				cmd.Connection = conn;	
				cmd.ExecuteNonQuery();
				OleDbDataAdapter da = new OleDbDataAdapter(cmd);
				da.Fill(dsResult);
			
				if(conn != null)
					cmd.Connection.Dispose();		

			}
			catch(Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", w_SQL, e.Message));
			}
			
			w_Result = Convert.ToInt32(dsResult.Tables[0].Rows[0][0]);
			return w_Result;

		}


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
			DataSet dsResult = new DataSet();	//DataSet con resultado
			try
			{
//#line 700
				XmlNode root = pXMLParam.FirstChild;
				if (root.HasChildNodes)
				{
					int cantNodos = root.ChildNodes.Count;
					int cantItems = root.ChildNodes[0].ChildNodes.Count;

					OleDbCommand cmd = new OleDbCommand();

					OleDbParameter[] parametros = new OleDbParameter[cantItems];

					for (int i=0; i<cantNodos; i++)
					{
						string nombreParam = null;
						string valorParam = null;
						nombreParam = "@" + root.ChildNodes[i].Name;
						valorParam = root.ChildNodes[i].InnerText;
						
						
						if (valorParam !="")
						{
							string wTipoDatoStruct =  root.ChildNodes[i].Attributes["dataType"].Value.ToUpper();
							switch(wTipoDatoStruct)
							{
								case "NUMERIC":
									valorParam =  MycConvert.AdaptToDecimalDB(valorParam);
									break;
								case "DATETIME":
									valorParam =  MycConvert.AdaptToDateDB(valorParam);
									break;
								case "STRING":
									valorParam =  MycConvert.AdaptToStringDB(valorParam);
									break;			
							}								

							cmd.Parameters.AddWithValue(nombreParam, valorParam);
						}
					}

					OleDbConnection conn = GetConnection();
					cmd.Connection = conn;	
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					cmd.ExecuteNonQuery();
					OleDbDataAdapter da = new OleDbDataAdapter(cmd);
					da.Fill(dsResult);
				
					if(conn != null)
						cmd.Connection.Dispose();		
				}
			}
			catch(Exception e)
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
			DataSet dsResult = new DataSet();	//DataSet con resultado
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

					OleDbCommand cmd = new OleDbCommand();

					OleDbParameter[] parametros = new OleDbParameter[cantItems];

					for (int i=0; i<cantNodos; i++)
					{
						string nombreParam = null;
						string valorParam = null;
						nombreParam = "@" + root.ChildNodes[i].Name;
						valorParam = root.ChildNodes[i].InnerText;
						
						if (valorParam !="")
						{
							string wTipoDatoStruct =  root.ChildNodes[i].Attributes["dataType"].Value.ToUpper();
							switch(wTipoDatoStruct)
							{
								case "NUMERIC":
									valorParam =  MycConvert.AdaptToDecimalDB(valorParam);
									break;
								case "DATETIME":
									valorParam =  MycConvert.AdaptToDateDB(valorParam);
									break;
								case "STRING":
									valorParam =  MycConvert.AdaptToStringDB(valorParam);
									break;			
							}								

							cmd.Parameters.AddWithValue(nombreParam, valorParam);
						}
					}
					OleDbConnection conn = GetConnection();
					cmd.Connection = conn;	
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					cmd.ExecuteNonQuery();
					OleDbDataAdapter da = new OleDbDataAdapter(cmd);
					da.Fill(dsResult);
				
					if(conn != null)
						cmd.Connection.Dispose();		
				}
			}
			catch(Exception e)
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
			DataSet dsResult = new DataSet();	//DataSet con resultado

			try
			{
//#line 900
				XmlNode root = pXMLParam.FirstChild;
				if (root.HasChildNodes)
				{
					int cantNodos = root.ChildNodes.Count;
					int cantItems = root.ChildNodes[0].ChildNodes.Count;

					OleDbCommand cmd = new OleDbCommand();

					OleDbParameter[] parametros = new OleDbParameter[cantItems];

					for (int i=0; i<cantNodos; i++)
					{
						string nombreParam = null;
						string valorParam = null;
						nombreParam = "@" + root.ChildNodes[i].Name;
						valorParam = root.ChildNodes[i].InnerText;
						
						if (valorParam !="")
						{
							string wTipoDatoStruct =  root.ChildNodes[i].Attributes["dataType"].Value.ToUpper();
							switch(wTipoDatoStruct)
							{
								case "NUMERIC":
									valorParam =  MycConvert.AdaptToDecimalDB(valorParam);
									break;
								case "DATETIME":
									valorParam =  MycConvert.AdaptToDateDB(valorParam);
									break;
								case "STRING":
									valorParam =  MycConvert.AdaptToStringDB(valorParam);
									break;			
							}								

							cmd.Parameters.AddWithValue(nombreParam, valorParam);
						}
					}

					OleDbConnection conn = GetConnection(pStrConn);
					cmd.Connection = conn;	
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					cmd.ExecuteNonQuery();
					OleDbDataAdapter da = new OleDbDataAdapter(cmd);
					da.Fill(dsResult);
				
					if(conn != null)
						cmd.Connection.Dispose();						
				}
			}
			catch(Exception e)
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
			DataSet dsResult = new DataSet();	//DataSet con resultado
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

					OleDbCommand cmd = new OleDbCommand();

					OleDbParameter[] parametros = new OleDbParameter[cantItems];
			
					for (int i=0; i<cantNodos; i++)
					{
						string nombreParam = null;
						string valorParam = null;
						nombreParam = "@" + root.ChildNodes[i].Name;
						valorParam = root.ChildNodes[i].InnerText;
						
						if (valorParam !="")
						{
							string wTipoDatoStruct =  root.ChildNodes[i].Attributes["dataType"].Value.ToUpper();
							switch(wTipoDatoStruct)
							{
								case "NUMERIC":
									valorParam =  MycConvert.AdaptToDecimalDB(valorParam);
									break;
								case "DATETIME":
									valorParam =  MycConvert.AdaptToDateDB(valorParam);
									break;
								case "STRING":
									valorParam =  MycConvert.AdaptToStringDB(valorParam);
									break;			
							}								

							cmd.Parameters.AddWithValue(nombreParam, valorParam);
						}
					}
					OleDbConnection conn = GetConnection(pStrConn);
					cmd.Connection = conn;	
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					cmd.ExecuteNonQuery();
					OleDbDataAdapter da = new OleDbDataAdapter(cmd);
					da.Fill(dsResult);

					if(conn != null)
						cmd.Connection.Dispose();                 
				}
			}
			catch(Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", pSPname, e.Message));
			}
			return dsResult;
		}

		#endregion

		#endregion
	
		#region ====== Insert
		#region Ejecuta un SP de INSERT pasándole un DataSet.
		/// <summary>
		/// Ejecuta un SP de INSERT (_ins) pasándole un DATASET y el string de conexión.
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

				OleDbCommand cmd = new OleDbCommand();

				for (int i=0; i<dt.Columns.Count; i++)
				{
					string nombreParam = "@" + dt.Columns[i].ColumnName;
					cmd.Parameters.AddWithValue(nombreParam, dr[i]);
				}

				OleDbConnection conn = GetConnection();
				cmd.Connection = conn;	
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = pSPname;
				OleDbDataAdapter da = new OleDbDataAdapter(cmd);
				DataSet ds = new DataSet();
				da.Fill(ds);
				if (ds.Tables[0].Rows.Count > 0)
					wUltimoID = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
				
				if(conn != null)
					conn.Dispose();
			}
			catch(Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", pSPname, e.Message));
			}
			return wUltimoID;
		}

		#endregion

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

					OleDbCommand cmd = new OleDbCommand();
					OleDbParameter[] parametros = new OleDbParameter[cantItems];
			

					for (int i=0; i<cantNodos; i++)
					{
						string nombreParam = null;
						string valorParam = null;
						nombreParam = "@" + root.ChildNodes[i].Name;
						valorParam = root.ChildNodes[i].InnerText;
						
						if (valorParam !="")
						{
							string wTipoDatoStruct =  root.ChildNodes[i].Attributes["dataType"].Value.ToUpper();
							switch(wTipoDatoStruct)
							{
								case "NUMERIC":
									valorParam =  MycConvert.AdaptToDecimalDB(valorParam);
									break;
								case "DATETIME":
									valorParam =  MycConvert.AdaptToDateDB(valorParam);
									break;
								case "STRING":
									valorParam =  MycConvert.AdaptToStringDB(valorParam);
									break;			
							}								

							cmd.Parameters.AddWithValue(nombreParam, valorParam);
						}
					}

					OleDbConnection conn = GetConnection();
					cmd.Connection = conn;	
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					OleDbDataAdapter da = new OleDbDataAdapter(cmd);
					DataSet ds = new DataSet();
					da.Fill(ds);
					if (ds.Tables[0].Rows.Count > 0)
						wUltimoID = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
				
					if(conn != null)
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

					OleDbCommand cmd = new OleDbCommand();
					OleDbParameter[] parametros = new OleDbParameter[cantItems];
			
					for (int i=0; i<cantNodos; i++)
					{
						string nombreParam = null;
						string valorParam = null;
						nombreParam = "@" + root.ChildNodes[i].Name;
						valorParam = root.ChildNodes[i].InnerText;
						cmd.Parameters.AddWithValue(nombreParam, valorParam);
					}

					OleDbConnection conn = GetConnection();
					cmd.Connection = conn;	
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					OleDbDataAdapter da = new OleDbDataAdapter(cmd);
					DataSet ds = new DataSet();
					da.Fill(ds);
					if (ds.Tables[0].Rows.Count > 0)
						wUltimoID = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
				
					if(conn != null)
						cmd.Connection.Dispose();
                    
				}
			}
			catch(Exception e)
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

				OleDbCommand cmd = new OleDbCommand();

				for (int i=0; i<dt.Columns.Count; i++)
				{
					string nombreParam = "@" + dt.Columns[i].ColumnName;
					cmd.Parameters.AddWithValue(nombreParam, dr[i]);
				}

				OleDbConnection conn = GetConnection(pStrConn);
				cmd.Connection = conn;	
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = pSPname;
				OleDbDataAdapter da = new OleDbDataAdapter(cmd);
				DataSet ds = new DataSet();
				da.Fill(ds);
				if (ds.Tables[0].Rows.Count > 0)
					wUltimoID = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
				
				if(conn != null)
					cmd.Connection.Dispose();
			}
			catch(Exception e)
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

					OleDbCommand cmd = new OleDbCommand();
					OleDbParameter[] parametros = new OleDbParameter[cantItems];
			
					for (int i=0; i<cantNodos; i++)
					{
						string nombreParam = null;
						string valorParam = null;
						nombreParam = "@" + root.ChildNodes[i].Name;
						valorParam = root.ChildNodes[i].InnerText;
						cmd.Parameters.AddWithValue(nombreParam, valorParam);
					}

					OleDbConnection conn = GetConnection(pStrConn);
					cmd.Connection = conn;	
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					OleDbDataAdapter da = new OleDbDataAdapter(cmd);
					DataSet ds = new DataSet();
					da.Fill(ds);
					if (ds.Tables[0].Rows.Count > 0)
						wUltimoID = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
				
					if(conn != null)
						cmd.Connection.Dispose();
                    
				}
			}
			catch(Exception e)
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

					OleDbCommand cmd = new OleDbCommand();
					OleDbParameter[] parametros = new OleDbParameter[cantItems];
			
					for (int i=0; i<cantNodos; i++)
					{
						string nombreParam = null;
						string valorParam = null;
						nombreParam = "@" + root.ChildNodes[i].Name;
						valorParam = root.ChildNodes[i].InnerText;
						if (valorParam == "")
							valorParam = null;

						cmd.Parameters.AddWithValue(nombreParam, valorParam);
					}

					OleDbConnection conn = GetConnection(pStrConn);
					cmd.Connection = conn;	
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					OleDbDataAdapter da = new OleDbDataAdapter(cmd);
					DataSet ds = new DataSet();
					da.Fill(ds);
					if (ds.Tables[0].Rows.Count > 0)
						wUltimoID = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
				
					if(conn != null)
						cmd.Connection.Dispose();
                    
				}
			}
			catch(Exception e)
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
			string parametro = pSPname.Replace("spOCT_","@ID_").ToUpper();
			int ctrlString = parametro.IndexOf("_DEL");
			parametro = parametro.Replace("_DEL","");
			
			if(parametro.IndexOf("@ID_") < 0 || ctrlString == 0)
			{
				throw new Exception(string.Format("Nombre de Stored Procedure incorrecto. Imposible eliminar el registro. [Param = {0}; SP = {1}]", parametro, pSPname));
			}
			try
			{
				OleDbConnection conn = GetConnection();
				OleDbCommand cmd = new OleDbCommand();
				cmd.Parameters.AddWithValue(parametro, IDrow);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = pSPname;
				cmd.Connection = conn;
				cmd.ExecuteNonQuery();
				if (conn != null)
					conn.Dispose();
			}
			catch(Exception e)
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
			string parametro = "@" + pNombreParam.Replace("@","").ToUpper();
						
			try
			{
				OleDbConnection conn = GetConnection();
				OleDbCommand cmd = new OleDbCommand();
				cmd.Parameters.AddWithValue(parametro, IDrow);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = pSPname;
				cmd.Connection = conn;
				cmd.ExecuteNonQuery();
				if (conn != null)
					conn.Dispose();
			}
			catch(Exception e)
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
			string parametro = pSPname.Replace("spOCT_","@ID_").ToUpper();
			int ctrlString = parametro.IndexOf("_DEL");
			parametro = parametro.Replace("_DEL","");
			
			if(parametro.IndexOf("@ID_") < 0 || ctrlString == 0)
			{
				throw new Exception(string.Format("Nombre de Stored Procedure incorrecto. Imposible eliminar el registro. [Param = {0}; SP = {1}]", parametro, pSPname));
			}
			try
			{
				OleDbConnection conn = GetConnection(StrConn);
				OleDbCommand cmd = new OleDbCommand();
				cmd.Parameters.AddWithValue(parametro, IDrow);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = pSPname;
				cmd.Connection = conn;
				cmd.ExecuteNonQuery();
				if (conn != null)
					conn.Dispose();
			}
			catch(Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", pSPname, e.Message));
			}
		}
		#endregion

		#endregion

		#region ====== Update
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

				OleDbCommand cmd = new OleDbCommand();

				for (int i=0; i<dt.Columns.Count; i++)
				{
					string nombreParam = "@" + dt.Columns[i].ColumnName;
					cmd.Parameters.AddWithValue(nombreParam, dr[i]);
				}

				OleDbConnection conn = GetConnection();
				cmd.Connection = conn;	
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = pSPname;
				OleDbDataAdapter da = new OleDbDataAdapter(cmd);
				da.Fill(ds);

				if(conn != null)
					cmd.Connection.Dispose();
			}
			catch(Exception e)
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

				OleDbCommand cmd = new OleDbCommand();

				for (int i=0; i<dt.Columns.Count; i++)
				{
					string nombreParam = "@" + dt.Columns[i].ColumnName;
					cmd.Parameters.AddWithValue(nombreParam, dr[i]);
				}

				OleDbConnection conn = GetConnection(strConn);
				cmd.Connection = conn;	
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = pSPname;
				OleDbDataAdapter da = new OleDbDataAdapter(cmd);
				da.Fill(ds);

				if(conn != null)
					cmd.Connection.Dispose();
			}
			catch(Exception e)
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

					OleDbCommand cmd = new OleDbCommand();
					OleDbParameter[] parametros = new OleDbParameter[cantItems];
			
					for (int i=0; i<cantNodos; i++)
					{
						string nombreParam = null;
						string valorParam = null;
						nombreParam = "@" + root.ChildNodes[i].Name;
						valorParam = root.ChildNodes[i].InnerText;
						
						if (valorParam !="")
						{
							string wTipoDatoStruct =  root.ChildNodes[i].Attributes["dataType"].Value.ToUpper();
							switch(wTipoDatoStruct)
							{
								case "NUMERIC":
									valorParam =  MycConvert.AdaptToDecimalDB(valorParam);
									break;
								case "DATETIME":
									valorParam =  MycConvert.AdaptToDateDB(valorParam);
									break;
								case "STRING":
									valorParam =  MycConvert.AdaptToStringDB(valorParam);
									break;			
							}								

							cmd.Parameters.AddWithValue(nombreParam, valorParam);
						}
					}


					OleDbConnection conn = GetConnection();
					cmd.Connection = conn;	
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					OleDbDataAdapter da = new OleDbDataAdapter(cmd);
					DataSet ds = new DataSet();
					da.Fill(ds);
					if (ds.Tables[0].Rows.Count > 0)
						wUltimoID = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
				
					if(conn != null)
						cmd.Connection.Dispose();
                    
				}
			}
			catch(Exception e)
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

					OleDbCommand cmd = new OleDbCommand();
					OleDbParameter[] parametros = new OleDbParameter[cantItems];
			
					for (int i=0; i<cantNodos; i++)
					{
						string nombreParam = null;
						string valorParam = null;
						nombreParam = "@" + root.ChildNodes[i].Name;
						valorParam = root.ChildNodes[i].InnerText;
						if (valorParam == "")
							valorParam = null;

						cmd.Parameters.AddWithValue(nombreParam, valorParam);
					}

					OleDbConnection conn = GetConnection(pStrConn);
					cmd.Connection = conn;	
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					OleDbDataAdapter da = new OleDbDataAdapter(cmd);
					DataSet ds = new DataSet();
					da.Fill(ds);
					if (ds.Tables[0].Rows.Count > 0)
						wUltimoID = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
				
					if(conn != null)
						cmd.Connection.Dispose();
                    
				}
			}
			catch(Exception e)
			{
				throw new Exception(string.Format("[SP: {0}] {1}", pSPname, e.Message));
			}
			return wUltimoID;
		}

		#endregion

		#endregion
		
	}
}
