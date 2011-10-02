﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using GyCAP.Entidades;
using GyCAP.Entidades.BindingEntity;

namespace GyCAP.BLL
{
    public class CierreParcialOrdenTrabajoBLL
    {
        public static void Actualizar(Entidades.CierreParcialOrdenTrabajo cierreOrdenTrabajo)
        {
            DAL.CierreParcialOrdenTrabajoDAL.Actualizar(cierreOrdenTrabajo);
        }

        public static void Eliminar(int codigoCierreOrdenTrabajo)
        {
            DAL.CierreParcialOrdenTrabajoDAL.Eliminar(codigoCierreOrdenTrabajo);
        }

        public static SortableBindingList<CierreParcialOrdenTrabajo> ObtenerCierresParcialesOrdenTrabajo(OrdenTrabajo orden, 
                                                                                                         SortableBindingList<Maquina> maquinas, 
                                                                                                         SortableBindingList<Empleado> empleados)
        {
            Data.dsOrdenTrabajo.CIERRE_ORDEN_TRABAJODataTable dt = new GyCAP.Data.dsOrdenTrabajo.CIERRE_ORDEN_TRABAJODataTable();
            DAL.CierreParcialOrdenTrabajoDAL.ObtenerCierresParcialesOrdenTrabajo(orden.Numero, dt);
            SortableBindingList<CierreParcialOrdenTrabajo> lista = new SortableBindingList<CierreParcialOrdenTrabajo>();

            foreach (Data.dsOrdenTrabajo.CIERRE_ORDEN_TRABAJORow row in dt.Rows)
            {
                CierreParcialOrdenTrabajo cierre = new CierreParcialOrdenTrabajo();
                cierre.Codigo = Convert.ToInt32(row.CORD_CODIGO);
                cierre.Cantidad = row.CORD_CANTIDAD;
                cierre.Empleado = empleados.Where(p => p.Codigo == long.Parse(row.E_CODIGO.ToString())).Single();
                if (row.IsCORD_FECHACIERRENull()) { cierre.Fecha = null; }
                else { cierre.Fecha = row.CORD_FECHACIERRE; }
                cierre.Hora = 0;
                cierre.Maquina = maquinas.Where(p => p.Codigo == Convert.ToInt32(row.MAQ_CODIGO)).Single();
                cierre.Observaciones = row.CORD_OBSERVACIONES;
                cierre.OrdenTrabajo = orden;

                lista.Add(cierre);
            }

            return orden.CierresParciales = lista;
        }
    }
}
