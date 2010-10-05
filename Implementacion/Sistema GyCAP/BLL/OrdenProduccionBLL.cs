using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace GyCAP.BLL
{
    public class OrdenProduccionBLL
    {
        public static readonly int parteTipoConjunto = 1;
        public static readonly int parteTipoSubconjunto = 2;
        public static readonly int parteTipoPieza = 3;
        public static readonly int parteTipoMateriaPrima = 4;
        public static readonly int nodoOrdenTrabajo = 1;
        public static readonly int nodoDetalleOrdenTrabajo = 2;
        public static readonly int nodoComplemento = 3;

        public static readonly int EstadoGenerado = 1;
        public static readonly int EstadoEnProceso = 2;
        public static readonly int EstadoFinalizado = 5;

        public static string GetTipoParte(int tipo)
        {
            if (tipo == parteTipoConjunto) return "Conjunto";
            if (tipo == parteTipoSubconjunto) return "Subconjunto";
            if (tipo == parteTipoPieza) return "Pieza";
            if (tipo == parteTipoMateriaPrima) return "Materia Prima";
            return string.Empty;
        }
        
        public static void Insertar(int numeroOrdenProduccion, Data.dsOrdenTrabajo dsOrdenTrabajo)
        {
            DAL.OrdenProduccionDAL.Insertar(numeroOrdenProduccion, dsOrdenTrabajo);
        }

        public static void Eliminar(int numeroOrdenProduccion)
        {
            //revisar que condiciones hacen faltan pra poder elimiarse - gonzalo
            BLL.OrdenProduccionBLL.Eliminar(numeroOrdenProduccion);
        }

        /// <summary>
        /// Genera las órdenes de producción de todos los días para una semana dada.
        /// </summary>
        /// <param name="codigoSemana"></param>
        /// <param name="dsPlanSemanal"></param>
        /// <param name="dsOrdenTrabajo"></param>
        public static void GenerarOrdenProduccionSemana(int codigoSemana, Data.dsPlanSemanal dsPlanSemanal, Data.dsOrdenTrabajo dsOrdenTrabajo)
        {
            //Declaramos las variables necesarias
            int codigoOrdenP = -1;
            if (dsOrdenTrabajo.ORDENES_PRODUCCION.Rows.Count > 0)
            { codigoOrdenP = Convert.ToInt32((dsOrdenTrabajo.ORDENES_PRODUCCION.Select("ORDP_NUMERO = Min (ORDP_NUMERO)") as Data.dsOrdenTrabajo.ORDENES_PRODUCCIONRow[])[0].ORDP_NUMERO - 1); }
                        
            //Recorremos todos los días
            foreach (Data.dsPlanSemanal.DIAS_PLAN_SEMANALRow rowDia in dsPlanSemanal.PLANES_SEMANALES.FindByPSEM_CODIGO(codigoSemana).GetDIAS_PLAN_SEMANALRows())
            {
                //Recorremos el detalle de cada día y generamos la orden de producción
                foreach (Data.dsPlanSemanal.DETALLE_PLANES_SEMANALESRow rowDetalle in rowDia.GetDETALLE_PLANES_SEMANALESRows())
                {
                    //Primero controlamos si no tiene órdenes
                    if (rowDetalle.DPSEM_ESTADO == BLL.DetallePlanSemanalBLL.estadoGenerado)
                    {
                        //No tiene órdenes, controlamos si la cocina tiene una estructura activa
                        int codigoEstructura = CocinaBLL.ObtenerCodigoEstructuraActiva(Convert.ToInt32(rowDetalle.COC_CODIGO));
                        string mensaje = string.Empty;
                        if (codigoEstructura == 0)
                        {
                            mensaje = "La Cocina " + rowDetalle.COCINASRow.COC_CODIGO_PRODUCTO + " no posee una Estructura activa.";
                            throw new Entidades.Excepciones.OrdenTrabajoException(mensaje);
                        }

                        //Tiene estructura activa, creamos la orden de producción
                        Data.dsOrdenTrabajo.ORDENES_PRODUCCIONRow rowOrdenP = dsOrdenTrabajo.ORDENES_PRODUCCION.NewORDENES_PRODUCCIONRow();
                        rowOrdenP.BeginEdit();
                        rowOrdenP.ORDP_NUMERO = codigoOrdenP--;
                        rowOrdenP.ORDP_CODIGO = "OPA" + (rowOrdenP.ORDP_NUMERO * -1).ToString();
                        rowOrdenP.EORD_CODIGO = (dsOrdenTrabajo.ESTADO_ORDENES_TRABAJO.Select("EORD_NOMBRE = 'Generada'") as Data.dsOrdenTrabajo.ESTADO_ORDENES_TRABAJORow[])[0].EORD_CODIGO;
                        rowOrdenP.ORDP_FECHAALTA = DBBLL.GetFechaServidor();
                        rowOrdenP.DPSEM_CODIGO = rowDetalle.DPSEM_CODIGO;
                        rowOrdenP.ORDP_ORIGEN = rowOrdenP.ORDP_CODIGO + "-" + rowDetalle.COCINASRow.COC_CODIGO_PRODUCTO;
                        rowOrdenP.SetORDP_FECHAINICIOREALNull();
                        rowOrdenP.SetORDP_FECHAFINREALNull();
                        rowOrdenP.SetORDPM_NUMERONull();
                        rowOrdenP.ORDP_PRIORIDAD = 0;
                        rowOrdenP.ORDP_OBSERVACIONES = string.Empty;
                        rowOrdenP.COC_CODIGO = rowDetalle.COC_CODIGO;
                        rowOrdenP.ORDP_CANTIDADESTIMADA = rowDetalle.DPSEM_CANTIDADESTIMADA;
                        rowOrdenP.ORDP_CANTIDADREAL = 0;
                        rowOrdenP.ESTR_CODIGO = codigoEstructura;
                        rowOrdenP.EndEdit();
                        dsOrdenTrabajo.ORDENES_PRODUCCION.AddORDENES_PRODUCCIONRow(rowOrdenP);
                        rowDetalle.BeginEdit();
                        rowDetalle.DPSEM_ESTADO = BLL.DetallePlanSemanalBLL.estadoConOrden;
                        rowDetalle.EndEdit();
                    }
                }
            }
        }

        /// <summary>
        /// Genera las órdenes de producción dado un día.
        /// </summary>
        /// <param name="codigoDia"></param>
        /// <param name="dsPlanSemanal"></param>
        /// <param name="dsOrdenTrabajo"></param>
        public static void GenerarOrdenProduccionDia(int codigoDia, Data.dsPlanSemanal dsPlanSemanal, Data.dsOrdenTrabajo dsOrdenTrabajo)
        {
            //Generamos el código de la orden de producción
            int codigoOrdenP = -1;
            if (dsOrdenTrabajo.ORDENES_PRODUCCION.Rows.Count > 0)
            { codigoOrdenP = Convert.ToInt32((dsOrdenTrabajo.ORDENES_PRODUCCION.Select("ORDP_NUMERO = Min (ORDP_NUMERO)") as Data.dsOrdenTrabajo.ORDENES_PRODUCCIONRow[])[0].ORDP_NUMERO - 1); }
            
            //Recorremos el detalle del día y generamos la orden de producción para cada detalle
            foreach (Data.dsPlanSemanal.DETALLE_PLANES_SEMANALESRow rowDetalle in (Data.dsPlanSemanal.DETALLE_PLANES_SEMANALESRow[])dsPlanSemanal.DETALLE_PLANES_SEMANALES.Select("diapsem_codigo = " + codigoDia))
            {
                //Primero controlamos si no tiene órdenes
                if (rowDetalle.DPSEM_ESTADO == BLL.DetallePlanSemanalBLL.estadoGenerado)
                {
                    //No tiene órdenes, controlamos si la cocina tiene una estructura activa
                    int codigoEstructura = CocinaBLL.ObtenerCodigoEstructuraActiva(Convert.ToInt32(rowDetalle.COC_CODIGO));
                    string mensaje = string.Empty;
                    if (codigoEstructura == 0)
                    {
                        mensaje = "La Cocina " + rowDetalle.COCINASRow.COC_CODIGO_PRODUCTO + " no posee una Estructura activa.";
                        throw new Entidades.Excepciones.OrdenTrabajoException(mensaje);
                    }

                    //Tiene estructura activa, creamos la orden de producción
                    Data.dsOrdenTrabajo.ORDENES_PRODUCCIONRow rowOrdenP = dsOrdenTrabajo.ORDENES_PRODUCCION.NewORDENES_PRODUCCIONRow();
                    rowOrdenP.BeginEdit();
                    rowOrdenP.ORDP_NUMERO = codigoOrdenP--;
                    rowOrdenP.ORDP_CODIGO = "OPA" + (rowOrdenP.ORDP_NUMERO * -1).ToString();
                    rowOrdenP.EORD_CODIGO = (dsOrdenTrabajo.ESTADO_ORDENES_TRABAJO.Select("EORD_NOMBRE = 'Generada'") as Data.dsOrdenTrabajo.ESTADO_ORDENES_TRABAJORow[])[0].EORD_CODIGO;
                    rowOrdenP.ORDP_FECHAALTA = DBBLL.GetFechaServidor();
                    rowOrdenP.DPSEM_CODIGO = rowDetalle.DPSEM_CODIGO;
                    rowOrdenP.ORDP_ORIGEN = rowOrdenP.ORDP_CODIGO + "-" + rowDetalle.COCINASRow.COC_CODIGO_PRODUCTO;
                    rowOrdenP.SetORDP_FECHAINICIOREALNull();
                    rowOrdenP.SetORDP_FECHAFINREALNull();
                    rowOrdenP.SetORDPM_NUMERONull();
                    rowOrdenP.ORDP_PRIORIDAD = 0;
                    rowOrdenP.ORDP_OBSERVACIONES = string.Empty;
                    rowOrdenP.COC_CODIGO = rowDetalle.COC_CODIGO;
                    rowOrdenP.ORDP_CANTIDADESTIMADA = rowDetalle.DPSEM_CANTIDADESTIMADA;
                    rowOrdenP.ORDP_CANTIDADREAL = 0;
                    rowOrdenP.ESTR_CODIGO = codigoEstructura;
                    rowOrdenP.EndEdit();
                    dsOrdenTrabajo.ORDENES_PRODUCCION.AddORDENES_PRODUCCIONRow(rowOrdenP);
                    rowDetalle.BeginEdit();
                    rowDetalle.DPSEM_ESTADO = BLL.DetallePlanSemanalBLL.estadoConOrden;
                    rowDetalle.EndEdit();
                }
            }            
        }        

        /// <summary>
        /// Genera los tres tipos de árbol de las órdenes de trabajo para la orden de producción dada.
        /// </summary>
        /// <param name="codigoOrdenProduccion"></param>
        /// <param name="tvDependenciaSimple"></param>
        /// <param name="tvDependenciaCompleta"></param>
        /// <param name="tvOrdenesYEstructura"></param>
        /// <param name="dsOrdenTrabajo"></param>
        /// <param name="dsEstructura"></param>
        /// <param name="dsHojaRuta"></param>
        public static void GenerarArbolOrdenesTrabajo(int codigoOrdenProduccion, TreeView tvDependenciaSimple, TreeView tvDependenciaCompleta, TreeView tvOrdenesYEstructura, Data.dsOrdenTrabajo dsOrdenTrabajo, Data.dsEstructura dsEstructura, Data.dsHojaRuta dsHojaRuta)
        {
            Data.dsOrdenTrabajo.ORDENES_PRODUCCIONRow rowOrdenP = dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codigoOrdenProduccion);
            tvDependenciaSimple.Nodes.Clear();
            tvOrdenesYEstructura.Nodes.Clear();
            tvDependenciaCompleta.Nodes.Clear();
            TreeNode nodoOrdenOT = new TreeNode();
            TreeNode nodoOrdenOTyE = new TreeNode();
            TreeNode nodoOrdenDC = new TreeNode();
            nodoOrdenOT.Name = rowOrdenP.ORDP_NUMERO.ToString();
            nodoOrdenOT.Text = rowOrdenP.ORDP_ORIGEN;
            nodoOrdenOT.Tag = nodoOrdenTrabajo;
            tvDependenciaSimple.Nodes.Add(nodoOrdenOT);
            nodoOrdenOTyE.Name = rowOrdenP.ORDP_NUMERO.ToString();
            nodoOrdenOTyE.Text = rowOrdenP.ORDP_ORIGEN;
            nodoOrdenOTyE.Tag = nodoOrdenTrabajo;
            tvOrdenesYEstructura.Nodes.Add(nodoOrdenOTyE);
            nodoOrdenDC.Name = rowOrdenP.ORDP_NUMERO.ToString();
            nodoOrdenDC.Text = rowOrdenP.ORDP_ORIGEN;
            nodoOrdenDC.Tag = nodoOrdenTrabajo;
            tvDependenciaCompleta.Nodes.Add(nodoOrdenDC);

            TreeNode nodoDetalleOT, nodoDetalleOTyE, nodoDetalleDC, ultimoNodoParte = new TreeNode(), parte, ordenes;
            foreach (Data.dsOrdenTrabajo.ORDENES_TRABAJORow rowOrdenT in rowOrdenP.GetORDENES_TRABAJORows())
            {
                #region Árbol dependencia simple
                nodoDetalleOT = new TreeNode();
                nodoDetalleOT.Name = rowOrdenT.ORDT_NUMERO.ToString();
                nodoDetalleOT.Text = rowOrdenT.ORDT_ORIGEN;
                nodoDetalleOT.Tag = nodoDetalleOrdenTrabajo;
                if (rowOrdenT.IsORDT_ORDENSIGUIENTENull())
                {
                    nodoOrdenOT.Nodes.Add(nodoDetalleOT);
                }
                else
                {
                    if (rowOrdenT.ORDT_NIVEL == 1)
                    {
                        nodoOrdenOT.Nodes.Find(rowOrdenT.ORDT_ORDENSIGUIENTE.ToString(), true)[0].Nodes.Add(nodoDetalleOT);
                    }
                    else
                    {
                        nodoOrdenOT.Nodes.Find(rowOrdenT.ORDT_ORDENSIGUIENTE.ToString(), true)[0].Parent.Nodes.Add(nodoDetalleOT);
                    }
                }
                #endregion

                #region Árbol dependencia completa

                nodoDetalleDC = new TreeNode();
                nodoDetalleDC.Name = rowOrdenT.ORDT_NUMERO.ToString();
                nodoDetalleDC.Text = rowOrdenT.ORDT_ORIGEN;
                nodoDetalleDC.Tag = nodoDetalleOrdenTrabajo;
                if (rowOrdenT.IsORDT_ORDENSIGUIENTENull())
                {
                    nodoOrdenDC.Nodes.Add(nodoDetalleDC);
                }
                else
                {
                    nodoOrdenDC.Nodes.Find(rowOrdenT.ORDT_ORDENSIGUIENTE.ToString(), true)[0].Nodes.Add(nodoDetalleDC);
                }

                #endregion

                #region Árbol órdenes y estructura
                nodoDetalleOTyE = new TreeNode();
                nodoDetalleOTyE.Name = rowOrdenT.ORDT_NUMERO.ToString();
                nodoDetalleOTyE.Text = rowOrdenT.ORDT_ORIGEN;
                nodoDetalleOTyE.Tag = nodoDetalleOrdenTrabajo;
                if (rowOrdenT.IsORDT_ORDENSIGUIENTENull())
                {
                    parte = new TreeNode();
                    ordenes = new TreeNode();
                    parte.Name = rowOrdenT.PAR_CODIGO.ToString();
                    parte.Text = GetTipoParte(Convert.ToInt32(rowOrdenT.PAR_TIPO));
                    parte.Tag = nodoComplemento;
                    nodoOrdenOTyE.Nodes.Add(parte);
                    ordenes.Name = "ordenes";
                    ordenes.Text = "Órdenes";
                    ordenes.Tag = nodoComplemento;
                    parte.Nodes.Add(ordenes);                    
                    ordenes.Nodes.Add(nodoDetalleOTyE);
                    ultimoNodoParte = parte;
                }
                else
                {
                    if (Convert.ToInt32(rowOrdenT.ORDT_NIVEL) == 0)
                    {
                        ultimoNodoParte.Nodes["ordenes"].Nodes.Add(nodoDetalleOTyE);
                    }
                    else
                    {
                        while (rowOrdenT.ORDT_ORDENSIGUIENTE != Convert.ToDecimal(ultimoNodoParte.Nodes["ordenes"].LastNode.Name))
                        {
                            ultimoNodoParte = ultimoNodoParte.Parent;
                        }

                        parte = new TreeNode();
                        parte.Name = rowOrdenT.PAR_CODIGO.ToString();
                        parte.Text = GetTipoParte(Convert.ToInt32(rowOrdenT.PAR_TIPO));
                        parte.Tag = nodoComplemento;
                        ultimoNodoParte.Nodes.Add(parte);
                        ordenes = new TreeNode();
                        ordenes.Name = "ordenes";
                        ordenes.Text = "Órdenes";
                        ordenes.Tag = nodoComplemento;
                        parte.Nodes.Add(ordenes);
                        ultimoNodoParte = parte;
                        ultimoNodoParte.Nodes["ordenes"].Nodes.Add(nodoDetalleOTyE);
                    }
                }

                #endregion
            }
            tvDependenciaSimple.ExpandAll();
            tvDependenciaCompleta.ExpandAll();
            tvOrdenesYEstructura.ExpandAll();
        }        

        public static void PlanearFechaHaciaAtras(int codigoOrdenTrabajo, DateTime fechaFinalizacion, TreeView tvDependenciaCompleta, Data.dsOrdenTrabajo dsOrdenTrabajo, Data.dsEstructura dsEstructura, Data.dsHojaRuta dsHojaRuta)
        {
            TreeNode nodoOrden = tvDependenciaCompleta.Nodes[codigoOrdenTrabajo.ToString()];
            dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(codigoOrdenTrabajo).ORDT_FECHAFINESTIMADA = fechaFinalizacion;
            DateTime fechaMenor = fechaFinalizacion;
            DateTime nuevaFecha;
            foreach (TreeNode nodoHijo in nodoOrden.Nodes)
            {
                nuevaFecha = CalcularFechaHaciaAtras(nodoHijo, fechaFinalizacion, dsOrdenTrabajo, dsEstructura, dsHojaRuta);
                if (nuevaFecha < fechaMenor) { fechaMenor = nuevaFecha; }
            }

            dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(codigoOrdenTrabajo).ORDT_FECHAINICIOESTIMADA = fechaMenor;
        }

        private static DateTime CalcularFechaHaciaAtras(TreeNode nodo, DateTime fechaFin, Data.dsOrdenTrabajo dsOrdenTrabajo, Data.dsEstructura dsEstructura, Data.dsHojaRuta dsHojaRuta)
        {
            if (nodo != null)
            {
                dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(Convert.ToInt32(nodo.Name)).ORDT_FECHAFINESTIMADA = fechaFin;
                decimal codOperacion = dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(Convert.ToInt32(nodo.Name)).OPR_NUMERO;
                decimal codCentro = dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(Convert.ToInt32(nodo.Name)).CTO_CODIGO;
                decimal horasOperacion = dsHojaRuta.OPERACIONES.FindByOPR_NUMERO(codOperacion).OPR_HORASREQUERIDA;
                decimal cantidad = dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(Convert.ToInt32(nodo.Name)).ORDT_CANTIDADESTIMADA;
                TimeSpan restar = TimeSpan.FromHours(Convert.ToDouble(cantidad * horasOperacion));                
                DateTime fechaInicioDetalle = fechaFin.Subtract(restar);
                dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(Convert.ToInt32(nodo.Name)).ORDT_FECHAINICIOESTIMADA = fechaInicioDetalle;
                DateTime fechaMenor = fechaInicioDetalle;
                DateTime nuevaFecha;
                foreach (TreeNode nodoHijo in nodo.Nodes)
                {
                    nuevaFecha = CalcularFechaHaciaAtras(nodoHijo, fechaInicioDetalle, dsOrdenTrabajo, dsEstructura, dsHojaRuta);
                    if (nuevaFecha < fechaMenor) { fechaMenor = nuevaFecha; }
                }
                return fechaMenor;
            }

            return fechaFin;
        }

        public static void PlanearFechaHaciaDelante(int codigoOrdenProduccion, DateTime fechaInicio, TreeView tvDependenciaCompleta, Data.dsOrdenTrabajo dsOrdenTrabajo, Data.dsEstructura dsEstructura, Data.dsHojaRuta dsHojaRuta)
        {
            TreeNode nodoOrden = tvDependenciaCompleta.Nodes[codigoOrdenProduccion.ToString()];
            dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codigoOrdenProduccion).ORDP_FECHAINICIOESTIMADA = fechaInicio;
            DateTime fechaMayor = fechaInicio;
            DateTime nuevaFecha;
            foreach (TreeNode nodoHijo in nodoOrden.Nodes)
            {
                nuevaFecha = CalcularFechaHaciaDelante(nodoHijo, fechaInicio, dsOrdenTrabajo, dsEstructura, dsHojaRuta);
                if (nuevaFecha > fechaMayor) { fechaMayor = nuevaFecha; }
            }

            dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codigoOrdenProduccion).ORDP_FECHAFINESTIMADA = fechaMayor;
        }
        
        private static DateTime CalcularFechaHaciaDelante(TreeNode nodo, DateTime fechaInicio, Data.dsOrdenTrabajo dsOrdenTrabajo, Data.dsEstructura dsEstructura, Data.dsHojaRuta dsHojaRuta)
        {
            if (nodo != null)
            {
                dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(Convert.ToInt32(nodo.Name)).ORDT_FECHAINICIOESTIMADA = fechaInicio;
                decimal codOperacion = dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(Convert.ToInt32(nodo.Name)).OPR_NUMERO;
                decimal codCentro = dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(Convert.ToInt32(nodo.Name)).CTO_CODIGO;
                decimal horasOperacion = dsHojaRuta.OPERACIONES.FindByOPR_NUMERO(codOperacion).OPR_HORASREQUERIDA;
                decimal cantidad = dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(Convert.ToInt32(nodo.Name)).ORDT_CANTIDADESTIMADA;
                decimal sumar = cantidad * horasOperacion;
                DateTime fechaFinDetalle = fechaInicio.AddHours(Double.Parse(sumar.ToString()));
                dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(Convert.ToInt32(nodo.Name)).ORDT_FECHAFINESTIMADA = fechaFinDetalle;
                DateTime fechaMayor = fechaFinDetalle;
                DateTime nuevaFecha;
                foreach (TreeNode nodoHijo in nodo.Nodes)
                {
                    nuevaFecha = CalcularFechaHaciaDelante(nodoHijo, fechaFinDetalle, dsOrdenTrabajo, dsEstructura, dsHojaRuta);
                    if (nuevaFecha > fechaMayor) { fechaMayor = nuevaFecha; }
                }
                return fechaMayor;
            }

            return fechaInicio;
        }
    }
}
