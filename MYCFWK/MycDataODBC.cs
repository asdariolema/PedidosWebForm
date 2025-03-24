using System;
using System.Data;
using System.Data.Odbc;
using System.Configuration;
using Myc.data.SQLServer;
using System.Xml;
using Myc.data.Conversiones;

namespace Myc.data.ODBC
{
	/// <summary>
	/// Acciones de DAC (Data Access Component) para bases de datos que soporten stored procedures y conexi�n Odbc.
	/// </summary>
	public class MycData : MycConvert
	{
		/// <summary>
		/// M�todo Constructor
		/// </summary>
		public MycData()
		{
//			string registroFWK = Myc.General.MycRegistry.readReg("SOFTWARE\\Myc\\FWK");
//			if(registroFWK != "OCT-F5E35E7B-2EDA-43cb-8A1E-0CB75BF8F147")
//				throw new Exception("Cannot access Myc Assembly - [Invalid Use]");
		}

		#region ====== conexi�n
		/// <summary>
		/// Crea un objeto Conexi�n, pas�ndole un string de conexi�n determinado.
		/// </summary>
		/// <param name="pStrConn">String de conexi�n</param>
		/// <returns>El objeto conexi�n se devuelve ya abierto.</returns>
		public OdbcConnection GetConnection(string pStrConn)
		{
			//usa un string de conexi�n espec�fico
			//#line 100
			OdbcConnection connection = null;
			try
			{
				connection = new OdbcConnection(pStrConn);
				connection.Open();
			}
			catch(Exception e)
			{
				throw new Exception(string.Format("No se pudo crear la conexi�n con el string del par�metro] {0}",e.Message));
			}
			return connection;
		}


		/// <summary>
		/// Crea un objeto Conexi�n, tomando el string de conexi�n determinado del archivo de configuraci�n.
		/// </summary>
		/// <remarks>
		/// El string en el archivo de configuraci�n debe aparecer bajo una clave con nombre "ConnString".
		/// </remarks>
		/// <returns>El objeto conexi�n se devuelve ya abierto.</returns>
		public OdbcConnection GetConnection()
		{	
			//usa el string de conexi�n tomado de la configuraci�n
			OdbcConnection connection = null;
			//#line 200
			try
			{
				string strConn = ConfigurationManager.AppSettings["ConnString"];
				connection = new OdbcConnection(strConn);
				connection.Open();
			}
			catch(Exception e)
			{
				throw new Exception(string.Format("[No se pudo crear la conexi�n con el string de la configuraci�n] {0}", e.Message));
			}
			return connection;
		}
		#endregion

		#region ====== Prop�sito Gral.

		#region Ejecuta un SP de prop�sito general pas�ndole los par�metros en un objeto XML. Devuelve un DATASET.
		/// <summary>
		/// Ejecuta un Stored Procedure de prop�sito general (para uso general). Los par�metros se pasan en un OBJETO XML.
		/// </summary>
		/// <remarks>
		/// El string de conexi�n lo toma del archivo de configuraci�n, cuya clave es "ConnString".
		/// </remarks>
		/// <param name="pSPname">Nombre del Stored Procedure</param>
		/// <param name="pXMLParam">Un objeto XML conteniendo los par�metros a pasarle al Stored Procedure y sus valores. El nombre del nodo debe llamase como el par�metro, 
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

					OdbcCommand cmd = new OdbcCommand();

					OdbcParameter[] parametros = new OdbcParameter[cantItems];
				
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
					OdbcConnection conn = GetConnection();
					cmd.Connection = conn;	
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					cmd.ExecuteNonQuery();
					OdbcDataAdapter da = new OdbcDataAdapter(cmd);
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

		#region Ejecuta un SP de prop�sito general pas�ndole los par�metros en un string XML. Devuelve un DATASET.
		/// <summary>
		/// Ejecuta un Stored Procedure de prop�sito general (para uso general). Los par�metros se pasan en un STRING XML.
		/// </summary>
		/// <remarks>
		/// El string de conexi�n lo toma del archivo de configuraci�n, cuya clave es "ConnString".
		/// El string XML con los par�metros debe estar bien formado y debe tener un solo nivel de hijos.
		/// </remarks>
		/// <param name="pSPname">Nombre del Stored Procedure</param>
		/// <param name="pXMLParam">Un string XML conteniendo los par�metros a pasarle al Stored Procedure y sus valores. El nombre del nodo debe llamase como el par�metro, 
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

					OdbcCommand cmd = new OdbcCommand();

					OdbcParameter[] parametros = new OdbcParameter[cantItems];
				
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
					OdbcConnection conn = GetConnection();
					cmd.Connection = conn;	
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					cmd.ExecuteNonQuery();
					OdbcDataAdapter da = new OdbcDataAdapter(cmd);
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

		#region Ejecuta un SP de prop�sito general pas�ndole los par�metros en un OBJETO XML y el string de conexi�n. Devuelve un DATASET.
		/// <summary>
		/// Ejecuta un Stored Procedure de prop�sito general (para uso general). Los par�metros se pasan en un OBJETO XML.
		/// </summary>
		/// <remarks>
		/// El string de conexi�n lo toma del archivo de configuraci�n, cuya clave es "ConnString".
		/// </remarks>
		/// <param name="pSPname">Nombre del Stored Procedure</param>
		/// <param name="pXMLParam">Un objeto XML conteniendo los par�metros a pasarle al Stored Procedure y sus valores. El nombre del nodo debe llamase como el par�metro, 
		/// y su contenido como valor.</param>
		/// <param name="pStrConn">El string de conexi�n.</param>
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

					OdbcCommand cmd = new OdbcCommand();

					OdbcParameter[] parametros = new OdbcParameter[cantItems];
			
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
					OdbcConnection conn = GetConnection(pStrConn);
					cmd.Connection = conn;	
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					cmd.ExecuteNonQuery();
					OdbcDataAdapter da = new OdbcDataAdapter(cmd);
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

		#region Ejecuta un SP de prop�sito general pas�ndole los par�metros en un string XML y el string de conexi�n. Devuelve un DATASET.
		/// <summary>
		/// Ejecuta un Stored Procedure de prop�sito general devolviendo un DataSet con los datos.
		/// Los par�metros se pasan en un STRING XML.
		/// </summary>
		/// <param name="pSPname">Nombre del Stored Procedure</param>
		/// <param name="pStrXMLParam">Un string XML conteniendo los par�metros a pasarle al Stored Procedure y sus valores. El nombre del nodo debe llamase como el par�metro, 
		/// y su contenido como valor.</param>
		/// <param name="pStrConn">El string de conexi�n.</param>
		/// <returns></returns>
		public DataSet ExecuteSP(string pSPname, string pStrXMLParam, string pStrConn)
		{
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

					OdbcCommand cmd = new OdbcCommand();

					OdbcParameter[] parametros = new OdbcParameter[cantItems];
			
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
					OdbcConnection conn = GetConnection(pStrConn);
					cmd.Connection = conn;	
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					cmd.ExecuteNonQuery();
					OdbcDataAdapter da = new OdbcDataAdapter(cmd);
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
		/// <summary>
		/// Ejecuta una Ejecuta una consulta SQL literal sobre la base de datos conectada mediante ODBC.
		/// El nombre de la conexi�n se toma del archivo .config
		/// </summary>
		/// <param name="pSQL">SQL Query</param>
		/// <returns>DataSet con el resultado de la consulta</returns>
		public DataSet ExecuteSQLquery(string pSQL)
		{
			DataSet dsResult = new DataSet();	//DataSet con resultado
			try
			{
				OdbcCommand cmd = new OdbcCommand(pSQL);

				OdbcConnection conn = GetConnection();
				cmd.Connection = conn;	
				cmd.ExecuteNonQuery();
				OdbcDataAdapter da = new OdbcDataAdapter(cmd);
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


		/// <summary>
		/// Ejecuta una Ejecuta una consulta SQL literal sobre la base de datos conectada mediante ODBC.
		/// </summary>
		/// <param name="pSQL">SQL Query</param>
		/// <param name="pStrConn">Nombre de la conexi�n ODBC. Debe pasarse como "DSN=nombreConexionODBC".</param>
		/// <returns>DataSet con el resultado de la consulta</returns>
		public DataSet ExecuteSQLquery(string pSQL, string pStrConn)
		{
			DataSet dsResult = new DataSet();	//DataSet con resultado
			try
			{
				OdbcCommand cmd = new OdbcCommand(pSQL);

				OdbcConnection conn = GetConnection(pStrConn);
				cmd.Connection = conn;	
				cmd.ExecuteNonQuery();
				OdbcDataAdapter da = new OdbcDataAdapter(cmd);
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

		#region Ejecuta una consulta SQL literal de actualizaci�n (Update)
		public int ExecuteSQLupdate(string pSQL)
		{

			int wResult = 0;
			try
			{

				OdbcCommand cmd = new OdbcCommand(pSQL);

				OdbcConnection conn = GetConnection();
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

		#region Ejecuta una consulta SQL literal de inserci�n (Insert)
		public int ExecuteSQLinsert(string pSQL)
		{

			int wResult = 0;
			try
			{

				OdbcCommand cmd = new OdbcCommand(pSQL);

				OdbcConnection conn = GetConnection();
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


		#endregion

		#region ====== Query

		#region *Ejecuta un SP de consulta pas�ndole los par�metros en un objeto XML. Devuelve un DATASET.
		/// <summary>
		/// Ejecuta un Stored Procedure de consulta devolviendo un DataSet con los datos. 
		/// Los par�metros se pasan en un OBJETO XML.
		/// El string de conexi�n lo toma del archivo de configuraci�n, cuya clave es "ConnString".
		/// </summary>
		/// <param name="pSPname">Nombre del Stored Procedure</param>
		/// <param name="pXMLParam">Un objeto XML conteniendo los par�metros a pasarle al Stored Procedure y sus valores. El nombre del nodo debe llamase como el par�metro, 
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

					OdbcCommand cmd = new OdbcCommand();

					OdbcParameter[] parametros = new OdbcParameter[cantItems];

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

					OdbcConnection conn = GetConnection();
					cmd.Connection = conn;	
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					cmd.ExecuteNonQuery();
					OdbcDataAdapter da = new OdbcDataAdapter(cmd);
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

		#region *Ejecuta un SP de consulta pas�ndole los par�metros en un string XML. Devuelve un DATASET.
		/// <summary>
		/// Ejecuta un Stored Procedure de consulta devolviendo un DataSet con los datos.
		/// Los par�metros se pasan en un STRING XML.
		/// El string de conexi�n lo toma del archivo de configuraci�n, cuya clave es "ConnString".
		/// </summary>
		/// <param name="pSPname">Nombre del Stored Procedure</param>
		/// <param name="pXMLParam">Un string XML conteniendo los par�metros a pasarle al Stored Procedure y sus valores. El nombre del nodo debe llamase como el par�metro, 
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

					OdbcCommand cmd = new OdbcCommand();

					OdbcParameter[] parametros = new OdbcParameter[cantItems];

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
					OdbcConnection conn = GetConnection();
					cmd.Connection = conn;	
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					cmd.ExecuteNonQuery();
					OdbcDataAdapter da = new OdbcDataAdapter(cmd);
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

		#region *Ejecuta un SP de consulta pas�ndole los par�metros en un objeto XML y el string de conexi�n. Devuelve un DATASET.
		/// <summary>
		/// Ejecuta un Stored Procedure de consulta devolviendo un DataSet con los datos.
		/// Los par�metros se pasan en un OBJETO XML.
		/// </summary>
		/// <param name="pSPname">Nombre del Stored Procedure</param>
		/// <param name="pXMLParam">Un objeto XML conteniendo los par�metros a pasarle al Stored Procedure y sus valores. El nombre del nodo debe llamase como el par�metro, 
		/// y su contenido como valor.</param>
		/// <param name="pStrConn">String de Conexi�n</param>
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

					OdbcCommand cmd = new OdbcCommand();

					OdbcParameter[] parametros = new OdbcParameter[cantItems];

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

					OdbcConnection conn = GetConnection(pStrConn);
					cmd.Connection = conn;	
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					cmd.ExecuteNonQuery();
					OdbcDataAdapter da = new OdbcDataAdapter(cmd);
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

		#region *Ejecuta un SP de consulta pas�ndole los par�metros en un string XML y el string de conexi�n. Devuelve un DATASET.
		/// <summary>
		/// Ejecuta un Stored Procedure de consulta devolviendo un DataSet con los datos.
		/// Los par�metros se pasan en un STRING XML.
		/// </summary>
		/// <param name="pSPname">Nombre del Stored Procedure</param>
		/// <param name="pXMLParam">Un string XML conteniendo los par�metros a pasarle al Stored Procedure y sus valores. El nombre del nodo debe llamase como el par�metro, 
		/// y su contenido como valor.</param>
		/// <param name="pStrConn">String de Conexi�n</param>
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

					OdbcCommand cmd = new OdbcCommand();

					OdbcParameter[] parametros = new OdbcParameter[cantItems];
			
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
					OdbcConnection conn = GetConnection(pStrConn);
					cmd.Connection = conn;	
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					cmd.ExecuteNonQuery();
					OdbcDataAdapter da = new OdbcDataAdapter(cmd);
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
		#region Ejecuta un SP de INSERT pas�ndole un DataSet.
		/// <summary>
		/// Ejecuta un SP de INSERT (_ins) pas�ndole un DATASET y el string de conexi�n.
		/// El string de conexi�n lo toma del archivo de configuraci�n, cuya clave es "ConnString".
		/// </summary>
		/// <param name="pSPname">Nombre del Stored Procedure</param>
		/// <param name="pDStableBD">DataSet con los datos. Los nombres de los campos corresponden a los par�metros del SP.</param>
		public int ExecuteSpInsert(string pSPname, DataSet pDStableBD)
		{
			int wUltimoID = 0;

			try
			{
//#line 1100
				DataRow dr = pDStableBD.Tables[0].Rows[0];
				DataTable dt = pDStableBD.Tables[0];

				OdbcCommand cmd = new OdbcCommand();

				for (int i=0; i<dt.Columns.Count; i++)
				{
					string nombreParam = "@" + dt.Columns[i].ColumnName;
					cmd.Parameters.AddWithValue(nombreParam, dr[i]);
				}

				OdbcConnection conn = GetConnection();
				cmd.Connection = conn;	
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = pSPname;
				OdbcDataAdapter da = new OdbcDataAdapter(cmd);
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

		#region *Ejecuta un SP de INSERT pas�ndole un string XML.
		/// <summary>
		/// Ejecuta un SP de INSERT (_ins) pas�ndole un DATASET.
		/// El string de conexi�n lo toma del archivo de configuraci�n, cuya clave es "ConnString".
		/// </summary>
		/// <param name="pSPname">Nombre del Stored Procedure</param>
		/// <param name="pStrXMLParam">Un string XML conteniendo los par�metros a pasarle al Stored Procedure y sus valores. El nombre del nodo debe llamase como el par�metro, 
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

					OdbcCommand cmd = new OdbcCommand();
					OdbcParameter[] parametros = new OdbcParameter[cantItems];
			

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

					OdbcConnection conn = GetConnection();
					cmd.Connection = conn;	
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					OdbcDataAdapter da = new OdbcDataAdapter(cmd);
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

		#region Ejecuta un SP de INSERT pas�ndole un objeto XML.
		/// <summary>
		/// Ejecuta un SP de INSERT (_ins) pas�ndole un DataSet y el string de conexi�n.
		/// Los par�metros se pasan en un OBJETO XML.
		/// El string de conexi�n lo toma del archivo de configuraci�n, cuya clave es "ConnString".
		/// </summary>
		/// <param name="pSPname">Nombre del Stored Procedure</param>
		/// <param name="pXMLParam">Un objeto XML conteniendo los par�metros a pasarle al Stored Procedure y sus valores. El nombre del nodo debe llamase como el par�metro, 
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

					OdbcCommand cmd = new OdbcCommand();
					OdbcParameter[] parametros = new OdbcParameter[cantItems];
			
					for (int i=0; i<cantNodos; i++)
					{
						string nombreParam = null;
						string valorParam = null;
						nombreParam = "@" + root.ChildNodes[i].Name;
						valorParam = root.ChildNodes[i].InnerText;
						cmd.Parameters.AddWithValue(nombreParam, valorParam);
					}

					OdbcConnection conn = GetConnection();
					cmd.Connection = conn;	
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					OdbcDataAdapter da = new OdbcDataAdapter(cmd);
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

		#region Ejecuta un SP de INSERT pas�ndole un DataSet y el string de conexi�n.
		/// <summary>
		/// Ejecuta un SP de INSERT (_ins) pas�ndole un DATASET y el string de conexi�n.
		/// </summary>
		/// <param name="pSPname">Nombre del Stored Procedure</param>
		/// <param name="pDStableBD">DataSet con los datos. Los nombres de los campos corresponden a los par�metros del SP.
		/// Si el ID de la tabla es clave primaria autonum�tico (Identity) no se debe pasar este par�metro.</param>
		/// <param name="pStrConn">String de conexi�n</param>
		public int ExecuteSpInsert(string pSPname, DataSet pDStableBD, string pStrConn)
		{
			int wUltimoID = 0;

			try
			{
//#line 1400
				DataRow dr = pDStableBD.Tables[0].Rows[0];
				DataTable dt = pDStableBD.Tables[0];

				OdbcCommand cmd = new OdbcCommand();

				for (int i=0; i<dt.Columns.Count; i++)
				{
					string nombreParam = "@" + dt.Columns[i].ColumnName;
					cmd.Parameters.AddWithValue(nombreParam, dr[i]);
				}

				OdbcConnection conn = GetConnection(pStrConn);
				cmd.Connection = conn;	
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = pSPname;
				OdbcDataAdapter da = new OdbcDataAdapter(cmd);
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

		#region Ejecuta un SP de INSERT pas�ndole un objeto XML y el string de conexi�n.
		/// <summary>
		/// Ejecuta un SP de INSERT (_ins) pas�ndole un DataSet y el string de conexi�n.
		/// Los par�metros se pasan en un OBJETO XML.
		/// </summary>
		/// <param name="pSPname">Nombre del Stored Procedure</param>
		/// <param name="pXMLParam">Un objeto XML conteniendo los par�metros a pasarle al Stored Procedure y sus valores. El nombre del nodo debe llamase como el par�metro, 
		/// y su contenido como valor.
		/// Si el ID de la tabla es clave primaria autonum�tico (Identity) no se debe pasar este par�metro.</param>
		/// <param name="pStrConn">String de conexi�n</param>
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

					OdbcCommand cmd = new OdbcCommand();
					OdbcParameter[] parametros = new OdbcParameter[cantItems];
			
					for (int i=0; i<cantNodos; i++)
					{
						string nombreParam = null;
						string valorParam = null;
						nombreParam = "@" + root.ChildNodes[i].Name;
						valorParam = root.ChildNodes[i].InnerText;
						cmd.Parameters.AddWithValue(nombreParam, valorParam);
					}

					OdbcConnection conn = GetConnection(pStrConn);
					cmd.Connection = conn;	
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					OdbcDataAdapter da = new OdbcDataAdapter(cmd);
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

		#region Ejecuta un SP de INSERT pas�ndole un string XML y el string de conexi�n.
		/// <summary>
		/// Ejecuta un SP de INSERT (_ins) pas�ndole un DATASET y el string de conexi�n.
		/// Los par�metros se pasan en un STRING XML.
		/// </summary>
		/// <param name="pSPname">Nombre del Stored Procedure</param>
		/// <param name="pStrXMLParam">Un string XML conteniendo los par�metros a pasarle al Stored Procedure y sus valores. El nombre del nodo debe llamase como el par�metro, 
		/// y su contenido como valor.
		/// Si el ID de la tabla es clave primaria autonum�tico (Identity) no se debe pasar este par�metro.</param>		
		/// <param name="pStrConn">String de conexi�n</param>
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

					OdbcCommand cmd = new OdbcCommand();
					OdbcParameter[] parametros = new OdbcParameter[cantItems];
			
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

					OdbcConnection conn = GetConnection(pStrConn);
					cmd.Connection = conn;	
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					OdbcDataAdapter da = new OdbcDataAdapter(cmd);
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
		/// <remarks>El nombre del par�metro del a pasar al Stored Procedure se toma del nombre pasado como par�metro. 
		/// Si el par�metro se llama "@ID_Precio", el nombre del Stored Procedure a pasar deber� llamarse "spPrecio_del".
		/// El nombre del par�metro siempre debe comenzar con "@ID_".
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
				OdbcConnection conn = GetConnection();
				OdbcCommand cmd = new OdbcCommand();
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

		#region Ejecuta un Stored Procedure que elimina un registro, pasando el nombre del par�metro.
		/// <summary>
		/// Ejecuta un Stored Procedure que elimina un registro. Se pasa el nombre del Stored Procedure, 
		/// el nombre del par�metro que identifica el ID del registro a eliminar y el ID del registro.
		/// </summary>
		/// <remarks>
		/// Utilizar este m�todo cuando el ID del registro a eliminar no coincida con el nombre de SP. 
		/// </remarks>
		/// <param name="pSPname">string. Nombre del Stored Procedure</param>
		/// <param name="pNombreParam">Nombre del par�metro del registro a eliminar</pNombreParam>
		/// <param name="IDrow">ID del registro a eliminar</param>
		public void ExecuteSpDelete(string pSPname, string pNombreParam, int IDrow)
		{
			//#line 1750
			string parametro = "@" + pNombreParam.Replace("@","").ToUpper();
						
			try
			{
				OdbcConnection conn = GetConnection();
				OdbcCommand cmd = new OdbcCommand();
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

		#region Ejecuta un Stored Procedure que elimina un registro, pasando el string de conexi�n.
		/// <summary>
		/// Ejecuta un Stored Procedure que elimina un registro. Se pasa el ID del regitro, el nombre del Stored Procedure
		/// y el string de conexi�n.
		/// </summary>
		/// <remarks>El nombre del par�metro del a pasar al Stored Procedure se toma del nombre pasado como par�metro. 
		/// Si el par�metro se llama "@ID_Precio", el nombre del Stored Procedure a pasar deber� llamarse "spPrecio_del".
		/// El nombre del par�metro siempre debe comenzar con "@ID_".
		/// </remarks>
		/// <param name="pSPname">string. Nombre del Stored Procedure</param>
		/// <param name="IDrow">int. ID del registro a eliminar</param>
		/// <param name="pSPname">string. String de Conexi�n</param>
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
				OdbcConnection conn = GetConnection(StrConn);
				OdbcCommand cmd = new OdbcCommand();
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
		#region Ejecuta un SP de UPDATE pas�ndole un DataSet.	
		/// <summary>
		/// Ejecuta un SP de UPDATE (_upd) pas�ndole un DataSet.
		/// </summary>
		/// <param name="pSPname">Nombre del Stored Procedure.</param>
		/// <param name="pDStableBD">DataSet con los datos actualizar. Los nombres de los campos corresponden a los par�metros del SP.</param>
		/// <returns>Cantidad de registros afectados.</returns>
		public int ExecuteSPupdate(string pSPname, DataSet pDStableBD)
		{
			DataSet ds = new DataSet();
			try
			{
//#line 1900
				DataRow dr = pDStableBD.Tables[0].Rows[0];
				DataTable dt = pDStableBD.Tables[0];

				OdbcCommand cmd = new OdbcCommand();

				for (int i=0; i<dt.Columns.Count; i++)
				{
					string nombreParam = "@" + dt.Columns[i].ColumnName;
					cmd.Parameters.AddWithValue(nombreParam, dr[i]);
				}

				OdbcConnection conn = GetConnection();
				cmd.Connection = conn;	
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = pSPname;
				OdbcDataAdapter da = new OdbcDataAdapter(cmd);
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

		#region Ejecuta un SP de UPDATE pas�ndole un DataSet y el string de conexi�n.
		/// <summary>
		/// Ejecuta un SP de UPDATE (_upd) pas�ndole un DataSet y el string de conexi�n.
		/// </summary>
		/// <param name="pSPname">Nombre del Stored Procedure.</param>
		/// <param name="pDStableBD">DataSet con los datos actualizar. Los nombres de los campos corresponden a los par�metros del SP.</param>
		/// <returns>Cantidad de registros afectados.</returns>		/// <param name="strConn">String de conexi�n.</param>
		public int ExecuteSPupdate(string pSPname, DataSet pDStableBD, string strConn)
		{
			DataSet ds = new DataSet();
			try
			{
//#line 2000
				DataRow dr = pDStableBD.Tables[0].Rows[0];
				DataTable dt = pDStableBD.Tables[0];

				OdbcCommand cmd = new OdbcCommand();

				for (int i=0; i<dt.Columns.Count; i++)
				{
					string nombreParam = "@" + dt.Columns[i].ColumnName;
					cmd.Parameters.AddWithValue(nombreParam, dr[i]);
				}

				OdbcConnection conn = GetConnection(strConn);
				cmd.Connection = conn;	
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = pSPname;
				OdbcDataAdapter da = new OdbcDataAdapter(cmd);
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

		#region *Ejecuta un SP de UPDATE pas�ndole un string XML.	
		/// <summary>
		/// Ejecuta un SP de UPDATE (_upd) pas�ndole un string XML.
		/// </summary>
		/// <param name="pSPname">Nombre del Stored Procedure.</param>
		/// <param name="pStrXMLParam">Un string XML conteniendo los par�metros a pasarle al Stored Procedure y sus valores. El nombre del nodo debe llamase como el par�metro, 
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

					OdbcCommand cmd = new OdbcCommand();
					OdbcParameter[] parametros = new OdbcParameter[cantItems];
			
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


					OdbcConnection conn = GetConnection();
					cmd.Connection = conn;	
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					OdbcDataAdapter da = new OdbcDataAdapter(cmd);
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

		#region Ejecuta un SP de UPDATE pas�ndole un string XML y el string de conexi�n.	
		/// <summary>
		/// Ejecuta un SP de UPDATE (_upd) pas�ndole un string XML y el string de conexi�n
		/// </summary>
		/// <param name="pSPname">Nombre del Stored Procedure.</param>
		/// <param name="pStrXMLParam">Un string XML conteniendo los par�metros a pasarle al Stored Procedure y sus valores. El nombre del nodo debe llamase como el par�metro, 
		/// <param name="pStrConn">String de conexi�n</param>
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

					OdbcCommand cmd = new OdbcCommand();
					OdbcParameter[] parametros = new OdbcParameter[cantItems];
			
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

					OdbcConnection conn = GetConnection(pStrConn);
					cmd.Connection = conn;	
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = pSPname;
					OdbcDataAdapter da = new OdbcDataAdapter(cmd);
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
