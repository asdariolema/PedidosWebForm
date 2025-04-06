using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CotizContenido
    {
 
        private string _IDCONTCOTIZ;
        private string _IDCOTIZ;
        private string _CANT;
        private string _DESCRIPCION;
        private string _ID_ESPESOR;
        private string _ESPESOR;
        private string _ID_ANCHO;
        private string _ANCHO;
        private string _ID_LARGO;
        private string _LARGO;


        private string _ID_UNIDAD;
        private string _UNIDAD;

        private string _ID_TASA;
        private string _TASA;
        private string _PRECIOUNITARIO;

        private string _PRECIOTOTAL;

       
            


















        public string IDCONTCOTIZ // DataType [NUMERIC ],Nullable [NO]
        {
            get { return string.IsNullOrEmpty(_IDCONTCOTIZ) ? SQL.numericNULL : _IDCONTCOTIZ.Replace(',', '.'); }
            set { _IDCONTCOTIZ = value; }
        }
        public string IDCOTIZ // DataType [NUMERIC ],Nullable [NO]
        {
            get { return string.IsNullOrEmpty(_IDCOTIZ) ? SQL.numericNULL : _IDCOTIZ.Replace(',', '.'); }
            set { _IDCOTIZ = value; }
        }

        public string CANT // DataType [CHARACTER VARYING],Nullable [YES]
        {
            get { return _CANT ?? SQL.characterNULL; }
            set { _CANT = value; }
        }
        public string DESCRIPCION // DataType [NUMERIC ],Nullable [YES]
        {
            get { return _DESCRIPCION ?? SQL.characterNULL; }
            set { _DESCRIPCION = value; }
        }
        public string ID_ESPESOR // DataType [NUMERIC ],Nullable [YES]
        {
            get { return _ID_ESPESOR ?? SQL.characterNULL; }
            set { _ID_ESPESOR = value; }
        }

        public string ESPESOR // DataType [NUMERIC ],Nullable [YES]
        {
            get { return _ESPESOR ?? SQL.characterNULL; }
            set { _ESPESOR = value; }
        }



        public string ID_ANCHO // DataType [NUMERIC ],Nullable [YES]
        {
            get { return _ID_ANCHO ?? SQL.characterNULL; }
            set { _ID_ANCHO = value; }
        }

        public string ANCHO // DataType [NUMERIC ],Nullable [YES]
        {
            get { return _ANCHO ?? SQL.characterNULL; }
            set { _ANCHO = value; }
        }


        public string ID_LARGO // DataType [NUMERIC ],Nullable [YES]
        {
            get { return _ID_LARGO ?? SQL.characterNULL; }
            set { _ID_LARGO = value; }
        }

        public string LARGO // DataType [NUMERIC ],Nullable [YES]
        {
            get { return _LARGO ?? SQL.characterNULL; }
            set { _LARGO = value; }
        }



        public string ID_UNIDAD // DataType [NUMERIC ],Nullable [YES]
        {
            get { return _ID_UNIDAD ?? SQL.characterNULL; }
            set { _ID_UNIDAD = value; }
        }

        public string UNIDAD // DataType [NUMERIC ],Nullable [YES]
        {
            get { return _UNIDAD ?? SQL.characterNULL; }
            set { _UNIDAD = value; }
        }


        public string PRECIOUNITARIO // DataType [NUMERIC ],Nullable [YES]
        {
            get { return _PRECIOUNITARIO ?? SQL.characterNULL; }
            set { _PRECIOUNITARIO = value; }
        }





        public string ID_TASA // DataType [NUMERIC ],Nullable [YES]
        {
            get { return _ID_TASA ?? SQL.characterNULL; }
            set { _ID_TASA = value; }
        }









        public string TASA // DataType [NUMERIC ],Nullable [YES]
        {
            get { return _TASA ?? SQL.characterNULL; }
            set { _TASA = value; }
        }


        public string PRECIOTOTAL // DataType [NUMERIC ],Nullable [YES]
        {
            get { return _PRECIOTOTAL ?? SQL.characterNULL; }
            set { _PRECIOTOTAL = value; }
        }










        public DataTable CotizCont_INS()
        {
            try
            {


                string aux = SQL.Call + "CotizCont_INS ";
                aux += IDCOTIZ + ',';
                aux += ConvertirComasAPuntos(CANT) + ',';
                aux += Comillas(DESCRIPCION.Replace("'", "''")) + ',';
                aux += Comillas(ID_ESPESOR) + ',';
                aux += Comillas(ESPESOR) + ',';
                aux += Comillas(ID_ANCHO) + ',';
                aux += Comillas(ANCHO) + ',';
                aux += Comillas(ID_LARGO) + ',';
                aux += Comillas(LARGO) + ',';
                aux += Comillas(ID_UNIDAD) + ',';
                aux += Comillas(UNIDAD) + ',';
                aux += Comillas(ID_TASA) + ',';
                aux += Comillas(TASA) + ',';
                aux += Comillas(PRECIOUNITARIO) + ',';
                aux += Comillas(PRECIOTOTAL);
                return SQL.EjecutaStored(aux).Tables[0];




            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public DataTable CotizCont_del()
        {
            try
            {


                string aux = SQL.Call + "CotizCont_del ";
                aux += IDCOTIZ;


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


        public string ConvertirComasAPuntos(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                // Reemplaza todas las comas por puntos
                return input.Replace(',', '.');
            }
            return input;
        }

    }

}

