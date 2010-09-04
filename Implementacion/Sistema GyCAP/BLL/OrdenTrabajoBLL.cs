﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace GyCAP.BLL
{
    public class OrdenTrabajoBLL
    {
        public static readonly int parteTipoConjunto = 1;
        public static readonly int parteTipoSubconjunto = 2;
        public static readonly int parteTipoPieza = 3;
        public static readonly int parteTipoMateriaPrima = 4;
        
        public static void GenerarOrdenTrabajoDia(int codigoDia, Data.dsPlanSemanal dsPlanSemanal, Data.dsOrdenTrabajo dsOrdenTrabajo, Data.dsEstructura dsEstructura, Data.dsHojaRuta dsHojaRuta)
        {
            //Declaramos las variables necesarias
            int codigoOrden = -1, codigoDetalleOrden = -1;
            decimal tiempoTotal = 0;
                        
            //Recorremos todo el detalle del día
            foreach (Data.dsPlanSemanal.DETALLE_PLANES_SEMANALESRow rowDetalle in (Data.dsPlanSemanal.DETALLE_PLANES_SEMANALESRow[])dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.Select("diapsem_codigo = " + codigoDia))
            {
                //Primero armamos la cabecera que corresponde uno a uno con el detalle del día
                Data.dsOrdenTrabajo.ORDENES_TRABAJORow rowOrden = dsOrdenTrabajo.ORDENES_TRABAJO.NewORDENES_TRABAJORow();
                rowOrden.BeginEdit();
                rowOrden.ORD_NUMERO = codigoOrden--;
                rowOrden.ORD_CODIGO = "codigoOrden";
                rowOrden.EORD_CODIGO = (dsOrdenTrabajo.ESTADO_ORDENES_TRABAJO.Select("EORD_NOMBRE = 'Generada'") as Data.dsOrdenTrabajo.ESTADO_ORDENES_TRABAJORow[])[0].EORD_CODIGO;
                rowOrden.ORD_FECHAALTA = DBBLL.GetFechaServidor();
                rowOrden.DPSEM_CODIGO = rowDetalle.DPSEM_CODIGO;
                rowOrden.ORD_ORIGEN = "OA-" + rowDetalle.COCINASRow.COC_CODIGO_PRODUCTO;
                rowOrden.ORD_FECHAINICIOESTIMADA = DateTime.Today;
                rowOrden.ORD_FECHAFINESTIMADA = DateTime.Today;
                rowOrden.SetORD_FECHAINICIOREALNull();
                rowOrden.SetORD_FECHAFINREALNull();
                rowOrden.SetORDM_NUMERONull();
                rowOrden.ORD_PRIORIDAD = 0;
                rowOrden.ORD_OBSERVACIONES = string.Empty;
                rowOrden.EndEdit();
                dsOrdenTrabajo.ORDENES_TRABAJO.AddORDENES_TRABAJORow(rowOrden);

                //Ahora creamos el detalle de la orden, necesitamos la estructura completa de la cocina y la cantidad pedida
                int codigoEstructura = CocinaBLL.ObtenerCodigoEstructuraActiva(Convert.ToInt32(rowDetalle.COC_CODIGO));
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
                
                //Creamos el detalle de la orden de trabajo
                //Calculamos las cantidades necesarias de cada cosa y al mismo tiempo el tiempo necesario para producir todo
                //Empezamos por las partes de primer nivel que dependen de estructura, bajamos hasta el último nivel y desde ahí
                //empezamos a crear los detalles, primero los conjuntos que forman la estructura
                decimal ordenPrecedente = 0, cantidadOrdenes = 0;
                foreach (Data.dsEstructura.CONJUNTOSXESTRUCTURARow rowCxE in dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).GetCONJUNTOSXESTRUCTURARows())
                {
                    ordenPrecedente = 0;
                    cantidadOrdenes = dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(rowCxE.CONJUNTOSRow.HR_CODIGO).GetDETALLE_HOJARUTARows().Length;
                    foreach (Data.dsHojaRuta.DETALLE_HOJARUTARow rowHojaCxE in dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(rowCxE.CONJUNTOSRow.HR_CODIGO).GetDETALLE_HOJARUTARows())
                    {
                        cantidadOrdenes--;
                        Data.dsOrdenTrabajo.DETALLE_ORDENES_TRABAJORow rowDOCxE = dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.NewDETALLE_ORDENES_TRABAJORow();
                        rowDOCxE.BeginEdit();
                        rowDOCxE.DORD_NUMERO = codigoDetalleOrden--;
                        rowDOCxE.DORD_CODIGO = "código detalle";
                        rowDOCxE.DORD_ORIGEN = rowOrden.ORD_ORIGEN;
                        rowDOCxE.EORD_CODIGO = (dsOrdenTrabajo.ESTADO_ORDENES_TRABAJO.Select("EORD_NOMBRE = 'Generada'") as Data.dsOrdenTrabajo.ESTADO_ORDENES_TRABAJORow[])[0].EORD_CODIGO;
                        rowDOCxE.ORD_NUMERO = rowOrden.ORD_NUMERO;
                        rowDOCxE.PAR_CODIGO = rowCxE.CONJUNTOSRow.CONJ_CODIGO;
                        rowDOCxE.PAR_TIPO = parteTipoConjunto;
                        rowDOCxE.ESTR_CODIGO = codigoEstructura;
                        rowDOCxE.DORD_OBSERVACIONES = string.Empty;
                        rowDOCxE.DORD_CANTIDADESTIMADA = rowDetalle.DPSEM_CANTIDADESTIMADA * rowCxE.CXE_CANTIDAD;
                        rowDOCxE.CTO_CODIGO = rowHojaCxE.CTO_CODIGO;
                        rowDOCxE.OPR_NUMERO = rowHojaCxE.OPR_NUMERO;
                        rowDOCxE.DORD_OBSERVACIONES = string.Empty;
                        if (ordenPrecedente == 0) { rowDOCxE.SetDORD_ORDENPRECEDENTENull(); }
                        else 
                        { 
                            rowDOCxE.DORD_ORDENPRECEDENTE = ordenPrecedente;
                            dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.FindByDORD_NUMERO(ordenPrecedente).DORD_ORDENSIGUIENTE = rowDOCxE.DORD_NUMERO;
                        }
                        if (cantidadOrdenes == 0) { rowDOCxE.SetDORD_ORDENSIGUIENTENull(); }
                        rowDOCxE.DORD_NIVEL = 0;
                        rowDOCxE.EndEdit();
                        dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.AddDETALLE_ORDENES_TRABAJORow(rowDOCxE);
                        ordenPrecedente = rowDOCxE.DORD_NUMERO;                        
                    }
                    
                    foreach (Data.dsEstructura.SUBCONJUNTOSXCONJUNTORow rowSCxC in dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(rowCxE.CONJ_CODIGO).GetSUBCONJUNTOSXCONJUNTORows())
                    {
                        cantidadOrdenes = dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(rowSCxC.SUBCONJUNTOSRow.HR_CODIGO).GetDETALLE_HOJARUTARows().Length;
                        foreach (Data.dsHojaRuta.DETALLE_HOJARUTARow rowHojaSCxC in dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(rowSCxC.SUBCONJUNTOSRow.HR_CODIGO).GetDETALLE_HOJARUTARows())
                        {
                            cantidadOrdenes--;
                            Data.dsOrdenTrabajo.DETALLE_ORDENES_TRABAJORow rowDOSCxC = dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.NewDETALLE_ORDENES_TRABAJORow();
                            rowDOSCxC.BeginEdit();
                            rowDOSCxC.DORD_NUMERO = codigoDetalleOrden--;
                            rowDOSCxC.DORD_CODIGO = "código detalle";
                            rowDOSCxC.DORD_ORIGEN = rowOrden.ORD_ORIGEN + "/" + rowCxE.CONJUNTOSRow.CONJ_CODIGOPARTE;
                            rowDOSCxC.EORD_CODIGO = (dsOrdenTrabajo.ESTADO_ORDENES_TRABAJO.Select("EORD_NOMBRE = 'Generada'") as Data.dsOrdenTrabajo.ESTADO_ORDENES_TRABAJORow[])[0].EORD_CODIGO;
                            rowDOSCxC.ORD_NUMERO = rowOrden.ORD_NUMERO;
                            rowDOSCxC.PAR_CODIGO = rowSCxC.SUBCONJUNTOSRow.SCONJ_CODIGO;
                            rowDOSCxC.PAR_TIPO = parteTipoSubconjunto;
                            rowDOSCxC.ESTR_CODIGO = codigoEstructura;
                            rowDOSCxC.DORD_OBSERVACIONES = string.Empty;
                            rowDOSCxC.DORD_CANTIDADESTIMADA = rowDetalle.DPSEM_CANTIDADESTIMADA * rowCxE.CXE_CANTIDAD * rowSCxC.SCXCJ_CANTIDAD;
                            rowDOSCxC.CTO_CODIGO = rowHojaSCxC.CTO_CODIGO;
                            rowDOSCxC.OPR_NUMERO = rowHojaSCxC.OPR_NUMERO;
                            if (ordenPrecedente == 0) { rowDOSCxC.SetDORD_ORDENPRECEDENTENull(); }
                            else
                            {
                                rowDOSCxC.DORD_ORDENPRECEDENTE = ordenPrecedente;
                                dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.FindByDORD_NUMERO(ordenPrecedente).DORD_ORDENSIGUIENTE = rowDOSCxC.DORD_NUMERO;
                            }
                            if (cantidadOrdenes == 0) { rowDOSCxC.SetDORD_ORDENSIGUIENTENull(); }
                            rowDOSCxC.DORD_NIVEL = 0;
                            rowDOSCxC.EndEdit();
                            dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.AddDETALLE_ORDENES_TRABAJORow(rowDOSCxC);
                            ordenPrecedente = rowDOSCxC.DORD_NUMERO;                            
                        }
                        
                        foreach (Data.dsEstructura.PIEZASXSUBCONJUNTORow rowPxSC in dsEstructura.SUBCONJUNTOS.FindBySCONJ_CODIGO(rowSCxC.SCONJ_CODIGO).GetPIEZASXSUBCONJUNTORows())
                        {
                            //Ahora generamos un detalle para cada par operación-centro de la hoja de ruta de la parte
                            cantidadOrdenes = dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(rowPxSC.PIEZASRow.HR_CODIGO).GetDETALLE_HOJARUTARows().Length;
                            foreach (Data.dsHojaRuta.DETALLE_HOJARUTARow rowHojaPxSC in dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(rowPxSC.PIEZASRow.HR_CODIGO).GetDETALLE_HOJARUTARows())
                            {
                                cantidadOrdenes--;
                                Data.dsOrdenTrabajo.DETALLE_ORDENES_TRABAJORow rowDOPxSC = dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.NewDETALLE_ORDENES_TRABAJORow();
                                rowDOPxSC.BeginEdit();
                                rowDOPxSC.DORD_NUMERO = codigoDetalleOrden--;
                                rowDOPxSC.DORD_CODIGO = "código detalle";
                                rowDOPxSC.DORD_ORIGEN = rowOrden.ORD_ORIGEN + "/" + rowCxE.CONJUNTOSRow.CONJ_CODIGOPARTE + "/" + rowPxSC.SUBCONJUNTOSRow.SCONJ_CODIGOPARTE;
                                rowDOPxSC.EORD_CODIGO = (dsOrdenTrabajo.ESTADO_ORDENES_TRABAJO.Select("EORD_NOMBRE = 'Generada'") as Data.dsOrdenTrabajo.ESTADO_ORDENES_TRABAJORow[])[0].EORD_CODIGO;
                                rowDOPxSC.ORD_NUMERO = rowOrden.ORD_NUMERO;
                                rowDOPxSC.PAR_CODIGO = rowPxSC.PZA_CODIGO;
                                rowDOPxSC.PAR_TIPO = parteTipoPieza;
                                rowDOPxSC.ESTR_CODIGO = codigoEstructura;
                                rowDOPxSC.DORD_OBSERVACIONES = string.Empty;
                                rowDOPxSC.DORD_CANTIDADESTIMADA = rowDetalle.DPSEM_CANTIDADESTIMADA * rowCxE.CXE_CANTIDAD * rowSCxC.SCXCJ_CANTIDAD * rowPxSC.PXSC_CANTIDAD;
                                rowDOPxSC.CTO_CODIGO = rowHojaPxSC.CTO_CODIGO;
                                rowDOPxSC.OPR_NUMERO = rowHojaPxSC.OPR_NUMERO;
                                if (ordenPrecedente == 0) { rowDOPxSC.SetDORD_ORDENPRECEDENTENull(); }
                                else
                                {
                                    rowDOPxSC.DORD_ORDENPRECEDENTE = ordenPrecedente;
                                    dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.FindByDORD_NUMERO(ordenPrecedente).DORD_ORDENSIGUIENTE = rowDOPxSC.DORD_NUMERO;
                                }
                                if (cantidadOrdenes == 0) { rowDOPxSC.SetDORD_ORDENSIGUIENTENull(); }
                                rowDOPxSC.DORD_NIVEL = 0;
                                rowDOPxSC.EndEdit();
                                dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.AddDETALLE_ORDENES_TRABAJORow(rowDOPxSC);
                                ordenPrecedente = rowDOPxSC.DORD_NUMERO;
                            }                                
                        }
                    }

                    ordenPrecedente = 0;
                    foreach (Data.dsEstructura.PIEZASXCONJUNTORow rowPxC in dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(rowCxE.CONJ_CODIGO).GetPIEZASXCONJUNTORows())
                    {                        
                        cantidadOrdenes = dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(rowPxC.PIEZASRow.HR_CODIGO).GetDETALLE_HOJARUTARows().Length;
                        foreach (Data.dsHojaRuta.DETALLE_HOJARUTARow rowHojaPxC in dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(rowPxC.PIEZASRow.HR_CODIGO).GetDETALLE_HOJARUTARows())
                        {
                            cantidadOrdenes--;
                            Data.dsOrdenTrabajo.DETALLE_ORDENES_TRABAJORow rowDOPxC = dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.NewDETALLE_ORDENES_TRABAJORow();
                            rowDOPxC.BeginEdit();
                            rowDOPxC.DORD_NUMERO = codigoDetalleOrden--;
                            rowDOPxC.DORD_CODIGO = "código detalle";
                            rowDOPxC.DORD_ORIGEN = rowOrden.ORD_ORIGEN + "/" + rowCxE.CONJUNTOSRow.CONJ_CODIGOPARTE;
                            rowDOPxC.EORD_CODIGO = (dsOrdenTrabajo.ESTADO_ORDENES_TRABAJO.Select("EORD_NOMBRE = 'Generada'") as Data.dsOrdenTrabajo.ESTADO_ORDENES_TRABAJORow[])[0].EORD_CODIGO;
                            rowDOPxC.ORD_NUMERO = rowOrden.ORD_NUMERO;
                            rowDOPxC.PAR_CODIGO = rowCxE.CONJUNTOSRow.CONJ_CODIGO;
                            rowDOPxC.PAR_TIPO = parteTipoConjunto;
                            rowDOPxC.ESTR_CODIGO = codigoEstructura;
                            rowDOPxC.DORD_OBSERVACIONES = string.Empty;
                            rowDOPxC.DORD_CANTIDADESTIMADA = rowDetalle.DPSEM_CANTIDADESTIMADA * rowCxE.CXE_CANTIDAD * rowPxC.PXCJ_CANTIDAD;
                            rowDOPxC.CTO_CODIGO = rowHojaPxC.CTO_CODIGO;
                            rowDOPxC.OPR_NUMERO = rowHojaPxC.OPR_NUMERO;
                            if (ordenPrecedente == 0) { rowDOPxC.SetDORD_ORDENPRECEDENTENull(); }
                            else
                            {
                                rowDOPxC.DORD_ORDENPRECEDENTE = ordenPrecedente;
                                dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.FindByDORD_NUMERO(ordenPrecedente).DORD_ORDENSIGUIENTE = rowDOPxC.DORD_NUMERO;
                            }
                            if (cantidadOrdenes == 0) { rowDOPxC.SetDORD_ORDENSIGUIENTENull(); }
                            rowDOPxC.DORD_NIVEL = 0;
                            rowDOPxC.EndEdit();
                            dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.AddDETALLE_ORDENES_TRABAJORow(rowDOPxC);
                            ordenPrecedente = rowDOPxC.DORD_NUMERO;
                        }
                    }
                }
                //Ahora las piezas que forman la estructura
                ordenPrecedente = 0;
                foreach (Data.dsEstructura.PIEZASXESTRUCTURARow rowPxE in dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).GetPIEZASXESTRUCTURARows())
                {
                    cantidadOrdenes = dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(rowPxE.PIEZASRow.HR_CODIGO).GetDETALLE_HOJARUTARows().Length;
                    foreach (Data.dsHojaRuta.DETALLE_HOJARUTARow rowHojaPxE in dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(rowPxE.PIEZASRow.HR_CODIGO).GetDETALLE_HOJARUTARows())
                    {
                        cantidadOrdenes--;
                        Data.dsOrdenTrabajo.DETALLE_ORDENES_TRABAJORow rowDOPxE = dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.NewDETALLE_ORDENES_TRABAJORow();
                        rowDOPxE.BeginEdit();
                        rowDOPxE.DORD_NUMERO = codigoDetalleOrden--;
                        rowDOPxE.DORD_CODIGO = "código detalle";
                        rowDOPxE.DORD_ORIGEN = rowOrden.ORD_ORIGEN;
                        rowDOPxE.EORD_CODIGO = (dsOrdenTrabajo.ESTADO_ORDENES_TRABAJO.Select("EORD_NOMBRE = 'Generada'") as Data.dsOrdenTrabajo.ESTADO_ORDENES_TRABAJORow[])[0].EORD_CODIGO;
                        rowDOPxE.ORD_NUMERO = rowOrden.ORD_NUMERO;
                        rowDOPxE.PAR_CODIGO = rowPxE.PZA_CODIGO;
                        rowDOPxE.PAR_TIPO = parteTipoPieza;
                        rowDOPxE.ESTR_CODIGO = codigoEstructura;
                        rowDOPxE.DORD_OBSERVACIONES = string.Empty;
                        rowDOPxE.DORD_CANTIDADESTIMADA = rowDetalle.DPSEM_CANTIDADESTIMADA * rowPxE.PXE_CANTIDAD;
                        rowDOPxE.CTO_CODIGO = rowHojaPxE.CTO_CODIGO;
                        rowDOPxE.OPR_NUMERO = rowHojaPxE.OPR_NUMERO;
                        if (ordenPrecedente == 0) { rowDOPxE.SetDORD_ORDENPRECEDENTENull(); }
                        else
                        {
                            rowDOPxE.DORD_ORDENPRECEDENTE = ordenPrecedente;
                            dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.FindByDORD_NUMERO(ordenPrecedente).DORD_ORDENSIGUIENTE = rowDOPxE.DORD_NUMERO;
                        }
                        if (cantidadOrdenes == 0) { rowDOPxE.SetDORD_ORDENSIGUIENTENull(); }
                        rowDOPxE.DORD_NIVEL = 0;
                        rowDOPxE.EndEdit();
                        dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.AddDETALLE_ORDENES_TRABAJORow(rowDOPxE);
                        ordenPrecedente = rowDOPxE.DORD_NUMERO;
                    }                        
                }
            }
            
        }

        public static void GenerarArbolOrdenes(int codigoOrden, TreeView tvOT, TreeView tvOTyE, Data.dsOrdenTrabajo dsOrdenTrabajo)
        {
        
        }
    }
}
