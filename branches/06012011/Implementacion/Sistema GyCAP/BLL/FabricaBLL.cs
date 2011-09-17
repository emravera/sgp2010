using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GyCAP.Entidades;
using GyCAP.Entidades.ArbolEstructura;
using GyCAP.Entidades.Excepciones;
using GyCAP.Entidades.Enumeraciones;
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
                decimal horasTrabajo = 0;
                decimal tiempoAntes = row.Field<decimal>("cto_tiempoantes");
                decimal tiempoDespues = row.Field<decimal>("cto_tiempodespues");
                decimal eficiencia = row.Field<decimal>("cto_eficiencia");
                decimal capacidadCiclo = 0, horasCiclo = 0, capacidadUnidadHora = 0, capacidadCentro = 0;
                
                if (row.Field<decimal>("cto_activo") == BLL.CentroTrabajoBLL.CentroActivo)
                {
                    if (tipoHorario == TipoHorario.Normal)
                    {
                        horasTrabajo = row.Field<decimal>("cto_horastrabajonormal");
                    }
                    else
                    {
                        horasTrabajo = row.Field<decimal>("cto_horastrabajoextendido");
                    }
                    
                    horasTrabajo -= (tiempoAntes + tiempoDespues) * BLL.ConfiguracionSistemaBLL.GetConfiguracion<int>("DiasLaborales");

                    if (row.Field<decimal>("cto_tipo") == BLL.CentroTrabajoBLL.TipoHombre)
                    {
                        capacidadUnidadHora = row.Field<decimal>("cto_capacidadunidadhora");
                        capacidadCentro = horasTrabajo * capacidadUnidadHora;
                    }
                    else
                    {
                        capacidadCiclo = row.Field<decimal>("cto_capacidadciclo");
                        horasCiclo = row.Field<decimal>("cto_horasciclo");
                        capacidadCentro = (horasTrabajo / horasCiclo) * capacidadCiclo;
                    }

                    capacidadCentro *= eficiencia;
                    if (capacidadCentro < 0) { capacidadCentro = 0; }
                }

                CapacidadNecesidadCombinada cnc = (from x in listaCombinada
                                                   where x.Parte.Numero == parte
                                                   select x).FirstOrDefault();
                
                cnc.ListaOperacionesCentros.Add(new CapacidadNecesidadCombinadaItem()
                {
                    CapacidadCentroTrabajo = capacidadCentro,
                    CantidadNecesaria = cnc.Necesidad,
                    Resultado = Convert.ToInt32(capacidadCentro / cnc.Necesidad)
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

        /// <summary>
        /// Obtiene la cantidad de stock estimada para una fecha dada.
        /// </summary>
        /// <param name="stock">El código de la ubicación de stock.</param>
        /// <param name="fecha">La fecha objetivo.</param>
        /// <returns>La cantidad de stock para la fecha especificada.</returns>
        public static decimal GetStockForDay(int stock, DateTime fecha)
        {
            UbicacionStock ubicacion = UbicacionStockBLL.GetUbicacionStock(stock);
            if (ubicacion == null) { throw new Entidades.Excepciones.ElementoInexistenteException(); }
            
            IList<MovimientoStock> lista = MovimientoStockBLL.ObtenerMovimientosUbicacionStock(DateTime.Today, fecha, stock, EstadoMovimientoStockBLL.Planificado, StockEnum.TipoFecha.FechaPrevista);
            
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].Origen.TipoEntidad.Nombre == TipoEntidadBLL.UbicacionStockNombre
                    &&
                   (lista[i].Origen.EntidadExterna as UbicacionStock).Codigo == ubicacion.Codigo)
                {
                    ubicacion.CantidadReal += lista[i].CantidadOrigenEstimada;
                }
                else
                {
                    ubicacion.CantidadReal += lista[i].CantidadDestinoEstimada;
                }
            }

            return ubicacion.CantidadReal;
        }

        /// <summary>
        /// Obtiene el costo total de una cocina dada o en su defecto de la cocina base.
        /// Está formado por el costo de la estructura y el costo de proceso.
        /// </summary>
        /// <param name="codigoCocina">El código de la cocina, en caso de ser null se tma la cocina base.</param>
        /// <returns>El costo de la cocina.</returns>
        public static decimal GetCostoTotalProducto(int? codigoCocina)
        {
            decimal costo = 0;
            try
            {
                if (!codigoCocina.HasValue) { codigoCocina = CocinaBLL.GetCodigoCocinaBase(); }

                ArbolEstructura arbol = EstructuraBLL.GetArbolEstructura(codigoCocina.Value, true);

                costo = (arbol == null) ? 0 : arbol.GetCostoEstructura() + arbol.GetCostoProceso();
            }
            catch (BaseDeDatosException) { }
            catch (CocinaBaseException) { }

            return costo;
        }
    }
}
