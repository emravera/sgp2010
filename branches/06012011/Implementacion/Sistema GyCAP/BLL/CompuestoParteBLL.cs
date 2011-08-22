using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class CompuestoParteBLL
    {
        public static readonly int HijoEsParte = 1;
        public static readonly int HijoEsMP = 2;
        
        public static void ObtenerCompuestosEstructura(int codigoEstructura, DataTable dtCompuestos_Partes)
        {
            DAL.CompuestoParteDAL.ObtenerCompuestosEstructura(codigoEstructura, dtCompuestos_Partes);
        }

        public static Entidades.CompuestoParte AsCompuestoParteEntity(int codigoCompuesto, Data.dsEstructuraProducto dsEstructura)
        {
            Data.dsEstructuraProducto.COMPUESTOS_PARTESRow row = dsEstructura.COMPUESTOS_PARTES.FindByCOMP_CODIGO(codigoCompuesto);

            Entidades.CompuestoParte compuesto = new GyCAP.Entidades.CompuestoParte()
            {
                Cantidad = row.COMP_CANTIDAD,
                Codigo = Convert.ToInt32(row.COMP_CODIGO),
                Estructura = null,
                MateriaPrima = (row.IsMP_CODIGONull()) ? null : BLL.MateriaPrimaBLL.AsMateriaPrimaEntity(row.MATERIAS_PRIMASRow),
                Parte = (row.IsMP_CODIGONull()) ? BLL.ParteBLL.AsParteEntity(Convert.ToInt32(row.PART_NUMERO), dsEstructura) : null,                
                UnidadMedida = BLL.UnidadMedidaBLL.AsUnidadMedidaEntity(Convert.ToInt32(row.UMED_CODIGO), dsEstructura),
                CompuestoPadre = (row.IsCOMP_CODIGO_PADRENull()) ? null : BLL.CompuestoParteBLL.AsCompuestoParteEntity(Convert.ToInt32(row.COMPUESTOS_PARTESRowParent.COMP_CODIGO), dsEstructura)
            };

            return compuesto;
        }
    }
}
