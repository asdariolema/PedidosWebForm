using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Security.Cryptography;


namespace DAL
{
    public class FormCondicionesEntregaaDePago
    {


        private string _ID_FORMA_PAGO;

        private string _DS_FOR_DESCRIPCION;

        private string _NU_FOR_DIAS;

        private string _CD_FOR_STATUS;


        private string _ID_TIPO_FORMA_PAGO;

        private string _CD_FOR_DA_CAMBIO;

        public string CD_FOR_STATUS // DataType [NUMERIC ],Nullable [NO]
        {
            get { return string.IsNullOrEmpty(_CD_FOR_STATUS) ? SQL.numericNULL : _CD_FOR_STATUS.Replace(',', '.'); }
            set { _CD_FOR_STATUS = value; }
        }





        public string DS_FOR_DESCRIPCION // DataType [CHARACTER VARYING],Nullable [YES]
        {
            get { return string.IsNullOrEmpty(_DS_FOR_DESCRIPCION) ? SQL.numericNULL : _DS_FOR_DESCRIPCION.Replace(',', '.'); }
            set { _DS_FOR_DESCRIPCION = value; }
        }



        public string NU_FOR_DIAS // DataType [CHARACTER VARYING],Nullable [YES]
        {

            get { return string.IsNullOrEmpty(_NU_FOR_DIAS) ? SQL.numericNULL : _NU_FOR_DIAS.Replace(',', '.'); }

            set { _NU_FOR_DIAS = value; }
        }






        public string ID_FORMA_PAGO // DataType [NUMERIC ],Nullable [NO]
        {
            get { return string.IsNullOrEmpty(_ID_FORMA_PAGO) ? SQL.numericNULL : _ID_FORMA_PAGO.Replace(',', '.'); }
            set { _ID_FORMA_PAGO = value; }
        }





        public string ID_TIPO_FORMA_PAGO // DataType [CHARACTER VARYING],Nullable [YES]
        {
            get { return string.IsNullOrEmpty(_ID_TIPO_FORMA_PAGO) ? SQL.numericNULL : _ID_TIPO_FORMA_PAGO.Replace(',', '.'); }
            set { ID_TIPO_FORMA_PAGO = value; }
        }



        public string CD_FOR_DA_CAMBIO // DataType [CHARACTER VARYING],Nullable [YES]
        {
            get { return string.IsNullOrEmpty(_CD_FOR_DA_CAMBIO) ? SQL.numericNULL : _CD_FOR_DA_CAMBIO.Replace(',', '.'); }
            set { _CD_FOR_DA_CAMBIO = value; }
        }






        public DataTable GET()
        {
            try
            {



                string aux = SQL.Call + "formapago_QRY ";
                aux += (ID_FORMA_PAGO) + ',';
                aux += (DS_FOR_DESCRIPCION) + ',';
                aux += (NU_FOR_DIAS) + ',';
                aux += (CD_FOR_STATUS) + ',';
                aux += (ID_TIPO_FORMA_PAGO) + ',';
                aux += (CD_FOR_DA_CAMBIO);



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


