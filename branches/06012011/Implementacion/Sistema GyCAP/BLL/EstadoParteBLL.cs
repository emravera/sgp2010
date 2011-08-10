using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class EstadoParteBLL
    {
        public static void ObtenerTodos(System.Data.DataTable dtEstados)
        {
            DAL.EstadoParteDAL.ObtenerTodos(dtEstados);
        }

        public static Entidades.EstadoParte AsEstadoParteEntity(Data.dsEstructuraProducto.ESTADO_PARTESRow row)
        {
            Entidades.EstadoParte estado = new GyCAP.Entidades.EstadoParte()
            {
                Codigo = Convert.ToInt32(row.PAR_CODIGO),
                Descripcion = row.PAR_DESCRIPCION,
                Nombre = row.PAR_NOMBRE
            };

            return estado;
        }
    }
}
