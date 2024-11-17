using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Security.Cryptography;

namespace DAL
{
 public    class Articulo
    {
    
        private string _ID_ARTICULO;
        private string _ART_CODIGO;
        private string _ART_DESCRIPCION;
        private string _ART_ID_CATEGORIA;
        private string _ART_ID_SUB_CATEGORIA;

        public string ID_ARTICULO // DataType [NUMERIC ],Nullable [NO]
        {
            get { return string.IsNullOrEmpty(_ID_ARTICULO) ? SQL.numericNULL : _ID_ARTICULO.Replace(',', '.'); }
            set { _ID_ARTICULO = value; }
        }


        public string ART_CODIGO // DataType [NUMERIC ],Nullable [NO]
        {
            get { return string.IsNullOrEmpty(_ART_CODIGO) ? SQL.numericNULL : _ART_CODIGO.Replace(',', '.'); }

            set { _ART_CODIGO = value; }
        }

        public string ART_DESCRIPCION // DataType [CHARACTER VARYING],Nullable [YES]
        {
            get { return string.IsNullOrEmpty(_ART_DESCRIPCION) ? SQL.nvarcharNULL_SQL : _ART_DESCRIPCION.Replace(',', '.'); }
        
            set { _ART_DESCRIPCION = value; }
        }


              public string ART_ID_CATEGORIA // DataType [NUMERIC ],Nullable [NO]
        {
            get { return string.IsNullOrEmpty(_ART_ID_CATEGORIA) ? SQL.numericNULL : _ART_ID_CATEGORIA.Replace(',', '.'); }
            set { _ART_ID_CATEGORIA = value; }
        }


        public string ART_ID_SUB_CATEGORIA // DataType [NUMERIC ],Nullable [NO]
        {
            get { return string.IsNullOrEmpty(_ART_ID_SUB_CATEGORIA) ? SQL.numericNULL : _ART_ID_SUB_CATEGORIA.Replace(',', '.'); }
            set { _ART_ID_SUB_CATEGORIA = value; }

        }

        public DataTable GETArticulo()
        {
            try
            {


                string aux = SQL.Call + "ARTICULOWEB_QRY ";
                aux += ID_ARTICULO + ',';
                aux += ART_CODIGO + ',';
                aux += ART_DESCRIPCION;



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





    }
}
