using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Security.Cryptography;


namespace DAL
{
    public class Estados
    {

        private string _ID;
        private string _estado;

        private string _DESCRIPCION;

        public string ID // DataType [NUMERIC ],Nullable [NO]
        {
            get { return string.IsNullOrEmpty(_ID) ? SQL.numericNULL : _ID.Replace(',', '.'); }
            set { _ID = value; }
        }





        public string estado // DataType [CHARACTER VARYING],Nullable [YES]
        {
            get { return string.IsNullOrEmpty(_estado) ? SQL.numericNULL : _ID.Replace(',', '.'); }
            set { _estado = value; }
        }



        public string DESCRIPCION // DataType [CHARACTER VARYING],Nullable [YES]
        {
            get { return string.IsNullOrEmpty(_DESCRIPCION) ? SQL.numericNULL : _ID.Replace(',', '.'); }
            set { _DESCRIPCION = value; }
        }




        public DataTable GETESTADOS()
        {
            try
            {


                string aux = SQL.Call + "estados_QRY ";
                aux += ID+ ',';
             
                aux += estado;



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


        public DataTable GETUNIMED()
        {
            try
            {


                string aux = SQL.Call + "UNIMED_QRY ";
                aux += ID + ',';

                aux += DESCRIPCION;



                return SQL.EjecutaStored(aux).Tables[0];




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

