﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades.Enumeraciones
{
    public class RecursosFabricacionEnum
    {
        public enum TipoCentroTrabajo { TipoHombre = 1, TipoMaquina = 2 };
        public enum EstadoCentroTrabajo { Activo = 1, Inactivo = 0 };
        public enum TipoHorario { Normal, Extendido };
    }
}
