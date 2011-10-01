using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using GyCAP.Entidades;
using GyCAP.Entidades.BindingEntity;

namespace GyCAP.BLL
{
    public class MaquinaBLL
    {
        //Busqueda
        //Obtiene los datos de acuerdo a los criterios de busqueda
        public static void ObtenerTodos(object nombre, int idEstado, string cadFabricantes, string cadModelo, Data.dsMaquina ds)
        {
            DAL.MaquinaDAL.ObtenerMaquinas(nombre, idEstado, cadFabricantes,cadModelo, ds);
        }

        //Eliminacion
        public static void Eliminar(long codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.MaquinaDAL.PuedeEliminarse(codigo))
            {
                //Puede eliminarse
                DAL.MaquinaDAL.Eliminar(codigo);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }

        }

        //Guardado de Datos
        public static int Insertar(Entidades.Maquina maquina)
        {
            //Si existe lanzamos la excepción correspondiente
            if (EsMaquina(maquina)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            return DAL.MaquinaDAL.Insertar(maquina);
        }

        //Metodo que valida que no se este guardando algo que ya existe
        public static bool EsMaquina(Entidades.Maquina maquina)
        {
            return DAL.MaquinaDAL.esMaquina(maquina);
        }
        //Actualización de los datos
        public static void Actualizar(Entidades.Maquina maquina)
        {
            if (EsMaquina(maquina)) throw new Entidades.Excepciones.ElementoExistenteException();
            DAL.MaquinaDAL.Actualizar(maquina);
        }

        /// <summary>
        /// Obtiene todas las máquinas sin filtrar, los carga en una DataTable del tipo máquina.
        /// </summary>
        /// <param name="dtMaquina">La tabla donde cargar los datos.</param>
        public static void ObtenerMaquinas(DataTable dtMaquina)
        {
            DAL.MaquinaDAL.ObtenerMaquinas(dtMaquina);
        }

        public static void ObtenerMaquinas(Data.dsMantenimiento dsMantenimiento)
        {
            DAL.MaquinaDAL.ObtenerMaquinas(dsMantenimiento);
        }

        public static SortableBindingList<Maquina> GetAll()
        {
            Data.dsMaquina.MAQUINASDataTable dt = new GyCAP.Data.dsMaquina.MAQUINASDataTable();
            ObtenerMaquinas(dt);
            SortableBindingList<Maquina> lista = new SortableBindingList<Maquina>();

            foreach (Data.dsMaquina.MAQUINASRow row in dt.Rows)
            {
                Maquina maquina = new Maquina()
                {
                    Codigo = Convert.ToInt32(row.MAQ_CODIGO),
                    EsCritica = row.MAQ_ES_CRITICA,
                    Estado = new EstadoMaquina() { Codigo = Convert.ToInt32(row.EMAQ_CODIGO) },
                    Fabricante = new FabricanteMaquina() { Codigo = Convert.ToInt32(row.FAB_CODIGO) },
                    FechaAlta = row.MAQ_FECHAALTA,
                    Marca = row.MAQ_MARCA,
                    Modelo = new ModeloMaquina() { Codigo = Convert.ToInt32(row.MODM_CODIGO) },
                    Nombre = row.MAQ_NOMBRE,
                    NumeroSerie = row.MAQ_NUMEROSERIE
                };

                lista.Add(maquina);
            }

            return lista;
        }
    }
}
