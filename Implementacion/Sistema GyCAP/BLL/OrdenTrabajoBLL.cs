using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class OrdenTrabajoBLL
    {
        public static readonly int parteTipoConjunto = 1;
        public static readonly int parteTipoSubconjunto = 2;
        public static readonly int parteTipoPieza = 3;
        public static readonly int parteTipoMateriaPrima = 4;
        
        public static void GenerarOrdenTrabajoSemana(int semana, Data.dsPlanSemanal dsPlanSemanal, Data.dsOrdenTrabajo dsOrdenTrabajo, Data.dsEstructura dsEstructura, Data.dsHojaRuta dsHojaRuta)
        {
            //Declaramos las variables necesarias
            int codigoOrden = -1, codigoDetalleOrden = -1;
            decimal tiempoTotal = 0;
            
            //Recorremos todos los días de la semana seleccionada
            foreach (Data.dsPlanSemanal.DIAS_PLAN_SEMANALRow rowDia in (Data.dsPlanSemanal.DIAS_PLAN_SEMANALRow[])dsPlanSemanal.DIAS_PLAN_SEMANAL.Select("PSEM_CODIGO = " + semana))
            {
                //Recorremos todo el detalle de cada día
                foreach (Data.dsPlanSemanal.DETALLE_PLANES_SEMANALESRow rowDetalle in rowDia.GetDETALLE_PLANES_SEMANALESRows())
                {
                    //Primero armamos la cabecera que corresponde uno a uno con el detalle del día
                    Data.dsOrdenTrabajo.ORDENES_TRABAJORow rowOrden = dsOrdenTrabajo.ORDENES_TRABAJO.NewORDENES_TRABAJORow();
                    rowOrden.BeginEdit();
                    rowOrden.ORD_NUMERO = codigoOrden--;
                    rowOrden.ORD_CODIGO = "codigoOrden";
                    rowOrden.EORD_CODIGO = (dsOrdenTrabajo.ESTADO_ORDENES_TRABAJO.Select("EORD_NOMBRE = 'Generada'") as Data.dsOrdenTrabajo.ESTADO_ORDENES_TRABAJORow[])[0].EORD_CODIGO;
                    rowOrden.ORD_FECHAALTA = DBBLL.GetFechaServidor();
                    rowOrden.DPSEM_CODIGO = rowDetalle.DPSEM_CODIGO;
                    rowOrden.ORD_ORIGEN = "OA-";
                    rowOrden.ORD_FECHAINICIOESTIMADA = DateTime.Today;
                    rowOrden.ORD_FECHAFINESTIMADA = DateTime.Today;
                    rowOrden.SetORD_FECHAINICIOREALNull();
                    rowOrden.SetORD_FECHAFINREALNull();
                    rowOrden.SetORDM_NUMERONull();
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
                    //empezamos a crear los detalles
                    foreach (Data.dsEstructura.CONJUNTOSXESTRUCTURARow rowCxE in dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).GetCONJUNTOSXESTRUCTURARows())
	                {
                        foreach (Data.dsEstructura.SUBCONJUNTOSXCONJUNTORow rowSCxC in dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(rowCxE.CONJ_CODIGO).GetSUBCONJUNTOSXCONJUNTORows())
                        {
                            foreach (Data.dsEstructura.PIEZASXSUBCONJUNTORow rowPxSC in dsEstructura.SUBCONJUNTOS.FindBySCONJ_CODIGO(rowSCxC.SCONJ_CODIGO).GetPIEZASXSUBCONJUNTORows())
                            {
                                //Hay que obtener la hoja ruta de la pieza y recien ahí generar un detalle para cada operación de la hoja
                            }
                        }
                        
                        
                        /*Data.dsOrdenTrabajo.DETALLE_ORDENES_TRABAJORow rowDetalleOrden = dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.NewDETALLE_ORDENES_TRABAJORow();
                        rowDetalleOrden.BeginEdit();
                        rowDetalleOrden.DORD_NUMERO = codigoDetalleOrden--;
                        rowDetalleOrden.DORD_CODIGO = "código detalle";
                        rowDetalleOrden.DORD_ORIGEN = "OA-" + rowOrden.ORD_NUMERO.ToString();
                        rowDetalleOrden.EORD_CODIGO = (dsOrdenTrabajo.ESTADO_ORDENES_TRABAJO.Select("EORD_NOMBRE = 'Generada'") as Data.dsOrdenTrabajo.ESTADO_ORDENES_TRABAJORow[])[0].EORD_CODIGO;
                        rowDetalleOrden.ORD_NUMERO = rowOrden.ORD_NUMERO;
                        rowDetalleOrden.PAR_CODIGO = rowCxE.CONJ_CODIGO;
                        rowDetalleOrden.PAR_TIPO = parteTipoConjunto;
                        rowDetalleOrden.ESTR_CODIGO = codigoEstructura;
                        rowDetalleOrden.DORD_OBSERVACIONES = string.Empty;
                        rowDetalleOrden.DORD_CANTIDADESTIMADA = rowCxE.CXE_CANTIDAD * rowDetalle.DPSEM_CANTIDADESTIMADA;*/
                        
	                }
                    
                    
                }
            }
        }
    }
}
