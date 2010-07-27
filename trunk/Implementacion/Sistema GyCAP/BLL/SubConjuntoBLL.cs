using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GyCAP.BLL
{
    public class SubConjuntoBLL
    {
        /// <summary>
        /// Setea el directorio que contiene las imágenes de los subconjuntos en base al directorio en que
        /// se está ejecutando la aplicación.
        /// </summary>
        private static readonly string directorioImagenes = SistemaBLL.WorkingPath + "BLL\\Img\\SCImg\\";
        
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
            //Primero armamos el código de la parte si no lo tiene
            if (rowSubconjunto.SCONJ_CODIGOPARTE == string.Empty)
            {
                rowSubconjunto.SCONJ_CODIGOPARTE = "SC" + rowSubconjunto.SCONJ_CODIGO + "T" + rowSubconjunto.TE_CODIGO + "P" + rowSubconjunto.PNO_CODIGO;
            }
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

        public static void ObtenerSubconjuntos(object nombre, object codTerminacion, Data.dsEstructura ds, bool obtenerDetalle)
        {
            if (codTerminacion != null && Convert.ToInt32(codTerminacion.ToString()) <= 0) { codTerminacion = null; }
            DAL.SubConjuntoDAL.ObtenerSubconjuntos(nombre, codTerminacion, ds, obtenerDetalle);
        }

        public static void ObtenerSubconjuntos(System.Data.DataTable dtSubconjuntos)
        {
            DAL.SubConjuntoDAL.ObtenerSubconjuntos(dtSubconjuntos);
        }
        
        /*public static void ObtenerTodos(Data.dsEstructura ds)
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
        }*/

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

        /// <summary>
        /// Guarda una imagen de un subconjunto, si ya tiene una almacenada ésta se reemplaza.
        /// Si se llama al método sin pasar la imagen, se guarda una por defecto con la leyenda
        /// imagen no disponible.
        /// </summary>
        /// <param name="codigoSubConjunto">El código del subconjunto cuya imagen se quiere guardar.</param>
        /// <param name="imagen">La imagen del subconjunto.</param>
        public static void GuardarImagen(int codigoSubConjunto, Image imagen)
        {
            if (imagen == null) { imagen = BLL.Properties.Resources.sinimagen; }
            string nombreImagen = "SC" + codigoSubConjunto + ".jpg";
            imagen.Save(nombreImagen, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        /// <summary>
        /// Obtiene la imagen de un subconjunto, en caso de no tenerla retorna una imagen por defecto con
        /// la leyenda sin imagen.
        /// </summary>
        /// <param name="codigoSubConjunto">El código del subconjunto cuya imagen se quiere obtener.</param>
        /// <returns>El objeto image con la imagen del subconjunto si la tiene, caso contrario una imagen por defecto.</returns>
        public static Image ObtenerImagen(int codigoSubConjunto)
        {
            try
            {
                Image imagen = Image.FromFile(directorioImagenes + "SC" + codigoSubConjunto + ".jpg");
                return imagen;
            }
            catch (System.IO.FileNotFoundException) { return BLL.Properties.Resources.sinimagen; }
        }
    }
}
