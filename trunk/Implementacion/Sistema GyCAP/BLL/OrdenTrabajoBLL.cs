using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public static string GetTipoParte(int tipo)
        {
            if (tipo == parteTipoConjunto) return "Conjunto";
            if (tipo == parteTipoSubconjunto) return "Subconjunto";
            if (tipo == parteTipoPieza) return "Pieza";
            if (tipo == parteTipoMateriaPrima) return "Materia Prima";
            return string.Empty;
        }
        
        /// <summary>
        /// Genera las órdenes de trabajo para la orden de producción dada.
        /// </summary>
        /// <param name="codigoOrdenProduccion"></param>
        /// <param name="dsPlanSemanal"></param>
        /// <param name="dsOrdenTrabajo"></param>
        /// <param name="dsEstructura"></param>
        /// <param name="dsHojaRuta"></param>
        public static void GenerarOrdenesTrabajo(int codigoOrdenProduccion, Data.dsOrdenTrabajo dsOrdenTrabajo, Data.dsEstructura dsEstructura, Data.dsHojaRuta dsHojaRuta)
        {
            //Si la orden de producción tiene órdenes de trabajo generadas las eliminamos
            //Y si ya estan en la DB ??? - gonzalo
            foreach (Data.dsOrdenTrabajo.ORDENES_TRABAJORow row in dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codigoOrdenProduccion).GetORDENES_TRABAJORows())
            {
                row.Delete();
            }
            
            //Generamos los códigos de las órdenes de trabajo
            int codigoOrdenT = -1;
            if(dsOrdenTrabajo.ORDENES_TRABAJO.Rows.Count > 0)
            { codigoOrdenT = Convert.ToInt32((dsOrdenTrabajo.ORDENES_TRABAJO.Select("ORDT_NUMERO = Min (ORDT_NUMERO)") as Data.dsOrdenTrabajo.ORDENES_TRABAJORow[])[0].ORDT_NUMERO - 1); }

            //Obtenemos la estructura completa de la cocina            
            int codigoEstructura = Convert.ToInt32(dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codigoOrdenProduccion).ESTR_CODIGO);
            EstructuraBLL.ObtenerEstructura(codigoEstructura, dsEstructura, true);

            //Creamos las órdenes de trabajo, generamos una para cada par operación-centro de la hoja de ruta de la parte
            //De forma recursiva empezando por el nivel más alto de la estructura
            decimal ordenSiguiente = 0, ultimaOrdenConjunto = 0, nivel = 0, ultimaOrdenSubconjunto = 0;
            foreach (Data.dsEstructura.CONJUNTOSXESTRUCTURARow rowCxE in dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).GetCONJUNTOSXESTRUCTURARows())
            {
                ordenSiguiente = 0;
                //Comprobamos si tiene hoja de ruta
                if (!rowCxE.CONJUNTOSRow.IsHR_CODIGONull())
                {
                    //Tiene hoja de ruta, la obtenemos y la recorremos
                    DAL.HojaRutaDAL.ObtenerHojaRuta(Convert.ToInt32(rowCxE.CONJUNTOSRow.HR_CODIGO), true, dsHojaRuta);
                    foreach (Data.dsHojaRuta.DETALLE_HOJARUTARow rowHojaCxE in dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(rowCxE.CONJUNTOSRow.HR_CODIGO).GetDETALLE_HOJARUTARows())
                    {                        
                        //Creamos la orden de trabajo para cada detalle de la hoja de ruta
                        Data.dsOrdenTrabajo.ORDENES_TRABAJORow rowOTCxE = dsOrdenTrabajo.ORDENES_TRABAJO.NewORDENES_TRABAJORow();
                        rowOTCxE.BeginEdit();
                        rowOTCxE.ORDT_NUMERO = codigoOrdenT--;
                        rowOTCxE.ORDT_CODIGO = "OTA" + (rowOTCxE.ORDT_NUMERO * -1).ToString();
                        rowOTCxE.ORDT_ORIGEN = dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codigoOrdenProduccion).ORDP_ORIGEN + " / " + rowCxE.CONJUNTOSRow.CONJ_CODIGOPARTE;
                        rowOTCxE.EORD_CODIGO = (dsOrdenTrabajo.ESTADO_ORDENES_TRABAJO.Select("EORD_NOMBRE = 'Generada'") as Data.dsOrdenTrabajo.ESTADO_ORDENES_TRABAJORow[])[0].EORD_CODIGO;
                        rowOTCxE.ORDP_NUMERO = dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codigoOrdenProduccion).ORDP_NUMERO;
                        rowOTCxE.PAR_CODIGO = rowCxE.CONJUNTOSRow.CONJ_CODIGO;
                        rowOTCxE.PAR_TIPO = parteTipoConjunto;
                        rowOTCxE.ESTR_CODIGO = codigoEstructura;
                        rowOTCxE.ORDT_OBSERVACIONES = string.Empty;
                        rowOTCxE.ORDT_CANTIDADESTIMADA = dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codigoOrdenProduccion).ORDP_CANTIDADESTIMADA * rowCxE.CXE_CANTIDAD;
                        rowOTCxE.CTO_CODIGO = rowHojaCxE.CTO_CODIGO;
                        rowOTCxE.OPR_NUMERO = rowHojaCxE.OPR_NUMERO;
                        if (ordenSiguiente == 0) { rowOTCxE.SetORDT_ORDENSIGUIENTENull(); }
                        else { rowOTCxE.ORDT_ORDENSIGUIENTE = ordenSiguiente; }
                        rowOTCxE.ORDT_NIVEL = 0;
                        rowOTCxE.ORDT_SECUENCIA = rowHojaCxE.DHR_SECUENCIA;
                        rowOTCxE.EndEdit();
                        dsOrdenTrabajo.ORDENES_TRABAJO.AddORDENES_TRABAJORow(rowOTCxE);
                        ordenSiguiente = rowOTCxE.ORDT_NUMERO;
                        ultimaOrdenConjunto = ordenSiguiente;
                    }
                }
                //Los subconjuntos del conjunto
                foreach (Data.dsEstructura.SUBCONJUNTOSXCONJUNTORow rowSCxC in dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(rowCxE.CONJ_CODIGO).GetSUBCONJUNTOSXCONJUNTORows())
                {
                    nivel = 1;
                    ordenSiguiente = ultimaOrdenConjunto;
                    //Comprobamos si tiene hoja de ruta
                    if (!rowSCxC.SUBCONJUNTOSRow.IsHR_CODIGONull())
                    {
                        //Tiene hoja de ruta, la obtenemos y la recorremos
                        DAL.HojaRutaDAL.ObtenerHojaRuta(Convert.ToInt32(rowSCxC.SUBCONJUNTOSRow.HR_CODIGO), true, dsHojaRuta);
                        foreach (Data.dsHojaRuta.DETALLE_HOJARUTARow rowHojaSCxC in dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(rowSCxC.SUBCONJUNTOSRow.HR_CODIGO).GetDETALLE_HOJARUTARows())
                        {                            
                            //Creamos la orden de trabajo para cada detalle de la hoja de ruta
                            Data.dsOrdenTrabajo.ORDENES_TRABAJORow rowOTSCxC = dsOrdenTrabajo.ORDENES_TRABAJO.NewORDENES_TRABAJORow();
                            rowOTSCxC.BeginEdit();
                            rowOTSCxC.ORDT_NUMERO = codigoOrdenT--;
                            rowOTSCxC.ORDT_CODIGO = "OTA" + (rowOTSCxC.ORDT_NUMERO * -1).ToString();
                            rowOTSCxC.ORDT_ORIGEN = dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(ultimaOrdenConjunto).ORDT_ORIGEN + " / " + rowSCxC.SUBCONJUNTOSRow.SCONJ_CODIGOPARTE;
                            rowOTSCxC.EORD_CODIGO = (dsOrdenTrabajo.ESTADO_ORDENES_TRABAJO.Select("EORD_NOMBRE = 'Generada'") as Data.dsOrdenTrabajo.ESTADO_ORDENES_TRABAJORow[])[0].EORD_CODIGO;
                            rowOTSCxC.ORDP_NUMERO = dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codigoOrdenProduccion).ORDP_NUMERO;
                            rowOTSCxC.PAR_CODIGO = rowSCxC.SUBCONJUNTOSRow.SCONJ_CODIGO;
                            rowOTSCxC.PAR_TIPO = parteTipoSubconjunto;
                            rowOTSCxC.ESTR_CODIGO = codigoEstructura;
                            rowOTSCxC.ORDT_OBSERVACIONES = string.Empty;
                            rowOTSCxC.ORDT_CANTIDADESTIMADA = dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codigoOrdenProduccion).ORDP_CANTIDADESTIMADA * rowCxE.CXE_CANTIDAD * rowSCxC.SCXCJ_CANTIDAD;
                            rowOTSCxC.CTO_CODIGO = rowHojaSCxC.CTO_CODIGO;
                            rowOTSCxC.OPR_NUMERO = rowHojaSCxC.OPR_NUMERO;
                            if (ordenSiguiente == 0) { rowOTSCxC.SetORDT_ORDENSIGUIENTENull(); }
                            else { rowOTSCxC.ORDT_ORDENSIGUIENTE = ordenSiguiente; }
                            rowOTSCxC.ORDT_NIVEL = nivel;
                            rowOTSCxC.ORDT_SECUENCIA = rowHojaSCxC.DHR_SECUENCIA;
                            rowOTSCxC.EndEdit();
                            dsOrdenTrabajo.ORDENES_TRABAJO.AddORDENES_TRABAJORow(rowOTSCxC);
                            ordenSiguiente = rowOTSCxC.ORDT_NUMERO;
                            ultimaOrdenSubconjunto = rowOTSCxC.ORDT_NUMERO;
                            nivel = 0;
                        }
                    }
                    //Las piezas del subconjunto
                    foreach (Data.dsEstructura.PIEZASXSUBCONJUNTORow rowPxSC in dsEstructura.SUBCONJUNTOS.FindBySCONJ_CODIGO(rowSCxC.SCONJ_CODIGO).GetPIEZASXSUBCONJUNTORows())
                    {                        
                        nivel = 1;
                        ordenSiguiente = ultimaOrdenSubconjunto;
                        //Comprobamos si tiene hoja de ruta
                        if (!rowPxSC.PIEZASRow.IsHR_CODIGONull())
                        {
                            //Tiene hoja de ruta, la obtenemos y la recorremos
                            DAL.HojaRutaDAL.ObtenerHojaRuta(Convert.ToInt32(rowPxSC.PIEZASRow.HR_CODIGO), true, dsHojaRuta);
                            //Creamos la orden de trabajo para cada detalle de la hoja de ruta
                            foreach (Data.dsHojaRuta.DETALLE_HOJARUTARow rowHojaPxSC in dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(rowPxSC.PIEZASRow.HR_CODIGO).GetDETALLE_HOJARUTARows())
                            {
                                Data.dsOrdenTrabajo.ORDENES_TRABAJORow rowOTPxSC = dsOrdenTrabajo.ORDENES_TRABAJO.NewORDENES_TRABAJORow();
                                rowOTPxSC.BeginEdit();
                                rowOTPxSC.ORDT_NUMERO = codigoOrdenT--;
                                rowOTPxSC.ORDT_CODIGO = "OTA" + (rowOTPxSC.ORDT_NUMERO * -1).ToString();
                                rowOTPxSC.ORDT_ORIGEN = dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(ultimaOrdenSubconjunto).ORDT_ORIGEN + " / " + rowPxSC.PIEZASRow.PZA_CODIGOPARTE;
                                rowOTPxSC.EORD_CODIGO = (dsOrdenTrabajo.ESTADO_ORDENES_TRABAJO.Select("EORD_NOMBRE = 'Generada'") as Data.dsOrdenTrabajo.ESTADO_ORDENES_TRABAJORow[])[0].EORD_CODIGO;
                                rowOTPxSC.ORDP_NUMERO = dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codigoOrdenProduccion).ORDP_NUMERO;
                                rowOTPxSC.PAR_CODIGO = rowPxSC.PZA_CODIGO;
                                rowOTPxSC.PAR_TIPO = parteTipoPieza;
                                rowOTPxSC.ESTR_CODIGO = codigoEstructura;
                                rowOTPxSC.ORDT_OBSERVACIONES = string.Empty;
                                rowOTPxSC.ORDT_CANTIDADESTIMADA = dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codigoOrdenProduccion).ORDP_CANTIDADESTIMADA * rowCxE.CXE_CANTIDAD * rowSCxC.SCXCJ_CANTIDAD * rowPxSC.PXSC_CANTIDAD;
                                rowOTPxSC.CTO_CODIGO = rowHojaPxSC.CTO_CODIGO;
                                rowOTPxSC.OPR_NUMERO = rowHojaPxSC.OPR_NUMERO;
                                if (ordenSiguiente == 0) { rowOTPxSC.SetORDT_ORDENSIGUIENTENull(); }
                                else { rowOTPxSC.ORDT_ORDENSIGUIENTE = ordenSiguiente; }
                                rowOTPxSC.ORDT_NIVEL = nivel;
                                rowOTPxSC.ORDT_SECUENCIA = rowHojaPxSC.DHR_SECUENCIA;
                                rowOTPxSC.EndEdit();
                                dsOrdenTrabajo.ORDENES_TRABAJO.AddORDENES_TRABAJORow(rowOTPxSC);
                                ordenSiguiente = rowOTPxSC.ORDT_NUMERO;
                                nivel = 0;
                            }
                        }
                    }
                }

                ordenSiguiente = ultimaOrdenConjunto;
                //Las piezas del conjunto
                foreach (Data.dsEstructura.PIEZASXCONJUNTORow rowPxC in dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(rowCxE.CONJ_CODIGO).GetPIEZASXCONJUNTORows())
                {
                    nivel = 1;
                    ordenSiguiente = ultimaOrdenConjunto;
                    //Comprobamos si tiene hoja de ruta
                    if (!rowPxC.PIEZASRow.IsHR_CODIGONull())
                    {
                        //Tiene hoja de ruta, la obtenemos y la recorremos
                        DAL.HojaRutaDAL.ObtenerHojaRuta(Convert.ToInt32(rowPxC.PIEZASRow.HR_CODIGO), true, dsHojaRuta);
                        //Creamos la orden de trabajo para cada detalle de la hoja de ruta
                        foreach (Data.dsHojaRuta.DETALLE_HOJARUTARow rowHojaPxC in dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(rowPxC.PIEZASRow.HR_CODIGO).GetDETALLE_HOJARUTARows())
                        {
                            Data.dsOrdenTrabajo.ORDENES_TRABAJORow rowOTPxC = dsOrdenTrabajo.ORDENES_TRABAJO.NewORDENES_TRABAJORow();
                            rowOTPxC.BeginEdit();
                            rowOTPxC.ORDT_NUMERO = codigoOrdenT--;
                            rowOTPxC.ORDT_CODIGO = "OTA" + (rowOTPxC.ORDT_NUMERO * -1).ToString();
                            rowOTPxC.ORDT_ORIGEN = dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(ultimaOrdenConjunto).ORDT_ORIGEN + " / " + rowPxC.PIEZASRow.PZA_CODIGOPARTE;
                            rowOTPxC.EORD_CODIGO = (dsOrdenTrabajo.ESTADO_ORDENES_TRABAJO.Select("EORD_NOMBRE = 'Generada'") as Data.dsOrdenTrabajo.ESTADO_ORDENES_TRABAJORow[])[0].EORD_CODIGO;
                            rowOTPxC.ORDP_NUMERO = dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codigoOrdenProduccion).ORDP_NUMERO;
                            rowOTPxC.PAR_CODIGO = rowCxE.CONJUNTOSRow.CONJ_CODIGO;
                            rowOTPxC.PAR_TIPO = parteTipoConjunto;
                            rowOTPxC.ESTR_CODIGO = codigoEstructura;
                            rowOTPxC.ORDT_OBSERVACIONES = string.Empty;
                            rowOTPxC.ORDT_CANTIDADESTIMADA = dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codigoOrdenProduccion).ORDP_CANTIDADESTIMADA * rowCxE.CXE_CANTIDAD * rowPxC.PXCJ_CANTIDAD;
                            rowOTPxC.CTO_CODIGO = rowHojaPxC.CTO_CODIGO;
                            rowOTPxC.OPR_NUMERO = rowHojaPxC.OPR_NUMERO;
                            if (ordenSiguiente == 0) { rowOTPxC.SetORDT_ORDENSIGUIENTENull(); }
                            else { rowOTPxC.ORDT_ORDENSIGUIENTE = ordenSiguiente; }
                            rowOTPxC.ORDT_NIVEL = nivel;
                            rowOTPxC.ORDT_SECUENCIA = rowHojaPxC.DHR_SECUENCIA;
                            rowOTPxC.EndEdit();
                            dsOrdenTrabajo.ORDENES_TRABAJO.AddORDENES_TRABAJORow(rowOTPxC);
                            ordenSiguiente = rowOTPxC.ORDT_NUMERO;
                            nivel = 0;
                        }
                    }
                }
            }
            
            //Ahora las piezas que forman la estructura
            foreach (Data.dsEstructura.PIEZASXESTRUCTURARow rowPxE in dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).GetPIEZASXESTRUCTURARows())
            {
                ordenSiguiente = 0;
                //Comprobamos si tiene hoja de ruta
                if (!rowPxE.PIEZASRow.IsHR_CODIGONull())
                {
                    //Tiene hoja de ruta, la obtenemos y la recorremos
                    DAL.HojaRutaDAL.ObtenerHojaRuta(Convert.ToInt32(rowPxE.PIEZASRow.HR_CODIGO), true, dsHojaRuta);
                    //Creamos la orden de trabajo para cada detalle de la hoja de ruta
                    foreach (Data.dsHojaRuta.DETALLE_HOJARUTARow rowHojaPxE in dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(rowPxE.PIEZASRow.HR_CODIGO).GetDETALLE_HOJARUTARows())
                    {
                        Data.dsOrdenTrabajo.ORDENES_TRABAJORow rowOTPxE = dsOrdenTrabajo.ORDENES_TRABAJO.NewORDENES_TRABAJORow();
                        rowOTPxE.BeginEdit();
                        rowOTPxE.ORDT_NUMERO = codigoOrdenT--;
                        rowOTPxE.ORDT_CODIGO = "OTA" + (rowOTPxE.ORDT_NUMERO * -1).ToString();
                        rowOTPxE.ORDT_ORIGEN = dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codigoOrdenProduccion).ORDP_ORIGEN + " / " + rowPxE.PIEZASRow.PZA_CODIGOPARTE;
                        rowOTPxE.EORD_CODIGO = (dsOrdenTrabajo.ESTADO_ORDENES_TRABAJO.Select("EORD_NOMBRE = 'Generada'") as Data.dsOrdenTrabajo.ESTADO_ORDENES_TRABAJORow[])[0].EORD_CODIGO;
                        rowOTPxE.ORDP_NUMERO = dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codigoOrdenProduccion).ORDP_NUMERO;
                        rowOTPxE.PAR_CODIGO = rowPxE.PZA_CODIGO;
                        rowOTPxE.PAR_TIPO = parteTipoPieza;
                        rowOTPxE.ESTR_CODIGO = codigoEstructura;
                        rowOTPxE.ORDT_OBSERVACIONES = string.Empty;
                        rowOTPxE.ORDT_CANTIDADESTIMADA = dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codigoOrdenProduccion).ORDP_CANTIDADESTIMADA * rowPxE.PXE_CANTIDAD;
                        rowOTPxE.CTO_CODIGO = rowHojaPxE.CTO_CODIGO;
                        rowOTPxE.OPR_NUMERO = rowHojaPxE.OPR_NUMERO;
                        if (ordenSiguiente == 0) { rowOTPxE.SetORDT_ORDENSIGUIENTENull(); }
                        else { rowOTPxE.ORDT_ORDENSIGUIENTE = ordenSiguiente; }
                        rowOTPxE.ORDT_NIVEL = 0;
                        rowOTPxE.ORDT_SECUENCIA = rowHojaPxE.DHR_SECUENCIA;
                        rowOTPxE.EndEdit();
                        dsOrdenTrabajo.ORDENES_TRABAJO.AddORDENES_TRABAJORow(rowOTPxE);
                        ordenSiguiente = rowOTPxE.ORDT_NUMERO;
                    }
                }
            }            
        }
    }
}
