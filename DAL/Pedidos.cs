using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Security.Cryptography;


namespace DAL
{
  public   class Pedidos
    {
        private string _idPedido;
        private string _idCliente;
        private string _nombreCliente;
        private string _ordenCompra;
        private string _direccionEntrega;
        private string _contactoObra; 
        private string _enviarSolamente;
        private string _avisar;
        private string _retirarPago;
        private string _observacionesGral;
        private string _urgencia;
        private string _fechaUrgencia;
        private string _fechaAlta;
        private string _fechaEntrega;
        private string _estado;
        private string _usuarioAlta;
        private string _fechaModif;
        private string _usuarioModif;
        private string _notas;
        private string _importetotal;
        private string _retiraPedido;
        private string _pedidoConCambios;
        private string _impresa;
        private string _remitoOriginal;
        private string _cuit;
        private string _localidad;
        private string _idCotizacion;
        private string _piibb;
        private string _condicionPago;
        private string _condicionEntrega;
        private string _impresalegal;
        private string _fechadesde;
        private string _fechahasta;
        private string _pbuscafechaalta;




        public string idPedido // DataType [NUMERIC ],Nullable [NO]
        {
            get { return string.IsNullOrEmpty(_idPedido) ? SQL.numericNULL : _idPedido.Replace(',', '.'); }
            set { _idPedido = value; }
        }
        

               public string pbuscafechaalta // DataType [NUMERIC ],Nullable [NO]
        {
            get { return string.IsNullOrEmpty(_pbuscafechaalta) ? SQL.numericCero : _pbuscafechaalta.Replace(',', '.'); }

            set { _pbuscafechaalta = value; }
        }

        public string idCliente // DataType [NUMERIC ],Nullable [NO]
        {
            get { return string.IsNullOrEmpty(_idCliente) ? SQL.numericNULL : _idCliente.Replace(',', '.'); }

            set { _idCliente = value; }
        }
        public string nombreCliente // DataType [CHARACTER VARYING],Nullable [YES]
        {
            get { return string.IsNullOrEmpty(_nombreCliente) ? SQL.numericNULL : _nombreCliente.Replace(',', '.'); }
           // get { return _nombreCliente ?? SQL.numericNULL; }
            set { _nombreCliente = value; }
        }
        public string ordenCompra // DataType [NUMERIC ],Nullable [YES]
        {

            get { return string.IsNullOrEmpty(_ordenCompra) ? SQL.numericNULL : _ordenCompra.Replace(',', '.'); }
       
            set { _ordenCompra = value; }
        }
        public string direccionEntrega // DataType [NUMERIC ],Nullable [YES]
        {
            get { return string.IsNullOrEmpty(_direccionEntrega) ? SQL.numericNULL : _direccionEntrega.Replace(',', '.'); }
          
            set { _direccionEntrega = value; }
        }
        public string contactoObra // DataType [NUMERIC ],Nullable [YES]
        {
            get { return string.IsNullOrEmpty(_contactoObra) ? SQL.numericNULL : _contactoObra.Replace(',', '.'); }
     
            set { _contactoObra = value; }
        }

        public string fechaEntrega // DataType [DATE    ],Nullable [YES]
        {
         
            get { return string.IsNullOrEmpty(_fechaEntrega) ? SQL.dateNULLsql : DateTime.Parse(_fechaEntrega).Date.ToString("dd-MM-yyyy"); }
            set { _fechaEntrega = value; }
        }

        public string fechadesde // DataType [DATE    ],Nullable [YES]
        {

            get { return string.IsNullOrEmpty(_fechadesde) ? SQL.dateNULLsql : DateTime.Parse(_fechadesde).Date.ToString("yyyy-MM-dd"); }
            set { _fechadesde = value; }
        }

        public string fechahasta // DataType [DATE    ],Nullable [YES]
        {

            get { return string.IsNullOrEmpty(_fechahasta) ? SQL.dateNULLsql : DateTime.Parse(_fechahasta).Date.ToString("yyyy-MM-dd"); }
            set { _fechahasta = value; }
        }





        public string estado // DataType [NUMERIC ],Nullable [NO]
        {
            get { return string.IsNullOrEmpty(_estado) ? SQL.numericNULL : _estado.Replace(',', '.'); }

            set { _estado = value; }
        }


        public string usuarioAlta // DataType [NUMERIC ],Nullable [YES]
        {
          
            get { return string.IsNullOrEmpty(_usuarioAlta) ? SQL.numericNULL : _usuarioAlta.Replace(',', '.'); }

            set { _usuarioAlta = value; }
        }











        public string enviarSolamente // DataType [NUMERIC ],Nullable [YES]
        {

            get { return string.IsNullOrEmpty(_enviarSolamente) ? SQL.numericNULL : _enviarSolamente.Replace(',', '.'); }

     
            set { _enviarSolamente = value; }
        }
        public string avisar // DataType [CHARACTER VARYING],Nullable [YES]
        {

            get { return string.IsNullOrEmpty(_avisar) ? SQL.numericNULL : _avisar.Replace(',', '.'); }
    
            set { _avisar = value; }
        }
        public string retirarPago // DataType [DATE    ],Nullable [YES]

        {
            get { return string.IsNullOrEmpty(_retirarPago) ? SQL.numericNULL : _retirarPago.Replace(',', '.'); }

         
            //get { return string.IsNullOrEmpty(_retirarPago) ? SQL.dateNULL : DateTime.Parse(_retirarPago).Date.ToString("yyyy-MM-dd"); }
            set { _retirarPago = value; }
        }
        public string observacionesGral // DataType [CHARACTER VARYING],Nullable [YES]
        {
            get { return string.IsNullOrEmpty(_observacionesGral) ? SQL.numericNULL : _observacionesGral.Replace(',', '.'); }
          
            set { _observacionesGral = value; }
        }
        public string urgencia // DataType [DATE    ],Nullable [YES]
        {
            get { return string.IsNullOrEmpty(_urgencia) ? SQL.numericNULL : _urgencia.Replace(',', '.'); }



       
            set { _urgencia = value; }
        }
        public string fechaUrgencia // DataType [CHARACTER VARYING],Nullable [YES]
        {
            get { return string.IsNullOrEmpty(_fechaUrgencia) ? SQL.dateNULLsql : DateTime.Parse(_fechaUrgencia).Date.ToString("dd-MM-yyyy"); }
            set { _fechaUrgencia = value; }
        }
        public string fechaAlta // DataType [DATE    ],Nullable [YES]
        {
            get { return string.IsNullOrEmpty(_fechaAlta) ? SQL.dateNULLsql : DateTime.Parse(_fechaAlta).Date.ToString("yyyy-MM-dd"); }
            set { _fechaAlta = value; }
        }

        public string fechaModif // DataType [CHARACTER VARYING],Nullable [YES]
        {
            get { return string.IsNullOrEmpty(_fechaModif) ? SQL.dateNULLsql : DateTime.Parse(_fechaModif).Date.ToString("dd-MM-yyyy"); }
            set { _fechaModif = value; }
        }


        public string usuarioModif // DataType [NUMERIC ],Nullable [NO]
        {
            get { return _usuarioModif ?? SQL.characterNULL; }

            set { _usuarioModif = value; }
        }
        public string notas // DataType [CHARACTER VARYING],Nullable [YES]
        {
            get { return string.IsNullOrEmpty(_notas) ? SQL.numericNULL : _notas.Replace(',', '.'); }
            set { _notas = value; }
        }

        public string importetotal // DataType [NUMERIC ],Nullable [YES]
        {
            get { return string.IsNullOrEmpty(_importetotal) ? SQL.numericNULL : _importetotal.Replace(',', '.'); }
            set { _importetotal = value; }
        }


        public string retiraPedido // DataType [NUMERIC ],Nullable [YES]
        {
            get { return string.IsNullOrEmpty(_retiraPedido) ? SQL.numericNULL : _retiraPedido.Replace(',', '.'); }
            set { _retiraPedido = value; }
        }
        public string pedidoConCambios // DataType [NUMERIC ],Nullable [YES]
        {
            get { return string.IsNullOrEmpty(_pedidoConCambios) ? SQL.numericNULL : _pedidoConCambios.Replace(',', '.'); }
            set { _pedidoConCambios = value; }
        }
        public string impresa // DataType [NUMERIC ],Nullable [YES]
        {
            get { return string.IsNullOrEmpty(_impresa) ? SQL.numericNULL : _impresa.Replace(',', '.'); }
            set { _impresa = value; }
        }

     

        public string remitoOriginal // DataType [NUMERIC ],Nullable [YES]
        {
            get { return _remitoOriginal ?? SQL.characterNULL; }
            set { _remitoOriginal = value; }
        }
        public string cuit // DataType [CHARACTER VARYING],Nullable [YES]
        {
            get { return _cuit ?? SQL.characterNULL; }
            set { _cuit = value; }
        }
        public string localidad // DataType [DATE    ],Nullable [YES]
        {
            get { return _localidad ?? SQL.characterNULL; }
            set { _localidad = value; }
        }

        public string idCotizacion // DataType [NUMERIC ],Nullable [YES]
        {
            get { return string.IsNullOrEmpty(_idCotizacion) ? SQL.numericNULL : _idCotizacion.Replace(',', '.'); }
            set { _idCotizacion = value; }
        }

        



        public string piibb // DataType [CHARACTER VARYING],Nullable [YES]
        {
            get { return string.IsNullOrEmpty(_piibb) ? SQL.numericNULL : _piibb.Replace(',', '.'); }

         
            set { _piibb = value; }
        }
        public string condicionPago // DataType [DATE    ],Nullable [YES]
        {
            get { return string.IsNullOrEmpty(_condicionPago) ? SQL.numericNULL : _condicionPago.Replace(',', '.'); }



         
            set { _condicionPago = value; }
        }
        public string condicionEntrega // DataType [CHARACTER VARYING],Nullable [YES]
        {
            get { return string.IsNullOrEmpty(_condicionEntrega) ? SQL.numericNULL : _condicionEntrega.Replace(',', '.'); }
    
            set { _condicionEntrega = value; }
        }


        public string impresalegal // DataType [CHARACTER VARYING],Nullable [YES]
        {
            get { return string.IsNullOrEmpty(_impresalegal) ? SQL.numericNULL : _impresalegal.Replace(',', '.'); }
            set { _impresalegal = value; }
        }











    

        public DataTable GETpedidosSimple()
        {
            try
            {


                string aux = SQL.Call + "PEDIDOSSimple_QRY ";
                aux += idPedido + ',';
                aux += idCliente + ',';
                aux += nombreCliente + ',';
                aux += ordenCompra + ',';
                aux += direccionEntrega + ',';
                aux += contactoObra + ',';
                aux += enviarSolamente + ',';
                aux += (avisar) + ',';
                aux += (retirarPago) + ',';
                aux += (observacionesGral) + ',';
                aux += (urgencia) + ',';
                aux += Comillas(fechaUrgencia) + ',';
                aux += Comillas(fechaAlta) + ',';
                aux += Comillas(fechaEntrega) + ',';
                aux += (estado) + ',';
                aux += (usuarioAlta) + ',';
                aux += (notas) + ',';
                aux += importetotal + ',';
                aux += (retiraPedido) + ',';
                aux += idCotizacion + ',';

                aux += (piibb) + ',';
                aux += (condicionPago) + ',';
                aux += (condicionEntrega) + ',';

                aux += Comillas(fechadesde) + ',';
                aux += Comillas(fechahasta);


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
        public DataTable GETpedidos()
        {
            try
            {

               
                string aux = SQL.Call + "pedidos_qry ";
                aux += idPedido + ',';
                aux += idCliente + ',';
                aux +=  nombreCliente + ',';
                aux += ordenCompra + ',';
                aux += direccionEntrega + ',';
                aux += contactoObra + ',';
                aux += enviarSolamente + ',';
                aux += (avisar) + ',';
                aux += (retirarPago) + ',';
                aux += (observacionesGral) + ',';
                aux += (urgencia) + ',';
                aux += Comillas(fechaUrgencia) + ',';
                aux += Comillas(fechaAlta) + ',';
                aux += Comillas(fechaEntrega) + ',';
                aux += (estado) + ',';
                aux += (usuarioAlta )+ ',';
                aux += (notas) + ',';
                aux += importetotal + ',';
                aux += (retiraPedido) + ',';
                aux += idCotizacion + ',';
                               
                aux += (piibb) + ',';
                aux += (condicionPago) + ',';
                aux += (condicionEntrega) + ',';
         
                aux += Comillas(fechadesde) + ',';
                aux += Comillas(fechahasta);
     

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


        public DataTable  InsertPedido()
        {
            try
            {


                string aux = SQL.Call + "PEDIDOS_INS ";
                aux += idCliente + ',';
                aux += Comillas(nombreCliente) + ',';
                aux += Comillas(ordenCompra) + ',';
                aux += Comillas(direccionEntrega) + ',';
                aux += Comillas(contactoObra) + ',';
                aux += Comillas(enviarSolamente) + ',';
                aux += Comillas(avisar) + ',';
                aux += Comillas(retirarPago) + ',';
                aux += Comillas(observacionesGral) + ',';
                aux += Comillas(urgencia) + ',';
                aux += Comillas(estado) + ',';
                aux += Comillas(usuarioAlta) + ',';
                aux += Comillas(notas) + ',';
                aux += importetotal + ',';
                aux += idCotizacion + ',';
                aux += Comillas(piibb) + ',';
                aux += Comillas(condicionPago) + ',';
                aux += Comillas(condicionEntrega) + ',';
                aux += Comillas(@fechaAlta);


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





        public DataTable UpadatePedido()
        {
            try
            {


                string aux = SQL.Call + "PEDIDOS_upd ";
                aux += idPedido + ',';
                aux += idCliente + ',';
                aux += Comillas(nombreCliente) + ',';
                aux += Comillas(ordenCompra) + ',';
                aux += Comillas(direccionEntrega) + ',';
                aux += Comillas(contactoObra) + ',';
                aux += Comillas(enviarSolamente) + ',';
                aux += Comillas(avisar) + ',';
                aux += Comillas(retirarPago) + ',';
                aux += Comillas(observacionesGral) + ',';
                aux += Comillas(urgencia) + ',';
                aux += Comillas(estado) + ',';
                aux += Comillas(usuarioAlta) + ',';
                aux += Comillas(notas) + ',';
                aux += importetotal + ',';
                aux += idCotizacion + ',';
                aux += Comillas(piibb) + ',';
                aux += Comillas(condicionPago) + ',';
                aux += Comillas(condicionEntrega) + ',';
                aux += Comillas(@fechaAlta);


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
