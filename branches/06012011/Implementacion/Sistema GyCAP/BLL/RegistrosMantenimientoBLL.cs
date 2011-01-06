using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class RegistrosMantenimientoBLL
    {
        //Busqueda
        //Obtiene los datos de acuerdo a los criterios de busqueda
        public static void ObtenerRegistrosMantenimiento(object fechaDesde, object fechaHasta, int idEmpleado, int idMaquina, int idTipoMantenimiento, Data.dsRegistrarMantenimiento ds, bool obtenerDetalle)
        {
            DAL.RegistrosMantenimientoDAL.ObtenerRegistroMantenimiento(fechaDesde, fechaHasta,idEmpleado,idMaquina,idTipoMantenimiento, ds, obtenerDetalle);
        }

        public static void Insertar(Data.dsRegistrarMantenimiento dsRegistrosMantenimiento)
        {
            //Si existe lanzamos la excepción correspondiente
            //Entidades.RegistrosMantenimientos registrosMantenimiento = new GyCAP.Entidades.RegistrosMantenimientos();
            ////Así obtenemos el pedido nuevo del dataset, indicamos la primer fila de la agregadas ya que es una sola y convertimos al tipo correcto
            //Data.dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOSRow rowRegistrosMantenimiento = dsRegistrosMantenimiento.REGISTROS_MANTENIMIENTOS.GetChanges(System.Data.DataRowState.Added).Rows[0] as Data.dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOSRow;
            ////Creamos el objeto pedido para verificar si existe
            //registrosMantenimiento.Tipo = Convert.ToInt32(rowRegistrosMantenimiento.TMAN_CODIGO);
            //registrosMantenimiento.Observacion = rowRegistrosMantenimiento.RMAN_OBSERVACION;

            //if (EsRegistroMAntenimiento(registrosMantenimiento)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            DAL.RegistrosMantenimientoDAL.Insertar(dsRegistrosMantenimiento);
        }

        //Comprueba si existe una pieza dado su nombre y terminación
        public static bool EsRegistroMAntenimiento(Entidades.RegistrosMantenimientos registrosMantenimiento)
        {
            return DAL.RegistrosMantenimientoDAL.EsRegistroMantenimiento(registrosMantenimiento);
        }

        public static void Actualizar(Data.dsRegistrarMantenimiento dsRegistrarMantenimiento)
        {
            DAL.RegistrosMantenimientoDAL.Actualizar(dsRegistrarMantenimiento);
        }

        public static void Eliminar(long codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.RegistrosMantenimientoDAL.PuedeEliminarse(codigo))
            {
                //Puede eliminarse
                DAL.RegistrosMantenimientoDAL.Eliminar(codigo);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }

        }
    }
}
