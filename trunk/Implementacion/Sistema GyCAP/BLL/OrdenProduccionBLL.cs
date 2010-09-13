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

        public static void Insertar(Data.dsOrdenTrabajo dsOrdenTrabajo)
        {
            DAL.OrdenProduccionDAL.Insertar(dsOrdenTrabajo);
        }
        
        public static string GetTipoParte(int tipo)
        {
            if (tipo == parteTipoConjunto) return "Conjunto";
            if (tipo == parteTipoSubconjunto) return "Subconjunto";
            if (tipo == parteTipoPieza) return "Pieza";
            if (tipo == parteTipoMateriaPrima) return "Materia Prima";
            return string.Empty;
        }
        
        public static void GenerarOrdenTrabajoDia(int codigoDia, Data.dsPlanSemanal dsPlanSemanal, Data.dsOrdenTrabajo dsOrdenTrabajo, Data.dsEstructura dsEstructura, Data.dsHojaRuta dsHojaRuta)
        {
            //Declaramos las variables necesarias
            int codigoOrden = -1, codigoDetalleOrden = -1;
                        
            //Recorremos todo el detalle del día
            foreach (Data.dsPlanSemanal.DETALLE_PLANES_SEMANALESRow rowDetalle in (Data.dsPlanSemanal.DETALLE_PLANES_SEMANALESRow[])dsPlanSemanal.DETALLE_PLANES_SEMANALES.Select("diapsem_codigo = " + codigoDia))
            {
                //Primero armamos lasórdenes de producción que corresponden uno a uno con el detalle del día
                Data.dsOrdenTrabajo.ORDENES_PRODUCCIONRow rowOrdenP = dsOrdenTrabajo.ORDENES_PRODUCCION.NewORDENES_PRODUCCIONRow();
                rowOrdenP.BeginEdit();
                rowOrdenP.ORDP_NUMERO = codigoOrden--;
                rowOrdenP.ORDP_CODIGO = "codigoOrden";
                rowOrdenP.EORD_CODIGO = (dsOrdenTrabajo.ESTADO_ORDENES_TRABAJO.Select("EORD_NOMBRE = 'Generada'") as Data.dsOrdenTrabajo.ESTADO_ORDENES_TRABAJORow[])[0].EORD_CODIGO;
                rowOrdenP.ORDP_FECHAALTA = DBBLL.GetFechaServidor();
                rowOrdenP.DPSEM_CODIGO = rowDetalle.DPSEM_CODIGO;
                rowOrdenP.ORDP_ORIGEN = "OA-" + rowDetalle.COCINASRow.COC_CODIGO_PRODUCTO;
                rowOrdenP.SetORDP_FECHAINICIOREALNull();
                rowOrdenP.SetORDP_FECHAFINREALNull();
                rowOrdenP.SetORDPM_NUMERONull();
                rowOrdenP.ORDP_PRIORIDAD = 0;
                rowOrdenP.ORDP_OBSERVACIONES = string.Empty;
                
                //Obtenemos la estructura completa de la cocina y la cantidad pedida
                int codigoEstructura = CocinaBLL.ObtenerCodigoEstructuraActiva(Convert.ToInt32(rowDetalle.COC_CODIGO));

                rowOrdenP.ESTR_CODIGO = codigoEstructura;
                rowOrdenP.EndEdit();
                dsOrdenTrabajo.ORDENES_PRODUCCION.AddORDENES_PRODUCCIONRow(rowOrdenP);
                rowDetalle.BeginEdit();
                rowDetalle.DPSEM_ESTADO = BLL.DetallePlanSemanalBLL.estadoConOrden;
                rowDetalle.EndEdit();
                
                string mensaje = string.Empty;
                if (codigoEstructura == 0)
                {
                    mensaje = "La Cocina " + rowDetalle.COCINASRow.COC_CODIGO_PRODUCTO + " no posee una Estructura activa.";
                    throw new Entidades.Excepciones.OrdenTrabajoException(mensaje); 
                }
                EstructuraBLL.ObtenerEstructura(codigoEstructura, dsEstructura, true);
                
                //Ahora necesitamos las hojas de ruta de cada parte de la estructura
                #region Obtener Hojas Rutas
                foreach (Data.dsEstructura.CONJUNTOSXESTRUCTURARow rowCxE in (Data.dsEstructura.CONJUNTOSXESTRUCTURARow[])dsEstructura.CONJUNTOSXESTRUCTURA.Select("estr_codigo = " + codigoEstructura))
                {
                    if (rowCxE.CONJUNTOSRow.IsHR_CODIGONull())
                    {
                        mensaje = "El Conjunto: " + rowCxE.CONJUNTOSRow.CONJ_CODIGOPARTE + " no posee una Hoja de Ruta.";
                        throw new Entidades.Excepciones.OrdenTrabajoException(mensaje);
                    }
                    DAL.HojaRutaDAL.ObtenerHojaRuta(Convert.ToInt32(rowCxE.CONJUNTOSRow.HR_CODIGO), true, dsHojaRuta);

                    foreach (Data.dsEstructura.SUBCONJUNTOSXCONJUNTORow rowSCxC in rowCxE.CONJUNTOSRow.GetSUBCONJUNTOSXCONJUNTORows())
                    {
                        if (rowSCxC.SUBCONJUNTOSRow.IsHR_CODIGONull())
                        {
                            mensaje = "El Subconjunto: " + rowSCxC.SUBCONJUNTOSRow.SCONJ_CODIGOPARTE + " no posee una Hoja de Ruta.";
                            throw new Entidades.Excepciones.OrdenTrabajoException(mensaje);
                        }
                        DAL.HojaRutaDAL.ObtenerHojaRuta(Convert.ToInt32(rowSCxC.SUBCONJUNTOSRow.HR_CODIGO), true, dsHojaRuta);

                        foreach (Data.dsEstructura.PIEZASXSUBCONJUNTORow rowPxSC in rowSCxC.SUBCONJUNTOSRow.GetPIEZASXSUBCONJUNTORows())
                        {
                            if (rowPxSC.PIEZASRow.IsHR_CODIGONull())
                            {
                                mensaje = "La Pieza: " + rowPxSC.PIEZASRow.PZA_CODIGOPARTE + " no posee una Hoja de Ruta.";
                                throw new Entidades.Excepciones.OrdenTrabajoException(mensaje);
                            }
                            DAL.HojaRutaDAL.ObtenerHojaRuta(Convert.ToInt32(rowPxSC.PIEZASRow.HR_CODIGO), true, dsHojaRuta);
                        }
                    }
                }

                foreach (Data.dsEstructura.PIEZASXESTRUCTURARow rowPxE in (Data.dsEstructura.PIEZASXESTRUCTURARow[])dsEstructura.PIEZASXESTRUCTURA.Select("estr_codigo = " + codigoEstructura))
                {
                    if (rowPxE.PIEZASRow.IsHR_CODIGONull())
                    {
                        mensaje = "La Pieza: " + rowPxE.PIEZASRow.PZA_CODIGOPARTE + " no posee una Hoja de Ruta.";
                        throw new Entidades.Excepciones.OrdenTrabajoException(mensaje);
                    }
                    DAL.HojaRutaDAL.ObtenerHojaRuta(Convert.ToInt32(rowPxE.PIEZASRow.HR_CODIGO), true, dsHojaRuta);
                }
                #endregion
                
                //Creamos las órdenes de trabajo para cada orden de producción
                //Calculamos las cantidades necesarias de cada cosa y al mismo tiempo el tiempo necesario para producir todo
                //Empezamos por las partes de primer nivel que dependen de estructura, bajamos hasta el último nivel y desde ahí
                //empezamos a crear las órdenes de trabajo, primero los conjuntos que forman la estructura
                decimal ordenSiguiente = 0, ultimaOrdenConjunto = 0, nivel = 0, ultimaOrdenSubconjunto = 0;
                foreach (Data.dsEstructura.CONJUNTOSXESTRUCTURARow rowCxE in dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).GetCONJUNTOSXESTRUCTURARows())
                {
                    ordenSiguiente = 0;
                    foreach (Data.dsHojaRuta.DETALLE_HOJARUTARow rowHojaCxE in dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(rowCxE.CONJUNTOSRow.HR_CODIGO).GetDETALLE_HOJARUTARows())
                    {
                        Data.dsOrdenTrabajo.ORDENES_TRABAJORow rowOTCxE = dsOrdenTrabajo.ORDENES_TRABAJO.NewORDENES_TRABAJORow();
                        rowOTCxE.BeginEdit();
                        rowOTCxE.ORDT_NUMERO = codigoDetalleOrden--;
                        rowOTCxE.ORDT_CODIGO = "código detalle";
                        rowOTCxE.ORDT_ORIGEN = rowOrdenP.ORDP_ORIGEN + "/" + rowCxE.CONJUNTOSRow.CONJ_CODIGOPARTE;
                        rowOTCxE.EORD_CODIGO = (dsOrdenTrabajo.ESTADO_ORDENES_TRABAJO.Select("EORD_NOMBRE = 'Generada'") as Data.dsOrdenTrabajo.ESTADO_ORDENES_TRABAJORow[])[0].EORD_CODIGO;
                        rowOTCxE.ORDP_NUMERO = rowOrdenP.ORDP_NUMERO;
                        rowOTCxE.PAR_CODIGO = rowCxE.CONJUNTOSRow.CONJ_CODIGO;
                        rowOTCxE.PAR_TIPO = parteTipoConjunto;
                        rowOTCxE.ESTR_CODIGO = codigoEstructura;
                        rowOTCxE.ORDT_OBSERVACIONES = string.Empty;
                        rowOTCxE.ORDT_CANTIDADESTIMADA = rowDetalle.DPSEM_CANTIDADESTIMADA * rowCxE.CXE_CANTIDAD;
                        rowOTCxE.CTO_CODIGO = rowHojaCxE.CTO_CODIGO;
                        rowOTCxE.OPR_NUMERO = rowHojaCxE.OPR_NUMERO;
                        rowOTCxE.ORDT_OBSERVACIONES = string.Empty;
                        rowOTCxE.ORDT_ORDENPRECEDENTE = 0;
                        if (ordenSiguiente == 0) { rowOTCxE.SetORDT_ORDENSIGUIENTENull(); }
                        else { rowOTCxE.ORDT_ORDENSIGUIENTE = ordenSiguiente; }
                        rowOTCxE.ORDT_NIVEL = 0;
                        rowOTCxE.EndEdit();
                        dsOrdenTrabajo.ORDENES_TRABAJO.AddORDENES_TRABAJORow(rowOTCxE);
                        ordenSiguiente = rowOTCxE.ORDT_NUMERO;
                        ultimaOrdenConjunto = ordenSiguiente;
                    }
                    
                    foreach (Data.dsEstructura.SUBCONJUNTOSXCONJUNTORow rowSCxC in dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(rowCxE.CONJ_CODIGO).GetSUBCONJUNTOSXCONJUNTORows())
                    {
                        nivel = 1;
                        ordenSiguiente = ultimaOrdenConjunto;
                        foreach (Data.dsHojaRuta.DETALLE_HOJARUTARow rowHojaSCxC in dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(rowSCxC.SUBCONJUNTOSRow.HR_CODIGO).GetDETALLE_HOJARUTARows())
                        {
                            Data.dsOrdenTrabajo.ORDENES_TRABAJORow rowOTSCxC = dsOrdenTrabajo.ORDENES_TRABAJO.NewORDENES_TRABAJORow();
                            rowOTSCxC.BeginEdit();
                            rowOTSCxC.ORDT_NUMERO = codigoDetalleOrden--;
                            rowOTSCxC.ORDT_CODIGO = "código detalle";
                            rowOTSCxC.ORDT_ORIGEN = dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(ultimaOrdenConjunto).ORDT_ORIGEN + "/" + rowSCxC.SUBCONJUNTOSRow.SCONJ_CODIGOPARTE;
                            rowOTSCxC.EORD_CODIGO = (dsOrdenTrabajo.ESTADO_ORDENES_TRABAJO.Select("EORD_NOMBRE = 'Generada'") as Data.dsOrdenTrabajo.ESTADO_ORDENES_TRABAJORow[])[0].EORD_CODIGO;
                            rowOTSCxC.ORDP_NUMERO = rowOrdenP.ORDP_NUMERO;
                            rowOTSCxC.PAR_CODIGO = rowSCxC.SUBCONJUNTOSRow.SCONJ_CODIGO;
                            rowOTSCxC.PAR_TIPO = parteTipoSubconjunto;
                            rowOTSCxC.ESTR_CODIGO = codigoEstructura;
                            rowOTSCxC.ORDT_OBSERVACIONES = string.Empty;
                            rowOTSCxC.ORDT_CANTIDADESTIMADA = rowDetalle.DPSEM_CANTIDADESTIMADA * rowCxE.CXE_CANTIDAD * rowSCxC.SCXCJ_CANTIDAD;
                            rowOTSCxC.CTO_CODIGO = rowHojaSCxC.CTO_CODIGO;
                            rowOTSCxC.OPR_NUMERO = rowHojaSCxC.OPR_NUMERO;
                            if (ordenSiguiente == 0) { rowOTSCxC.SetORDT_ORDENSIGUIENTENull(); }
                            else { rowOTSCxC.ORDT_ORDENSIGUIENTE = ordenSiguiente; }
                            rowOTSCxC.ORDT_ORDENPRECEDENTE = 0;
                            rowOTSCxC.ORDT_NIVEL = nivel;
                            rowOTSCxC.EndEdit();
                            dsOrdenTrabajo.ORDENES_TRABAJO.AddORDENES_TRABAJORow(rowOTSCxC);
                            ordenSiguiente = rowOTSCxC.ORDT_NUMERO;
                            ultimaOrdenSubconjunto = rowOTSCxC.ORDT_NUMERO;
                            nivel = 0;
                        }
                        
                        foreach (Data.dsEstructura.PIEZASXSUBCONJUNTORow rowPxSC in dsEstructura.SUBCONJUNTOS.FindBySCONJ_CODIGO(rowSCxC.SCONJ_CODIGO).GetPIEZASXSUBCONJUNTORows())
                        {
                            //Ahora generamos un detalle para cada par operación-centro de la hoja de ruta de la parte
                            nivel = 1;
                            ordenSiguiente = ultimaOrdenSubconjunto;
                            foreach (Data.dsHojaRuta.DETALLE_HOJARUTARow rowHojaPxSC in dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(rowPxSC.PIEZASRow.HR_CODIGO).GetDETALLE_HOJARUTARows())
                            {
                                Data.dsOrdenTrabajo.ORDENES_TRABAJORow rowOTPxSC = dsOrdenTrabajo.ORDENES_TRABAJO.NewORDENES_TRABAJORow();
                                rowOTPxSC.BeginEdit();
                                rowOTPxSC.ORDT_NUMERO = codigoDetalleOrden--;
                                rowOTPxSC.ORDT_CODIGO = "código detalle";
                                rowOTPxSC.ORDT_ORIGEN = dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(ultimaOrdenSubconjunto).ORDT_ORIGEN + "/" + rowPxSC.PIEZASRow.PZA_CODIGOPARTE;
                                rowOTPxSC.EORD_CODIGO = (dsOrdenTrabajo.ESTADO_ORDENES_TRABAJO.Select("EORD_NOMBRE = 'Generada'") as Data.dsOrdenTrabajo.ESTADO_ORDENES_TRABAJORow[])[0].EORD_CODIGO;
                                rowOTPxSC.ORDP_NUMERO = rowOrdenP.ORDP_NUMERO;
                                rowOTPxSC.PAR_CODIGO = rowPxSC.PZA_CODIGO;
                                rowOTPxSC.PAR_TIPO = parteTipoPieza;
                                rowOTPxSC.ESTR_CODIGO = codigoEstructura;
                                rowOTPxSC.ORDT_OBSERVACIONES = string.Empty;
                                rowOTPxSC.ORDT_CANTIDADESTIMADA = rowDetalle.DPSEM_CANTIDADESTIMADA * rowCxE.CXE_CANTIDAD * rowSCxC.SCXCJ_CANTIDAD * rowPxSC.PXSC_CANTIDAD;
                                rowOTPxSC.CTO_CODIGO = rowHojaPxSC.CTO_CODIGO;
                                rowOTPxSC.OPR_NUMERO = rowHojaPxSC.OPR_NUMERO;
                                if (ordenSiguiente == 0) { rowOTPxSC.SetORDT_ORDENSIGUIENTENull(); }
                                else { rowOTPxSC.ORDT_ORDENSIGUIENTE = ordenSiguiente; }
                                rowOTPxSC.ORDT_ORDENPRECEDENTE = 0;
                                rowOTPxSC.ORDT_NIVEL = nivel;
                                rowOTPxSC.EndEdit();
                                dsOrdenTrabajo.ORDENES_TRABAJO.AddORDENES_TRABAJORow(rowOTPxSC);
                                ordenSiguiente = rowOTPxSC.ORDT_NUMERO;
                                nivel = 0;
                            }                                
                        }
                    }

                    ordenSiguiente = ultimaOrdenConjunto;
                    foreach (Data.dsEstructura.PIEZASXCONJUNTORow rowPxC in dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(rowCxE.CONJ_CODIGO).GetPIEZASXCONJUNTORows())
                    {                        
                        nivel = 1;
                        ordenSiguiente = ultimaOrdenConjunto;
                        foreach (Data.dsHojaRuta.DETALLE_HOJARUTARow rowHojaPxC in dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(rowPxC.PIEZASRow.HR_CODIGO).GetDETALLE_HOJARUTARows())
                        {
                            Data.dsOrdenTrabajo.ORDENES_TRABAJORow rowOTPxC = dsOrdenTrabajo.ORDENES_TRABAJO.NewORDENES_TRABAJORow();
                            rowOTPxC.BeginEdit();
                            rowOTPxC.ORDT_NUMERO = codigoDetalleOrden--;
                            rowOTPxC.ORDT_CODIGO = "código detalle";
                            rowOTPxC.ORDT_ORIGEN = dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(ultimaOrdenConjunto).ORDT_ORIGEN + "/" + rowPxC.PIEZASRow.PZA_CODIGOPARTE;
                            rowOTPxC.EORD_CODIGO = (dsOrdenTrabajo.ESTADO_ORDENES_TRABAJO.Select("EORD_NOMBRE = 'Generada'") as Data.dsOrdenTrabajo.ESTADO_ORDENES_TRABAJORow[])[0].EORD_CODIGO;
                            rowOTPxC.ORDP_NUMERO = rowOrdenP.ORDP_NUMERO;
                            rowOTPxC.PAR_CODIGO = rowCxE.CONJUNTOSRow.CONJ_CODIGO;
                            rowOTPxC.PAR_TIPO = parteTipoConjunto;
                            rowOTPxC.ESTR_CODIGO = codigoEstructura;
                            rowOTPxC.ORDT_OBSERVACIONES = string.Empty;
                            rowOTPxC.ORDT_CANTIDADESTIMADA = rowDetalle.DPSEM_CANTIDADESTIMADA * rowCxE.CXE_CANTIDAD * rowPxC.PXCJ_CANTIDAD;
                            rowOTPxC.CTO_CODIGO = rowHojaPxC.CTO_CODIGO;
                            rowOTPxC.OPR_NUMERO = rowHojaPxC.OPR_NUMERO;
                            if (ordenSiguiente == 0) { rowOTPxC.SetORDT_ORDENSIGUIENTENull(); }
                            else { rowOTPxC.ORDT_ORDENSIGUIENTE = ordenSiguiente; }
                            rowOTPxC.ORDT_ORDENPRECEDENTE = 0;
                            rowOTPxC.ORDT_NIVEL = nivel;
                            rowOTPxC.EndEdit();
                            dsOrdenTrabajo.ORDENES_TRABAJO.AddORDENES_TRABAJORow(rowOTPxC);
                            ordenSiguiente = rowOTPxC.ORDT_NUMERO;
                            nivel = 0;
                        }
                    }
                }
                //Ahora las piezas que forman la estructura
                //ordenSiguiente = 0;
                foreach (Data.dsEstructura.PIEZASXESTRUCTURARow rowPxE in dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).GetPIEZASXESTRUCTURARows())
                {
                    ordenSiguiente = 0;
                    foreach (Data.dsHojaRuta.DETALLE_HOJARUTARow rowHojaPxE in dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(rowPxE.PIEZASRow.HR_CODIGO).GetDETALLE_HOJARUTARows())
                    {
                        Data.dsOrdenTrabajo.ORDENES_TRABAJORow rowOTPxE = dsOrdenTrabajo.ORDENES_TRABAJO.NewORDENES_TRABAJORow();
                        rowOTPxE.BeginEdit();
                        rowOTPxE.ORDT_NUMERO = codigoDetalleOrden--;
                        rowOTPxE.ORDT_CODIGO = "código detalle";
                        rowOTPxE.ORDT_ORIGEN = rowOrdenP.ORDP_ORIGEN + "/" + rowPxE.PIEZASRow.PZA_CODIGOPARTE;
                        rowOTPxE.EORD_CODIGO = (dsOrdenTrabajo.ESTADO_ORDENES_TRABAJO.Select("EORD_NOMBRE = 'Generada'") as Data.dsOrdenTrabajo.ESTADO_ORDENES_TRABAJORow[])[0].EORD_CODIGO;
                        rowOTPxE.ORDP_NUMERO = rowOrdenP.ORDP_NUMERO;
                        rowOTPxE.PAR_CODIGO = rowPxE.PZA_CODIGO;
                        rowOTPxE.PAR_TIPO = parteTipoPieza;
                        rowOTPxE.ESTR_CODIGO = codigoEstructura;
                        rowOTPxE.ORDT_OBSERVACIONES = string.Empty;
                        rowOTPxE.ORDT_CANTIDADESTIMADA = rowDetalle.DPSEM_CANTIDADESTIMADA * rowPxE.PXE_CANTIDAD;
                        rowOTPxE.CTO_CODIGO = rowHojaPxE.CTO_CODIGO;
                        rowOTPxE.OPR_NUMERO = rowHojaPxE.OPR_NUMERO;
                        if (ordenSiguiente == 0) { rowOTPxE.SetORDT_ORDENSIGUIENTENull(); }
                        else { rowOTPxE.ORDT_ORDENSIGUIENTE = ordenSiguiente; }
                        rowOTPxE.ORDT_ORDENPRECEDENTE = 0;
                        rowOTPxE.ORDT_NIVEL = 0;
                        rowOTPxE.EndEdit();
                        dsOrdenTrabajo.ORDENES_TRABAJO.AddORDENES_TRABAJORow(rowOTPxE);
                        ordenSiguiente = rowOTPxE.ORDT_NUMERO;
                    }
                }
            }
        }

        public static void GenerarArbolOrdenes(int codigoOrdenProduccion, TreeView tvDependenciaSimple, TreeView tvDependenciaCompleta, TreeView tvOrdenesYEstructura, Data.dsOrdenTrabajo dsOrdenTrabajo, Data.dsEstructura dsEstructura, Data.dsHojaRuta dsHojaRuta)
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
