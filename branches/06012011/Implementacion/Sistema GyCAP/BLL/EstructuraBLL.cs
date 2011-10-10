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
            if (codPlano != null && Convert.ToInt32(codPlano.ToString()) <= 0) { codPlano = null; }
            if (codCocina != null && Convert.ToInt32(codCocina.ToString()) <= 0) { codCocina = null; }
            if (codResponsable != null && Convert.ToInt32(codResponsable.ToString()) <= 0) { codResponsable = null; }
            if (activoSiNo != null && Convert.ToInt32(activoSiNo) < 0) { activoSiNo = null; }
            DAL.EstructuraDAL.ObtenerEstructuras(nombre, codPlano, fechaCreacion, codCocina, codResponsable, activoSiNo, ds);
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
            //Obtenemos la parte padre de nivel 0 y la agregamos al árbol
            string filtro = "estr_codigo = " + codigoEstructura + " AND comp_codigo_padre IS NULL";
            Data.dsEstructuraProducto.COMPUESTOS_PARTESRow rowCompuestoInicio = (dsEstructura.COMPUESTOS_PARTES.Select(filtro) as Data.dsEstructuraProducto.COMPUESTOS_PARTESRow[])[0];
            TreeNode nodoInicio = new TreeNode();
            if (clonar) 
            {
                nodoInicio.Name = (rowCompuestoInicio.COMP_CODIGO * -1).ToString();
                Data.dsEstructuraProducto.COMPUESTOS_PARTESRow rowCompInicioCloned = dsEstructura.COMPUESTOS_PARTES.NewCOMPUESTOS_PARTESRow();
                rowCompInicioCloned.BeginEdit();
                rowCompInicioCloned.COMP_CODIGO = rowCompuestoInicio.COMP_CODIGO * -1;
                rowCompInicioCloned.COMP_CANTIDAD = rowCompuestoInicio.COMP_CANTIDAD;
                rowCompInicioCloned.ESTR_CODIGO = -1;
                rowCompInicioCloned.SetMP_CODIGONull();
                rowCompInicioCloned.PART_NUMERO = rowCompuestoInicio.PART_NUMERO;
                rowCompInicioCloned.SetCOMP_CODIGO_PADRENull();
                rowCompInicioCloned.UMED_CODIGO = rowCompuestoInicio.UMED_CODIGO;
                rowCompInicioCloned.EndEdit();
                dsEstructura.COMPUESTOS_PARTES.AddCOMPUESTOS_PARTESRow(rowCompInicioCloned);
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
                
                if (clonar) { nodo.Name = (rowComp.COMP_CODIGO * -1).ToString(); }
                else { nodo.Name = rowComp.COMP_CODIGO.ToString(); }

                nodo.Text = ((rowComp.IsMP_CODIGONull()) ? (rowComp.PARTESRow.PART_NOMBRE + " - " + rowComp.PARTESRow.PART_CODIGO) : (rowComp.MATERIAS_PRIMASRow.MP_NOMBRE));
                nodo.Text += " / #" + rowComp.COMP_CANTIDAD.ToString() + " " + rowComp.UNIDADES_MEDIDARow.UMED_ABREVIATURA;
                nodo.Tag = ((rowComp.IsMP_CODIGONull()) ? BLL.CompuestoParteBLL.HijoEsParte : BLL.CompuestoParteBLL.HijoEsMP);
                arbol.Nodes.Find((clonar) ? (rowComp.COMP_CODIGO_PADRE * -1).ToString() : rowComp.COMP_CODIGO_PADRE.ToString(), true)[0].Nodes.Add(nodo);
                if (clonar)
                {
                    Data.dsEstructuraProducto.COMPUESTOS_PARTESRow rowCompCloned = dsEstructura.COMPUESTOS_PARTES.NewCOMPUESTOS_PARTESRow();
                    rowCompCloned.BeginEdit();
                    rowCompCloned.COMP_CODIGO = rowComp.COMP_CODIGO * -1;
                    rowCompCloned.COMP_CANTIDAD = rowComp.COMP_CANTIDAD;
                    rowCompCloned.ESTR_CODIGO = -1;
                    if (rowComp.IsMP_CODIGONull()) { rowCompCloned.SetMP_CODIGONull(); rowCompCloned.PART_NUMERO = rowComp.PART_NUMERO; }
                    else { rowCompCloned.SetMP_CODIGONull(); rowCompCloned.MP_CODIGO = rowComp.MP_CODIGO; }
                    rowCompCloned.UMED_CODIGO = rowComp.UMED_CODIGO;
                    rowCompCloned.COMP_CODIGO_PADRE = rowComp.COMP_CODIGO_PADRE * -1;
                    rowCompCloned.EndEdit();
                    dsEstructura.COMPUESTOS_PARTES.AddCOMPUESTOS_PARTESRow(rowCompCloned);
                }
            }
            
            arbol.ExpandAll();

            lastCompID = (clonar) ? Convert.ToInt32(dsEstructura.COMPUESTOS_PARTES.Min(p => p.COMP_CODIGO) - 1) : -1;
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
                                     MateriaPrima = ((rowComp.IsMP_CODIGONull()) ? null : BLL.MateriaPrimaBLL.AsMateriaPrimaEntity(Convert.ToInt32(rowComp.MP_CODIGO), dsEstructura)),
                                     Cantidad = rowComp.COMP_CANTIDAD,
                                     UnidadMedida = BLL.UnidadMedidaBLL.AsUnidadMedidaEntity(Convert.ToInt32(rowComp.UNIDADES_MEDIDARow.UMED_CODIGO), dsEstructura),
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
                UbicacionStockBLL.ObtenerUbicacionesStock(ds.UBICACIONES_STOCK);
                TipoUbicacionStockBLL.ObtenerTiposUbicacionStock(ds.TIPOS_UBICACIONES_STOCK);
                ContenidoUbicacionStockBLL.ObtenerContenidosUbicacionStock(ds.CONTENIDO_UBICACION_STOCK);

                ArbolEstructura arbol = ArmarArbol(Convert.ToInt32(ds.ESTRUCTURAS.Rows[0]["estr_codigo"]), ds);
                arbol.NodoRaiz.Compuesto.Cantidad = cantidadCocina;
                return arbol.GetMPQuantityForStructure().ToList();
            }
            else
            {
                throw new Entidades.Excepciones.CocinaSinEstructuraActivaException();
            }
        }

        public static ArbolEstructura GetArbolEstructura(int codigoCocina, bool fillHojaRuta)
        {
            ArbolEstructura arbol = new ArbolEstructura(-1);

            Data.dsEstructuraProducto ds = new GyCAP.Data.dsEstructuraProducto();
            ObtenerEstructuras(null, null, null, codigoCocina, null, EstructuraActiva, ds);

            if (ds.ESTRUCTURAS.Rows.Count > 0)
            {
                UnidadMedidaBLL.ObtenerTodos(ds.UNIDADES_MEDIDA);
                TipoUnidadMedidaBLL.ObtenerTodos(ds.TIPOS_UNIDADES_MEDIDA);
                MateriaPrimaBLL.ObtenerMP(ds.MATERIAS_PRIMAS);
                ParteBLL.ObtenerPartes(null, null, null, null, null, null, ds.PARTES);
                EstadoParteBLL.ObtenerTodos(ds.ESTADO_PARTES);
                TipoParteBLL.ObtenerTodos(ds.TIPOS_PARTES);
                UbicacionStockBLL.ObtenerUbicacionesStock(ds.UBICACIONES_STOCK);
                TipoUbicacionStockBLL.ObtenerTiposUbicacionStock(ds.TIPOS_UBICACIONES_STOCK);
                ContenidoUbicacionStockBLL.ObtenerContenidosUbicacionStock(ds.CONTENIDO_UBICACION_STOCK);
                arbol = ArmarArbol(Convert.ToInt32(ds.ESTRUCTURAS.Rows[0]["estr_codigo"].ToString()), ds);

                ds.Dispose();

                if (fillHojaRuta) { FillHojasRutas(arbol); }
            }

            return arbol;
        }

        private static void FillHojasRutas(ArbolEstructura arbol)
        {
            Data.dsHojaRuta dsHoja = new GyCAP.Data.dsHojaRuta();

            UbicacionStockBLL.ObtenerUbicacionesStock(dsHoja.UBICACIONES_STOCK);
            TipoUbicacionStockBLL.ObtenerTiposUbicacionStock(dsHoja.TIPOS_UBICACIONES_STOCK);
            ContenidoUbicacionStockBLL.ObtenerContenidosUbicacionStock(dsHoja.CONTENIDO_UBICACION_STOCK);
            CentroTrabajoBLL.ObetenerCentrosTrabajo(null, null, null, null, dsHoja.CENTROS_TRABAJOS);
            OperacionBLL.ObetenerOperaciones(dsHoja.OPERACIONES);
            HojaRutaBLL.ObtenerHojasRuta(null, null, dsHoja, true);
            UnidadMedidaBLL.ObtenerTodos(dsHoja.UNIDADES_MEDIDA);
            TipoUnidadMedidaBLL.ObtenerTodos(dsHoja.TIPOS_UNIDADES_MEDIDA);
            TipoCentroTrabajoBLL.GetAll(dsHoja.TIPOS_CENTRO_TRABAJO);
            TurnoTrabajoBLL.ObtenerTurnos(dsHoja.TURNOS_TRABAJO);
            TurnoTrabajoBLL.ObtenerTurnosPorCentros(dsHoja.TURNOSXCENTROTRABAJO);

            IList<ParteNecesidadCombinada> listaPartes = arbol.AsListOfParts();

            foreach (ParteNecesidadCombinada parte in listaPartes)
            {
                if (parte.Parte.HojaRuta != null) { parte.Parte.HojaRuta = HojaRutaBLL.AsHojaRutaEntity(parte.Parte.HojaRuta.Codigo, dsHoja); }
            }
            
            dsHoja.Dispose();
        }
        
        public static IList<CapacidadNecesidadCombinada> AsListForCapacity(int codigoCocina)
        {
            return GetArbolEstructura(codigoCocina, false).AsListForCapacity();
        }
    }
}
