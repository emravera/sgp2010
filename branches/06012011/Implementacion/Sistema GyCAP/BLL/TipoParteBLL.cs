using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class TipoParteBLL
    {
        public static readonly int ValorSI = 1;
        public static readonly int ValorNO = 0;


        public static void ObtenerTodos(DataTable dtTiposPartes)
        {
            DAL.TipoParteDAL.ObtenerTodos(dtTiposPartes);
        }

        public static void Insertar(Entidades.TipoParte tipoParte)
        {
            if (DAL.TipoParteDAL.EsTipoParte(tipoParte)) { throw new Entidades.Excepciones.ElementoExistenteException(); }
            DAL.TipoParteDAL.Insertar(tipoParte);            
        }

        public static void Actualizar(Entidades.TipoParte tipoParte)
        {
            DAL.TipoParteDAL.Actualizar(tipoParte);
        }

        public static void Eliminar(int codigoTipoParte)
        {
            if (!DAL.TipoParteDAL.PuedeEliminarse(codigoTipoParte)) { throw new Entidades.Excepciones.ElementoEnTransaccionException(); }
            DAL.TipoParteDAL.Eliminar(codigoTipoParte);
        }

        public static void ObtenerTiposPartes(object nombre, object fantasma, object orden, object ensamblado, object adquirido, object terminado, DataTable dtTiposPartes)
        {
            if (fantasma != null && Convert.ToBoolean(fantasma.ToString())) { fantasma = ValorSI; }
            else { fantasma = ValorNO; }
            if (orden != null && Convert.ToBoolean(orden.ToString())) { orden = ValorSI; }
            else { orden = ValorNO; }
            if (ensamblado != null && Convert.ToBoolean(ensamblado.ToString())) { ensamblado = ValorSI; }
            else { ensamblado = ValorNO; }
            if (adquirido != null && Convert.ToBoolean(adquirido.ToString())) { adquirido = ValorSI; }
            else { adquirido = ValorNO; }
            if (terminado != null && Convert.ToBoolean(terminado.ToString())) { terminado = ValorSI; }
            else { terminado = ValorNO; }
            DAL.TipoParteDAL.ObtenerTiposPartes(nombre, fantasma, orden, ensamblado, adquirido, terminado, dtTiposPartes);
        }

        public static bool EsTipoFantasma(int codigoTipoParte)
        {
            return DAL.TipoParteDAL.EsTipoFantasma(codigoTipoParte);
        }

        public static bool EsProductoTerminado(int codigoTipoParte)
        {
            return DAL.TipoParteDAL.EsProductoTerminado(codigoTipoParte);
        }

        public static bool EsTipoAdquirido(int codigoTipoParte)
        {
            return DAL.TipoParteDAL.EsTipoAdquirido(codigoTipoParte);
        }

        public static bool EsTipoAdquiridoConOrden(int codigoTipoParte)
        {
            return DAL.TipoParteDAL.EsTipoAdquiridoConOrden(codigoTipoParte);
        }
    }
}
