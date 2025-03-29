using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UnidadesMedida
    {
        private string _ID;
    private string _codigo;

    private string _DESCRIPCION;

    public string ID // DataType [NUMERIC ],Nullable [NO]
    {
        get { return string.IsNullOrEmpty(_ID) ? SQL.numericNULL : _ID.Replace(',', '.'); }
        set { _ID = value; }
    }





    public string codigo // DataType [CHARACTER VARYING],Nullable [YES]
        {
        get { return string.IsNullOrEmpty(_codigo) ? SQL.numericNULL : _ID.Replace(',', '.'); }
        set { _codigo = value; }
    }



    public string DESCRIPCION // DataType [CHARACTER VARYING],Nullable [YES]
    {
        get { return string.IsNullOrEmpty(_DESCRIPCION) ? SQL.numericNULL : _ID.Replace(',', '.'); }
        set { _DESCRIPCION = value; }
    }




    


    public DataTable GETUNIMED()
    {
        try
        {


            string aux = SQL.Call + "UNIdadesMEDidas_QRY ";
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

