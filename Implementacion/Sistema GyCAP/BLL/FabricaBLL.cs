using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GyCAP.Entidades;
using GyCAP.Entidades.ArbolEstructura;
using GyCAP.Entidades.Excepciones;
using System.Data;

namespace GyCAP.BLL
{
    public class FabricaBLL
    {
        public enum TipoHorario { Normal, Extendido };
        
        /// <summary>
        /// Obtiene la capacidad bruta de la fábrica anual. 
        /// No tiene en cuenta la producción en curso.
        /// </summary>
        /// <param name="codigoCocina">La cocina base, si es null se determina una.</param>
        /// <param name="tipoHorario">El tipo de horario: normal o extendido.</param>
        /// <returns>El código de la cocina base.</returns>
        /// <exception cref="Entidades.CocinaBaseException">Cuando no exite una cocina base definida.</exception>
        public static int GetCapacidadSemanalBruta(int? codigoCocina, TipoHorario tipoHorario)
        {
            int capacidad = 0;

            if (!codigoCocina.HasValue) { codigoCocina = CocinaBLL.GetCodigoCocinaBase(); }

            IList<CapacidadNecesidadCombinada> listaCombinada = EstructuraBLL.AsListForCapacity(codigoCocina.Value);

            int[] numerosPartes = new int[listaCombinada.Count];

            for (int i = 0; i < listaCombinada.Count; i++) { numerosPartes[i] = listaCombinada[i].Parte.Numero; }

            DataTable dt = DAL.FabricaDAL.GetOperacionesCentrosByPartes(numerosPartes);

            foreach (DataRow row in dt.Rows)
            {
                int parte = Convert.ToInt32(row["part_numero"].ToString());
                decimal operacion = row.Field<decimal>("opr_horasrequerida");
                decimal horasTrabajo = 0;
                if (tipoHorario == TipoHorario.Normal)
                {
                    horasTrabajo = row.Field<decimal>("cto_horastrabajonormal");
                }
                else
                {
                    horasTrabajo = row.Field<decimal>("cto_horastrabajoextendido");
                }

                CapacidadNecesidadCombinada cnc = (from x in listaCombinada
                                                   where x.Parte.Numero == parte
                                                   select x).FirstOrDefault();

                cnc.ListaOperacionesCentros.Add(new CapacidadNecesidadCombinadaItem()
                {
                    CapacidadCentroTrabajo = horasTrabajo,                    
                    TiempoOperacion = operacion * cnc.Necesidad,
                    Resultado = Convert.ToInt32(horasTrabajo / (operacion * cnc.Necesidad))
                });
            }

            capacidad = listaCombinada.Min(p => p.ListaOperacionesCentros.Min(x => x.Resultado));

            return capacidad;
        }

        /// <summary>
        /// Obtiene la capacidad bruta de la fábrica anual, con horario normal. 
        /// No tiene en cuenta la producción en curso.
        /// </summary>
        /// <returns>La cantidad de cocinas que puede fabricarse.</returns>
        /// <exception cref="Entidades.CocinaBseException">Cuando no exite una cocina base definida.</exception>
        public static int GetCapacidadAnualBruta(int? codigoCocina, TipoHorario tipoHorario)
        {
            int capacidad = GetCapacidadSemanalBruta(codigoCocina, tipoHorario);

            return capacidad * 53;
        }
    }
}
