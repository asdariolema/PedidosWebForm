using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TBL_TIPO_COMPROBANTE

    {
        private string _CUIT;
      
        private string _Neto;

        private string _Letra;

        private string _ID_TIPO_COMPROBANTE;
            private string _DS_TIP_CODIGO;

            private string _DS_TIP_DESCRIPCION;

            private string _NU_TIP_CODIGO_FE;
            private string _CD_TIP_STATUS;



        public string Neto // DataType [NUMERIC ],Nullable [NO]
        {
            get { return string.IsNullOrEmpty(_Neto) ? SQL.numericNULL : _Neto.Replace(',', '.'); }
            set { _Neto = value; }
        }

        public string CUIT // DataType [NUMERIC ],Nullable [NO]
        {
            get { return string.IsNullOrEmpty(_CUIT) ? SQL.numericNULL : _CUIT.Replace(',', '.'); }
            set { _CUIT = value; }
        }

       
    









        public string ID_TIPO_COMPROBANTE // DataType [NUMERIC ],Nullable [NO]
        {
        get { return string.IsNullOrEmpty(_ID_TIPO_COMPROBANTE) ? SQL.numericNULL : _ID_TIPO_COMPROBANTE.Replace(',', '.'); }
        set { _ID_TIPO_COMPROBANTE = value; }
    }





    public string DS_TIP_CODIGO // DataType [CHARACTER VARYING],Nullable [YES]
        {
        get { return string.IsNullOrEmpty(_DS_TIP_CODIGO) ? SQL.numericNULL : _DS_TIP_CODIGO.Replace(',', '.'); }
        set { _DS_TIP_CODIGO = value; }
    }



    public string DS_TIP_DESCRIPCION // DataType [CHARACTER VARYING],Nullable [YES]
        {
        get { return string.IsNullOrEmpty(_DS_TIP_DESCRIPCION) ? SQL.numericNULL : _DS_TIP_DESCRIPCION.Replace(',', '.'); }
        set { _DS_TIP_DESCRIPCION = value; }
    }


        public string NU_TIP_CODIGO_FE // DataType [CHARACTER VARYING],Nullable [YES]
        {
            get { return string.IsNullOrEmpty(_NU_TIP_CODIGO_FE) ? SQL.numericNULL : _NU_TIP_CODIGO_FE.Replace(',', '.'); }
            set { _NU_TIP_CODIGO_FE = value; }
        }

        public string CD_TIP_STATUS // DataType [CHARACTER VARYING],Nullable [YES]
        {
            get { return string.IsNullOrEmpty(_CD_TIP_STATUS) ? SQL.numericNULL : _CD_TIP_STATUS.Replace(',', '.'); }
            set { _CD_TIP_STATUS = value; }
        }

        public string Letra // DataType [CHARACTER VARYING],Nullable [YES]
        {
            get { return string.IsNullOrEmpty(_Letra) ? SQL.numericNULL : _Letra.Replace(',', '.'); }
            set { _Letra = value; }
        }













        public DataTable GETTipos()
    {
        try
        {
     

       

        string aux = SQL.Call + "tipos_Documentos_QRY ";
                aux += (ID_TIPO_COMPROBANTE) + ',';
                aux += (DS_TIP_CODIGO) + ',';
                aux += (DS_TIP_DESCRIPCION) + ',';
                aux += (NU_TIP_CODIGO_FE) + ',';
                aux += (CD_TIP_STATUS) + ',';
                aux += (Letra)  ;

          
            return SQL.EjecutaStored(aux).Tables[0];




            //string aux = SQL.Call + "PEDIDOSCOTIZACIONES_QRY ";
            //aux += string.IsNullOrEmpty(pidCliente) ? SQL.numericNULL : pidCliente; aux += ',';
            //aux += Comillas(string.IsNullOrEmpty(pnombreCliente) ? SQL.numericNULL : pnombreCliente);
            //return SQL.EjecutaStored(aux).Tables[0];
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }





        public DataTable GETIMpuestosYtotal()
        {
            try
            {




                string aux = SQL.Call + "CALCULAIMPUESTOS ";
                aux += Comillas(CUIT) + ',';
                aux += Comillas(Letra) + ',';
             
                aux += (Neto);


                return SQL.EjecutaStored(aux).Tables[0];




                //string aux = SQL.Call + "PEDIDOSCOTIZACIONES_QRY ";
                //aux += string.IsNullOrEmpty(pidCliente) ? SQL.numericNULL : pidCliente; aux += ',';
                //aux += Comillas(string.IsNullOrEmpty(pnombreCliente) ? SQL.numericNULL : pnombreCliente);
                //return SQL.EjecutaStored(aux).Tables[0];
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        private string Comillas(string valor)
    {
        return "'" + valor + "'";
    }
}
}


