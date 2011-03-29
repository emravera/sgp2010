using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class EstructuraBLL
    {
        public static readonly int EstructuraActiva = 1;
        public static readonly int EstructuraInactiva = 0;
        
        public static decimal Insertar(Data.dsEstructuraProducto ds)
        {
            return DAL.EstructuraDAL.Insertar(ds);
        }
        
        public static void Eliminar(int codigoEstructura)
        {
            //De momento solo chequeamos si está activa o no, luego agregaremos las demás condiciones - gonzalo
            if(!DAL.EstructuraDAL.EsEstructuraActiva(codigoEstructura))
            {
                DAL.EstructuraDAL.Eliminar(codigoEstructura);
            }
            else
            {
                throw new Entidades.Excepciones.ElementoActivoException();
            }
            
            
            /*if(DAL.EstructuraDAL.PuedeEliminarse(codigoEstructura))
            {
                DAL.EstructuraDAL.Eliminar(codigoEstructura);
            }
            else
            {
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }*/
        }

        public static void Actualizar(Data.dsEstructuraProducto ds)
        {
            DAL.EstructuraDAL.Actualizar(ds);
        }
        
        public static void ObtenerEstructuras(object nombre, object codPlano, object fechaCreacion, object codCocina, object codResponsable, object activoSiNo, Data.dsEstructuraProducto ds)
        {
            if (Convert.ToInt32(codPlano.ToString()) <= 0) { codPlano = null; }
            if (Convert.ToInt32(codCocina.ToString()) <= 0) { codCocina = null; }
            if (Convert.ToInt32(codResponsable.ToString()) <= 0) { codResponsable = null; }
            if (Convert.ToInt32(activoSiNo) < 0) { activoSiNo = null; }
            DAL.EstructuraDAL.ObtenerEstructuras(nombre, codPlano, fechaCreacion, codCocina, codResponsable, activoSiNo, ds);
        }

        public static void ObtenerEstructura(int codigoEstructura, Data.dsEstructura ds, bool detalle)
        {
            DAL.EstructuraDAL.ObtenerEstructura(codigoEstructura, ds, detalle);
        }

        /// <summary>
        /// Obtiene las materias primas y las cantidades necesarias para fabricar una cocina dada.
        /// </summary>
        /// <param name="codigoCocina">El código de la cocina.</param>
        /// <returns>Un array decimal[,] donde las filas representan materias primas, la primer columna ([n,0]) es el
        /// código y la segunda ([n,1]) es la cantidad necesaria de esa materia prima para fabricar la cocina dada.</returns>
        public static decimal[,] ObtenerMateriasPrimasYCantidades(int codigoCocina)
        {
            int codigoEstructura = BLL.CocinaBLL.ObtenerCodigoEstructuraActiva(codigoCocina);
            Data.dsEstructura dsEstructura = new GyCAP.Data.dsEstructura();
            ObtenerEstructura(codigoEstructura, dsEstructura, true);

            decimal[,] listaMateriasPrimas = new decimal[dsEstructura.MATERIAS_PRIMAS.Count,2];
            int indice = 0;
            foreach (Data.dsEstructura.MATERIAS_PRIMASRow row in dsEstructura.MATERIAS_PRIMAS.Rows)
            {
                listaMateriasPrimas[indice, 0] = row.MP_CODIGO;
                indice++;
            }

            foreach (Data.dsEstructura.CONJUNTOSXESTRUCTURARow rowCxE in dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).GetCONJUNTOSXESTRUCTURARows())
            {
                foreach (Data.dsEstructura.SUBCONJUNTOSXCONJUNTORow rowSCxC in rowCxE.CONJUNTOSRow.GetSUBCONJUNTOSXCONJUNTORows())
                {
                    foreach (Data.dsEstructura.PIEZASXSUBCONJUNTORow rowPxSC in rowSCxC.SUBCONJUNTOSRow.GetPIEZASXSUBCONJUNTORows())
                    {
                        foreach (Data.dsEstructura.MATERIASPRIMASXPIEZARow rowMPxP in rowPxSC.PIEZASRow.GetMATERIASPRIMASXPIEZARows())
                        {
                            indice = 0;
                            while (listaMateriasPrimas[indice, 0] != rowMPxP.MATERIAS_PRIMASRow.MP_CODIGO) { indice++; }
                            listaMateriasPrimas[indice, 1] += rowCxE.CXE_CANTIDAD * rowSCxC.SCXCJ_CANTIDAD * rowPxSC.PXSC_CANTIDAD * rowMPxP.MPXP_CANTIDAD;
                        }
                    }
                }

                foreach (Data.dsEstructura.PIEZASXCONJUNTORow rowPxC in rowCxE.CONJUNTOSRow.GetPIEZASXCONJUNTORows())
                {
                    foreach (Data.dsEstructura.MATERIASPRIMASXPIEZARow rowMPxP in rowPxC.PIEZASRow.GetMATERIASPRIMASXPIEZARows())
                    {
                        indice = 0;
                        while (listaMateriasPrimas[indice, 0] != rowMPxP.MATERIAS_PRIMASRow.MP_CODIGO) { indice++; }
                        listaMateriasPrimas[indice, 1] += rowCxE.CXE_CANTIDAD * rowPxC.PXCJ_CANTIDAD * rowMPxP.MPXP_CANTIDAD;
                    }
                }
            }

            foreach (Data.dsEstructura.PIEZASXESTRUCTURARow rowPxE in dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).GetPIEZASXESTRUCTURARows())
            {
                foreach (Data.dsEstructura.MATERIASPRIMASXPIEZARow rowMPxP in rowPxE.PIEZASRow.GetMATERIASPRIMASXPIEZARows())
                {
                    indice = 0;
                    while (listaMateriasPrimas[indice, 0] != rowMPxP.MATERIAS_PRIMASRow.MP_CODIGO) { indice++; }
                    listaMateriasPrimas[indice, 1] += rowPxE.PXE_CANTIDAD * rowMPxP.MPXP_CANTIDAD;
                }
            }

            return listaMateriasPrimas;

        }
    }
}
