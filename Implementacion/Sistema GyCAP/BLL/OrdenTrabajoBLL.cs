using System;
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
        public static readonly int nodoOrdenTrabajo = 1;
        public static readonly int nodoDetalleOrdenTrabajo = 2;
        public static readonly int nodoComplemento = 3;

        public static void Insertar(Data.dsOrdenTrabajo dsOrdenTrabajo)
        {
            DAL.OrdenTrabajoDAL.Insertar(dsOrdenTrabajo);
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
                //Primero armamos la cabecera que corresponde uno a uno con el detalle del día
                Data.dsOrdenTrabajo.ORDENES_TRABAJORow rowOrden = dsOrdenTrabajo.ORDENES_TRABAJO.NewORDENES_TRABAJORow();
                rowOrden.BeginEdit();
                rowOrden.ORD_NUMERO = codigoOrden--;
                rowOrden.ORD_CODIGO = "codigoOrden";
                rowOrden.EORD_CODIGO = (dsOrdenTrabajo.ESTADO_ORDENES_TRABAJO.Select("EORD_NOMBRE = 'Generada'") as Data.dsOrdenTrabajo.ESTADO_ORDENES_TRABAJORow[])[0].EORD_CODIGO;
                rowOrden.ORD_FECHAALTA = DBBLL.GetFechaServidor();
                rowOrden.DPSEM_CODIGO = rowDetalle.DPSEM_CODIGO;
                rowOrden.ORD_ORIGEN = "OA-" + rowDetalle.COCINASRow.COC_CODIGO_PRODUCTO;
                rowOrden.SetORD_FECHAINICIOREALNull();
                rowOrden.SetORD_FECHAFINREALNull();
                rowOrden.SetORDM_NUMERONull();
                rowOrden.ORD_PRIORIDAD = 0;
                rowOrden.ORD_OBSERVACIONES = string.Empty;
                
                //Obtenemos la estructura completa de la cocina y la cantidad pedida
                int codigoEstructura = CocinaBLL.ObtenerCodigoEstructuraActiva(Convert.ToInt32(rowDetalle.COC_CODIGO));

                rowOrden.ESTR_CODIGO = codigoEstructura;
                rowOrden.EndEdit();
                dsOrdenTrabajo.ORDENES_TRABAJO.AddORDENES_TRABAJORow(rowOrden);
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
                
                //Creamos el detalle de la orden de trabajo
                //Calculamos las cantidades necesarias de cada cosa y al mismo tiempo el tiempo necesario para producir todo
                //Empezamos por las partes de primer nivel que dependen de estructura, bajamos hasta el último nivel y desde ahí
                //empezamos a crear los detalles, primero los conjuntos que forman la estructura
                decimal ordenSiguiente = 0, ultimaOrdenConjunto = 0, nivel = 0, ultimaOrdenSubconjunto = 0;
                foreach (Data.dsEstructura.CONJUNTOSXESTRUCTURARow rowCxE in dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).GetCONJUNTOSXESTRUCTURARows())
                {
                    ordenSiguiente = 0;
                    foreach (Data.dsHojaRuta.DETALLE_HOJARUTARow rowHojaCxE in dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(rowCxE.CONJUNTOSRow.HR_CODIGO).GetDETALLE_HOJARUTARows())
                    {
                        Data.dsOrdenTrabajo.DETALLE_ORDENES_TRABAJORow rowDOCxE = dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.NewDETALLE_ORDENES_TRABAJORow();
                        rowDOCxE.BeginEdit();
                        rowDOCxE.DORD_NUMERO = codigoDetalleOrden--;
                        rowDOCxE.DORD_CODIGO = "código detalle";
                        rowDOCxE.DORD_ORIGEN = rowOrden.ORD_ORIGEN + "/" + rowCxE.CONJUNTOSRow.CONJ_CODIGOPARTE;
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
                        rowDOCxE.DORD_ORDENPRECEDENTE = 0;
                        if (ordenSiguiente == 0) { rowDOCxE.SetDORD_ORDENSIGUIENTENull(); }
                        else { rowDOCxE.DORD_ORDENSIGUIENTE = ordenSiguiente; }
                        rowDOCxE.DORD_NIVEL = 0;
                        rowDOCxE.EndEdit();
                        dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.AddDETALLE_ORDENES_TRABAJORow(rowDOCxE);
                        ordenSiguiente = rowDOCxE.DORD_NUMERO;
                        ultimaOrdenConjunto = ordenSiguiente;
                    }
                    
                    foreach (Data.dsEstructura.SUBCONJUNTOSXCONJUNTORow rowSCxC in dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(rowCxE.CONJ_CODIGO).GetSUBCONJUNTOSXCONJUNTORows())
                    {
                        nivel = 1;
                        ordenSiguiente = ultimaOrdenConjunto;
                        foreach (Data.dsHojaRuta.DETALLE_HOJARUTARow rowHojaSCxC in dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(rowSCxC.SUBCONJUNTOSRow.HR_CODIGO).GetDETALLE_HOJARUTARows())
                        {
                            Data.dsOrdenTrabajo.DETALLE_ORDENES_TRABAJORow rowDOSCxC = dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.NewDETALLE_ORDENES_TRABAJORow();
                            rowDOSCxC.BeginEdit();
                            rowDOSCxC.DORD_NUMERO = codigoDetalleOrden--;
                            rowDOSCxC.DORD_CODIGO = "código detalle";
                            rowDOSCxC.DORD_ORIGEN = dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.FindByDORD_NUMERO(ultimaOrdenConjunto).DORD_ORIGEN + "/" + rowSCxC.SUBCONJUNTOSRow.SCONJ_CODIGOPARTE;
                            rowDOSCxC.EORD_CODIGO = (dsOrdenTrabajo.ESTADO_ORDENES_TRABAJO.Select("EORD_NOMBRE = 'Generada'") as Data.dsOrdenTrabajo.ESTADO_ORDENES_TRABAJORow[])[0].EORD_CODIGO;
                            rowDOSCxC.ORD_NUMERO = rowOrden.ORD_NUMERO;
                            rowDOSCxC.PAR_CODIGO = rowSCxC.SUBCONJUNTOSRow.SCONJ_CODIGO;
                            rowDOSCxC.PAR_TIPO = parteTipoSubconjunto;
                            rowDOSCxC.ESTR_CODIGO = codigoEstructura;
                            rowDOSCxC.DORD_OBSERVACIONES = string.Empty;
                            rowDOSCxC.DORD_CANTIDADESTIMADA = rowDetalle.DPSEM_CANTIDADESTIMADA * rowCxE.CXE_CANTIDAD * rowSCxC.SCXCJ_CANTIDAD;
                            rowDOSCxC.CTO_CODIGO = rowHojaSCxC.CTO_CODIGO;
                            rowDOSCxC.OPR_NUMERO = rowHojaSCxC.OPR_NUMERO;
                            if (ordenSiguiente == 0) { rowDOSCxC.SetDORD_ORDENSIGUIENTENull(); }
                            else { rowDOSCxC.DORD_ORDENSIGUIENTE = ordenSiguiente; }
                            rowDOSCxC.DORD_ORDENPRECEDENTE = 0;
                            rowDOSCxC.DORD_NIVEL = nivel;
                            rowDOSCxC.EndEdit();
                            dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.AddDETALLE_ORDENES_TRABAJORow(rowDOSCxC);
                            ordenSiguiente = rowDOSCxC.DORD_NUMERO;
                            ultimaOrdenSubconjunto = rowDOSCxC.DORD_NUMERO;
                            nivel = 0;
                        }
                        
                        foreach (Data.dsEstructura.PIEZASXSUBCONJUNTORow rowPxSC in dsEstructura.SUBCONJUNTOS.FindBySCONJ_CODIGO(rowSCxC.SCONJ_CODIGO).GetPIEZASXSUBCONJUNTORows())
                        {
                            //Ahora generamos un detalle para cada par operación-centro de la hoja de ruta de la parte
                            nivel = 1;
                            ordenSiguiente = ultimaOrdenSubconjunto;
                            foreach (Data.dsHojaRuta.DETALLE_HOJARUTARow rowHojaPxSC in dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(rowPxSC.PIEZASRow.HR_CODIGO).GetDETALLE_HOJARUTARows())
                            {
                                Data.dsOrdenTrabajo.DETALLE_ORDENES_TRABAJORow rowDOPxSC = dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.NewDETALLE_ORDENES_TRABAJORow();
                                rowDOPxSC.BeginEdit();
                                rowDOPxSC.DORD_NUMERO = codigoDetalleOrden--;
                                rowDOPxSC.DORD_CODIGO = "código detalle";
                                rowDOPxSC.DORD_ORIGEN = dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.FindByDORD_NUMERO(ultimaOrdenSubconjunto).DORD_ORIGEN + "/" + rowPxSC.PIEZASRow.PZA_CODIGOPARTE;
                                rowDOPxSC.EORD_CODIGO = (dsOrdenTrabajo.ESTADO_ORDENES_TRABAJO.Select("EORD_NOMBRE = 'Generada'") as Data.dsOrdenTrabajo.ESTADO_ORDENES_TRABAJORow[])[0].EORD_CODIGO;
                                rowDOPxSC.ORD_NUMERO = rowOrden.ORD_NUMERO;
                                rowDOPxSC.PAR_CODIGO = rowPxSC.PZA_CODIGO;
                                rowDOPxSC.PAR_TIPO = parteTipoPieza;
                                rowDOPxSC.ESTR_CODIGO = codigoEstructura;
                                rowDOPxSC.DORD_OBSERVACIONES = string.Empty;
                                rowDOPxSC.DORD_CANTIDADESTIMADA = rowDetalle.DPSEM_CANTIDADESTIMADA * rowCxE.CXE_CANTIDAD * rowSCxC.SCXCJ_CANTIDAD * rowPxSC.PXSC_CANTIDAD;
                                rowDOPxSC.CTO_CODIGO = rowHojaPxSC.CTO_CODIGO;
                                rowDOPxSC.OPR_NUMERO = rowHojaPxSC.OPR_NUMERO;
                                if (ordenSiguiente == 0) { rowDOPxSC.SetDORD_ORDENSIGUIENTENull(); }
                                else { rowDOPxSC.DORD_ORDENSIGUIENTE = ordenSiguiente; }
                                rowDOPxSC.DORD_ORDENPRECEDENTE = 0;
                                rowDOPxSC.DORD_NIVEL = nivel;
                                rowDOPxSC.EndEdit();
                                dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.AddDETALLE_ORDENES_TRABAJORow(rowDOPxSC);
                                ordenSiguiente = rowDOPxSC.DORD_NUMERO;
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
                            Data.dsOrdenTrabajo.DETALLE_ORDENES_TRABAJORow rowDOPxC = dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.NewDETALLE_ORDENES_TRABAJORow();
                            rowDOPxC.BeginEdit();
                            rowDOPxC.DORD_NUMERO = codigoDetalleOrden--;
                            rowDOPxC.DORD_CODIGO = "código detalle";
                            rowDOPxC.DORD_ORIGEN = dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.FindByDORD_NUMERO(ultimaOrdenConjunto).DORD_ORIGEN + "/" + rowPxC.PIEZASRow.PZA_CODIGOPARTE;
                            rowDOPxC.EORD_CODIGO = (dsOrdenTrabajo.ESTADO_ORDENES_TRABAJO.Select("EORD_NOMBRE = 'Generada'") as Data.dsOrdenTrabajo.ESTADO_ORDENES_TRABAJORow[])[0].EORD_CODIGO;
                            rowDOPxC.ORD_NUMERO = rowOrden.ORD_NUMERO;
                            rowDOPxC.PAR_CODIGO = rowCxE.CONJUNTOSRow.CONJ_CODIGO;
                            rowDOPxC.PAR_TIPO = parteTipoConjunto;
                            rowDOPxC.ESTR_CODIGO = codigoEstructura;
                            rowDOPxC.DORD_OBSERVACIONES = string.Empty;
                            rowDOPxC.DORD_CANTIDADESTIMADA = rowDetalle.DPSEM_CANTIDADESTIMADA * rowCxE.CXE_CANTIDAD * rowPxC.PXCJ_CANTIDAD;
                            rowDOPxC.CTO_CODIGO = rowHojaPxC.CTO_CODIGO;
                            rowDOPxC.OPR_NUMERO = rowHojaPxC.OPR_NUMERO;
                            if (ordenSiguiente == 0) { rowDOPxC.SetDORD_ORDENSIGUIENTENull(); }
                            else { rowDOPxC.DORD_ORDENSIGUIENTE = ordenSiguiente; }
                            rowDOPxC.DORD_ORDENPRECEDENTE = 0;
                            rowDOPxC.DORD_NIVEL = nivel;
                            rowDOPxC.EndEdit();
                            dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.AddDETALLE_ORDENES_TRABAJORow(rowDOPxC);
                            ordenSiguiente = rowDOPxC.DORD_NUMERO;
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
                        Data.dsOrdenTrabajo.DETALLE_ORDENES_TRABAJORow rowDOPxE = dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.NewDETALLE_ORDENES_TRABAJORow();
                        rowDOPxE.BeginEdit();
                        rowDOPxE.DORD_NUMERO = codigoDetalleOrden--;
                        rowDOPxE.DORD_CODIGO = "código detalle";
                        rowDOPxE.DORD_ORIGEN = rowOrden.ORD_ORIGEN + "/" + rowPxE.PIEZASRow.PZA_CODIGOPARTE;
                        rowDOPxE.EORD_CODIGO = (dsOrdenTrabajo.ESTADO_ORDENES_TRABAJO.Select("EORD_NOMBRE = 'Generada'") as Data.dsOrdenTrabajo.ESTADO_ORDENES_TRABAJORow[])[0].EORD_CODIGO;
                        rowDOPxE.ORD_NUMERO = rowOrden.ORD_NUMERO;
                        rowDOPxE.PAR_CODIGO = rowPxE.PZA_CODIGO;
                        rowDOPxE.PAR_TIPO = parteTipoPieza;
                        rowDOPxE.ESTR_CODIGO = codigoEstructura;
                        rowDOPxE.DORD_OBSERVACIONES = string.Empty;
                        rowDOPxE.DORD_CANTIDADESTIMADA = rowDetalle.DPSEM_CANTIDADESTIMADA * rowPxE.PXE_CANTIDAD;
                        rowDOPxE.CTO_CODIGO = rowHojaPxE.CTO_CODIGO;
                        rowDOPxE.OPR_NUMERO = rowHojaPxE.OPR_NUMERO;
                        if (ordenSiguiente == 0) { rowDOPxE.SetDORD_ORDENSIGUIENTENull(); }
                        else { rowDOPxE.DORD_ORDENSIGUIENTE = ordenSiguiente; }
                        rowDOPxE.DORD_ORDENPRECEDENTE = 0;
                        rowDOPxE.DORD_NIVEL = 0;
                        rowDOPxE.EndEdit();
                        dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.AddDETALLE_ORDENES_TRABAJORow(rowDOPxE);
                        ordenSiguiente = rowDOPxE.DORD_NUMERO;
                    }
                }
            }
        }

        public static void GenerarArbolOrdenes(int codigoOrden, TreeView tvDependenciaSimple, TreeView tvDependenciaCompleta, TreeView tvOrdenesYEstructura, Data.dsOrdenTrabajo dsOrdenTrabajo, Data.dsEstructura dsEstructura, Data.dsHojaRuta dsHojaRuta)
        {
            Data.dsOrdenTrabajo.ORDENES_TRABAJORow rowOrden = dsOrdenTrabajo.ORDENES_TRABAJO.FindByORD_NUMERO(codigoOrden);
            tvDependenciaSimple.Nodes.Clear();
            tvOrdenesYEstructura.Nodes.Clear();
            tvDependenciaCompleta.Nodes.Clear();
            TreeNode nodoOrdenOT = new TreeNode();
            TreeNode nodoOrdenOTyE = new TreeNode();
            TreeNode nodoOrdenDC = new TreeNode();
            nodoOrdenOT.Name = rowOrden.ORD_NUMERO.ToString();
            nodoOrdenOT.Text = rowOrden.ORD_ORIGEN;
            nodoOrdenOT.Tag = nodoOrdenTrabajo;
            tvDependenciaSimple.Nodes.Add(nodoOrdenOT);
            nodoOrdenOTyE.Name = rowOrden.ORD_NUMERO.ToString();
            nodoOrdenOTyE.Text = rowOrden.ORD_ORIGEN;
            nodoOrdenOTyE.Tag = nodoOrdenTrabajo;
            tvOrdenesYEstructura.Nodes.Add(nodoOrdenOTyE);
            nodoOrdenDC.Name = rowOrden.ORD_NUMERO.ToString();
            nodoOrdenDC.Text = rowOrden.ORD_ORIGEN;
            nodoOrdenDC.Tag = nodoOrdenTrabajo;
            tvDependenciaCompleta.Nodes.Add(nodoOrdenDC);

            TreeNode nodoDetalleOT, nodoDetalleOTyE, nodoDetalleDC, ultimoNodoParte = new TreeNode(), parte, ordenes;
            foreach (Data.dsOrdenTrabajo.DETALLE_ORDENES_TRABAJORow rowDetalle in rowOrden.GetDETALLE_ORDENES_TRABAJORows())
            {
                #region Árbol dependencia simple
                nodoDetalleOT = new TreeNode();
                nodoDetalleOT.Name = rowDetalle.DORD_NUMERO.ToString();
                nodoDetalleOT.Text = rowDetalle.DORD_ORIGEN;
                nodoDetalleOT.Tag = nodoDetalleOrdenTrabajo;
                if (rowDetalle.IsDORD_ORDENSIGUIENTENull())
                {
                    nodoOrdenOT.Nodes.Add(nodoDetalleOT);
                }
                else
                {
                    if (rowDetalle.DORD_NIVEL == 1)
                    {
                        nodoOrdenOT.Nodes.Find(rowDetalle.DORD_ORDENSIGUIENTE.ToString(), true)[0].Nodes.Add(nodoDetalleOT);
                    }
                    else
                    {
                        nodoOrdenOT.Nodes.Find(rowDetalle.DORD_ORDENSIGUIENTE.ToString(), true)[0].Parent.Nodes.Add(nodoDetalleOT);
                    }
                }
                #endregion

                #region Árbol dependencia completa

                nodoDetalleDC = new TreeNode();
                nodoDetalleDC.Name = rowDetalle.DORD_NUMERO.ToString();
                nodoDetalleDC.Text = rowDetalle.DORD_ORIGEN;
                nodoDetalleDC.Tag = nodoDetalleOrdenTrabajo;
                if (rowDetalle.IsDORD_ORDENSIGUIENTENull())
                {
                    nodoOrdenDC.Nodes.Add(nodoDetalleDC);
                }
                else
                {
                    nodoOrdenDC.Nodes.Find(rowDetalle.DORD_ORDENSIGUIENTE.ToString(), true)[0].Nodes.Add(nodoDetalleDC);
                }

                #endregion

                #region Árbol órdenes y estructura
                nodoDetalleOTyE = new TreeNode();
                nodoDetalleOTyE.Name = rowDetalle.DORD_NUMERO.ToString();
                nodoDetalleOTyE.Text = rowDetalle.DORD_ORIGEN;
                nodoDetalleOTyE.Tag = nodoDetalleOrdenTrabajo;
                if (rowDetalle.IsDORD_ORDENSIGUIENTENull())
                {
                    parte = new TreeNode();
                    ordenes = new TreeNode();
                    parte.Name = rowDetalle.PAR_CODIGO.ToString();
                    parte.Text = GetTipoParte(Convert.ToInt32(rowDetalle.PAR_TIPO));
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
                    if (Convert.ToInt32(rowDetalle.DORD_NIVEL) == 0)
                    {
                        ultimoNodoParte.Nodes["ordenes"].Nodes.Add(nodoDetalleOTyE);
                    }
                    else
                    {
                        while (rowDetalle.DORD_ORDENSIGUIENTE != Convert.ToDecimal(ultimoNodoParte.Nodes["ordenes"].LastNode.Name))
                        {
                            ultimoNodoParte = ultimoNodoParte.Parent;
                        }

                        parte = new TreeNode();
                        parte.Name = rowDetalle.PAR_CODIGO.ToString();
                        parte.Text = GetTipoParte(Convert.ToInt32(rowDetalle.PAR_TIPO));
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

        public static void PlanearFechaHaciaDelante(int codigoOrden, DateTime fechaInicio, decimal factorCorreccion, TreeView tvDependenciaCompleta, Data.dsOrdenTrabajo dsOrdenTrabajo, Data.dsEstructura dsEstructura, Data.dsHojaRuta dsHojaRuta)
        {
            TreeNode nodoOrden = tvDependenciaCompleta.Nodes[codigoOrden.ToString()];
            dsOrdenTrabajo.ORDENES_TRABAJO.FindByORD_NUMERO(codigoOrden).ORD_FECHAINICIOESTIMADA = fechaInicio;
            DateTime fechaMayor = fechaInicio;
            DateTime nuevaFecha;
            foreach (TreeNode nodoHijo in nodoOrden.Nodes)
            {
                nuevaFecha = CalcularFechaHaciaDelante(nodoHijo, fechaInicio, factorCorreccion, dsOrdenTrabajo, dsEstructura, dsHojaRuta);
                if (nuevaFecha > fechaMayor) { fechaMayor = nuevaFecha; }
            }

            dsOrdenTrabajo.ORDENES_TRABAJO.FindByORD_NUMERO(codigoOrden).ORD_FECHAFINESTIMADA = fechaMayor;
        }

        public static void PlanearFechaHaciaAtras(int codigoOrden, DateTime fechaFinalizacion, decimal factorCorreccion, TreeView tvDependenciaCompleta, Data.dsOrdenTrabajo dsOrdenTrabajo, Data.dsEstructura dsEstructura, Data.dsHojaRuta dsHojaRuta)
        {
            TreeNode nodoOrden = tvDependenciaCompleta.Nodes[codigoOrden.ToString()];
            dsOrdenTrabajo.ORDENES_TRABAJO.FindByORD_NUMERO(codigoOrden).ORD_FECHAFINESTIMADA = fechaFinalizacion;
            DateTime fechaMenor = fechaFinalizacion;
            DateTime nuevaFecha;
            foreach (TreeNode nodoHijo in nodoOrden.Nodes)
            {
                nuevaFecha = CalcularFechaHaciaAtras(nodoHijo, fechaFinalizacion, factorCorreccion, dsOrdenTrabajo, dsEstructura, dsHojaRuta);
                if (nuevaFecha < fechaMenor) { fechaMenor = nuevaFecha; }
            }

            dsOrdenTrabajo.ORDENES_TRABAJO.FindByORD_NUMERO(codigoOrden).ORD_FECHAINICIOESTIMADA = fechaMenor;
        }

        private static DateTime CalcularFechaHaciaAtras(TreeNode nodo, DateTime fechaFin, decimal factorCorreccion, Data.dsOrdenTrabajo dsOrdenTrabajo, Data.dsEstructura dsEstructura, Data.dsHojaRuta dsHojaRuta)
        {
            if (nodo != null)
            {
                dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.FindByDORD_NUMERO(Convert.ToInt32(nodo.Name)).DORD_FECHAFINESTIMADA = fechaFin;
                decimal codOperacion = dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.FindByDORD_NUMERO(Convert.ToInt32(nodo.Name)).OPR_NUMERO;
                decimal codCentro = dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.FindByDORD_NUMERO(Convert.ToInt32(nodo.Name)).CTO_CODIGO;
                decimal horasOperacion = dsHojaRuta.OPERACIONES.FindByOPR_NUMERO(codOperacion).OPR_HORASREQUERIDA;
                decimal cantidad = dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.FindByDORD_NUMERO(Convert.ToInt32(nodo.Name)).DORD_CANTIDADESTIMADA;
                TimeSpan restar = TimeSpan.FromHours(Convert.ToDouble(cantidad * horasOperacion));                
                DateTime fechaInicioDetalle = fechaFin.Subtract(restar);
                dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.FindByDORD_NUMERO(Convert.ToInt32(nodo.Name)).DORD_FECHAINICIOESTIMADA = fechaInicioDetalle;
                DateTime fechaMenor = fechaInicioDetalle;
                DateTime nuevaFecha;
                foreach (TreeNode nodoHijo in nodo.Nodes)
                {
                    nuevaFecha = CalcularFechaHaciaAtras(nodoHijo, fechaInicioDetalle, factorCorreccion, dsOrdenTrabajo, dsEstructura, dsHojaRuta);
                    if (nuevaFecha < fechaMenor) { fechaMenor = nuevaFecha; }
                }
                return fechaMenor;
            }

            return fechaFin;
        }

        private static DateTime CalcularFechaHaciaDelante(TreeNode nodo, DateTime fechaInicio, decimal factorCorreccion, Data.dsOrdenTrabajo dsOrdenTrabajo, Data.dsEstructura dsEstructura, Data.dsHojaRuta dsHojaRuta)
        {
            if (nodo != null)
            {
                dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.FindByDORD_NUMERO(Convert.ToInt32(nodo.Name)).DORD_FECHAINICIOESTIMADA = fechaInicio;
                decimal codOperacion = dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.FindByDORD_NUMERO(Convert.ToInt32(nodo.Name)).OPR_NUMERO;
                decimal codCentro = dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.FindByDORD_NUMERO(Convert.ToInt32(nodo.Name)).CTO_CODIGO;
                decimal horasOperacion = dsHojaRuta.OPERACIONES.FindByOPR_NUMERO(codOperacion).OPR_HORASREQUERIDA;
                decimal cantidad = dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.FindByDORD_NUMERO(Convert.ToInt32(nodo.Name)).DORD_CANTIDADESTIMADA;
                decimal sumar = cantidad * horasOperacion;
                DateTime fechaFinDetalle = fechaInicio.AddHours(Double.Parse(sumar.ToString()));
                dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.FindByDORD_NUMERO(Convert.ToInt32(nodo.Name)).DORD_FECHAFINESTIMADA = fechaFinDetalle;
                DateTime fechaMayor = fechaFinDetalle;
                DateTime nuevaFecha;
                foreach (TreeNode nodoHijo in nodo.Nodes)
                {
                    nuevaFecha = CalcularFechaHaciaDelante(nodoHijo, fechaFinDetalle, factorCorreccion, dsOrdenTrabajo, dsEstructura, dsHojaRuta);
                    if (nuevaFecha > fechaMayor) { fechaMayor = nuevaFecha; }
                }
                return fechaMayor;
            }

            return fechaInicio;
        }
    }
}
