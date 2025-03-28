﻿using System;
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
        private string _DESC1;
        private string _DESC2;
        private string _MEDIDA;
        private string _PUNIT;
        private string _TASA;
        private string _PTOTAL;




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
        public string DESC1 // DataType [NUMERIC ],Nullable [YES]
        {
            get { return _DESC1 ?? SQL.characterNULL; }
            set { _DESC1 = value; }
        }
        public string DESC2 // DataType [NUMERIC ],Nullable [YES]
        {
            get { return _DESC2 ?? SQL.characterNULL; }
            set { _DESC2 = value; }
        }
        public string MEDIDA // DataType [NUMERIC ],Nullable [YES]
        {
            get { return _MEDIDA ?? SQL.characterNULL; }
            set { _MEDIDA = value; }
        }



        public string PUNIT // DataType [NUMERIC ],Nullable [YES]
        {
            get { return _PUNIT ?? SQL.characterNULL; }
            set { _PUNIT = value; }
        }

        public string TASA // DataType [NUMERIC ],Nullable [YES]
        {
            get { return _TASA ?? SQL.characterNULL; }
            set { _TASA = value; }
        }


        public string PTOTAL // DataType [NUMERIC ],Nullable [YES]
        {
            get { return _PTOTAL ?? SQL.characterNULL; }
            set { _PTOTAL = value; }
        }










        public DataTable CotizCont_INS()
        {
            try
            {


                string aux = SQL.Call + "CotizCont_INS ";
                aux += IDCOTIZ + ',';
                aux += ConvertirComasAPuntos(CANT) + ',';
                aux += Comillas(DESC1.Replace("'", "''")) + ',';
                aux += Comillas(DESC2.Replace("'", "''")) + ',';


                aux += Comillas(MEDIDA) + ',';
                aux += Comillas(PUNIT) + ',';
                aux += Comillas(TASA) + ',';
                aux += Comillas(PTOTAL);
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

