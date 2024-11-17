using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Security.Cryptography;


namespace DAL
{
    public class Cliente
            {

private string _ID_CLIENTE;
private string _ID_LOCALIDAD;
private string _ID_FORMA_PAGO;
private string _ID_SITUACION_IMPUESTO;
private string _ID_CATEGORIA;
private string _ID_vENDEDOR;
private string _NU_CLI_CODIGO;
private string _DS_CLI_RAZON_SOCIAL;
private string _DS_CLI_DIRECCION;
private string _DS_CLI_CP;
private string _DS_CLI_TELEFONO;
private string _DS_CLI_CUIT;
private string _DS_CLI_IB;
private string _NU_CLI_IND_PERC_IB;
private string _DS_CLI_IND_IB_PUBLICADO;
private string _DS_CLI_IND_IB_DESDE;
private string _DS_CLI_IND_IB_HASTA;
private string _NU_CLI_LIMITE_CREDITO;
private string _DS_CLI_OBSERVACIONES;
private string _DS_CLI_WEB;
private string _DS_CLI_EMAIL;
private string _NU_CLI_SALDO;
private string _CD_CLI_STATUS;
private string _smallint;


        public string ID_CLIENTE // DataType [NUMERIC ],Nullable [NO]
        {
            get { return string.IsNullOrEmpty(_ID_CLIENTE) ? SQL.numericNULL : _ID_CLIENTE.Replace(',', '.'); }
            set { _ID_CLIENTE = value; }
        }


        public string ID_LOCALIDAD // DataType [NUMERIC ],Nullable [NO]
        {
            get { return string.IsNullOrEmpty(_ID_LOCALIDAD) ? SQL.numericNULL : _ID_LOCALIDAD.Replace(',', '.'); }

            set { _ID_LOCALIDAD = value; }
        }

        public string ID_FORMA_PAGO // DataType [NUMERIC ],Nullable [NO]
        {
            get { return string.IsNullOrEmpty(_ID_FORMA_PAGO) ? SQL.numericNULL : _ID_FORMA_PAGO.Replace(',', '.'); }

            set { _ID_FORMA_PAGO = value; }
        }

        public string ID_CATEGORIA // DataType [NUMERIC ],Nullable [NO]
        {
            get { return string.IsNullOrEmpty(_ID_CATEGORIA) ? SQL.numericNULL : _ID_CATEGORIA.Replace(',', '.'); }

            set { _ID_CATEGORIA = value; }
        }

        public string ID_vENDEDOR // DataType [NUMERIC ],Nullable [NO]
        {
            get { return string.IsNullOrEmpty(_ID_vENDEDOR) ? SQL.numericNULL : _ID_vENDEDOR.Replace(',', '.'); }

            set { _ID_vENDEDOR = value; }
        }




        public string ID_SITUACION_IMPUESTO // DataType [NUMERIC ],Nullable [NO]
        {
            get { return string.IsNullOrEmpty(_ID_SITUACION_IMPUESTO) ? SQL.numericNULL : _ID_SITUACION_IMPUESTO.Replace(',', '.'); }

            set { _ID_SITUACION_IMPUESTO = value; }
        }


        
              public string NU_CLI_CODIGO // DataType [NUMERIC ],Nullable [NO]
        {
            get { return string.IsNullOrEmpty(_NU_CLI_CODIGO) ? SQL.numericNULL : _NU_CLI_CODIGO.Replace(',', '.'); }

            set { _NU_CLI_CODIGO = value; }
        }






        public string DS_CLI_RAZON_SOCIAL // DataType [CHARACTER VARYING],Nullable [YES]
        {
            get { return _DS_CLI_RAZON_SOCIAL ?? SQL.characterNULL; }
            set { _DS_CLI_RAZON_SOCIAL = value; }
        }



        public string DS_CLI_DIRECCION // DataType [NUMERIC ],Nullable [YES]
        {
            get { return _DS_CLI_DIRECCION ?? SQL.numericNULL; }
            set { _DS_CLI_DIRECCION = value; }
        }
        public string DS_CLI_CP // DataType [NUMERIC ],Nullable [YES]
        {
            get { return _DS_CLI_CP ?? SQL.characterNULL; }
            set { DS_CLI_CP = value; }
        }
        public string DS_CLI_TELEFONO // DataType [NUMERIC ],Nullable [YES]
        {
            get { return _DS_CLI_TELEFONO ?? SQL.characterNULL; }
            set { _DS_CLI_TELEFONO = value; }
        }

        public string DS_CLI_CUIT // DataType [NUMERIC ],Nullable [YES]
        {

            get { return string.IsNullOrEmpty(_DS_CLI_CUIT) ? SQL.numericNULL : _DS_CLI_CUIT.Replace(',', '.'); }
            set { _DS_CLI_CUIT = value; }
        }


                public string DS_CLI_EMAIL // DataType [NUMERIC ],Nullable [YES]
        {
            get { return _DS_CLI_EMAIL ?? SQL.characterNULL; }
            set { _DS_CLI_EMAIL = value; }
        }






        public DataTable GETcLIENTE()
        {
            try
            {


                string aux = SQL.Call + "CLIENTEWEB_QRY ";
                aux += ID_CLIENTE + ',';
                aux += ID_LOCALIDAD + ',';
                aux += NU_CLI_CODIGO + ',';
                aux +=Comillas ( DS_CLI_RAZON_SOCIAL) + ',';
                aux += (DS_CLI_DIRECCION) + ',';
                aux += DS_CLI_CUIT;
         


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


        //        public DataTable GETpedidosCotizaciones(string idCotizacion,
        //string pidCliente,
        //string pnombreCliente,
        //string pordenCompra,
        //string pdireccionEntrega,
        //string pcontactoObra,
        //string penviarSolamente,
        //string pavisar,
        //string pretirarPago,
        //string pobservacionesGral,
        //string purgencia ,
        //string pfechaUrgencia ,
        //string pfechaAlta  ,
        //string pfechaEntrega,
        //string pestado,
        //string pusuarioAlta,
        //string pfechaModif,
        //string pusuarioModif,
        //string pnotas,
        //string pretiraPedido,
        //string ppedidoConCambios,
        //string pimpresa,
        //string premitoOriginal,
        //string pcuit,
        //string plocalidad,
        //string ppiibb,
        //string pcondicionPago,
        //string pcondicionEntrega,
        //string pcotizacionAprobada)
        private string Comillas(string valor)
        {
            return "'" + valor + "'";
        }
    }
}

