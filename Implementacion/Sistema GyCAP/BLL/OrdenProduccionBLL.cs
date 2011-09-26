using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using GyCAP.Entidades;
using GyCAP.Entidades.ArbolEstructura;
using GyCAP.Entidades.ArbolOrdenesTrabajo;
using GyCAP.Entidades.Enumeraciones;
using GyCAP.Entidades.BindingEntity;

namespace GyCAP.BLL
{
    public class OrdenProduccionBLL
    {
        public static void ObtenerOrdenesProduccion(object codigo, object estado, object modo, object fechaGeneracion, object fechaDesde, object fechaHasta, Data.dsOrdenTrabajo dsOrdenTrabajo)
        {
            if (estado != null && Convert.ToInt32(estado) <= 0) { estado = null; }
            if (modo != null && Convert.ToInt32(modo) <= 0) { modo = null; }
            DAL.OrdenProduccionDAL.ObtenerOrdenesProduccion(codigo, estado, modo, fechaGeneracion, fechaDesde, fechaHasta, dsOrdenTrabajo);
            foreach (Data.dsOrdenTrabajo.ORDENES_PRODUCCIONRow row in dsOrdenTrabajo.ORDENES_PRODUCCION)
            {
                OrdenTrabajoBLL.ObtenerOrdenesTrabajo(Convert.ToInt32(row.ORDP_NUMERO), dsOrdenTrabajo, true);
            }

            foreach (Data.dsOrdenTrabajo.ORDENES_TRABAJORow row in dsOrdenTrabajo.ORDENES_TRABAJO)
            {
                CierreParcialOrdenTrabajoBLL.ObtenerCierresParcialesOrdenTrabajo(Convert.ToInt32(row.ORDT_NUMERO), dsOrdenTrabajo.CIERRE_ORDEN_TRABAJO);
            }
        }
        
        public static void Insertar(ArbolProduccion arbol)
        {
            DAL.OrdenProduccionDAL.Insertar(arbol);
        }

        public static void Actualizar(ArbolProduccion arbol)
        {

        }

        public static void Guardar(ArbolProduccion arbol)
        {
            if (arbol.OrdenProduccion.Numero > 0) { Insertar(arbol); }
            else { Actualizar(arbol); }
        }

        public static void Eliminar(int numeroOrdenProduccion)
        {
            //revisar que condiciones hacen faltan para poder elimiarse - gonzalo
            DAL.OrdenProduccionDAL.Eliminar(numeroOrdenProduccion);
        }

        public static void Eliminar(IList<Entidades.OrdenProduccion> ordenesProduccion)
        {
            //revisar que condiciones hacen faltan para poder elimiarse - gonzalo
            DAL.OrdenProduccionDAL.Eliminar(ordenesProduccion);
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
                        //rowOrdenP.SetORDPM_NUMERONull();
                        rowOrdenP.ORDP_PRIORIDAD = 0;
                        rowOrdenP.ORDP_OBSERVACIONES = string.Empty;
                        rowOrdenP.COC_CODIGO = rowDetalle.COC_CODIGO;
                        rowOrdenP.ORDP_CANTIDADESTIMADA = rowDetalle.DPSEM_CANTIDADESTIMADA;
                        rowOrdenP.ORDP_CANTIDADREAL = 0;
                        rowOrdenP.ESTR_CODIGO = codigoEstructura;
                        rowOrdenP.SetUSTCK_DESTINONull();
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
                    //rowOrdenP.SetORDPM_NUMERONull();
                    rowOrdenP.ORDP_PRIORIDAD = 0;
                    rowOrdenP.ORDP_OBSERVACIONES = string.Empty;
                    rowOrdenP.COC_CODIGO = rowDetalle.COC_CODIGO;
                    rowOrdenP.ORDP_CANTIDADESTIMADA = rowDetalle.DPSEM_CANTIDADESTIMADA;
                    rowOrdenP.ORDP_CANTIDADREAL = 0;
                    rowOrdenP.ESTR_CODIGO = codigoEstructura;
                    rowOrdenP.SetUSTCK_DESTINONull();
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
        public static void GenerarArbolOrdenesTrabajo(int codigoOrdenProduccion, TreeView tvDependenciaSimple, TreeView tvDependenciaCompleta, TreeView tvOrdenesYEstructura, Data.dsOrdenTrabajo dsOrdenTrabajo)
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
            nodoOrdenOT.Tag = 1;
            tvDependenciaSimple.Nodes.Add(nodoOrdenOT);
            nodoOrdenOTyE.Name = rowOrdenP.ORDP_NUMERO.ToString();
            nodoOrdenOTyE.Text = rowOrdenP.ORDP_ORIGEN;
            nodoOrdenOTyE.Tag = 1;
            tvOrdenesYEstructura.Nodes.Add(nodoOrdenOTyE);
            nodoOrdenDC.Name = rowOrdenP.ORDP_NUMERO.ToString();
            nodoOrdenDC.Text = rowOrdenP.ORDP_ORIGEN;
            nodoOrdenDC.Tag = 1;
            tvDependenciaCompleta.Nodes.Add(nodoOrdenDC);

            TreeNode nodoDetalleOT, nodoDetalleOTyE, nodoDetalleDC, ultimoNodoParte = new TreeNode(), parte, ordenes;
            foreach (Data.dsOrdenTrabajo.ORDENES_TRABAJORow rowOrdenT in rowOrdenP.GetORDENES_TRABAJORows())
            {
                #region Árbol dependencia simple
                nodoDetalleOT = new TreeNode();
                nodoDetalleOT.Name = rowOrdenT.ORDT_NUMERO.ToString();
                nodoDetalleOT.Text = rowOrdenT.ORDT_ORIGEN;
                nodoDetalleOT.Tag = 1;
                /*if (rowOrdenT.IsORDT_ORDENSIGUIENTENull())
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
                }*/
                #endregion

                #region Árbol dependencia completa

                nodoDetalleDC = new TreeNode();
                nodoDetalleDC.Name = rowOrdenT.ORDT_NUMERO.ToString();
                nodoDetalleDC.Text = rowOrdenT.ORDT_ORIGEN;
                nodoDetalleDC.Tag = 0;
                /*if (rowOrdenT.IsORDT_ORDENSIGUIENTENull())
                {
                    nodoOrdenDC.Nodes.Add(nodoDetalleDC);
                }
                else
                {
                    nodoOrdenDC.Nodes.Find(rowOrdenT.ORDT_ORDENSIGUIENTE.ToString(), true)[0].Nodes.Add(nodoDetalleDC);
                }*/

                #endregion

                #region Árbol órdenes y estructura
                nodoDetalleOTyE = new TreeNode();
                nodoDetalleOTyE.Name = rowOrdenT.ORDT_NUMERO.ToString();
                nodoDetalleOTyE.Text = rowOrdenT.ORDT_ORIGEN;
                nodoDetalleOTyE.Tag = 0;
                /*if (rowOrdenT.IsORDT_ORDENSIGUIENTENull())
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
                }*/

                #endregion
            }
            tvDependenciaSimple.ExpandAll();
            tvDependenciaCompleta.ExpandAll();
            tvOrdenesYEstructura.ExpandAll();
        }

        public static void PlanearFechaHaciaAtras(DateTime fechaFinalizacion, ArbolProduccion arbol)
        {
            
        }

        private static void CalcularFechaHaciaAtras()
        {
            
        }
        
        public static void PlanearFechaHaciaDelante(DateTime fechaInicio, ArbolProduccion arbol)
        {
            
        }
        
        private static void CalcularFechaHaciaDelante()
        {
            
        }

        /*private static decimal CalcularTiempoTotal(TreeNode nodo, Data.dsOrdenTrabajo dsOrdenTrabajo, Data.dsEstructura dsEstructura, Data.dsHojaRuta dsHojaRuta)
        {
            if (nodo != null)
            {
                decimal tiempo = 0, tiempoMayor = 0;
                foreach (TreeNode nodoHijo in nodo.Nodes)
                {
                    tiempo = CalcularTiempoTotal(nodoHijo, dsOrdenTrabajo, dsEstructura, dsHojaRuta);
                    if (tiempo > tiempoMayor) { tiempoMayor = tiempo; }
                }
                
                decimal codOperacion = dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(Convert.ToInt32(nodo.Name)).OPR_NUMERO;
                decimal codCentro = dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(Convert.ToInt32(nodo.Name)).CTO_CODIGO;
                decimal horasOperacion = dsHojaRuta.OPERACIONES.FindByOPR_NUMERO(codOperacion).OPR_HORASREQUERIDA;
                decimal cantidad = dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(Convert.ToInt32(nodo.Name)).ORDT_CANTIDADESTIMADA;
                decimal sumar = cantidad * horasOperacion;

                return tiempo + sumar;
            }

            return 0;
        }

        public static void IniciarOrdenProduccion(int numeroOrdenProduccion, DateTime fechaInicioReal, Data.dsOrdenTrabajo dsOrdenTrabajo, Data.dsStock dsStock, Data.dsHojaRuta dsHojaRuta, Data.dsEstructura dsEstructura)
        {
            int codigoMovimiento = -1;
            
            //Iniciamos la orden de producción
            dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).ORDP_FECHAINICIOREAL = fechaInicioReal;
            dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).EORD_CODIGO = EstadoEnProceso;

            //Obtenemos la estructura entera de la cocina a fabricar
            BLL.EstructuraBLL.ObtenerEstructura(Convert.ToInt32(dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).ESTR_CODIGO), dsEstructura, true);
            //Iniciamos las órdenes de trabajo
            string filtro = "ORDP_NUMERO = " + numeroOrdenProduccion + " AND ORDT_FECHAINICIOESTIMADA = MIN (ORDT_FECHAINICIOESTIMADA)";
            decimal ultimaParte = 0, stockDestino = 0, cantidadDestino = 0;
            foreach (Data.dsOrdenTrabajo.ORDENES_TRABAJORow rowOT in (Data.dsOrdenTrabajo.ORDENES_TRABAJORow[])dsOrdenTrabajo.ORDENES_TRABAJO.Select(filtro))
	        {
                //Cambiamos el estado de la orden de trabajo a EnProceso y anotamos la fecha real de inicio
                rowOT.EORD_CODIGO = EstadoEnProceso;
                rowOT.ORDT_FECHAINICIOREAL = fechaInicioReal;
                
                //Realizamos los movimientos de stock correspondientes y actualizamos las cantidades
                //Como son las órdenes de la/s primera/s operación/es de la parte tenemos que descontar el stock de cada
                //materia prima que usa cada parte, que sería si o si una pieza en este punto de ejecución, pero sólo de la primer
                //operación de la hoja de ruta
                if (rowOT.PAR_CODIGO != ultimaParte)
                {                    
                    foreach (Data.dsEstructura.MATERIASPRIMASXPIEZARow rowMPxP in dsEstructura.PIEZAS.FindByPZA_CODIGO(rowOT.PAR_CODIGO).GetMATERIASPRIMASXPIEZARows())
                    {
                        //Creamos un movimiento de stock para cada materia prima según su ubicación de stock
                        if (!rowMPxP.MATERIAS_PRIMASRow.IsUSTCK_NUMERONull())
                        {
                            Data.dsStock.MOVIMIENTOS_STOCKRow rowMovimiento = dsStock.MOVIMIENTOS_STOCK.NewMOVIMIENTOS_STOCKRow();
                            rowMovimiento.BeginEdit();
                            rowMovimiento.MVTO_NUMERO = codigoMovimiento--;
                            //rowMovimiento.EMVTO_CODIGO = EstadoMovimientoStockBLL.EstadoPlanificado;
                            rowMovimiento.MVTO_CODIGO = "MSA/OT- " + rowOT.ORDT_CODIGO;
                            rowMovimiento.MVTO_DESCRIPCION = "Automático. Origen: Orden de Trabajo.";
                            rowMovimiento.MVTO_FECHAALTA = DBBLL.GetFechaServidor();
                            rowMovimiento.MVTO_FECHAPREVISTA = rowOT.ORDT_FECHAFINESTIMADA;
                            rowMovimiento.SetMVTO_FECHAREALNull();
                            //rowMovimiento.USTCK_ORIGEN = rowMPxP.MATERIAS_PRIMASRow.USTCK_NUMERO;
                            //if (!rowOT.IsUSTCK_DESTINONull()) { rowMovimiento.USTCK_DESTINO = rowOT.USTCK_DESTINO; }
                            //else 
                            //{
                                //Data.dsOrdenTrabajo.ORDENES_TRABAJORow rowOTTemp = rowOT;
                                //while (rowOTTemp.PAR_CODIGO == rowOTTemp.ORDENES_TRABAJORowParent.PAR_CODIGO && rowOTTemp.PAR_TIPO == rowOTTemp.ORDENES_TRABAJORowParent.PAR_TIPO && rowOTTemp.ORDENES_TRABAJORowParent.IsUSTCK_DESTINONull())
                                //{
                                //    rowOTTemp = rowOTTemp.ORDENES_TRABAJORowParent;
                                //}
                                //rowMovimiento.USTCK_DESTINO = rowOTTemp.ORDENES_TRABAJORowParent.USTCK_DESTINO;
                            //}
                            rowMovimiento.MVTO_CANTIDAD_ORIGEN_ESTIMADA = rowMPxP.MPXP_CANTIDAD * rowOT.ORDT_CANTIDADESTIMADA;
                            rowMovimiento.MVTO_CANTIDAD_DESTINO_ESTIMADA = rowOT.ORDT_CANTIDADESTIMADA;
                            rowMovimiento.MVTO_CANTIDAD_ORIGEN_REAL = 0;
                            rowMovimiento.MVTO_CANTIDAD_DESTINO_REAL = 0;
                            //rowMovimiento.ORDT_NUMERO = rowOT.ORDT_NUMERO;
                            rowMovimiento.EndEdit();
                            dsStock.MOVIMIENTOS_STOCK.AddMOVIMIENTOS_STOCKRow(rowMovimiento);

                            //Actualizamos los datos de la ubicación de stock origen
                            //dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(rowMovimiento.USTCK_ORIGEN).USTCK_CANTIDADVIRTUAL -= rowMovimiento.MVTO_CANTIDAD_ORIGEN_ESTIMADA;
                            //if (!rowMovimiento.IsUSTCK_DESTINONull())
                            ///{
                            //    stockDestino = rowMovimiento.USTCK_DESTINO;
                           //     cantidadDestino = rowMovimiento.MVTO_CANTIDAD_DESTINO_ESTIMADA;
                            //}
                        }
                    }
                    //Actualizamos los datos de la ubicación de stock destino, que para todos los casos anteriores es el mismo
                    //dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(stockDestino).USTCK_CANTIDADVIRTUAL += cantidadDestino;
                    ultimaParte = rowOT.PAR_CODIGO;
                }
	        }

            //Actualizamos el estado de las demás órdenes de trabajo de la orden de producción a EnEspera
            foreach (Data.dsOrdenTrabajo.ORDENES_TRABAJORow row in (Data.dsOrdenTrabajo.ORDENES_TRABAJORow[])dsOrdenTrabajo.ORDENES_TRABAJO.Select("ORDP_NUMERO = " + numeroOrdenProduccion))
            {
                if (row.EORD_CODIGO == EstadoGenerado) { row.EORD_CODIGO = EstadoEnEspera; }
            }
            
            //Ahora mandamos todo a la BD
            DAL.OrdenProduccionDAL.IniciarOrdenProduccion(numeroOrdenProduccion, dsOrdenTrabajo, dsStock);
        }*/

        public static void FinalizarOrdenProduccion(int numeroOrdenProduccion, Data.dsOrdenTrabajo dsOrdenTrabajo, Data.dsStock dsStock)
        {
            DAL.OrdenProduccionDAL.FinalizarOrdenProduccion(numeroOrdenProduccion, dsOrdenTrabajo, dsStock);
        }

        public static SortableBindingList<ArbolProduccion> GenerarOrdenesProduccion(int codigoDia, Data.dsPlanSemanal dsPlanSemanal, IList<Cocina> listaCocinas)
        {
            int numeroOrdenProduccion = 1;
            SortableBindingList<ArbolProduccion> ordenesProduccion = new SortableBindingList<ArbolProduccion>();
            EstadoOrdenTrabajo estadoOrden = EstadoOrdenTrabajoBLL.GetEstado(OrdenesTrabajoEnum.EstadoOrdenEnum.Generada);

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
                        //TODO: crear excepcion Cocina Sin Estructura
                    }

                    ordenesProduccion.Add(new ArbolProduccion()
                    {
                        OrdenProduccion = new OrdenProduccion()
                                            {
                                                Numero = numeroOrdenProduccion,
                                                Codigo = string.Concat("OPA", numeroOrdenProduccion),
                                                Estado = estadoOrden,
                                                FechaAlta = DBBLL.GetFechaServidor(),
                                                DetallePlanSemanal = new DetallePlanSemanal() { Codigo = Convert.ToInt32(rowDetalle.DPSEM_CODIGO) },
                                                Origen = string.Concat("OPA", numeroOrdenProduccion, " - ", rowDetalle.COCINASRow.COC_CODIGO_PRODUCTO),
                                                FechaInicioReal = null,
                                                FechaFinReal = null,
                                                FechaInicioEstimada = null,
                                                FechaFinEstimada = null,
                                                Prioridad = 0,
                                                Observaciones = string.Empty,
                                                Cocina = listaCocinas.First(p => p.CodigoCocina == Convert.ToInt32(rowDetalle.COC_CODIGO)),
                                                CantidadEstimada = Convert.ToInt32(rowDetalle.DPSEM_CANTIDADESTIMADA),
                                                CantidadReal = 0,
                                                Estructura = 0
                                            },
                        OrdenesTrabajo = new List<NodoOrdenTrabajo>()
                    });
                    rowDetalle.BeginEdit();
                    rowDetalle.DPSEM_ESTADO = BLL.DetallePlanSemanalBLL.estadoConOrden;
                    rowDetalle.EndEdit();                    
                }
                numeroOrdenProduccion++;
            }

            return ordenesProduccion;
        }
        
        public static ArbolProduccion GetArbolProduccion()
        {
            return new ArbolProduccion();
        }
    }
}
