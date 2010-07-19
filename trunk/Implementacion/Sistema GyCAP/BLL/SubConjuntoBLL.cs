using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class SubConjuntoBLL
    {
        public static void Insertar(Data.dsEstructura dsEstructura)
        {
            //Si existe lanzamos la excepción correspondiente
            Entidades.SubConjunto subconjunto = new GyCAP.Entidades.SubConjunto ();
            //Así obtenemos el subconjunto nuevo del dataset, indicamos la primer fila de la agregadas ya que es una sola y convertimos al tipo correcto
            Data.dsEstructura.SUBCONJUNTOSRow rowSubconjunto = dsEstructura.SUBCONJUNTOS.GetChanges(System.Data.DataRowState.Added).Rows[0] as Data.dsEstructura.SUBCONJUNTOSRow;
            subconjunto.CodigoSubconjunto = Convert.ToInt32(rowSubconjunto.SCONJ_CODIGO);
            subconjunto.Nombre = rowSubconjunto.SCONJ_NOMBRE;
            subconjunto.CodigoTerminacion = Convert.ToInt32(rowSubconjunto.TE_CODIGO);
            subconjunto.Descripcion = rowSubconjunto.SCONJ_DESCRIPCION;
            if (EsSubconjunto(subconjunto)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            DAL.SubConjuntoDAL.Insertar(dsEstructura);
        }

        public static void Eliminar(int codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.SubConjuntoDAL.PuedeEliminarse(codigo))
            {
                //Puede eliminarse
                DAL.SubConjuntoDAL.Eliminar(codigo);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }

        }

        public static void Actualizar(Data.dsEstructura dsEstructura)
        {
            DAL.SubConjuntoDAL.Actualizar(dsEstructura);
        }

        public static void ActualizarStock(int codigoSubconjunto, int cantidad)
        {
            if (cantidad >= 0)
            {
                DAL.SubConjuntoDAL.ActualizarStock(codigoSubconjunto, cantidad);
            }
            else
            {
                throw new Entidades.Excepciones.ValorNoNegativoException();
            }
        }

        //Comprueba si existe un subconjunto dado su nombre y terminación
        public static bool EsSubconjunto(Entidades.SubConjunto subconjunto)
        {
            return DAL.SubConjuntoDAL.EsSubConjunto(subconjunto);
        }
        
        /// <summary>
        /// Obitiene un subconjunto por su código.
        /// </summary>
        /// <param name="codigoSubconjunto">El código del subconjunto deseado.</param>
        /// <returns>El objeto subconjunto con sus datos.</returns>
        /// <exception cref="ElementoInexistenteException">En caso de que no exista el subconjunto.</exception>
        /// <exception cref="BaseDeDatosException">En caso de problemas con la base de datos.</exception>
        public static Entidades.SubConjunto ObtenerSubconjunto(int codigoSubconjunto)
        {
            return DAL.SubConjuntoDAL.ObtenerSubconjunto(codigoSubconjunto);
        }

        public static void ObtenerTodos(Data.dsEstructura ds)
        {
            DAL.SubConjuntoDAL.ObtenerSubconjuntos(ds);
            //Ya tenemos los subconjuntos, ahora necesitamos las piezas que los forman
            foreach (Data.dsEstructura.SUBCONJUNTOSRow rowSubconjunto in ds.SUBCONJUNTOS)
            {
                ObtenerEstructura(Convert.ToInt32(rowSubconjunto.SCONJ_CODIGO.ToString()), ds);
            }
        }

        public static void ObtenerTodos(string nombre, Data.dsEstructura ds)
        {
            DAL.SubConjuntoDAL.ObtenerSubconjuntos(nombre, ds);
            //Ya tenemos los subconjuntos, ahora necesitamos las piezas que los forman
            foreach (Data.dsEstructura.SUBCONJUNTOSRow rowSubconjunto in ds.SUBCONJUNTOS)
            {
                ObtenerEstructura(Convert.ToInt32(rowSubconjunto.SCONJ_CODIGO.ToString()), ds);
            }
        }

        public static void ObtenerTodos(int codigoTerminacion, Data.dsEstructura ds)
        {
            DAL.SubConjuntoDAL.ObtenerSubconjuntos(codigoTerminacion, ds);
            //Ya tenemos los subconjuntos, ahora necesitamos las piezas que los forman
            foreach (Data.dsEstructura.SUBCONJUNTOSRow rowSubconjunto in ds.SUBCONJUNTOS)
            {
                ObtenerEstructura(Convert.ToInt32(rowSubconjunto.SCONJ_CODIGO.ToString()), ds);
            }
        }

        /// <summary>
        /// Obtiene todas las piezas que forman el subconjunto.
        /// </summary>
        /// <param name="ds">El dataset del tipo dsEstructura.</param>
        /// <param name="codigoConjunto">El código del subconjunto.</param>
        /// <exception cref="ElementoInexistenteException">Si no existe el subconjunto con el código ingresado.</exception>
        /// <exception cref="BaseDeDatosException">En caso de problemas con la base de datos.</exception>
        public static void ObtenerEstructura(int codigoSubconjunto, Data.dsEstructura ds)
        {
            Entidades.SubConjunto subconjunto = new GyCAP.Entidades.SubConjunto();
            subconjunto.CodigoSubconjunto = codigoSubconjunto;
            if (!EsSubconjunto(subconjunto)) { throw new Entidades.Excepciones.ElementoInexistenteException(); }
            DAL.SubConjuntoDAL.ObtenerEstructura(codigoSubconjunto, ds);
        }
    }
}
