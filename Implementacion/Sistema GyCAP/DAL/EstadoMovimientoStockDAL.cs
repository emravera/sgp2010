﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class EstadoMovimientoStockDAL
    {
        public static readonly int EstadoPlanificado = 1;
        public static readonly int EstadoFinalizado = 2;
        public static readonly int EstadoCancelado = 3;
        
        public static void ObtenerEstados(DataTable dtEstadosMovimientoStock)
        {
            string sql = "SELECT emvto_codigo, emvto_nombre, emvto_descripcion FROM ESTADO_MOVIMIENTOS_STOCK";

            try
            {
                DB.FillDataTable(dtEstadosMovimientoStock, sql, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
    }
}
