using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades.Enumeraciones
{
    public class OrdenesTrabajoEnum
    {
        public enum EstadoOrdenEnum { Generada = 1, EnEspera = 2, EnProceso = 3, Finalizada = 4, Cancelada = 5 };

        public static string GetFriendlyName(EstadoOrdenEnum estado)
        {
            switch (estado)
            {
                case EstadoOrdenEnum.Generada:
                    return "Generada";
                    break;
                case EstadoOrdenEnum.EnEspera:
                    return "En Espera";
                    break;
                case EstadoOrdenEnum.EnProceso:
                    return "En Proceso";
                    break;
                case EstadoOrdenEnum.Finalizada:
                    return "Finalizada";
                    break;
                case EstadoOrdenEnum.Cancelada:
                    return "Cancelada";
                    break;
                default:
                    return string.Empty;
                    break;
            }
        }
    }
}
