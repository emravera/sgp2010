using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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

        public static TreeView CrearArbolEstructura(int codigoEstructura, Data.dsEstructuraProducto dsEstructura, TreeView arbol, bool clonar, out int lastCompID)
        {
            arbol.Nodes.Clear();
            lastCompID = -1;
            //Obtenemos la parte padre de nivel 0 y la agregamos al árbol
            string filtro = "estr_codigo = " + codigoEstructura + " AND part_numero_padre IS NULL";
            Data.dsEstructuraProducto.COMPUESTOS_PARTESRow rowCompuestoInicio = (dsEstructura.COMPUESTOS_PARTES.Select(filtro) as Data.dsEstructuraProducto.COMPUESTOS_PARTESRow[])[0];
            TreeNode nodoInicio = new TreeNode();
            if (clonar) 
            { 
                nodoInicio.Name = lastCompID.ToString(); 
                Data.dsEstructuraProducto.COMPUESTOS_PARTESRow rowCompInicioCloned = dsEstructura.COMPUESTOS_PARTES.NewCOMPUESTOS_PARTESRow();
                rowCompInicioCloned.BeginEdit();
                rowCompInicioCloned.COMP_CODIGO = lastCompID;
                rowCompInicioCloned.COMP_CANTIDAD = rowCompuestoInicio.COMP_CANTIDAD;
                rowCompInicioCloned.ESTR_CODIGO = -1;
                rowCompInicioCloned.SetMP_CODIGONull();
                rowCompInicioCloned.PART_NUMERO_HIJO = rowCompuestoInicio.PART_NUMERO_HIJO;
                rowCompInicioCloned.SetPART_NUMERO_PADRENull();
                rowCompInicioCloned.UMED_CODIGO = rowCompuestoInicio.UMED_CODIGO;
                rowCompInicioCloned.EndEdit();
                dsEstructura.COMPUESTOS_PARTES.AddCOMPUESTOS_PARTESRow(rowCompInicioCloned);
                lastCompID--;
            }
            else { nodoInicio.Name = rowCompuestoInicio.COMP_CODIGO.ToString(); }
            nodoInicio.Text = rowCompuestoInicio.PARTESRowByFK_COMPUESTOS_PARTES_PARTES_HIJO.PART_NOMBRE + " - " + rowCompuestoInicio.PARTESRowByFK_COMPUESTOS_PARTES_PARTES_HIJO.PART_CODIGO;
            nodoInicio.Tag = BLL.CompuestoParteBLL.HijoEsParte;
            arbol.Nodes.Add(nodoInicio);

            //Ahora creamos todas las ramas con sus hojas
            foreach (Data.dsEstructuraProducto.COMPUESTOS_PARTESRow rowComp in (Data.dsEstructuraProducto.COMPUESTOS_PARTESRow[])dsEstructura.COMPUESTOS_PARTES.Select("estr_codigo = " + codigoEstructura + " AND part_numero_hijo <> " + rowCompuestoInicio.PART_NUMERO_HIJO.ToString()))
            {
                //ArmarRamas(codigoEstructura, rowComp.PART_NUMERO_HIJO, dsEstructura);
                TreeNode nodo = new TreeNode();
                if (clonar) { nodo.Name = lastCompID.ToString(); }
                else { nodo.Name = rowComp.COMP_CODIGO.ToString(); }
                nodo.Text = ((rowComp.IsMP_CODIGONull()) ? (rowComp.PARTESRowByFK_COMPUESTOS_PARTES_PARTES_HIJO.PART_NOMBRE + " - " + rowComp.PARTESRowByFK_COMPUESTOS_PARTES_PARTES_HIJO.PART_CODIGO) : (rowComp.MATERIAS_PRIMASRow.MP_NOMBRE));
                nodo.Text += " / #" + rowComp.COMP_CANTIDAD.ToString() + " " + rowComp.UNIDADES_MEDIDARow.UMED_ABREVIATURA;
                nodo.Tag = ((rowComp.IsMP_CODIGONull()) ? BLL.CompuestoParteBLL.HijoEsParte : BLL.CompuestoParteBLL.HijoEsMP);
                string key = (dsEstructura.COMPUESTOS_PARTES.Select("estr_codigo = " + ((clonar)? "-1" : codigoEstructura.ToString()) + " AND part_numero_hijo = " + rowComp.PART_NUMERO_PADRE.ToString()) as Data.dsEstructuraProducto.COMPUESTOS_PARTESRow[])[0].COMP_CODIGO.ToString();
                arbol.Nodes.Find(key, true)[0].Nodes.Add(nodo);
                if (clonar)
                {
                    Data.dsEstructuraProducto.COMPUESTOS_PARTESRow rowCompCloned = dsEstructura.COMPUESTOS_PARTES.NewCOMPUESTOS_PARTESRow();
                    rowCompCloned.BeginEdit();
                    rowCompCloned.COMP_CODIGO = lastCompID;
                    rowCompCloned.COMP_CANTIDAD = rowComp.COMP_CANTIDAD;
                    rowCompCloned.ESTR_CODIGO = -1;
                    if (rowComp.IsMP_CODIGONull()) { rowCompCloned.SetMP_CODIGONull(); rowCompCloned.PART_NUMERO_HIJO = rowComp.PART_NUMERO_HIJO; }
                    else { rowCompCloned.SetPART_NUMERO_HIJONull(); rowCompCloned.MP_CODIGO = rowComp.MP_CODIGO; }
                    rowCompCloned.PART_NUMERO_PADRE = rowComp.PART_NUMERO_PADRE;
                    rowCompCloned.UMED_CODIGO = rowComp.UMED_CODIGO;
                    rowCompCloned.EndEdit();
                    dsEstructura.COMPUESTOS_PARTES.AddCOMPUESTOS_PARTESRow(rowCompCloned);
                    lastCompID--;
                }
            }

            arbol.ExpandAll();
            return arbol;
        }        
    }
}
