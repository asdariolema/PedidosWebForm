using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace DAL
{

    public partial class CMUSER
    {
        #region Variables Privadas
        private const string SP_GET_LISTA = "SISTEMA_USUARIO_TABLA_QRY";
        private const string SP_INSERT = "CMUSER_INS";
        private const string SP_UPDATE = "CMUSER_INS";
        private const string SP_DELETE = "CMUSER_DEL";
        private string _ConnString;
        #endregion

        public string ConnString
        {
            get { return _ConnString ?? SQL.Decrypt(System.Configuration.ConfigurationSettings.AppSettings["ConnectionISeries"].ToString(), "mlmweb"); }
            set { _ConnString = value; }
        }

        public DataTable LOGIN(string USER, string PASSWORD)
        {
            try
            {
                string aux = "EXECUTE [MatanzaSQL].[dbo].[LOGIN] ";
                //string aux = "EXECUTE LOGIN ";
                aux += Comillas(string.IsNullOrEmpty(USER) ? SQL.characterNULL : USER); aux += ',';
                aux += Comillas(string.IsNullOrEmpty(PASSWORD) ? SQL.characterNULL : PASSWORD);
                return SQL.ExecuteSQLquery(aux).Tables[0];
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //public DataTable GETRECURSOUSER(string PIDUSUARIO, string PIDSISTEMA)
        //{
        //    try
        //    {
        //        string aux = SQL.Call + " GETRECURSOUSER ";
        //        aux += string.IsNullOrEmpty(PIDUSUARIO) ? SQL.numericNULL : PIDUSUARIO; aux += ',';
        //        aux += Comillas(string.IsNullOrEmpty(PIDSISTEMA) ? SQL.numericNULL : PIDSISTEMA);
        //        return SQL.EjecutaStored(aux).Tables[0];
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}

        public void CHGPWD(string USER, string CURPWD, string NEWPWD)
        {
            try
            {
                string aux = "EXECUTE [MatanzaSQL].[dbo].[CHGPWD] ";
                //string aux = "EXECUTE CHGPWD ";
                aux += Comillas(string.IsNullOrEmpty(USER) ? SQL.characterNULL : USER); aux += ',';
                aux += Comillas(string.IsNullOrEmpty(CURPWD) ? SQL.characterNULL : CURPWD); aux += ',';
                aux += Comillas(string.IsNullOrEmpty(NEWPWD) ? SQL.characterNULL : NEWPWD);
                SQL.ExecuteSQLquery(aux);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


       

    }
}
