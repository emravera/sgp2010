using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class ConjuntoBLL
    {
        public static int Insertar(Entidades.Conjunto conjunto)
        {
            //Si existe lanzamos la excepción correspondiente
            if (EsConjunto(conjunto)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            return DAL.ConjuntoDAL.Insertar(conjunto);
        }

        public static void Eliminar(int codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.ConjuntoDAL.PuedeEliminarse(codigo))
            {
                //Puede eliminarse
                DAL.ConjuntoDAL.Eliminar(codigo);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }

        }

        public static void Actualizar(Entidades.Conjunto conjunto)
        {
            DAL.ConjuntoDAL.Actualizar(conjunto);
        }

        public static void ActualizarStock(int codigoConjunto, int cantidad)
        {
            if (cantidad >= 0)
            {
                DAL.ConjuntoDAL.ActualizarStock(codigoConjunto, cantidad);
            }
            else
            {
                throw new Entidades.Excepciones.ValorNoNegativoException();
            }
        }
        
        public static bool EsConjunto(Entidades.Conjunto conjunto)
        {
            return DAL.ConjuntoDAL.EsConjunto(conjunto);
        }

        /// <summary>
        /// Obtiene un conjunto por su código.
        /// </summary>
        /// <param name="codigoConjunto">El código del conjunto deseado.</param>
        /// <returns>El objeto conjunto con sus datos.</returns>
        /// <exception cref="ElementoInexistenteException">En caso de que no exista el conjunto.</exception>
        /// <exception cref="BaseDeDatosException">En caso de problemas con la base de datos.</exception>
        public static Entidades.Conjunto ObtenerConjunto(int codigoConjunto)
        {
            return DAL.ConjuntoDAL.ObtenerConjunto(codigoConjunto);
        }
        
        public static void ObtenerTodos(Data.dsEstructura ds)
        {
            DAL.ConjuntoDAL.ObtenerConjuntos(ds);
            //Ya tenemos los conjuntos, ahora necesitamos los subconjuntos que los forman
            foreach (Data.dsEstructura.CONJUNTOSRow rowConjunto in ds.CONJUNTOS)
            {
                ObtenerEstructura(Convert.ToInt32(rowConjunto.CONJ_CODIGO.ToString()), ds);
            }            
        }

        public static void ObtenerTodos(string nombre, Data.dsEstructura ds)
        {
            DAL.ConjuntoDAL.ObtenerConjuntos(nombre, ds);
            //Ya tenemos los conjuntos, ahora necesitamos los subconjuntos que los forman
            foreach (Data.dsEstructura.CONJUNTOSRow rowConjunto in ds.CONJUNTOS)
            {
                ObtenerEstructura(Convert.ToInt32(rowConjunto.CONJ_CODIGO.ToString()), ds);
            } 
        }

        public static void ObtenerTodos(int codigoTerminacion, Data.dsEstructura ds)
        {
            DAL.ConjuntoDAL.ObtenerConjuntos(codigoTerminacion, ds);
            //Ya tenemos los conjuntos, ahora necesitamos los subconjuntos que los forman
            foreach (Data.dsEstructura.CONJUNTOSRow rowConjunto in ds.CONJUNTOS)
            {
                ObtenerEstructura(Convert.ToInt32(rowConjunto.CONJ_CODIGO.ToString()), ds);
            } 
        }

        /// <summary>
        /// Obtiene todos los subconjuntos que forman el conjunto.
        /// </summary>
        /// <param name="ds">El dataset con las tablas CONJUNTOS y SUBCONJUNTOSXCONJUNTOS</param>
        /// <param name="codigoConjunto">El código del conjunto.</param>
        /// <exception cref="ElementoInexistenteException">Si no existe el conjunto con el código ingresado.</exception>
        /// <exception cref="BaseDeDatosException">En caso de problemas con la base de datos.</exception>
        public static void ObtenerEstructura(int codigoConjunto, Data.dsEstructura ds)
        {
            Entidades.Conjunto conjunto = new GyCAP.Entidades.Conjunto();
            conjunto.CodigoConjunto = codigoConjunto;
            if (!EsConjunto(conjunto)) { throw new Entidades.Excepciones.ElementoInexistenteException(); }
            DAL.ConjuntoDAL.ObtenerEstructura(codigoConjunto, ds);
        }
    }
}
