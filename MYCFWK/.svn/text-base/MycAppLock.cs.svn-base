using System;
using System.IO;
using System.Management;
using Myc.data.Conversiones;

namespace Myc.General
{
	/// <summary>
	/// Summary description for AppLock.
	/// </summary>
	public class AppLock
	{
		public AppLock()
		{
		}

		private string wClaveEncriptacion = "%#04?01*1963!";

		//Devuelve NULL si la validación es correcta. Si no, devuelve la clave que el usuario debe
		//enviar para solicitar la clave de licencia.
		//La clave de validación se obtiene de la registry o del archivo de texto "MycSID.dll"
		public string getAcceso()
		{
			string idSerial = serialIDfromDisk();
			if ((idSerial.CompareTo(serialIDfromRegistry()) == 0) || (idSerial.CompareTo(serialIDfromFile()) == 0))
			{
				return null;
			}
			else
			{
				return generarClaveParaLicencia();
			}
		}

		//Devuelve NULL si la validación es correcta. Si no, devuelve la clave que el usuario debe
		//enviar para solicitar la clave de licencia.
		//La validación se obtiene directamente del serial id comparado con el valor que se le pase."
		public string getAcceso(string pIDserial)
		{
			string idSerial = serialIDfromDisk();
			if ((idSerial.CompareTo(pIDserial) == 0))
			{
				return null;
			}
			else
			{
				return generarClaveParaLicencia();
			}
		}

		public bool serialIDtoRegistry()
		{
			try
			{
				MycRegistry.writeReg("Myc\\FWK", "SID", MycConvert.Encrypt(serialIDfromDisk(), wClaveEncriptacion));
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool serialIDtoFile()
		{
			try
			{
				StreamWriter sw = new StreamWriter("MycSID.dll", false);
				string claveLicencia = MycConvert.Encrypt(serialIDfromDisk(), wClaveEncriptacion);
				sw.WriteLine(claveLicencia);
				sw.Close();
				return true;
			}
			catch
			{
				return false;
			}
		}


		//Crea una clave para que sea enviada al proveedor y este le genere la clave de la licencia
		//correspondiente. La clave es el serialID del disco + algunos caracteres al azar.
		private string generarClaveParaLicencia()
		{
			//nro. de serie del disco
			string idDisk = serialIDfromDisk();

			//posición donde se insertará la fecha, en medio del id del disco
			int maxRnd = 10;
			if (idDisk.Length < maxRnd)
				maxRnd = idDisk.Length;

			Random rnd = new Random();
			int posInsertar = rnd.Next(maxRnd);

			//fecha con caracteres agregados (aaMmmCdd)
			string fecha = string.Format("{0:00}M{1:00}C{2:00}", DateTime.Now.Year.ToString().Remove(0, 2), DateTime.Now.Month, DateTime.Now.Day);

			//Generar la clave para la licencia. Se agregan 4 caracteres al azar al final.
			string strClave = string.Format("{0:000}{1:0000}{2:00}", idDisk, DateTime.Now.Millisecond, posInsertar).Trim();
			strClave = strClave.Insert(posInsertar, fecha);
			return strClave;
		}

		public string getSerialFromClaveLicencia(string pClaveParaLicencia)
		{
			//Quitar los dos caracteres correspondientes a la posición donde se encuentra la fecha
			string Clave = pClaveParaLicencia.Substring(0, (pClaveParaLicencia.Length - 2));

			//Posición donde se encuentra la fecha
			int posExtraerFecha = Convert.ToInt32(pClaveParaLicencia.Substring(pClaveParaLicencia.Length - 2));

			//fecha con caracteres "M" entre el año y mes y "C" entre el mes y el día (aaMmmCdd)
			string fechaCruda = pClaveParaLicencia.Substring(posExtraerFecha, 8);

			//limpiar la fecha y dejarla como aammdd
			string fecha = pClaveParaLicencia.Replace("M", string.Empty);
			fecha = pClaveParaLicencia.Replace("C", string.Empty);

			//Quitar la fecha sin limpiar de la clave
			Clave = Clave.Replace(fechaCruda, string.Empty);

			//Quitar los últimos 4 caracteres de la clave
			Clave = Clave.Substring(0, Clave.Length - 4);

			return Clave;
		}


		public string serialIDfromDisk()
		{
			ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
			disk.Get();
			return disk["VolumeSerialNumber"].ToString();
		}

		private string serialIDfromRegistry()
		{
			try
			{
				return MycConvert.Decrypt(MycRegistry.readReg("SOFTWARE\\Myc\\FWK", "SID"), wClaveEncriptacion);
			}
			catch
			{
				return string.Empty;
			}
		}

		//Lee la clave desde un archivo de texto llamado MycSid.dll
		private string serialIDfromFile()
		{
			try
			{
				StreamReader sr = new StreamReader("MycSid.dll");
				return MycConvert.Decrypt(sr.ReadLine(), wClaveEncriptacion);
			}
			catch
			{
				return string.Empty;
			}
		}
	}
}