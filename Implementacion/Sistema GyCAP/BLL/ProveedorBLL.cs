using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class ProveedorBLL
    {
        //Metodo de Busqueda
        public static void ObtenerProveedor(object razonSocial, object codigoSector, DataTable dtProveedor)
        {
            object codigo = null;
            if (codigoSector != null && Convert.ToInt32(codigoSector.ToString()) > 0) { codigo = codigoSector; }
            DAL.ProveedorDAL.ObtenerProveedores(razonSocial, codigo, dtProveedor);
        }

        //Metodo de Guardado de proveedor nuevo
        public static int GuardarProveedor(Entidades.Proveedor proveedor, Data.dsProveedor dsProveedor)
        {
            if (DAL.ProveedorDAL.EsProveedorNuevo(proveedor)) { return DAL.ProveedorDAL.GuardarProveedor(proveedor, dsProveedor); }
            else { throw new Entidades.Excepciones.ElementoExistenteException(); }             
        }

        //Metodo que se usa para guardar las modificaciones hechas sobre un proveedor
        public static void ModificarProveedor(Entidades.Proveedor proveedor, Data.dsProveedor dsProveedor)
        {
            if (DAL.ProveedorDAL.EsProveedorActualizar(proveedor)) { DAL.ProveedorDAL.ModificarProveedor(proveedor, dsProveedor); }
            else { throw new Entidades.Excepciones.ElementoExistenteException(); }           
        }

        //Metodo para eliminar el proveedor de la BD
        public static void EliminarProveedor(int codigoProveedor)
        {
            DAL.ProveedorDAL.EliminarProveedor(codigoProveedor);
        }

    }
}
