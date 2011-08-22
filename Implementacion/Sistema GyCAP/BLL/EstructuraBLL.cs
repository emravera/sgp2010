using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GyCAP.Entidades;
using GyCAP.Entidades.ArbolEstructura;

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
            IList<MPEstructura> lista = MateriasPrimasCocina(codigoCocina, 1);

            decimal[,] mps = new decimal[lista.Count, 2];

            for (int i = 0; i < lista.Count; i++)
            {
                mps[i, 0] = lista[i].MateriaPrima.CodigoMateriaPrima;
                mps[i, 1] = lista[i].Cantidad;
            }

            return mps;

        }

        public static void CrearArbolEstructura(int codigoEstructura, Data.dsEstructuraProducto dsEstructura, TreeView arbol, bool clonar, out int lastCompID)
        {
            arbol.Nodes.Clear();
            lastCompID = -1;
            //Obtenemos la parte padre de nivel 0 y la agregamos al árbol
            string filtro = "estr_codigo = " + codigoEstructura + " AND comp_codigo_padre IS NULL";
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
                rowCompInicioCloned.PART_NUMERO = rowCompuestoInicio.PART_NUMERO;
                rowCompInicioCloned.SetCOMP_CODIGO_PADRENull();
                rowCompInicioCloned.UMED_CODIGO = rowCompuestoInicio.UMED_CODIGO;
                rowCompInicioCloned.EndEdit();
                dsEstructura.COMPUESTOS_PARTES.AddCOMPUESTOS_PARTESRow(rowCompInicioCloned);
                lastCompID--;
            }
            else { nodoInicio.Name = rowCompuestoInicio.COMP_CODIGO.ToString(); }
            
            nodoInicio.Text = rowCompuestoInicio.PARTESRow.PART_NOMBRE + " - " + rowCompuestoInicio.PARTESRow.PART_CODIGO;            
            nodoInicio.Tag = BLL.CompuestoParteBLL.HijoEsParte;
            arbol.Nodes.Add(nodoInicio);

            //Ahora creamos todas las ramas con sus hojas
            filtro = "estr_codigo = " + codigoEstructura + " AND comp_codigo_padre IS NOT NULL";
            foreach (Data.dsEstructuraProducto.COMPUESTOS_PARTESRow rowComp in (Data.dsEstructuraProducto.COMPUESTOS_PARTESRow[])dsEstructura.COMPUESTOS_PARTES.Select(filtro))
            {
                TreeNode nodo = new TreeNode();
                if (clonar) { nodo.Name = lastCompID.ToString(); }
                else { nodo.Name = rowComp.COMP_CODIGO.ToString(); }
                nodo.Text = ((rowComp.IsMP_CODIGONull()) ? (rowComp.PARTESRow.PART_NOMBRE + " - " + rowComp.PARTESRow.PART_CODIGO) : (rowComp.MATERIAS_PRIMASRow.MP_NOMBRE));
                nodo.Text += " / #" + rowComp.COMP_CANTIDAD.ToString() + " " + rowComp.UNIDADES_MEDIDARow.UMED_ABREVIATURA;
                nodo.Tag = ((rowComp.IsMP_CODIGONull()) ? BLL.CompuestoParteBLL.HijoEsParte : BLL.CompuestoParteBLL.HijoEsMP);
                string key = (dsEstructura.COMPUESTOS_PARTES.Select("estr_codigo = " + ((clonar)? "-1" : codigoEstructura.ToString()) + " AND comp_codigo_padre = " + rowComp.COMP_CODIGO_PADRE.ToString()) as Data.dsEstructuraProducto.COMPUESTOS_PARTESRow[])[0].COMP_CODIGO.ToString();
                arbol.Nodes.Find(rowComp.COMP_CODIGO_PADRE.ToString(), true)[0].Nodes.Add(nodo);
                if (clonar)
                {
                    Data.dsEstructuraProducto.COMPUESTOS_PARTESRow rowCompCloned = dsEstructura.COMPUESTOS_PARTES.NewCOMPUESTOS_PARTESRow();
                    rowCompCloned.BeginEdit();
                    rowCompCloned.COMP_CODIGO = lastCompID;
                    rowCompCloned.COMP_CANTIDAD = rowComp.COMP_CANTIDAD;
                    rowCompCloned.ESTR_CODIGO = -1;
                    if (rowComp.IsMP_CODIGONull()) { rowCompCloned.SetMP_CODIGONull(); rowCompCloned.PART_NUMERO = rowComp.PART_NUMERO; }
                    else { rowCompCloned.SetMP_CODIGONull(); rowCompCloned.MP_CODIGO = rowComp.MP_CODIGO; }
                    rowCompCloned.UMED_CODIGO = rowComp.UMED_CODIGO;
                    rowCompCloned.COMP_CODIGO_PADRE = rowComp.COMP_CODIGO_PADRE;
                    rowCompCloned.EndEdit();
                    dsEstructura.COMPUESTOS_PARTES.AddCOMPUESTOS_PARTESRow(rowCompCloned);
                    lastCompID--;
                }
            }
            
            arbol.ExpandAll();            
        }

        public static ArbolEstructura ArmarArbol(int codigoEstructura, Data.dsEstructuraProducto dsEstructura)
        {
            ArbolEstructura arbolEstructura = new ArbolEstructura(codigoEstructura);

            string filtro = "estr_codigo = " + codigoEstructura + " AND comp_codigo_padre IS NULL";
            Data.dsEstructuraProducto.COMPUESTOS_PARTESRow rowCompuestoInicio = (dsEstructura.COMPUESTOS_PARTES.Select(filtro) as Data.dsEstructuraProducto.COMPUESTOS_PARTESRow[])[0];

            NodoEstructura nodoRaiz = new NodoEstructura();

            nodoRaiz.CodigoNodo = arbolEstructura.GetNextCodigoNodo();
            nodoRaiz.Contenido = NodoEstructura.tipoContenido.ProductoFinal;
            nodoRaiz.Compuesto = new CompuestoParte()
                                            {
                                                Codigo = Convert.ToInt32(rowCompuestoInicio.COMP_CODIGO),
                                                CompuestoPadre = null,
                                                Parte = BLL.ParteBLL.AsParteEntity(Convert.ToInt32(rowCompuestoInicio.PART_NUMERO), dsEstructura),
                                                MateriaPrima = null,
                                                Cantidad = rowCompuestoInicio.COMP_CANTIDAD,
                                                UnidadMedida = BLL.UnidadMedidaBLL.AsUnidadMedidaEntity(Convert.ToInt32(rowCompuestoInicio.UMED_CODIGO), dsEstructura),
                                                Estructura = null
                                            };

            nodoRaiz.Text = rowCompuestoInicio.PARTESRow.PART_NOMBRE;
            nodoRaiz.NodoPadre = null;
            arbolEstructura.AddRaiz(nodoRaiz);
            
            foreach (Data.dsEstructuraProducto.COMPUESTOS_PARTESRow rowComp in (Data.dsEstructuraProducto.COMPUESTOS_PARTESRow[])dsEstructura.COMPUESTOS_PARTES.Select
                    ("estr_codigo = " + codigoEstructura + " AND comp_codigo_padre IS NOT NULL"))
            {
                NodoEstructura nodo = new NodoEstructura();
                nodo.CodigoNodo = Convert.ToInt32(rowComp.COMP_CODIGO);
                nodo.Text = ((rowComp.IsMP_CODIGONull()) ? (rowComp.PARTESRow.PART_NOMBRE + " - " + rowComp.PARTESRow.PART_CODIGO) : (rowComp.MATERIAS_PRIMASRow.MP_NOMBRE));
                nodo.Contenido = ((rowComp.IsMP_CODIGONull()) ? NodoEstructura.tipoContenido.Parte : NodoEstructura.tipoContenido.MateriaPrima);
                nodo.Compuesto = new CompuestoParte()
                                {
                                     Codigo = Convert.ToInt32(rowComp.COMP_CODIGO),
                                     Parte = ((rowComp.IsMP_CODIGONull()) ? BLL.ParteBLL.AsParteEntity(Convert.ToInt32(rowComp.PART_NUMERO), dsEstructura) : null),
                                     MateriaPrima = ((rowComp.IsMP_CODIGONull()) ? null : BLL.MateriaPrimaBLL.AsMateriaPrimaEntity(rowComp.MATERIAS_PRIMASRow)),
                                     Cantidad = rowComp.COMP_CANTIDAD,
                                     UnidadMedida = BLL.UnidadMedidaBLL.AsUnidadMedidaEntity(Convert.ToInt32(rowComp.UNIDADES_MEDIDARow), dsEstructura),
                                     Estructura = null,
                                     CompuestoPadre  = (rowComp.IsCOMP_CODIGO_PADRENull()) ? null : BLL.CompuestoParteBLL.AsCompuestoParteEntity(Convert.ToInt32(rowComp.COMPUESTOS_PARTESRowParent.COMP_CODIGO), dsEstructura)
                                };
                
                arbolEstructura.AddNodo(nodo, Convert.ToInt32(rowComp.COMP_CODIGO_PADRE));
            }

            return arbolEstructura;
        }

        public static List<Entidades.MPEstructura> MateriasPrimasCocina(int codigoCocina, decimal cantidadCocina)
        {
            Data.dsEstructuraProducto ds = new GyCAP.Data.dsEstructuraProducto();

            DAL.EstructuraDAL.ObtenerEstructuras(null, null, null, codigoCocina, null, EstructuraActiva, ds);

            if (ds.ESTRUCTURAS.Rows.Count > 0)
            {
                if (ds.COMPUESTOS_PARTES.Select(p => !p.IsMP_CODIGONull()).Count() == 0) { throw new Entidades.Excepciones.EstructuraSinMateriaPrimaException(); }
                
                TerminacionBLL.ObtenerTodos(string.Empty, ds.TERMINACIONES);
                PlanoBLL.ObtenerTodos(ds.PLANOS);
                ParteBLL.ObtenerPartes(null, null, null, null, null, null, ds.PARTES);
                MateriaPrimaBLL.ObtenerMP(ds.MATERIAS_PRIMAS);
                UnidadMedidaBLL.ObtenerTodos(ds.UNIDADES_MEDIDA);
                TipoParteBLL.ObtenerTodos(ds.TIPOS_PARTES);
                EstadoParteBLL.ObtenerTodos(ds.ESTADO_PARTES);
                HojaRutaBLL.ObtenerHojasRuta(ds.HOJAS_RUTA);
                TipoUnidadMedidaBLL.ObtenerTodos(ds.TIPOS_UNIDADES_MEDIDA);

                ArbolEstructura arbol = ArmarArbol(Convert.ToInt32(ds.ESTRUCTURAS.Rows[0]["estr_codigo"]), ds);
                arbol.NodoRaiz.Compuesto.Cantidad = cantidadCocina;
                return arbol.GetMPQuantityForStructure().ToList();
            }
            else
            {
                throw new Entidades.Excepciones.CocinaSinEstructuraActivaException();
            }
        }
    }
}
