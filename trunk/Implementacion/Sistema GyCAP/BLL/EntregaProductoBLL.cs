using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class EntregaProductoBLL
    {
        //Metodo que recibe el codigo del cliente
        public static void ObtenerEntregas(int codigoCliente, DataTable dtEntregas)
        {
            DAL.EntregaProductoDAL.ObtenerEntregas(codigoCliente, dtEntregas);
        }

        public static void ObtenerEntregas(DateTime fechaEntrega, DataTable dtEntregas)
        {
            DAL.EntregaProductoDAL.ObtenerEntregas(fechaEntrega, dtEntregas);
        }

        public static void ObtenerEntregas(int codigoCliente, DateTime fechaEntrega, DataTable dtEntregas)
        {
            DAL.EntregaProductoDAL.ObtenerEntregas(codigoCliente,fechaEntrega, dtEntregas);
        }

        public static void ObtenerDetalleEntrega(int idEntrega, DataTable dtEntregas)
        {
            DAL.EntregaProductoDAL.ObtenerDetalleEntrega(idEntrega, dtEntregas);
        }

        //Metodo que guarda la entrega
        public static void GuardarEntrega(Entidades.EntregaProducto entrega, Data.dsEntregaProducto dsEntregaProducto)
        {
            DAL.EntregaProductoDAL.GuardarEntrega(entrega, dsEntregaProducto);
        }
        //Metodo que guarda la entrega modificada
        public static void GuardarEntregaModificada(Entidades.EntregaProducto entrega, Data.dsEntregaProducto dsEntregaProducto)
        {
            DAL.EntregaProductoDAL.GuardarEntregaModificada(entrega, dsEntregaProducto);
        }
        //Metodo que elimina la entrega de la BD
        public static void EliminarEntrega(int codigoEntrega)
        {
            DAL.EntregaProductoDAL.EliminarEntrega(codigoEntrega);
        }


    }
}
