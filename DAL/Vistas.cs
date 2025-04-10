using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Vistas
    {

        public DataTable getestadisticasVentas(string idcliente)
        {
            try
            {


                string aux = SQL.Call + "estadisticasVentas ";
            
                aux += idcliente;

               return SQL.EjecutaStored(aux).Tables[0];




            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }




    }
}
