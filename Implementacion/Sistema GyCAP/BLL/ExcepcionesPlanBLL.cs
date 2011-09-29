using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GyCAP.Entidades;

namespace GyCAP.BLL
{
    public class ExcepcionesPlanBLL
    {
        //Implementar método Add que se le pase la MP y cantidad faltante y devuelva
        //el objeto ExcepcionPlan formateado para la lista
        //FALTA mp_nombre
        //DESC: Cant. Faltante:XX
        //TIPO: Enumeracion

        //Metodo que crea un objeto estandar con el formato como sigue
        public static ExcepcionesPlan Add_ExcepcionMP(decimal cantidad, MateriaPrima materiaPrima)
        {
            return new ExcepcionesPlan()
            {
                 Tipo = ExcepcionesPlan.TipoExcepcion.MateriaPrima,
                 Nombre = string.Format("FALTA: {0}", materiaPrima.Nombre),
                 Descripcion = string.Format("Faltante: {0} {1}", 
                                             decimal.Round(cantidad * -1, 3), 
                                             BLL.UnidadMedidaBLL.ObtenerUnidad(materiaPrima.CodigoUnidadMedida)
                                            )
            };
        }

        public static ExcepcionesPlan Add_ExcepcionCocinaSinEstructura(string cocina)
        {
            return new ExcepcionesPlan()
            {
                Tipo = ExcepcionesPlan.TipoExcepcion.CocinaSinEstructuraActiva,
                Nombre = "ESTRUCTURA INACTIVA",
                Descripcion = string.Format("La cocina {0} no posee una estructura activa", cocina)
            };
        }

        public static ExcepcionesPlan Add_ExcepcionCentroInactivo(string centro)
        {
            return new ExcepcionesPlan()
            {
                Tipo = ExcepcionesPlan.TipoExcepcion.CentroTrabajoInactivo,
                Nombre = "CENTRO DE TRABAJO INACTIVO",
                Descripcion = string.Format("El centro de trabajo {0} se encuentra inactivo", centro)
            };
        }

        public static ExcepcionesPlan Add_ExcepcionUbicacionStock(ExcepcionesPlan.TipoElemento tipo)
        {
            return new ExcepcionesPlan()
            {
                Tipo = ExcepcionesPlan.TipoExcepcion.SinUbicacionStock,
                Nombre = "SIN UBICACIÓN DE STOCK",
                Descripcion = string.Format("El elemento {0} no posee una ubicación de stock definida.", GetTipoElementoFriendlyName(tipo))
            };
        }

        public static ExcepcionesPlan Add_ExcepcionParteSinHojaRuta(string parte)
        {
            return new ExcepcionesPlan()
            {
                Tipo = ExcepcionesPlan.TipoExcepcion.ParteSinHojaRuta,
                Nombre = "SIN HOJA DE RUTA",
                Descripcion = string.Format("La parte {0} no posee una hoja de ruta definida.", parte)
            };
        }

        public static string GetTipoElementoFriendlyName(ExcepcionesPlan.TipoElemento tipo)
        {
            switch (tipo)
            {
                case ExcepcionesPlan.TipoElemento.DetalleHojaRuta:
                    return "Detalle Hoja de Ruta";
                    break;
                case ExcepcionesPlan.TipoElemento.OrdenTrababjo:
                    return "Orden de Trabajo";
                    break;
                case ExcepcionesPlan.TipoElemento.OrdenProduccion:
                    return "Orden de Producción";
                    break;
                case ExcepcionesPlan.TipoElemento.MateriaPrima:
                    return "Materia Prima";
                    break;
                case ExcepcionesPlan.TipoElemento.Parte:
                    return "Parte";
                    break;
                default:
                    return string.Empty;
                    break;
            }
        }
    }
}
