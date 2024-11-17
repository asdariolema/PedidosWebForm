using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Security.Cryptography;


namespace DAL
{

    public partial class CMUSER
    {
        #region Variables Privadas
        private string _CantPorPag;
        private string _IniPag;
        private string _IDUSUARIO;
        private string _USRDESCRIP;
        private string _IDPERFIL;
        private string _IDPERSONA;
        private string _IDOFICINA;
        private string _USRESTADO;
        private string _USRUSUALT;
        private string _USRFECALT;
        private string _USRUSUBAJ;
        private string _USRFECBAJ;
        private string _USRUSUMOD;
        private string _USRFECMOD;
        private string _USRPASS;
        #endregion

        #region Propiedades
        public string CantPorPag { get { return _CantPorPag; } set { _CantPorPag = value; } }
        public string IniPag { get { return _IniPag; } set { _IniPag = value; } }












        public string IDUSUARIO // DataType [NUMERIC ],Nullable [NO]
        {
            get { return string.IsNullOrEmpty(_IDUSUARIO) ? SQL.numericNULL : _IDUSUARIO.Replace(',', '.'); }

            set { _IDUSUARIO = value; }
        }
        public string USRDESCRIP // DataType [CHARACTER VARYING],Nullable [YES]
        {
            get { return _USRDESCRIP ?? SQL.characterNULL; }
            set { _USRDESCRIP = value; }
        }
        public string IDPERFIL // DataType [NUMERIC ],Nullable [YES]
        {
            get { return string.IsNullOrEmpty(_IDPERFIL) ? SQL.numericNULL : _IDPERFIL.Replace(',', '.'); }
            set { _IDPERFIL = value; }
        }
        public string IDPERSONA // DataType [NUMERIC ],Nullable [YES]
        {
            get { return string.IsNullOrEmpty(_IDPERSONA) ? SQL.numericNULL : _IDPERSONA.Replace(',', '.'); }
            set { _IDPERSONA = value; }
        }
        public string IDOFICINA // DataType [NUMERIC ],Nullable [YES]
        {
            get { return string.IsNullOrEmpty(_IDOFICINA) ? SQL.numericNULL : _IDOFICINA.Replace(',', '.'); }
            set { _IDOFICINA = value; }
        }
        public string USRESTADO // DataType [NUMERIC ],Nullable [YES]
        {
            get { return string.IsNullOrEmpty(_USRESTADO) ? SQL.numericNULL : _USRESTADO.Replace(',', '.'); }
            set { _USRESTADO = value; }
        }
        public string USRUSUALT // DataType [CHARACTER VARYING],Nullable [YES]
        {
            get { return _USRUSUALT ?? SQL.characterNULL; }
            set { _USRUSUALT = value; }
        }
        public string USRFECALT // DataType [DATE    ],Nullable [YES]
        {
            get { return string.IsNullOrEmpty(_USRFECALT) ? SQL.dateNULL : DateTime.Parse(_USRFECALT).Date.ToString("yyyy-MM-dd"); }
            set { _USRFECALT = value; }
        }
        public string USRUSUBAJ // DataType [CHARACTER VARYING],Nullable [YES]
        {
            get { return _USRUSUBAJ ?? SQL.characterNULL; }
            set { _USRUSUBAJ = value; }
        }
        public string USRFECBAJ // DataType [DATE    ],Nullable [YES]
        {
            get { return string.IsNullOrEmpty(_USRFECBAJ) ? SQL.dateNULL : DateTime.Parse(_USRFECBAJ).Date.ToString("yyyy-MM-dd"); }
            set { _USRFECBAJ = value; }
        }
        public string USRUSUMOD // DataType [CHARACTER VARYING],Nullable [YES]
        {
            get { return _USRUSUMOD ?? SQL.characterNULL; }
            set { _USRUSUMOD = value; }
        }
        public string USRFECMOD // DataType [DATE    ],Nullable [YES]
        {
            get { return string.IsNullOrEmpty(_USRFECMOD) ? SQL.dateNULL : DateTime.Parse(_USRFECMOD).Date.ToString("yyyy-MM-dd"); }
            set { _USRFECMOD = value; }
        }

        public string USRPASS // DataType [CHARACTER VARYING],Nullable [YES]
        {
            get { return _USRPASS ?? SQL.characterNULL; }
            set { _USRPASS = value; }
        }

        public const string COL_IDUSUARIO = "IDUSUARIO";
        public const string COL_USRDESCRIP = "USRDESCRIP";
        public const string COL_IDPERFIL = "IDPERFIL";
        public const string COL_IDPERSONA = "IDPERSONA";
        public const string COL_IDOFICINA = "IDOFICINA";
        public const string COL_USRESTADO = "USRESTADO";
        public const string COL_USRUSUALT = "USRUSUALT";
        public const string COL_USRFECALT = "USRFECALT";
        public const string COL_USRUSUBAJ = "USRUSUBAJ";
        public const string COL_USRFECBAJ = "USRFECBAJ";
        public const string COL_USRUSUMOD = "USRUSUMOD";
        public const string COL_USRFECMOD = "USRFECMOD";
        public const string COL_USRPASS = "USRPASS";
        #endregion

        #region Funciones públicas

        //QRY

        public DataTable GETRECURSOUSER(string PIDUSUARIO, string PIDSISTEMA)
        {
            try
            {
                string aux = SQL.Call + " GETRECURSOUSER ";
                aux += string.IsNullOrEmpty(PIDUSUARIO) ? SQL.numericNULL : PIDUSUARIO; aux += ',';
                aux += Comillas(string.IsNullOrEmpty(PIDSISTEMA) ? SQL.numericNULL : PIDSISTEMA);
                return SQL.EjecutaStored(aux).Tables[0];
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public DataTable getCMUSER()
        {
            try
            {


                string aux = SQL.Call + SP_GET_LISTA + ' ';
                aux += IDUSUARIO + ',';
                aux += Comillas(USRDESCRIP) + ',';
                aux += IDPERFIL + ',';
                aux += IDPERSONA + ',';
                aux += IDOFICINA + ',';
                aux += USRESTADO + ',';
                aux += Comillas(USRUSUALT) + ',';
                aux += Comillas(USRFECALT) + ',';
                aux += Comillas(USRUSUBAJ) + ',';
                aux += Comillas(USRFECBAJ) + ',';
                aux += Comillas(USRUSUMOD) + ',';
                aux += Comillas(USRFECMOD) + ',';
                aux += Comillas(USRPASS);
                return SQL.EjecutaStored(aux).Tables[0];


                //string aux = SP_GET_LISTA + " " +
                //"@ID=" +  ID + ',';
                //aux += "@NOMBRE=" + Comillas(NOMBRE) + ',';
                //aux += "@ROL="+ ROL + ',';
                //aux += "@CLAVE=" + Comillas(CLAVE) + ',';
                //aux += "@OTRO=" + Comillas(OTRO);

                //return SQL.EjecutaStored(aux, ConnString).Tables[0];
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }




        public DataTable getTipDocu()
        {
            try
            {

                string aux = "EXECUTE [MatanzaSQL].[dbo].[getTIPDOCU] ";

                aux += _IDUSUARIO != "" ? _IDUSUARIO : "null";
                return SQL.ExecuteSQLquery(aux).Tables[0];
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        //INS
        public DataTable insCMUSER()
        {
            try
            {
                string aux = SQL.Call + SP_INSERT + '(';
                aux += IDUSUARIO + ',';
                aux += Comillas(USRDESCRIP) + ',';
                aux += IDPERFIL + ',';
                aux += IDPERSONA + ',';
                aux += IDOFICINA + ',';
                aux += USRESTADO + ',';
                aux += Comillas(USRUSUALT) + ',';
                aux += Comillas(USRFECALT) + ',';
                aux += Comillas(USRUSUBAJ) + ',';
                aux += Comillas(USRFECBAJ) + ',';
                aux += Comillas(USRUSUMOD) + ',';
                aux += Comillas(USRFECMOD) + ')';
                return SQL.EjecutaStored(aux).Tables[0];
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //UPD
        public DataTable updCMUSER()
        {
            try
            {
                string aux = SQL.Call + SP_UPDATE + '(';
                aux += IDUSUARIO + ',';
                aux += Comillas(USRDESCRIP) + ',';
                aux += IDPERFIL + ',';
                aux += IDPERSONA + ',';
                aux += IDOFICINA + ',';
                aux += USRESTADO + ',';
                aux += Comillas(USRUSUALT) + ',';
                aux += Comillas(USRFECALT) + ',';
                aux += Comillas(USRUSUBAJ) + ',';
                aux += Comillas(USRFECBAJ) + ',';
                aux += Comillas(USRUSUMOD) + ',';
                aux += Comillas(USRFECMOD) + ')';
                return SQL.EjecutaStored(aux).Tables[0];
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //DEL
        public DataTable delCMUSER()
        {
            try
            {
                string aux = SQL.Call + SP_DELETE + '(';
                aux += IDUSUARIO + ')';
                return SQL.EjecutaStored(aux).Tables[0];
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        #endregion

        #region Funciones Privadas

        private string Comillas(string valor)
        {
            return "'" + valor + "'";
        }
        #endregion
    }
}
