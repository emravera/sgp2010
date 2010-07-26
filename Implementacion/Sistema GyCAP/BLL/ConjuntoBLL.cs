using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GyCAP.BLL
{
    public class ConjuntoBLL
    {
        /// <summary>
        /// Setea el directorio que contiene las imágenes de los conjuntos en base al directorio en que
        /// se está ejecutando la aplicación.
        /// </summary>
        private static readonly string directorioImagenes = SistemaBLL.WorkingPath + "BLL\\Img\\CImg\\";
        
        public static void Insertar(Data.dsEstructura dsEstructura)
        {
            //Si existe lanzamos la excepción correspondiente
            Entidades.Conjunto conjunto = new GyCAP.Entidades.Conjunto();
            //Así obtenemos el conjunto nuevo del dataset, indicamos la primer fila de la agregadas ya que es una sola y convertimos al tipo correcto
            Data.dsEstructura.CONJUNTOSRow rowConjunto = dsEstructura.CONJUNTOS.GetChanges(System.Data.DataRowState.Added).Rows[0] as Data.dsEstructura.CONJUNTOSRow;
            conjunto.CodigoConjunto = Convert.ToInt32(rowConjunto.CONJ_CODIGO);
            conjunto.Nombre = rowConjunto.CONJ_NOMBRE;
            conjunto.CodigoTerminacion = Convert.ToInt32(rowConjunto.TE_CODIGO);
            conjunto.Descripcion = rowConjunto.CONJ_DESCRIPCION;
            if (EsConjunto(conjunto)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            DAL.ConjuntoDAL.Insertar(dsEstructura);
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

        public static void Actualizar(Data.dsEstructura dsEstructura)
        {
            DAL.ConjuntoDAL.Actualizar(dsEstructura);
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
        
        //Comprueba si existe un conjunto dado su nombre y terminación
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

        public static void ObtenerConjuntos(object nombre, object codTerminacion, Data.dsEstructura ds, bool obtenerDetalle)
        {
            if (codTerminacion != null && Convert.ToInt32(codTerminacion.ToString()) <= 0) { codTerminacion = null; }
            DAL.ConjuntoDAL.ObtenerConjuntos(nombre, codTerminacion, ds, obtenerDetalle);
        }
        
        /*public static void ObtenerTodos(Data.dsEstructura ds)
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
        }*/

        /// <summary>
        /// Obtiene todos los subconjuntos que forman el conjunto.
        /// </summary>
        /// <param name="ds">El dataset del tipo dsEstructura.</param>
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

        /// <summary>
        /// Guarda una imagen de un conjunto, si ya tiene una almacenada ésta se reemplaza.
        /// Si se llama al método sin pasar la imagen, se guarda una por defecto con la leyenda
        /// imagen no disponible.
        /// </summary>
        /// <param name="codigoConjunto">El código del conjunto cuya imagen se quiere guardar.</param>
        /// <param name="imagen">La imagen del conjunto.</param>
        public static void GuardarImagen(int codigoConjunto, Image imagen)
        {
            if (imagen == null) { imagen = BLL.Properties.Resources.sinimagen; }
            string nombreImagen = "C" + codigoConjunto + ".jpg";
            imagen.Save(nombreImagen, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        /// <summary>
        /// Obtiene la imagen de un conjunto, en caso de no tenerla retorna una imagen por defecto con
        /// la leyenda sin imagen.
        /// </summary>
        /// <param name="codigoConjunto">El código del conjunto cuya imagen se quiere obtener.</param>
        /// <returns>El objeto image con la imagen del conjunto si la tiene, caso contrario una imagen por defecto.</returns>
        public static Image ObtenerImagen(int codigoConjunto)
        {
            try
            {
                Image imagen = Image.FromFile(directorioImagenes + "C" + codigoConjunto + ".jpg");
                return imagen;
            }
            catch (System.IO.FileNotFoundException) { return BLL.Properties.Resources.sinimagen; }            
        }
    }
}
