using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class PiezaBLL
    {
        public static void Insertar(Data.dsEstructura dsEstructura)
        {
            //Si existe lanzamos la excepción correspondiente
            Entidades.Pieza pieza = new GyCAP.Entidades.Pieza();
            //Así obtenemos la pieza nueva del dataset, indicamos la primer fila de la agregadas ya que es una sola y convertimos al tipo correcto
            Data.dsEstructura.PIEZASRow rowPieza = dsEstructura.PIEZAS.GetChanges(System.Data.DataRowState.Added).Rows[0] as Data.dsEstructura.PIEZASRow;
            pieza.CodigoPieza = Convert.ToInt32(rowPieza.PZA_CODIGO);
            pieza.Nombre = rowPieza.PZA_NOMBRE;
            pieza.CodigoTerminacion = Convert.ToInt32(rowPieza.TE_CODIGO);
            if (EsPieza(pieza)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            DAL.PiezaDAL.Insertar(dsEstructura);
        }

        public static void Eliminar(int codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.PiezaDAL.PuedeEliminarse(codigo))
            {
                //Puede eliminarse
                DAL.PiezaDAL.Eliminar(codigo);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }

        }

        public static void Actualizar(Data.dsEstructura dsEstructura)
        {
            DAL.PiezaDAL.Actualizar(dsEstructura);
        }

        public static void ActualizarStock(int codigoPieza, int cantidad)
        {
            if (cantidad >= 0)
            {
                DAL.PiezaDAL.ActualizarStock(codigoPieza, cantidad);
            }
            else
            {
                throw new Entidades.Excepciones.ValorNoNegativoException();
            }
        }

        //Comprueba si existe una pieza dado su nombre y terminación
        public static bool EsPieza(Entidades.Pieza pieza)
        {
            return DAL.PiezaDAL.EsPieza(pieza);
        }

        /// <summary>
        /// Obtiene una pieza por su código.
        /// </summary>
        /// <param name="codigoPieza">El código de la pieza deseada.</param>
        /// <returns>El objeto pieza con sus datos.</returns>
        /// <exception cref="ElementoInexistenteException">En caso de que no exista la pieza.</exception>
        /// <exception cref="BaseDeDatosException">En caso de problemas con la base de datos.</exception>
        public static Entidades.Pieza ObtenerPieza(int codigoPieza)
        {
            return DAL.PiezaDAL.ObtenerPieza(codigoPieza);
        }

        public static void ObtenerTodos(Data.dsEstructura ds)
        {
            DAL.PiezaDAL.ObtenerPiezas(ds);
            //Ya tenemos las piezas, ahora necesitamos las materias primas que las forman
            foreach (Data.dsEstructura.PIEZASRow rowPieza in ds.PIEZAS)
            {
                ObtenerEstructura(Convert.ToInt32(rowPieza.PZA_CODIGO.ToString()), ds);
            }
        }

        public static void ObtenerTodos(string nombre, Data.dsEstructura ds)
        {
            DAL.PiezaDAL.ObtenerPiezas(nombre, ds);
            //Ya tenemos las piezas, ahora necesitamos las materias primas que las forman
            foreach (Data.dsEstructura.PIEZASRow rowPieza in ds.PIEZAS)
            {
                ObtenerEstructura(Convert.ToInt32(rowPieza.PZA_CODIGO.ToString()), ds);
            }
        }

        public static void ObtenerTodos(int codigoTerminacion, Data.dsEstructura ds)
        {
            DAL.PiezaDAL.ObtenerPiezas(codigoTerminacion, ds);
            //Ya tenemos las piezas, ahora necesitamos las materias primas que las forman
            foreach (Data.dsEstructura.PIEZASRow rowPieza in ds.PIEZAS)
            {
                ObtenerEstructura(Convert.ToInt32(rowPieza.PZA_CODIGO.ToString()), ds);
            }
        }

        /// <summary>
        /// Obtiene todas las materia primas que forman la pieza.
        /// </summary>
        /// <param name="ds">El dataset del tipo dsEstructura.</param>
        /// <param name="codigoConjunto">El código de la pieza.</param>
        /// <exception cref="ElementoInexistenteException">Si no existe la pieza con el código ingresado.</exception>
        /// <exception cref="BaseDeDatosException">En caso de problemas con la base de datos.</exception>
        public static void ObtenerEstructura(int codigoPieza, Data.dsEstructura ds)
        {
            Entidades.Pieza pieza = new GyCAP.Entidades.Pieza();
            pieza.CodigoPieza = codigoPieza;
            if (!EsPieza(pieza)) { throw new Entidades.Excepciones.ElementoInexistenteException(); }
            DAL.PiezaDAL.ObtenerEstructura(codigoPieza, ds);
        }
    }
}
