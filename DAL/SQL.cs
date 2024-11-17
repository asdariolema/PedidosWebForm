using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Odbc;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Web;
using System.Globalization;

namespace DAL
{
   public class SQL
    {

        //private static string _connectionString = leerApp();
        public static string DNS = Decrypt(System.Configuration.ConfigurationSettings.AppSettings["ConnectionISeries"].ToString(), "mlmweb").Replace("Dsn=", "").Replace(";", "").Trim();
        //private string[] param = {"","2"};
        public static string varcharNULL = "";
        public static string dateNULL = "0001-01-01";
        public static string dateNULLsql = "01-01-2900";
        public static string numericNULL = "null";
        public static string numericCero = "0";
        public static string characterNULL = null;
        public static string integerNULL = "null";
        public static string timeNULL = "null";

        public static string characterNULL_SQL = "null";

        public static string Call = "EXECUTE "; // + As400Helper.DNS + ".";

        //public static string DNS = Decrypt(System.Configuration.ConfigurationSettings.AppSettings["ConnString"].ToString(), "mlmweb").Replace("Dsn=", "").Replace(";", "").Trim();
        private string[] param = { "", "2" };
        public static string numericNULL_SQL = "null";
        public static string decimalNULL_SQL = "null";
        public static string smallintNULL_SQL = "null";
        public static string intNULL_SQL = "null";
        public static string bigintNULL_SQL = "null";
        public static string bitNULL_SQL = "null";
        public static string floatNULL_SQL = "null";
        public static string realNULL_SQL = "null";
        public static string moneyNULL_SQL = "null";
        public static string dateNULL_SQL = "null";
        public static string datetimeNULL_SQL = "null";
        public static string smalldatetimeNULL_SQL = "null";
        public static string timeNULL_SQL = "null";
        public static string varcharNULL_SQL = "null";
        public static string charNULL_SQL = "null";
        public static string textNULL_SQL = "null";
        public static string nvarcharNULL_SQL = "null";
        public static string ncharNULL_SQL = "null";

        public static string AdaptToDateDBSQL(string p_DateTime)
        {
            try
            {
                DateTime w_Date = Convert.ToDateTime(p_DateTime, System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat);

                string año = string.Format("{0:0000}", w_Date.Year);
                string mes = string.Format("{0:00}", w_Date.Month);
                string dia = string.Format("{0:00}", w_Date.Day);
                string hora = string.Format("{0:00}", w_Date.Hour);
                string min = string.Format("{0:00}", w_Date.Minute);
                string seg = string.Format("{0:00}", w_Date.Second);
                string mseg = string.Format("{0:000}", w_Date.Millisecond);

                string w_strFecha = string.Empty;

                if (System.Configuration.ConfigurationSettings.AppSettings["DBdate"] != null)
                {
                    w_strFecha = System.Configuration.ConfigurationSettings.AppSettings["DBdate"];
                    w_strFecha = w_strFecha.Replace("YYYY", año);
                    w_strFecha = w_strFecha.Replace("MM", mes);
                    w_strFecha = w_strFecha.Replace("DD", dia);
                    w_strFecha = w_strFecha.Replace("hh", hora);
                    w_strFecha = w_strFecha.Replace("mm", min);
                    w_strFecha = w_strFecha.Replace("ss", seg);
                    w_strFecha = w_strFecha.Replace("ms", mseg);
                }
                else
                {
                    w_strFecha = string.Format("{0:0000}-{1:00}-{2:00} {3:00}:{4:00}:{5:00}",
                    w_Date.Year, w_Date.Month, w_Date.Day,
                    w_Date.Hour, w_Date.Minute, w_Date.Second);
                }

                return w_strFecha.Trim();
            }
            catch (Exception e)
            {
                string w_msje = string.Format("Se produjo un error al intentar transformar la fecha {0}. [{1}]", p_DateTime, e.Message);

                return string.Empty;
            }

        }


        public static void EjecutarTransaccion(List<string> call)
        {

            //string strConn = "Dsn=SGMDESA;uid=ISNET;pwd=isnet";
            //string wPassw = "mlmweb";
            //string strConn = Decrypt(System.Configuration.ConfigurationSettings.AppSettings["connectionIs"], wPassw);

            using (OdbcConnection connection = new OdbcConnection(leerApp()))
            {
                OdbcCommand command = new OdbcCommand();
                OdbcTransaction transaction = null;

                // Set the Connection to the new OdbcConnection.
                command.Connection = connection;

                try
                {
                    connection.Open();
                    // Start a local transaction
                    transaction = connection.BeginTransaction();
                    // Assign transaction object for a pending local transaction.
                    command.Connection = connection;
                    command.Transaction = transaction;

                    foreach (string aux in call)
                    {// Execute the commands.
                        command.CommandText = aux;
                        command.ExecuteNonQuery();
                    }

                    // Commit the transaction.
                    transaction.Commit();
                    //Console.WriteLine("Both records are written to database.");
                }
                catch (Exception ex)
                {
                    // Attempt to roll back the transaction.
                    transaction.Rollback();
                    throw ex;
                }
                // The connection is automatically closed when the
                // code exits the using block.
            }

        }

        private static string leerApp()
        {
            string wResult = Decrypt(System.Configuration.ConfigurationSettings.AppSettings["ConnectionISeries"].ToString(), "mlmweb");
            //string wUsuario = HttpContext.Current.Session["NOMBRE_USUARIO"].ToString();
            //string wPass = HttpContext.Current.Session["PASSWORD"].ToString();
            string wUsuario = "";
            string wPass = "";
            return wResult;
            //Dsn=PUBLICIDAD;uid=isnet;pwd=isnet
        }



        public static SqlConnection GetConnection()
        {
            //usa el string de conexión tomado de la configuración
            SqlConnection connection = null;
            //#line 200
            try
            {
                //string wPassw = "SiSeM"; //crear una clave de encriptación por default.

                //string strConn = MycConvert.Decrypt(ConfigurationSettings.AppSettings["ConnString"], wPassw);

                string wPassw = "mlmweb";// "%#0" + "4?0" + "1*19" + "63!"; //crear una clave de encriptación por default.

                string strConn = Decrypt(System.Configuration.ConfigurationSettings.AppSettings["ConnString"], wPassw);

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


        public static DataSet ExecuteSQLquery(string pSQL)
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


        public static DataSet ExecuteSQLquery(string pSQL, string row, string cant)
        {
            DataSet dsResult = new DataSet(); //DataSet con resultado
            try
            {
                SqlCommand cmd = new SqlCommand(pSQL);

                SqlConnection conn = GetConnection();
                cmd.Connection = conn;
                //cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                dsResult.Tables.Add(dt);
                da.Fill(int.Parse(row), int.Parse(cant), dsResult.Tables[0]);

                if (conn != null)
                    cmd.Connection.Dispose();
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("[SP: {0}] {1}", pSQL, e.Message));
            }
            return dsResult;
        }


        public static DataSet EjecutaStored(string stored, string row, string cant)
        {
            try
            {
                DataSet dsResult = new DataSet();
                //Crea Conexion
                OdbcConnection conn = new OdbcConnection(leerApp());
                OdbcCommand cmd = new OdbcCommand();
                //Inicializa Comando
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandText = stored;
                //LLena dataset
                OdbcDataAdapter da = new OdbcDataAdapter(cmd);
                DataTable dt = new DataTable();
                dsResult.Tables.Add(dt);
                da.Fill(int.Parse(row), int.Parse(cant), dsResult.Tables[0]);
                //Desconecta
                conn.Close();
                return dsResult;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public static DataSet EjecutaStored(string stored)
        {
            try
            {
                DataSet dsResult = new DataSet();
                //Crea Conexion
                OdbcConnection conn = new OdbcConnection(leerApp());
                OdbcCommand cmd = new OdbcCommand();
                //Inicializa Comando
                cmd.Connection = conn;
                conn.Open();

                cmd.CommandText = stored;
                cmd.CommandTimeout = 0;
                //LLena dataset
                OdbcDataAdapter da = new OdbcDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dsResult);
                //Desconecta
                conn.Close();
                return dsResult;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public static DataSet EjecutaStored(string stored, string connectionString)
        {
            //Crea Conexion
            OdbcConnection conn = new OdbcConnection(connectionString);
            OdbcCommand cmd = new OdbcCommand();
            //Inicializa Comando
            cmd.Connection = conn;
            conn.Open();
            cmd.CommandText = stored;
            //LLena dataset
            OdbcDataAdapter da = new OdbcDataAdapter(cmd);
            DataSet dsResult = new DataSet();
            da.Fill(dsResult);
            //Desconecta
            conn.Close();
            return dsResult;
        }


        #region Encriptación

        //		public class EncDec 
        //		{
        // Encrypt a byte array into a byte array using a key and an IV 
        public static byte[] Encrypt(byte[] clearData, byte[] Key, byte[] IV)
        {
            // Create a MemoryStream to accept the encrypted bytes 
            MemoryStream ms = new MemoryStream();

            // Create a symmetric algorithm. 
            // We are going to use Rijndael because it is strong and
            // available on all platforms. 
            // You can use other algorithms, to do so substitute the
            // next line with something like 
            //      TripleDES alg = TripleDES.Create(); 
            Rijndael alg = Rijndael.Create();

            // Now set the key and the IV. 
            // We need the IV (Initialization Vector) because
            // the algorithm is operating in its default 
            // mode called CBC (Cipher Block Chaining).
            // The IV is XORed with the first block (8 byte) 
            // of the data before it is encrypted, and then each
            // encrypted block is XORed with the 
            // following block of plaintext.
            // This is done to make encryption more secure. 

            // There is also a mode called ECB which does not need an IV,
            // but it is much less secure. 
            alg.Key = Key;
            alg.IV = IV;

            // Create a CryptoStream through which we are going to be
            // pumping our data. 
            // CryptoStreamMode.Write means that we are going to be
            // writing data to the stream and the output will be written
            // in the MemoryStream we have provided. 
            CryptoStream cs = new CryptoStream(ms,
                                               alg.CreateEncryptor(), CryptoStreamMode.Write);

            // Write the data and make it do the encryption 
            cs.Write(clearData, 0, clearData.Length);

            // Close the crypto stream (or do FlushFinalBlock). 
            // This will tell it that we have done our encryption and
            // there is no more data coming in, 
            // and it is now a good time to apply the padding and
            // finalize the encryption process. 
            cs.Close();

            // Now get the encrypted data from the MemoryStream.
            // Some people make a mistake of using GetBuffer() here,
            // which is not the right way. 
            byte[] encryptedData = ms.ToArray();

            return encryptedData;
        }


        // Encrypt a string into a string using a password 
        //    Uses Encrypt(byte[], byte[], byte[]) 

        public static string Encrypt(string clearText, string Password)
        {
            // First we need to turn the input string into a byte array. 
            byte[] clearBytes =
                System.Text.Encoding.Unicode.GetBytes(clearText);

            // Then, we need to turn the password into Key and IV 
            // We are using salt to make it harder to guess our key
            // using a dictionary attack - 
            // trying to guess a password by enumerating all possible words. 
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
                                                              new byte[]
                                                                  {
                                                                      0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d,
                                                                      0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                                                                  });

            // Now get the key/IV and do the encryption using the
            // function that accepts byte arrays. 
            // Using PasswordDeriveBytes object we are first getting
            // 32 bytes for the Key 
            // (the default Rijndael key length is 256bit = 32bytes)
            // and then 16 bytes for the IV. 
            // IV should always be the block size, which is by default
            // 16 bytes (128 bit) for Rijndael. 
            // If you are using DES/TripleDES/RC2 the block size is
            // 8 bytes and so should be the IV size. 
            // You can also read KeySize/BlockSize properties off
            // the algorithm to find out the sizes. 
            byte[] encryptedData = Encrypt(clearBytes,
                                           pdb.GetBytes(32), pdb.GetBytes(16));

            // Now we need to turn the resulting byte array into a string. 
            // A common mistake would be to use an Encoding class for that.
            //It does not work because not all byte values can be
            // represented by characters. 
            // We are going to be using Base64 encoding that is designed
            //exactly for what we are trying to do. 
            return Convert.ToBase64String(encryptedData);

        }


        // Encrypt bytes into bytes using a password 
        //    Uses Encrypt(byte[], byte[], byte[]) 

        public static byte[] Encrypt(byte[] clearData, string Password)
        {
            // We need to turn the password into Key and IV. 
            // We are using salt to make it harder to guess our key
            // using a dictionary attack - 
            // trying to guess a password by enumerating all possible words. 
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
                                                              new byte[]
                                                                  {
                                                                      0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d,
                                                                      0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                                                                  });

            // Now get the key/IV and do the encryption using the function
            // that accepts byte arrays. 
            // Using PasswordDeriveBytes object we are first getting
            // 32 bytes for the Key 
            // (the default Rijndael key length is 256bit = 32bytes)
            // and then 16 bytes for the IV. 
            // IV should always be the block size, which is by default
            // 16 bytes (128 bit) for Rijndael. 
            // If you are using DES/TripleDES/RC2 the block size is 8
            // bytes and so should be the IV size. 
            // You can also read KeySize/BlockSize properties off the
            // algorithm to find out the sizes. 
            return Encrypt(clearData, pdb.GetBytes(32), pdb.GetBytes(16));

        }


        // Encrypt a file into another file using a password 
        public static void Encrypt(string fileIn,
                                   string fileOut, string Password)
        {
            // First we are going to open the file streams 
            FileStream fsIn = new FileStream(fileIn,
                                             FileMode.Open, FileAccess.Read);
            FileStream fsOut = new FileStream(fileOut,
                                              FileMode.OpenOrCreate, FileAccess.Write);

            // Then we are going to derive a Key and an IV from the
            // Password and create an algorithm 
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
                                                              new byte[]
                                                                  {
                                                                      0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d,
                                                                      0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                                                                  });

            Rijndael alg = Rijndael.Create();
            alg.Key = pdb.GetBytes(32);
            alg.IV = pdb.GetBytes(16);

            // Now create a crypto stream through which we are going
            // to be pumping data. 
            // Our fileOut is going to be receiving the encrypted bytes. 
            CryptoStream cs = new CryptoStream(fsOut,
                                               alg.CreateEncryptor(), CryptoStreamMode.Write);

            // Now will will initialize a buffer and will be processing
            // the input file in chunks. 
            // This is done to avoid reading the whole file (which can
            // be huge) into memory. 
            int bufferLen = 4096;
            byte[] buffer = new byte[bufferLen];
            int bytesRead;

            do
            {
                // read a chunk of data from the input file 
                bytesRead = fsIn.Read(buffer, 0, bufferLen);

                // encrypt it 
                cs.Write(buffer, 0, bytesRead);
            } while (bytesRead != 0);

            // close everything 

            // this will also close the unrelying fsOut stream
            cs.Close();
            fsIn.Close();
        }


        // Decrypt a byte array into a byte array using a key and an IV 
        public static byte[] Decrypt(byte[] cipherData,
                                     byte[] Key, byte[] IV)
        {
            // Create a MemoryStream that is going to accept the
            // decrypted bytes 
            MemoryStream ms = new MemoryStream();

            // Create a symmetric algorithm. 
            // We are going to use Rijndael because it is strong and
            // available on all platforms. 
            // You can use other algorithms, to do so substitute the next
            // line with something like 
            //     TripleDES alg = TripleDES.Create(); 
            Rijndael alg = Rijndael.Create();

            // Now set the key and the IV. 
            // We need the IV (Initialization Vector) because the algorithm
            // is operating in its default 
            // mode called CBC (Cipher Block Chaining). The IV is XORed with
            // the first block (8 byte) 
            // of the data after it is decrypted, and then each decrypted
            // block is XORed with the previous 
            // cipher block. This is done to make encryption more secure. 
            // There is also a mode called ECB which does not need an IV,
            // but it is much less secure. 
            alg.Key = Key;
            alg.IV = IV;

            // Create a CryptoStream through which we are going to be
            // pumping our data. 
            // CryptoStreamMode.Write means that we are going to be
            // writing data to the stream 
            // and the output will be written in the MemoryStream
            // we have provided. 
            CryptoStream cs = new CryptoStream(ms,
                                               alg.CreateDecryptor(), CryptoStreamMode.Write);

            // Write the data and make it do the decryption 
            cs.Write(cipherData, 0, cipherData.Length);

            // Close the crypto stream (or do FlushFinalBlock). 
            // This will tell it that we have done our decryption
            // and there is no more data coming in, 
            // and it is now a good time to remove the padding
            // and finalize the decryption process. 
            cs.Close();

            // Now get the decrypted data from the MemoryStream. 
            // Some people make a mistake of using GetBuffer() here,
            // which is not the right way. 
            byte[] decryptedData = ms.ToArray();

            return decryptedData;
        }


        // Decrypt a string into a string using a password 
        //    Uses Decrypt(byte[], byte[], byte[]) 

        public static string Decrypt(string cipherText, string Password)
        {
            // First we need to turn the input string into a byte array. 
            // We presume that Base64 encoding was used 
            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            // Then, we need to turn the password into Key and IV 
            // We are using salt to make it harder to guess our key
            // using a dictionary attack - 
            // trying to guess a password by enumerating all possible words. 
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
                                                              new byte[]
                                                                  {
                                                                      0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65,
                                                                      0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                                                                  });

            // Now get the key/IV and do the decryption using
            // the function that accepts byte arrays. 
            // Using PasswordDeriveBytes object we are first
            // getting 32 bytes for the Key 
            // (the default Rijndael key length is 256bit = 32bytes)
            // and then 16 bytes for the IV. 
            // IV should always be the block size, which is by
            // default 16 bytes (128 bit) for Rijndael. 
            // If you are using DES/TripleDES/RC2 the block size is
            // 8 bytes and so should be the IV size. 
            // You can also read KeySize/BlockSize properties off
            // the algorithm to find out the sizes. 
            byte[] decryptedData = Decrypt(cipherBytes,
                                           pdb.GetBytes(32), pdb.GetBytes(16));

            // Now we need to turn the resulting byte array into a string. 
            // A common mistake would be to use an Encoding class for that.
            // It does not work 
            // because not all byte values can be represented by characters. 
            // We are going to be using Base64 encoding that is 
            // designed exactly for what we are trying to do. 
            return System.Text.Encoding.Unicode.GetString(decryptedData);
        }


        // Decrypt bytes into bytes using a password 
        //    Uses Decrypt(byte[], byte[], byte[]) 

        public static byte[] Decrypt(byte[] cipherData, string Password)
        {
            // We need to turn the password into Key and IV. 
            // We are using salt to make it harder to guess our key
            // using a dictionary attack - 
            // trying to guess a password by enumerating all possible words. 
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
                                                              new byte[]
                                                                  {
                                                                      0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d,
                                                                      0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                                                                  });

            // Now get the key/IV and do the Decryption using the 
            //function that accepts byte arrays. 
            // Using PasswordDeriveBytes object we are first getting
            // 32 bytes for the Key 
            // (the default Rijndael key length is 256bit = 32bytes)
            // and then 16 bytes for the IV. 
            // IV should always be the block size, which is by default
            // 16 bytes (128 bit) for Rijndael. 
            // If you are using DES/TripleDES/RC2 the block size is
            // 8 bytes and so should be the IV size. 

            // You can also read KeySize/BlockSize properties off the
            // algorithm to find out the sizes. 
            return Decrypt(cipherData, pdb.GetBytes(32), pdb.GetBytes(16));
        }


        // Decrypt a file into another file using a password 
        public static void Decrypt(string fileIn,
                                   string fileOut, string Password)
        {
            // First we are going to open the file streams 
            FileStream fsIn = new FileStream(fileIn,
                                             FileMode.Open, FileAccess.Read);
            FileStream fsOut = new FileStream(fileOut,
                                              FileMode.OpenOrCreate, FileAccess.Write);

            // Then we are going to derive a Key and an IV from
            // the Password and create an algorithm 
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
                                                              new byte[]
                                                                  {
                                                                      0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d,
                                                                      0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                                                                  });
            Rijndael alg = Rijndael.Create();

            alg.Key = pdb.GetBytes(32);
            alg.IV = pdb.GetBytes(16);

            // Now create a crypto stream through which we are going
            // to be pumping data. 
            // Our fileOut is going to be receiving the Decrypted bytes. 
            CryptoStream cs = new CryptoStream(fsOut,
                                               alg.CreateDecryptor(), CryptoStreamMode.Write);

            // Now will will initialize a buffer and will be 
            // processing the input file in chunks. 
            // This is done to avoid reading the whole file (which can be
            // huge) into memory. 
            int bufferLen = 4096;
            byte[] buffer = new byte[bufferLen];
            int bytesRead;

            do
            {
                // read a chunk of data from the input file 
                bytesRead = fsIn.Read(buffer, 0, bufferLen);

                // Decrypt it 
                cs.Write(buffer, 0, bytesRead);

            } while (bytesRead != 0);

            // close everything 
            cs.Close(); // this will also close the unrelying fsOut stream 
            fsIn.Close();
        }

        //		}

        #endregion
    }
}

